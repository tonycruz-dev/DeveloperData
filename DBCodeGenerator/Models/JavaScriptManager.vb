Imports DBExtenderLib
Imports System.Text

Public Class JavaScriptManager
    Public Shared Function CreateFormViewEdit(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("Public Class " & DT.TableValue & "Info")
        sb.AppendLine("function " & DT.TableValue & "Item(data) {") '     
        sb.AppendLine("    var self = this;")
        sb.AppendLine("   data = data || {};")

        sb.AppendLine("   // Persisted properties")
        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("  self." & R.ColumnValue & " = ko.observable(data." & R.ColumnValue & "); ")
        Next
       

        sb.AppendLine("   // Non-persisted properties")
        sb.AppendLine("   self.errorMessage = ko.observable();")

        sb.AppendLine("   saveChanges = function () {")
        sb.AppendLine("   return datacontext.saveChanged" & DT.TableValue & "Item(self);")
        sb.AppendLine("   };")

        sb.AppendLine("   // Auto-save when these properties change")
        sb.AppendLine("  // self.isDone.subscribe(saveChanges);")
        sb.AppendLine("  // self.title.subscribe(saveChanges);")
        sb.AppendLine("  self.toJson = function () { return ko.toJSON(self) };")
        sb.AppendLine("  };")

        Return sb.ToString
    End Function


End Class
