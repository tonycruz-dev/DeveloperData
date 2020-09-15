Imports DBExtenderLib

Public Class FormEditColumns
    Private _ListWpfColumns As List(Of wpfColumnInfo)
    Private _DBList As DatabaseNameInfo
    Private _wpfcnt As WPFDataContext
    Private cnttype As New List(Of ClassControls)
    Public Sub New(ByVal DBList_ As DatabaseNameInfo, ByVal wpfcnt_ As WPFDataContext, ByVal ListWpfColumns_ As List(Of wpfColumnInfo))

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



        _DBList = DBList_
        _ListWpfColumns = ListWpfColumns_
        _wpfcnt = wpfcnt_
        WpfColumnInfoBindingSource.DataSource = _ListWpfColumns
        ClassControlsBindingSource.DataSource = cnttype

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

    End Sub

    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        _ListWpfColumns.RemoveAt(DataRepeater1.CurrentItemIndex)
        WpfColumnInfoBindingSource.DataSource = (From col In _ListWpfColumns).ToList
    End Sub
End Class