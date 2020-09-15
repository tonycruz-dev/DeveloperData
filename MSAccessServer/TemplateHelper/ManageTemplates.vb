Imports DBExtenderLib
Imports System.IO
Imports System.Text

Public Class ManageTemplates
    Enum TemplateData
        simpleVM
        SimpleDataVM
        SimpleDataMasterDetailsVM
    End Enum

    Public Shared Sub CreateMasterDetailsVM(SaveLocation As String, tb As TableNameInfo, temple As TemplateData)
        Dim contextFileName As String = ""


        Select Case temple
            Case TemplateData.simpleVM
                contextFileName = Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
            Case TemplateData.SimpleDataVM
                contextFileName = Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
            Case TemplateData.SimpleDataMasterDetailsVM
                contextFileName = Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
        End Select
        Dim fsClass As New FileStream(contextFileName, FileMode.Open)
        Dim srKockoutMasterDetails As String = ""
        Using srclass As New StreamReader(fsClass)
            srKockoutMasterDetails = srclass.ReadToEnd
        End Using
        fsClass.Close()

    End Sub
End Class
