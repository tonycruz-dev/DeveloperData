Imports System.Text

Public Class LinqDatabaseNameInfo
    Public Property ClassName As String
    Public Property ProjectName As String
    Public Property ConnectionString() As String
    Public Property DatabaseName() As String
    Public Property DatabaseName_And_Ext() As String
    Public Property DBPath() As String
    Public Property ContentClass As String
    Public Property ClassNameSpace As String
    Public Property SaveLocation As String
    Public Property DatabaseFileName() As String
    Public Property LinqDatabaseName As String
    Public Property ListViews() As New List(Of ViewNameInfo)
    Public Property ListTable() As New List(Of TableNameInfo)
    Public Property ListLinqTable As New List(Of LinqTableNameinfo)
    Public Property ListLinqView As New List(Of LinqViewNameifo)
    Public Property RelateTables As New List(Of LinqReatedTables)
End Class
Public Class LinqReatedTables
    Public Property TableName As String
    Public Property ForeignKeyName() As String
    Public Property RelatedTable() As String
    Public Property ColumnName() As String
    Public Property RelatedColumnName() As String
    Public Property Column As ColumnsInfo
    Public Property RelateColumn As ColumnsInfo
    Public ReadOnly Property ColumnValue As String
        Get
            Return Replace(Replace(Replace(Replace(_ColumnName, " ", "_"), "/", "_"), "-", ""), ".", "_")
        End Get
    End Property
    Public ReadOnly Property RelatedColumnValue As String
        Get
            Return Replace(Replace(Replace(Replace(_RelatedColumnName, " ", "_"), "/", "_"), "-", ""), ".", "_")
        End Get
    End Property

    Public ReadOnly Property TableValue() As String
        Get
            Return Replace(_TableName, " ", "_")
        End Get
    End Property
    Public ReadOnly Property RelatedTableValue() As String
        Get
            Return Replace(_RelatedTable, " ", "_")
        End Get
    End Property
    Public ReadOnly Property ForeignKeyNameValue() As String
        Get
            Return Replace(_ForeignKeyName, " ", "_")
        End Get
    End Property
End Class
Partial Public Class LinqTableNameinfo
    Inherits TableNameInfo
    Public Property TableClass As String
    Public Property ClassManager As String
    Public Shared Function CopyTable(ByVal tv As TableNameInfo) As LinqTableNameinfo
        Dim tb As New LinqTableNameinfo With {.TableName = tv.TableName,
                                              .SchemaTable = tv.SchemaTable,
                                              .StrConnection = tv.StrConnection,
                                              .TableType = tv.TableType}
        For Each col In tv.ListColumn
            tb.ListColumn.Add(col)
        Next
        Return tb
    End Function
   
End Class
Partial Public Class LinqViewNameifo
    Inherits TableNameInfo
    Public Property TableClass As String
    Public Shared Function CopyTable(ByVal tv As TableNameInfo) As LinqTableNameinfo
        Dim tb As New LinqTableNameinfo With {.TableName = tv.TableName,
                                              .SchemaTable = tv.SchemaTable,
                                              .StrConnection = tv.StrConnection,
                                              .TableType = tv.TableType}
        For Each col In tv.ListColumn
            tb.ListColumn.Add(col)
        Next
        Return tb
    End Function
    
End Class
