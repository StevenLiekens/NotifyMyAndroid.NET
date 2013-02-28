Imports System.Net.Http

Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents a success response.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NMASuccess : Inherits NMAResponse

        Public Sub New(response As XDocument)
            MyBase.New(response)
            If Not Me.IsSuccessStatusCode Then
                Throw New InvalidOperationException("response does not indicate success")
            End If
        End Sub

        ''' <summary>
        ''' Indicates the API calls quota associated with the current IP address.
        ''' </summary>
        Public ReadOnly Property CallsRemaining As Integer
            Get
                Return Me.GetAttribute(Of Integer)("remaining")
            End Get
        End Property

    End Class

End Namespace