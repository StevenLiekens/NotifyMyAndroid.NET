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
    ''' Base class for parameter container types.
    ''' </summary>
    Friend MustInherit Class Parameter

        Protected Sub New(name As String)
            _name = name
        End Sub

        Private _name As String
        Public ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

#Region "Implementation details"

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Me.Equals(TryCast(obj, Parameter))
        End Function

        Public Overloads Function Equals(obj As Parameter) As Boolean
            If obj Is Nothing Then Return False
            Return String.Equals(Me.Name, obj.Name, StringComparison.OrdinalIgnoreCase)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.Name.ToUpperInvariant().GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function

        Public Overloads Shared Operator =(left As Parameter, right As Parameter) As Boolean
            If left Is Nothing Then Return right Is Nothing
            If right Is Nothing Then Return left Is Nothing

            Return String.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
        End Operator

        Public Overloads Shared Operator <>(left As Parameter, right As Parameter) As Boolean
            Return Not (left = right)
        End Operator

        Public Shared Widening Operator CType(value As Parameter) As String
            Return value.Name
        End Operator

#End Region

    End Class

End Namespace
