Imports DBExtenderLib
Imports System.Collections.ObjectModel

Public Class TablesStep1Control
    Private tblis As New List(Of TableNameInfo)
    Private DisplayCBTables As New List(Of TableNameInfo)
    Private SelectedTableData As TableNameInfo
    Private dbInfo As System.Windows.Data.CollectionViewSource
    Private _db As DatabaseNameInfo
    Private listColumnsob As ObservableCollection(Of ColumnsInfo)
    Private listSelectedColumnsob As New ObservableCollection(Of ColumnsInfo)
    '	myCollectionViewSource.Source = your data
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        listboxSelectedColumns.ItemsSource = listSelectedColumnsob
    End Sub
    Public Sub New(db As DatabaseNameInfo)

        ' This call is required by the designer.
        InitializeComponent()
        _db = db
        listboxSelectedColumns.ItemsSource = listSelectedColumnsob
        DisplayCBTables = db.ListTable
        DisplayListTable()
        ' Add any initialization after the InitializeComponent() call.


    End Sub
    Public Sub New(_vm As SimpleWizardVM)

        ' This call is required by the designer.
        InitializeComponent()
        _db = _vm.Database
        listboxSelectedColumns.ItemsSource = listSelectedColumnsob
        DisplayCBTables = _vm.Database.ListTable
        DisplayListTable()
        VM = _vm

        ' Add any initialization after the InitializeComponent() call.


    End Sub
    Public Property SetData As DatabaseNameInfo
        Get
            Return _db
        End Get
        Set(value As DatabaseNameInfo)
            _db = value
            DisplayListTable()
        End Set
    End Property
    Public Property VM As SimpleWizardVM

    Private Sub TablesStep1Control_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded



        'Me.cbListTables.ItemsSource = DisplayCBTables
    End Sub
    Private Sub DisplayListTable()
        '  dbInfo = CType(Me.Resources("dbInfo"), System.Windows.Data.CollectionViewSource)
        If _db IsNot Nothing Then
            cbListTables.ItemsSource = _db.ListTable
        End If
    End Sub

    Private Sub cbListTables_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbListTables.SelectionChanged
        listColumnsob = New ObservableCollection(Of ColumnsInfo)
        Dim tb = CType(cbListTables.SelectedItem, TableNameInfo)
        SelectedTableData = New TableNameInfo
        SetTableData(tb)


        For Each col In tb.ListColumn
            listColumnsob.Add(col)
        Next
        lbListColumns.ItemsSource = listColumnsob
    End Sub
    Private Sub SetTableData(tb As TableNameInfo)
        With tb
            SelectedTableData.TableName = .TableName
            SelectedTableData.StrConnection = .StrConnection
            SelectedTableData.SchemaTable = .SchemaTable
            SelectedTableData.TableType = .TableType
        End With
        VM.TableToProcess = SelectedTableData
    End Sub
    Private Sub CopyColumnsButton_Click(sender As Object, e As RoutedEventArgs) Handles CopyColumnsButton.Click
        Dim tb = CType(cbListTables.SelectedItem, TableNameInfo)


        If lbListColumns.SelectedItems.Count > 0 Then
            For Each item In lbListColumns.SelectedItems
                Dim selecCol = CType(item, ColumnsInfo)
                listSelectedColumnsob.Add(selecCol)
                SelectedTableData.ListColumn.Add(selecCol)
            Next
        End If


    End Sub
End Class
