Public Class wpfColumnInfo
    Public Property ColumnName() As String

    Public Property Size() As Long

    Public Property TypeSQL() As String

    Public Property TypeVB() As String

    Public Property IsRequared() As Boolean

    Public Property IsPrimary_Key() As Boolean

    Public Property IsForeign_Key() As Boolean

    Public Property TypeOfControl() As String

    Public Property IsAutoincrement() As Boolean

    Public Property LinqVar() As String
    Public Overloads Function ToString() As String
        Return _ColumnName
    End Function
    Public ReadOnly Property ColumnValue() As String
        Get
            Return Replace(Replace(Replace(Replace(_ColumnName, " ", "_"), "/", "_"), "-", ""), ".", "_")
        End Get
    End Property
    Public Property ColumnCombobox() As WPFComboBox
    Public Shared Function CopyColumn(ByVal Col As ColumnsInfo) As wpfColumnInfo
        Dim _Col As New wpfColumnInfo With {.ColumnName = Col.ColumnName,
                                            .Size = Col.Size,
                                            .TypeSQL = Col.TypeSQL,
                                            .TypeVB = Col.TypeVB,
                                            .IsRequared = Col.IsRequared,
                                            .IsPrimary_Key = Col.IsPrimary_Key,
                                            .IsForeign_Key = Col.IsForeign_Key,
                                            .TypeOfControl = Col.TypeOfControl,
                                            .IsAutoincrement = Col.IsAutoincrement,
                                            .LinqVar = Col.LinqVar}
        Return _Col
    End Function
End Class
