Imports DBExtenderLib
Imports System.Text

Public Class ManageRiaClass
    
    Public Shared Function VBClassRia2010(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String

        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine(" #Region " & """" & DT.TableName & """")
        strWriteModel.AppendLine("Public NotInheritable Class " & DT.TableValue & "Ria")
        Dim count As Integer = 0
        For Each R As ColumnsInfo In DT.ListColumn
            If R.IsPrimary_Key Then
                strWriteModel.AppendLine("    <Key(), _")
                strWriteModel.AppendLine("    Required(ErrorMessage:=" & """" & R.ColumnName.ToProperCase & " is required" & """" & "), ")
                strWriteModel.AppendLine("    Display(Name:=" & """" & R.ColumnName.ToProperCase & """" & " , ")
                strWriteModel.AppendLine("    Description:=" & """" & R.ColumnName.ToProperCase & """" & ", ")
                strWriteModel.AppendLine("    Order:=" & count & ")> ")
                strWriteModel.AppendLine("    Public Property " & R.ColumnValue & "() As " & R.LinqVar)
                strWriteModel.AppendLine()
                count = count + 1
            Else
                If R.IsRequared Then
                    strWriteModel.AppendLine("   <Required(ErrorMessage:=" & """" & R.ColumnName.ToProperCase & " is required" & """" & "), ")
                    strWriteModel.AppendLine("    Display(Name:=" & """" & R.ColumnName.ToProperCase & """" & " , ")
                    strWriteModel.AppendLine("    Description:=" & """" & R.ColumnName.ToProperCase & """" & ", ")
                    strWriteModel.AppendLine("    Order:=" & count & ")> ")
                    strWriteModel.AppendLine("    Public Property " & R.ColumnValue & "() As " & R.LinqVar)
                    strWriteModel.AppendLine()
                    count = count + 1
                Else
                    strWriteModel.AppendLine("    <Display(Name:=" & """" & R.ColumnName.ToProperCase & """" & " , ")
                    strWriteModel.AppendLine("    Description:=" & """" & R.ColumnName.ToProperCase & """" & ", ")
                    strWriteModel.AppendLine("    Order:=" & count & ")> ")
                    strWriteModel.AppendLine("    Public Property " & R.ColumnValue & "() As " & R.LinqVar)
                    strWriteModel.AppendLine()
                    count = count + 1
                End If
            End If

        Next
        Dim ralateTables = DT.GetRalationalTables(DB)
        For Each RT In ralateTables
            strWriteModel.AppendLine("     <Include()> ")
            strWriteModel.AppendLine("     <Association(" & """" & DT.TableValue & "_" & RT.RelateTableValue & """" & ", " & """" & RT.SelectedTableLinqKey & """" & "," & """" & RT.ForeignKey & """" & ")> ")
            strWriteModel.AppendLine("     Public Property " & RT.RelateTableValue & "() As List(Of " & RT.RelateTableValue & "Ria)")
        Next
        strWriteModel.AppendLine()
        Dim FkTables = DT.GetForeignKey
        For Each RT In FkTables
            strWriteModel.AppendLine("     <Include()> ")
            strWriteModel.AppendLine("     <Association(" & """" & DT.TableValue & "_" & RT.RelatedTable & """" & ", " & """" & RT.ColumnName & """" & "," & """" & RT.RelatedColumnName & """" & ", IsForeignKey:=True)> ")
            strWriteModel.AppendLine("     Public Property " & RT.RelatedTable & "() As  " & RT.RelatedTable & "Ria")
            strWriteModel.AppendLine()
        Next
        strWriteModel.Append("End Class")
        strWriteModel.Replace("I D", "ID")
        strWriteModel.AppendLine()
        strWriteModel.AppendLine("#End Region")
        strWriteModel.AppendLine()
        strWriteModel.AppendLine()
        Return strWriteModel.ToString

    End Function


#Region "Ria Server"

    Public Shared Function RiaSelect(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder

        sb.AppendLine(" #Region " & """" & DT.TableName & """")

        sb.AppendLine("Public Function Get" & DT.TableValue & "FromRia() As List(Of " & DT.TableValue & "Ria)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As List(Of " & DT.TableValue & "Ria) = Nothing")

        sb.AppendLine("     Dim db As New " & DBName & "DataContext(GetCnn)")

        sb.AppendLine("     Result" & DT.TableValue & " = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " Order BY " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue & " _")
        Dim sbselect As New StringBuilder("     Select New " & DT.TableValue & "Ria With { _" & vbNewLine)
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
    Public Shared Function RiaUpdate(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("")
        sb.AppendLine(" #Region " & """" & "Update " & DT.TableName & """")
        sb.AppendLine("    Public Sub Update" & DT.TableValue & "Ria(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Ria)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")

        sb.AppendLine()
        sb.AppendLine("     Dim db As New " & DBName & "DataContext(GetCnn)")

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
    Public Shared Function RiaDelete(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("")
        sb.AppendLine(" #Region " & """" & "Delete " & DT.TableName & """")
        sb.AppendLine("    Public Function Delete" & DT.TableValue & "FromRia(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Ria) As Boolean")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As Boolean")
        sb.AppendLine()
        sb.AppendLine("        Dim db As New " & DBName & "DataContext(GetCnn)")

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

    Public Shared Function RiaInsert(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine()
        sb.AppendLine(" #Region " & """" & "Insert " & DT.TableName & """")
        sb.AppendLine("    Public Sub Insert" & DT.TableValue & "FromRia(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & "Ria)")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(ConfigurationManager.ConnectionStrings(""" & "NorthwindConnection""" & ").ConnectionString)")

        sb.AppendLine()
        sb.AppendLine("     Dim db As New " & DBName & "DataContext(GetCnn)")
        sb.AppendLine()

        sb.AppendLine("        Dim " & Left(DT.TableName, 3) & "Info As New " & DT.TableValue & "Info")
        sb.AppendLine("    With " & Left(DT.TableName, 3) & "Info")

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         ." & R.ColumnValue & " =  _" & DT.TableValue & "." & R.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("        db." & DT.TableValue & ".InsertOnSubmit(" & Left(DT.TableValue, 3) & "Info)")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & " Insert " & DT.TableName & """")
        sb.AppendLine()
        Return sb.ToString
    End Function

#End Region


#Region "Ria Server For Class Manager"
    Public Shared Function RiaSelectManager(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder

        Dim colFK = (From c In DT.ListColumn Where c.IsForeign_Key = True Select c)

        'If Not colPk Is Nothing Then
        sb.AppendLine(" #Region " & """" & "Master Details Class " & DT.TableName & """")
        sb.AppendLine()
        sb.AppendLine("Public Function Get" & DT.TableValue & "CollectionFromRia() As List(Of " & DT.TableValue & "Ria)")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As List(Of " & DT.TableValue & "Ria) = Nothing")
        sb.AppendLine("        Dim db As New " & DB.DatabaseName & "DataContext(GetCnn)")

        sb.AppendLine("     Result" & DT.TableValue & " = (From " & Left(DT.TableName, 4) & " In db." & DT.TableValue & " Order BY " & Left(DT.TableValue, 4) & "." & DT.GetPrimaryKey.ColumnValue & " _")
        Dim sbselect As New StringBuilder("     Select New " & DT.TableValue & "Ria With { _" & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            sbselect.AppendLine("         ." & R.ColumnValue & " =  " & Left(DT.TableValue, 4) & "." & R.ColumnValue & ", ")
        Next

        Dim FkTables = DT.GetForeignKey
        Dim count = 1
        For Each fk In FkTables
            If count = FkTables.Count Then
                sbselect.Append(CreateTableFromForeignkey(DT, fk, DB) & ",")
            Else
                sbselect.AppendLine(CreateTableFromForeignkey(DT, fk, DB) & ",")
            End If
            count += 1
        Next

        Dim ralateTables = DT.GetRalationalTables(DB)
        For Each RT In ralateTables
            sbselect.AppendLine("         ." & RT.RelateTableValue & " = (From " & Left(RT.RelateTableValue, 3) & " In db." & RT.RelateTableValue & " Where " & Left(RT.RelateTableValue, 3) & "." & RT.ForeignKey & " = " & Left(DT.TableName, 4) & "." & RT.SelectedTableLinqKey)

            sbselect.AppendLine("                Select New " & RT.RelateTableValue & "Ria With {")
            For Each colrt In RT.RelateTable.ListColumn
                sbselect.AppendLine("                   ." & colrt.ColumnValue & " = " & Left(RT.RelateTableValue, 3) & "." & colrt.ColumnValue & ",")
            Next
            Dim SubmylastComar = sbselect.ToString.LastIndexOf(",")
            Dim SubStrSelect = sbselect.Remove(SubmylastComar, 1).ToString
            SubStrSelect = SubStrSelect & "                    }).ToList"
            sb.Append(SubStrSelect)
        Next
        If ralateTables.Count = 0 Then
            Dim SubmylastComar = sbselect.ToString.LastIndexOf(",")
            Dim SubStrSelect = sbselect.Remove(SubmylastComar, 1).ToString
            sb.Append(sbselect.ToString)
        End If
        sb.AppendLine("}).ToList")
        sb.AppendLine("            Return Result" & DT.TableValue)
        sb.AppendLine("")
        sb.AppendLine("    End Function")
        sb.AppendLine(" #End Region '" & "Select Sub Class" & """" & DT.TableName & """")
        sb.AppendLine()
        sb.AppendLine()
        Return sb.ToString

    End Function

#End Region
    Private Shared Function CreateTableFromForeignkey(ByVal CurrTB As TableNameInfo, ByVal fk As ForeignKeyInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder
        Dim sbselect As New StringBuilder()
        Dim results = (From tb In DB.ListTable Where tb.TableName = fk.RelatedTable).SingleOrDefault
        sbselect.AppendLine("         ." & fk.RelatedTable & "  = (From " & Left(fk.RelatedTable, 3) & " In db." & fk.RelatedTable & " Where " & Left(fk.RelatedTable, 3) & "." & fk.RelatedColumnName & " = " & Left(CurrTB.TableValue, 4) & "." & fk.ColumnName)
        sbselect.AppendLine("                Select New " & fk.RelatedTable & "Ria With {")
        For Each col In results.ListColumn
            sbselect.AppendLine("               ." & col.ColumnValue & "  = " & Left(results.TableValue, 3) & "." & col.ColumnValue & ",")
        Next
        Dim SubmylastComar = sbselect.ToString.LastIndexOf(",")
        Dim SubStrSelect = sbselect.Remove(SubmylastComar, 1).ToString
        SubStrSelect = SubStrSelect & "          }).SingleOrDefault"
        sb.Append(SubStrSelect)
        Return sb.ToString
    End Function
    Public Shared Function CreateDomainServicesClass(ByVal DB As DatabaseNameInfo, ByVal DbLinq As LinqDatabaseNameInfo) As DomainServiceManager
        Dim sb As New StringBuilder
        Dim ds As New DomainServiceManager
        ds.ClassNameSpace = DbLinq.ClassNameSpace
        ds.ConnectionString = DbLinq.ConnectionString
        ds.DomainServiceName = "DomainService" & DbLinq.DatabaseName
        ds.DatabaseName = DbLinq.DatabaseName
        ds.DBPath = DbLinq.DBPath
        ds.ProjectName = DbLinq.ProjectName
        ds.SaveLocation = DbLinq.SaveLocation
        sb.AppendLine(" Imports System")
        sb.AppendLine(" Imports System.Collections.Generic")
        sb.AppendLine(" Imports System.ComponentModel.DataAnnotations")
        sb.AppendLine(" Imports System.ServiceModel.DomainServices.Hosting")
        sb.AppendLine(" Imports System.ServiceModel.DomainServices.Server")
        sb.AppendLine(" Imports System.Web.Profile")
        sb.AppendLine(" Imports System.Web.Security")
        sb.AppendLine(" Namespace " & DbLinq.ClassNameSpace)
        sb.AppendLine(" ''' <summary>")
        sb.AppendLine(" ''' RIA Services Domain Service that exposes methods for performing user")
        sb.AppendLine(" ''' registrations.")
        sb.AppendLine(" ''' </summary>")
        sb.AppendLine("'<EnableClientAccess()>")
        sb.AppendLine("Public Class DomainService" & DB.DatabaseName)
        sb.AppendLine("    Inherits DomainService")

        For Each ctb In DbLinq.ListLinqTable.Where(Function(tb) tb.TableType = "Table")
            sb.AppendLine()
            sb.AppendLine(" #Region " & """" & " Manage " & ctb.TableName & "Ria Select,Insert,Update, Delete" & """")
            Dim FuncLst As New FunctionList
            FuncLst.TableValue = ctb.TableValue
            FuncLst.FunctionSelect = RiaSelect(ctb, DB.DatabaseName)
            sb.AppendLine(FuncLst.FunctionSelect)
            FuncLst.FunctionInsert = RiaInsert(ctb, DB.DatabaseName)
            sb.AppendLine(FuncLst.FunctionInsert)
            FuncLst.FunctionUpdate = RiaUpdate(ctb, DB.DatabaseName)
            sb.AppendLine(FuncLst.FunctionUpdate)
            FuncLst.FunctionDelete = RiaDelete(ctb, DB.DatabaseName)
            sb.AppendLine(FuncLst.FunctionDelete)
            FuncLst.FunctionSelectSubClass = RiaSelectManager(ctb, DB)
            sb.AppendLine(FuncLst.FunctionSelectSubClass)
            sb.AppendLine()
            sb.AppendLine(" #End Region '" & """" & " Manage " & ctb.TableName & "Ria Select,Insert,Update, Delete" & """")
            sb.AppendLine()
            sb.AppendLine()
            ds.ListFunctions.Add(FuncLst)
            ds.ListTable.Add(ctb)
        Next
        For Each ctb In DbLinq.ListLinqTable.Where(Function(tb) tb.TableType = "View")
            sb.AppendLine()
            sb.AppendLine(" #Region " & """" & " Manage " & ctb.TableName & "Ria Select " & """")
            Dim FuncLst As New FunctionList
            FuncLst.TableValue = ctb.TableValue
            FuncLst.FunctionSelect = RiaSelect(ctb, DB.DatabaseName)
            sb.AppendLine(FuncLst.FunctionSelect)
            sb.AppendLine()
            sb.AppendLine(" #End Region '" & """" & " Manage " & ctb.TableName & "Ria Select " & """")
            sb.AppendLine()
            sb.AppendLine()
            ds.ListFunctions.Add(FuncLst)
            ds.ListTable.Add(ctb)
        Next
        sb.AppendLine()
        sb.AppendLine(" #Region " & """" & " Manage Connection Function  " & """")

        sb.AppendLine("Private Function GetCnn() As OleDbConnection)")
        sb.AppendLine("'        Dim cnn As New OleDbConnection(" & """" & DB.ConnectionString & """" & ")")
        sb.AppendLine(" '       Dim cnn As New OleDbConnection(ConfigurationManager.ConnectionStrings(""" & "NorthwindConnection""" & ").ConnectionString)")
        sb.AppendLine("    Dim cnn As New OleDbConnection(My.Settings.Cnn)")
        sb.AppendLine("    Return cnn")
        sb.AppendLine(" End Function")
        sb.AppendLine(" #End Region ")
        sb.AppendLine()
        sb.AppendLine(" End Class")
        ds.DomainServiceContext = sb.ToString
        Return ds
    End Function

    Public Shared Function CreateRiaContext(ByVal DB As DatabaseNameInfo, ByVal DbLinq As LinqDatabaseNameInfo) As RiaClassContent
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("Imports System.ComponentModel")
        strWriteModel.AppendLine("Imports System.ComponentModel.DataAnnotations")
        Dim RiaContext As New RiaClassContent
        RiaContext.ClassNameSpace = DbLinq.ClassNameSpace
        RiaContext.SaveLocation = DbLinq.SaveLocation
        RiaContext.RiaServiceName = "Ria" & DbLinq.DatabaseName
        For Each DT In DbLinq.ListLinqTable
            Dim riatb As New RiaClassInfo
            riatb.ComplexClass = VBClassRia2010(DT, DB)
            strWriteModel.AppendLine(riatb.ComplexClass)
            riatb.Table = DT
            RiaContext.ListRiaClass.Add(riatb)
        Next
        RiaContext.ClassContent = strWriteModel.ToString
        Return RiaContext

    End Function
    Public Shared Function CreateRiaMainContext(ByVal DB As DatabaseNameInfo, ByVal DbLinq As LinqDatabaseNameInfo) As RiaClassContent
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("Imports System.ComponentModel")
        strWriteModel.AppendLine("Imports System.ComponentModel.DataAnnotations")
        Dim RiaContext As New RiaClassContent
        RiaContext.ClassNameSpace = DbLinq.ClassNameSpace
        RiaContext.SaveLocation = DbLinq.SaveLocation
        RiaContext.RiaServiceName = "Ria" & DbLinq.DatabaseName
        For Each DT In DbLinq.ListLinqTable
            Dim riatb As New RiaClassInfo
            riatb.ComplexClass = VBClassRia2010(DT, DB)
            strWriteModel.AppendLine(riatb.ComplexClass)
            riatb.Table = DT
            RiaContext.ListRiaClass.Add(riatb)
        Next
        RiaContext.ClassContent = strWriteModel.ToString
        Return RiaContext
    End Function
End Class
