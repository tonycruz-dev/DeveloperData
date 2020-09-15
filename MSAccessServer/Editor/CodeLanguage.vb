Option Strict On : Option Explicit On : Option Compare Binary : Option Infer On


''' <summary>
''' public enum for known languages.
''' Used primarily with the code colorisers.
''' </summary>
''' <remarks>
''' Potentially subject to change in the future !  
''' But does provide a more efficient way of filtering than using strings.
''' </remarks>
Public Enum CodeLanguage
    VB
    SQLServer
    JScriptCodeColoriser
    ASPNET
    XML
    Unknown
End Enum
