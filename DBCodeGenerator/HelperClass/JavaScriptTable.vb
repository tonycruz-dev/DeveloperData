Imports DBExtenderLib
Imports System.Text

Public Class JavaScriptTable

    Public Shared Function JQueryTableTemplate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" $.ajaxSetup({ cache: false });")
        sb.AppendLine("'$(document).ready(function () {")
        sb.AppendLine("    var page = parseInt(0);")
        sb.AppendLine("    my.utils.jqLoadListData('" & DT.TableValue & "', '" & DT.TableValue & "', '#" & DT.TableValue & "List', 'Services/NorthwindWS.asmx/Get" & DT.TableValue & "Page', page);")
        sb.AppendLine("    $('.pagination_link').live('click', function () {")
        sb.AppendLine("        var thelink = $(this);")
        sb.AppendLine("        page = parseInt(thelink.attr('href'));")
        sb.AppendLine("        my.utils.jqLoadListData('" & DT.TableValue & "', '" & DT.TableValue & "', '#" & DT.TableValue & "List', 'Services/NorthwindWS.asmx/Get" & DT.TableValue & "Page', page);")
        sb.AppendLine("        return false;")
        sb.AppendLine("    });")
        sb.AppendLine("    $('.menu_linkDetails').live('click', function () {")
        sb.AppendLine("        var thelink = $(this);")
        sb.AppendLine("        my.utils.jqFindSingleRecord('Services/NorthwindWS.asmx/Find" & DT.TableValue & "', thelink.attr('href'), '" & DT.TableValue & "', '" & DT.TableValue & "Details', '#" & DT.TableValue & "DetailsData');")
        sb.AppendLine("        $('#" & DT.TableValue & "DetailsDisplay').modal('show');")
        sb.AppendLine("         return false;")
        sb.AppendLine("    });")
        sb.AppendLine("      $('.menu_linkDetailsEdit').live('click', function () {")
        sb.AppendLine("          var thelink = $(this);")
        sb.AppendLine("          my.utils.jqFindSingleRecord('Services/NorthwindWS.asmx/Find" & DT.TableValue & "', thelink.attr('href'), '" & DT.TableValue & "', '" & DT.TableValue & "Edit', '#InsertEdit" & DT.TableValue & "');")
        sb.AppendLine("          $('#" & DT.TableValue & "DialogBoxEdit').modal('show');")
        sb.AppendLine("          return false;")
        sb.AppendLine("     });")
        sb.AppendLine("     $('.menu_linkDelete').live('click', function () {")
        sb.AppendLine("          var thelink = $(this);")
        sb.AppendLine("          my.utils.jqFindSingleRecord('Services/NorthwindWS.asmx/Find" & DT.TableValue & "', thelink.attr('href'), '" & DT.TableValue & "', '" & DT.TableValue & "Delete', '#InsertDelete" & DT.TableValue & "Template');")
        sb.AppendLine("          $('#" & DT.TableValue & "DialogBoxDelete').modal('show');")
        sb.AppendLine("          return false;")
        sb.AppendLine("      });")
        sb.AppendLine("     $('#DeleteData').click(function () {")
        sb.AppendLine("          var deleteID = $('#Deleteid').html();")
        sb.AppendLine("          my.utils.jqFindSingleRecord('Services/NorthwindWS.asmx/Delete" & DT.TableValue & "', deleteID, '" & DT.TableValue & "Delete', '#InsertDelete" & DT.TableValue & "Template');")
        sb.AppendLine("          my.utils.jqLoadListData('" & DT.TableValue & "', '" & DT.TableValue & "', '#" & DT.TableValue & "List', 'Services/NorthwindWS.asmx/Get" & DT.TableValue & "Page', page);")
        sb.AppendLine("          return false;")
        sb.AppendLine("      })")
        sb.AppendLine("     $('#closeDelete').click(function () {")
        sb.AppendLine("         my.utils.jqLoadListData('" & DT.TableValue & "', '" & DT.TableValue & "', '#" & DT.TableValue & "List', 'Services/NorthwindWS.asmx/Get" & DT.TableValue & "Page', page);")
        sb.AppendLine("    })")
        sb.AppendLine(" $('#SaveData').click(function () {")
        sb.Append("    var " & DT.TableValue & " = {")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("  " & Col.ColumnValue & ": $('#Text" & Col.ColumnValue & "').val(),")
        Next
        Dim mylastComar = sb.ToString.LastIndexOf(",")
        sb.Remove(mylastComar, 1)

        sb.AppendLine(" }; ")
        sb.AppendLine(" $.ajax({ url: 'Services/NorthwindWS.asmx/Update" & DT.TableValue & "',")
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
        sb.AppendLine("          my.utils.jqLoadListData('" & DT.TableValue & "', '" & DT.TableValue & "', '#" & DT.TableValue & "List', 'Services/NorthwindWS.asmx/Get" & DT.TableValue & "Page', page);")
        sb.AppendLine("});")
        sb.AppendLine("     $('#ButtonCreateNew').click(function () {")
        sb.AppendLine("         my.utils.createNewRecordNoTemplate('Services/NorthwindWS.asmx/Create" & DT.TableValue & "');")
        sb.AppendLine("         my.utils.jqLoadListData('" & DT.TableValue & "', '" & DT.TableValue & "', '#" & DT.TableValue & "List', 'Services/NorthwindWS.asmx/Get" & DT.TableValue & "Page', page);")
        sb.AppendLine("     })")
        sb.AppendLine("})")
        Return sb.ToString
    End Function
End Class
