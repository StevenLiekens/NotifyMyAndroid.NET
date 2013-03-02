Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents a notification.
    ''' </summary>
    Public Class Notification

        Private _from As String
        ''' <summary>
        ''' Indicates the person or application that is generating the call.
        ''' </summary>
        ''' <remarks>Required. Maximum length is 256 characters.</remarks>
        ''' <example>Example: Nagios</example>
        Public Property From As String
            Get
                If String.IsNullOrEmpty(_from) Then
                    Return "Application"
                End If
                Return _from
            End Get
            Set(value As String)
                If String.IsNullOrEmpty(value) Then
                    Throw New ArgumentNullException("value", "Name cannot be empty.")
                End If
                If value.Length > 256 Then
                    Throw New ArgumentOutOfRangeException("Name exceeds the 256 characters limitation.", "value")
                End If
                _from = value
            End Set
        End Property

        Private _subject As String
        ''' <summary>
        ''' Indicates the subject of the notification.
        ''' </summary>
        ''' <remarks>Required. Maximum length is 1000 characters.</remarks>
        ''' <example>Example: Service is down!</example>
        Public Property Subject As String
            Get
                If String.IsNullOrEmpty(_subject) Then
                    Return "Subject"
                End If
                Return _subject
            End Get
            Set(value As String)
                If String.IsNullOrEmpty(value) Then
                    Throw New ArgumentNullException("value", "Subject cannot be empty.")
                End If
                If value.Length > 1000 Then
                    Throw New ArgumentOutOfRangeException("Subject exceeds the 1000 characters limitation.", "value")
                End If
                _subject = value
            End Set
        End Property

        Private _message As String
        ''' <summary>
        ''' Indicates the notification text.
        ''' </summary>
        ''' <remarks>Required. Maximum length is 10.000 characters.</remarks>
        ''' <example>
        ''' Example:
        ''' Server: 1.2.3.4
        ''' Service: mysqld
        ''' Status: DOWN
        ''' Time of the alert: 1/21/2011 1:32am
        ''' </example>
        Public Property Message As String
            Get
                If String.IsNullOrEmpty(_message) Then
                    Return "Message"
                End If
                Return _message
            End Get
            Set(value As String)
                If String.IsNullOrEmpty(value) Then
                    Throw New ArgumentNullException("value", "Message cannot be empty.")
                End If
                If value.Length > 10000 Then
                    Throw New ArgumentOutOfRangeException("Message exceeds the 10.000 characters limitation.", "value")
                End If
                _message = value
            End Set
        End Property

        Private _priority As Priority?
        ''' <summary>
        ''' Indicates the priority level for this notification.
        ''' </summary>
        ''' <remarks>Optional.</remarks>
        Public Property Priority As Priority?
            Get
                Return _priority
            End Get
            Set(value As Priority?)
                If value.HasValue Then
                    If Not [Enum].IsDefined(GetType(Priority), value.Value) Then
                        Throw New ArgumentOutOfRangeException("value", "Specified priority level is invalid.")
                    End If
                End If
                _priority = value
            End Set
        End Property

        Private _hyperlink As Uri
        ''' <summary>
        ''' Indicates a URI/URL which can be activated by pressing the notification.
        ''' </summary>
        ''' <remarks>Optional. Maximum length is 2000 characters.</remarks>
        Public Property HyperLink As Uri
            Get
                Return _hyperlink
            End Get
            Set(value As Uri)
                If value IsNot Nothing AndAlso value.ToString.Length > 2000 Then
                    Throw New ArgumentOutOfRangeException("Hyperlink exceeds the 2000 characters limitation.", "value")
                End If
                _hyperlink = value
            End Set
        End Property

        ''' <summary>
        ''' Indicates whether basic HTML tags should be interpreted and rendered while displaying the notification.
        ''' </summary>
        Public Property IsHTML As Boolean

        ''' <summary>
        ''' Sends this notification to the specified recipient and returns an object indicating the command result.
        ''' </summary>
        ''' <returns>Returns an instance of <see cref="NMASuccess"/> or <see cref="NMAError"/> depending on whether the call was successful.</returns>
        Public Function SendAsync(recipient As NMAKey) As Task(Of NMAResponse)
            Return NMAClient.GetInstance().SendNotificationAsync(recipient, Me)
        End Function

        ''' <summary>
        ''' Sends this notification to all of the specified recipients and returns an object indicating the command result.
        ''' </summary>
        ''' <returns>Returns an instance of <see cref="NMASuccess"/> or <see cref="NMAError"/> depending on whether the call was successful.</returns>
        ''' <remarks>If the specified key ring contains one or more invalid API keys, this method will not report an error unless all of the keys were invalid.</remarks>
        Public Function SendAsync(recipients As KeyRing) As Task(Of NMAResponse)
            Return NMAClient.GetInstance().SendNotificationAsync(recipients, Me)
        End Function

    End Class

End Namespace