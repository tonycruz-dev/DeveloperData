Imports DBExtenderLib
Imports DBCodeGenerator
Imports System.Text

Public Class SQLEditorUserControl
    Private oManager As VBServerManager
    Private _SelectedDatabase As DatabaseNameInfo
    Public Event DisplayDataset(ByVal dsValue As DataSet)

    Public Sub LoadDatabase(ByVal objToSave As VBServerManager)
        oManager = objToSave
        TSCmbDatabase.ComboBox.DataSource = Nothing
        TSCmbDatabase.ComboBox.DataSource = objToSave.DatabaseList
        TSCmbDatabase.ComboBox.DisplayMember = "DatabaseName"
        TSCmbDatabase.ComboBox.ValueMember = "DatabaseID"

        _SelectedDatabase = objToSave.DatabaseList.Item(0)
        TSCmbTables.ComboBox.DataSource = Nothing
        TSCmbTables.ComboBox.DataSource = _SelectedDatabase.ListTable
        TSCmbTables.ComboBox.DisplayMember = "TableName"
        TSCmbTables.ComboBox.ValueMember = "TableID"
        loadIconselected()
    End Sub
    Private Sub loadIconselected()
        If _SelectedDatabase.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftAccess Then
            TSBSelectedDB.Image = My.Resources.Accicons
        End If
    End Sub
    Private Sub SqlServerEditor_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles SqlServerEditor.DragDrop
        If e.Data.GetDataPresent(GetType(TreeNodeTable)) Then
            Dim ONode As TreeNodeTable = CType(e.Data.GetData(GetType(TreeNodeTable)), TreeNodeTable)
            SqlServerEditor.Text = SQLCodeGen.CreateSelectCommand(ONode.Table)
        End If

        If e.Data.GetDataPresent(GetType(TreeNodeProcedures)) Then
            Dim ONode As TreeNodeProcedures = CType(e.Data.GetData(GetType(TreeNodeProcedures)), TreeNodeProcedures)
            SqlServerEditor.Text = ONode.SP_Procedure.SP_TextHeader
            SqlServerEditor.Text &= ONode.SP_Procedure.SP_TextBody
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeView)) Then
            Dim ONode As TreeNodeView = CType(e.Data.GetData(GetType(TreeNodeView)), TreeNodeView)
            SqlServerEditor.Text = ONode.View.TextHeader
            SqlServerEditor.Text &= ONode.View.TextBody
        End If
    End Sub

    Private Sub SqlServerEditor_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles SqlServerEditor.DragEnter
        If e.Data.GetDataPresent(GetType(TreeNodeTable)) Or e.Data.GetDataPresent(GetType(TreeNodeProcedures)) _
   Or e.Data.GetDataPresent(GetType(TreeNodeView)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub SqlServerEditor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SqlServerEditor.TextChanged

    End Sub
    Private Sub TSCmbDatabase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSCmbDatabase.SelectedIndexChanged
        _SelectedDatabase = CType(TSCmbDatabase.ComboBox.SelectedItem, DatabaseNameInfo)
        If Not _SelectedDatabase Is Nothing Then
            TSCmbTables.ComboBox.DataSource = _SelectedDatabase.ListTable
            loadIconselected()
        End If
    End Sub

    Private Sub SelectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectToolStripMenuItem.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
            SqlServerEditor.Text = SQLCodeGen.CreateSelectCommand(objTable)
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
            SqlServerEditor.Text = SQLCodeGen.CreateSQLDELETECommandParam(objTable)
        End If
    End Sub

    Private Sub InsertToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertToolStripMenuItem.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
            SqlServerEditor.Text = SQLCodeGen.CreateInsert(objTable)
        End If
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripMenuItem.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
            SqlServerEditor.Text = SQLCodeGen.CreateUpdate(objTable)
        End If
    End Sub

    Private Sub RunQueryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunQueryButton.Click
        If SqlServerEditor.Text <> "" Then
            ExecuteQuery(SqlServerEditor.Text)
        End If
    End Sub


    Private Sub ExecuteQuery(Sql)

        If _SelectedDatabase.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftAccess Then
            DataGridView1.DataSource = MSAccessManager.dsHelper.GetDataSet(SqlServerEditor.Text, _SelectedDatabase.ConnectionString).Tables(0)
        End If
        If _SelectedDatabase.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServer Then
            DataGridView1.DataSource = MSSqlManager.dsHelper.GetDataSet(SqlServerEditor.Text, _SelectedDatabase.ConnectionString).Tables(0)
        End If
        If _SelectedDatabase.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerFile Then
            DataGridView1.DataSource = MSSqlManager.dsHelper.GetDataSet(SqlServerEditor.Text, _SelectedDatabase.ConnectionString).Tables(0)
        End If
    End Sub
    Private Sub GenarateInsertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenarateInsertToolStripMenuItem.Click
        Dim sb As New StringBuilder()
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim Statment As String = SQLCodeGen.CreateInsertValues(objTable)
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(objTable)
        Dim dsTB As DataTable = Nothing

        If objTable.StrConnection.Contains("OLEDB") Then
            dsTB = dsHelper.GetDataSet(sqlstr, objTable.StrConnection).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(objTable.StrConnection, sqlstr).Tables(0)
        End If

        For Each r In dsTB.Rows

            Dim sbConv As New StringBuilder
            For Each col In objTable.ListColumn
                sbConv.AppendLine("                             ." & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "                             })")
        Next
        sb.AppendLine()
        SqlServerEditor.Text = sb.ToString

    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

            Dim frmResults As New FormResultsDisplay
            frmResults.TextBoxResults.Text = SQLCodeGen.CreateStoredProcedureInsert(objTable)
            frmResults.Show()
        End If

        'If _SelectedDatabase Is Nothing Then
        '    Exit Sub
        'End If
        'Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        ''objTable.Database = _SelectedDatabase

        'Dim frmTable As New FormSqlInsertGenerator(_SelectedDatabase)
        'Dim cols As List(Of ColumnsInfo) = Nothing
        'Dim whereClose = ""
        'Dim isOK = frmTable.ShowDialog()
        'Dim selectedTable As TableNameInfo = Nothing

        'If isOK = DialogResult.OK Then
        '    selectedTable = frmTable.SelectedTable

        '    cols = frmTable.listSelectedColumns
        '    whereClose = frmTable.WhereClose
        '    frmTable.Close()
        'Else
        '    frmTable.Close()
        '    Exit Sub
        'End If
        'Dim frmResults As New FormResultsDisplay
        'frmResults.TextBoxResults.Text = VBClassManager.InsertSqlCollectionSample(selectedTable, cols, whereClose)
        'frmResults.Show()
        'VBCodeEditor.Text = VBClassManager.VBClass2010CollectionSample(selectedTable, cols)
    End Sub

    Private Sub GenerateInsertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateInsertToolStripMenuItem.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

            Dim frmResults As New FormResultsDisplay
            frmResults.TextBoxResults.Text = SQLCodeGen.CreateStoredProcedureSelectAll(objTable)
            frmResults.Show()
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)


            Dim frmResults As New FormResultsDisplay
            frmResults.TextBoxResults.Text = SQLCodeGen.CreateStoredProcedureDelete(objTable)
            frmResults.Show()
        End If
    End Sub

    Private Sub SelecteByIdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelecteByIdToolStripMenuItem.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

            Dim frmResults As New FormResultsDisplay
            frmResults.TextBoxResults.Text = SQLCodeGen.CreateStoredProcedureSelectById(objTable)
            frmResults.Show()
        End If
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If Not _SelectedDatabase Is Nothing Then
            Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

            Dim frmResults As New FormResultsDisplay
            frmResults.TextBoxResults.Text = SQLCodeGen.CreateStoredProcedureUpdate(objTable)
            frmResults.Show()
        End If
    End Sub
End Class
