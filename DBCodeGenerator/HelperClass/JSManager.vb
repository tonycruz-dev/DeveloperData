Imports System.Text
Imports DBExtenderLib

Public Class JSManager

    Public Shared Function GetHtmlTableList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<input id=" & "ButtonCreateNew".QT & " type=" & "button".QT & " value=" & ("Crate New " & DT.TableValue).QT & " class=" & "btn btn-primary".QT & " />")
        sb.AppendLine("<table class=" & "table table-condensed".QT & ">")
        sb.AppendLine("       <thead>")
        sb.AppendLine("           <tr>")
        sb.AppendLine("               <td>")
        sb.AppendLine("               </td>")
        For Each col In DT.ListColumn
            sb.AppendLine("               <th>")
            sb.AppendLine("                  " & col.ColumnValue)
            sb.AppendLine("               </th>")
        Next
        sb.AppendLine("           </tr>")
        sb.AppendLine("       </thead>")
        sb.AppendLine("       <tbody id=" & (DT.TableValue & "List").QT & ">")
        sb.AppendLine("       </tbody>")
        sb.AppendLine("   </table>")
        sb.AppendLine("   <div id=" & "pagination".QT & "/>")

        Return sb.ToString
    End Function
    Public Shared Function TableListTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        '<!-- list of Suppliers template-->
        sb.AppendLine(" <!-- list of " & DT.TableName & " template-->")
        sb.AppendLine(" {{for " & DT.TableName & "}}")
        sb.AppendLine(" <tr>")
        sb.AppendLine("    <td>")
        sb.AppendLine("       <a class=" & "menu_linkDetails".QT & " href=" & ("{{:" & DT.GetPrimaryKey.ColumnValue & "}}").QT & ">Details</a> |")
        sb.AppendLine("       <a class=" & "menu_linkDetailsEdit".QT & " href=" & ("{{:" & DT.GetPrimaryKey.ColumnValue & "}}").QT & ">Edit</a> |")
        sb.AppendLine("       <a class=" & "menu_linkDelete".QT & " href=" & ("{{:" & DT.GetPrimaryKey.ColumnValue & "}}").QT & ">Delete</a> |")
        sb.AppendLine("     </td>")
        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("     <td>  {{:" & col.ColumnValue & "}} </td>")
        Next
        sb.AppendLine("  </tr>")
        sb.AppendLine("  {{/for}}")

        Return sb.ToString

    End Function
    Public Shared Function TableAddOrEditTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        '        @* Edit customer template*@
        sb.AppendLine(" <!-- Edit " & DT.TableName & " template   " & DT.TableName & "edit.temp.html -->")
        '<table class="well">
        sb.AppendLine(" <table class=" & "table".QT & " >")

        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine(" <tr>")
            sb.AppendLine("    <td><label>" & col.ColumnValue & "</label></td>")
            sb.AppendLine("    <td><input id=" & ("Text" & col.ColumnValue).QT & "  type=" & "text".QT & " value=" & ("{{:" & col.ColumnValue & "}}").QT & " /></td>")
            sb.AppendLine(" </tr>")
        Next
        sb.AppendLine("  </table>")

        Return sb.ToString
    End Function
    Public Shared Function TableDetailsTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        '        @* Edit customer template*@
        sb.AppendLine(" <!-- Details " & DT.TableName & " template    " & DT.TableName & "Details.temp.html-->")
        '<table class="well">
        sb.AppendLine(" <table class=" & "table".QT & " >")

        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine(" <tr>")
            sb.AppendLine("    <td><label>" & col.ColumnValue & "</label></td>")
            sb.AppendLine("    <td><label>{{:" & col.ColumnValue & "}}</label> </td>")
            sb.AppendLine(" </tr>")
        Next
        sb.AppendLine("    </table>")
        Return sb.ToString

    End Function
    Public Shared Function TableDeleteTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        '        @* Edit customer template*@
        sb.AppendLine(" <!-- delete " & DT.TableName & " template   " & DT.TableName & "Delete.temp.html-->")
        '<table class="well">
        sb.AppendLine(" <table class=" & "table".QT & " >")

        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine(" <tr>")
            sb.AppendLine("    <td><label>" & col.ColumnValue & "</label></td>")
            sb.AppendLine("    <td><label>{{:" & col.ColumnValue & "}}</label> </td>")
            sb.AppendLine(" </tr>")
        Next
        sb.AppendLine("    </table>")
        Return sb.ToString

    End Function
    Public Shared Function JQueryTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" $('#SaveNewData').click(function () {")
        sb.Append("    var " & DT.TableValue & " = {")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("  " & Col.ColumnValue & ": $('#Text" & Col.ColumnValue & "').val(),")
        Next
        Dim mylastComar = sb.ToString.LastIndexOf(",")
        sb.Remove(mylastComar, 1)

        sb.AppendLine(" }; ")
        '         my.utils.renderPageListTemplate('Suppliers', 'Suppliers', '#SuppliersList', 'Services/NorthwindWS.asmx/GetSuppliersPage', page);
        sb.AppendLine("});")
        Return sb.ToString
    End Function
    Public Shared Function JQuerySaveTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" $.ajax({ url: 'Services/NorthwindWS.asmx/SaveNew" & DT.TableValue & "',")
        sb.AppendLine("        type: 'POST', ")
        sb.AppendLine("        data: ")
        Dim count As Integer = 0
        For Each Col As ColumnsInfo In DT.ListColumn
            If count = 0 Then
                sb.AppendLine(("{" & Col.ColumnValue & ": '").QT & " + " & DT.TableValue & "." & Col.ColumnValue & " + ")
                count = count + 1
            Else
                sb.AppendLine(("'," & Col.ColumnValue & ":'").QT & " + " & DT.TableValue & "." & Col.ColumnValue & " + ")
            End If

        Next
        sb.AppendLine("'}".QT & ",")
        sb.AppendLine("     contentType:  'application/json; charset=UTF-8',")
        sb.AppendLine("    dataType:  'json',")
        sb.AppendLine("     async: false,")
        sb.AppendLine("     error: function (err) {")
        sb.AppendLine("         alert('Error message: ' + err.toString());")
        sb.AppendLine("     },")
        sb.AppendLine("     success: function (data) {")
        sb.AppendLine("     }")
        sb.AppendLine("   });")
        Return sb.ToString
    End Function

End Class
