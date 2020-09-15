<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditColumns
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
        Me.components = New System.ComponentModel.Container()
        Dim ColumnNameLabel As System.Windows.Forms.Label
        Dim ColumnValueLabel As System.Windows.Forms.Label
        Dim SizeLabel As System.Windows.Forms.Label
        Dim TypeOfControlLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.DataRepeater1 = New Microsoft.VisualBasic.PowerPacks.DataRepeater()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TypeOfControlTextBox = New System.Windows.Forms.TextBox()
        Me.WpfColumnInfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ClassControlsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.ColumnNameTextBox = New System.Windows.Forms.TextBox()
        Me.SizeTextBox = New System.Windows.Forms.TextBox()
        Me.ColumnValueTextBox = New System.Windows.Forms.TextBox()
        ColumnNameLabel = New System.Windows.Forms.Label()
        ColumnValueLabel = New System.Windows.Forms.Label()
        SizeLabel = New System.Windows.Forms.Label()
        TypeOfControlLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Me.DataRepeater1.ItemTemplate.SuspendLayout()
        Me.DataRepeater1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.WpfColumnInfoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassControlsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColumnNameLabel
        '
        ColumnNameLabel.AutoSize = True
        ColumnNameLabel.Location = New System.Drawing.Point(11, 6)
        ColumnNameLabel.Name = "ColumnNameLabel"
        ColumnNameLabel.Size = New System.Drawing.Size(94, 16)
        ColumnNameLabel.TabIndex = 0
        ColumnNameLabel.Text = "Column Name:"
        '
        'ColumnValueLabel
        '
        ColumnValueLabel.AutoSize = True
        ColumnValueLabel.Location = New System.Drawing.Point(247, 11)
        ColumnValueLabel.Name = "ColumnValueLabel"
        ColumnValueLabel.Size = New System.Drawing.Size(92, 16)
        ColumnValueLabel.TabIndex = 2
        ColumnValueLabel.Text = "Column Value:"
        '
        'SizeLabel
        '
        SizeLabel.AutoSize = True
        SizeLabel.Location = New System.Drawing.Point(301, 42)
        SizeLabel.Name = "SizeLabel"
        SizeLabel.Size = New System.Drawing.Size(38, 16)
        SizeLabel.TabIndex = 4
        SizeLabel.Text = "Size:"
        '
        'TypeOfControlLabel
        '
        TypeOfControlLabel.AutoSize = True
        TypeOfControlLabel.Location = New System.Drawing.Point(4, 39)
        TypeOfControlLabel.Name = "TypeOfControlLabel"
        TypeOfControlLabel.Size = New System.Drawing.Size(101, 16)
        TypeOfControlLabel.TabIndex = 6
        TypeOfControlLabel.Text = "Type Of Control:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(0, 70)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(101, 16)
        Label1.TabIndex = 6
        Label1.Text = "Change Control:"
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(412, 427)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(108, 36)
        Me.ButtonOk.TabIndex = 0
        Me.ButtonOk.Text = "&Ok"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(5, 429)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(127, 33)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'DataRepeater1
        '
        Me.DataRepeater1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        'DataRepeater1.ItemTemplate
        '
        Me.DataRepeater1.ItemTemplate.Controls.Add(Me.Panel1)
        Me.DataRepeater1.ItemTemplate.Size = New System.Drawing.Size(515, 98)
        Me.DataRepeater1.Location = New System.Drawing.Point(5, 4)
        Me.DataRepeater1.Name = "DataRepeater1"
        Me.DataRepeater1.Size = New System.Drawing.Size(523, 417)
        Me.DataRepeater1.TabIndex = 2
        Me.DataRepeater1.Text = "DataRepeater1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel1.Controls.Add(Me.TypeOfControlTextBox)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.DeleteButton)
        Me.Panel1.Controls.Add(Me.ColumnNameTextBox)
        Me.Panel1.Controls.Add(Label1)
        Me.Panel1.Controls.Add(TypeOfControlLabel)
        Me.Panel1.Controls.Add(SizeLabel)
        Me.Panel1.Controls.Add(ColumnValueLabel)
        Me.Panel1.Controls.Add(Me.SizeTextBox)
        Me.Panel1.Controls.Add(ColumnNameLabel)
        Me.Panel1.Controls.Add(Me.ColumnValueTextBox)
        Me.Panel1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 93)
        Me.Panel1.TabIndex = 10
        '
        'TypeOfControlTextBox
        '
        Me.TypeOfControlTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "TypeOfControl", True))
        Me.TypeOfControlTextBox.Location = New System.Drawing.Point(111, 34)
        Me.TypeOfControlTextBox.Name = "TypeOfControlTextBox"
        Me.TypeOfControlTextBox.Size = New System.Drawing.Size(130, 22)
        Me.TypeOfControlTextBox.TabIndex = 10
        '
        'WpfColumnInfoBindingSource
        '
        Me.WpfColumnInfoBindingSource.DataSource = GetType(DBExtenderLib.wpfColumnInfo)
        '
        'ComboBox1
        '
        Me.ComboBox1.DataSource = Me.ClassControlsBindingSource
        Me.ComboBox1.DisplayMember = "ControlName"
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(111, 66)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(129, 24)
        Me.ComboBox1.TabIndex = 9
        Me.ComboBox1.ValueMember = "ControlName"
        '
        'ClassControlsBindingSource
        '
        Me.ClassControlsBindingSource.DataSource = GetType(MSAccessServer.ClassControls)
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(404, 67)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(79, 23)
        Me.DeleteButton.TabIndex = 8
        Me.DeleteButton.Text = "Delete"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'ColumnNameTextBox
        '
        Me.ColumnNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "ColumnName", True))
        Me.ColumnNameTextBox.Location = New System.Drawing.Point(111, 6)
        Me.ColumnNameTextBox.Name = "ColumnNameTextBox"
        Me.ColumnNameTextBox.Size = New System.Drawing.Size(130, 22)
        Me.ColumnNameTextBox.TabIndex = 1
        '
        'SizeTextBox
        '
        Me.SizeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "Size", True))
        Me.SizeTextBox.Location = New System.Drawing.Point(345, 39)
        Me.SizeTextBox.Name = "SizeTextBox"
        Me.SizeTextBox.Size = New System.Drawing.Size(138, 22)
        Me.SizeTextBox.TabIndex = 5
        '
        'ColumnValueTextBox
        '
        Me.ColumnValueTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "ColumnValue", True))
        Me.ColumnValueTextBox.Location = New System.Drawing.Point(345, 8)
        Me.ColumnValueTextBox.Name = "ColumnValueTextBox"
        Me.ColumnValueTextBox.Size = New System.Drawing.Size(138, 22)
        Me.ColumnValueTextBox.TabIndex = 3
        '
        'FormEditColumns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 463)
        Me.Controls.Add(Me.DataRepeater1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Name = "FormEditColumns"
        Me.Text = "Edit WPF Columns"
        Me.DataRepeater1.ItemTemplate.ResumeLayout(False)
        Me.DataRepeater1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.WpfColumnInfoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassControlsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents DataRepeater1 As Microsoft.VisualBasic.PowerPacks.DataRepeater
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ColumnNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WpfColumnInfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ColumnValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SizeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents ClassControlsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TypeOfControlTextBox As System.Windows.Forms.TextBox
End Class
