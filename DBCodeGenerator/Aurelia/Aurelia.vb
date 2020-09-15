Imports DBExtenderLib
Imports System.Text

Public Class Aurelia

    Public Shared Function AureliaClassList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("import {inject} from 'aurelia-framework';")
        sb.AppendLine("import {SourceManager} from '../modules/SourceManager';")
        sb.AppendLine("import {CategoriesDataContext} from './" & DT.TablePluralize & "DataContext';")
        sb.AppendLine("import 'bootstrap';")
        sb.AppendLine("import _ from 'lodash';")
        sb.AppendLine("@inject(SourceManager, " & DT.TablePluralize & "DataContext)")
        sb.AppendLine("export class List" & DT.TablePluralize & " {")
        sb.AppendLine("   title = '" & DT.TablePluralize & "';")
        sb.AppendLine("   constructor(datasource, datacontext) {")
        sb.AppendLine("      this.title = 'List " & DT.TablePluralize & "';")
        sb.AppendLine("       this.datasource = new SourceManager(5);")
        sb.AppendLine("       this.datacontext = datacontext;")
        sb.AppendLine("")
        sb.AppendLine("   }")
        sb.AppendLine("    activate() {")
        sb.AppendLine("        Return this.datacontext.getAll()")
        sb.AppendLine("           .then(result => {")
        sb.AppendLine("               this.datasource.pageInit(result." & DT.TablePluralize & ")")
        sb.AppendLine("           });")
        sb.AppendLine("    }")
        sb.AppendLine("    onKeyUp(ev) {")
        sb.AppendLine("        if (ev.keyCode === 13) {")
        sb.AppendLine("            this.searchAll();")
        sb.AppendLine("        }")
        sb.AppendLine("    }")
        sb.AppendLine("    searchAll() {")
        sb.AppendLine("       if (this.datasource.searchCriteria !== '') {")
        sb.AppendLine("          var filter = this.datasource.searchCriteria.trim().toLowerCase();")
        sb.AppendLine("          var categoriesResults = _.filter(this.datasource.sourceCache,")
        sb.AppendLine("               function (cat) {")
        sb.AppendLine("                   return _.includes(cat.categoryName.trim().toLowerCase(),")
        sb.AppendLine("                       filter.trim().toLowerCase()) ||")
        sb.AppendLine("                       _.includes(cat.description.trim().toLowerCase(),")
        sb.AppendLine("                           filter.trim().toLowerCase());")
        sb.AppendLine("               });")
        sb.AppendLine("           this.datasource.pageSearch(categoriesResults);")
        sb.AppendLine("       } else {")
        sb.AppendLine("           this.datasource.pageSearch(this.datasource.sourceCache);")
        sb.AppendLine("       }")
        sb.AppendLine("    }")
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function AureliaClassValidation(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine(" export class " & DT.TableSingularize & "Info {")
        sb.AppendLine()
        sb.AppendLine("    constructor(){")

        For Each col In DT.ListColumn
            sb.AppendLine("              this." & col.ColumnValue.LowerTheFistChar & " = " & IIf(col.TypeVB = "String", "".QT & ";", 0 & ";"))
        Next
        sb.AppendLine("      this.validation;  ")
        sb.AppendLine("    }")
        sb.AppendLine("    static toJson(item) {")
        sb.AppendLine("        return {")
        For Each col In DT.ListColumn
            sb.AppendLine("    " & (col.ColumnValue.LowerTheFistChar).QT & " : " & "item." & col.ColumnValue.LowerTheFistChar & ",")
        Next
        sb.AppendLine("            }")
        sb.AppendLine("    }")
        sb.AppendLine("    static Validate(item, validation) {")
        sb.AppendLine("        item.validation = validation.on(item)")

        For Each col In DT.ListColumn
            If col.IsRequared And Not col.IsForeign_Key Then
                sb.AppendLine("        .ensure(" & (col.ColumnValue.LowerTheFistChar).QT & ")")
                sb.AppendLine("        .isNotEmpty()")
                sb.AppendLine("        .hasLengthBetween(0," & col.Size & ")")
            Else
                sb.AppendLine("        .ensure(" & (col.ColumnValue.LowerTheFistChar).QT & ")")
                sb.AppendLine("        .hasLengthBetween(0," & col.Size & ")")
            End If

        Next
        sb.AppendLine("    }")
        sb.AppendLine("")
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function GetAureliaTemplateList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<table class=" & "table table-condensed".QT & ">")
        sb.AppendLine("          <thead>")
        sb.AppendLine("              <tr>")
        For Each col In DT.ListColumn
            sb.AppendLine("                  <td>" & col.ColumnName & "</td>")
        Next
        sb.AppendLine("<td></td>")
        sb.AppendLine("<td></td>")
        sb.AppendLine("<td></td>")
        sb.AppendLine("              </tr>")
        sb.AppendLine("          </thead>")
        sb.AppendLine("          <tbody>")
        sb.AppendLine("              <tr repeat.for=" & "item of  datasource.displaySource".QT & ">")
        For Each col In DT.ListColumn
            sb.AppendLine("              <td>${item." & col.ColumnValue & "}</td>")
        Next
        sb.AppendLine("                  <td><a route-href=" & ("route: " & DT.TableSingularize.LowerTheFistChar & "details; params.bind: {id: item." & DT.GetPrimaryKey.ColumnValue.LowerTheFistChar & "}").QT & "><i class=" & "fa fa-file-text".QT & "></i> Details </a></td>")
        sb.AppendLine("                  <td><a route-href=" & ("route: " & DT.TableSingularize.LowerTheFistChar & "edit; params.bind: {id: item." & DT.GetPrimaryKey.ColumnValue.LowerTheFistChar & "}").QT & "><i class=" & "fa fa-pencil-square-o".QT & "></i> Edit </a></td>")
        sb.AppendLine("                  <td><a route-href=" & ("route: " & DT.TableSingularize.LowerTheFistChar & "delete; params.bind: {id: item." & DT.GetPrimaryKey.ColumnValue.LowerTheFistChar & "}").QT & "><i class=" & "fa fa-trash-o".QT & "  style=" & "color:red".QT & "></i> Delete </a></td>")
        sb.AppendLine("              </tr>")
        sb.AppendLine("          </tbody>")
        sb.AppendLine("      </table>")

        Return sb.ToString
    End Function
    Public Shared Function GetAureliaTemplateForm(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("  <form id=" & "sky-form4".QT & " class=" & "sky-form".QT & ">")
        sb.AppendLine("        <header> " & DT.TableSingularize & " form </header>")
        sb.AppendLine("        <fieldset>")
        For Each col In DT.ListColumn
            sb.AppendLine("          <section>")
            sb.AppendLine("              <label Class=" & "input".QT & " >")
            sb.AppendLine("			         <i Class=" & "icon-append fa fa-user".QT & "></i>")
            sb.AppendLine("			    	 <input type=" & "text".QT & " name=" & col.ColumnValue.QT & " placeholder=" & col.ColumnValue.QT & ">")
            sb.AppendLine("				     <b Class=" & "tooltip tooltip-bottom-right".QT & ">Needed to enter the website</b>")
            sb.AppendLine("              </label>")
            sb.AppendLine("          </section>")
        Next
        sb.AppendLine("        </fieldset>")
        sb.AppendLine("	<footer>")
        sb.AppendLine("		<button type=" & "submit".QT & " Class=" & "btn-u".QT & " >Submit</button>")
        sb.AppendLine("	</footer>")
        sb.AppendLine("   </form>")
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

End Class
