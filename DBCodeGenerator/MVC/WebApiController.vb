﻿Imports System.Text
Imports DBExtenderLib

Public Class WebApiController
    Public Shared Function MVCTableEditTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("Imports System.Net")
        sb.AppendLine("Imports System.Net.Http")
        sb.AppendLine("Imports System.Web.Http")
        sb.AppendLine("Imports System.Web.Http.Description")
        sb.AppendLine("")
        sb.AppendLine("Public Class " & DT.TablePluralize & "Controller")
        sb.AppendLine("    Inherits ApiController")
        sb.AppendLine("")
        sb.AppendLine("    Private Repo As I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm)")
        sb.AppendLine("")
        sb.AppendLine("    Public Sub New(_rep As I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine("        Repo = _rep")
        sb.AppendLine("    End Sub")
        sb.AppendLine("    ' GET: api/" & DT.TablePluralize & "")
        sb.AppendLine("#Region " & (DT.TablePluralize & " Get/list".QT))
        sb.AppendLine("    Public Async Function Get" & DT.TablePluralize & "() As Threading.Tasks.Task(Of " & DT.TablePluralize & "ManagerVM)")
        sb.AppendLine("        Return Await Repo.Get" & DT.TablePluralize)
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine("#Region " & (DT.TablePluralize & " Get/{id}".QT))
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Async Function Get" & DT.TableSingularize & "(id As String) As Threading.Tasks.Task(Of IHttpActionResult)")
        sb.AppendLine("        Dim result = Await Repo.FindItem(id)")
        sb.AppendLine("        If IsNothing(result) Then")
        sb.AppendLine("            Return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound," & "No Electricity Supplier Found".QT & "))")
        sb.AppendLine("        End If")
        sb.AppendLine("")
        sb.AppendLine("        Return Ok(result)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine("")
        sb.AppendLine("#Region " & (DT.TablePluralize & " Post".QT))
        sb.AppendLine("    ' POST api/" & DT.TableSingularize & "Vm")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Async Function Post" & DT.TableSingularize & "(<FromBody()> vm As " & DT.TableSingularize & "Vm) As Threading.Tasks.Task(Of IHttpActionResult)")
        sb.AppendLine("        If Not ModelState.IsValid Then")
        sb.AppendLine("            Return BadRequest(ModelState)")
        sb.AppendLine("        End If")
        sb.AppendLine("        Await Repo.Add(vm)")
        sb.AppendLine("")
        sb.AppendLine("        Return CreatedAtRoute( " & "DefaultApi".QT & ", New With {.id = vm." & DT.GetPrimaryKey.ColumnValue & "}, vm)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine("")
        sb.AppendLine("#Region " & (DT.TablePluralize & " Update".QT))
        sb.AppendLine("    ' PUT api/" & DT.TablePluralize & "/5")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Async Function Put" & DT.TableSingularize & "(id As Integer, vm As " & DT.TableSingularize & "Vm) As Threading.Tasks.Task(Of IHttpActionResult)")
        sb.AppendLine("        If Not ModelState.IsValid Then")
        sb.AppendLine("            Return BadRequest(ModelState)")
        sb.AppendLine("        End If")
        sb.AppendLine("        If Not id = vm." & DT.GetPrimaryKey.ColumnValue & " Then")
        sb.AppendLine("            Return BadRequest()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Await Repo.Update(vm)")
        sb.AppendLine("        Return Ok(vm)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine("")
        sb.AppendLine("#Region" & (DT.TablePluralize & " Delete".QT))
        sb.AppendLine("    '  DELETE api/" & DT.TablePluralize & "/5")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Async Function Delete" & DT.TableSingularize & "(id As Integer) As Threading.Tasks.Task(Of IHttpActionResult)")
        sb.AppendLine("        Dim result = Await Repo.RemoveItem(id)")
        sb.AppendLine("        If IsNothing(result) Then")
        sb.AppendLine("            Return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound," & "No Electricity Supplier Found".QT & "))")
        sb.AppendLine("        End If")
        sb.AppendLine("        Return Ok(result)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine("End Class")




        Return sb.ToString
    End Function
    Public Share
End Class
