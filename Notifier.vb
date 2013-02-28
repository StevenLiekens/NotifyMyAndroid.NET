Namespace NotifyMyAndroid

    ''' <summary>
    ''' Provides static helper methods for sending notifications.
    ''' </summary>
    Public NotInheritable Class Notifier

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Sends a notification to the specified recipient.
        ''' </summary>
        Public Shared Function NotifyAsync(recipient As String, sender As String, subject As String, message As String, Optional priority As Priority? = Nothing, Optional hyperlink As Uri = Nothing, Optional isHtml As Boolean = False) As Task(Of NMAResponse)
            Return Notifier.NotifyAsync(ApiKey.Parse(recipient), sender, subject, message, priority, hyperlink, isHtml)
        End Function

        ''' <summary>
        ''' Sends a notification to the specified recipient.
        ''' </summary>
        Public Shared Function NotifyAsync(recipient As ApiKey, sender As String, subject As String, message As String, Optional priority As Priority? = Nothing, Optional hyperlink As Uri = Nothing, Optional isHtml As Boolean = False) As Task(Of NMAResponse)
            Dim ring As New KeyRing
            ring.Add(recipient)
            Return Notifier.NotifyAsync(ring, sender, subject, message, priority, hyperlink, isHtml)
        End Function

        ''' <summary>
        ''' Sends a notification to all of the specified recipients.
        ''' </summary>
        Public Shared Function NotifyAsync(recipients As KeyRing, sender As String, subject As String, message As String, Optional priority As Priority? = Nothing, Optional hyperlink As Uri = Nothing, Optional isHtml As Boolean = False) As Task(Of NMAResponse)
            Dim notification As New Notification()
            With notification
                .From = sender
                .Subject = subject
                .Message = message
                .Priority = priority
                .HyperLink = hyperlink
                .IsHTML = isHtml
            End With
            Return NMAClient.GetInstance().SendNotificationAsync(recipients, notification)
        End Function

    End Class

End Namespace