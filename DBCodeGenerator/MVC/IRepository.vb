Imports DBExtenderLib
Imports System.Text

Public Class IRepository

    Public Shared Function CreateIRepository(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" Public Interface I" & DT.TablePluralize & "Repository(Of Items) : Inherits IRepositoryBase(Of " & DT.TableSingularize & "Vm)")
        sb.AppendLine("    ")
        sb.AppendLine("        Function Get" & DT.TablePluralize & "() As " & DT.TablePluralize & "ManagerVM")
        sb.AppendLine("        Function Get" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ") As " & DT.TableSingularize & "Vm")
        sb.AppendLine("    ")
        sb.AppendLine("   End Interface")
        Return sb.ToString
    End Function
    Public Shared Function CreateIRepositoryBase(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" Public Interface IRepositoryBase(Of T As Class)")
        sb.AppendLine("     Sub Add(Item As T)")
        sb.AppendLine("     Sub Update(Item As T)")
        sb.AppendLine("     Sub RemoveItem(Item As T)")
        sb.AppendLine("     Sub Save()")
        sb.AppendLine(" End Interface")
        Return sb.ToString
    End Function
End Class
