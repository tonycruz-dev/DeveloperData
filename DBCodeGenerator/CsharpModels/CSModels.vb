Imports DBExtenderLib
Imports System.Text
Public Class CSModels
    Public Shared Function CreateCSClass(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("public class " & DT.TableSingularize & "Vm")
        strWriteModel.AppendLine("{")
        For Each R As ColumnsInfo In DT.ListColumn
            'public int Number { get; set; }
            strWriteModel.AppendLine("    public " & R.VarCSharp & " " & R.ColumnValue & " { get; set;}  ")
        Next
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateDto(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("public class " & DT.TableSingularize & "Dto: ValidatableBindableBase ")

        sb.AppendLine("{")
        sb.AppendLine(" ")
        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("   private  " & R.VarCSharp & " _" & R.ColumnValue & ";")
        Next
        sb.AppendLine(" ")
        For Each R As ColumnsInfo In DT.ListColumn
            '                  public int Id { get => _Id; set => SetProperty(ref _Id, value); }
            sb.AppendLine("   public " & R.VarCSharp & " " & R.ColumnValue & " { get =>" & " _" & R.ColumnValue & "; set => SetProperty(ref" & " _" & R.ColumnValue & " , value); }")
        Next
        sb.AppendLine(" ")
        For Each t In DT.GetMasterTables(DB)
            'private AccountCustomerDto _accountCustomer;
            sb.AppendLine("  private  " & t.RelateTableValue & "Dto _" & t.RelateTableValue & ";")
        Next
        For Each t In DT.GetMasterTables(DB)
            sb.AppendLine("  public virtual  " & t.RelateTableValue & "Dto " & t.RelateTableValue & " { get => _" & t.RelateTableSingularize & "; set => SetProperty(ref _" & t.RelateTableSingularize & ", value); }")
        Next
        ' public virtual AccountCustomerDto AccountCustomer { get => _accountCustomer; set => SetProperty(ref _accountCustomer, value); }
        ' public List<AccountOrderDetailDto> AccountOrderDetails { get => _accountOrderDetails; set => SetProperty(ref _accountOrderDetails , value); }
        ' Dim ralateTables = objTable.GetRalationalTables(_SelectedDatabase)
        For Each t In DT.GetRalationalTables(DB)
            'private AccountCustomerDto _accountCustomer;
            sb.AppendLine("  private  " & t.RelateTableValue & "Dto _" & t.RelateTableValue & ";")
        Next
        For Each t In DT.GetRalationalTables(DB)
            sb.AppendLine(" public List<" & t.RelateTableValue & "Dto> " & t.RelateTableValue & " { Get => _" & t.RelateTableValue & "; Set => SetProperty(ref _" & t.RelateTableValue & " , value); }")
        Next


        sb.AppendLine("}")
        sb.AppendLine(" ")
        Return sb.ToString
    End Function
    Public Shared Function CreateJSonCSClass(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("public class " & LowerTheFistChar(DT.TableSingularize) & "Vm")
        strWriteModel.AppendLine("{")
        For Each R As ColumnsInfo In DT.ListColumn
            'public int Number { get; set; }
            strWriteModel.AppendLine("    public " & R.VarCSharp & " " & LowerTheFistChar(R.ColumnValue) & " { get; set;}  ")
        Next

        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateCSClass_INotifyPropertyChanged(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder("using System.ComponentModel; " & vbNewLine)

        strWriteModel.AppendLine("public class " & DT.TableSingularize & "Vm: INotifyPropertyChanged")
        strWriteModel.AppendLine("{")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("   private  " & R.VarCSharp & " _" & R.ColumnValue & ";")
            'private string customerName = String.Empty
        Next
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("  public " & R.VarCSharp & " " & R.ColumnValue)
            strWriteModel.AppendLine("      {")
            strWriteModel.AppendLine("         get")
            strWriteModel.AppendLine("         {")
            strWriteModel.AppendLine("            return _" & R.ColumnValue & ";")
            strWriteModel.AppendLine("          }")
            strWriteModel.AppendLine("         set ")
            strWriteModel.AppendLine("          {")
            strWriteModel.AppendLine("             if (value != _" & R.ColumnValue & ")")
            strWriteModel.AppendLine("              {")
            strWriteModel.AppendLine("                 _" & R.ColumnValue & " = value; OnPropertyChanged(" & R.ColumnValue.QT & ");")
            strWriteModel.AppendLine("               }")
            strWriteModel.AppendLine("            }")
            strWriteModel.AppendLine("          }")
        Next
        strWriteModel.AppendLine("      protected void OnPropertyChanged(string property)")
        strWriteModel.AppendLine("      {")
        strWriteModel.AppendLine("          if (PropertyChanged != null) ")
        strWriteModel.AppendLine("           {")
        strWriteModel.AppendLine("	             PropertyChanged(this, new PropertyChangedEventArgs(property));")
        strWriteModel.AppendLine("            }")
        strWriteModel.AppendLine("       }")
        strWriteModel.AppendLine("    public event PropertyChangedEventHandler PropertyChanged;")



        strWriteModel.Append("}")
        Return strWriteModel.ToString
    End Function
    Public Shared Function SelecteToViewModel(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("public List<" & DT.TableSingularize & "Vm> Get" & DT.TableSingularize & "Vm()")
        sb.AppendLine("{")
        sb.AppendLine("	List<" & DT.TableSingularize & "Vm> Result" & DT.TableSingularize & " = null;")
        sb.AppendLine("	" & DB.DatabaseName & "Context db = new " & DB.DatabaseName & "Context();")


        sb.AppendLine("	Result" & DT.TableSingularize & " = (from " & Left(DT.TableName, 3) & " in db." & DT.TablePluralize & " orderby " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)

        Dim sbselect As New StringBuilder("     select new " & DT.TableSingularize & "Vm { " & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            If R.VarCSharp = "string" Then
                sbselect.AppendLine("         " & R.ColumnValue & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            Else
                sbselect.AppendLine("         " & R.ColumnValue & " =   (" & R.VarCSharp & ")" & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            End If

        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList();"
        sb.AppendLine(StrSelect)
        sb.AppendLine("	return Result" & DT.TableSingularize & ";")
        sb.AppendLine()
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function SelecteToDataViewModel(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("public List<" & DT.TableSingularize & "Vm> Get" & DT.TableSingularize & "Vm()")
        sb.AppendLine("{")
        sb.AppendLine("	List<" & DT.TableSingularize & "Vm> Result" & DT.TableSingularize & " = null;")
        sb.AppendLine("	" & DB.DatabaseName & "Context db = new " & DB.DatabaseName & "Context();")


        sb.AppendLine("	Result" & DT.TableSingularize & " = (from " & Left(DT.TableName, 3) & " in db." & DT.TablePluralize)

        Dim sbselect As New StringBuilder("     select new " & DT.TableSingularize & "Vm { " & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            If R.VarCSharp = "string" Then
                sbselect.AppendLine("         " & R.ColumnValue & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            Else
                sbselect.AppendLine("         " & R.ColumnValue & " = " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            End If

        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList();"
        sb.AppendLine(StrSelect)
        sb.AppendLine("	return Result" & DT.TableSingularize & ";")
        sb.AppendLine()
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function SelecteJosnToViewModel(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("public List<" & LowerTheFistChar(DT.TableSingularize) & "Vm> Get" & DT.TableSingularize & "Vm()")
        sb.AppendLine("{")
        sb.AppendLine("	List<" & LowerTheFistChar(DT.TableSingularize) & "Vm> Result" & LowerTheFistChar(DT.TableSingularize) & " = null;")
        sb.AppendLine("	" & DB.DatabaseName & "Context db = new " & DB.DatabaseName & "Context();")


        sb.AppendLine("	Result" & LowerTheFistChar(DT.TableSingularize) & " = (from " & Left(DT.TableName, 3) & " in db." & DT.TablePluralize & " orderby " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)

        Dim sbselect As New StringBuilder("     select new " & LowerTheFistChar(DT.TableSingularize) & "Vm { " & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            If R.VarCSharp = "string" Then
                sbselect.AppendLine("         " & LowerTheFistChar(R.ColumnValue) & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            Else
                sbselect.AppendLine("         " & LowerTheFistChar(R.ColumnValue) & " =   (" & R.VarCSharp & ")" & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            End If

        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList();"
        sb.AppendLine(StrSelect)
        sb.AppendLine("	return Result" & LowerTheFistChar(DT.TableSingularize) & ";")
        sb.AppendLine()
        sb.AppendLine("}")
        Return sb.ToString
    End Function

    Public Shared Function RiaSelectManager(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder

        Dim colFK = (From c In DT.ListColumn Where c.IsForeign_Key = True Select c)

        'If Not colPk Is Nothing Then
        sb.AppendLine(" #region " & """" & "Master Details Class " & DT.TableName & """")
        sb.AppendLine()
        sb.AppendLine("public Function Get" & DT.TableValue & "CollectionFromRia() As List(Of " & DT.TableValue & "Ria)")
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

    Shared Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function
End Class
