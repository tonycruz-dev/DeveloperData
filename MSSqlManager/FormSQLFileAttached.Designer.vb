<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSQLFileAttached
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSQLFileAttached))
        Me.lblResults = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnProcessData = New System.Windows.Forms.Button
        Me.txtFullLocation = New System.Windows.Forms.TextBox
        Me.btnFindPath = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.RBSQLServer = New System.Windows.Forms.RadioButton
        Me.RBWindows = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblResults
        '
        Me.lblResults.AutoSize = True
        Me.lblResults.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!)
        Me.lblResults.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblResults.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblResults.Location = New System.Drawing.Point(16, 327)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(54, 16)
        Me.lblResults.TabIndex = 19
        Me.lblResults.Text = "% Done"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 301)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(375, 23)
        Me.ProgressBar1.TabIndex = 18
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.RBSQLServer)
        Me.GroupBox1.Controls.Add(Me.RBWindows)
        Me.GroupBox1.Controls.Add(Me.ButtonCancel)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnProcessData)
        Me.GroupBox1.Controls.Add(Me.txtFullLocation)
        Me.GroupBox1.Controls.Add(Me.btnFindPath)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(375, 191)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Get SQL Server Database File"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(7, 147)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(144, 31)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Open SQL Server File"
        '
        'btnProcessData
        '
        Me.btnProcessData.Location = New System.Drawing.Point(233, 147)
        Me.btnProcessData.Name = "btnProcessData"
        Me.btnProcessData.Size = New System.Drawing.Size(123, 31)
        Me.btnProcessData.TabIndex = 3
        Me.btnProcessData.Text = "Process Data"
        Me.btnProcessData.UseVisualStyleBackColor = True
        '
        'txtFullLocation
        '
        Me.txtFullLocation.Location = New System.Drawing.Point(9, 46)
        Me.txtFullLocation.Name = "txtFullLocation"
        Me.txtFullLocation.Size = New System.Drawing.Size(314, 20)
        Me.txtFullLocation.TabIndex = 2
        '
        'btnFindPath
        '
        Me.btnFindPath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindPath.Location = New System.Drawing.Point(329, 44)
        Me.btnFindPath.Name = "btnFindPath"
        Me.btnFindPath.Size = New System.Drawing.Size(28, 23)
        Me.btnFindPath.TabIndex = 1
        Me.btnFindPath.Text = "..."
        Me.btnFindPath.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(387, 98)
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Arial Black", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(196, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(179, 24)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "SQL Database File"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(56, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(56, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "User Name:"
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(125, 121)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(232, 20)
        Me.txtPassword.TabIndex = 13
        '
        'txtUserName
        '
        Me.txtUserName.Enabled = False
        Me.txtUserName.Location = New System.Drawing.Point(125, 95)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(232, 20)
        Me.txtUserName.TabIndex = 12
        '
        'RBSQLServer
        '
        Me.RBSQLServer.AutoSize = True
        Me.RBSQLServer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RBSQLServer.Location = New System.Drawing.Point(125, 72)
        Me.RBSQLServer.Name = "RBSQLServer"
        Me.RBSQLServer.Size = New System.Drawing.Size(87, 17)
        Me.RBSQLServer.TabIndex = 10
        Me.RBSQLServer.Text = "SQL Security"
        Me.RBSQLServer.UseVisualStyleBackColor = True
        '
        'RBWindows
        '
        Me.RBWindows.AutoSize = True
        Me.RBWindows.Checked = True
        Me.RBWindows.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RBWindows.Location = New System.Drawing.Point(7, 72)
        Me.RBWindows.Name = "RBWindows"
        Me.RBWindows.Size = New System.Drawing.Size(110, 17)
        Me.RBWindows.TabIndex = 11
        Me.RBWindows.TabStop = True
        Me.RBWindows.Text = "Windows Security"
        Me.RBWindows.UseVisualStyleBackColor = True
        '
        'FormSQLFileAttached
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 353)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblResults)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormSQLFileAttached"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Connection "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblResults As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnProcessData As System.Windows.Forms.Button
    Friend WithEvents txtFullLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnFindPath As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents RBSQLServer As System.Windows.Forms.RadioButton
    Friend WithEvents RBWindows As System.Windows.Forms.RadioButton
End Class
