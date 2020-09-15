Imports DBExtenderLib

Public Class FormWPFDesigner
    Private wpfContent As New WPFDataContext
    Private _DBList As DatabaseNameInfo
    Public Sub New(ByVal DBList As DatabaseNameInfo)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DisplayTree(tvOpenDBServer, DBList)
        _DBList = DBList
        wpfContent.Database = DBList
        TextBoxdatabase.Text = DBList.DatabaseName
        TextBoxProject.Text = DBList.DatabaseName & "Proj"
        TextBoxSaveLocation.Text = DBList.MSAccessPath
    End Sub
    Public Sub DisplayTree(ByVal tv As TreeView, ByVal DBList As DatabaseNameInfo)
        Dim tnDatabases As New TreeNode("Databases")
        tnDatabases.ImageIndex = 15
        tnDatabases.SelectedImageIndex = 15
        _DBList = DBList
        Dim dbNode As TreeNodeDatabase
        dbNode = New TreeNodeDatabase(DBList)
        Dim tnTables As New TreeNode("Tables")
        tnTables.ImageIndex = 15
        tnTables.SelectedImageIndex = 15

        If wpfContent.ListwpfTable.Count > 0 Then
            'If wpfContent.ListTable.Where(Function(ct) ct.TableType = "Table").Count = DBList.ListTable.Where(Function(qt) qt.TableType = "Table").Count Then
            '    tnTables.Checked = True
            'End If
        End If

        For Each tb As TableNameInfo In DBList.ListTable
            Dim tbNode As New TreeNodeTable(tb)

            tbNode.ImageIndex = 3
            tbNode.SelectedImageIndex = 3
            If wpfContent.ListwpfTable.Count > 0 Then
                'Dim tbName = tb.Name
                'Dim results = LinqClass.ListTable.Where(Function(ctb) ctb.TableName = tbName).SingleOrDefault
                'If results Is Nothing Then
                '    tbNode.Checked = False
                'Else
                '    tbNode.Checked = True
                'End If
            End If
            Dim NodeCol As New TreeNode("Columns")
            NodeCol.SelectedImageIndex = 15
            NodeCol.ImageIndex = 15


            For Each Col As ColumnsInfo In tb.ListColumn
                Dim NodeColumn As New TreeNodeColumns(Col)
                If Col.IsPrimary_Key Then
                    NodeColumn.ImageIndex = 12
                    NodeColumn.SelectedImageIndex = 12
                ElseIf Col.IsForeign_Key Then
                    NodeColumn.ImageIndex = 16
                    NodeColumn.SelectedImageIndex = 16
                Else
                    NodeColumn.ImageIndex = 4
                    NodeColumn.SelectedImageIndex = 4
                End If

                NodeCol.Nodes.Add(NodeColumn)
            Next
            tbNode.Nodes.Add(NodeCol)
            tnTables.Expand()
            tnTables.Nodes.Add(tbNode)

        Next
        Dim tnViews As New TreeNode("Views")
        tnViews.ImageIndex = 15
        tnViews.SelectedImageIndex = 15
        'If LinqClass.ListViews.Count = DBList.ListViews.Count Then
        '    tnViews.Checked = True
        'End If
        For Each OView As ViewNameInfo In DBList.ListViews
            Dim tbViewNode As New TreeNodeView(OView)
            tbViewNode.ImageIndex = 6
            tbViewNode.SelectedImageIndex = 6
            'If LinqClass.ListTable.Count > 0 Then
            '    Dim tbName = OView.Name
            '    Dim results = LinqClass.ListTable.Where(Function(ctb) ctb.TableName = tbName).SingleOrDefault
            '    If results Is Nothing Then
            '        tbViewNode.Checked = False
            '    Else
            '        tbViewNode.Checked = True
            '    End If

            'End If

            Dim NodeCol As New TreeNode("Columns")
            NodeCol.SelectedImageIndex = 15
            NodeCol.ImageIndex = 15
            For Each Col As ColumnsInfo In OView.ViewColumns
                Dim NodeColumn As New TreeNode(Col.ColumnName)
                If Col.IsPrimary_Key Then
                    NodeColumn.ImageIndex = 12
                    NodeColumn.SelectedImageIndex = 12
                ElseIf Col.IsForeign_Key Then
                    NodeColumn.ImageIndex = 16
                    NodeColumn.SelectedImageIndex = 16
                Else
                    NodeColumn.ImageIndex = 4
                    NodeColumn.SelectedImageIndex = 4
                End If

                NodeCol.Nodes.Add(NodeColumn)
            Next
            tbViewNode.Nodes.Add(NodeCol)
            tnViews.Nodes.Add(tbViewNode)
        Next

        dbNode.Nodes.Add(tnTables)
        dbNode.Nodes.Add(tnViews)
        dbNode.Expand()
        tnDatabases.Nodes.Add(dbNode)
        tnDatabases.Expand()
        tv.Nodes.Add(tnDatabases)
        tv.Nodes(0).Expand()


    End Sub

    Private Sub tvOpenDBServer_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvOpenDBServer.AfterCheck
        If e.Action <> TreeViewAction.Unknown Then
            If e.Node.Nodes.Count > 0 Then
                ' Calls the CheckAllChildNodes method, passing in the current 
                ' Checked value of the TreeNode whose checked state changed. 
                Me.CheckAllChildNodes(e.Node, e.Node.Checked)
            End If
        End If
    End Sub
