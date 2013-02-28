Namespace NotifyMyAndroid

    ''' <summary>
    ''' The exception that is thrown if <see cref="NMAResponse.EnsureSuccessStatusCode"/> is called when its status code indicates failure.
    ''' </summary>
    Public Class NMAException : Inherits Exception

        Public Sub New(status As StatusCode, message As String)
            MyBase.New(message)
            If Not [Enum].IsDefined(GetType(StatusCode), status) Then
                Throw New ArgumentException("the specified status code is invalid")
            End If
            If status = StatusCode.Success Then
                Throw New ArgumentException("the specified status code does not indicate failure")
            End If
            _errorCode = status
        End Sub

        Private _errorCode As StatusCode
        ''' <summary>
        ''' Indicates the error code returned by the API.
        ''' </summary>
        Public ReadOnly Property ErrorCode As StatusCode
            Get
                Return _errorCode
            End Get
        End Property

    End Class

End Namespace