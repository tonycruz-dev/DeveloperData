Imports DBExtenderLib

#Region "Linq TreeViews"

Public Class TreeNodeWPFDataContext
    Inherits TreeNode
    Private _wpfContext As WPFDataContext
    Public Sub New()
    End Sub
    Public Sub New(ByVal wpfcontext_ As WPFDataContext)
        _wpfcontext = wpfcontext_
        Me.Text = _wpfContext.WPFContext
    End Sub
    Public Property WpfContext As WPFDataContext
        Get
            Return _wpfContext
        End Get
        Set(ByVal Value As WPFDataContext)
            _wpfContext = Value
            Me.Text = _wpfContext.WPFContext
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeWPFTable
    Inherits TreeNode
    Private ObjTable As WPFTablesInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As WPFTablesInfo)
        ObjTable = OTable
        Me.Text = ObjTable.WPFTableseName
    End Sub
    Public Property Table() As WPFTablesInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As WPFTablesInfo)
            ObjTable = Value
            Me.Text = ObjTable.WPFTableseName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeWPFDetailsTable
    Inherits TreeNode
    Private ObjTable As WPFTablesInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As WPFTablesInfo)
        ObjTable = OTable
        Me.Text = ObjTable.WPFTableseName & " Details"
    End Sub
    Public Property Table() As WPFTablesInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As WPFTablesInfo)
            ObjTable = Value
            Me.Text = ObjTable.WPFTableseName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeWPFDatagridTable
    Inherits TreeNode
    Private ObjTable As WPFTablesInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As WPFTablesInfo)
        ObjTable = OTable
        Me.Text = ObjTable.WPFTableseName & " Data Grid"
    End Sub
    Public Property Table() As WPFTablesInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As WPFTablesInfo)
            ObjTable = Value
            Me.Text = ObjTable.WPFTableseName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeWPFView
    Inherits TreeNode
    Private ObjTable As WPFTablesInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As WPFTablesInfo)
        ObjTable = OTable
        Me.Text = ObjTable.WPFTableseName
    End Sub
    Public Property Table() As WPFTablesInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As WPFTablesInfo)
            ObjTable = Value
            Me.Text = ObjTable.WPFTableseName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeWPFVBFileContent
    Inherits TreeNode
    Private ObjTable As WPFTablesInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As WPFTablesInfo)
        ObjTable = OTable
        Me.Text = ObjTable.VBFileContent
    End Sub
    Public Property Table() As WPFTablesInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As WPFTablesInfo)
            ObjTable = Value
            Me.Text = ObjTable.VBFileContent
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeWPFFileContent
    Inherits TreeNode
    Private ObjTable As WPFTablesInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As WPFTablesInfo)
        ObjTable = OTable
        Me.Text = ObjTable.WPFFileContent
    End Sub
    Public Property Table() As WPFTablesInfo
        Get
            Return ObjTable
        End Get
        Set(ByVal Value As WPFTablesInfo)
            ObjTable = Value
            Me.Text = ObjTable.WPFFileContent
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
#End Region

