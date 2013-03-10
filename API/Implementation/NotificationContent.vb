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
    '''     A container for NMA notification name/value tuples encoded using application/x-www-form-urlencoded MIME type.
    ''' </summary>
    Friend Class NotificationContent
        Inherits FormUrlEncodedContent

        Sub New(recipients As KeyRing, notification As Notification)
            MyBase.New(GetQuery(recipients, notification))
        End Sub

        Private Shared Function GetQuery(recipients As KeyRing, notification As Notification) As IEnumerable(Of KeyValuePair(Of String, String))
            Dim query As New Dictionary(Of String, String)

            With query
                .Add(Input.APIKey, recipients.ToQueryString)
                .Add(Input.Application, notification.From)
                .Add(Input.Event, notification.Subject)
                .Add(Input.Description, notification.Message)
            End With

            If notification.Priority.HasValue Then
                query.Add(Input.Priority, CInt(notification.Priority.Value).ToString)
            End If
            If notification.HyperLink IsNot Nothing Then
                query.Add(Input.URL, notification.HyperLink.ToString)
            End If
            If notification.IsHTML Then
                query.Add(Input.ContentType, "text/html")
            End If
            If NMAClient.DeveloperKey IsNot Nothing Then
                query.Add(Input.DeveloperKey, NMAClient.DeveloperKey.Value)
            End If

            Return query
        End Function
    End Class
End Namespace