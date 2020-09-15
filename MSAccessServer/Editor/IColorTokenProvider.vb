Option Strict On : Option Explicit On : Option Compare Binary : Option Infer On

Imports System
Imports System.Collections.ObjectModel
''' <summary>
''' Interface that all code colorisers need to implement.
''' Provides the basis of providing code colorign information to the application via the
''' IColorTokenProvider.GetColorTokens method
''' </summary>
''' <remarks></remarks>
Public Interface IColorTokenProvider

    Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Int32 = 0) As Collection(Of ColorToken)

End Interface
