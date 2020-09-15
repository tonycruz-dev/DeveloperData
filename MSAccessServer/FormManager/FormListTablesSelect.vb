Imports DBExtenderLib

Public Class FormListTablesSelect
    Private _ListTables As New List(Of TableNameInfo)
    Private _db As DatabaseNameInfo
    Public Sub New(db As DatabaseNameInfo)


        ' This call is required by the designer.
        InitializeComponent()
        _db = db

        ' Add any initialization after the InitializeComponent() call.
        'CheckedListBoxTables
        CheckedListBoxTables.DataSource = _db.ListTable
        CheckedListBoxTables.DisplayMember = "TableName"
        CheckedListBoxTables.ValueMember = "TableValue"
    End Sub
    Public ReadOnly Property listSelectedTables As List(Of TableNameInfo)
        Get
            Return _ListTables
        End Get
    End Property

    Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click
        For Each col In CheckedListBoxTables.CheckedItems
            Dim selectedTables As TableNameInfo = CType(col, TableNameInfo)
            _ListTables.Add(selectedTables)
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class