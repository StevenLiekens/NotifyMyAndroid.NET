Namespace NotifyMyAndroid

    ''' <summary>
    ''' Specifies the status code associated with a response.
    ''' </summary>
    Public Enum StatusCode
        Success = 200
        InvalidData = 400
        InvalidKey = 401
        LimitReached = 402
        InternalError = 500
    End Enum

End Namespace