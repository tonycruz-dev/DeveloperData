Imports System.Data.Entity.Design.PluralizationServices
Imports System.Globalization

Public Class RelationalTable
    Public Property SelectedTable As TableNameInfo
    Public Property SelectedTableName As String
    Public Property SelectedTableLinqKey As String
    Public Property RelateTable As TableNameInfo
    Public Property RelateTableName As String
    Public Property ForeignKey As String
    Public Property ColumnType As String
    Public Property ColumnTypeVB As String
    Public Property ColumnTypeCS As String
    Public ReadOnly Property SelectedTableValue() As String
        Get
            Return Replace(_SelectedTableName, " ", "_")
        End Get
    End Property
    Public ReadOnly Property RelateTableValue() As String
        Get
            Return Replace(_RelateTableName, " ", "_")
        End Get
    End Property
    Public ReadOnly Property RelateTableSingularize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Singularize(_RelateTableName)

            Return Replace(data, " ", "_")
        End Get
    End Property
    Public ReadOnly Property RelateTablePluralize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Pluralize(_RelateTableName)
            Return Replace(data, " ", "_")
        End Get
    End Property
End Class
