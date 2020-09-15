Imports DBExtenderLib
Imports System.Data.Entity.Design.PluralizationServices
Imports System.Globalization

Public Class SimpleWizardVM
    Public Property Database As DatabaseNameInfo
    Public Property TableToProcess As TableNameInfo
    Public Property SaveLocation As String
    Public Property Table As New TableSelected
    Public Property TableShowColumn As New TableSelected
    Public Property ColumnKey As String
   
End Class
Public Class columnSelected
    Public Property ColumnName As String
    Public Property DisplayName As String

End Class
Public Class TableSelected
    Public Property Columns As New List(Of columnSelected)
    Public Property TableName As String
    Public Property DisplayName As String
    Public ReadOnly Property TableSingularize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Singularize(TableName)

            Return Replace(data, " ", "_")
        End Get
    End Property
    Public ReadOnly Property TablePluralize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Pluralize(TableName)
            Return Replace(data, " ", "_")
        End Get
    End Property
End Class
