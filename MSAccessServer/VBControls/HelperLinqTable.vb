Imports DBExtenderLib
Imports System.Text
Imports ManagLinqFile

Public Class HelperLinqTable

    Public Shared Function Createtable(ByVal TB As TableNameInfo, ByVal LinqDB As LinqDatabaseNameInfo) As String
        Dim sbTable As New StringBuilder()
        If TableTamplate = "" Then
            Dim tmpLinqTableTemplate As String = Application.StartupPath & "\Templates\LinqTableTemplate.txt"
            Dim LinqTableTemplate As String = TemplateManager.OpenTemplates(tmpLinqTableTemplate)
            TableTamplate = LinqTableTemplate
        End If
        sbTable.AppendLine(TableTamplate)
        sbTable.Replace("[TABLENAME]", TB.TableName)
        sbTable.Replace("[TABLEVALUE]", TB.TableValue)
        sbTable.Replace("[WRITE_LINQ_VAR]", LinqHelperClass.WriteLinqVar(TB, LinqDB))
        sbTable.AppendLine()
        sbTable.Replace("[COLUMNS_CHANGE_Extensibility_Method_Definitions]", LinqHelperClass.Extensibility_Columns_Method_Definitions(TB))
        sbTable.AppendLine()
        sbTable.Replace("[INITIALIZE_LINQ_ENTITY]", LinqHelperClass.WriteLinqInitialize(TB, LinqDB))
        sbTable.Replace("[LINQ_PROPERTIES]", LinqHelperClass.WriteLinqDataColumnsProperties(TB, LinqDB))
        sbTable.Replace("[LINQ_ATTACH_AND_DETACH_ENTITYSETPROPERTIES]", LinqHelperClass.Linq_Attach_and_Detach_Properties(TB, LinqDB))
        Return sbTable.ToString
    End Function
    Public Shared Property TableTamplate As String
    Public Shared Sub CreateLinqDatacontextServicesClass(ByVal DbLinq As LinqDatabaseNameInfo)
        For Each ctb In DbLinq.ListLinqTable.Where(Function(tb) tb.TableType = "Table")
            ctb.TableClass = Createtable(ctb, DbLinq)
            ctb.ClassManager = HelperPartialClass.CreatePartialClass(DbLinq.DatabaseName, ctb)
        Next
        For Each ctb In DbLinq.ListLinqTable.Where(Function(tb) tb.TableType = "View")
            ctb.TableClass = Createtable(ctb, DbLinq)
            'ctb.ClassManager = HelperPartialClass.CreatePartialClass(DbLinq.DatabaseName, ctb)
        Next
    End Sub


    'LinqDB
End Class
