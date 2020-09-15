Imports DBExtenderLib
Imports System.Text

Public Class DataRepository

    Public Shared Function MVCDataRepository(db As DatabaseNameInfo, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("Public Class " & DT.TablePluralize & "Repository")
        sb.AppendLine("    Implements I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm)")
        sb.AppendLine("    Private db As New  " & db.DatabaseName & "DataContext")

        sb.AppendLine(RepositoryGetList(DT))
        sb.AppendLine(RepositoryGetItem(DT))
        sb.AppendLine(RepositoryAddItem(DT))
        sb.AppendLine(RepositoryDelete(DT))
        sb.AppendLine(RepositorySave(DT))
        sb.AppendLine(RepositoryUpdate(DT))
        sb.AppendLine("End Class")
        Return sb.ToString

    End Function

    Public Shared Function RepositoryGetList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("  Public Function Get" & DT.TablePluralize & "() As " & DT.TablePluralize & "ManagerVM Implements I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm).Get" & DT.TablePluralize)

        sb.AppendLine("      Dim results As New " & DT.TablePluralize & "ManagerVM")
        sb.AppendLine("        results." & DT.TablePluralize.LowerTheFistChar & " = (From " & Left(DT.TableValue, 3) & " In db." & DT.TablePluralize & " Order By " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " Select New " & DT.TableSingularize.LowerTheFistChar & "Vm With { _")
        Dim sbselect As New StringBuilder
        For Each col In DT.ListColumn
            sbselect.AppendLine("            ." & col.ColumnValue.LowerTheFistChar & "= " & Left(DT.TableValue, 3) & "." & col.ColumnValue & ",")
        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList()"
        sb.AppendLine(StrSelect)
        sb.AppendLine("            Return results")
        sb.AppendLine(" End Function")
        Return sb.ToString
    End Function
    Public Shared Function RepositoryGetItem(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("    Public Function Get" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ") As " & DT.TableSingularize & "Vm Implements I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm).Get" & DT.TableSingularize)
        sb.AppendLine("        Dim result As " & DT.TableSingularize & " = db." & DT.TablePluralize & ".Where(Function(_" & Left(DT.TableValue, 3) & ") _" & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = id).SingleOrDefault()")

        sb.AppendLine()
        sb.AppendLine("         If IsNothing(result) Then")
        sb.AppendLine("             Return Nothing")
        sb.AppendLine("         End If")
        sb.AppendLine("        Dim Vm = New " & DT.TableSingularize & "Vm() With {")
        Dim sbselect As New StringBuilder
        For Each col In DT.ListColumn
            sbselect.AppendLine("            ." & col.ColumnValue.LowerTheFistChar & "= result." & col.ColumnValue & ",")
        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }"
        sb.AppendLine(StrSelect)
        sb.AppendLine("   Return Vm")
        sb.AppendLine(" End Sub")
        Return sb.ToString
    End Function
    Public Shared Function RepositoryAddItem(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("Public Sub Add(Item As " & DT.TableSingularize & "Vm) Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).Add")

        sb.AppendLine("        Dim " & Left(DT.TableValue, 3) & " As New " & DT.TableSingularize & "()")
        For Each col In DT.ListColumn
            sb.AppendLine("        " & Left(DT.TableValue, 3) & "." & col.ColumnValue & " = Item." & col.ColumnValue.LowerTheFistChar)
        Next
        sb.AppendLine("        db." & DT.TablePluralize & ".InsertOnSubmit(" & Left(DT.TableValue, 3) & ")")

        sb.AppendLine(" End Sub")
        Return sb.ToString
    End Function
    Public Shared Function RepositoryDelete(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub RemoveItem(Item As " & DT.TableSingularize & "Vm) Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).RemoveItem")
        sb.AppendLine("        Dim result As " & DT.TableSingularize & " = db." & DT.TablePluralize & ".Where(Function(_" & Left(DT.TableValue, 3) & ") _" & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = Item" & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault()")
        sb.AppendLine("        db." & DT.TablePluralize & ".DeleteOnSubmit(result)")

        sb.AppendLine("    End Sub")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function RepositorySave(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub Save() Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).Save")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function RepositoryUpdate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub Update(Item As " & DT.TableSingularize & "Vm) Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).Update")
        sb.AppendLine("        Dim result As " & DT.TableSingularize & " = db." & DT.TablePluralize & ".Where(Function(_" & Left(DT.TableValue, 3) & ") _" & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = Item" & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault()")

        For Each col In DT.ListColumn
            sb.AppendLine("        result." & col.ColumnValue & " = Item." & col.ColumnValue.LowerTheFistChar)
        Next
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        Return sb.ToString
    End Function


   

End Class
