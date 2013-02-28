Namespace NotifyMyAndroid.Http

    ''' <summary>
    ''' Represents an HTTP request message targetting the NMA notification API.
    ''' </summary>
    Public Class NotifyRequestMessage : Inherits HttpRequestMessage

        Public Sub New()
            Me.Method = HttpMethod.Post
            Me.RequestUri = NMAClient.GetUriBuilder(NMACommand.Notify).Uri
        End Sub

        Public Sub New(content As NotificationContent)
            Me.New()
            Me.Content = content
        End Sub

        Public Shadows Property Content As NotificationContent
            Get
                Return TryCast(MyBase.Content, NotificationContent)
            End Get
            Set(value As NotificationContent)
                MyBase.Content = value
            End Set
        End Property

    End Class

End Namespace
