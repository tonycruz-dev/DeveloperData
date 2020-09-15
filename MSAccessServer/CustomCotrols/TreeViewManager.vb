Imports DBExtenderLib

#Region " Database TreeNode"
Public Class TreeNodeColumns
    Inherits TreeNode
    Private ObjColumn As ColumnsInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OColumns As ColumnsInfo)
        ObjColumn = OColumns
        Me.Text = ObjColumn.ColumnName
    End Sub
    Public Property Columns() As ColumnsInfo
        Get
            Return ObjColumn
        End Get
        Set(ByVal Value As ColumnsInfo)
            ObjColumn = Value
            Me.Text = ObjColumn.ColumnName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function

End Class

Public Class TreeNodeDatabase
    Inherits TreeNode
    Private ObjDatabase As DatabaseNameInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal ODatabase As DatabaseNameInfo)
        ObjDatabase = ODatabase
        Me.Text = ObjDatabase.DatabaseName
    End Sub
    Public Property Database() As DatabaseNameInfo
        Get
            Return ObjDatabase
        End Get
        Set(ByVal Value As DatabaseNameInfo)
            ObjDatabase = Value
            Me.Text = ObjDatabase.DatabaseName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class

Public Class TreeNodeProcedures
    Inherits TreeNode
    Private ObjSP As StoredProcedureNameInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OSP As StoredProcedureNameInfo)
        ObjSP = OSP
        If ObjSP.SchemaTable = "" Then
            Me.Text = ObjSP.SP_Name
        Else
            Me.Text = ObjSP.SchemaTable & "." & ObjSP.SP_Name
        End If

    End Sub
    Public Property SP_Procedure() As StoredProcedureNameInfo
        Get
            Return ObjSP
        End Get
        Set(ByVal Value As StoredProcedureNameInfo)
            ObjSP = Value
            Me.Text = ObjSP.SP_Name
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class

Public Class TreeNodeTable
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
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class

Public Class TreeNodeView
    Inherits TreeNode
    Private ObjView As ViewNameInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As ViewNameInfo)
        ObjView = OTable
        If OTable.Schema_ViewName = "" Then
            Me.Text = ObjView.ViewName_Name
        Else
            Me.Text = OTable.Schema_ViewName & "." & ObjView.ViewName_Name
        End If

    End Sub
    Public Property View() As ViewNameInfo
        Get
            Return ObjView
        End Get
        Set(ByVal Value As ViewNameInfo)
            ObjView = Value
            Me.Text = ObjView.ViewName_Name
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
#End Region

#Region "Linq TreeViews"

#End Region

'RiaClassContent
Public Class TreeNodeRiaClassContent
    Inherits TreeNode
    Private _RiaClassContent As RiaClassContent
    Public Sub New()
    End Sub
    Public Sub New(ByVal _RiaContext As RiaClassContent)
        _RiaClassContent = _RiaContext

        Me.Text = _RiaClassContent.RiaServiceName

    End Sub
    Public Property RiaContent() As RiaClassContent
        Get
            Return _RiaClassContent
        End Get
        Set(ByVal Value As RiaClassContent)
            _RiaClassContent = Value
            Me.Text = _RiaClassContent.RiaServiceName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class
Public Class TreeNodeRiaClass
    Inherits TreeNode
    Private _RiaClass As RiaClassInfo
    Public Sub New()
    End Sub
    Public Sub New(ByVal OTable As RiaClassInfo)
        _RiaClass = OTable

        Me.Text = _RiaClass.Table.TableName

    End Sub
    Public Property Table() As RiaClassInfo
        Get
            Return _RiaClass
        End Get
        Set(ByVal Value As RiaClassInfo)
            _RiaClass = Value
            Me.Text = _RiaClass.Table.TableName
        End Set
    End Property
    Public Overloads Function ToString() As String
        Return Me.Text
    End Function
End Class

