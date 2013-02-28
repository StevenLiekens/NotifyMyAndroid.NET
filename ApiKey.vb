Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents an API key and provides methods to validate and compare API keys.
    ''' </summary>
    Public Class ApiKey

        Private Sub New(key As String)
            _value = key
        End Sub

        Private _value As String
        ''' <summary>
        ''' Gets the hexadecimal string representation of this API key.
        ''' </summary>
        Public ReadOnly Property Value As String
            Get
                Return _value
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
        ''' <param name="value">48 bytes hexadecimal string"</param>
        Public Shared Function Parse(value As String) As ApiKey
            If Not ApiKey.IsValidKeyFormat(value) Then
                Throw New ArgumentException("invalid format: key must be a 48 bytes hexadecimal string")
            End If
            Return New ApiKey(value)
        End Function

        ''' <summary>
        ''' Attempts to create a new instance of <see cref="ApiKey"/> for the specified literal string.
        ''' </summary>
        ''' <param name="value">48 bytes hexadecimal string"</param>
        ''' <param name="key">Out value</param>
        ''' <returns>True if the method ran to completion; otherwise false.</returns>
        Public Shared Function TryParse(value As String, ByRef key As ApiKey) As Boolean
            If Not ApiKey.IsValidKeyFormat(value) Then Return False
            key = New ApiKey(value)
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
            For Each character In key.ToCharArray
                If Not Char.IsLetterOrDigit(character) Then
                    Return False
                End If
            Next
            Return True
        End Function

        ''' <summary>
        ''' Generates a random key.
        ''' </summary>
        Public Shared Function GenerateKey() As ApiKey
            Dim builder As New Text.StringBuilder
            Dim r As New Random()

            For i = 1 To 48
                builder.Append(r.Next(0, 15).ToString("x"))
            Next

            Return ApiKey.Parse(builder.ToString)
        End Function

#Region "Implementation"

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