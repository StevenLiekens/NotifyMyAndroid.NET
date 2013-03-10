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

Namespace API.Implementation

    ''' <summary>
    ''' Represents an HTTP request message targeting the NMA verification API.
    ''' </summary>
    Friend Class VerifyRequestMessage : Inherits HttpRequestMessage

        Public Sub New(key As NMAKey)
            Me.Method = HttpMethod.Get
            Me.RequestUri = Me.GetRequestUri(key)
        End Sub

        Private Function GetRequestUri(key As NMAKey) As Uri
            Dim builder = NMAClient.GetUriBuilder(NMACommand.Verify)
            builder.Query = Me.GetQueryString(key)
            Return builder.Uri
        End Function

        Private Function GetQueryString(key As NMAKey) As String
            Dim builder As New Text.StringBuilder()
            builder.Append("apikey=") : builder.Append(key.Value)
            If NMAClient.DeveloperKey IsNot Nothing Then
                builder.Append("&developerkey=") : builder.Append(NMAClient.DeveloperKey.Value)
            End If
            Return builder.ToString
        End Function

    End Class

End Namespace
