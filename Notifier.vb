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

Namespace API

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
            Return Notifier.NotifyAsync(NMAKey.Parse(recipient), sender, subject, message, priority, hyperlink, isHtml)
        End Function

        ''' <summary>
        ''' Sends a notification to the specified recipient.
        ''' </summary>
        Public Shared Function NotifyAsync(recipient As NMAKey, sender As String, subject As String, message As String, Optional priority As Priority? = Nothing, Optional hyperlink As Uri = Nothing, Optional isHtml As Boolean = False) As Task(Of NMAResponse)
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