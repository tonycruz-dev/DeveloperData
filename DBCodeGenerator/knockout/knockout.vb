Imports DBExtenderLib
Imports System.Text

Public Class knockout
    Public Shared Function GetHtmlTableList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<table class=" & "table table-condensed".QT & ">")
        '
        sb.AppendLine("  <thead>")
        sb.AppendLine("       <tr>")
        For Each col In DT.ListColumn
            sb.AppendLine("       <th>" & col.ColumnName & "</th>")
        Next
        sb.AppendLine("      </tr>")
        sb.AppendLine("  </thead>")
        '<tbody data-bind="foreach: vmData.data">
        sb.AppendLine("    <tbody data-bind=" & ("foreach: vmData.data").QT & ">")
        sb.AppendLine("       <tr>")
        For Each col In DT.ListColumn
            sb.AppendLine("    <td data-bind=" & ("text:" & col.ColumnValue).QT & "></td>")
        Next
        sb.AppendLine("     </tr>")
        sb.AppendLine("  </tbody>")
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function
    Public Shared Function GetHtmlTableWithDetails(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<table class=" & "table table-hover table-striped table-condensed ordersTable".QT & " style=" & "margin-left: 50px".QT & ">")
        '
        sb.AppendLine("  <thead>")
        sb.AppendLine("       <tr>")
        For Each col In DT.ListColumn
            sb.AppendLine("       <th>" & col.ColumnName & "</th>")
        Next
        sb.AppendLine("      </tr>")
        sb.AppendLine("  </thead>")
        sb.AppendLine("    <tbody data-bind=" & ("foreach:" & DT.TablePluralize).QT & ">")
        sb.AppendLine("       <tr>")
        For Each col In DT.ListColumn
            sb.AppendLine("    <td data-bind=" & ("text:" & LowerTheFistChar(col.ColumnValue).QT & "></td>"))
        Next
        sb.AppendLine("     </tr>")
        sb.AppendLine("  </tbody>")
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function
    Public Shared Function GetHtmlForm(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<form class=" & "form-horizontal".QT & " role=" & "form".QT & " >")
        sb.AppendLine("<div data-bind=" & ("with:" & DT.TableSingularize).QT & " >")
        For Each col In DT.ListColumn
            sb.AppendLine("     <div class=" & "form-group".QT & " >")
            sb.AppendLine("         <label for=" & col.ColumnValue.QT & "  class=" & "control-label col-md-3".QT & " >" & col.ColumnName & "</label>")
            sb.AppendLine("         <div class=" & "col-md-6".QT & " >")
            sb.AppendLine("             <input type=" & "text".QT & " data-bind=" & ("value:" & col.ColumnValue).QT & " id=" & col.ColumnName.QT & " name=" & col.ColumnName.QT & " class=" & "form-control".QT & "title=" & col.ColumnName.QT & " placeholder=" & col.ColumnName.QT & " />")
            sb.AppendLine("         </div>")
            sb.AppendLine("      </div>")
        Next
        sb.AppendLine("      </div>")
        sb.AppendLine("      <div class=" & "form-group".QT & " >")
        sb.AppendLine("          <div class=" & "col-md-6 col-md-offset-2".QT & " >")
        sb.AppendLine("               <button type=" & "submit".QT & " class=" & "btn btn-primary".QT & "><span class=" & "glyphicon glyphicon-floppy-save".QT & "> </span> Save</button>")
        sb.AppendLine("        </div>")
        sb.AppendLine("      </div>")

        sb.AppendLine("      </form>")
        Return sb.ToString
    End Function
    Public Shared Function GetHtmlDataForm(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
      
        For Each col In DT.ListColumn
            sb.AppendLine("     <div class=" & "form-group".QT & " >")
            sb.AppendLine("         <label for=" & col.ColumnValue.QT & "  class=" & "control-label col-md-3".QT & " >" & col.ColumnName & "</label>")
            sb.AppendLine("         <div class=" & "col-md-6".QT & " >")
            sb.AppendLine("             <input type=" & "text".QT & " data-bind=" & ("value:" & col.ColumnValue).QT & " id=" & col.ColumnName.QT & " name=" & col.ColumnName.QT & " class=" & "form-control".QT & "title=" & col.ColumnName.QT & " placeholder=" & col.ColumnName.QT & " />")
            sb.AppendLine("         </div>")
            sb.AppendLine("      </div>")
        Next
        Return sb.ToString
    End Function
    Public Shared Function GetKoClass(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("var " & DT.TableSingularize & "Item = function (data) { ")
        sb.AppendLine("     var self = this;")
        '    var ShippersItem = function (data) {
        '    
        For Each col In DT.ListColumn
            If col.TypeVB.ToLower = "Date".ToLower Or col.TypeVB.ToLower = "DateTime".ToLower Then
                sb.AppendLine("if ((data." & LowerTheFistChar(col.ColumnValue) & ") === null || data." & LowerTheFistChar(col.ColumnValue) & " === '') {")
                sb.AppendLine("     self.shippedDate = ko.observable(); ")
                sb.AppendLine("     } else { ")
                sb.AppendLine("    self." & LowerTheFistChar(col.ColumnValue) & " = ko.observable(moment(data." & LowerTheFistChar(col.ColumnValue) & ").format('DD-MM-YYYY'));")
                sb.AppendLine(" } ")
            Else
                sb.AppendLine("     self." & LowerTheFistChar(col.ColumnValue) & " = ko.observable(data." & LowerTheFistChar(col.ColumnValue) & " );")
            End If
        Next
        sb.AppendLine("    self.toJson = function () { return ko.toJSON(self) };")
        sb.AppendLine("};")

        Return sb.ToString
    End Function
    Public Shared Function GetKoReadClass(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("var find" & DT.TableSingularize & " = _.find(db." & DT.TableSingularize & "List, function (item) { return item." & DT.GetPrimaryKey.ColumnValue & " === " & DT.TableSingularize & "()." & DT.GetPrimaryKey.ColumnValue & "(); });")
        For Each col In DT.ListColumn
            sb.AppendLine("    find" & DT.TableSingularize & "." & LowerTheFistChar(col.ColumnValue) & " = " & DT.TableSingularize.LowerTheFistChar & "()." & LowerTheFistChar(col.ColumnValue) & "()")
        Next


        Return sb.ToString
    End Function
    Public Shared Function GetKoClassView(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("var " & DT.TableSingularize & "Item = function (data) { ")
        sb.AppendLine("     var self = this;")
        '    var ShippersItem = function (data) {
        '    
        For Each col In DT.ListColumn
            sb.AppendLine("     self." & LowerTheFistChar(col.ColumnValue) & " = ko.observable(data." & LowerTheFistChar(col.ColumnValue) & " );")
        Next
        sb.AppendLine("    self.toJson = function () { return ko.toJSON(self) };")
        sb.AppendLine("};")

        Return sb.ToString
    End Function


    Shared Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function
End Class
