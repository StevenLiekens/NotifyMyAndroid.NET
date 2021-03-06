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
    Public Class NMAUsageChangedEventArgs
        Inherits EventArgs

        Public Sub New(remainingCalls As Integer, timeUntilReset As TimeSpan)
            _callsRemaining = remainingCalls
            _timeUntilReset = timeUntilReset
        End Sub

        Private ReadOnly _callsRemaining As Integer

        ''' <summary>
        '''     Indicates how many API calls can still be made using the current IP address.
        ''' </summary>
        Public ReadOnly Property CallsRemaining As Integer
            Get
                Return _callsRemaining
            End Get
        End Property

        Private ReadOnly _timeUntilReset As TimeSpan

        ''' <summary>
        '''     Indicates how many minutes remain before the remaining amount of API calls resets.
        ''' </summary>
        Public ReadOnly Property TimeUntilReset As TimeSpan
            Get
                Return _timeUntilReset
            End Get
        End Property
    End Class
End Namespace