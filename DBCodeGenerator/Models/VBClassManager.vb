Imports DBExtenderLib
Imports System.Text

Public Class VBClassManager

    Public Shared Function CreateVBClass2010(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine("Public Class " & DT.TableSingularize)
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.TypeVB)
        Next
        strWriteModel.AppendLine("End Class")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateVBClass2010Serializable(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder
        strWriteModel.AppendLine("<Serializable()> Public Class " & DT.TableSingularize)
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & "() As " & R.TypeVB)
        Next
        strWriteModel.AppendLine("End Class")
        Return strWriteModel.ToString

    End Function
    Public Shared Function CreateVBClass2010_INotifyPropertyChanged(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder("Imports System.ComponentModel " & vbNewLine)

        strWriteModel.AppendLine("<Serializable()>")
        strWriteModel.AppendLine("Public Class " & DT.TableSingularize)
        strWriteModel.AppendLine("    Implements INotifyPropertyChanged")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Private  _" & R.ColumnValue & " As " & R.TypeVB)
        Next
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.TypeVB)
            strWriteModel.AppendLine("        Get")
            strWriteModel.AppendLine("            Return _" & R.ColumnValue)
            strWriteModel.AppendLine("        End Get")
            strWriteModel.AppendLine("        Set(ByVal Value As " & R.TypeVB & ")")
            strWriteModel.AppendLine("            _" & R.ColumnName & " = Value")
            strWriteModel.AppendLine("        OnPropertyChanged(" & """" & R.ColumnValue & """" & ")")
            strWriteModel.AppendLine("        End Set")
            strWriteModel.AppendLine("    End Property")
        Next
        strWriteModel.AppendLine("Protected Sub OnPropertyChanged(ByVal [property] As String)")
        strWriteModel.AppendLine("    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs([property]))")
        strWriteModel.AppendLine("End Sub")
        strWriteModel.AppendLine(" Public Event PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged")

        strWriteModel.Append("End Class")
        Return strWriteModel.ToString
    End Function
    Public Shared Function CreateVBClassWithTable2010_INotifyPropertyChanged(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder("Imports System.ComponentModel " & vbNewLine)

        strWriteModel.AppendLine("<Serializable()>")
        strWriteModel.AppendLine("Public Class " & DT.TableName)
        strWriteModel.AppendLine("    Implements INotifyPropertyChanged")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Private  _" & DT.TableName & R.ColumnValue & " As " & R.TypeVB)
        Next
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & DT.TableName & R.ColumnValue & " As " & R.TypeVB)
            strWriteModel.AppendLine("        Get")
            strWriteModel.AppendLine("            Return _" & DT.TableName & R.ColumnValue)
            strWriteModel.AppendLine("        End Get")
            strWriteModel.AppendLine("        Set(ByVal Value As " & R.TypeVB & ")")
            strWriteModel.AppendLine("            _" & DT.TableName & R.ColumnName & " = Value")
            strWriteModel.AppendLine("        OnPropertyChanged(" & """" & DT.TableName & R.ColumnValue & """" & ")")
            strWriteModel.AppendLine("        End Set")
            strWriteModel.AppendLine("    End Property")
        Next
        strWriteModel.AppendLine("Protected Sub OnPropertyChanged(ByVal [property] As String)")
        strWriteModel.AppendLine("    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs([property]))")
        strWriteModel.AppendLine("End Sub")
        strWriteModel.AppendLine(" Public Event PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged")

        strWriteModel.Append("End Class")
        Return strWriteModel.ToString
    End Function
    Public Shared Function VBClass2010SampleData(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.Append("Public Class " & DT.TableName)

        strWriteModel.AppendLine()
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.LinqVar)
        Next
        strWriteModel.AppendLine(HelpClass.getClassModelList(DT))
        strWriteModel.Append("End Class")
        Return strWriteModel.ToString

    End Function
    Public Shared Function VBClass2010XMLSample(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.Append("Public Class " & DT.TableName)

        strWriteModel.AppendLine()
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.LinqVar)
        Next
        strWriteModel.AppendLine(HelpClass.GetTestingXMLListCollection(DT))
        strWriteModel.AppendLine()
        strWriteModel.AppendLine(HelpClass.GetTestingListCollection(DT))
        strWriteModel.Replace("[TABLENAME]", DT.TableValue)
        strWriteModel.Append("End Class")
        Return strWriteModel.ToString

    End Function
    Public Shared Function InsertSqlCollectionSample(ByVal DT As TableNameInfo, colunsList As List(Of ColumnsInfo), WhereClose As String) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.AppendLine(HelpClass.GetInsertListCollection(DT, colunsList, WhereClose))
        Return strWriteModel.ToString

    End Function
    Public Shared Function VBClass2010CollectionSample(ByVal DT As TableNameInfo) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.AppendLine(HelpClass.GetFullListCollection(DT))
        strWriteModel.Replace("[TABLENAME]", DT.TableValue)
        Return strWriteModel.ToString

    End Function
    Public Shared Function VBClass2010CollectionSample(ByVal DT As TableNameInfo, colunsList As List(Of ColumnsInfo)) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.AppendLine(HelpClass.GetFullListCollection(DT, colunsList))
        strWriteModel.Replace("[TABLENAME]", DT.TableValue)
        Return strWriteModel.ToString

    End Function
    Public Shared Function VBClass2010JsonSample(ByVal DT As TableNameInfo, colunsList As List(Of ColumnsInfo)) As String
        Dim strWriteModel As New StringBuilder()

        strWriteModel.AppendLine(HelpClass.GetFullListJsonCollection(DT, colunsList))
        strWriteModel.Replace("[TABLENAME]", DT.TableValue)
        Return strWriteModel.ToString

    End Function
End Class
