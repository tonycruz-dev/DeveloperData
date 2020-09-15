Imports DBExtenderLib

Public Class TreeNodeTableDomainClass
    Inherits TreeNode
    Private _DomainService As DomainServiceManager
    Public Sub New()
    End Sub
    Public Sub New(ByVal DomainService As DomainServiceManager)
        _DomainService = DomainService
        Me.Text = _DomainService.DomainServiceName
    End Sub
    Public Property DomainService As DomainServiceManager
        Get
            Return _DomainService
        End Get
        Set(ByVal Value As DomainServiceManager)
            _DomainService = Value
            Me.Text = _DomainService.DomainServiceName
        End Set
    End Property
    Public Property ListFunction As FunctionList
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeTableDomainTables
    Inherits TreeNode
    Private ObjTable As TableNameInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As TableNameInfo)
        ObjTable = OTable

        If OTable.SchemaTable = "" Then
            Me.Text = ObjTable.TableName
        Else
            If ObjTable.Database.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MySqlServer Then
                Me.Text = ObjTable.TableName
            Else
                Me.Text = OTable.SchemaTable & "." & ObjTable.TableName
            End If

        End If

    End Sub
    Public Property Table() As TableNameInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As TableNameInfo)
            ObjTable = Value
            Me.Text = ObjTable.TableName
        End Set
    End Property
    Public Property ListFunction As FunctionList
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class

Public Class TreeNodeFunctionSelectLinq
    Inherits TreeNode
    Private ObjFinction As FunctionList
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As FunctionList)
        ObjFinction = OTable
        Me.Text = "Select " & ObjFinction.TableValue
    End Sub
    Public Property Table() As FunctionList
        Get
            Return ObjFinction
        End Get
        Set(ByVal Value As FunctionList)
            ObjFinction = Value
            Me.Text = "Select " & ObjFinction.TableValue
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeFunctionSelectSubClass
    Inherits TreeNode
    Private ObjFinction As FunctionList
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As FunctionList)
        ObjFinction = OTable
        Me.Text = "Select Sub Class" & ObjFinction.TableValue
    End Sub
    Public Property Table() As FunctionList
        Get
            Return ObjFinction
        End Get
        Set(ByVal Value As FunctionList)
            ObjFinction = Value
            Me.Text = "Select Sub Class " & ObjFinction.TableValue
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeFunctionInsertLinq
    Inherits TreeNode
    Private ObjFinction As FunctionList
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As FunctionList)
        ObjFinction = OTable
        Me.Text = "Insert " & ObjFinction.TableValue
    End Sub
    Public Property Table() As FunctionList
        Get
            Return ObjFinction
        End Get
        Set(ByVal Value As FunctionList)
            ObjFinction = Value
            Me.Text = "Insert " & ObjFinction.TableValue
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeFunctionUpdateLinq
    Inherits TreeNode
    Private ObjFinction As FunctionList
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As FunctionList)
        ObjFinction = OTable
        Me.Text = "Update " & ObjFinction.TableValue
    End Sub
    Public Property Table() As FunctionList
        Get
            Return ObjFinction
        End Get
        Set(ByVal Value As FunctionList)
            ObjFinction = Value
            Me.Text = "Update " & ObjFinction.TableValue
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeFunctionDeleteLinq
    Inherits TreeNode
    Private ObjFinction As FunctionList
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As FunctionList)
        ObjFinction = OTable
        Me.Text = "Delete " & OTable.TableValue
    End Sub
    Public Property Table() As FunctionList
        Get
            Return ObjFinction
        End Get
        Set(ByVal Value As FunctionList)
            ObjFinction = Value
            Me.Text = "Delete " & Value.TableValue
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
