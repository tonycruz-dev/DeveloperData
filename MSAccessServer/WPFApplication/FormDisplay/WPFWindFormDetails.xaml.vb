Imports DBExtenderLib
Imports System.Windows.Markup
Imports <xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
Imports <xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
Imports System.Windows.Data

Public Class WPFWindFormDetails
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
    Private Sub WPFWindFormDetails_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        Dim UI = <Grid xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                     xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                     Name="Grid1">
                     <Grid.ColumnDefinitions>
                         <ColumnDefinition Width="100*"/>
                         <ColumnDefinition Width="200*"/>
                     </Grid.ColumnDefinitions>
                     <StackPanel Name="StackLabels" Margin="3">
                         <%= From column In TableObj.ListColumn _
                             Where column.IsPrimary_Key = False _
                             Select <Label
                                        Height="28"
                                        Name=<%= column.ColumnValue & "Label" %>
                                        HorizontalContentAlignment="Right">
                                        <%= column.ColumnValue %>:</Label> %>
                     </StackPanel>
                     <StackPanel Grid.Column="1" Name="StackFields" Margin="3">
                         <%= From column In TableObj.ListColumn _
                             Where column.IsPrimary_Key = False _
                             Select GetUIElement(column) %>
                     </StackPanel>
                 </Grid>

        Me.DynamicContent.Content = XamlReader.Load(UI.CreateReader())
        Dim dataTb = dsHelper.GetDataSet(TableObj.GetSelectTable, TableObj.StrConnection).Tables(0)

        Me.DataContext = dataTb
        View = CollectionViewSource.GetDefaultView(dataTb)
    End Sub


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
End Class
