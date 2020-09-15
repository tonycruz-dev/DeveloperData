Imports DBExtenderLib

Public Class FormSqlInsertGenerator
    Private _listColumns As New List(Of ColumnsInfo)
    Private _SelectedTable As TableNameInfo
    Public Sub New(db As DatabaseNameInfo)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TableNameInfoBindingSource.DataSource = db.ListTable
        _SelectedTable = TableNameInfoBindingSource.Current

        CheckedListBoxColumns.DataSource = _SelectedTable.ListColumn
        CheckedListBoxColumns.DisplayMember = "ColumnName"
        CheckedListBoxColumns.ValueMember = "ColumnValue"
    End Sub
    Public ReadOnly Property SelectedTable As TableNameInfo
        Get
            Return _SelectedTable
        End Get
    End Property
    Public ReadOnly Property listSelectedColumns As List(Of ColumnsInfo)
        Get
            Return _listColumns
        End Get
    End Property
    Public ReadOnly Property WhereClose As String
        Get
            Return TextBoxWhere.Text
        End Get
    End Property
    Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click
        For Each col In CheckedListBoxColumns.CheckedItems
            Dim selectedCol As ColumnsInfo = CType(col, ColumnsInfo)
            _listColumns.Add(selectedCol)
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

   
    Private Sub ComboBoxTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTables.SelectedIndexChanged
        ' Dim tb As TableNameInfo = TableNameInfoBindingSource.Current
        _SelectedTable = CType(ComboBoxTables.SelectedItem, TableNameInfo)
        If _SelectedTable IsNot Nothing Then
            CheckedListBoxColumns.DataSource = _SelectedTable.ListColumn
            CheckedListBoxColumns.DisplayMember = "ColumnName"
            CheckedListBoxColumns.ValueMember = "ColumnValue"
        End If
    End Sub
End Class