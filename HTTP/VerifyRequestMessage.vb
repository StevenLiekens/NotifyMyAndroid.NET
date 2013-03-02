Namespace NotifyMyAndroid.Http

    ''' <summary>
    ''' Represents an HTTP request message targetting the NMA verification API.
    ''' </summary>
    Public Class VerifyRequestMessage : Inherits HttpRequestMessage

        Public Sub New(key As NMAKey)
            Me.Method = HttpMethod.Get
            Dim builder = NMAClient.GetUriBuilder(NMACommand.Verify)
            Dim query As String = "apikey=" & key.Value
            If NMAClient.DeveloperKey IsNot Nothing Then
                query &= "&developerkey=" & NMAClient.DeveloperKey.Value
            End If
            builder.Query = query
            Me.RequestUri = builder.Uri
        End Sub

    End Class

End Namespace
