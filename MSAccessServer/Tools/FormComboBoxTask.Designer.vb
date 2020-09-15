<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormComboBoxTask
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelTbTarget = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonSetComboBox = New System.Windows.Forms.Button()
        Me.ComboBoxValueMember = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxListTables = New System.Windows.Forms.ComboBox()
        Me.ComboBoxDisplayMember = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxSelectedValue = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel1.Controls.Add(Me.LabelTbTarget)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(329, 36)
        Me.Panel1.TabIndex = 0
        '
        'LabelTbTarget
        '
        Me.LabelTbTarget.AutoSize = True
        Me.LabelTbTarget.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTbTarget.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LabelTbTarget.Location = New System.Drawing.Point(118, 9)
        Me.LabelTbTarget.Name = "LabelTbTarget"
        Me.LabelTbTarget.Size = New System.Drawing.Size(79, 18)
        Me.LabelTbTarget.TabIndex = 1
        Me.LabelTbTarget.Text = "[TBName]"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Table Target:"
        '
        'ButtonSetComboBox
        '
        Me.ButtonSetComboBox.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonSetComboBox.Location = New System.Drawing.Point(233, 302)
        Me.ButtonSetComboBox.Name = "ButtonSetComboBox"
        Me.ButtonSetComboBox.Size = New System.Drawing.Size(84, 36)
        Me.ButtonSetComboBox.TabIndex = 15
        Me.ButtonSetComboBox.Text = "Finish Close"
        Me.ButtonSetComboBox.UseVisualStyleBackColor = True
        '
        'ComboBoxValueMember
        '
        Me.ComboBoxValueMember.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxValueMember.FormattingEnabled = True
        Me.ComboBoxValueMember.Items.AddRange(New Object() {"=", ">", ">=", "<", "<=", "Like"})
        Me.ComboBoxValueMember.Location = New System.Drawing.Point(12, 201)
        Me.ComboBoxValueMember.Name = "ComboBoxValueMember"
        Me.ComboBoxValueMember.Size = New System.Drawing.Size(305, 28)
        Me.ComboBoxValueMember.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(13, 170)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 20)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Value Member:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(12, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 20)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Data Source:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(13, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 20)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Display Member:"
        '
        'ComboBoxListTables
        '
        Me.ComboBoxListTables.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxListTables.FormattingEnabled = True
        Me.ComboBoxListTables.Location = New System.Drawing.Point(12, 68)
        Me.ComboBoxListTables.Name = "ComboBoxListTables"
        Me.ComboBoxListTables.Size = New System.Drawing.Size(305, 28)
        Me.ComboBoxListTables.TabIndex = 9
        '
        'ComboBoxDisplayMember
        '
        Me.ComboBoxDisplayMember.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxDisplayMember.FormattingEnabled = True
        Me.ComboBoxDisplayMember.Location = New System.Drawing.Point(16, 134)
        Me.ComboBoxDisplayMember.Name = "ComboBoxDisplayMember"
        Me.ComboBoxDisplayMember.Size = New System.Drawing.Size(301, 28)
        Me.ComboBoxDisplayMember.TabIndex = 10
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(12, 302)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 36)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(13, 237)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 20)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Selected Value :"
        '
        'ComboBoxSelectedValue
        '
        Me.ComboBoxSelectedValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxSelectedValue.FormattingEnabled = True
        Me.ComboBoxSelectedValue.Items.AddRange(New Object() {"=", ">", ">=", "<", "<=", "Like"})
        Me.ComboBoxSelectedValue.Location = New System.Drawing.Point(12, 268)
        Me.ComboBoxSelectedValue.Name = "ComboBoxSelectedValue"
        Me.ComboBoxSelectedValue.Size = New System.Drawing.Size(305, 28)
        Me.ComboBoxSelectedValue.TabIndex = 14
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(12, 344)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(305, 86)
        Me.TextBox1.TabIndex = 17
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(121, 302)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(88, 36)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Add"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FormComboBoxTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(329, 434)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ButtonSetComboBox)
        Me.Controls.Add(Me.ComboBoxSelectedValue)
        Me.Controls.Add(Me.ComboBoxValueMember)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxDisplayMember)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxListTables)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FormComboBoxTask"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Combo box Task"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LabelTbTarget As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonSetComboBox As System.Windows.Forms.Button
    Friend WithEvents ComboBoxValueMember As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxListTables As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxDisplayMember As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSelectedValue As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
