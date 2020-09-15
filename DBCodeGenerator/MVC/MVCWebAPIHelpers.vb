Imports System.Text
Imports DBExtenderLib

Public Class MVCWebAPIHelpers


    Public Shared Function ControllerGetList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" #Region " & DT.TableName.QT)
        sb.AppendLine("    <ResponseType(GetType(" & DT.TablePluralize & "ManagerVM))>")
        sb.AppendLine("    Public Function Get" & DT.TablePluralize & "() As " & DT.TablePluralize & "ManagerVM")
        sb.AppendLine("        Dim " & DT.TablePluralize.ToLower & " = Repo.Get" & DT.TablePluralize & "")
        sb.AppendLine("        Return " & DT.TablePluralize.ToLower)
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        Return sb.ToString
    End Function
    Public Shared Function ControllerGetItem(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region  " & (DT.TableName & " Get By Id ").QT)
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Function Get" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ") As IHttpActionResult")
        sb.AppendLine("        Dim Vm = Repo.Get" & DT.TableSingularize & "(id)")
        sb.AppendLine("        If IsNothing(Vm) Then")
        sb.AppendLine("            Return NotFound()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Return Ok(Vm)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        Return sb.ToString
    End Function

    Public Shared Function ControllerPost(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region " & (DT.TableName & " Post").QT)
        sb.AppendLine("    ' POST api/" & DT.TableSingularize & "Vm")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Function Post" & DT.TableSingularize & "(vm As " & DT.TableSingularize & "Vm) As IHttpActionResult")
        sb.AppendLine("        If Not ModelState.IsValid Then")
        sb.AppendLine("            Return BadRequest(ModelState)")
        sb.AppendLine("        End If")
        sb.AppendLine()
        sb.AppendLine("        Repo.Add(vm)")
        sb.AppendLine("        Repo.Save()")
        sb.AppendLine("        Return CreatedAtRoute(" & "DefaultApi".QT & ", New With {.id = vm." & DT.GetPrimaryKey.ColumnValue & "}, vm)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        Return sb.ToString
    End Function
    Public Shared Function ControllerPut(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region " & (DT.TableName & " Put").QT)
        sb.AppendLine("    ' PUT api/" & DT.TablePluralize & "/5")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize.LowerTheFistChar & "Vm))>")
        sb.AppendLine("    Public Function Put" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ", vm As " & DT.TableSingularize.LowerTheFistChar & "Vm) As IHttpActionResult")
        sb.AppendLine("        If Not ModelState.IsValid Then")
        sb.AppendLine("            Return BadRequest(ModelState)")
        sb.AppendLine("        End If")
        sb.AppendLine("        If Not id = vm." & DT.GetPrimaryKey.ColumnValue & " Then")
        sb.AppendLine("            Return BadRequest()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Try")
        sb.AppendLine("            Repo.Update(vm)")
        sb.AppendLine("            Repo.Save()")
        sb.AppendLine("       Catch ex As DbUpdateConcurrencyException")
        sb.AppendLine("           Return NotFound()")
        sb.AppendLine("       End Try")
        sb.AppendLine("       Return StatusCode(HttpStatusCode.NoContent)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        Return sb.ToString
    End Function
    Public Shared Function ControllerDelete(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region " & (DT.TableName & " Put").QT)
        sb.AppendLine("    '  DELETE api/" & DT.TablePluralize & "/5")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Function Delete" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ") As IHttpActionResult")
        sb.AppendLine("        Dim vm = Repo.Get" & DT.TableSingularize & "(id)")
        sb.AppendLine("        If vm Is Nothing Then")
        sb.AppendLine("            Return NotFound()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Repo.RemoveItem(vm)")
        sb.AppendLine("        Repo.Save()")
        sb.AppendLine("        Return Ok(vm)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
        Return sb.ToString
    End Function
    

End Class
