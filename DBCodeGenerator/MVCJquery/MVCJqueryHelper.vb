Imports DBExtenderLib
Imports System.Text
Public Class MVCJqueryHelper

    Public Shared Function TableListTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("  '        @* list of " & DT.TableName & " template*@")
        sb.AppendLine("  <script id=" & (DT.TableValue & "Template").QT & " type=" & "text/x-jsrender".QT & " >")
        sb.AppendLine(" <tr>")
        sb.AppendLine("    <td>")
        sb.AppendLine("       <a class=" & "menu_link".QT & " href=" & ("{{:" & DT.GetPrimaryKey.ColumnValue & "}}").QT & ">Edit</a> |")
        sb.AppendLine("       <a class=" & "menu_linkDetails".QT & " href=" & ("{{:" & DT.GetPrimaryKey.ColumnValue & "}}").QT & ">Details</a> |")
        sb.AppendLine("       <a class=" & "menu_linkdelete".QT & " href=" & ("{{:" & DT.GetPrimaryKey.ColumnValue & "}}").QT & ">Delete</a> |")
        sb.AppendLine("     </td>")
        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("     <td>  {{:" & col.ColumnValue & "}} </td>")
        Next
        sb.AppendLine("  </tr>")
        sb.AppendLine(" </script>")

        sb.AppendLine(" <table class=" & "table table-condensed".QT & " >")
        sb.AppendLine("  <thead>")
        sb.AppendLine("   <tr>")
        sb.AppendLine("   <td></td>")
        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("     <td> " & col.ColumnValue & "</td>")
        Next
        sb.AppendLine("  </tr>")
        sb.AppendLine("  </thead>")
        sb.AppendLine()
        sb.AppendLine("  @*insert list  " & DT.TableName & " template here*@")
        sb.AppendLine("  <tbody id=Insert" & (DT.TableValue & "Template").QT & ">")
        sb.AppendLine("  </tbody>")
        sb.AppendLine("</table>")

        Return sb.ToString

    End Function
    Public Shared Function TableAddOrEditTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        '        @* Edit customer template*@
        sb.AppendLine(" @* Edit " & DT.TableName & " template*@")
        sb.AppendLine("  <script id=" & (DT.TableValue & "EditTemplate").QT & " type=" & "text/x-jsrender".QT & " >")

        '<table class="well">
        sb.AppendLine(" <table class=" & "table".QT & " >")

        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine(" <tr>")
            sb.AppendLine("    <td><label>" & col.ColumnValue & "</label></td>")
            sb.AppendLine("    <td><input id=" & ("Text" & col.ColumnValue).QT & "  type=" & "text".QT & " value=" & ("{{:" & col.ColumnValue & "}}").QT & " /></td>")
            sb.AppendLine(" </tr>")
        Next
        sb.AppendLine("    </table>")
        sb.AppendLine("</script>")

        ' @* dialobox template here*@
        sb.AppendLine(" @* Edit " & DT.TableName & " dialobox template here*@")
        sb.AppendLine(" <div id=" & ("Edit" & DT.TableName).QT & "title=" & ("Edit " & DT.TableName).QT & " >")
        sb.AppendLine(" @* Insert " & DT.TableName & " data template here*@")
        sb.AppendLine("    <div id=" & (DT.TableName & "Data").QT & "></div>")
        sb.AppendLine(" </div>")

        Return sb.ToString
    End Function
    Public Shared Function TableDetailsTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        '        @* Edit customer template*@
        sb.AppendLine(" @* Edit " & DT.TableName & " template*@")
        sb.AppendLine("  <script id=" & (DT.TableValue & "EditTemplate").QT & " type=" & "text/x-jsrender".QT & " >")

        '<table class="well">
        sb.AppendLine(" <table class=" & "table".QT & " >")

        For Each col As ColumnsInfo In DT.ListColumn
            sb.AppendLine(" <tr>")
            sb.AppendLine("    <td><label>" & col.ColumnValue & "</label></td>")
            sb.AppendLine("    <td><label>{{:" & col.ColumnValue & "}}</label> </td>")
            sb.AppendLine(" </tr>")
        Next
        sb.AppendLine("    </table>")
        sb.AppendLine("</script>")

        ' @* dialobox template here*@
        sb.AppendLine(" @* Edit " & DT.TableName & " dialobox template here*@")
        sb.AppendLine(" <div id=" & ("Edit" & DT.TableName).QT & "title=" & ("Edit " & DT.TableName).QT & " >")
        sb.AppendLine(" @* Insert " & DT.TableName & " data template here*@")
        sb.AppendLine("    <div id=" & (DT.TableName & "Data").QT & "></div>")
        sb.AppendLine(" </div>")

        Return sb.ToString

    End Function
    Public Shared Function MVCControllerTemplate(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" Public Function Get" & DT.TableValue & "() As JsonResult")
        sb.AppendLine("    Dim db As New " & DBName & "Entities")
        sb.AppendLine("    Dim Results" & DT.TableValue & " = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue)
        sb.Append("              Select ")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        " & Left(DT.TableName, 3) & "." & Col.ColumnValue & ", ")
        Next
        Dim mylastComar = sb.ToString.LastIndexOf(",")
        sb.Remove(mylastComar, 1)
        sb.AppendLine("        ).ToList")
        Return sb.ToString
    End Function

    Public Shared Function JQueryTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("$(document).ready(function () {")
        sb.AppendLine("    $.getJSON('/" & DT.TableValue & "/Get" & DT.TableValue & "', null, function (data) {")
        sb.AppendLine()
        sb.AppendLine("        $('#InstructorList').html($('#InstructerTemplate').render(data));")
        sb.AppendLine("    });")
        sb.AppendLine("    $('.menu_link').live('click', function () {")
        sb.AppendLine("        var thelink = $(this);")
        sb.AppendLine("        $.getJSON('/" & DT.TableValue & "/GetSingle" & DT.TableValue & "', { id: thelink.attr('href') },")
        sb.AppendLine("                function (data) {")
        sb.AppendLine("                    $('#" & DT.TableValue & "Data').html($('#" & DT.TableValue & "EditTemplate').render(data));")
        sb.AppendLine("                });")
        sb.AppendLine("         $('#Edit" & DT.TableValue & "').dialog({ autoOpen: true, minHeight: 450, minWidth: 400, show: 'slide',")
        sb.AppendLine("            buttons: {")
        sb.AppendLine("        'Close': function () ")
        sb.AppendLine("                   {")
        sb.Append("                var " & DT.TableValue & " = {")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("  " & Col.ColumnValue & ": $('#Text" & Col.ColumnValue & "').val(),")
        Next
        sb.AppendLine(" }; ")
        sb.AppendLine("                   $.ajax({url: '/" & DT.TableValue & "/Edit" & DT.TableValue & "',data: JSON.stringify(" & DT.TableValue & "),")
        sb.AppendLine("                        type: 'POST',contentType: 'application/json; charset=utf-8',")
        sb.AppendLine("                        dataType:  'json', success: function () { }")
        sb.AppendLine("                    });")
        sb.AppendLine("                    $('#Edit" & DT.TableValue & "').dialog('close');")
        sb.AppendLine("                }")
        sb.AppendLine("            }")
        sb.AppendLine("        })")
        sb.AppendLine("        return false;")
        sb.AppendLine("    });")
        sb.AppendLine("});")
        Return sb.ToString
    End Function
    Public Shared Function CreateVMClass(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<DataContract()>")
        sb.AppendLine("Public Class " & DT.TableValue & "VM")
        For Each Col As ColumnsInfo In DT.ListColumn
            If Col.IsPrimary_Key Then
                sb.AppendLine("    <Key()> ")
                sb.AppendLine("    <DataMember()>")
                sb.AppendLine("    Public Property  " & Col.ColumnValue & " As " & Col.LinqVar)
            Else
                sb.AppendLine("    <DataMember()>")
                sb.AppendLine("    Public Property  " & Col.ColumnValue & " As " & Col.LinqVar)
            End If

        Next
        sb.AppendLine("End Class")
        sb.AppendLine("'----------------")
        sb.AppendLine("<DataContract()>")
        sb.AppendLine("Public Class Manager" & DT.TableValue & "VM")
        'Public Property Customers As List(Of CustomersViewModel)
                sb.AppendLine("    <DataMember()>")
        sb.AppendLine("    Public Property  " & DT.TableValue & " As List(Of " & DT.TableValue & "VM)")

                sb.AppendLine("    <DataMember()>")
                sb.AppendLine("    Public Property  HasNext As Boolean")

                sb.AppendLine("    <DataMember()>")
                sb.AppendLine("    Public Property  HasPrevious As Boolean")

                sb.AppendLine("    <DataMember()>")
        sb.AppendLine("     Public Property TotalPage As New List(Of PageVM)")
        sb.AppendLine("End Class")
        Return sb.ToString
    End Function

End Class
