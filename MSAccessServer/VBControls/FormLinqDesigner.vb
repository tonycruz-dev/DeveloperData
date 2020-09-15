Imports DBExtenderLib
'Imports ManagLinqFile

Public Class FormLinqDesigner

    Private LinqClass As New LinqDatabaseNameInfo
    Public Sub New(ByVal DBList As DatabaseNameInfo)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DisplayTree(tvOpenDBServer, DBList)
        TextBoxdatabase.Text = DBList.DatabaseName
        TextBoxProject.Text = DBList.DatabaseName & "Proj"
        TextBoxSaveLocation.Text = DBList.MSAccessPath

        TextBoxNameSpace.Text = "Web"
    End Sub
    Public Sub New(ByVal DBList As DatabaseNameInfo, ByVal LinqDB As LinqDatabaseNameInfo)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        LinqClass = LinqDB
        DisplayTree(tvOpenDBServer, DBList)
        ' TextBoxdatabase.Text = DBList.DatabaseName
        ListTableBindingSource.DataSource = LinqDB.ListTable
        TextBoxdatabase.Text = LinqDB.DatabaseName
        TextBoxProject.Text = LinqDB.ProjectName
        TextBoxSaveLocation.Text = LinqDB.SaveLocation
        TextBoxNameSpace.Text = LinqDB.ClassNameSpace
    End Sub
    Public Sub DisplayTree(ByVal tv As TreeView, ByVal DBList As DatabaseNameInfo)
        Dim tnDatabases As New TreeNode("Databases")
        tnDatabases.ImageIndex = 15
        tnDatabases.SelectedImageIndex = 15

        Dim dbNode As TreeNodeDatabase
        dbNode = New TreeNodeDatabase(DBList)
        Dim tnTables As New TreeNode("Tables")
        tnTables.ImageIndex = 15
        tnTables.SelectedImageIndex = 15
       
        If LinqClass.ListTable.Count > 0 Then
            If LinqClass.ListTable.Where(Function(ct) ct.TableType = "Table").Count = DBList.ListTable.Where(Function(qt) qt.TableType = "Table").Count Then
                tnTables.Checked = True
            End If
        End If

        For Each tb As TableNameInfo In DBList.ListTable
            Dim tbNode As New TreeNodeTable(tb)

            tbNode.ImageIndex = 3
            tbNode.SelectedImageIndex = 3
            If LinqClass.ListTable.Count > 0 Then
                Dim tbName = tb.Name
                Dim results = LinqClass.ListTable.Where(Function(ctb) ctb.TableName = tbName).SingleOrDefault
                If results Is Nothing Then
                    tbNode.Checked = False
                Else
                    tbNode.Checked = True
                End If
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
        If LinqClass.ListViews.Count = DBList.ListViews.Count Then
            tnViews.Checked = True
        End If
        For Each OView As ViewNameInfo In DBList.ListViews
            Dim tbViewNode As New TreeNodeView(OView)
            tbViewNode.ImageIndex = 6
            tbViewNode.SelectedImageIndex = 6
            If LinqClass.ListTable.Count > 0 Then
                Dim tbName = OView.Name
                Dim results = LinqClass.ListTable.Where(Function(ctb) ctb.TableName = tbName).SingleOrDefault
                If results Is Nothing Then
                    tbViewNode.Checked = False
                Else
                    tbViewNode.Checked = True
                End If

            End If

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
    Private Sub tvOpenDBServer_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvOpenDBServer.ItemDrag
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.tvOpenDBServer.DoDragDrop(e.Item, DragDropEffects.Move Or DragDropEffects.Copy)
        End If
    End Sub
    Private Sub DataRepeater1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataRepeater1.DragDrop
        If e.Data.GetDataPresent(GetType(TreeNodeTable)) Then
            Dim ONode As TreeNodeTable = CType(e.Data.GetData(GetType(TreeNodeTable)), TreeNodeTable)
            ONode.Table.TableType = "Table"
            CopyTable(ONode.Table)

        End If
        If e.Data.GetDataPresent(GetType(TreeNodeView)) Then
            Dim ONode As TreeNodeView = CType(e.Data.GetData(GetType(TreeNodeView)), TreeNodeView)
            CopyView(ONode.View)
        End If
    End Sub
    Private Sub DataRepeater1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataRepeater1.DragEnter
        If e.Data.GetDataPresent(GetType(TreeNodeTable)) Or e.Data.GetDataPresent(GetType(TreeNodeView)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
#Region "Table manager"
    Private Sub CopyTable(ByVal tb As TableNameInfo)
        tb.TableType = "Table"
        Dim tbnew = LinqTableNameinfo.CopyTable(tb)
        'LinqClass.ListTable.Add(tb)
        LinqClass.ListLinqTable.Add(tbnew)
        OrganazrTableRelations()
        Dim results = From stb In LinqClass.ListLinqTable Select stb
        ListTableBindingSource.DataSource = results
    End Sub
    Private Sub RemoveTable(ByVal tb As TableNameInfo)
        'LinqClass.ListTable.Remove(tb)
        LinqClass.ListLinqTable.Remove(tb)
        OrganazrTableRelations()
        Dim results = From stb In LinqClass.ListLinqTable Select stb
        ListTableBindingSource.DataSource = results
    End Sub
    Private Sub RemoveTableView(ByVal vi As ViewNameInfo)
        Dim tbv = (From tv In LinqClass.ListTable Where tv.TableName = vi.ViewName_Name).SingleOrDefault
        'LinqClass.ListTable.Remove(tbv)
        LinqClass.ListLinqTable.Remove(tbv)
        OrganazrTableRelations()
        Dim results = From stb In LinqClass.ListLinqTable Select stb
        ListTableBindingSource.DataSource = results
    End Sub
    Private Sub CopyView(ByVal tbView As ViewNameInfo)
        Dim tb As TableNameInfo = TableNameInfo.CreateTableFromView(tbView)
        tb.TableType = "View"
        Dim tbnew = LinqTableNameinfo.CopyTable(tb)
        'LinqClass.ListTable.Add(tb)
        LinqClass.ListLinqTable.Add(tbnew)
        OrganazrTableRelations()
        LinqClass.ListViews.Add(tbView)
        Dim results = From stb In LinqClass.ListLinqTable Select stb
        ListTableBindingSource.DataSource = results
    End Sub
    Private Sub RemoveView(ByVal tbView As ViewNameInfo)
        RemoveTableView(tbView)
        LinqClass.ListViews.Remove(tbView)
        OrganazrTableRelations()
        Dim results = From stb In LinqClass.ListTable Select stb
        ListTableBindingSource.DataSource = results
    End Sub
#End Region

    Private Sub ListTableBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListTableBindingSource.PositionChanged
        Dim tb As LinqTableNameinfo = ListTableBindingSource.Current
        If Not tb Is Nothing Then
            ListColumnBindingSource.DataSource = tb.ListColumn
            ' OrganazrTableRelations()
            RelateTablesBindingSource.DataSource = (From tr In LinqClass.RelateTables Where tr.RelatedTableValue = tb.TableValue).ToList
            EntityRefBindingSource.DataSource = (From tr In LinqClass.RelateTables Where tr.TableValue = tb.TableValue).ToList
        End If
    End Sub
    Private Sub OrganazrTableRelations()
        LinqClass.RelateTables.Clear()
        Dim listtb = (From tb In LinqClass.ListLinqTable Select tb).ToList

        For Each t In listtb
            Dim lstcol = (From fk In t.GetForeignKey).ToList
            For Each kb In lstcol
                If IsTableSelected(kb.RelatedTable) Then
                    LinqClass.RelateTables.Add(New LinqReatedTables With
                                                              {.ColumnName = kb.ColumnName,
                                                               .ForeignKeyName = kb.ForeignKeyName,
                                                               .RelatedColumnName = kb.RelatedColumnName,
                                                               .RelatedTable = kb.RelatedTable,
                                                               .TableName = t.TableValue,
                                                               .Column = GetColumnInfo(kb.ColumnName, t.TableName),
                                                               .RelateColumn = GetColumnInfo(kb.RelatedColumnName, kb.RelatedTable)})
                End If
            Next
        Next
    End Sub
    Private Function IsTableSelected(ByVal tbName As String) As Boolean
        Dim count = (From tb In LinqClass.ListLinqTable Where tb.TableName = tbName Select tb).Count
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function GetColumnInfo(ByVal ColumnName As String, ByVal TableName As String)
        Dim listtb = (From tb In LinqClass.ListLinqTable
                      Where tb.TableName = TableName Select tb).SingleOrDefault
        Dim SelectedColcol = (From col In listtb.ListColumn Where col.ColumnName = ColumnName).SingleOrDefault
        Return SelectedColcol
    End Function
    Private Sub DataRepeater1_ItemTemplate_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataRepeater1.ItemTemplate.DragDrop
        If e.Data.GetDataPresent(GetType(TreeNodeTable)) Then
            Dim ONode As TreeNodeTable = CType(e.Data.GetData(GetType(TreeNodeTable)), TreeNodeTable)
            CopyTable(ONode.Table)
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeView)) Then
            Dim ONode As TreeNodeView = CType(e.Data.GetData(GetType(TreeNodeView)), TreeNodeView)
            CopyView(ONode.View)
        End If
    End Sub
    Private Sub DataRepeater1_ItemTemplate_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataRepeater1.ItemTemplate.DragEnter
        If e.Data.GetDataPresent(GetType(TreeNodeTable)) Or e.Data.GetDataPresent(GetType(TreeNodeView)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Public ReadOnly Property LinqDatabaseName As LinqDatabaseNameInfo
        Get
            Return LinqClass
        End Get
    End Property

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = vbCancel
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
    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        If TextBoxProject.Text = "" Then
            MessageBox.Show("Please you need to Enter Project Name")
            Exit Sub
        End If
        LinqClass.DatabaseName = TextBoxdatabase.Text
        LinqClass.ProjectName = TextBoxProject.Text
        LinqClass.SaveLocation = TextBoxSaveLocation.Text
        LinqClass.ClassNameSpace = TextBoxNameSpace.Text
        LinqClass.LinqDatabaseName = LinqClass.DatabaseName & "DataContext"

        Me.DialogResult = vbOK
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Me.DialogResult = vbCancel
    End Sub

    Private Sub ButtonOpenFolderLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpenFolderLocation.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxSaveLocation.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub tvOpenDBServer_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvOpenDBServer.AfterSelect

    End Sub
End Class