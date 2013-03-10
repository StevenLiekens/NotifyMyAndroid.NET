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
    ''' Represents an API key and provides methods to create, validate and compare API keys.
    ''' </summary>
    Public Class NMAKey

        Public Sub New(key As Byte())
            If key Is Nothing Then
                Throw New ArgumentNullException("key", "The key cannot be empty.")
            ElseIf key.Length <> 24 Then
                Throw New ArgumentOutOfRangeException("key", "The key's length must be exactly 24 bytes.")
            End If
            _key = key
        End Sub

        Private ReadOnly _key() As Byte
        ''' <summary>
        ''' Gets the API key.
        ''' </summary>
        Public ReadOnly Property Key As Byte()
            Get
                Return _key.ToArray
            End Get
        End Property

        ''' <summary>
        ''' Gets the hexadecimal string representation of the API key.
        ''' </summary>
        Public ReadOnly Property Value As String
            Get
                Return String.Concat(BitConverter.ToString(_key).Split("-"c))
            End Get
        End Property

        ''' <summary>
        ''' Gets whether this API key is a valid key.
        ''' </summary>
        Public Async Function VerifyAsync() As Task(Of NMAResponse)
            Return Await NMAClient.GetInstance().VerifyAsync(Me).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Creates a new instance of <see cref="NMAKey"/> for the specified literal string.
        ''' </summary>
        ''' <param name="value">Hexadecimal string representation of an API key.</param>
        Public Shared Function Parse(value As String) As NMAKey
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentNullException("value", "The value cannot be empty.")
            ElseIf value.Length <> 48 Then
                Throw New ArgumentOutOfRangeException("value", "The value must be a 48 bytes hexadecimal string.")
            End If

            Dim key As New Queue(Of Byte)

            Dim container As Byte = Nothing
            For i = 0 To value.Length - 1 Step 2
                If Not Byte.TryParse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber, Globalization.NumberFormatInfo.InvariantInfo, container) Then
                    Throw New ArgumentException("The value must be a hexadecimal string.", "value")
                End If
                key.Enqueue(container)
            Next

            Return New NMAKey(key.ToArray)
        End Function

        ''' <summary>
        ''' Attempts to create a new instance of <see cref="NMAKey"/> for the specified literal string.
        ''' </summary>
        ''' <param name="value">Hexadecimal string representation of an API key.</param>
        ''' <returns>An instance of <see cref="NMAKey"/> if the call was successful; otherwise Nothing.</returns>
        Public Shared Function TryParse(value As String) As NMAKey
            If String.IsNullOrEmpty(value) OrElse value.Length <> 48 Then
                Return Nothing
            End If

            Dim key As New Queue(Of Byte)
            Dim container As Byte = Nothing

            For i = 0 To value.Length - 1 Step 2
                If Not Byte.TryParse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber, Globalization.NumberFormatInfo.InvariantInfo, container) Then
                    Return Nothing
                End If
                key.Enqueue(container)
            Next

            Return New NMAKey(key.ToArray)
        End Function

        ''' <summary>
        ''' Gets whether the specified literal string is a valid key format.
        ''' </summary>
        Public Shared Function IsValidKeyFormat(value As String) As Boolean
            If String.IsNullOrEmpty(value) OrElse value.Length <> 48 Then
                Return False
            End If

            Dim container As Byte

            For i = 0 To value.Length - 1 Step 2
                If Not Byte.TryParse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber, Globalization.NumberFormatInfo.InvariantInfo, container) Then
                    Return False
                End If
            Next
            Return True
        End Function

        ''' <summary>
        ''' Generates a random key for testing purposes.
        ''' </summary>
        ''' <remarks>There are 2¹⁹² possible key combinations, so the odds of this method ever returning an existing key are quite low.</remarks>
        Public Shared Function GenerateKey() As NMAKey
            Return New NMAKey(Utilities.RandomNumberGenerator.GetRandomBytes(24))
        End Function

#Region "Implementation details"

        Public Overrides Function ToString() As String
            Return Me.Value
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Me.Equals(TryCast(obj, NMAKey))
        End Function

        Public Overloads Function Equals(obj As NMAKey) As Boolean
            If obj Is Nothing Then Return False

            For i = 0 To 23
                If Me._key(i) <> obj._key(i) Then
                    Return False
                End If
            Next
            Return True
        End Function

        Public Shared Operator =(left As NMAKey, right As NMAKey) As Boolean
            If left IsNot Nothing Then
                Return left.Equals(right)
            Else
                Return right Is Nothing
            End If
        End Operator

        Public Shared Operator <>(left As NMAKey, right As NMAKey) As Boolean
            Return Not (left = right)
        End Operator

        Public Overloads Shared Widening Operator CType(value As NMAKey) As String
            Return value.Value
        End Operator

        Public Overloads Shared Narrowing Operator CType(value As String) As NMAKey
            Return NMAKey.Parse(value)
        End Operator

#End Region

    End Class


End Namespace