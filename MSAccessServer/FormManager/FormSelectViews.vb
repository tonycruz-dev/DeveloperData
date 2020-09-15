Imports DBExtenderLib

Public Class FormSelectViews
    Private _listColumns As New List(Of ColumnsInfo)
    Private _SelectedViews As ViewNameInfo
    Public Sub New(db As DatabaseNameInfo)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TableNameInfoBindingSource.DataSource = db.ListViews
        _SelectedViews = TableNameInfoBindingSource.Current

        CheckedListBoxColumns.DataSource = _SelectedViews.ViewColumns
        CheckedListBoxColumns.DisplayMember = "ColumnName"
        CheckedListBoxColumns.ValueMember = "ColumnValue"
    End Sub

    Private Sub ComboBoxTables_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxTables.SelectedIndexChanged
        ' Dim tb As TableNameInfo = TableNameInfoBindingSource.Current
        _SelectedViews = CType(ComboBoxTables.SelectedItem, ViewNameInfo)
        If _SelectedViews IsNot Nothing Then
            CheckedListBoxColumns.DataSource = _SelectedViews.ViewColumns
            CheckedListBoxColumns.DisplayMember = "ColumnName"
            CheckedListBoxColumns.ValueMember = "ColumnValue"
        End If

    End Sub
    Public ReadOnly Property SelectedViews As ViewNameInfo
        Get
            Return _SelectedViews
        End Get
    End Property
    Public ReadOnly Property listSelectedColumns As List(Of ColumnsInfo)
        Get
            Return _listColumns
        End Get
    End Property
    Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click
        For Each col In CheckedListBoxColumns.CheckedItems
            Dim selectedCol As ColumnsInfo = CType(col, ColumnsInfo)
            _listColumns.Add(selectedCol)
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class