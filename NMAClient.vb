Imports System.Net.Http

Namespace NotifyMyAndroid

    Public Class NMAClient
        Implements IDisposable

        Private Sub New()
            NMAClient.CallsRemaining = 800
            NMAClient.TimeUntilReset = TimeSpan.FromHours(1)
        End Sub

        Private Shared _instance As New Lazy(Of NMAClient)(Function() New NMAClient(), False)
        ''' <summary>
        ''' Gets an instance.
        ''' </summary>
        Public Shared Function GetInstance() As NMAClient
            Return _instance.Value
        End Function

        ''' <summary>
        ''' Indicates whether to use HTTPS.
        ''' </summary>
        Public Shared Property UseSSL As Boolean = True

        Private Shared _developerKey As ApiKey
        ''' <summary>
        ''' Gets or sets a developer key.
        ''' </summary>
        ''' <remarks>A developer key lifts the hourly API call limit.</remarks>
        Public Shared Property DeveloperKey As ApiKey
            Get
                Return _developerKey
            End Get
            Set(value As ApiKey)
                _developerKey = value
            End Set
        End Property

        Private Shared _callsRemaining As Integer
        ''' <summary>
        ''' Indicates the API calls quota associated with the current IP address since the last call.
        ''' </summary>
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
        ''' Indicates the amount of minutes since the last call until the remaining amount of API calls resets.
        ''' </summary>
        Public Shared Property TimeUntilReset As TimeSpan
            Get
                Return _timeUntilReset
            End Get
            Private Set(value As TimeSpan)
                _timeUntilReset = value
            End Set
        End Property

        Private _client As New Lazy(Of HttpClient)(False)
        Private ReadOnly Property HttpClient As HttpClient
            Get
                Return _client.Value
            End Get
        End Property

        ''' <summary>
        ''' Sends the specified notification to the specified recipient.
        ''' </summary>
        Public Function SendNotificationAsync(recipient As ApiKey, notification As Notification) As Task(Of NMAResponse)
            If recipient Is Nothing Then
                Throw New ArgumentNullException("recipient")
            End If
            Dim ring As New KeyRing()
            ring.Add(recipient)
            Return Me.SendNotificationAsync(ring, notification)
        End Function

        ''' <summary>
        ''' Sends the specified notification to all of the specified recipients.
        ''' </summary>
        Public Function SendNotificationAsync(recipients As KeyRing, notification As Notification) As Task(Of NMAResponse)
            If recipients Is Nothing Then
                Throw New ArgumentNullException("recipients")
            End If
            If notification Is Nothing Then
                Throw New ArgumentNullException("notification")
            End If
            Return Me.SendNotificationAsyncImplementation(recipients, notification)
        End Function

        Private Async Function SendNotificationAsyncImplementation(recipients As KeyRing, notification As Notification) As Task(Of NMAResponse)
            Using request As New Http.NotifyRequestMessage
                request.Content = New Http.NotificationContent(recipients, notification)
                Using response = Await Me.HttpClient.SendAsync(request).ConfigureAwait(False)
                    Return Await HandleResponse(response).ConfigureAwait(False)
                End Using
            End Using
        End Function

        Private Async Function HandleResponse(response As HttpResponseMessage) As Task(Of NMAResponse)
            response.EnsureSuccessStatusCode()
            Dim xml = XDocument.Load(Await response.Content.ReadAsStreamAsync().ConfigureAwait(False))
            Dim result = NMAResponse.GetResponse(xml)
            If result.TimeUntilReset.HasValue Then
                NMAClient.TimeUntilReset = result.TimeUntilReset.Value
            End If
            If result.IsSuccessStatusCode Then
                NMAClient.CallsRemaining = DirectCast(result, NMASuccess).CallsRemaining
            ElseIf result.StatusCode = StatusCode.LimitReached Then
                NMAClient.CallsRemaining = 0
            Else
                NMAClient.CallsRemaining -= 1
            End If
            Return result
        End Function

        ''' <summary>
        ''' Gets whether the specified key is valid.
        ''' </summary>
        Public Function VerifyAsync(key As ApiKey) As Task(Of NMAResponse)
            If key Is Nothing Then
                Throw New ArgumentNullException("key")
            End If
            Return Me.VerifyAsyncImplementation(key)
        End Function

        Private Async Function VerifyAsyncImplementation(key As ApiKey) As Task(Of NMAResponse)
            Using request As New Http.VerifyRequestMessage(key)
                Using response = Await NMAClient.GetInstance.HttpClient.SendAsync(request).ConfigureAwait(False)
                    Return Await HandleResponse(response).ConfigureAwait(False)
                End Using
            End Using
        End Function

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

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If _client IsNot Nothing AndAlso _client.IsValueCreated Then
                        _client.Value.Dispose()
                        _client = Nothing
                    End If
                End If

            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class


End Namespace