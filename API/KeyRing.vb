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

Imports System.Runtime.InteropServices
Imports System.Collections.ObjectModel
Imports System.Text

Namespace API
    ''' <summary>
    '''     Represents a collection of API keys.
    ''' </summary>
    <ComVisible(False)>
    Public Class KeyRing
        Inherits Collection(Of NMAKey)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ParamArray keys As NMAKey())
            MyBase.New(keys)
        End Sub

        ''' <summary>
        '''     Gets a comma seperated list of all API keys in this instance.
        ''' </summary>
        Public Function ToQueryString() As String
            Dim builder As New StringBuilder
            For Each key In (From keys In Me Where keys IsNot Nothing)
                builder.Append(key.Value)
                builder.Append(",")
            Next
            Return builder.ToString.TrimEnd(","c)
        End Function

        Public Overrides Function ToString() As String
            Return ToQueryString()
        End Function
    End Class
End Namespace