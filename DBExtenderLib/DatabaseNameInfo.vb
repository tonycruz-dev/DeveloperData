Imports System.Runtime.CompilerServices

Partial Public Class DatabaseNameInfo

#Region " Database Type"
    Enum TypeOfDatabase
        MicrosoftAccess
        MicrosoftSqlServer
        MicrosoftSqlServerFile
        MicrosoftSqlServerMobile
        MySqlServer
    End Enum
#End Region

#Region "List Of Properties"
    Public Property isDataLoad() As Boolean

    Public Property ConnectionString() As String

    Public Property SqlCompactConnection() As String

    Public Property DatabaseID() As Integer

    Public Property DatabaseName() As String

    Public Property DBPath() As String

    Public Property IsAccess2003() As Boolean

    Public Property IsAccess2007() As Boolean

    Public Property ListLinqDatabase As New List(Of LinqDatabaseNameInfo)

    Public Property ListViews() As New List(Of ViewNameInfo)

    Public Property ListSPro() As New List(Of StoredProcedureNameInfo)

    Public Property ListTable() As New List(Of TableNameInfo)

    Public Property ListDomainservices As New List(Of DomainServiceManager)

    Public Property ListRiaContext As New List(Of RiaClassContent)

    Public Property WPFDatabaseContext As New WPFDataContext

    Public Property ServerName() As String

    Public Property IntegratedSecurity() As Boolean

    Public Property UserName() As String

    Public Property Password() As String

    Public Property MSAccessPath() As String

    Public ReadOnly Property MSAccessConnection() As String
        Get
            If _IsAccess2007 Then
                Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & MSAccessPath
            Else
                Return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & MSAccessPath
            End If
        End Get
    End Property
    Public Property dbFileConnection() As String
    Public Overloads Function ToString() As String
        Return _DatabaseName
    End Function
    Public ReadOnly Property Name() As String
        Get
            Return _DatabaseName
        End Get
    End Property
    Public Property SelectedTypeOfDatabase() As TypeOfDatabase

    Public Property providerName() As String

    Public Property DatabaseFileName() As String



#End Region


End Class
