<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSqlInsertGenerator
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ComboBoxTables = New System.Windows.Forms.ComboBox()
        Me.TableNameInfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBoxWhere = New System.Windows.Forms.TextBox()
        Me.CheckedListBoxColumns = New System.Windows.Forms.CheckedListBox()
        Me.Panel1.SuspendLayout
        CType(Me.TableNameInfoBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel2.SuspendLayout
        Me.SuspendLayout
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(249,Byte),Integer))
        Me.Panel1.Controls.Add(Me.ComboBoxTables)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(727, 87)
        Me.Panel1.TabIndex = 2
        '
        'ComboBoxTables
        '
        Me.ComboBoxTables.DataSource = Me.TableNameInfoBindingSource
        Me.ComboBoxTables.DisplayMember = "TableName"
        Me.ComboBoxTables.Font = New System.Drawing.Font("Segoe WP", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ComboBoxTables.FormattingEnabled = true
        Me.ComboBoxTables.Location = New System.Drawing.Point(108, 12)
        Me.ComboBoxTables.Name = "ComboBoxTables"
        Me.ComboBoxTables.Size = New System.Drawing.Size(453, 29)
        Me.ComboBoxTables.TabIndex = 3
        Me.ComboBoxTables.ValueMember = "TableValue"
        '
        'TableNameInfoBindingSource
        '
        Me.TableNameInfoBindingSource.DataSource = GetType(DBExtenderLib.ViewNameInfo)
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Segoe WP", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 21)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Table:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(249,Byte),Integer))
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.ButtonOk)
        Me.Panel2.Controls.Add(Me.TextBoxWhere)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 474)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(727, 123)
        Me.Panel2.TabIndex = 3
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = true
        Me.CheckBox1.Location = New System.Drawing.Point(522, 22)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(121, 22)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "Include Where"
        Me.CheckBox1.UseVisualStyleBackColor = true
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Segoe WP", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button2.Location = New System.Drawing.Point(8, 77)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 34)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = true
        '
        'ButtonOk
        '
        Me.ButtonOk.Font = New System.Drawing.Font("Segoe WP", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonOk.Location = New System.Drawing.Point(600, 77)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(115, 34)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "OK"
        Me.ButtonOk.UseVisualStyleBackColor = true
        '
        'TextBoxWhere
        '
        Me.TextBoxWhere.Font = New System.Drawing.Font("Segoe WP", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBoxWhere.Location = New System.Drawing.Point(29, 18)
        Me.TextBoxWhere.Name = "TextBoxWhere"
        Me.TextBoxWhere.Size = New System.Drawing.Size(418, 29)
        Me.TextBoxWhere.TabIndex = 2
        Me.TextBoxWhere.Text = "Where "
        '
        'CheckedListBoxColumns
        '
        Me.CheckedListBoxColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBoxColumns.Font = New System.Drawing.Font("Segoe WP", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckedListBoxColumns.FormattingEnabled = true
        Me.CheckedListBoxColumns.Location = New System.Drawing.Point(0, 87)
        Me.CheckedListBoxColumns.Name = "CheckedListBoxColumns"
        Me.CheckedListBoxColumns.Size = New System.Drawing.Size(727, 387)
        Me.CheckedListBoxColumns.TabIndex = 4
        '
        'FormSqlInsertGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9!, 18!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(727, 597)
        Me.Controls.Add(Me.CheckedListBoxColumns)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormSqlInsertGenerator"
        Me.Text = "SQL Insert Generator"
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        CType(Me.TableNameInfoBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel2.ResumeLayout(false)
        Me.Panel2.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxWhere As System.Windows.Forms.TextBox
    Friend WithEvents CheckedListBoxColumns As System.Windows.Forms.CheckedListBox
    Friend WithEvents TableNameInfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ComboBoxTables As System.Windows.Forms.ComboBox
End Class
