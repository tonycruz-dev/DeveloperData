Imports System.Text
Imports DBExtenderLib
Imports Newtonsoft.Json
Imports System.Xml
Imports Newtonsoft.Json.Serialization

Public Class HelpClass


#Region "Oledb Actions"
    Public Shared Function getClassModelList(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" Public Shared Function Get" & DT.TableValue & "() As List(Of " & DT.TableName & ")")
        sb.AppendLine("    Dim sqlCmd As New OleDbCommand")
        sb.AppendLine("    SetCommandType(sqlCmd, CommandType.Text, " & """" & SQLCodeGen.CreateSelectCommand(DT) & """)")
        sb.AppendLine("    Dim List[TABLENAME] As New List(Of [TABLENAME])")
        sb.AppendLine("    Dim cn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")
        sb.AppendLine("    Try")
        sb.AppendLine("        sqlCmd.Connection = cn")
        sb.AppendLine("        cn.Open()")
        sb.AppendLine("        Dim returnData As OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)")
        sb.AppendLine("")
        sb.AppendLine("        While returnData.Read()")
        sb.AppendLine(" Dim _[TABLENAME] As New [TABLENAME] With {")
        Dim sbConv As New StringBuilder
        For Each r In DT.ListColumn
            sbConv.AppendLine("." & r.ColumnValue & "  = " & r.GetConverterType & ",")
        Next
        Dim mylastComar = sbConv.ToString.LastIndexOf(",")
        sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "}")
        sb.AppendLine("            List[TABLENAME].Add(_[TABLENAME])")
        sb.AppendLine("        End While")
        sb.AppendLine("        Return List[TABLENAME]")
        sb.AppendLine("    Finally")
        sb.AppendLine("        cn.Dispose()")
        sb.AppendLine("    End Try")
        sb.AppendLine("    Return List[TABLENAME]")
        sb.AppendLine("End Function")
        sb.Replace("[TABLENAME]", DT.Name)
        sb.AppendLine("")
        sb.AppendLine("Private Shared Sub SetCommandType(ByVal sqlCmd As OleDbCommand, ByVal cmdType As CommandType, ByVal cmdText As String)")
        sb.AppendLine("    sqlCmd.CommandType = cmdType")
        sb.AppendLine("    sqlCmd.CommandText = cmdText")
        sb.AppendLine("End Sub 'SetCommandType")
        sb.AppendLine("Public Shared Function ReturnEmptyStringIfDbNull(ByRef sqlVar As Object)")
        sb.AppendLine("    Dim ret As String")
        sb.AppendLine("    If TypeOf sqlVar Is DBNull Then")
        sb.AppendLine("        ret = String.Empty")
        sb.AppendLine("    Else")
        sb.AppendLine("        ret = CStr(sqlVar)")
        sb.AppendLine("    End If")
        sb.AppendLine("    Return ret")
        sb.AppendLine("End Function")
        sb.AppendLine("Public Shared Function ReturnEmptyStringIfNull(ByRef var As String)")
        sb.AppendLine("    Dim ret As String")
        sb.AppendLine("    If var Is Nothing Then")
        sb.AppendLine("        ret = String.Empty")
        sb.AppendLine("    Else")
        sb.AppendLine("        ret = var")
        sb.AppendLine("")
        sb.AppendLine("    End If")
        sb.AppendLine("    Return ret")
        sb.AppendLine("End Function")
        Return sb.ToString
    End Function

