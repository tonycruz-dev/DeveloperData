Imports DBExtenderLib
Imports System.Collections.ObjectModel

Public Class UCColumnShow
    Private _db As DatabaseNameInfo
    Private listSelectedColumnsob As New ObservableCollection(Of columnSelected)
    Private DisplayCBTables As New List(Of TableNameInfo)
    Private listColumnsob As ObservableCollection(Of ColumnsInfo)
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
    Public Property VM As SimpleWizardVM
    Private Sub DisplayListTable()
        If _db IsNot Nothing Then
            cbListTables.ItemsSource = _db.ListTable
        End If
    End Sub

    Private Sub cbListTables_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbListTables.SelectionChanged
        listColumnsob = New ObservableCollection(Of ColumnsInfo)
        Dim tb = CType(cbListTables.SelectedItem, TableNameInfo)
        ' SetTableData(tb)
        For Each col In tb.ListColumn
            listColumnsob.Add(col)
        Next
        lbListColumns.ItemsSource = listColumnsob
    End Sub
    Private Sub SetTableData(tb As TableNameInfo)
        With tb
            _VM.TableShowColumn.DisplayName = .TableName
            _VM.TableShowColumn.TableName = .TableValue
        End With
    End Sub
    Private Sub CopyColumnsButton_Click(sender As Object, e As RoutedEventArgs) Handles CopyColumnsButton.Click
        Dim tb = CType(cbListTables.SelectedItem, TableNameInfo)


        If lbListColumns.SelectedItems.Count > 0 Then
            For Each item In lbListColumns.SelectedItems
                Dim selecCol = CType(item, ColumnsInfo)
                Dim col = New columnSelected With {.ColumnName = selecCol.ColumnValue, .DisplayName = selecCol.ColumnName}
                listSelectedColumnsob.Add(col)
                _VM.TableShowColumn.Columns.Add(col)
            Next
        End If
    End Sub

    Private Sub listboxSelectedColumns_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listboxSelectedColumns.SelectionChanged
        Dim col = CType(listboxSelectedColumns.SelectedItem, columnSelected)
        TextDisplay.Text = col.DisplayName
    End Sub

    Private Sub TextDisplay_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TextDisplay.TextChanged
        Dim col = CType(listboxSelectedColumns.SelectedItem, columnSelected)
        col.DisplayName = TextDisplay.Text
    End Sub
End Class
