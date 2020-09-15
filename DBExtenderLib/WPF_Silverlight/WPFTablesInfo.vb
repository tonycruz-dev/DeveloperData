Public Class WPFTablesInfo
    Public Property WPFFileContent As String
    Public Property VBFileContent As String
    Public Property WPFTableseName As String
    Public Property ClassNameSpace As String
    Public Property MainTable As TableNameInfo
    Public Property DelailsTables As New List(Of WPFMasterDetails)
    Public Property TableType As String
    Public Property ListColumn As New List(Of wpfColumnInfo)
    Public Property DataGrigListColumn As New List(Of wpfColumnInfo)
    Public Property DetailsListColumn As New List(Of wpfColumnInfo)
    Public Property UIDatagrid As XElement
    Public Property UIDetails As XElement
    Public ReadOnly Property TableValue() As String
        Get
            Return Replace(_MainTable.TableName, " ", "_")
        End Get
    End Property
    Public Shared Function CopyTableToWPF(ByVal tv As TableNameInfo) As WPFTablesInfo
        Dim tb As New WPFTablesInfo With {.MainTable = tv.CopyTable,
                                          .WPFTableseName = tv.TableName,
                                          .TableType = tv.TableType}
        For Each col In tv.ListColumn
            tb.ListColumn.Add(col.CopyColumn)
        Next
        For Each col In tv.ListColumn
            tb.DataGrigListColumn.Add(col.CopyColumn)
        Next
        For Each col In tv.ListColumn
            tb.DetailsListColumn.Add(col.CopyColumn)
        Next
        Return tb
    End Function
End Class
