<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditColumn
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
        Dim IsRequaredLabel As System.Windows.Forms.Label
        Dim SizeLabel As System.Windows.Forms.Label
        Dim TypeOfControlLabel As System.Windows.Forms.Label
        Me.WpfColumnInfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ColumnNameTextBox = New System.Windows.Forms.TextBox()
        Me.ColumnValueTextBox = New System.Windows.Forms.TextBox()
        Me.IsRequaredCheckBox = New System.Windows.Forms.CheckBox()
        Me.SizeTextBox = New System.Windows.Forms.TextBox()
        Me.TypeOfControlComboBox = New System.Windows.Forms.ComboBox()
        Me.ClassControlsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        ColumnNameLabel = New System.Windows.Forms.Label()
        ColumnValueLabel = New System.Windows.Forms.Label()
        IsRequaredLabel = New System.Windows.Forms.Label()
        SizeLabel = New System.Windows.Forms.Label()
        TypeOfControlLabel = New System.Windows.Forms.Label()
        CType(Me.WpfColumnInfoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassControlsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColumnNameLabel
        '
        ColumnNameLabel.AutoSize = True
        ColumnNameLabel.Location = New System.Drawing.Point(36, 10)
        ColumnNameLabel.Name = "ColumnNameLabel"
        ColumnNameLabel.Size = New System.Drawing.Size(76, 13)
        ColumnNameLabel.TabIndex = 1
        ColumnNameLabel.Text = "Column Name:"
        '
        'ColumnValueLabel
        '
        ColumnValueLabel.AutoSize = True
        ColumnValueLabel.Location = New System.Drawing.Point(37, 36)
        ColumnValueLabel.Name = "ColumnValueLabel"
        ColumnValueLabel.Size = New System.Drawing.Size(75, 13)
        ColumnValueLabel.TabIndex = 3
        ColumnValueLabel.Text = "Column Value:"
        '
        'IsRequaredLabel
        '
        IsRequaredLabel.AutoSize = True
        IsRequaredLabel.Location = New System.Drawing.Point(41, 67)
        IsRequaredLabel.Name = "IsRequaredLabel"
        IsRequaredLabel.Size = New System.Drawing.Size(71, 13)
        IsRequaredLabel.TabIndex = 7
        IsRequaredLabel.Text = " Is Requared:"
        '
        'SizeLabel
        '
        SizeLabel.AutoSize = True
        SizeLabel.Location = New System.Drawing.Point(82, 95)
        SizeLabel.Name = "SizeLabel"
        SizeLabel.Size = New System.Drawing.Size(30, 13)
        SizeLabel.TabIndex = 9
        SizeLabel.Text = "Size:"
        '
        'TypeOfControlLabel
        '
        TypeOfControlLabel.AutoSize = True
        TypeOfControlLabel.Location = New System.Drawing.Point(28, 121)
        TypeOfControlLabel.Name = "TypeOfControlLabel"
        TypeOfControlLabel.Size = New System.Drawing.Size(84, 13)
        TypeOfControlLabel.TabIndex = 11
        TypeOfControlLabel.Text = "Type Of Control:"
        '
        'WpfColumnInfoBindingSource
        '
        Me.WpfColumnInfoBindingSource.DataSource = GetType(DBExtenderLib.wpfColumnInfo)
        '
        'ColumnNameTextBox
        '
        Me.ColumnNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "ColumnName", True))
        Me.ColumnNameTextBox.Location = New System.Drawing.Point(118, 7)
        Me.ColumnNameTextBox.Name = "ColumnNameTextBox"
        Me.ColumnNameTextBox.Size = New System.Drawing.Size(222, 20)
        Me.ColumnNameTextBox.TabIndex = 2
        '
        'ColumnValueTextBox
        '
        Me.ColumnValueTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "ColumnValue", True))
        Me.ColumnValueTextBox.Location = New System.Drawing.Point(118, 33)
        Me.ColumnValueTextBox.Name = "ColumnValueTextBox"
        Me.ColumnValueTextBox.Size = New System.Drawing.Size(222, 20)
        Me.ColumnValueTextBox.TabIndex = 4
        '
        'IsRequaredCheckBox
        '
        Me.IsRequaredCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.WpfColumnInfoBindingSource, "IsRequared", True))
        Me.IsRequaredCheckBox.Location = New System.Drawing.Point(118, 62)
        Me.IsRequaredCheckBox.Name = "IsRequaredCheckBox"
        Me.IsRequaredCheckBox.Size = New System.Drawing.Size(222, 24)
        Me.IsRequaredCheckBox.TabIndex = 8
        Me.IsRequaredCheckBox.UseVisualStyleBackColor = True
        '
        'SizeTextBox
        '
        Me.SizeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "Size", True))
        Me.SizeTextBox.Location = New System.Drawing.Point(118, 92)
        Me.SizeTextBox.Name = "SizeTextBox"
        Me.SizeTextBox.Size = New System.Drawing.Size(222, 20)
        Me.SizeTextBox.TabIndex = 10
        '
        'TypeOfControlComboBox
        '
        Me.TypeOfControlComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WpfColumnInfoBindingSource, "TypeOfControl", True))
        Me.TypeOfControlComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.WpfColumnInfoBindingSource, "TypeOfControl", True))
        Me.TypeOfControlComboBox.DataSource = Me.ClassControlsBindingSource
        Me.TypeOfControlComboBox.DisplayMember = "ControlName"
        Me.TypeOfControlComboBox.FormattingEnabled = True
        Me.TypeOfControlComboBox.Location = New System.Drawing.Point(118, 118)
        Me.TypeOfControlComboBox.Name = "TypeOfControlComboBox"
        Me.TypeOfControlComboBox.Size = New System.Drawing.Size(222, 21)
        Me.TypeOfControlComboBox.TabIndex = 12
        Me.TypeOfControlComboBox.ValueMember = "ControlName"
        '
        'ClassControlsBindingSource
        '
        Me.ClassControlsBindingSource.DataSource = GetType(MSAccessServer.ClassControls)
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(21, 223)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 36)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(240, 220)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 39)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Ok"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FormEditColumn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 271)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(ColumnNameLabel)
        Me.Controls.Add(Me.ColumnNameTextBox)
        Me.Controls.Add(ColumnValueLabel)
        Me.Controls.Add(Me.ColumnValueTextBox)
        Me.Controls.Add(IsRequaredLabel)
        Me.Controls.Add(Me.IsRequaredCheckBox)
        Me.Controls.Add(SizeLabel)
        Me.Controls.Add(Me.SizeTextBox)
        Me.Controls.Add(TypeOfControlLabel)
        Me.Controls.Add(Me.TypeOfControlComboBox)
        Me.Name = "FormEditColumn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Column"
        CType(Me.WpfColumnInfoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassControlsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WpfColumnInfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ColumnNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ColumnValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IsRequaredCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SizeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TypeOfControlComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ClassControlsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
