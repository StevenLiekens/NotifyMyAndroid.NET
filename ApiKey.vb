Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents an API key and provides methods to validate and compare API keys.
    ''' </summary>
    Public Class ApiKey

        Public Sub New(key As Byte())
            If key.Length <> 24 Then
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
                Return _key
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
            If Not ApiKey.IsValidKeyFormat(value) Then
                Throw New ArgumentException("invalid format: key must be a 48 bytes hexadecimal string")
            End If
            value = value.Trim
            Dim key As New List(Of Byte)

            For i = 0 To value.Length - 1 Step 2
                key.Add(Byte.Parse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber))
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
            If Not ApiKey.IsValidKeyFormat(value) Then Return False
            value = value.Trim
            Dim key As New List(Of Byte)
            For i = 0 To value.Length - 1 Step 2
                key.Add(Byte.Parse(value.Substring(i, 2), Globalization.NumberStyles.HexNumber))
            Next
            result = New ApiKey(key.ToArray)
            Return True
        End Function

        ''' <summary>
        ''' Gets whether the specified literal string is a valid key format.
        ''' </summary>
        Public Shared Function IsValidKeyFormat(key As String) As Boolean
            If String.IsNullOrWhiteSpace(key) Then
                Return False
            Else
                key = key.Trim
            End If
            If key.Length <> 48 Then
                Return False
            End If
            Dim result As Byte
            For i = 0 To key.Length - 1 Step 2
                If Not Byte.TryParse(key.Substring(i, 2), Globalization.NumberStyles.HexNumber, Globalization.CultureInfo.InvariantCulture, result) Then
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
            Return String.Equals(Me.Value, obj.Value, StringComparison.OrdinalIgnoreCase)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.Value.ToUpperInvariant.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            Return Me.Value
        End Function

        Public Shared Operator =(left As ApiKey, right As ApiKey) As Boolean
            If left Is Nothing Then Return right Is Nothing
            If right Is Nothing Then Return left Is Nothing

            Return String.Equals(left.Value, right.Value, StringComparison.OrdinalIgnoreCase)
        End Operator

        Public Shared Operator <>(left As ApiKey, right As ApiKey) As Boolean
            Return Not (left = right)
        End Operator

#End Region

    End Class


End Namespace