Partial Public Class ColumnComboBox

    Public Property SelectedTable As TableNameInfo
    Public Property SelectedTableName As String
    Public Property SelectedTableLinqKey As String
    Public Property RelateTable As TableNameInfo
    Public Property RelateTableName As String
    Public Property ForeignKey As String
    Public Property DisplayColumns As String
    Public Property Value_Member As String
    Public Property SelectedValue As String

    Public ReadOnly Property SelectedTableValue() As String
        Get
            Return Replace(_SelectedTableName, " ", "_")
        End Get
    End Property
    Public ReadOnly Property RelateTableValue() As String
        Get
            Return Replace(_RelateTableName, " ", "_")
        End Get
    End Property

End Class
