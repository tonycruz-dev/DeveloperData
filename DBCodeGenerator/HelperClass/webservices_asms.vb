Imports DBExtenderLib
Imports System.Text

Public Class webservices_asms
    Public Shared Function MVCControllerTemplate(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder()
        Dim mylastComar
        sb.AppendLine("#Region " & DT.TableValue.QT)
        sb.AppendLine()
        sb.AppendLine()
        sb.AppendLine("    <WebMethod()>")
        sb.AppendLine("    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>")
        sb.AppendLine("    Public Function Get" & DT.TableValue & "Page(id As Integer) As String")
        sb.AppendLine("        Const pageSize As Integer = 10")
        sb.AppendLine("        Dim pagSkip = id * pageSize")

        sb.AppendLine("       Dim Result" & DT.TableValue & " As New Manager" & DT.TableValue & "VM")
        sb.AppendLine("       Result" & DT.TableValue & "." & DT.TableValue & " = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " Order by " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)
        sb.AppendLine("             Select New " & DT.TableValue & "VM With {")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        ." & Col.ColumnValue & " = " & Left(DT.TableName, 3) & "." & Col.ColumnValue & ", ")
        Next
        mylastComar = sb.ToString.LastIndexOf(",")
        'sb.Remove(mylastComar, 1)
        sb.Replace(",", "}).Skip(pagSkip).Take(pageSize).ToList", mylastComar, 1)
        'sb.Append("}).Skip(pagSkip).Take(pageSize).ToList")
        sb.AppendLine()
        sb.AppendLine()
        sb.AppendLine("        Dim " & DT.TableValue & "Count = db." & DT.TableValue & ".Count")
        sb.AppendLine("        Result" & DT.TableValue & ".HasNext = (pagSkip + pageSize < " & DT.TableValue & "Count)")
        sb.AppendLine("        Result" & DT.TableValue & ".HasPrevious = (pagSkip > 0)")
        sb.AppendLine()
        sb.AppendLine("        Dim pgNum = CInt(Math.Ceiling(" & DT.TableValue & "Count / CDbl(pageSize)))")
        sb.AppendLine()
        sb.AppendLine("        Dim Numpg = Enumerable.Range(0, pgNum).ToList")
        sb.AppendLine("        Dim cntpage As Integer = 0")
        sb.AppendLine("        For Each p In Numpg")
        sb.AppendLine("            Dim cp As New PageVM With {.Page = p}")
        sb.AppendLine("            If p = id Then")
        sb.AppendLine("                cp.IsCurrentPage = True")
        sb.AppendLine("                cp.Page = p")
        sb.AppendLine("                cp.PageDisplay = p + 1")
        sb.AppendLine("            Else")
        sb.AppendLine("                cp.IsCurrentPage = False")
        sb.AppendLine("                cp.Page = p")
        sb.AppendLine("                cp.PageDisplay = p + 1")
        sb.AppendLine("            End If")
        sb.AppendLine("            Result" & DT.TableValue & ".TotalPage.Add(cp)")
        sb.AppendLine("            cntpage = cntpage + 1")
        sb.AppendLine("        Next")
        sb.AppendLine("        Dim QueryResults = New JavaScriptSerializer().Serialize(Result" & DT.TableValue & ")")
        sb.AppendLine("        Return QueryResults")
        sb.AppendLine("    End Function")
        sb.AppendLine()
        sb.AppendLine("     <WebMethod()>")
        sb.AppendLine("     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>")
        sb.AppendLine("     Public Function Find" & DT.TableValue & "(id As Integer) As String")
        sb.AppendLine("       Dim Result" & DT.TableValue & " As " & DT.TableValue & "VM = Nothing")
        sb.AppendLine()
        sb.AppendLine("       Result" & DT.TableValue & " = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " where " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = id ")
        sb.AppendLine("             Select New " & DT.TableValue & "VM With {")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        ." & Col.ColumnValue & " = " & Left(DT.TableName, 3) & "." & Col.ColumnValue & ", ")
        Next
        mylastComar = sb.ToString.LastIndexOf(",")
        sb.AppendLine()
        sb.Replace(",", "}).SingleOrDefault", mylastComar, 1)
        sb.AppendLine()
        sb.AppendLine("        Dim QueryResults = New JavaScriptSerializer().Serialize(Result" & DT.TableValue & ")")
        sb.AppendLine("        Return QueryResults")
        sb.AppendLine("    End Function")

        sb.AppendLine()
        sb.AppendLine()
        sb.AppendLine("     <WebMethod()>")
        sb.AppendLine("     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>")
        sb.AppendLine("     Public Function Update" & DT.TableValue & "(")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        " & Col.ColumnValue & " AS String ,")
        Next
        mylastComar = sb.ToString.LastIndexOf(",")
        sb.Replace(",", ") As String", mylastComar, 1)
        sb.AppendLine()
        '                    SupplierID As String, CompanyName As String, ContactName As String,
        '                               ContactTitle As String, Address As String, City As String,
        '                               Region As String, PostalCode As String, Country As String,
        '                               Phone As String, Fax As String, HomePage As String) As String
        sb.AppendLine("       Dim qc = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " where " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = " & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault")
        sb.AppendLine()

        sb.AppendLine("        With qc")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        ." & Col.ColumnValue & " =" & Col.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("        db.SaveChanges()")
        sb.AppendLine("        Dim QueryResults = New JavaScriptSerializer().Serialize(qc)")
        sb.AppendLine("        Return QueryResults")
        sb.AppendLine("    End Function")
        sb.AppendLine()

        sb.AppendLine()
        sb.AppendLine()

        sb.AppendLine("<WebMethod()>")
        sb.AppendLine("<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>")
        sb.AppendLine("    Public Function Create" & DT.TableValue & "() As String")
        sb.AppendLine("        Dim new" & DT.TableValue & " As " & DT.TableValue & " = Nothing")
        sb.AppendLine("        Try")

        sb.Append("        new" & DT.TableValue & " = New " & DT.TableValue & " With {")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("." & Col.ColumnValue & " =" & Col.ColumnValue.QT & ",")
        Next
        mylastComar = sb.ToString.LastIndexOf(",")
        sb.Replace(",", "}", mylastComar, 1)

        sb.AppendLine("            db." & DT.TableValue & ".Add(new" & DT.TableValue & ")")
        sb.AppendLine("            db.SaveChanges()")
        sb.AppendLine("        Catch ex As Exception")

        sb.AppendLine("        End Try")

        sb.AppendLine("        Dim QueryResults = New JavaScriptSerializer().Serialize(new" & DT.TableValue & ")")
        sb.AppendLine("        Return QueryResults")
        sb.AppendLine("    End Function")

        sb.AppendLine("    <WebMethod()>")
        sb.AppendLine("    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>")
        sb.AppendLine("    Public Function Delete" & DT.TableValue & "(id As String) As String")
        ' sb.AppendLine("        Dim qc = (From Sup In db.Suppliers Where Sup.SupplierID = CInt(id)).SingleOrDefault")
        sb.AppendLine("        Dim qc = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " where " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = cint(id)).SingleOrDefault ")

        sb.AppendLine("        db." & DT.TableValue & ".Remove(qc)")
        sb.AppendLine("        db.SaveChanges()")

        sb.AppendLine("       Dim QueryResults = New JavaScriptSerializer().Serialize(" & "{id=id}".QT & ")")
        sb.AppendLine("        Return QueryResults")


        sb.AppendLine("    End Function")

        sb.AppendLine("#End Region")

        Return sb.ToString

    End Function

End Class
