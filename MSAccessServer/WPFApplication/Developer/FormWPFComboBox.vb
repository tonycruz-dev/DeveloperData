Imports DBExtenderLib

Public Class FormWPFComboBox
    Private _TableDatasource As TableNameInfo
    Private _ColumnDisplayMember As List(Of ColumnsInfo)
    Private _ColumnValueMenber As List(Of ColumnsInfo)
    Private SelectedTable As TableNameInfo
    Private ListComboBox As New List(Of ColumnComboBox)
    Private _Database As DatabaseNameInfo
    Public Sub New(ByVal db As DatabaseNameInfo, ByVal TBSelected As TableNameInfo)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        SelectedTable = TBSelected
        LabelTbTarget.Text = SelectedTable.TableName

        Dim lsttable = (From lt In db.ListTable Select lt Where lt.TableName <> SelectedTable.TableName Select lt).ToList
        '  CLBSourceTable.DataSource = TB.ListColumn
        '  CLBSourceTable.DisplayMember = "ColumnName"

        ComboBoxListTables.DataSource = lsttable
        ComboBoxListTables.DisplayMember = "TableName"

        Dim colselVal = (From sv In SelectedTable.ListColumn Select sv).ToList
        ComboBoxSelectedValue.DataSource = colselVal
        ComboBoxSelectedValue.DisplayMember = "ColumnName"
    End Sub
    Private Sub ComboBoxListTables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxListTables.SelectedIndexChanged
        _TableDatasource = CType(ComboBoxListTables.SelectedValue, TableNameInfo)

        Dim coldispMem = (From dm In _TableDatasource.ListColumn Select dm).ToList
        Me.ComboBoxDisplayMember.DataSource = coldispMem
        Me.ComboBoxDisplayMember.DisplayMember = "ColumnName"

        Dim colValueMem = (From vm In _TableDatasource.ListColumn Select vm).ToList
        Me.ComboBoxValueMember.DataSource = colValueMem
        Me.ComboBoxValueMember.DisplayMember = "ColumnName"
    End Sub

    Private Sub ButtonSetComboBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSetComboBox.Click
        Dim col As ColumnsInfo = CType(ComboBoxSelectedValue.SelectedValue, ColumnsInfo)
        _DisplayMemberPath = CType(ComboBoxDisplayMember.SelectedValue, ColumnsInfo).ColumnName    ' _TableDatasource.TableName 
        _SelectedValuePath = CType(ComboBoxValueMember.SelectedValue, ColumnsInfo).ColumnName
        _SelectedValue = col.ColumnName
        _DataSource = _TableDatasource.TableName
    End Sub
    Public Property SelectedValuePath As String
    Public Property SelectedValue As String
    Public Property DisplayMemberPath As String
    Public Property DataSource As String

End Class