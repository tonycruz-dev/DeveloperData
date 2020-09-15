Imports System.IO
Imports DBExtenderLib
Imports System.Text
Imports DBCodeGenerator

Public Class FormSaveLocation
    Private SelectedPath As String
    Private Const ManageFile As String = "Manage"
    Private Const ViewModel As String = "ViewModel"
    Private Const CreateTemplate As String = "Create.temp.html"
    Private Const DeleteTemplate As String = "Delete.temp.html"
    Private Const DetailsTemplate As String = "Details.temp.html"
    Private Const EditTemplate As String = "Edit.temp.html"
    Private Const ListTemplate As String = ".temp.html"
    Private ObjTable As TableNameInfo

    'ProductsDetails.temp.html

    Public Sub New(_ObjTable As TableNameInfo)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ObjTable = _ObjTable
    End Sub

    Private Sub FormSaveLocation_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' The combo box will convert the enumerated values
        ' into their string representations for display:
        StartLocationComboBox.DataSource = _
         System.Enum.GetValues(GetType(Environment.SpecialFolder))
    End Sub

    Private Sub TestDialogButton_Click(sender As System.Object, e As System.EventArgs) Handles TestDialogButton.Click
        fbd.Description = DescriptionTextBox.Text
        fbd.RootFolder = _
         CType(StartLocationComboBox.SelectedValue,  _
         Environment.SpecialFolder)
        ' fbd.ShowNewFolderButton = ShowNewFolderCheckBox.Checked
        'fbd.SelectedPath = ResultsLabel.Text

        If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If String.IsNullOrEmpty(fbd.SelectedPath) Then
                ResultsLabel.Text = "<<Non-file system selection>>"
            Else
                SelectedPath = fbd.SelectedPath
                Me.DescriptionTextBox.Text = SelectedPath
                ResultsLabel.Text = fbd.SelectedPath
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Not Directory.Exists(SelectedPath & "\Templates\" & ObjTable.TableValue) Then
            Directory.CreateDirectory(SelectedPath & "\Templates\" & ObjTable.TableValue)
            createManage()
            CreateNewTemplate()
            createDeleteTemplate()
            CreateDetailsTemplate()
            createEditTemplate()
            createListTemplate()
        Else
            ' Directory.CreateDirectory(SelectedPath & "\Templates\" & ObjTable.TableValue)
            createManage()
            CreateNewTemplate()
            createDeleteTemplate()
            CreateDetailsTemplate()
            createEditTemplate()
            createListTemplate()
        End If
        If Not Directory.Exists(SelectedPath & "\Scripts\" & ObjTable.TableValue) Then
            Directory.CreateDirectory(SelectedPath & "\Scripts\" & ObjTable.TableValue)
            createJavaScripTemplate()
        End If
        If Directory.Exists(SelectedPath & "\ViewModel") Then
            createViewModel()
        End If
        '
        'Directory.CreateDirectory(CreateDirectoryTextBox.Text)

    End Sub
    Private Sub createManage()
        'CreateVMClass

        Dim DGTemplate As String = "\Templates\ASP\Maintemplate.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)


        Dim _MAINPAGELIST As String = JSManager.GetHtmlTableList(ObjTable)
        SB.Replace("[TABLE]", ObjTable.TableValue)
        SB.Replace("[MAINPAGELIST]", _MAINPAGELIST)
        sr.Close()
        SaveList(SB.ToString, SelectedPath & "\" & ObjTable.TableValue & ".htm")


    End Sub
    Private Sub createViewModel()
        Dim results = MVCJqueryHelper.CreateVMClass(ObjTable)
        If Not IO.File.Exists(SelectedPath & "\ViewModel\" & ObjTable.TableValue & "VM.vb") Then
            SaveList(results, SelectedPath & "\ViewModel\" & ObjTable.TableValue & "VM.vb")
        End If

        'ViewModel
    End Sub
    Private Sub CreateNewTemplate()

    End Sub
    Private Sub createDeleteTemplate()
        If Not IO.File.Exists(SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & "Delete.temp.html") Then
            Dim results = JSManager.TableDetailsTemplate(ObjTable)
            SaveList(results, SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & "Delete.temp.html")
        End If
    End Sub
    Private Sub CreateDetailsTemplate()
        If Not IO.File.Exists(SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & "Details.temp.html") Then
            Dim results = JSManager.TableDetailsTemplate(ObjTable)
            SaveList(results, SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & "Details.temp.html")
        End If
    End Sub
    Private Sub createEditTemplate()
        If Not IO.File.Exists(SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & "Edit.temp.html") Then
            Dim results = JSManager.TableAddOrEditTemplate(ObjTable)
            SaveList(results, SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & "Edit.temp.html")
        End If
    End Sub
    Private Sub createListTemplate()
        If Not IO.File.Exists(SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & ".temp.html") Then
            Dim results = JSManager.TableListTemplate(ObjTable)
            SaveList(results, SelectedPath & "\Templates\" & ObjTable.TableValue & "\" & ObjTable.TableValue & ".temp.html")
        End If
    End Sub

    Private Sub createJavaScripTemplate()
        If Not IO.File.Exists(SelectedPath & "\Scripts\" & ObjTable.TableValue & "\js" & ObjTable.TableValue & ".js") Then
            Dim results = JavaScriptTable.JQueryTableTemplate(ObjTable)
            SaveList(results, SelectedPath & "\Scripts\" & ObjTable.TableValue & "\js" & ObjTable.TableValue & ".js")
        End If
    End Sub
    Private Sub SaveList(txt As String, ByVal fileName As String)

        Try
            Using fs As New FileStream(fileName, FileMode.Create)
                Using sw As New StreamWriter(fs)
                    sw.WriteLine(txt)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "File Error")
        End Try
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        createJavaScripTemplate()
    End Sub
End Class