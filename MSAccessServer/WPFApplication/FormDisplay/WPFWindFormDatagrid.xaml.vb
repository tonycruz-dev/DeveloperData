Imports DBExtenderLib
Imports System.Windows.Markup
Imports <xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
Imports <xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
Imports System.Windows.Data

Public Class WPFWindFormDatagrid
    Private TableObj As TableNameInfo
    Private View As CollectionView

    Public Sub New(ByVal TableObj_ As TableNameInfo)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TableObj = TableObj_
    End Sub

    Private Function GetUIElement(ByVal column As ColumnsInfo) As XElement
        Select Case column.TypeSQL
            Case "datetime", "int"
                Return <DataGridTextColumn
                           x:Name=<%= column.ColumnValue & "Column" %>
                           Binding=<%= "{Binding Path=" & column.ColumnValue & "}" %>
                           Header=<%= column.ColumnName %>
                           Width="SizeToHeader"/>
                '<DataGridCheckBoxColumn Header="Enabled" Width=".5*" Binding="{Binding Path=IsEnabled}"/> 

            Case "bit"
                Return <DataGridCheckBoxColumn
                           Header=<%= column.ColumnName %>
                           Width="SizeToHeader"
                           Binding=<%= "{Binding Path=" & column.ColumnValue & "}" %>/>
            Case Else
                Return <DataGridTextColumn
                           x:Name=<%= column.ColumnValue & "Column" %>
                           Binding=<%= "{Binding Path=" & column.ColumnValue & "}" %>
                           Header=<%= column.ColumnName %>
                           Width="SizeToHeader"/>
        End Select
    End Function
    Private Function GetUIElementNew(ByVal column As ColumnsInfo) As XElement
        Select Case column.TypeOfControl
            Case "datetime", "int"
                Return <TextBox
                           Height="28"
                           Name=<%= "txt" & column.ColumnValue %>
                           Text=<%= "{Binding Path=" & column.ColumnValue & ", ValidatesOnDataErrors=True}" %>/>
            Case "bit"
                Return <CheckBox
                           HorizontalContentAlignment="Left"
                           Name=<%= "chk" & column.ColumnValue %>
                           IsChecked=<%= "{Binding Path=" & column.ColumnValue & ", ValidatesOnDataErrors=True}" %>>
                           <%= column.ColumnValue %>
                       </CheckBox>
            Case Else
                Return <TextBox
                           Height="28"
                           Name=<%= "txt" & column.ColumnValue %>
                           MaxLength=<%= column.Size %>
                           Text=<%= "{Binding Path=" & column.ColumnValue & ", ValidatesOnDataErrors=True}" %>/>
        End Select
    End Function


    Private Sub ButtonFirst_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ButtonFirst.Click
        View.MoveCurrentToFirst()
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ButtonPrevious.Click
        If View.CurrentPosition > 0 Then
            View.MoveCurrentToPrevious()
        End If
    End Sub
    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ButtonNext.Click
        If View.CurrentPosition < View.Count - 1 Then
            View.MoveCurrentToNext()
        End If
    End Sub

    Private Sub ButtonLast_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ButtonLast.Click
        View.MoveCurrentToLast()
    End Sub

    Private Sub WPFWindFormDatagrid_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim UI = <Grid xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                     xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                     Name="Grid1">
                     <Grid.RowDefinitions>
                         <RowDefinition Height="50"/>
                         <RowDefinition Height="100*"/>
                     </Grid.RowDefinitions>
                     <DataGrid Grid.Row="1" AutoGenerateColumns="False" EnableRowVirtualization="True" Height="460" HorizontalAlignment="Left" ItemsSource="{Binding}" Name="CustomersDataGrid" VerticalAlignment="Top" Width="580">
                         <DataGrid.Columns>
                             <%= From column In TableObj.ListColumn _
                                 Where column.IsPrimary_Key = False _
                                 Select GetUIElement(column) %>
                         </DataGrid.Columns>
                     </DataGrid>
                 </Grid>

        Me.DynamicContent.Content = XamlReader.Load(UI.CreateReader())
        Dim dataTb = dsHelper.GetDataSet(TableObj.GetSelectTable, TableObj.StrConnection).Tables(0)

        Me.DataContext = dataTb
        View = CollectionViewSource.GetDefaultView(dataTb)
    End Sub
End Class
