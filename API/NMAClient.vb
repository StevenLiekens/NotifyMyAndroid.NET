#Region "LICENSE"
' Copyright 2013 Steven Liekens
' Contact: steven.liekens@gmail.com
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
'
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#End Region

Imports NotifyMyAndroid.API.Implementation

Namespace API
    ''' <summary>
    '''     Provides methods for interacting with the Notify My Android public API.
    ''' </summary>
    Public Class NMAClient
        Implements IDisposable

        Shared Sub New()
            SetUsage(800, TimeSpan.MaxValue)
        End Sub

        Private Sub New()
        End Sub

        Private Shared _instance As New Lazy(Of NMAClient)(Function() New NMAClient())

        ''' <summary>
        '''     Gets an instance.
        ''' </summary>
        Public Shared Function GetInstance() As NMAClient
            Return _instance.Value
        End Function

        ''' <summary>
        '''     Indicates whether to use HTTPS.
        ''' </summary>
        Public Shared Property UseSSL As Boolean = True

        Private Shared _developerKey As NMAKey

        ''' <summary>
        '''     Gets or sets a developer key.
        ''' </summary>
        ''' <remarks>A developer key lifts the hourly API call limit.</remarks>
        Public Shared Property DeveloperKey As NMAKey
            Get
                Return _developerKey
            End Get
            Set(value As NMAKey)
                _developerKey = value
            End Set
        End Property

        Private Shared _callsRemaining As Integer

        ''' <summary>
        '''     Indicates how many API calls can still be made using the current IP address.
        ''' </summary>
        ''' <remarks>
        '''     This value is synchronized with the server whenever an API call is made using the current object instance.
        '''     If other applications targetting the NMA API are active on the current IP address, this value may go out of sync with the actual amount of remaining calls.
        ''' </remarks>
        Public Shared Property CallsRemaining As Integer
            Get
                Return _callsRemaining
            End Get
            Private Set(value As Integer)
                _callsRemaining = value
            End Set
        End Property

        Private Shared _timeUntilReset As TimeSpan

        ''' <summary>
        '''     Indicates how many minutes before the remaining amount of API calls resets.
        ''' </summary>
        ''' <remarks>
        '''     This value is synchronised with the server whenever an API call is made using the current object instance.
        '''     However, <see cref="NMAClient" /> does try to calculate the actual value in between calls.
        ''' </remarks>
        Public Shared Property TimeUntilReset As TimeSpan
            Get
                If _timeUntilReset = TimeSpan.MaxValue Then
                    Return _timeUntilReset
                End If
                Dim estimatedTimeSince = Date.Now - LastUsageUpdate.Value

                If _timeUntilReset - estimatedTimeSince < TimeSpan.Zero Then
                    SetUsage(800, TimeSpan.MaxValue)
                    Return _timeUntilReset
                Else
                    Return TimeSpan.FromMinutes(Math.Ceiling((_timeUntilReset - estimatedTimeSince).TotalSeconds / 60))
                End If
            End Get
            Private Set(value As TimeSpan)
                _timeUntilReset = value
            End Set
        End Property

        ''' <summary>
        '''     Sends the specified notification to the specified recipient.
        ''' </summary>
        ''' <returns>
        '''     Returns an instance of <see cref="NMASuccess" /> or <see cref="NMAError" /> depending on whether the call was successful.
        ''' </returns>
        Public Function SendNotificationAsync(recipient As NMAKey, notification As Notification) As Task(Of NMAResponse)
            If recipient Is Nothing Then
                Throw New ArgumentNullException("recipient", "The recipient cannot be empty.")
            End If
            Dim ring As New KeyRing()
            ring.Add(recipient)
            Return SendNotificationAsync(ring, notification)
        End Function

        ''' <summary>
        '''     Sends the specified notification to all of the specified recipients.
        ''' </summary>
        ''' <returns>
        '''     Returns an instance of <see cref="NMASuccess" /> or <see cref="NMAError" /> depending on whether the call was successful.
        ''' </returns>
        Public Function SendNotificationAsync(recipients As KeyRing, notification As Notification) As Task(Of NMAResponse)
            If recipients Is Nothing Then
                Throw New ArgumentNullException("recipients", "The recipients cannot be empty.")
            End If
            If notification Is Nothing Then
                Throw New ArgumentNullException("notification", "The notification cannot be empty.")
            End If
            Return SendNotificationAsyncImplementation(recipients, notification)
        End Function

        ''' <summary>
        '''     Gets whether the specified key is valid.
        ''' </summary>
        ''' <returns>
        '''     Returns an instance of <see cref="NMASuccess" /> or <see cref="NMAError" /> depending on whether the call was successful.
        ''' </returns>
        Public Function VerifyAsync(key As NMAKey) As Task(Of NMAResponse)
            If key Is Nothing Then
                Throw New ArgumentNullException("key", "The key cannot be empty.")
            End If
            Return VerifyAsyncImplementation(key)
        End Function

        Private Shared ReadOnly UsageChangedEventHandlers As New Lazy(Of List(Of EventHandler(Of NMAUsageChangedEventArgs)))

        ''' <summary>
        '''     Occurs whenever <see cref="CallsRemaining" /> or <see cref="TimeUntilReset" /> are updated.
        ''' </summary>
        ''' <remarks>
        '''     This event typically occurs whenever an API call is made, because that's when values are synchronized with the server.
        ''' </remarks>
        Public Shared Custom Event NMAUsageChanged As EventHandler(Of NMAUsageChangedEventArgs)
            AddHandler(value As EventHandler(Of NMAUsageChangedEventArgs))
                If Not UsageChangedEventHandlers.Value.Contains(value) Then
                    UsageChangedEventHandlers.Value.Add(value)
                End If
            End AddHandler

            RemoveHandler(value As EventHandler(Of NMAUsageChangedEventArgs))
                If UsageChangedEventHandlers.Value.Contains(value) Then
                    UsageChangedEventHandlers.Value.Remove(value)
                End If
            End RemoveHandler

            RaiseEvent(sender As Object, e As NMAUsageChangedEventArgs)
                For Each handler In UsageChangedEventHandlers.Value
                    If handler IsNot Nothing Then
                        Task.Factory.FromAsync(handler.BeginInvoke(sender, e, Nothing, Nothing), AddressOf handler.EndInvoke)
                    End If
                Next
            End RaiseEvent
        End Event

#Region "Implementation details"

        Private _client As New Lazy(Of HttpClient)(False)

        Private ReadOnly Property HttpClient As HttpClient
            Get
                Return _client.Value
            End Get
        End Property

        Private Shared Property LastUsageUpdate As Date?

        Private Async Function SendNotificationAsyncImplementation(recipients As KeyRing, notification As Notification) As Task(Of NMAResponse)
            Using request As New NotifyRequestMessage
                request.Content = New NotificationContent(recipients, notification)
                Using response = Await HttpClient.SendAsync(request).ConfigureAwait(False)
                    Return Await HandleResponse(response).ConfigureAwait(False)
                End Using
            End Using
        End Function

        Private Async Function VerifyAsyncImplementation(key As NMAKey) As Task(Of NMAResponse)
            Using request As New VerifyRequestMessage(key)
                Using response = Await GetInstance.HttpClient.SendAsync(request).ConfigureAwait(False)
                    Return Await HandleResponse(response).ConfigureAwait(False)
                End Using
            End Using
        End Function

        Private Async Function HandleResponse(response As HttpResponseMessage) As Task(Of NMAResponse)
            response.EnsureSuccessStatusCode()
            Dim xml = XDocument.Load(Await response.Content.ReadAsStreamAsync().ConfigureAwait(False))
            Dim result = NMAResponse.GetResponse(xml)

            Dim time As TimeSpan = TimeUntilReset
            If result.TimeUntilReset.HasValue Then
                time = result.TimeUntilReset.Value
            ElseIf time = TimeSpan.MaxValue Then
                time = New TimeSpan(1, 0, 0)
            End If

            Dim calls As Integer = _callsRemaining
            If result.IsSuccessStatusCode Then
                calls = DirectCast(result, NMASuccess).CallsRemaining
            ElseIf result.StatusCode = StatusCode.LimitReached Then
                calls = 0
            Else
                calls -= 1
            End If
            SetUsage(calls, time)

            Return result
        End Function

        Private Shared Sub SetUsage(calls As Integer, time As TimeSpan)
            LastUsageUpdate = Date.Now
            CallsRemaining = calls
            TimeUntilReset = time
            RaiseEvent NMAUsageChanged(Nothing, New NMAUsageChangedEventArgs(calls, time))
        End Sub

        Friend Shared Function GetUriBuilder(command As NMACommand) As UriBuilder
            Dim builder As New UriBuilder
            If UseSSL Then
                builder.Scheme = "https"
            Else
                builder.Scheme = "http"
            End If
            builder.Host = "www.notifymyandroid.com"
            builder.Path = "/publicapi/" & command.Value
            Return builder
        End Function

#End Region

#Region "IDisposable Support"

        Private _disposedValue As Boolean ' To detect redundant calls

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then
                    _instance = New Lazy(Of NMAClient)(Function() New NMAClient())
                    If _client IsNot Nothing AndAlso _client.IsValueCreated Then
                        _client.Value.Dispose()
                    End If
                    _client = Nothing
                End If
            End If
            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class
End Namespace