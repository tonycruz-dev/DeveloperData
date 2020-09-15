Imports System.IO
Imports DBExtenderLib
Imports System.Text
Imports ManageWizard
Imports System.Runtime.CompilerServices

Public Class DurandalHelper

    Public Shared Function CreateDBFromTemplate(DT As TableNameInfo, ByVal tmpLocal As String, ByVal saveLocal As String) As String

        Dim ft = GetTemplateFile(tmpLocal)
        Dim SB As New StringBuilder(ft)
        SB.Replace("[$tableName]", DT.TableValue)

        Return SB.ToString


    End Function
    Public Shared Function GetTemplateFile(filePath As String) As String
        Dim contextFileName = filePath ' Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
        Dim fs As New FileStream(contextFileName, FileMode.Open)
        Dim content As String = ""
        Using sr As New StreamReader(fs)
            content = sr.ReadToEnd
        End Using
        fs.Close()
        Return content
    End Function

    Public Shared Function SaveLocation(fileName As String, Data As String, location As String) As Boolean
        If My.Computer.FileSystem.DirectoryExists(location) Then
            Dim saveFile = location & fileName.ToLower
            My.Computer.FileSystem.WriteAllText(saveFile, Data, False)
        Else
            My.Computer.FileSystem.CreateDirectory(location)
            Dim saveFile = location & fileName.ToLower
            My.Computer.FileSystem.WriteAllText(saveFile, Data, False)
        End If
        Return True
    End Function
    Public Shared Function GetDisplayColumnTitles(DT As TableSelected) As String
        Dim sb As New StringBuilder()
        For Each col In DT.Columns
            sb.AppendLine("       <th>" & col.DisplayName & "</th>")
        Next
        Return sb.ToString
    End Function
    Public Shared Function GetTableDisplayColumnList(DT As TableSelected) As String
        Dim sb As New StringBuilder()
        For Each col In DT.Columns
            sb.AppendLine("    <td data-bind=" & ("text:" & col.ColumnName).QT & "></td>")
        Next
        Return sb.ToString
    End Function
    '
End Class
Module ModuleHelper
    <Extension()>
    Public Function QT(Value As String) As String
        Return """" & Value & """"
    End Function
End Module