#Region "Private Functions"
    ' Updates all child tree nodes recursively.
    Private Sub CheckAllChildNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
        ' Dim node As TreeNode
        For Each node In treeNode.Nodes
            node.Checked = nodeChecked
            If node.Nodes.Count > 0 Then
                ' If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                Me.CheckAllChildNodes(node, nodeChecked)
                If nodeChecked Then
                    If TypeOf treeNode Is TreeNodeTable Then
                        Dim ONode As TreeNodeTable = CType(treeNode, TreeNodeTable)
                        CopyTable(ONode.Table)
                    End If
                    If TypeOf treeNode Is TreeNodeView Then
                        Dim ONode As TreeNodeView = CType(treeNode, TreeNodeView)
                        CopyView(ONode.View)
                    End If
                Else
                    If TypeOf treeNode Is TreeNodeTable Then
                        Dim ONode As TreeNodeTable = CType(treeNode, TreeNodeTable)
                        RemoveTable(ONode.Table)
                    End If
                    If TypeOf treeNode Is TreeNodeView Then
                        Dim ONode As TreeNodeView = CType(treeNode, TreeNodeView)
                        RemoveView(ONode.View)
                    End If
                End If

            End If
        Next node
    End Sub

#End Region


    Private Sub CopyTable(ByVal tb As TableNameInfo)
        tb.TableType = "Table"
        Dim tbnew = WPFTablesInfo.CopyTableToWPF(tb)
        wpfContent.ListwpfTable.Add(tbnew)
        Dim results = From stb In wpfContent.ListwpfTable Select stb
        WPFTablesInfoBindingSource.DataSource = results
    End Sub
    Private Sub RemoveTable(ByVal tb As TableNameInfo)
        Dim rt = (From rtw In wpfContent.ListwpfTable Where rtw.MainTable.TableName = tb.TableName Select rtw).SingleOrDefault
        wpfContent.ListwpfTable.Remove(rt)
        Dim results = From stb In wpfContent.ListwpfTable Select stb
        WPFTablesInfoBindingSource.DataSource = results
    End Sub
    Private Sub CopyView(ByVal tbView As ViewNameInfo)
        Dim tb As TableNameInfo = TableNameInfo.CreateTableFromView(tbView)
        tb.TableType = "View"
        Dim tbnew = WPFTablesInfo.CopyTableToWPF(tb)
        wpfContent.ListwpfTable.Add(tbnew)
        Dim results = From stb In wpfContent.ListwpfTable Select stb
        WPFTablesInfoBindingSource.DataSource = results
    End Sub
    Private Sub RemoveView(ByVal tb As ViewNameInfo)
        Dim rt = (From rtw In wpfContent.ListwpfTable Where rtw.MainTable.TableName = tb.ViewName_Name Select rtw).SingleOrDefault
        wpfContent.ListwpfTable.Remove(rt)
        Dim results = From stb In wpfContent.ListwpfTable Select stb
        WPFTablesInfoBindingSource.DataSource = results
    End Sub
    Private Sub tvOpenDBServer_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvOpenDBServer.AfterSelect

    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        'Dim cr As WPFTablesInfo = WPFTablesInfoBindingSource.Current
        'Dim listcoldg = (From col In cr.ListColumn Select col).ToList

        'Dim frmeditcolm As New FormEditColumns(_DBList, wpfContent, listcoldg)
        'frmeditcolm.ShowDialog()

        Dim editCol As wpfColumnInfo = ListColumnBindingSource.Current
        Dim formedit As New FormEditColumn(editCol)
        formedit.ShowDialog()

    End Sub

    Private Sub ListColumnDataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ListColumnDataGridView1.CellClick
        If ListColumnDataGridView1.Columns(e.ColumnIndex).Name = "AddColumn" Then
            'message = MessageNotepadBindingSource.Current
            'Dim frm As New FormTenantMessage(_TenantName, _PropertyID)
            'frm.Show()
        End If
        If ListColumnDataGridView1.Columns(e.ColumnIndex).Name = "EditColumn" Then
            ' Dim MessageID As Integer = CInt(ListColumnDataGridView1(0, ListColumnDataGridView1.CurrentRow.Index).Value)
            'Message = MessageNotepadBindingSource.Current
            'Dim frm As New FormTenantMessage(Message.NoteID, _TenantName, Message.PropertyID)
            'frm.Show()
        End If
        If ListColumnDataGridView1.Columns(e.ColumnIndex).Name = "DeleteColumn" Then
            Dim messageDelete As String = "Are You sure you want delete this Column?"
            Dim YesNo As DialogResult = MsgBox(messageDelete, MsgBoxStyle.YesNo, "Delete Message")
            If YesNo = DialogResult.Yes Then
                DataGrigListColumnBindingSource.Remove(DataGrigListColumnBindingSource.Current)
                '  Dim DeleteColumn As wpfColumnInfo = DataGrigListColumnBindingSource.Current ' CInt(ListColumnDataGridView1(0, ListColumnDataGridView1.CurrentRow.Index).Value)

            End If

        End If
    End Sub
    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        If TextBoxProject.Text = "" Then
            MessageBox.Show("Please you need to Enter Project Name")
            Exit Sub
        End If
        wpfContent.DatabaseName = TextBoxdatabase.Text
        wpfContent.SaveLocation = TextBoxSaveLocation.Text
        wpfContent.ClassNameSpace = TextBoxNameSpace.Text
        wpfContent.WPFContext = wpfContent.DatabaseName & "DataContext"

        _WPFDatabaseContext = wpfContent
        Me.DialogResult = vbOK
    End Sub

    Private Sub NewToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton2.Click
        Dim editCol As wpfColumnInfo = ListColumnBindingSource.Current
        Dim formedit As New FormEditColumn(editCol)
        If formedit.ShowDialog() = vbOK Then

        End If
    End Sub

    Private Sub ListColumnDataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ListColumnDataGridView2.CellContentClick
        If ListColumnDataGridView2.Columns(e.ColumnIndex).Name = "ComboBoxColumn" Then
            Dim tb As WPFTablesInfo = WPFTablesInfoBindingSource.Current
            Dim frmCToCmb As New FormWPFComboBox(_DBList, tb.MainTable)
            If frmCToCmb.ShowDialog() = vbOK Then
                Dim wpfcol As wpfColumnInfo = DetailsListColumnBindingSource.Current
                Dim wpfcmb As New WPFComboBox With {.DataSource = frmCToCmb.DataSource,
                                                    .DisplayMemberPath = frmCToCmb.DisplayMemberPath,
                                                    .SelectedValue = frmCToCmb.SelectedValue,
                                                    .SelectedValuePath = frmCToCmb.SelectedValuePath}
                wpfcol.ColumnCombobox = wpfcmb
                wpfcol.TypeOfControl = "ComboBox"
                WPFTablesInfoBindingSource_PositionChanged(Nothing, Nothing)
            End If

        End If
    End Sub
    Private Sub WPFTablesInfoBindingSource_PositionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WPFTablesInfoBindingSource.PositionChanged

    End Sub
    Public Property WPFDatabaseContext As New WPFDataContext

End Class