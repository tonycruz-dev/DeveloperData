Imports DBExtenderLib
Imports System.Text

Public Class ManageInfoClass

    Public Shared Function CreateVBClass2010(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("Public Class " & DT.TableValue & "Info")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.TypeVB)
        Next
        strWriteModel.AppendLine("End Class")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateJSonVBClass(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("Public Class " & LowerTheFistChar(DT.TableSingularize) & "Vm")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & LowerTheFistChar(R.ColumnValue) & " As " & R.TypeVB)
        Next
        strWriteModel.AppendLine("End Class")
        Return strWriteModel.ToString
    End Function
    Public Shared Function InfoSelect(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder

        sb.AppendLine(" #Region " & """" & DT.TableName & """")

        sb.AppendLine("Public Function Get" & DT.TableValue & "() As List(Of " & DT.TableValue & "Info)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As List(Of " & DT.TableValue & "Info) = Nothing")

        sb.AppendLine("     Dim db As New " & DBName & "DataContext")

        sb.AppendLine("     Result" & DT.TableValue & " = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " Order BY " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue & " _")
        Dim sbselect As New StringBuilder("     Select New " & DT.TableValue & "Info With { _" & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            sbselect.AppendLine("         ." & R.ColumnValue & " =  " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
        Next

        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList"
        sb.AppendLine(StrSelect)
        sb.AppendLine("            Return Result" & DT.TableValue)
        sb.AppendLine("")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
        Return sb.ToString

    End Function
    Public Shared Function InfoUpdate(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("")
        sb.AppendLine(" #Region " & """" & "Update " & DT.TableName & """")
        sb.AppendLine("    Public Sub Update" & DT.TableValue & "(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Info)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")

        sb.AppendLine()
        sb.AppendLine("     Dim db As New " & DBName & "DataContext")

        sb.AppendLine("     Dim qc = (From " & Left(DT.TableValue, 3) & " In db." & DT.TableValue & " Where " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = _" & DT.TableValue & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault")
        sb.AppendLine("    With qc")

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         ." & R.ColumnValue & " =  _" & DT.TableValue & "." & R.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("    db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & "Update " & DT.TableName & """")
        sb.AppendLine("")
        Return sb.ToString
    End Function
    Public Shared Function InfoDelete(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("")
        sb.AppendLine(" #Region " & """" & "Delete " & DT.TableName & """")
        sb.AppendLine("    Public Function Delete" & DT.TableValue & "(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Info) As Boolean")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As Boolean")
        sb.AppendLine()
        sb.AppendLine("        Dim db As New " & DBName & "DataContext")

        sb.AppendLine("         Dim Result = (From " & Left(DT.TableValue, 3) & " In db." & DT.TableValue & " Where " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = _" & DT.TableValue & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault")
        sb.AppendLine("         db." & DT.TableValue & ".DeleteOnSubmit(Result)")
        sb.AppendLine("         db.SubmitChanges()")
        sb.AppendLine("         Result" & DT.TableValue & "= true ")
        sb.AppendLine("         Return Result" & DT.TableValue & "")
        sb.AppendLine("      End Function")
        sb.AppendLine("#End Region '" & """" & "Delete " & DT.TableName & """")
        sb.AppendLine("")
        Return sb.ToString
    End Function

    Public Shared Function InfoInsert(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine()
        sb.AppendLine(" #Region " & """" & "Insert " & DT.TableName & """")
        sb.AppendLine("    Public Sub Insert" & DT.TableValue & "(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Info)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(ConfigurationManager.ConnectionStrings(""" & "NorthwindConnection""" & ").ConnectionString)")

        'Dim _Cus As New Customers
        'With CusInfo

        sb.AppendLine()
        sb.AppendLine("    ' Dim db As New " & DBName & "DataContext(GetCnn)")
        sb.AppendLine("     Dim db As New " & DBName & "DataContext")
        sb.AppendLine()

        sb.AppendLine("        Dim _" & Left(DT.TableName, 3) & " As New " & DT.TableValue)
        sb.AppendLine("    With _" & Left(DT.TableName, 3))

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         ." & R.ColumnValue & " =  _" & DT.TableValue & "." & R.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("        db." & DT.TableValue & ".InsertOnSubmit(_" & Left(DT.TableValue, 3) & ")")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & " Insert " & DT.TableName & """")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function ReadFromTextBox(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine()
        sb.AppendLine(" #Region " & """" & "Insert " & DT.TableName & """")
        sb.AppendLine("    Public Sub Insert" & DT.TableValue & "(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Info)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(ConfigurationManager.ConnectionStrings(""" & "NorthwindConnection""" & ").ConnectionString)")

        'Dim _Cus As New Customers
        'With CusInfo

        sb.AppendLine()
        sb.AppendLine("    ' Dim db As New " & DBName & "DataContext(GetCnn)")
        sb.AppendLine("     Dim db As New " & DBName & "DataContext")
        sb.AppendLine()

        sb.AppendLine("        Dim _" & Left(DT.TableName, 3) & " As New " & DT.TableValue)
        sb.AppendLine("    With _" & Left(DT.TableName, 3))

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         " & R.ColumnValue & "TextBox.Text =  _" & DT.TableValue & "." & R.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("        db." & DT.TableValue & ".InsertOnSubmit(_" & Left(DT.TableValue, 3) & ")")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & " Insert " & DT.TableName & """")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function WriteToTextBox(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine()
        sb.AppendLine(" #Region " & """" & "Insert " & DT.TableName & """")
        sb.AppendLine("    Public Sub Insert" & DT.TableValue & "(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Info)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(ConfigurationManager.ConnectionStrings(""" & "NorthwindConnection""" & ").ConnectionString)")

        'Dim _Cus As New Customers
        'With CusInfo

        sb.AppendLine()
        sb.AppendLine("    ' Dim db As New " & DBName & "DataContext(GetCnn)")
        sb.AppendLine("     Dim db As New " & DBName & "DataContext")
        sb.AppendLine()

        sb.AppendLine("        Dim _" & Left(DT.TableName, 3) & " As New " & DT.TableValue)
        sb.AppendLine("    With _" & Left(DT.TableName, 3))

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         " & Left(DT.TableName, 3) & "." & R.ColumnValue & "= " & R.ColumnValue & "TextBox.Text  ")
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("        db." & DT.TableValue & ".InsertOnSubmit(_" & Left(DT.TableValue, 3) & ")")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & " Insert " & DT.TableName & """")
        sb.AppendLine()
        Return sb.ToString
    End Function
End Class
