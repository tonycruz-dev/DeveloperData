Imports DBExtenderLib
Imports System.Text

Public Class angularJS
    Public Shared Function CreateTypeScripClass(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("export interface " & LowerTheFistChar(DT.TableSingularize) & " {")

        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    " & GetTypeScrip(R))
        Next
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString
    End Function
    Public Shared Function GetHtmlTableList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<table class=" & "table table-hover table-striped table-condensed ordersTable".QT & " style=" & "margin-left: 50px".QT & ">")
        sb.AppendLine("  <tr>")
        For Each col In DT.ListColumn
            sb.AppendLine("    <th>" & col.ColumnName & "</th>")
        Next
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sb.AppendLine("  </tr>")
        sb.AppendLine("  <tr data-ng-repeat=" & ("item in " & LowerTheFistChar(DT.TableValue) & " | orderBy:" & LowerTheFistChar(colPk.ColumnValue)).QT & ">")
        For Each col In DT.ListColumn
            sb.AppendLine("    <td>{{ item." & LowerTheFistChar(col.ColumnValue) & "}}</td>")
        Next
        sb.AppendLine("  </tr>")
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function
    Public Shared Function GetTableController(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("   app.controller('" & DT.TableValue & " Controller',")
        sb.AppendLine("    function ($scope, Services) {")
        sb.AppendLine("        init();")
        sb.AppendLine()
        sb.AppendLine("        function init() {")
        sb.AppendLine("            get" & DT.TableValue & "();")
        sb.AppendLine("             function get" & DT.TableValue & "() {")
        sb.AppendLine("               Services.get" & DT.TableValue & "()")
        sb.AppendLine("               .then(success)")
        sb.AppendLine("               .fail(failed)")
        sb.AppendLine("               .fin(refreshView);")
        sb.AppendLine("             }")
        sb.AppendLine("             function success(data) {")
        sb.AppendLine("                 $scope." & LowerTheFistChar(DT.TableValue) & " = data.results;")
        sb.AppendLine("             }")
        sb.AppendLine("             function failed() {")
        sb.AppendLine("               $scope.errorMessage = Error.message;")
        sb.AppendLine("           }")
        sb.AppendLine("             function refreshView() {")
        sb.AppendLine("                 $scope.$apply();")
        sb.AppendLine("             }")
        sb.AppendLine("         }")
        sb.AppendLine("     });")

        Return sb.ToString
    End Function
    Public Shared Function GetTableServer(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sb.AppendLine("    // categories Management start here ")
        sb.AppendLine("   this.get" & DT.TableValue & " = function () {")
        sb.AppendLine("    var " & LowerTheFistChar(DT.TableValue) & "Query = breeze.EntityQuery.from('" & LowerTheFistChar(DT.TableValue) & "')")
        sb.AppendLine("                        .expand('Products')")
        sb.AppendLine("                        .orderBy('" & LowerTheFistChar(colPk.ColumnValue) & "');")
        sb.AppendLine("    return manager.executeQuery(" & LowerTheFistChar(DT.TableValue) & "Query)")
        sb.AppendLine("    .then(Succeeded)")
        sb.AppendLine("    .fail(Failed);")
        sb.AppendLine("}")
        Return sb.ToString

    End Function
    Public Shared Function GetTableByIDController(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sb.AppendLine(" app.controller('" & LowerTheFistChar(DT.TableValue) & "ByIDController', function ($scope, $routeParams, Services) {")
        sb.AppendLine()
        sb.AppendLine("     var" & LowerTheFistChar(colPk.ColumnValue) & " = Number($routeParams." & LowerTheFistChar(colPk.ColumnValue) & ");")
        sb.AppendLine("     get" & LowerTheFistChar(DT.TableValue) & "();")
        sb.AppendLine("     function get" & LowerTheFistChar(DT.TableValue) & "() {")
        sb.AppendLine("         Services.get" & LowerTheFistChar(DT.TableValue) & "(" & LowerTheFistChar(colPk.ColumnValue) & ")")
        sb.AppendLine("         .then(success)")
        sb.AppendLine("         .fail(failed)")
        sb.AppendLine("         .fin(refreshView);")
        sb.AppendLine("     }")
        sb.AppendLine("     function success(data) {")
        sb.AppendLine("        $scope." & LowerTheFistChar(DT.TableValue) & " = data.results;")
        sb.AppendLine("     }")
        sb.AppendLine("     function failed() {")
        sb.AppendLine("         $scope.errorMessage = Error.message;")
        sb.AppendLine("     }")
        sb.AppendLine("     function refreshView() {")
        sb.AppendLine("         $scope.$apply();")
        sb.AppendLine("     }")
        sb.AppendLine(" });")
        Return sb.ToString

    End Function
    Shared Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function

    Shared Function Isrequired(DT As TableNameInfo, col As ColumnsInfo) As String
        Dim results As String = ""
        Dim textIsrequired As String = IIf(col.IsRequared, "required data-ng-maxlength=" & col.Size.ToString.QT, "")
        If col.IsRequared Then
            results = "     <input name=" & col.ColumnValue.QT & " type=" & "text".QT & " data-ng-model=" & ("new" & DT.TableSingularize & "." & col.ColumnValue).QT & textIsrequired & " />" & vbNewLine
            results &= "     <span class=" & "error".QT & " data-ng-show=" & ("new" & DT.TableSingularize & "." & col.ColumnValue & ".$error.required").QT & " >*</span>" & vbNewLine
            results &= "     <span class=" & "error".QT & " data-ng-show=" & ("new" & DT.TableSingularize & "." & col.ColumnValue & ".$error.maxlength").QT & ">Max  " & col.Size & " Characters </span>" & vbNewLine
        Else
            results = "     <input name=" & col.ColumnValue.QT & " type=" & "text".QT & " data-ng-model=" & ("new" & DT.TableSingularize & "." & col.ColumnValue).QT & textIsrequired & " />" & vbNewLine
            results &= "     <span class=" & "error".QT & " data-ng-show=" & ("new" & DT.TableSingularize & "." & col.ColumnValue & ".$error.maxlength").QT & ">Max  " & col.Size & " Characters </span>" & vbNewLine
        End If
        Return results
    End Function
    Shared Function IsrequiredValue(DT As TableNameInfo, col As ColumnsInfo) As String
        Return "<input name=" & col.ColumnValue.QT & " type=" & "text".QT & " data-ng-model=" & ("new" & DT.TableSingularize & "." & col.ColumnValue).QT & " required data-ng-maxlength=" & col.Size.ToString.QT & " />"

    End Function
    Public Shared Function GetHtmlFormNew(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<h3>New Topic</h3>")
        sb.AppendLine("<form name=" & ("new" & DT.TableSingularize & "Form").QT & " novalidate data-ng-submit=" & "save()".QT & " >")
        sb.AppendLine("  <fieldset>")
        For Each col In DT.ListColumn
            sb.AppendLine("    <div>")
            sb.AppendLine("      <label for=" & col.ColumnValue.QT & " >" & col.ColumnValue & "</label>")
            sb.AppendLine(Isrequired(DT, col))
            sb.AppendLine("    </div>")
        Next


        sb.AppendLine("  </fieldset>")
        sb.AppendLine("</form>")
        Return sb.ToString
    End Function
    ''<div class="form-group" ng-class="{'has-error': customerForm.customerID.$invalid && customerForm.customerID.$dirty}">'
    Public Shared Function GetHtmlFormGroup(DT As TableNameInfo, ByVal col As ColumnsInfo) As String
        Return "<div class=" & "form-group".QT & " ng-class= " & ("{'has-error': " & DT.TableValue.ToLower & "Form." & col.ColumnValue & ".$invalid && " & DT.TableValue.ToLower & "Form." & col.ColumnValue & ".$dirty}").QT & ">"
    End Function
    Public Shared Function GetHtmlForm(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<h3>New Topic</h3>")
        sb.AppendLine("<form name=" & (DT.TableSingularize & "Form").QT & " novalidate data-ng-submit=" & "save()".QT & " >")
        sb.AppendLine("  <fieldset>")
        For Each col In DT.ListColumn

            sb.AppendLine("    <div >")
            sb.AppendLine("      <label for=" & col.ColumnValue.QT & " >" & col.ColumnValue & "</label>")
            sb.AppendLine(Isrequired(DT, col))
            sb.AppendLine("    </div>")
        Next


        sb.AppendLine("  </fieldset>")
        sb.AppendLine("</form>")
        Return sb.ToString
    End Function
    Public Shared Function GetTypeScrip(ByVal col As ColumnsInfo) As String
        If Not col.IsColumnnsRequared Then
            Select Case col.TypeVB
                Case "Integer"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "number;"
                Case "String"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "string;"
                Case "Decimal"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "number;"
                Case "Int16"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "number;"
                Case "Single"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "number;"
                Case "Date"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "Date;"
                Case "Boolean"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "boolean"
                Case "Double"
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "number;"
                Case Else
                    Return LowerTheFistChar(col.ColumnValue) & "?: " & "string;"
            End Select
        Else
            Select Case col.TypeVB
                Case "Integer"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "number;"
                Case "String"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "string;"
                Case "Decimal"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "number;"
                Case "Int16"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "number;"
                Case "Single"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "number;"
                Case "Date"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "Date;"
                Case "Boolean"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "boolean"

                Case "Double"
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "number;"
                Case Else
                    Return LowerTheFistChar(col.ColumnValue) & ": " & "string;"
            End Select
        End If


        Return LowerTheFistChar(col.ColumnValue) & ": " & "string;"
    End Function
End Class
