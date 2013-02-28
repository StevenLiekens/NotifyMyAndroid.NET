Namespace NotifyMyAndroid.Http

    ''' <summary>
    ''' A container for NMA notification name/value tuples encoded using application/x-www-form-urlencoded MIME type.
    ''' </summary>
    Public Class NotificationContent : Inherits FormUrlEncodedContent

        Sub New(recipients As KeyRing, notification As Notification)
            MyBase.New(NotificationContent.GetQuery(recipients, notification))
        End Sub

        Private Shared Function GetQuery(recipients As KeyRing, notification As Notification) As Dictionary(Of String, String)
            Dim query As New Dictionary(Of String, String)
            query.Add("apikey", recipients.ToQueryString)
            query.Add("application", notification.From)
            query.Add("event", notification.Subject)
            query.Add("description", notification.Message)
            If notification.Priority.HasValue Then
                query.Add("priority", CInt(notification.Priority.Value).ToString)
            End If
            If notification.HyperLink IsNot Nothing Then
                query.Add("url", notification.HyperLink.ToString)
            End If
            If notification.IsHTML Then
                query.Add("content-type", "text/html")
            End If
            If NMAClient.DeveloperKey IsNot Nothing Then
                query.Add("developerkey", NMAClient.DeveloperKey.Value)
            End If

            Return query
        End Function

    End Class

End Namespace