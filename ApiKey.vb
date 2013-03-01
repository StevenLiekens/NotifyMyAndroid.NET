Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents an API key and provides methods to validate and compare API keys.
    ''' </summary>
    Public Class ApiKey

        Public Sub New(key As Byte())
            If key Is Nothing Then
                Throw New ArgumentNullException("key", "key cannot be empty")
            ElseIf key.Length <> 24 Then
                Throw New ArgumentOutOfRangeException("key", "key length must be exactly 24 bytes")
            End If
            _key = key
        End Sub

        Private _key(23) As Byte
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
        ''' Creates a new instance of <see cref="ApiKey"/> for the specified literal string.
        ''' </summary>
        ''' <param name="value">Hexadecimal string representation of an API key.</param>
        Public Shared Function Parse(value As String) As ApiKey
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentNullException("value", "value cannot be empty")
            ElseIf value.Length <> 48 Then
                Throw New ArgumentException("invalid format: value must be a 48 bytes hexadecimal string")
            End If

            Dim key As New List(Of Byte)

            Dim container As Byte = Nothing
            For i = 0 To value.Length - 1 Step 2
                If Not Byte.TryParse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber, Globalization.NumberFormatInfo.InvariantInfo, container) Then
                    Throw New ArgumentException("invalid data: value must be a hexadecimal string")
                End If
                key.Add(container)
            Next

            Return New ApiKey(key.ToArray)
        End Function

        ''' <summary>
        ''' Attempts to create a new instance of <see cref="ApiKey"/> for the specified literal string.
        ''' </summary>
        ''' <param name="value">Hexadecimal string representation of an API key.</param>
        ''' <param name="result">When this method is able to successfully parse the value, contains an instance of <see cref="ApiKey"/> for the specified value.</param>
        ''' <returns>True if the method ran to completion; otherwise false.</returns>
        Public Shared Function TryParse(value As String, ByRef result As ApiKey) As Boolean
            If String.IsNullOrEmpty(value) OrElse value.Length <> 48 Then
                Return False
            End If

            Dim key As New List(Of Byte)

            Dim container As Byte = Nothing
            For i = 0 To value.Length - 1 Step 2
                If Not Byte.TryParse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber, Globalization.NumberFormatInfo.InvariantInfo, container) Then
                    Return False
                End If
                key.Add(container)
            Next

            result = New ApiKey(key.ToArray)

            Return True
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
        Public Shared Function GenerateKey() As ApiKey
            Dim key(23) As Byte
            Dim r As New Random()

            For i = 0 To 23
                key(i) = CByte(r.Next(255))
            Next

            Return New ApiKey(key)
        End Function

#Region "Implementation details"

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Me.Equals(TryCast(obj, ApiKey))
        End Function

        Public Overloads Function Equals(obj As ApiKey) As Boolean
            If obj Is Nothing Then Return False
            For i = 0 To 23
                If Me._key(i) <> obj._key(i) Then
                    Return False
                End If
            Next
            Return True
        End Function

        Public Overrides Function ToString() As String
            Return Me.Value
        End Function

        Public Shared Operator =(left As ApiKey, right As ApiKey) As Boolean
            If left Is Nothing Then Return right Is Nothing
            If right Is Nothing Then Return left Is Nothing

            For i = 0 To 23
                If left._key(i) <> right._key(i) Then
                    Return False
                End If
            Next
            Return True
        End Operator

        Public Shared Operator <>(left As ApiKey, right As ApiKey) As Boolean
            Return Not (left = right)
        End Operator

#End Region

    End Class


End Namespace