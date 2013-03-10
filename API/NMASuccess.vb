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

Imports NotifyMyAndroid.API.Implementation

Namespace API
    ''' <summary>
    '''     Represents a success response.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NMASuccess
        Inherits NMAResponse

        Public Sub New(response As XDocument)
            MyBase.New(response)
            If Not IsSuccessStatusCode Then
                Throw New InvalidOperationException("The response does not indicate success.")
            End If
        End Sub

        ''' <summary>
        '''     Indicates the API calls quota associated with the current IP address.
        ''' </summary>
        Public ReadOnly Property CallsRemaining As Integer
            Get
                Return GetAttribute(Of Integer)(Output.Remaining)
            End Get
        End Property
    End Class
End Namespace