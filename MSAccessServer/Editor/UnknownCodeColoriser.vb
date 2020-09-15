Option Strict On : Option Explicit On : Option Compare Binary : Option Infer On

Imports System
Imports System.Collections.ObjectModel
''' <summary>
''' Defualt code coloriser for unknown languages
''' </summary>
''' <remarks>
''' known languages can derive from this although it is preferable they implement IColorTokenProvider directly.
''' </remarks>
Public Class UnknownCodeColoriser
    Implements IColorTokenProvider


    Public Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Integer = 0) As System.Collections.ObjectModel.Collection(Of ColorToken) Implements IColorTokenProvider.GetColorTokens
        Return New Collection(Of ColorToken)
    End Function
End Class
