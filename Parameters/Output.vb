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

Namespace NotifyMyAndroid

    ''' <summary>
    ''' A helper class for comparing input parameters and creating new input parameters.
    ''' </summary>
    Public NotInheritable Class Input : Inherits Parameter

        Public Shared _apiKey As Input = New Input("apikey")
        Public Shared _application As Input = New Input("application")
        Public Shared _event As Input = New Input("event")
        Public Shared _description As Input = New Input("description")
        Public Shared _priority As Input = New Input("priority")
        Public Shared _url As Input = New Input("url")
        Public Shared _contentType As Input = New Input("content-type")
        Public Shared _developerkey As Input = New Input("developerkey")

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
