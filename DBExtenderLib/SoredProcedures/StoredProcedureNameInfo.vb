Partial Public Class StoredProcedureNameInfo
    Public Property SchemaTable() As String
    Public Property SP_Name() As String
    Public Property SP_Parameters() As New List(Of SP_Parameters)
    Public Property SP_TextBody() As String
    Public Property SP_TextHeader() As String
    Public Overloads Function ToString() As String
        Return _SP_Name
    End Function
    Public ReadOnly Property Name() As String
        Get
            Return _SP_Name
        End Get
    End Property
End Class
