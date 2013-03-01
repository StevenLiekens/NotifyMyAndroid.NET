Namespace NotifyMyAndroid

    Public Class NMAUsageChangedEventArgs : Inherits EventArgs

        Public Sub New(remainingCalls As Integer, timeUntilReset As TimeSpan)
            _callsRemaining = remainingCalls
            _timeUntilReset = timeUntilReset
        End Sub

        Private _callsRemaining As Integer
        ''' <summary>
        ''' Indicates how many API calls can still be made using the current IP address.
        ''' </summary>
        Public ReadOnly Property CallsRemaining As Integer
            Get
                Return _callsRemaining
            End Get
        End Property

        Private _timeUntilReset As TimeSpan
        ''' <summary>
        ''' Indicates how many minutes remain before the remaining amount of API calls resets.
        ''' </summary>
        Public ReadOnly Property TimeUntilReset As TimeSpan
            Get
                Return _timeUntilReset
            End Get
        End Property

    End Class

End Namespace