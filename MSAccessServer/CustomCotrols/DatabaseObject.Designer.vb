<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseObject
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DatabaseObject))
        Me.ILManageData = New System.Windows.Forms.ImageList(Me.components)
        Me.CMSManagetables = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FormatTableToFormStyleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormatTableToWebFormStyleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormatTableToSilverlightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChageColumnFormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectListViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateLinqQueryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EntityFramewordClassToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntityFrameworkConnectionStringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntityFrameworkCreateTablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvOpenDBServer = New System.Windows.Forms.TreeView()
        Me.HeaderStrip2 = New MSAccessServer.HeaderStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.HeaderStrip1 = New MSAccessServer.HeaderStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.CreateViewModelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CMSManagetables.SuspendLayout()
        Me.HeaderStrip2.SuspendLayout()
        Me.HeaderStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ILManageData
        '
        Me.ILManageData.ImageStream = CType(resources.GetObject("ILManageData.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ILManageData.TransparentColor = System.Drawing.Color.Transparent
        Me.ILManageData.Images.SetKeyName(0, "DatabaseList.bmp")
        Me.ILManageData.Images.SetKeyName(1, "Database.bmp")
        Me.ILManageData.Images.SetKeyName(2, "FolderOriginal.png")
        Me.ILManageData.Images.SetKeyName(3, "TableHS.png")
        Me.ILManageData.Images.SetKeyName(4, "Columns.bmp")
        Me.ILManageData.Images.SetKeyName(5, "StoredProcedure.bmp")
        Me.ILManageData.Images.SetKeyName(6, "Views.bmp")
        Me.ILManageData.Images.SetKeyName(7, "Form.bmp")
        Me.ILManageData.Images.SetKeyName(8, "VBClass.bmp")
        Me.ILManageData.Images.SetKeyName(9, "userControl.bmp")
        Me.ILManageData.Images.SetKeyName(10, "VBDesigner.bmp")
        Me.ILManageData.Images.SetKeyName(11, "WebForm.bmp")
        Me.ILManageData.Images.SetKeyName(12, "PrimaryKey.bmp")
        Me.ILManageData.Images.SetKeyName(13, "Database.bmp")
        Me.ILManageData.Images.SetKeyName(14, "DatabaseMain.bmp")
        Me.ILManageData.Images.SetKeyName(15, "Folders.bmp")
        Me.ILManageData.Images.SetKeyName(16, "ForeignKey.bmp")
        Me.ILManageData.Images.SetKeyName(17, "msaccess.png")
        Me.ILManageData.Images.SetKeyName(18, "mysqlDatabase.bmp")
        Me.ILManageData.Images.SetKeyName(19, "MobileData.bmp")
        Me.ILManageData.Images.SetKeyName(20, "Control_ComboBox.bmp")
        Me.ILManageData.Images.SetKeyName(21, "LinqClass.bmp")
        Me.ILManageData.Images.SetKeyName(22, "domainServices.bmp")
        Me.ILManageData.Images.SetKeyName(23, "LinqData.bmp")
        Me.ILManageData.Images.SetKeyName(24, "RiaClass.bmp")
        Me.ILManageData.Images.SetKeyName(25, "Delete.bmp")
        Me.ILManageData.Images.SetKeyName(26, "Insert.bmp")
        Me.ILManageData.Images.SetKeyName(27, "update.bmp")
        Me.ILManageData.Images.SetKeyName(28, "SelectTB.bmp")
        Me.ILManageData.Images.SetKeyName(29, "wpfMain.bmp")
        Me.ILManageData.Images.SetKeyName(30, "wpffile.bmp")
        Me.ILManageData.Images.SetKeyName(31, "VSProject_generatedfile.bmp")
        '
        'CMSManagetables
        '
        Me.CMSManagetables.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FormatTableToFormStyleToolStripMenuItem, Me.FormatToolStripMenuItem, Me.FormatTableToWebFormStyleToolStripMenuItem, Me.FormatTableToSilverlightToolStripMenuItem, Me.ChageColumnFormatToolStripMenuItem, Me.SelectListViewToolStripMenuItem, Me.CreateViewModelToolStripMenuItem, Me.CreateLinqQueryToolStripMenuItem, Me.ToolStripMenuItem1, Me.EntityFramewordClassToolStripMenuItem, Me.EntityFrameworkConnectionStringToolStripMenuItem, Me.EntityFrameworkCreateTablesToolStripMenuItem})
        Me.CMSManagetables.Name = "CMSManagetables"
        Me.CMSManagetables.Size = New System.Drawing.Size(272, 274)
        '
        'FormatTableToFormStyleToolStripMenuItem
        '
        Me.FormatTableToFormStyleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FormatTableToFormStyleToolStripMenuItem.Name = "FormatTableToFormStyleToolStripMenuItem"
        Me.FormatTableToFormStyleToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.FormatTableToFormStyleToolStripMenuItem.Text = "Format Table to windows Form Style"
        '
        'FormatToolStripMenuItem
        '
        Me.FormatToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.Wpfarp
        Me.FormatToolStripMenuItem.Name = "FormatToolStripMenuItem"
        Me.FormatToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.FormatToolStripMenuItem.Text = "Format WPF"
        '
        'FormatTableToWebFormStyleToolStripMenuItem
        '
        Me.FormatTableToWebFormStyleToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.Webcontrol_FormView
        Me.FormatTableToWebFormStyleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FormatTableToWebFormStyleToolStripMenuItem.Name = "FormatTableToWebFormStyleToolStripMenuItem"
        Me.FormatTableToWebFormStyleToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.FormatTableToWebFormStyleToolStripMenuItem.Text = "Format Table to Web Form Style"
        '
        'FormatTableToSilverlightToolStripMenuItem
        '
        Me.FormatTableToSilverlightToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.silverlightcontrol
        Me.FormatTableToSilverlightToolStripMenuItem.Name = "FormatTableToSilverlightToolStripMenuItem"
        Me.FormatTableToSilverlightToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.FormatTableToSilverlightToolStripMenuItem.Text = "Format Table to Silverlight"
        '
        'ChageColumnFormatToolStripMenuItem
        '
        Me.ChageColumnFormatToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.Control_ComboBox
        Me.ChageColumnFormatToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ChageColumnFormatToolStripMenuItem.Name = "ChageColumnFormatToolStripMenuItem"
        Me.ChageColumnFormatToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.ChageColumnFormatToolStripMenuItem.Text = "Chage Column Format to ComboBox"
        '
        'SelectListViewToolStripMenuItem
        '
        Me.SelectListViewToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.VBFunctions
        Me.SelectListViewToolStripMenuItem.Name = "SelectListViewToolStripMenuItem"
        Me.SelectListViewToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.SelectListViewToolStripMenuItem.Text = "Select List View"
        '
        'CreateLinqQueryToolStripMenuItem
        '
        Me.CreateLinqQueryToolStripMenuItem.Enabled = False
        Me.CreateLinqQueryToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.LinqVB
        Me.CreateLinqQueryToolStripMenuItem.Name = "CreateLinqQueryToolStripMenuItem"
        Me.CreateLinqQueryToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.CreateLinqQueryToolStripMenuItem.Text = "Create Linq Query"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(268, 6)
        '
        'EntityFramewordClassToolStripMenuItem
        '
        Me.EntityFramewordClassToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.EntityFramework
        Me.EntityFramewordClassToolStripMenuItem.Name = "EntityFramewordClassToolStripMenuItem"
        Me.EntityFramewordClassToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.EntityFramewordClassToolStripMenuItem.Text = "Entity Frameword Class"
        '
        'EntityFrameworkConnectionStringToolStripMenuItem
        '
        Me.EntityFrameworkConnectionStringToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.EntityFramework
        Me.EntityFrameworkConnectionStringToolStripMenuItem.Name = "EntityFrameworkConnectionStringToolStripMenuItem"
        Me.EntityFrameworkConnectionStringToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.EntityFrameworkConnectionStringToolStripMenuItem.Text = "Entity Framework Connection String"
        '
        'EntityFrameworkCreateTablesToolStripMenuItem
        '
        Me.EntityFrameworkCreateTablesToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.EntityFramework
        Me.EntityFrameworkCreateTablesToolStripMenuItem.Name = "EntityFrameworkCreateTablesToolStripMenuItem"
        Me.EntityFrameworkCreateTablesToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.EntityFrameworkCreateTablesToolStripMenuItem.Text = "Entity Framework Create tables"
        '
        'tvOpenDBServer
        '
        Me.tvOpenDBServer.ContextMenuStrip = Me.CMSManagetables
        Me.tvOpenDBServer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvOpenDBServer.ImageIndex = 0
        Me.tvOpenDBServer.ImageList = Me.ILManageData
        Me.tvOpenDBServer.Location = New System.Drawing.Point(0, 46)
        Me.tvOpenDBServer.Name = "tvOpenDBServer"
        Me.tvOpenDBServer.SelectedImageIndex = 0
        Me.tvOpenDBServer.Size = New System.Drawing.Size(277, 448)
        Me.tvOpenDBServer.TabIndex = 3
        '
        'HeaderStrip2
        '
        Me.HeaderStrip2.AutoSize = False
        Me.HeaderStrip2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.HeaderStrip2.ForeColor = System.Drawing.Color.Black
        Me.HeaderStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStrip2.HeaderStyle = MSAccessServer.AreaHeaderStyle.Small
        Me.HeaderStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2})
        Me.HeaderStrip2.Location = New System.Drawing.Point(0, 25)
        Me.HeaderStrip2.Name = "HeaderStrip2"
        Me.HeaderStrip2.Size = New System.Drawing.Size(277, 21)
        Me.HeaderStrip2.TabIndex = 1
        Me.HeaderStrip2.Text = "HeaderStrip2"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(167, 18)
        Me.ToolStripLabel2.Text = "Register Server database or file"
        '
        'HeaderStrip1
        '
        Me.HeaderStrip1.AutoSize = False
        Me.HeaderStrip1.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStrip1.ForeColor = System.Drawing.Color.White
        Me.HeaderStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStrip1.HeaderStyle = MSAccessServer.AreaHeaderStyle.Large
        Me.HeaderStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.HeaderStrip1.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStrip1.Name = "HeaderStrip1"
        Me.HeaderStrip1.Size = New System.Drawing.Size(277, 25)
        Me.HeaderStrip1.TabIndex = 0
        Me.HeaderStrip1.Text = "HeaderStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(132, 22)
        Me.ToolStripLabel1.Text = "Object Explorer"
        '
        'CreateViewModelToolStripMenuItem
        '
        Me.CreateViewModelToolStripMenuItem.Image = Global.MSAccessServer.My.Resources.Resources.VBFunctions
        Me.CreateViewModelToolStripMenuItem.Name = "CreateViewModelToolStripMenuItem"
        Me.CreateViewModelToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.CreateViewModelToolStripMenuItem.Text = "Create View Model"
        '
        'DatabaseObject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tvOpenDBServer)
        Me.Controls.Add(Me.HeaderStrip2)
        Me.Controls.Add(Me.HeaderStrip1)
        Me.Name = "DatabaseObject"
        Me.Size = New System.Drawing.Size(277, 494)
        Me.CMSManagetables.ResumeLayout(False)
        Me.HeaderStrip2.ResumeLayout(False)
        Me.HeaderStrip2.PerformLayout()
        Me.HeaderStrip1.ResumeLayout(False)
        Me.HeaderStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HeaderStrip1 As MSAccessServer.HeaderStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents HeaderStrip2 As MSAccessServer.HeaderStrip
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ILManageData As System.Windows.Forms.ImageList
    Friend WithEvents CMSManagetables As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FormatTableToFormStyleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatTableToWebFormStyleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatTableToSilverlightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChageColumnFormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tvOpenDBServer As System.Windows.Forms.TreeView
    Friend WithEvents CreateLinqQueryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EntityFramewordClassToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntityFrameworkConnectionStringToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntityFrameworkCreateTablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectListViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateViewModelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
