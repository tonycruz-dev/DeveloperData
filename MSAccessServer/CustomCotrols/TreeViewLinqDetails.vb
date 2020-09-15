Imports DBExtenderLib

#Region "Linq TreeViews"

Public Class TreeNodeLinqDataContext
    Inherits TreeNode
    Private _LinqDatabase As LinqDatabaseNameInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal LinqDatabase_ As LinqDatabaseNameInfo)
        _LinqDatabase = LinqDatabase_
        Me.Text = _LinqDatabase.LinqDatabaseName
    End Sub
    Public Property LinqDatabase() As LinqDatabaseNameInfo
        Get
            Return _LinqDatabase
        End Get
        Set(ByVal Value As LinqDatabaseNameInfo)
            _LinqDatabase = Value
            Me.Text = _LinqDatabase.LinqDatabaseName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeTableLinq
    Inherits TreeNode
    Private ObjTable As LinqTableNameinfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As LinqTableNameinfo)
        ObjTable = OTable
        Me.Text = ObjTable.TableName
    End Sub
    Public Property Table() As LinqTableNameinfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As LinqTableNameinfo)
            ObjTable = Value
            Me.Text = ObjTable.TableName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeViewLinq
    Inherits TreeNode
    Private ObjTable As LinqViewNameifo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As LinqViewNameifo)
        ObjTable = OTable
        Me.Text = ObjTable.TableName
    End Sub
    Public Property Table() As LinqViewNameifo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As LinqViewNameifo)
            ObjTable = Value
            Me.Text = ObjTable.TableName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class


#End Region
