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
Imports System.Collections.ObjectModel

Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents a collection of API keys.
    ''' </summary>
    <Runtime.InteropServices.ComVisible(False)>
    Public Class KeyRing : Inherits Collection(Of NMAKey)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ParamArray keys As NMAKey())
            MyBase.New(keys)
        End Sub

        ''' <summary>
        ''' Gets a comma seperated list of all API keys in this instance.
        ''' </summary>
        Public Function ToQueryString() As String
            If Me.Count = 0 Then
                Return String.Empty
            End If

            Dim builder As New Text.StringBuilder

            Dim count As Integer = 1
            For Each key In Me
                If key Is Nothing Then
                    Continue For
                End If
                builder.Append(key.Value)
                If count = Me.Count Then Exit For
                builder.Append(",")
                count += 1
            Next

            Return builder.ToString
        End Function

        Public Overrides Function ToString() As String
            Return Me.ToQueryString
        End Function

    End Class


End Namespace