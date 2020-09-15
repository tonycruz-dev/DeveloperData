Imports DBExtenderLib
Imports System.Text
Public Class MVCControls

    Public Shared Function MVCTableEditTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("@ModelType SKS.Models." & DT.TableValue)
        sb.AppendLine(" <table id='tabs" & DT.TableValue & "' class='table'>")
        '
        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine(" <tr>")
            sb.AppendLine("    <td><label>" & col.ColumnValue & "</label></td>")
            sb.AppendLine("    <td><label>@Html.DisplayFor(Function(model) model." & col.ColumnValue & ")</label></td>")
            sb.AppendLine(" </tr>")
        Next
        sb.AppendLine("    </table>")
        sb.Append("        @Using Ajax.BeginForm(" & ("Delete" & DT.TableValue & "Confirmed").QT & ", " & DT.TableValue.QT & ", New With {.id = Model." & DT.GetPrimaryKey.ColumnValue & "  }, New AjaxOptions With {.HttpMethod =" & "GET".QT & ", .UpdateTargetId =")
        sb.AppendLine((DT.TableValue & "ListTable").QT & ", .OnComplete = " & "closeDialogBox('#DeleteItem')".QT & "})")
        sb.AppendLine("      @<p>")
        sb.AppendLine("        <input type= " & "submit".QT & " value=" & "Delete".QT & " /> ")
        sb.AppendLine("      </p>")
        sb.AppendLine("End Using")
        Return sb.ToString
    End Function
    Public Shared Function MVCTableListNameTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("    <td> " & col.ColumnName & "</td>")
        Next
        Return sb.ToString
    End Function
    Public Shared Function MVCTableListValuesTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        For Each col As ColumnsInfo In DT.ListColumn
            '@Html.DisplayFor(Function(modelItem) currentItem.Address4)
            sb.AppendLine("    <td> @Html.DisplayFor(Function(modelItem) currentItem." & col.ColumnValue & ")</td>")
        Next
        Return sb.ToString
    End Function
End Class
