Imports DBExtenderLib
Imports System.Text

Public Class Durandal
    Public Shared Function StartBase(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<div class=" & "containerdetails".QT & ">")
        sb.AppendLine("    <section>")
        sb.AppendLine("        <h2 class=" & ("page-title").QT & " data-bind=" & "text: title".QT & "></h2>")
        sb.AppendLine("    </section>")
        sb.AppendLine("  </div>")
        
        Return sb.ToString
    End Function

    Public Shared Function BaseViewModel(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("define(['services/logger'], function (logger) {")
        sb.AppendLine("    var title = '" & DT.TableName & "';")
        sb.AppendLine("    var vm = {")
        sb.AppendLine("        activate: activate,")
        sb.AppendLine("        title: title")
        sb.AppendLine("    };")
        sb.AppendLine("")
        sb.AppendLine("    return vm;")

        sb.AppendLine("    //#region Internal Methods")
        sb.AppendLine("    function activate() {")
        sb.AppendLine("        logger.log(title + ' View Activated', null, title, true);")
        sb.AppendLine("       return true;")
        sb.AppendLine("    }")
        sb.AppendLine("    //#endregion")
        sb.AppendLine("});")
        Return sb.ToString
    End Function
    Public Shared Function JavaScriptRepository(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim pColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        sb.AppendLine("define(['ajaxhelper/ajaxHelper'], function (ajaxHelper) {")
        sb.AppendLine("    function " & DT.TableSingularize.LowerTheFistChar & "Url(id) { return '/api/" & DT.TablePluralize.ToLower & "/' + (id || ''); }")
        sb.AppendLine()
        sb.AppendLine("    var get" & DT.TablePluralize & " = function () {")
        sb.AppendLine("        return ajaxHelper.ajaxRequest(" & "get".QT & ", " & DT.TableSingularize.LowerTheFistChar & "Url())")
        sb.AppendLine("        .done(getSucceeded)")
        sb.AppendLine("                   .fail(getFailed);")
        sb.AppendLine("        function getSucceeded(data) {")
        sb.AppendLine("            return Q.when(data);")
        sb.AppendLine("        }")
        sb.AppendLine("        function getFailed() {")
        sb.AppendLine("            return" & ("Error " & DT.TablePluralize & "  lists.").QT & ";")
        sb.AppendLine("        }")
        sb.AppendLine("    };")
        sb.AppendLine("    function save" & DT.TableSingularize & "(" & DT.TableSingularize.LowerTheFistChar & ") {")
        sb.AppendLine("        return ajaxHelper.ajaxRequest(" & "post".QT & ", " & DT.TableSingularize.LowerTheFistChar & "Url(), " & DT.TableSingularize.LowerTheFistChar & ")")
        sb.AppendLine("            .done(function (result) {")
        sb.AppendLine("                " & DT.TableSingularize.LowerTheFistChar & "." & pColumn.ColumnValue.LowerTheFistChar & "(result." & pColumn.ColumnValue.LowerTheFistChar & ");")
        sb.AppendLine("            })")
        sb.AppendLine("            .fail(function () {")
        sb.AppendLine("                console.log(" & ("Error adding a new " & DT.TableSingularize.LowerTheFistChar).QT & " );")
        sb.AppendLine("            });")
        sb.AppendLine("    }")
        sb.AppendLine("    function Update" & DT.TableSingularize & "(" & DT.TableSingularize.LowerTheFistChar & ") {")
        sb.AppendLine("        return ajaxHelper.ajaxRequest(" & "put".QT & ", " & DT.TableSingularize.LowerTheFistChar & "Url(" & DT.TableSingularize.LowerTheFistChar & "." & pColumn.ColumnValue.LowerTheFistChar & "()), " & DT.TableSingularize.LowerTheFistChar & "," & "text".QT & ")")

        sb.AppendLine("          .fail(function () {")
        sb.AppendLine("              console.log(" & ("Error updating " & DT.TablePluralize.LowerTheFistChar & " item.").QT & ");")
        sb.AppendLine("          });")
        sb.AppendLine("    }")
        sb.AppendLine("    function delete" & DT.TableSingularize & "(" & DT.TableSingularize.LowerTheFistChar & ") {")
        sb.AppendLine("        return ajaxRequest(" & "delete".QT & ", " & DT.TableSingularize.LowerTheFistChar & "Url(" & DT.TableSingularize.LowerTheFistChar & "." & pColumn.ColumnValue.LowerTheFistChar & "()))")
        sb.AppendLine("            .fail(function () {")
        sb.AppendLine("              console.log(" & ("Error Delete " & DT.TablePluralize.LowerTheFistChar & " item.").QT & ");")
        sb.AppendLine("            });")
        sb.AppendLine("    }")
        sb.AppendLine("    return {")
        sb.AppendLine("        get" & DT.TablePluralize & ": get" & DT.TablePluralize.LowerTheFistChar & ",")
        sb.AppendLine("        save" & DT.TableSingularize & ": save" & DT.TableSingularize & ",")
        sb.AppendLine("        Update" & DT.TableSingularize & ": Update" & DT.TableSingularize & ",")
        sb.AppendLine("        delete" & DT.TableSingularize & ": delete" & DT.TableSingularize)
        sb.AppendLine("    };")
        sb.AppendLine("});")

        Return sb.ToString
    End Function



End Class
