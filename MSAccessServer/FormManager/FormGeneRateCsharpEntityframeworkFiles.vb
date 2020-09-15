Imports DBExtenderLib

Public Class FormGeneRateCsharpEntityframeworkFiles
    Private folderName As String
    Private _ListTables As New List(Of TableNameInfo)
    Private _db As DatabaseNameInfo
    Public Sub New(db As DatabaseNameInfo)


        ' This call is required by the designer.
        InitializeComponent()
        _db = db

        ' Add any initialization after the InitializeComponent() call.
        'CheckedListBoxTables
        TextBoxProjectName.Text = db.DatabaseName

        CheckedListBoxTables.DataSource = _db.ListTable
        CheckedListBoxTables.DisplayMember = "TableName"
        CheckedListBoxTables.ValueMember = "TableValue"
    End Sub
    Public ReadOnly Property listSelectedTables As List(Of TableNameInfo)
        Get
            Return _ListTables
        End Get
    End Property
    Public ReadOnly Property ProjectName As String
        Get
            Return TextBoxProjectName.Text
        End Get
    End Property
    Public Property PathLocation As String
        Get
            Return TextBoxSelectedFolder.Text
        End Get
        Set(value As String)
            TextBoxSelectedFolder.Text = value
        End Set
    End Property

    Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click
        If TextBoxSelectedFolder.Text = "" Then
            MessageBox.Show("Please Selecte Save Location")
            Exit Sub
        End If
        For Each col In CheckedListBoxTables.CheckedItems
            Dim selectedTables As TableNameInfo = CType(col, TableNameInfo)
            _ListTables.Add(selectedTables)
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub ButtonSelectFolder_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSelectFolder.Click


        Dim result As DialogResult = FBDSelectedFolder.ShowDialog()

        If (result = DialogResult.OK) Then
            folderName = FBDSelectedFolder.SelectedPath
            TextBoxSelectedFolder.Text = folderName
        End If
    End Sub
End Class