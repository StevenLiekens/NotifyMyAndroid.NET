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

Namespace API.Implementation

    ''' <summary>
    ''' A helper class for comparing input parameters and creating new input parameters.
    ''' </summary>
    Friend NotInheritable Class Input : Inherits Parameter

        Private Shared ReadOnly _apiKey As New Input("apikey")
        Private Shared ReadOnly _application As New Input("application")
        Private Shared ReadOnly _event As New Input("event")
        Private Shared ReadOnly _description As New Input("description")
        Private Shared ReadOnly _priority As New Input("priority")
        Private Shared ReadOnly _url As New Input("url")
        Private Shared ReadOnly _contentType As New Input("content-type")
        Private Shared ReadOnly _developerkey As New Input("developerkey")

        Private Sub New(parameter As String)
            MyBase.New(parameter)
        End Sub

        Public Shared ReadOnly Property APIKey As Input
            Get
                Return Input._apiKey
            End Get
        End Property

        Public Shared ReadOnly Property Application As Input
            Get
                Return Input._application
            End Get
        End Property

        Public Shared ReadOnly Property [Event] As Input
            Get
                Return Input._event
            End Get
        End Property

        Public Shared ReadOnly Property Description As Input
            Get
                Return Input._description
            End Get
        End Property

        Public Shared ReadOnly Property Priority As Input
            Get
                Return Input._priority
            End Get
        End Property

        Public Shared ReadOnly Property URL As Input
            Get
                Return Input._url
            End Get
        End Property

        Public Shared ReadOnly Property ContentType As Input
            Get
                Return Input._contentType
            End Get
        End Property

        Public Shared ReadOnly Property DeveloperKey As Input
            Get
                Return Input._developerkey
            End Get
        End Property

    End Class

End Namespace
