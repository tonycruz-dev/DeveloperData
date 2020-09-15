Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Text
Imports System.Reflection
Imports System.Windows.Forms.Design

Public Class CustomMenuStrip
    Inherits MenuStrip
    ' Methods
    Public Sub New()
        MyBase.Renderer = New CustomMenuRenderer
    End Sub


    ' Nested Types
    Friend Class CustomMenuRenderer
        Inherits ToolStripProfessionalRenderer
        ' Methods
        Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
            e.TextColor = Color.White
            MyBase.OnRenderItemText(e)
        End Sub

        Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As ToolStripItemRenderEventArgs)
            If e.Item.Selected Then
                Using b As Brush = New SolidBrush(Color.FromArgb(80, 80, 80))
                    e.Graphics.FillRectangle(b, e.Graphics.ClipBounds)
                End Using
            Else
                MyBase.OnRenderMenuItemBackground(e)
            End If
        End Sub

        Protected Overrides Sub OnRenderToolStripBackground(ByVal e As ToolStripRenderEventArgs)
            Dim g As Graphics = e.Graphics
            Using b As Brush = New SolidBrush(Color.FromArgb(&H5B, &H5B, &H5B))
                g.FillRectangle(b, e.Graphics.ClipBounds)
            End Using
        End Sub

    End Class
End Class
Public Class CustomToolStrip
    Inherits ToolStrip
    Private _renderer As CustomToolStripRenderer

    Public Sub New()
        MyBase.New()
        Me.GripStyle = ToolStripGripStyle.Hidden
        Me._renderer = New CustomToolStripRenderer()
        Me.Renderer = Me._renderer
    End Sub
End Class

Public Class CustomToolStripRenderer
    Inherits ToolStripProfessionalRenderer
    Public Sub New()
        Me.RoundedEdges = False
    End Sub
    Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
        If e.Item.Selected Then
            e.TextColor = Color.FromArgb(255, 223, 127)
        End If
        MyBase.OnRenderItemText(e)
    End Sub
    Protected Overrides Sub OnRenderButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
        If Not e.Item.Selected Then
            MyBase.OnRenderButtonBackground(e)
        End If

    End Sub
End Class


Public Class GradientHeaderStrip
    Inherits ToolStrip
    Private _renderer As GradientToolStripRenderer

    Public Sub New()
        MyBase.New()
        Me.GripStyle = ToolStripGripStyle.Hidden
        Me._renderer = New GradientToolStripRenderer
        Me.Renderer = Me._renderer
    End Sub
End Class

Friend Class GradientToolStripRenderer
    Inherits ToolStripProfessionalRenderer
    Private _startColor As Color = Color.White
    Private _endColor As Color = Color.FromArgb(168, 186, 212)
    Private _lines As Integer = 6
    Private _drawEndLine As Boolean = True

    Public Sub New()
        Me.RoundedEdges = False
    End Sub

    Public Property EndColor() As Color
        Get
            Return _endColor
        End Get
        Set(ByVal value As Color)
            _endColor = value
        End Set
    End Property

    Public Property StartColor() As Color
        Get
            Return _startColor
        End Get
        Set(ByVal value As Color)
            _startColor = value
        End Set
    End Property

    Public Property Lines() As Integer
        Get
            Return _lines
        End Get
        Set(ByVal value As Integer)
            _lines = value
        End Set
    End Property

    Public Property DrawEndLine() As Boolean
        Get
            Return _drawEndLine
        End Get
        Set(ByVal value As Boolean)
            _drawEndLine = value
        End Set
    End Property

    Protected Overloads Overrides Sub OnRenderToolStripBackground(ByVal e As ToolStripRenderEventArgs)
        Dim start As Color = _startColor
        Dim [end] As Color = _endColor
        Dim toolStrip As ToolStrip = e.ToolStrip
        Dim g As Graphics = e.Graphics
        Dim boundsHeight As Integer = e.AffectedBounds.Height
        Dim height As Integer = (boundsHeight + _lines - 1) / _lines
        Dim width As Integer = e.AffectedBounds.Width
        Dim stripeHeight As Integer = height - 1
        Dim stripeRect As Rectangle
        ' Using 
        Dim b As Brush = New LinearGradientBrush(New Rectangle(0, 0, width, stripeHeight), start, [end], LinearGradientMode.Horizontal)
        Try
            Dim idx As Integer = 0
            While idx < _lines
                stripeRect = New Rectangle(0, height * idx + 1, width, stripeHeight)
                g.FillRectangle(b, stripeRect)
                System.Math.Min(System.Threading.Interlocked.Increment(idx), idx - 1)
            End While
        Finally
            CType(b, IDisposable).Dispose()
        End Try
        If Me.DrawEndLine Then
            ' Using 
            Dim solidBrush As Brush = New SolidBrush(Color.FromArgb(177, 177, 177))
            Try
                g.FillRectangle(solidBrush, New Rectangle(0, boundsHeight - 1, width, 1))
            Finally
                CType(solidBrush, IDisposable).Dispose()
            End Try
        End If
    End Sub
