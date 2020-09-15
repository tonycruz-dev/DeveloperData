Public Class DomainServiceManager
    Public Property ProjectName As String
    Public Property ConnectionString As String
    Public Property DatabaseName As String
    Public Property DomainServiceName As String
    Public Property DBPath As String
    Public Property ClassNameSpace As String
    Public Property SaveLocation As String
    Public Property DomainServiceContext As String
    Public Property ListTable As New List(Of TableNameInfo)
    Public Property ListFunctions As New List(Of FunctionList)
End Class
Public Class FunctionList
    Public Property TableValue As String
    Public Property FunctionSelect As String
    Public Property FunctionInsert As String
    Public Property FunctionUpdate As String
    Public Property FunctionDelete As String
    Public Property FunctionSelectSearch As String
    Public Property FunctionSelectSubClass As String
End Class
Public Class RiaClassContent
    Public Property ClassContent As String
    Public Property RiaServiceName As String
    Public Property RiaServiceContext As String
    Public Property SaveLocation As String
    Public Property ClassNameSpace As String
    Public Property ListRiaClass As New List(Of RiaClassInfo)
End Class
Public Class RiaClassInfo
    Public Property Table As TableNameInfo
    Public Property SimpleClass As String
    Public Property ComplexClass As String
End Class


