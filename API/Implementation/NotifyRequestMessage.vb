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
    '''     Represents an HTTP request message targeting the NMA notification API.
    ''' </summary>
    Friend Class NotifyRequestMessage
        Inherits HttpRequestMessage

        Public Sub New()
            Method = HttpMethod.Post
            RequestUri = NMAClient.GetUriBuilder(NMACommand.Notify).Uri
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
