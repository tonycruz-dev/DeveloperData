Imports System.Text
Imports System.Data.Entity.Design.PluralizationServices
Imports System.Globalization

Partial Public Class TableNameInfo

    Public Property Database() As New DatabaseNameInfo
    Public Property TableID As Integer
    Public Property TableName() As String
    Public Property TableType() As String
    Public Property StrConnection() As String
    Public Property SchemaTable() As String
    Public Property ListColumn() As New List(Of ColumnsInfo)

    Public Sub AddColumn(ByVal objColumns As ColumnsInfo)
        _ListColumn.Add(objColumns)
    End Sub
    Public Overloads Function ToString() As String
        Return _TableName
    End Function
    Public ReadOnly Property Name() As String
        Get
            Return _TableName
        End Get
    End Property
    Public ReadOnly Property TableValue() As String
        Get
            Return Replace(_TableName, " ", "_")
        End Get
    End Property
    Public ReadOnly Property TableSingularize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Singularize(_TableName)

            Return Replace(data, " ", "_")
        End Get
    End Property
    Public ReadOnly Property TablePluralize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Pluralize(_TableName)
            Return Replace(data, " ", "_")
        End Get
    End Property

    Public Function GetPrimaryKey() As ColumnsInfo
        Dim qcpk = (From pk In _ListColumn Where pk.IsPrimary_Key = True).Take(1).SingleOrDefault
        If qcpk Is Nothing Then
            qcpk = _ListColumn(0)
        End If
        Return qcpk
    End Function
    Public Function GetForeignKey() As List(Of ForeignKeyInfo)
        Dim qcpk = (From pk In _ListColumn Where pk.IsForeign_Key = True
                    Select pk.ForeignKey).ToList
        Return qcpk
    End Function
    Public Shared Function CreateTableFromView(ByVal tv As ViewNameInfo) As TableNameInfo
        Dim tb As New TableNameInfo With {.TableName = tv.ViewName_Name,
                                          .SchemaTable = tv.Schema_ViewName,
                                          .StrConnection = tv.StrConnection,
                                          .TableType = "View"}
        For Each col In tv.ViewColumns
            tb.ListColumn.Add(col)
        Next
        Return tb
    End Function

    Public Shared Function CreateACopyTable(ByVal tv As TableNameInfo) As TableNameInfo
        Dim tb As New TableNameInfo With {.TableName = tv.TableName,
                                          .SchemaTable = tv.SchemaTable,
                                          .StrConnection = tv.StrConnection,
                                          .TableType = "Table"}
        For Each col In tv.ListColumn
            tb.ListColumn.Add(col)
        Next
        Return tb
    End Function
End Class
