Imports DBExtenderLib
Imports System.Text

Public Class ManageSilverlight
    Public Shared Function CreateDatagridColumns(ByVal DT As TableNameInfo) As String

        Dim sb As New StringBuilder
        sb.AppendLine("     <DataGridTemplateColumn x:Name=" & """" & "ColOpen" & DT.TableValue & """" & " Width=" & """80""" & " >")
        sb.AppendLine("                 <DataGridTemplateColumn.CellTemplate>")
        sb.AppendLine("                    <DataTemplate>")
        sb.AppendLine("                        <Button Height=" & "20".QT & " Width=" & "20".QT & " Click=" & """Open" & DT.TableValue & """" & "  >")
        sb.AppendLine("                            <Button.Content>")
        sb.AppendLine("                                <Image Source=" & """../Images/Pencils.png""" & "/>")
        sb.AppendLine("                            </Button.Content>")
        sb.AppendLine("                        </Button>")
        sb.AppendLine("                    </DataTemplate>")
        sb.AppendLine("                </DataGridTemplateColumn.CellTemplate>")
        sb.AppendLine("            </DataGridTemplateColumn>")
        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("      <DataGridTextColumn x:Name=" & """" & R.ColumnValue & "Column" & """" & " Binding=" & """" & "{Binding Path=" & R.ColumnValue & ", Mode=OneWay}" & """" & " Header=" & """" & R.ColumnName & """" & " IsReadOnly=" & """True""" & " Width=" & """100""" & " />")
        Next
        Return sb.ToString

    End Function
    Public Shared Function CreateWPFDatagridColumns(ByVal DT As TableNameInfo) As String

        Dim sb As New StringBuilder
        sb.AppendLine("<DataGrid Grid.Row=" & "1".QT & " AutoGenerateColumns=" & "False".QT & " EnableRowVirtualization=" & "True".QT & " IsReadOnly=" & "True".QT & " HorizontalAlignment=" & "Stretch".QT & " Margin=" & "5".QT & " Name=" & "StockDataGrid".QT & " RowDetailsVisibilityMode=" & "VisibleWhenSelected".QT & " VerticalAlignment=" & "Stretch".QT & " AlternatingRowBackground=" & "GhostWhite".QT & "  AlternationCount=" & "2".QT & " GridLinesVisibility=" & "Horizontal".QT & " HorizontalGridLinesBrush=" & "#FFBEB9B9".QT & " SelectionMode=" & "Single".QT & " >")
        sb.AppendLine(" <DataGrid.Columns>")
        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("      <DataGridTextColumn x:Name=" & (R.ColumnValue & "Column").QT & " Binding=" & ("{Binding Path=" & R.ColumnValue & ", Mode=OneWay}").QT & " Header=" & (R.ColumnName).QT & " IsReadOnly=" & "True".QT & " Width=" & "100".QT & " />")
        Next
        sb.AppendLine(" </DataGrid.Columns>")
        sb.AppendLine("</DataGrid>")
        sb.AppendLine(" <StackPanel Grid.Row=" & "0".QT & " HorizontalAlignment=" & "Stretch".QT & "  Margin=" & "10,0,0,0".QT & " Orientation=" & "Horizontal".QT & " Name=" & ("StackPanelSearch" & DT.TableValue).QT & " VerticalAlignment=" & "Stretch".QT & ">")
        sb.AppendLine("       <TextBlock Height=" & "30".QT & " Name=" & ("TextBlockSearch" & DT.GetPrimaryKey.ColumnName).QT & " Text=" & ("Search " & DT.GetPrimaryKey.ColumnName & ":").QT & " Margin=" & "5,0".QT & " />")
        sb.AppendLine("       <TextBox Height=" & "30".QT & " Name=" & ("TextBoxSearch" & DT.GetPrimaryKey.ColumnName).QT & " Width=" & "150".QT & " Margin=" & "0,0,10,0".QT & " />")
        sb.AppendLine("       <Button Content=" & "Search".QT & " Height=" & "35".QT & " Name=" & ("ButtonSearch" & DT.GetPrimaryKey.ColumnName).QT & " Width=" & "75".QT & " />")
        sb.AppendLine("       <TextBlock Height=" & "30".QT & " Name=" & "TextBlockSearchDescription".QT & " Text=" & "Search Description :".QT & " Margin=" & "5,0".QT & " />")
        sb.AppendLine("       <TextBox Height=" & "30".QT & " Name=" & "TextBoxSearchDescription".QT & " Width=" & "220".QT & "  Margin=" & "0,0,10,0".QT & " />")
        sb.AppendLine("       <Button Content=" & "Search Description".QT & " Height=" & "35".QT & " Name=" & "ButtonSearchDescription".QT & "  Width=" & "150".QT & " />")
        sb.AppendLine("   </StackPanel>")


        Return sb.ToString

    End Function
    Public Shared Function CreateDataTemplate(ByVal DT As TableNameInfo) As String

        Dim sb As New StringBuilder

        sb.AppendLine("<DataTemplate x:Key=" & QT("IT" & DT.TableValue) & ">")
        sb.AppendLine("      <StackPanel Orientation=" & QT("Horizontal") & ">")
        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         <TextBlock Padding=" & QT("2") & " Text=" & QT("{Binding " & R.ColumnValue & "}") & " />")
        Next
        sb.AppendLine("      </StackPanel>")
        sb.AppendLine("   </DataTemplate>")
        sb.AppendLine()
        sb.AppendLine()
        sb.AppendLine("<ListBox x:Name=" & QT("LB" & DT.TableValue) & " HorizontalAlignment=" & QT("Left") & " ItemTemplate=" & QT("{DynamicResource " & "IT" & DT.TableValue & "}") & "  Margin=" & QT("1,1,1,1") & " Width=" & QT("224") & "/>")
        Return sb.ToString
    End Function
    Private Shared Function QT(Value As String) As String
        Return """" & Value & """"
    End Function
    Public Shared Function CreateFrmgrid(ByVal DT As TableNameInfo) As String
        Dim sbgrid As New StringBuilder
        Dim sbtxtBlock As New StringBuilder
        Dim sbtxtBox As New StringBuilder

        sbgrid.AppendLine("<Grid x:Name=" & QT("grid" & DT.TableValue) & " Margin=" & "5".QT & ">")
        sbgrid.AppendLine("        <Grid.RowDefinitions>")
        Dim RowIndex As Integer = 0
        For Each col In DT.ListColumn
            sbgrid.AppendLine("         <RowDefinition Height=" & "Auto".QT & " />")
            sbtxtBlock.AppendLine("          <TextBlock  Grid.Column=" & "0".QT & " Grid.Row=" & RowIndex.ToString.QT & " Text=" & (col.ColumnValue).QT & "/>")
            sbtxtBox.AppendLine("            <TextBox  Grid.Column=" & "1".QT & " Width=" & "150".QT & " Grid.Row=" & RowIndex.ToString.QT & " Text=" & ("{Binding Path=" & col.ColumnName & "}").QT & " />")
            RowIndex += 1
        Next
        sbgrid.AppendLine("        </Grid.RowDefinitions>")
        sbgrid.AppendLine("         <Grid.ColumnDefinitions>")
        sbgrid.AppendLine("               <ColumnDefinition Width=" & "110".QT & " />")
        sbgrid.AppendLine("               <ColumnDefinition Width=" & "Auto".QT & " />")
        sbgrid.AppendLine("         </Grid.ColumnDefinitions>")
        sbgrid.AppendLine(sbtxtBlock.ToString)
        sbgrid.AppendLine(sbtxtBox.ToString)
        sbgrid.AppendLine("</Grid>")
        Return sbgrid.ToString
    End Function
    Public Shared Function CreateDataListFrmgrid(ByVal DT As TableNameInfo) As String
        Dim sbgrid As New StringBuilder
        Dim sbtxtBlock As New StringBuilder
        Dim sbtxtBox As New StringBuilder

        sbgrid.AppendLine("<ListBox x:Name=" & QT(DT.TableValue & "ListBox") & " Margin=" & "2".QT & " Background=" & "{x:Null}".QT & " BorderBrush=" & "#FF42BDEE".QT & ">")
        sbgrid.AppendLine("    <ListBox.ItemTemplate>")
        sbgrid.AppendLine("        <DataTemplate>")


        sbgrid.AppendLine("<Grid x:Name=" & QT("grid" & DT.TableValue) & " Margin=" & "2".QT & ">")
        sbgrid.AppendLine("        <Grid.RowDefinitions>")
        Dim RowIndex As Integer = 0
        For Each col In DT.ListColumn
            sbgrid.AppendLine("         <RowDefinition Height=" & "Auto".QT & " />")
            sbtxtBlock.AppendLine("          <TextBlock  Grid.Column=" & "0".QT & " Grid.Row=" & RowIndex.ToString.QT & "Text=" & col.ColumnValue.QT & "/>")
            sbtxtBox.AppendLine("            <TextBlock  Grid.Column=" & "1".QT & " Width=" & "150".QT & " Grid.Row=" & QT(RowIndex) & " Text=" & QT("{Binding Path=" & col.ColumnName & "}") & " />")
            RowIndex += 1
        Next
        sbgrid.AppendLine("        </Grid.RowDefinitions>")
        sbgrid.AppendLine("         <Grid.ColumnDefinitions>")
        sbgrid.AppendLine("               <ColumnDefinition Width=" & "110".QT & " />")
        sbgrid.AppendLine("               <ColumnDefinition Width=" & "Auto".QT & " />")
        sbgrid.AppendLine("         </Grid.ColumnDefinitions>")
        sbgrid.AppendLine(sbtxtBlock.ToString)
        sbgrid.AppendLine(sbtxtBox.ToString)
        sbgrid.AppendLine("</Grid>")
        sbgrid.AppendLine(" </DataTemplate>")
        sbgrid.AppendLine("</ListBox.ItemTemplate>")
        sbgrid.AppendLine(" </ListBox>")
        Return sbgrid.ToString
    End Function
End Class