End Class

<ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)> _
Public Class ToolStripHoverItem
    Inherits ToolStripLabel
    Private _hoverImage As Image = Nothing
    Private _image As Image = Nothing
    Private _checkImage As Image = Nothing
    Private _checkHoverImage As Image = Nothing
    Private _onEnter As Image = Nothing
    Private _onLeave As Image = Nothing
    Private _checked As Boolean = False
    Private _hover As Boolean = False

    Public Sub New()
        Me.DisplayStyle = ToolStripItemDisplayStyle.Image
    End Sub

    Public Property ImageHover() As Image
        Get
            Return _hoverImage
        End Get
        Set(ByVal value As Image)
            _hoverImage = value
            If _onEnter Is Nothing Then
                _onEnter = _hoverImage
            End If
        End Set
    End Property

    Public Property ImageNormal() As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            _image = value
            If MyBase.Image Is Nothing Then
                MyBase.Image = _image
            End If
            If _onLeave Is Nothing Then
                _onLeave = _image
            End If
        End Set
    End Property

    Public Property ImageCheck() As Image
        Get
            Return _checkImage
        End Get
        Set(ByVal value As Image)
            _checkImage = value
        End Set
    End Property

    Public Property ImageCheckHover() As Image
        Get
            Return _checkHoverImage
        End Get
        Set(ByVal value As Image)
            _checkHoverImage = value
        End Set
    End Property

    Public Property Checked() As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            SetState(value)
        End Set
    End Property

    Public Event CheckedChanged As EventHandler

    Protected Overridable Sub OnCheckedChanged(ByVal e As EventArgs)
        RaiseEvent CheckedChanged(Me, e)
    End Sub

    Protected Overloads Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        If _onEnter IsNot Nothing Then
            MyBase.Image = _onEnter
            _hover = True
        End If
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overloads Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        If _onLeave IsNot Nothing Then
            MyBase.Image = _onLeave
            _hover = False
        End If
        MyBase.OnMouseLeave(e)
    End Sub

    Private Sub SetState(ByVal state As Boolean)
        If Not (_checked = state) Then
            _checked = state
            If _checkImage IsNot Nothing AndAlso _checkHoverImage IsNot Nothing Then
                If _checked Then
                    _onEnter = _checkHoverImage
                    _onLeave = _checkImage
                Else
                    _onEnter = _hoverImage
                    _onLeave = _image
                End If
                MyBase.Image = (Microsoft.VisualBasic.IIf(_hover, _onEnter, _onLeave))
                OnCheckedChanged(EventArgs.Empty)
            End If
        End If
    End Sub

    Protected Overloads Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)
        SetState(Not _checked)
    End Sub
End Class