#End Region

    Public Shared Function GetTestingXMLListCollection(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandTop10(DT)
        Dim dsTB As DataTable = Nothing

        If DT.Database.IsAccess2007 Or DT.Database.IsAccess2003 Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
        End If

        sb.AppendLine("  Public Shared Function TestXMLGet[TABLENAME]() As List(Of [TABLENAME])")
        sb.AppendLine("    Dim xmldoc = <[TABLENAME]List>")

        For Each r In dsTB.Rows
            sb.AppendLine("         <" & DT.Name & ">")
            For Each col In DT.ListColumn
                sb.AppendLine("     <" & col.ColumnValue & ">" & Trim(col.GetTypeDataToXmlFormat(r)) & "</" & col.ColumnValue & ">")
            Next
            sb.AppendLine("         </" & DT.Name & ">")
        Next
        sb.AppendLine("       </[TABLENAME]List>")

        Dim colcount As Integer = 0
        sb.AppendLine("")
        Dim sbList As New StringBuilder
        sbList.AppendLine("Dim List[TABLENAME] = (From xe In xmldoc.Elements  ")
        sbList.AppendLine("               Select New [TABLENAME] With {")

        For Each col In DT.ListColumn
            sbList.AppendLine("                     ." & col.ColumnValue & " = " & col.CopyDataFromXml & ",")
        Next
        Dim mylastComar = sbList.ToString.LastIndexOf(",")
        sb.AppendLine(sbList.Remove(mylastComar, 1).ToString & "}).ToList")

        sb.AppendLine("    Return List[TABLENAME]")
        sb.AppendLine("End Function")
        Return sb.ToString
    End Function

    Public Shared Function GetTestingListCollection(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandTop10(DT)
        Dim dsTB As DataTable = Nothing

        If DT.Database.IsAccess2007 Or DT.Database.IsAccess2003 Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
        End If
        sb.AppendLine("Public Shared Function TestList[TABLENAME]() As List(Of [TABLENAME])")
        sb.AppendLine("    Dim List[TABLENAME] As New List(Of [TABLENAME])")
        For Each r In dsTB.Rows
            sb.Append("    List[TABLENAME].Add(New [TABLENAME] With {")
            Dim sbConv As New StringBuilder
            For Each col In DT.ListColumn
                sbConv.Append("." & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "})")
        Next
        sb.AppendLine(" Return List[TABLENAME]")

        sb.AppendLine("End Function")
        Return sb.ToString
    End Function
    Public Shared Function GetTestingListCollectionCS(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandTop10(DT)
        Dim dsTB As DataTable = Nothing
        If DT.Database.IsAccess2007 Or DT.Database.IsAccess2003 Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
        End If

        ' public static List<Customers> TestListCustomers()
        sb.AppendLine("public static List<" & DT.TableSingularize & "> TestList" & DT.TableSingularize & "()")
        sb.AppendLine("  {")
        sb.AppendLine("    List<" & DT.TableSingularize & "> List" & DT.TableSingularize & " = new List<" & DT.TableSingularize & ">();")
        'List<Customers> ListCustomers = new List<Customers>();
        For Each r In dsTB.Rows
            sb.Append("    List" & DT.TableSingularize & ".Add(new " & DT.TableSingularize & "  {")
            Dim sbConv As New StringBuilder
            For Each col In DT.ListColumn
                sbConv.Append(" " & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "});")
        Next
        sb.AppendLine(" return List" & DT.TableSingularize & " ;")

        sb.AppendLine(" }")
        Return sb.ToString
    End Function
    Public Shared Function GetFullListCollection(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT)
        Dim dsTB As DataTable = Nothing

        dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)

        sb.AppendLine("Public Shared Function TestList[TABLENAME]() As List(Of [TABLENAME])")
        sb.AppendLine("    Dim List[TABLENAME] As New List(Of [TABLENAME])")
        For Each r In dsTB.Rows
            sb.Append("    List[TABLENAME].Add(New [TABLENAME] With {")
            Dim sbConv As New StringBuilder
            For Each col In DT.ListColumn
                sbConv.Append("." & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "})")
        Next
        sb.AppendLine(" Return List[TABLENAME]")

        sb.AppendLine("End Function")
        Return sb.ToString
    End Function
    Public Shared Function GetFullListCollection(ByVal DT As TableNameInfo, colunList As List(Of ColumnsInfo)) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT, colunList)
        Dim dsTB As DataTable = Nothing

        dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Dim results = (From item In dsTB.AsEnumerable).ToList

        sb.AppendLine("Public Shared Function TestList[TABLENAME]() As List(Of [TABLENAME])")
        sb.AppendLine("    Dim List[TABLENAME] As New List(Of [TABLENAME])")
        For Each r In dsTB.Rows
            sb.Append("    List[TABLENAME].Add(New [TABLENAME] With {")
            Dim sbConv As New StringBuilder
            For Each col In colunList
                sbConv.Append("." & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "})")
        Next
        sb.AppendLine(" Return List[TABLENAME]")

        sb.AppendLine("End Function")
        Return sb.ToString
    End Function
    Public Shared Function GetInsertListCollection(ByVal DT As TableNameInfo, colunList As List(Of ColumnsInfo), WhereClose As String) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT, colunList, WhereClose)
        Dim dsTB As DataTable = Nothing

        dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Dim results = (From item In dsTB.AsEnumerable).ToList
      
        Dim sbInsert As New StringBuilder("Insert into [" & DT.TableName & "](")
        For Each col In colunList
            sbInsert.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbInsert.ToString.LastIndexOf(",")
        sbInsert.Remove(mylastComar, 1)
        sbInsert.Append(")")
        sbInsert.AppendLine()
        For Each r In dsTB.Rows

            Dim sbValues As New StringBuilder("Values(")
            For Each col In colunList
                sbValues.Append(col.GetSqlTypeData(r) & ",")
            Next
            Dim lastValueComar = sbValues.ToString.LastIndexOf(",")
            sb.AppendLine(sbInsert.ToString & sbValues.Remove(lastValueComar, 1).ToString & ")")
        Next
        Return sb.ToString
    End Function


    Public Shared Function GetFullListJsonCollection(ByVal DT As TableNameInfo, colunList As List(Of ColumnsInfo)) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT, colunList)
        Dim dsTB As DataTable = Nothing

        ' dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Dim doc As New XmlDocument()
        doc.LoadXml(dsHelper.GetDataSet(DT.StrConnection, sqlstr, DT.Database.DatabaseName, DT.TableName).GetXml())

        Dim json = JsonConvert.SerializeXmlNode(doc)


        Return json.ToString
    End Function
    Public Shared Function GetFullSqliteListCollection(ByVal DT As TableNameInfo, DatabaseName As String) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT)
        Dim dsTB As DataTable = Nothing

        dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)


        sb.AppendLine(" public class " & DT.TableValue)
        sb.AppendLine("    {")
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        sb.AppendLine("    [PrimaryKey, AutoIncrement]")
        sb.AppendLine("    public int " & colPk.ColumnValue & " {get;set; }")
        Dim col = (From co In DT.ListColumn Where co.IsPrimary_Key = False Select co).ToList

        For Each _col In col
            If _col.TypeVB = "String" Then
                sb.AppendLine("    [MaxLength(" & _col.Size & ")]")
                sb.AppendLine("    public string " & _col.ColumnValue & " {get;set;}")
                sb.AppendLine()
            Else
                sb.AppendLine("    public " & _col.LinqVarCSharp & " " & _col.ColumnValue & " {get;set;}")
                sb.AppendLine()
            End If
        Next
        sb.AppendLine("    }")

        sb.AppendLine("")

        For Each r In dsTB.Rows
            sb.Append(DatabaseName & "Context.Add" & DT.TableValue & " (_db, new " & DT.TableValue & "{")
            Dim sbConv As New StringBuilder
            For Each colums In DT.ListColumn
                'sbConv.Append("." & colums.ColumnValue & "  = " & colums.GetTypeData(r) & ",")
                sbConv.Append(colums.ColumnValue & " = " & colums.GetTypeData(r) & ", ")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "});")
        Next
        sb.AppendLine()
        sb.AppendLine()


        sb.AppendLine("  public List<" & DT.TableValue & "> Query" & DT.TableValue & "()")
        sb.AppendLine("{")
        sb.AppendLine("    return (from q in Table<" & DT.TableValue & ">() orderby q." & colPk.ColumnValue)
        sb.AppendLine("                select q).ToList();")
        sb.AppendLine("}")
        sb.AppendLine()
        sb.AppendLine("public " & DT.TableValue & " QueryByID(Int32 id)")
        sb.AppendLine("{")
        sb.AppendLine("     return (from q in Table<" & DT.TableValue & ">()")
        sb.AppendLine("            where          q." & colPk.ColumnValue & "== id")
        sb.AppendLine("            select c).FirstOrDefault(); ")
        sb.AppendLine("}")
        sb.AppendLine()
        sb.AppendLine("  public static void Add" & DT.TableValue & "(SQLiteConnection db, " & DT.TableValue & " item)")
        sb.AppendLine("{")
        sb.AppendLine("   db.Insert(item);")
        sb.AppendLine("}")
        sb.AppendLine()
        sb.AppendLine("public static void Update" & DT.TableValue & "(SQLiteConnection db, " & DT.TableValue & " item)")
        sb.AppendLine("{")
        sb.AppendLine("    db.Update(item); ")
        sb.AppendLine("}")
        sb.AppendLine()
        sb.AppendLine("public static void Delete" & DT.TableValue & "(SQLiteConnection db, " & DT.TableValue & " item)")
        sb.AppendLine("{")
        sb.AppendLine("    db.Delete(item);")
        sb.AppendLine("}")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function GetXMLToJSON(ByVal DT As TableNameInfo, colunList As List(Of ColumnsInfo)) As String
        Dim sb As New StringBuilder()
        ' Dim sqlstr As String = SQLCodeGen.CreateSelectCommandTop10(DT)
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT, colunList)
        Dim dsTB As DataTable = Nothing

        If DT.StrConnection.Contains("Microsoft.ACE.OLEDB") Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
        End If
        
        Dim json = JsonConvert.SerializeObject(dsTB, Newtonsoft.Json.Formatting.Indented, New JsonSerializerSettings With {.ContractResolver = New CamelCasePropertyNamesContractResolver()})


        Return json
    End Function
    Shared Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function
    Public Shared Function CreateSQLTable(ByVal DT As TableNameInfo) As String
        Dim Sp_SelectAll As New StringBuilder()
        Sp_SelectAll.Append("""" & " CREATE TABLE [" & DT.TableName & "](" & vbNewLine)
        Dim R As ColumnsInfo
        Dim RecNum As Integer = 0
        For Each R In DT.ListColumn
            If RecNum = 0 Then
                Sp_SelectAll.Append(R.ColumnName & " " & GetTypeData(R) & ",")
            ElseIf RecNum = DT.ListColumn.Count - 1 Then
                Sp_SelectAll.Append(R.ColumnName & " " & GetTypeData(R) & ")" & """" & ",")
            Else
                Sp_SelectAll.Append(R.ColumnName & " " & GetTypeData(R) & ",")
            End If
            RecNum += 1
        Next
        Return Sp_SelectAll.ToString

    End Function
    Public Shared Function GetListDBMenus(ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        For Each tb In DB.ListTable
            sb.AppendLine("  <Rectangle x:Name=" & """" & "Divider" & tb.TableName & """" & "   Style=" & """{StaticResource DividerStyle}""" & "/>")
            sb.AppendLine("      <HyperlinkButton x:Name=" & """" & "Link" & tb.TableValue & """" & " Style=" & """{StaticResource LinkStyle} """)
            sb.AppendLine("           NavigateUri=" & """" & "/" & tb.TableValue & """" & " TargetName=" & """" & "ContentFrame" & """" & "  Content=" & """{Binding Path=ApplicationStrings." & tb.TableValue & " , Source={StaticResource ResourceWrapper}}" & """" & " />")
            sb.AppendLine()
        Next
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function GenarateSqlInsertStatement(ByVal TB As TableNameInfo, ByVal myDataTable As DataTable) As String
        Dim StrGetHead As String = GenerateHeader(TB)
        Dim strResults As String = GenerateInsert(TB, StrGetHead, myDataTable)
        Return strResults
    End Function

    Private Shared Function GenerateHeader(ByVal TB As TableNameInfo) As String
        Dim sHeader As New StringBuilder("""" & "INSERT INTO [" & TB.TableName & "] (")
        Dim RecNum As Integer = 0
        For Each Col As ColumnsInfo In TB.ListColumn
            If RecNum = TB.ListColumn.Count - 1 Then
                sHeader.Append("" & Col.ColumnName & "")
            Else
                sHeader.Append("" & Col.ColumnName & "" & ",")
            End If
            RecNum += 1
        Next
        sHeader.Append(")")
        Return sHeader.ToString
    End Function
    Private Shared Function GenerateInsert(ByVal TB As TableNameInfo, ByVal strHeder As String, ByVal TbRows As DataTable) As String
        Dim strResults As New StringBuilder()

        For Each r As DataRow In TbRows.Rows
            Dim sCurRow As String = " Values ("
            Dim RecNum As Integer = 0
            For Each col As ColumnsInfo In TB.ListColumn
                Dim AllColumn As String = GetTypeData(col, r)
                If RecNum = TB.ListColumn.Count - 1 Then
                    sCurRow &= AllColumn
                Else
                    sCurRow &= AllColumn & ","
                End If
                RecNum += 1
            Next
            sCurRow &= ")" & """" & ","
            strResults.AppendLine(strHeder & sCurRow)
        Next
        Return strResults.ToString
    End Function
    Private Shared Function GetTypeData(ByVal R As ColumnsInfo, ByVal dr As DataRow) As String
        'varchar(30)
        Select Case LCase(R.TypeSQL)
            Case "char", "nchar", "nvarchar", "ntext"
                If dr.IsNull(R.ColumnName) Then
                    Return "''"
                Else
                    Return " '" & Replace(dr(R.ColumnName), "'", "''") & "'"
                End If


            Case "smallint", "int", "int identity", "real", "money", "numeric", "bit"
                If dr.IsNull(R.ColumnName) Then
                    Return 0
                Else
                    Return dr(R.ColumnName)
                End If


            Case "datetime"
                If dr.IsNull(R.ColumnName) Then
                    Return "''"
                Else
                    Dim DateVal As Date = dr(R.ColumnName)
                    Return "'" & Format(DateVal, "yyyy-MM-dd") & "'"
                End If

            Case "smalldatetime"
                If dr.IsNull(R.ColumnName) Then
                    Return "''"
                Else
                    Return "'" & Format(dr(R.ColumnName), "yyyy-MM-dd") & "'"
                End If


            Case "image"
                Dim myPic() As Byte
                myPic = dr(R.ColumnName)
                Dim MyStr As String = ""
                ' MyStr = ByteToString(myPic)
                Return MyStr

        End Select

        Return " '" & dr(R.ColumnName) & "'"
    End Function
    Private Shared Function GetTypeData(ByVal R As ColumnsInfo) As String
        'varchar(30)
        Select Case R.TypeSQL.ToLower
            Case "char"
                Return " char(" & R.Size & ")"
            Case "nchar"
                Return " nchar(" & R.Size & ")"
            Case "nvarchar"
                Return "nvarchar(" & R.Size & ")"
            Case "smallint"
                Return "int".ToUpper
            Case "int"
                Return "int".ToUpper

            Case "int identity"
                Return "int".ToUpper
            Case "real"
                Return "real"
            Case "money"
                Return "DECIMAL(10,5)"
            Case "numeric"
                Return "numeric".ToUpper
            Case "bit"
                Return "BOOLEAN"
            Case "smalldatetime"
                Return "DATETIME"
            Case "ntext"
                Return "ntext".ToUpper
            Case "image"
                Return "BLOB"
        End Select

        Return "nvarchar(50)"
    End Function
End Class
