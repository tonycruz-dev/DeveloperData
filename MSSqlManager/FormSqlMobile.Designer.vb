<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSqlMobile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSqlMobile))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblResults = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnProcessData = New System.Windows.Forms.Button
        Me.txtFullLocation = New System.Windows.Forms.TextBox
        Me.btnFindPath = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.RBSQLServer = New System.Windows.Forms.RadioButton
        Me.RBWindows = New System.Windows.Forms.RadioButton
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox1.Location = New System.Drawing.Point(-1, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(390, 101)
        Me.PictureBox1.TabIndex = 28
        Me.PictureBox1.TabStop = False
        '
        'lblResults
        '
        Me.lblResults.AutoSize = True
        Me.lblResults.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!)
        Me.lblResults.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblResults.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblResults.Location = New System.Drawing.Point(18, 254)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(54, 16)
        Me.lblResults.TabIndex = 27
        Me.lblResults.Text = "% Done"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ProgressBar1.Location = New System.Drawing.Point(11, 277)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(367, 23)
        Me.ProgressBar1.TabIndex = 26
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
        Me.GroupBox1.Location = New System.Drawing.Point(11, 108)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(367, 141)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Get Compact Database File"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(9, 89)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(140, 30)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Open Sql Server Compact Edition File"
        '
        'btnProcessData
        '
        Me.btnProcessData.Location = New System.Drawing.Point(201, 89)
        Me.btnProcessData.Name = "btnProcessData"
        Me.btnProcessData.Size = New System.Drawing.Size(126, 30)
        Me.btnProcessData.TabIndex = 3
        Me.btnProcessData.Text = "Process Data"
        Me.btnProcessData.UseVisualStyleBackColor = True
        '
        'txtFullLocation
        '
        Me.txtFullLocation.Location = New System.Drawing.Point(9, 37)
        Me.txtFullLocation.Name = "txtFullLocation"
        Me.txtFullLocation.Size = New System.Drawing.Size(318, 20)
        Me.txtFullLocation.TabIndex = 2
        '
        'btnFindPath
        '
        Me.btnFindPath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindPath.Location = New System.Drawing.Point(333, 34)
        Me.btnFindPath.Name = "btnFindPath"
        Me.btnFindPath.Size = New System.Drawing.Size(28, 23)
        Me.btnFindPath.TabIndex = 1
        Me.btnFindPath.Text = "..."
        Me.btnFindPath.UseVisualStyleBackColor = True
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
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(134, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(250, 19)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Sql Server Compact Edition File"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(26, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(58, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "User Name:"
        Me.Label4.Visible = False
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(95, 63)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(232, 20)
        Me.txtPassword.TabIndex = 19
        '
        'txtUserName
        '
        Me.txtUserName.Enabled = False
        Me.txtUserName.Location = New System.Drawing.Point(127, 176)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(232, 20)
        Me.txtUserName.TabIndex = 18
        Me.txtUserName.Visible = False
        '
        'RBSQLServer
        '
        Me.RBSQLServer.AutoSize = True
        Me.RBSQLServer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RBSQLServer.Location = New System.Drawing.Point(127, 153)
        Me.RBSQLServer.Name = "RBSQLServer"
        Me.RBSQLServer.Size = New System.Drawing.Size(87, 17)
        Me.RBSQLServer.TabIndex = 16
        Me.RBSQLServer.Text = "SQL Security"
        Me.RBSQLServer.UseVisualStyleBackColor = True
        Me.RBSQLServer.Visible = False
        '
        'RBWindows
        '
        Me.RBWindows.AutoSize = True
        Me.RBWindows.Checked = True
        Me.RBWindows.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RBWindows.Location = New System.Drawing.Point(9, 153)
        Me.RBWindows.Name = "RBWindows"
        Me.RBWindows.Size = New System.Drawing.Size(110, 17)
        Me.RBWindows.TabIndex = 17
        Me.RBWindows.TabStop = True
        Me.RBWindows.Text = "Windows Security"
        Me.RBWindows.UseVisualStyleBackColor = True
        Me.RBWindows.Visible = False
        '
        'FormSqlMobile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 312)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblResults)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormSqlMobile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormSqlMobile"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblResults As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnProcessData As System.Windows.Forms.Button
    Friend WithEvents txtFullLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnFindPath As System.Windows.Forms.Button
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
