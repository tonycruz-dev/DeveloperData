Imports DBExtenderLib

Public Class FormEditColumn
    Private cnttype As New List(Of ClassControls)
    Private _ColumnToedit As wpfColumnInfo
    Public Sub New(ByVal columnToEdit As wpfColumnInfo)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cnttype.Add(New ClassControls With {.ControlName = "CheckBox"})
        cnttype.Add(New ClassControls With {.ControlName = "ComboBox"})
        cnttype.Add(New ClassControls With {.ControlName = "DatePicker"})
        cnttype.Add(New ClassControls With {.ControlName = "Image"})
        cnttype.Add(New ClassControls With {.ControlName = "Label"})
        cnttype.Add(New ClassControls With {.ControlName = "TextBlock"})
        cnttype.Add(New ClassControls With {.ControlName = "TextBox"})
        _ColumnToedit = columnToEdit
        ClassControlsBindingSource.DataSource = cnttype
        WpfColumnInfoBindingSource.DataSource = _ColumnToedit

    End Sub
End Class