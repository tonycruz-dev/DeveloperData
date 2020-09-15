Imports DBExtenderLib
Imports System.Text

Public Class MVC5Controller

    Public Shared Function MVC5Template(db As DatabaseNameInfo, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("Public Class " & DT.TablePluralize & " Controller")
        sb.AppendLine("    Inherits ApiController")
        sb.AppendLine("    Private db As New " & db.DatabaseName & "Context")
        sb.AppendLine()
        sb.AppendLine(MVCHelpers.ControllerGetList(DT))
        sb.AppendLine(MVCHelpers.ControllerGetItem(DT))
        sb.AppendLine(MVCHelpers.ControllerPost(DT))
        sb.AppendLine(MVCHelpers.ControllerPut(DT))
        sb.AppendLine(MVCHelpers.ControllerDelete(DT))
        sb.AppendLine("End Class")
        Return sb.ToString
    End Function

    Public Shared Function MVC5WebAPITemplate(db As DatabaseNameInfo, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine()
        sb.AppendLine("Imports System.Net")
        sb.AppendLine("Imports System.Web.Http")
        sb.AppendLine("Imports System.Web.Http.Description")
        sb.AppendLine("Imports System.Data.Entity.Infrastructure")
        sb.AppendLine("Imports Newtonsoft.Json")
        sb.AppendLine("Imports Newtonsoft.Json.Serialization")
        sb.AppendLine("Imports System.IO")
        sb.AppendLine("Imports System.IO.Path")
        sb.AppendLine()
        sb.AppendLine("Public Class " & DT.TablePluralize & "ApiController")
        sb.AppendLine("    Inherits ApiController")
        sb.AppendLine("    Private Repo As I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm)")
        sb.AppendLine("    Public Sub New()")
        sb.AppendLine("        Repo = New " & DT.TablePluralize & "Repository")
        sb.AppendLine("    End Sub")
        sb.AppendLine("    Public Sub New(_Repo As I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine("        Repo = _Repo")
        sb.AppendLine("    End Sub")
       
        sb.AppendLine()
        sb.AppendLine(MVCWebAPIHelpers.ControllerGetList(DT))
        sb.AppendLine(MVCWebAPIHelpers.ControllerGetItem(DT))
        sb.AppendLine(MVCWebAPIHelpers.ControllerPost(DT))
        sb.AppendLine(MVCWebAPIHelpers.ControllerPut(DT))
        sb.AppendLine(MVCWebAPIHelpers.ControllerDelete(DT))

        sb.AppendLine("    Protected Overrides Sub Dispose(disposing As Boolean)")
        sb.AppendLine("        MyBase.Dispose(disposing)")
        sb.AppendLine("    End Sub")
        sb.AppendLine("")
        sb.AppendLine("End Class")

        Return sb.ToString
    End Function

End Class
