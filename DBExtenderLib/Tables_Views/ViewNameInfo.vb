Imports System.Globalization
Imports System.Data.Entity.Design.PluralizationServices

<Serializable()> Partial Public Class ViewNameInfo
    Public Sub New()
    End Sub
    Public Property Database As New DatabaseNameInfo
    Public Property Schema_ViewName As String
    Public Property ViewName_Name As String
    Public Property ViewColumns As New List(Of ColumnsInfo)
    Public Property TextBody As String
    Public Property TextHeader As String
    Public Property StrConnection As String
    Public Overloads Function ToString() As String
        Return _ViewName_Name
    End Function
    Public ReadOnly Property Name() As String
        Get
            Return _ViewName_Name
        End Get
    End Property
    Public ReadOnly Property ViewValue() As String
        Get
            Return Replace(_ViewName_Name, " ", "_")
        End Get
    End Property
    Public ReadOnly Property ViewSingularize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Singularize(_ViewName_Name)

            Return Replace(data, " ", "_")
        End Get
    End Property
    Public ReadOnly Property ViewPluralize() As String
        Get
            Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
            Dim data = ps.Pluralize(_ViewName_Name)
            Return Replace(data, " ", "_")
        End Get
    End Property
End Class

