Imports DBExtenderLib
Imports DBCodeGenerator

Public Class DatabaseObject
    Dim displayNode As Object
    Public Event ResultText(ByVal Result As TreeViewEventArgs)
    Public Event ResultView(ByVal Result As TreeNodeView)
    Public Event CreateViewModel(ByVal Result As TableNameInfo, db As DatabaseNameInfo)
    Public Event CreateSelecViewModel(ByVal Result As TableNameInfo, db As DatabaseNameInfo)
    Public Event ResultCreateFramewordClass(ByVal Result As TableNameInfo, db As DatabaseNameInfo)
    Public Event ResultEntityFrameworkConnectionString(ByVal db As DatabaseNameInfo)
    Public Event ResultEntityFrameworkEntityContext(ByVal db As DatabaseNameInfo)
    Public ReadOnly Property DBTreeView() As TreeView
        Get
            Return Me.tvOpenDBServer
        End Get
    End Property
    Public ReadOnly Property DBImageList() As ImageList
        Get
            Return Me.ILManageData
        End Get
    End Property
    Public Property SelectedDB As DatabaseNameInfo

    Private Sub tvOpenDBServer_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvOpenDBServer.AfterSelect
        RaiseEvent ResultText(e)
        'SelectedDB = CType(tvOpenDBServer.Nodes, TreeNodeDatabase)

    End Sub
    Private Sub tvOpenDBServer_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvOpenDBServer.ItemDrag
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.tvOpenDBServer.DoDragDrop(e.Item, DragDropEffects.Move)
        End If
    End Sub
    Private Sub tvOpenDBServer_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvOpenDBServer.MouseDown
        If TypeOf tvOpenDBServer.SelectedNode Is TreeNodeTable Then
            FormatTableToFormStyleToolStripMenuItem.Enabled = True
            FormatToolStripMenuItem.Enabled = True
            FormatTableToWebFormStyleToolStripMenuItem.Enabled = True
            FormatTableToSilverlightToolStripMenuItem.Enabled = True
            ChageColumnFormatToolStripMenuItem.Enabled = False
        ElseIf TypeOf tvOpenDBServer.SelectedNode Is TreeNodeColumns Then
            ChageColumnFormatToolStripMenuItem.Enabled = True
        ElseIf TypeOf tvOpenDBServer.SelectedNode Is TreeNodeView Then
            CreateLinqQueryToolStripMenuItem.Enabled = True
        Else
            ChageColumnFormatToolStripMenuItem.Enabled = False
            FormatTableToFormStyleToolStripMenuItem.Enabled = False
            FormatToolStripMenuItem.Enabled = False
            FormatTableToWebFormStyleToolStripMenuItem.Enabled = False
            FormatTableToSilverlightToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub ChageColumnFormatToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChageColumnFormatToolStripMenuItem.Click

    End Sub

    Private Sub CreateLinqQueryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateLinqQueryToolStripMenuItem.Click

        RaiseEvent ResultView(tvOpenDBServer.SelectedNode)

        'Dim tn As TreeNodeView = 
        'Dim tb As TableNameInfo = TableNameInfo.CreateTableFromView(tn.View)

        'e.Data.GetDataPresent(GetType(TreeNodeTableLinq)) Or
        ' 
        'tb.TableType = "View"
    End Sub

    Private Sub EntityFramewordClassToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntityFramewordClassToolStripMenuItem.Click
        If SelectedDB Is Nothing Then
            Exit Sub
        End If
        Dim node = tvOpenDBServer.SelectedNode
        If TypeOf node Is TreeNodeTable Then
            Dim TbNode As TreeNodeTable = CType(tvOpenDBServer.SelectedNode, TreeNodeTable)
            Dim objTable As TableNameInfo = TbNode.Table
            RaiseEvent ResultCreateFramewordClass(objTable, SelectedDB)
        ElseIf TypeOf node Is TreeNodeView Then
            Dim TbNode As TreeNodeView = CType(tvOpenDBServer.SelectedNode, TreeNodeView)
            Dim objTable As TableNameInfo = TableNameInfo.CreateTableFromView(TbNode.View)
            RaiseEvent ResultCreateFramewordClass(objTable, SelectedDB)
        End If


    End Sub

    Private Sub EntityFrameworkConnectionStringToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntityFrameworkConnectionStringToolStripMenuItem.Click
        If TypeOf tvOpenDBServer.SelectedNode Is TreeNodeDatabase Then
            Dim DBNode As TreeNodeDatabase = CType(tvOpenDBServer.SelectedNode, TreeNodeDatabase)
            Dim db As DatabaseNameInfo = DBNode.Database
            RaiseEvent ResultEntityFrameworkConnectionString(db)
        End If
    End Sub

    Private Sub EntityFrameworkCreateTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntityFrameworkCreateTablesToolStripMenuItem.Click
        If TypeOf tvOpenDBServer.SelectedNode Is TreeNodeDatabase Then
            Dim DBNode As TreeNodeDatabase = CType(tvOpenDBServer.SelectedNode, TreeNodeDatabase)
            Dim db As DatabaseNameInfo = DBNode.Database
            RaiseEvent ResultEntityFrameworkEntityContext(db)
        End If
    End Sub

    Private Sub SelectListViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectListViewToolStripMenuItem.Click
        Dim node = tvOpenDBServer.SelectedNode
        If TypeOf node Is TreeNodeView Then
            Dim TbNode As TreeNodeView = CType(tvOpenDBServer.SelectedNode, TreeNodeView)
            Dim objTable As TableNameInfo = TableNameInfo.CreateTableFromView(TbNode.View)
            RaiseEvent CreateSelecViewModel(objTable, SelectedDB)
        End If
    End Sub

    Private Sub CreateViewModelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateViewModelToolStripMenuItem.Click
        Dim node = tvOpenDBServer.SelectedNode
        If TypeOf node Is TreeNodeView Then
            Dim TbNode As TreeNodeView = CType(tvOpenDBServer.SelectedNode, TreeNodeView)
            Dim objTable As TableNameInfo = TableNameInfo.CreateTableFromView(TbNode.View)
            RaiseEvent CreateViewModel(objTable, SelectedDB)
        End If
    End Sub
End Class
