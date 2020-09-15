<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSelectViews
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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonJson = New System.Windows.Forms.RadioButton()
        Me.RadioButtonXmlClass = New System.Windows.Forms.RadioButton()
        Me.RadioButtonNon = New System.Windows.Forms.RadioButton()
        Me.CheckedListBoxColumns = New System.Windows.Forms.CheckedListBox()
        Me.Panel1.SuspendLayout()
        CType(Me.TableNameInfoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ComboBoxTables)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(394, 57)
        Me.Panel1.TabIndex = 1
        '
        'ComboBoxTables
        '
        Me.ComboBoxTables.DataSource = Me.TableNameInfoBindingSource
        Me.ComboBoxTables.DisplayMember = "Name"
        Me.ComboBoxTables.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTables.FormattingEnabled = True
        Me.ComboBoxTables.Location = New System.Drawing.Point(115, 10)
        Me.ComboBoxTables.Name = "ComboBoxTables"
        Me.ComboBoxTables.Size = New System.Drawing.Size(267, 29)
        Me.ComboBoxTables.TabIndex = 2
        Me.ComboBoxTables.ValueMember = "ViewValue"
        '
        'TableNameInfoBindingSource
        '
        Me.TableNameInfoBindingSource.DataSource = GetType(DBExtenderLib.ViewNameInfo)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(5, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 21)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Veiw:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.ButtonOk)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.RadioButton4)
        Me.Panel2.Controls.Add(Me.RadioButtonJson)
        Me.Panel2.Controls.Add(Me.RadioButtonXmlClass)
        Me.Panel2.Controls.Add(Me.RadioButtonNon)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 384)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(394, 123)
        Me.Panel2.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(8, 77)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 34)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ButtonOk
        '
        Me.ButtonOk.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOk.Location = New System.Drawing.Point(267, 77)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(115, 34)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "OK"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(141, 38)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(63, 29)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Text = "20"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Number of rows"
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton4.Location = New System.Drawing.Point(288, 6)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(97, 25)
        Me.RadioButton4.TabIndex = 0
        Me.RadioButton4.Text = "Collection"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButtonJson
        '
        Me.RadioButtonJson.AutoSize = True
        Me.RadioButtonJson.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtonJson.Location = New System.Drawing.Point(215, 6)
        Me.RadioButtonJson.Name = "RadioButtonJson"
        Me.RadioButtonJson.Size = New System.Drawing.Size(59, 25)
        Me.RadioButtonJson.TabIndex = 0
        Me.RadioButtonJson.Text = "Json"
        Me.RadioButtonJson.UseVisualStyleBackColor = True
        '
        'RadioButtonXmlClass
        '
        Me.RadioButtonXmlClass.AutoSize = True
        Me.RadioButtonXmlClass.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtonXmlClass.Location = New System.Drawing.Point(114, 6)
        Me.RadioButtonXmlClass.Name = "RadioButtonXmlClass"
        Me.RadioButtonXmlClass.Size = New System.Drawing.Size(95, 25)
        Me.RadioButtonXmlClass.TabIndex = 0
        Me.RadioButtonXmlClass.Text = "Xml Class"
        Me.RadioButtonXmlClass.UseVisualStyleBackColor = True
        '
        'RadioButtonNon
        '
        Me.RadioButtonNon.AutoSize = True
        Me.RadioButtonNon.Checked = True
        Me.RadioButtonNon.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtonNon.Location = New System.Drawing.Point(8, 6)
        Me.RadioButtonNon.Name = "RadioButtonNon"
        Me.RadioButtonNon.Size = New System.Drawing.Size(98, 25)
        Me.RadioButtonNon.TabIndex = 0
        Me.RadioButtonNon.TabStop = True
        Me.RadioButtonNon.Text = "Class only"
        Me.RadioButtonNon.UseVisualStyleBackColor = True
        '
        'CheckedListBoxColumns
        '
        Me.CheckedListBoxColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBoxColumns.Font = New System.Drawing.Font("Segoe WP", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBoxColumns.FormattingEnabled = True
        Me.CheckedListBoxColumns.Location = New System.Drawing.Point(0, 57)
        Me.CheckedListBoxColumns.Name = "CheckedListBoxColumns"
        Me.CheckedListBoxColumns.Size = New System.Drawing.Size(394, 327)
        Me.CheckedListBoxColumns.TabIndex = 3
        '
        'FormSelectViews
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 507)
        Me.Controls.Add(Me.CheckedListBoxColumns)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FormSelectViews"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Tables"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.TableNameInfoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ComboBoxTables As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CheckedListBoxColumns As System.Windows.Forms.CheckedListBox
    Friend WithEvents TableNameInfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonJson As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonXmlClass As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonNon As System.Windows.Forms.RadioButton
End Class
