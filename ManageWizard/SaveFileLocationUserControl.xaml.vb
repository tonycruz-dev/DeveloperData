Imports System.Windows.Forms

Public Class SaveFileLocationUserControl
    Protected Friend dlg As New FolderBrowserDialog
    Public Property VM As SimpleWizardVM
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(_VM As SimpleWizardVM)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        VM = _VM
    End Sub
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        dlg.Description = "select Forder"
        dlg.ShowNewFolderButton = True
        Dim result = dlg.ShowDialog()
        textboxLocation.Text = dlg.SelectedPath
        VM.SaveLocation = dlg.SelectedPath
    End Sub


    Private Sub textboxLocation_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textboxLocation.TextChanged
        VM.SaveLocation = textboxLocation.Text
    End Sub
End Class