#Region "AreaHeaderStyle"
Public Enum AreaHeaderStyle
    Large = 0
    Small = 1
End Enum
#End Region

Public Class HeaderStrip
    Inherits ToolStrip
    Private _headerStyle As AreaHeaderStyle = AreaHeaderStyle.Large
    Private _pr As ToolStripProfessionalRenderer = Nothing
    Public Sub New()
        MyBase.New()
        Me.Dock = DockStyle.Top
        Me.GripStyle = ToolStripGripStyle.Hidden
        Me.AutoSize = False
        SetRenderer()
        AddHandler Microsoft.Win32.SystemEvents.UserPreferenceChanged, AddressOf HeaderStrip_UserPreferenceChanged
        SetHeaderStyle()
    End Sub


    Private Sub SetRenderer()
        If (TypeOf Me.Renderer Is ToolStripProfessionalRenderer) AndAlso (Not (Me.Renderer Is _pr)) Then
            If _pr Is Nothing Then
                _pr = New ToolStripProfessionalRenderer
                _pr.RoundedEdges = False
                AddHandler _pr.RenderToolStripBackground, AddressOf Renderer_RenderToolStripBackground
            End If
            Me.Renderer = _pr
        End If
    End Sub
    Public Property HeaderStyle() As AreaHeaderStyle
        Get
            Return _headerStyle
        End Get
        Set(ByVal value As AreaHeaderStyle)
            If Not (_headerStyle = value) Then
                _headerStyle = value
                SetHeaderStyle()
            End If
        End Set
    End Property
    Protected Overloads Overrides Sub OnRendererChanged(ByVal e As EventArgs)
        MyBase.OnRendererChanged(e)
        SetRenderer()
    End Sub
    Private Sub SetHeaderStyle()
        Dim font As Font = SystemFonts.MenuFont
        If _headerStyle = AreaHeaderStyle.Large Then
            Me.Font = New Font("Arial", font.SizeInPoints + 3.75F, FontStyle.Bold)
            Me.ForeColor = System.Drawing.Color.White
        Else
            Me.Font = font
            Me.ForeColor = System.Drawing.Color.Black
        End If
        Dim tsl As ToolStripLabel = New ToolStripLabel
        tsl.Font = Me.Font
        tsl.Text = "I"
        Me.Height = tsl.GetPreferredSize(Drawing.Size.Empty).Height + 6
    End Sub

    Sub Renderer_RenderToolStripBackground(ByVal sender As Object, ByVal e As ToolStripRenderEventArgs)
        Dim start As Color
        Dim [end] As Color
        If TypeOf Me.Renderer Is ToolStripProfessionalRenderer Then
            'ToolStripProfessionalRenderer pr = (this.Renderer as ToolStripProfessionalRenderer);

            Dim pr As ToolStripProfessionalRenderer = CType(Me.Renderer, ToolStripProfessionalRenderer)
            If _headerStyle = AreaHeaderStyle.Large Then
                start = _pr.ColorTable.OverflowButtonGradientMiddle
                [end] = _pr.ColorTable.OverflowButtonGradientEnd
            Else
                start = _pr.ColorTable.MenuStripGradientEnd
                [end] = _pr.ColorTable.MenuStripGradientBegin
            End If
            Dim bounds As Rectangle = New Rectangle(Point.Empty, e.ToolStrip.Size)
            If (bounds.Width > 0) AndAlso (bounds.Height > 0) Then
                ' Using 
                Dim b As Brush = New LinearGradientBrush(bounds, start, [end], LinearGradientMode.Vertical)
                Try
                    e.Graphics.FillRectangle(b, bounds)
                Finally
                    CType(b, IDisposable).Dispose()
                End Try
            End If
        End If
    End Sub
    Private Sub HeaderStrip_UserPreferenceChanged(ByVal sender As Object, ByVal e As Microsoft.Win32.UserPreferenceChangedEventArgs)
        SetHeaderStyle()
    End Sub

End Class
