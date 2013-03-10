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
    '''     The exception that is thrown if <see cref="NMAResponse.EnsureSuccessStatusCode" /> is called when its status code indicates failure.
    ''' </summary>
    Public Class NMAException
        Inherits Exception

        Public Sub New(status As StatusCode, message As String)
            MyBase.New(message)
            If Not [Enum].IsDefined(GetType(StatusCode), status) Then
                Throw New ArgumentException("The specified status code is invalid.")
            ElseIf status = StatusCode.Success Then
                Throw New ArgumentException("The specified status code does not indicate failure.")
            End If
            _errorCode = status
        End Sub

        Private ReadOnly _errorCode As StatusCode

        ''' <summary>
        '''     Indicates the error code returned by the API.
        ''' </summary>
        Public ReadOnly Property ErrorCode As StatusCode
            Get
                Return _errorCode
            End Get
        End Property
    End Class
End Namespace