Imports System.Text
Imports DBExtenderLib

Public Class MVCHelpers

    Public Shared Function ControllerGetList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region " & DT.TableName.QT)
        sb.AppendLine(" Public Function Get" & DT.TablePluralize & "() As " & DT.TablePluralize & "ManagerVM")
        sb.AppendLine("      Dim results As New " & DT.TablePluralize & "ManagerVM")
        sb.AppendLine("        results." & DT.TablePluralize.LowerTheFistChar & " = (From " & Left(DT.TableValue, 3) & " In db." & DT.TablePluralize & " Order By " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " Select New " & DT.TableSingularize.LowerTheFistChar & "Vm With { _")
        Dim sbselect As New StringBuilder
        For Each col In DT.ListColumn
            sbselect.AppendLine("            ." & col.ColumnValue.LowerTheFistChar & "= " & Left(DT.TableValue, 3) & "." & col.ColumnValue & ",")
        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList()"
        sb.AppendLine(StrSelect)
        sb.AppendLine("            Return results")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function ControllerGetItem(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region " & (DT.TableName & " Get By Id ").QT)
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Function Get" & DT.TableSingularize & "(id As String) As IHttpActionResult")
        sb.AppendLine("        Dim result As " & DT.TableSingularize & " = db." & DT.TablePluralize & ".Where(Function(_" & Left(DT.TableValue, 3) & ") _" & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = id).SingleOrDefault()")
        sb.AppendLine()
        sb.AppendLine("         If IsNothing(result) Then")
        sb.AppendLine("             Return NotFound()")
        sb.AppendLine("         End If")
        sb.AppendLine("        Dim " & Left(DT.TableValue, 3) & "VM = New " & DT.TableSingularize & "Vm() With {")
        Dim sbselect As New StringBuilder
        For Each col In DT.ListColumn
            sbselect.AppendLine("            ." & col.ColumnValue.LowerTheFistChar & "= result." & col.ColumnValue & ",")
        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }"
        sb.AppendLine(StrSelect)
        sb.AppendLine("             Return Ok(" & Left(DT.TableValue, 3) & "VM)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
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
        sb.AppendLine("        Dim " & Left(DT.TableValue, 3) & " As New " & DT.TableSingularize & "()")
        For Each col In DT.ListColumn
            sb.AppendLine("        " & Left(DT.TableValue, 3) & "." & col.ColumnValue & " = vm." & col.ColumnValue.LowerTheFistChar)
        Next
        sb.AppendLine("        db." & DT.TablePluralize & ".InsertOnSubmit(" & Left(DT.TableValue, 3) & ")")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine("        vm." & DT.GetPrimaryKey.ColumnValue.LowerTheFistChar & " = " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue)
        sb.AppendLine("        Return CreatedAtRoute(" & "DefaultApi".QT & ", New With {.id = " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & "}, vm)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
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
        sb.AppendLine("        Dim result As " & DT.TableSingularize & " = db." & DT.TablePluralize & ".Where(Function(_" & Left(DT.TableValue, 3) & ") _" & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = id).SingleOrDefault()")

        For Each col In DT.ListColumn
            sb.AppendLine("        result." & col.ColumnValue & " = vm." & col.ColumnValue.LowerTheFistChar)
        Next
        sb.AppendLine("       Try")
        sb.AppendLine("           db.SubmitChanges()")
        sb.AppendLine("       Catch ex As DbUpdateConcurrencyException")
        sb.AppendLine("           Return NotFound()")
        sb.AppendLine("       End Try")
        sb.AppendLine("       Return StatusCode(HttpStatusCode.NoContent)")
        sb.AppendLine("   End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function ControllerDelete(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" #Region " & (DT.TableName & " Put").QT)
        sb.AppendLine("    '  DELETE api/" & DT.TablePluralize & "/5")
        sb.AppendLine("    <ResponseType(GetType(" & DT.TableSingularize & "Vm))>")
        sb.AppendLine("    Public Function Delete" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ") As IHttpActionResult")
        sb.AppendLine("        Dim result As " & DT.TableSingularize & " = db." & DT.TablePluralize & ".Where(Function(_" & Left(DT.TableValue, 3) & ") _" & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = id).SingleOrDefault()")
        sb.AppendLine("        If result Is Nothing Then")
        sb.AppendLine("            Return NotFound()")
        sb.AppendLine("        End If")
        sb.AppendLine("        db." & DT.TablePluralize & ".DeleteOnSubmit(result)")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine("        Return Ok(result)")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
        Return sb.ToString
    End Function
End Class
