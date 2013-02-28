Imports System.Net.Http

Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents an error response.
    ''' </summary>
    Public Class NMAError : Inherits NMAResponse

        Public Sub New(response As XDocument)
            MyBase.New(response)
            If Me.IsSuccessStatusCode Then
                Throw New InvalidOperationException("response does not indicate failure")
            End If
        End Sub

        Public ReadOnly Property Message As String
            Get
                Return Me.GetElement().Value
            End Get
        End Property

    End Class

End Namespace