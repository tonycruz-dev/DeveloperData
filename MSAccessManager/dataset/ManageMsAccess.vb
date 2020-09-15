Public Class ManageMsAccess
    Public Property DatabasePath As String
    Public Property DatabaseName As String
    Public Property DatabaseNameExt() As String
    Public Property DatabaseExt As String
    Public Property IsAccess2007 As Boolean
    Public Property UserName As String
    Public Property Password As String
    Public Property FilePath As String
    Public ReadOnly Property ConnectionString() As String
        Get
            If _IsAccess2007 Then
                Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & DatabasePath
            Else
                Return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DatabasePath
            End If
        End Get
    End Property
End Class
Public Structure PreserveColumn
    Public Property ColumnsName As String
End Structure