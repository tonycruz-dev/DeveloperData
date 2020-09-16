Imports DBExtenderLib
Imports System.Text

Public Class CSClassManager

    Public Shared Function CreateCSClass2010(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder
        strWriteModel.AppendLine(" public class " & DT.TableValue)
        strWriteModel.AppendLine("{")
        strWriteModel.Append(CsharpWriteautoimplentProperties(DT))
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateCSClass2010Serializable(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder
        strWriteModel.AppendLine("[Serializable()] public class " & DT.TableValue)
        strWriteModel.AppendLine("{")
        strWriteModel.Append(CsharpWriteautoimplentProperties(DT))
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
    Private Shared Function CsharpWriteautoimplentProperties(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder
        For Each R As ColumnsInfo In DT.ListColumn
            If R.IsRequared Or R.LinqVar.ToLower = "string" Then
                strWriteModel.AppendLine("    public " & R.VarCSharp & " " & R.ColumnValue & " { get;set;} ")
            Else
                strWriteModel.AppendLine("    public " & R.VarCSharp & "? " & R.ColumnValue & " { get;set;} ")
            End If

        Next
        Return strWriteModel.ToString
    End Function
    Public Shared Function CreateCSClass2010_INotifyPropertyChanged(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder("using System.ComponentModel; " & vbNewLine)

        strWriteModel.AppendLine("public class " & DT.TableName & ": INotifyPropertyChanged")
        strWriteModel.AppendLine("      {")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    private  " & R.VarCSharp & " _" & R.ColumnValue & ";")
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
        strWriteModel.AppendLine("        {")
        strWriteModel.AppendLine("          if (PropertyChanged != null) ")
        strWriteModel.AppendLine("        {")
        strWriteModel.AppendLine("	         PropertyChanged(this, new PropertyChangedEventArgs(property));")
        strWriteModel.AppendLine("          }")
        strWriteModel.AppendLine("    }")
        strWriteModel.AppendLine(" public event PropertyChangedEventHandler PropertyChanged;")

      

        strWriteModel.Append("}")
        Return strWriteModel.ToString
    End Function
    Public Shared Function CSClass2010SampleData(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.Append("public class " & DT.TableSingularize)
        strWriteModel.AppendLine("{")
        strWriteModel.AppendLine(CsharpWriteautoimplentProperties(DT))

        strWriteModel.AppendLine(CSClass2010CollectionSample(DT))
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CSClassSeedData(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.Append("public class " & DT.TableSingularize)
        strWriteModel.AppendLine("{")
        strWriteModel.AppendLine(CsharpWriteautoimplentProperties(DT))

        strWriteModel.AppendLine(CSClass2010CollectionSample(DT))
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CSClass2010CollectionSample(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.AppendLine(HelpClass.GetTestingListCollectionCS(DT))
        strWriteModel.Replace("[TABLENAME]", DT.TableSingularize)
        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateCSClass2016_INotifyPropertyChanged(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder("using System.ComponentModel; " & vbNewLine)

        strWriteModel.AppendLine("public class " & DT.TableName & "Info: ViewModelBase")
        strWriteModel.AppendLine("      {")

        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    private  " & R.LinqVarCSharp & " _" & R.ColumnValue & ";")
            strWriteModel.AppendLine("    public " & R.LinqVarCSharp & " " & R.ColumnValue)
            strWriteModel.AppendLine("      {")
            strWriteModel.AppendLine("         get {return _" & R.ColumnValue & ";}")

            strWriteModel.AppendLine("         set ")
            strWriteModel.AppendLine("          {")
            strWriteModel.AppendLine("             if (value.Equals( _" & R.ColumnValue & ")) return;")
            strWriteModel.AppendLine("              { _" & R.ColumnValue & " = value;")
            strWriteModel.AppendLine("                  OnPropertyChanged();")
            strWriteModel.AppendLine("               }")
            strWriteModel.AppendLine("            }")
            strWriteModel.AppendLine("     }")
        Next

        strWriteModel.Append("}")
        Return strWriteModel.ToString
    End Function
    Public Shared Function CSClassRead(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        'var landlord = New Landlord
        strWriteModel.AppendLine("{")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("     " & R.ColumnValue & " = request." & R.ColumnValue & ",")
        Next
        strWriteModel.AppendLine("}")


        Return strWriteModel.ToString

    End Function
    Public Shared Function CSClassUpdate(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        'var landlord = New Landlord
        strWriteModel.AppendLine("{")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("     " & DT.TableSingularize.LowerTheFistChar & "." & R.ColumnValue & " = request." & R.ColumnValue & " ?? " & DT.TableSingularize.LowerTheFistChar & "." & R.ColumnValue & ";")
        Next
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CSClassToJson(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("{")
        For Each R As ColumnsInfo In DT.ListColumn
            If (R.TypeVB = "Integer") Then
                strWriteModel.AppendLine(R.ColumnValue.LowerTheFistChar.QT & " : " & 1 & ",")
            ElseIf (R.TypeVB = "Decimal") Then
                strWriteModel.AppendLine(R.ColumnValue.LowerTheFistChar.QT & " : " & 100.0 & ",")
            ElseIf (R.TypeVB = "Date") Then
                ' "2019-09-20T00:00:00"
                Dim formatedDate = Format(Date.Now, "yyyy-MM-dd")
                strWriteModel.AppendLine(R.ColumnValue.LowerTheFistChar.QT & " : " & (formatedDate & "T00:00:00").QT & ",")
            Else
                strWriteModel.AppendLine(R.ColumnValue.LowerTheFistChar.QT & " : " & "".QT & ",")
            End If
        Next
        strWriteModel.AppendLine("}")
        Return strWriteModel.ToString

    End Function
End Class
