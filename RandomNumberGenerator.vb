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

''' <summary>
''' Provides static methods for creating random numbers.
''' </summary>
Friend NotInheritable Class RandomNumberGenerator

    Private Sub New()
    End Sub

    Private Shared randomGenerator As New Lazy(Of Random)

    Public Shared Function GetRandomNumber() As Integer
        SyncLock randomGenerator
            Return randomGenerator.Value.Next()
        End SyncLock
    End Function

    Public Shared Function GetRandomNumber(maxValue As Integer) As Integer
        SyncLock randomGenerator
            Return randomGenerator.Value.Next(maxValue)
        End SyncLock
    End Function

    Public Shared Function GetRandomNumber(minValue As Integer, maxValue As Integer) As Integer
        SyncLock randomGenerator
            Return randomGenerator.Value.Next(minValue, maxValue)
        End SyncLock
    End Function

    Public Shared Function GetRandomDouble() As Double
        SyncLock randomGenerator
            Return randomGenerator.Value.NextDouble()
        End SyncLock
    End Function

    Public Shared Sub GetRandomBytes(buffer() As Byte)
        SyncLock randomGenerator
            randomGenerator.Value.NextBytes(buffer)
        End SyncLock
    End Sub

    Public Shared Function GetRandomBytes(count As Integer) As Byte()
        Dim buffer(count - 1) As Byte
        SyncLock randomGenerator
            randomGenerator.Value.NextBytes(buffer)
        End SyncLock
        Return buffer
    End Function

End Class
