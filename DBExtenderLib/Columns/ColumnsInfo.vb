Partial Public Class ColumnsInfo
   
    Public Property ColumnID As Integer

    Public Property ColumnName As String

    Public Property Size As Long

    Public Property Table As TableNameInfo

    Public Property TypeSQL As String

    Public Property TypeAccess As String

    Public Property TypeVB As String

    Public Property IsRequared As Boolean

    Public Property IsNull As Boolean

    Public Property IsPrimary_Key As Boolean

    Public Property IsForeign_Key As Boolean

    Public Property TypeMySql As String

    Public Property TypeOfControl As String

    Public Property ForeignKey As ForeignKeyInfo

    Public Property IsAutoincrement As Boolean
    Public Property LinqVar As String
    Public Property LinqVarCSharp As String
    Public Property VarCSharp As String
    Public Property ComboBox As New ColumnComboBox

    Public Overloads Function ToString() As String
        Return _ColumnName
    End Function
    Public ReadOnly Property ColumnValue() As String
        Get
            Return Replace(Replace(Replace(Replace(_ColumnName, " ", "_"), "/", "_"), "-", ""), ".", "_")
        End Get
    End Property
    'Public Property ColumnCombobox() As New ColumnComboBox
    Public Shared Function CopyColumn(ByVal Col As ColumnsInfo) As wpfColumnInfo
        Dim _Col As New wpfColumnInfo With {.ColumnName = Col.ColumnName,
                                            .Size = Col.Size,
                                            .TypeSQL = Col.TypeSQL,
                                            .TypeVB = Col.TypeVB,
                                            .IsRequared = Col.IsRequared,
                                            .IsPrimary_Key = Col.IsPrimary_Key,
                                            .IsForeign_Key = Col.IsForeign_Key,
                                            .TypeOfControl = GetWpfControlField(Col),
                                            .IsAutoincrement = Col.IsAutoincrement,
                                            .LinqVar = Col.LinqVar}
        Return _Col
    End Function
    Private Shared Function GetWpfControlField(ByVal col As ColumnsInfo) As String
      
        Select Case col.TypeSQL.ToLower
            Case "nvarchar", "int", "smallint", "real", "money", "uniqueIdentifier", "tinyint"
                Return "TextBox"
            Case "bit"
                Return "CheckBox"
            Case "smalldatetime", "datetime", "date"
                Return "DatePicker"
            Case "image", "byte"
                Return "Image"
            Case Else
                Return "TextBox"

        End Select
        'CheckBox()
        'ComboBox
        'DatePicker()
        'Image()
        'Label()
        'TextBlock()
        'TextBox()
    End Function
End Class
