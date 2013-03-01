Imports System.Collections.ObjectModel

Namespace NotifyMyAndroid

    ''' <summary>
    ''' Represents a collection of API keys.
    ''' </summary>
    <Runtime.InteropServices.ComVisible(False)>
    Public Class KeyRing : Inherits Collection(Of ApiKey)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ParamArray keys As ApiKey())
            MyBase.New(keys)
        End Sub

        ''' <summary>
        ''' Gets a comma seperated list of all API keys in this instance.
        ''' </summary>
        Public Function ToQueryString() As String
            If Me.Count = 0 Then
                Return String.Empty
            End If

            Dim builder As New Text.StringBuilder

            Dim count As Integer = 1
            For Each key In Me
                If key Is Nothing Then
                    Continue For
                End If
                builder.Append(key.Value)
                If count = Me.Count Then Exit For
                builder.Append(",")
                count += 1
            Next

            Return builder.ToString
        End Function

        Public Overrides Function ToString() As String
            Return Me.ToQueryString
        End Function

    End Class


End Namespace