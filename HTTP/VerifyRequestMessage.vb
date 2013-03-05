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

Namespace API.Http

    ''' <summary>
    ''' Represents an HTTP request message targetting the NMA verification API.
    ''' </summary>
    Friend Class VerifyRequestMessage : Inherits HttpRequestMessage

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
