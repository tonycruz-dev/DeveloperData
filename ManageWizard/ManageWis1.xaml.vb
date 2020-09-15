Imports DBExtenderLib

Public Class ManageWis1
    Private data As DatabaseNameInfo
    Const DataStep1 As String = "Address"
    Const DataStep2 As String = "Tenants"
    Const DataStep3 As String = "Landlord"
    Const DataStep4 As String = "Suppliers"
    Const Step4 As String = "Final"
    Private CurrentStep As String = ""
    Private StartupPath As String = ""
    Public Property VM As New SimpleWizardVM
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Property SaveFilePath As String

    Public Property SetData As DatabaseNameInfo
        Get
            Return data
        End Get
        Set(value As DatabaseNameInfo)
            Me.data = value
            VM.Database = value
            ' Me.Step1.SetData = value
        End Set
    End Property
      
    Private Sub NextStep_Click(sender As Object, e As RoutedEventArgs) Handles NextStep.Click
        Select Case CurrentStep
            Case DataStep1
                DisplayWizard.Content = New UCColumnDisplay(VM) ' TablesStep1Control(VM)
                CurrentStep = DataStep2
                FinishStep.Visibility = Windows.Visibility.Hidden
                Exit Sub
            Case DataStep2
                DisplayWizard.Content = New UCColumnShow(VM) ' TablesStep1Control(VM)
                CurrentStep = DataStep3
                FinishStep.Visibility = Windows.Visibility.Hidden
                Exit Sub
            Case DataStep3
                DisplayWizard.Content = New UCColumnKey(VM) ' TablesStep1Control(VM)
                CurrentStep = DataStep4
                FinishStep.Visibility = Windows.Visibility.Hidden
                Exit Sub
            Case DataStep4
                DisplayWizard.Content = New SaveFileLocationUserControl(VM)
                CurrentStep = DataStep3
                FinishStep.Visibility = Windows.Visibility.Visible
                Exit Sub
        End Select
    End Sub

    Private Sub ManageWis1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DisplayWizard.Content = New UCColumnDisplay(VM)
        CurrentStep = DataStep2
        FinishStep.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub FinishStep_Click(sender As Object, e As RoutedEventArgs) Handles FinishStep.Click
        Me.DialogResult = True

    End Sub
End Class
