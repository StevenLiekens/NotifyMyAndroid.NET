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
    ''' A helper class for retrieving and comparing API commands.
    ''' </summary>
    Friend Class NMACommand

        Private Shared _verify As New NMACommand("verify")
        Private Shared _notify As New NMACommand("notify")

        Private _value As String

        Private Sub New(request As String)
            _value = request
        End Sub

        Public ReadOnly Property Value As String
            Get
                Return _value
            End Get
        End Property

        Public Shared ReadOnly Property Verify As NMACommand
            Get
                Return NMACommand._verify
            End Get
        End Property

        Public Shared ReadOnly Property Notify As NMACommand
            Get
                Return NMACommand._notify
            End Get
        End Property

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Me.Equals(TryCast(obj, NMACommand))
        End Function

        Public Overloads Function Equals(obj As NMACommand) As Boolean
            If obj Is Nothing Then Return False
            Return String.Equals(Me.Value, obj.Value, StringComparison.OrdinalIgnoreCase)
        End Function

        Public Shared Operator =(left As NMACommand, right As NMACommand) As Boolean
            If left Is Nothing Then Return right Is Nothing
            If right Is Nothing Then Return left Is Nothing

            Return String.Equals(left.Value, right.Value, StringComparison.OrdinalIgnoreCase)
        End Operator

        Public Shared Operator <>(left As NMACommand, right As NMACommand) As Boolean
            Return Not (left = right)
        End Operator

        Public Overrides Function GetHashCode() As Integer
            Return Me.Value.ToUpperInvariant().GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return Me.Value
        End Function

    End Class

End Namespace