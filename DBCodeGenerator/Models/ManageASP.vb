Imports System.Text
Imports DBExtenderLib

Public Class ManageASP



    Public Shared Function CreateFormViewEdit(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("<asp:FormView ID=" & StrQtt("FV" & DT.TableValue) & " runat=" & StrQtt("server") & " DataSourceID=" & StrQtt("ODS" & DT.TableValue) & " DefaultMode=" & StrQtt("Edit") & " >")
        sb.AppendLine("      <EditItemTemplate>")
        sb.AppendLine("        <table>")
        For Each Col As ColumnsInfo In DT.ListColumn
            If Col.TypeOfControl = "ComboBox" Then
                sb.AppendLine(CreateDropDownList(Col))
            Else
                sb.AppendLine("           <tr>")
                sb.AppendLine("               <td >")
                sb.AppendLine("               " & Col.ColumnName & " :")
                sb.AppendLine("             </td>")
                sb.AppendLine("                  <td>")
                sb.AppendLine("                   <asp:TextBox ID=" & StrQtt("Text" & Col.ColumnValue) & " runat=" & StrQtt("server") & "  Text=" & "'<%# Bind(" & """" & Col.ColumnValue & """" & ") %>' />")
                sb.AppendLine("                 </td>")
                sb.AppendLine("           </tr>")
            End If

        Next
        sb.AppendLine("            <tr>")
        sb.AppendLine("                <td colspan=" & StrQtt("2") & ">")
        sb.AppendLine("                 <asp:LinkButton ID=" & StrQtt("UpdateButton") & " runat=" & StrQtt("server") & " CausesValidation=" & StrQtt("True") & " CommandName=" & StrQtt("Update") & " Text=" & StrQtt("Update") & " />")
        sb.AppendLine("                 &nbsp;<asp:LinkButton ID=" & StrQtt("CancelButton") & " runat=" & StrQtt("server") & " CausesValidation=" & StrQtt("False") & " CommandName=" & StrQtt("Cancel") & " Text=" & StrQtt("Cancel") & " />")
        sb.AppendLine("                    </td>")
        sb.AppendLine("                <td>")
        sb.AppendLine("                    &nbsp;</td>")
        sb.AppendLine("           </tr>")
        sb.AppendLine("         </table>")
        sb.AppendLine("    </EditItemTemplate>")
        sb.AppendLine("</asp:FormView>")
        sb.AppendLine("        <asp:ObjectDataSource ID=" & StrQtt("ODS" & DT.TableValue) & " runat=" & StrQtt("server") & " DataObjectTypeName =" & StrQtt("[ApplicationName]." & DT.TableValue & "Info"))
        sb.AppendLine("        OldValuesParameterFormatString=" & StrQtt("original_{0}") & " SelectMethod=" & StrQtt("Get" & DT.TableValue))
        sb.AppendLine("        TypeName = " & StrQtt("[ApplicationName]." & DT.TableValue & "Management"))
        sb.AppendLine("        UpdateMethod=" & StrQtt("Update" & DT.TableValue) & ">")
        sb.AppendLine("    <SelectParameters>")
        sb.AppendLine("        <asp:QueryStringParameter Name=" & StrQtt("_" & DT.GetPrimaryKey.ColumnValue) & " QueryStringField=" & StrQtt(DT.GetPrimaryKey.ColumnValue))
        sb.AppendLine("            Type=" & StrQtt(DT.GetPrimaryKey.TypeVB) & " />")
        sb.AppendLine("    </SelectParameters>")
        sb.AppendLine("</asp:ObjectDataSource>")


        Return sb.ToString
       

    End Function
    Public Shared Function StrQtt(ByVal strVar As String) As String
        Return """" & strVar & """"
    End Function
    Private Shared Function CreateDropDownList(ByVal col As ColumnsInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine(" <tr>")
        sb.AppendLine("       <td align=" & StrQtt("right") & " valign=" & StrQtt("top") & ">")
        sb.AppendLine("   " & col.ComboBox.DisplayColumns)
        sb.AppendLine("     </td>")
        sb.AppendLine("          <td align=" & StrQtt("left") & ">")
        sb.AppendLine("            <asp:DropDownList ID=" & StrQtt("DDL" & col.ComboBox.RelateTable.TableValue) & " runat=" & StrQtt("server"))
        sb.AppendLine("                  DataSourceID=" & StrQtt("ODS" & col.ComboBox.RelateTable.TableValue) & " DataTextField=" & StrQtt(col.ComboBox.DisplayColumns))
        sb.AppendLine("                  DataValueField=" & StrQtt(col.ComboBox.SelectedValue) & ">")
        sb.AppendLine("              </asp:DropDownList>")
        sb.AppendLine("              <asp:ObjectDataSource ID=" & StrQtt("ODS" & col.ComboBox.RelateTable.TableValue) & " runat=" & StrQtt("server"))
        sb.AppendLine("                  OldValuesParameterFormatString=" & StrQtt("original_{0}") & " SelectMethod=" & StrQtt("Get" & col.ComboBox.RelateTable.TableValue))
        sb.AppendLine("                  TypeName=" & StrQtt("[ApplicationName].ManageDataPoperties") & "></asp:ObjectDataSource>")
        sb.AppendLine("         </td>")
        sb.AppendLine("   </tr>")
        Return sb.ToString
    End Function
   
    Public Function GetControlType(ByVal Col As ColumnsInfo) As String
        Dim sb As New StringBuilder
        Select Case Col.TypeSQL
            Case "numeric", "int", "nvarchar", "varchar", "ntext", "text", "Char"
                Return "TextBox"
            Case "money", "decimal", "single", "Int16", "smallint", "byte", "Double"
                Return "TextBox"
            Case "datetime"
                Return "DateTimePicker"
            Case "bit"
                Return "CheckBox"
            Case "byte"
                Return "PictureBox"
            Case Else
                Return "TextBox"
        End Select
        Return "TextBox"
    End Function
#Region " Create Class ObjectDataSource"
    Public Shared Function CreateClassObjectDataSource(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("<DataObject(True)>")
        sb.AppendLine("Public Class  " & DT.TableValue & "Management")

        'Select
        sb.AppendLine(ASPSelect(DT, DBName))
        'Update
        sb.AppendLine(ASPUpdate(DT, DBName))
        'Insert 
        sb.AppendLine(ASPInsert(DT, DBName))
        'Delete
        sb.AppendLine(ASPDelete(DT, DBName))

        sb.AppendLine("End Class ")
        Return sb.ToString
    End Function

#End Region

#Region " Object DataSource"

    Public Shared Function ASPSelect(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder

        sb.AppendLine(" #Region " & """" & DT.TableName & """")
        sb.AppendLine(" <DataObjectMethod(DataObjectMethodType.Select)>")
        sb.AppendLine(" Public Function Get" & DT.TableValue & "() As List(Of " & DT.TableValue & ")")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As List(Of " & DT.TableValue & ") = Nothing")
        sb.AppendLine("        Dim db As New " & DBName & "DataContext " & "' Dim db As New " & DBName & "DataContext(GetCnn)")

        sb.AppendLine("        Result" & DT.TableValue & " = (From " & Left(DT.TableName, 3) & " In db." & DT.TableValue & " Order BY " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)
        sb.AppendLine("                             Select " & Left(DT.TableName, 3) & ").ToList")
        'sb.AppendLine("         }).ToList")
        'For Each R As ColumnsInfo In DT.ListColumn
        '    sbselect.AppendLine("         ." & R.ColumnValue & " =  " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
        'Next
        sb.AppendLine("            Return Result" & DT.TableValue)
        sb.AppendLine("")
        sb.AppendLine("    End Function")
        sb.AppendLine("#End Region")
        sb.AppendLine()
        Return sb.ToString

    End Function
    Public Shared Function ASPUpdate(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("")
        sb.AppendLine(" #Region " & """" & "Update " & DT.TableName & """")
        sb.AppendLine("    Public Function Update" & DT.TableValue & "(ByVal _" & DT.TableValue & " As " & DT.TableValue & ") As Boolean")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")

        sb.AppendLine()
        sb.AppendLine("     Dim db As New " & DBName & "DataContext(GetCnn)")

        sb.AppendLine("     Dim qc = (From " & Left(DT.TableValue, 3) & " In db." & DT.TableValue & " Where " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = _" & DT.TableValue & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault")
        sb.AppendLine("    With qc")

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         ." & R.ColumnValue & " =  _" & DT.TableValue & "." & R.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("    db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & "Update " & DT.TableName & """")
        sb.AppendLine("")
        Return sb.ToString
    End Function
    Public Shared Function ASPDelete(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("")
        sb.AppendLine(" #Region " & """" & "Delete " & DT.TableName & """")
        sb.AppendLine("    Public Function Delete" & DT.TableValue & "(ByVal  _" & DT.TableValue & "  As " & DT.TableValue & ") As Boolean")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(" & """" & DT.StrConnection & """" & ")")
        sb.AppendLine("        Dim Result" & DT.TableValue & " As Boolean")
        sb.AppendLine()
        sb.AppendLine("        Dim db As New " & DBName & "DataContext(GetCnn)")

        sb.AppendLine("         Dim Result = (From " & Left(DT.TableValue, 3) & " In db." & DT.TableValue & " Where " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = _" & DT.TableValue & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault")
        sb.AppendLine("         db." & DT.TableValue & ".DeleteOnSubmit(Result)")
        sb.AppendLine("         db.SubmitChanges()")
        sb.AppendLine("         Result" & DT.TableValue & "= true ")
        sb.AppendLine("         Return Result" & DT.TableValue & "")
        sb.AppendLine("      End Function")
        sb.AppendLine("#End Region '" & """" & "Delete " & DT.TableName & """")
        sb.AppendLine("")
        Return sb.ToString
    End Function

    Public Shared Function ASPInsert(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine()
        sb.AppendLine(" #Region " & """" & "Insert " & DT.TableName & """")
        sb.AppendLine("    Public Function Insert" & DT.TableValue & "(ByVal _" & DT.TableValue & " As " & DT.TableValue & ") As Boolean")
        'sb.AppendLine("        Dim cnn As New OleDbConnection(ConfigurationManager.ConnectionStrings(""" & "NorthwindConnection""" & ").ConnectionString)")

        sb.AppendLine()
        sb.AppendLine("     Dim db As New " & DBName & "DataContext(GetCnn)")
        sb.AppendLine()

        sb.AppendLine("        Dim " & Left(DT.TableName, 3) & " As New " & DT.TableValue)
        sb.AppendLine("    With " & Left(DT.TableName, 3))

        For Each R As ColumnsInfo In DT.ListColumn
            sb.AppendLine("         ." & R.ColumnValue & " =  _" & DT.TableValue & "." & R.ColumnValue)
        Next
        sb.AppendLine("    End With")
        sb.AppendLine("        db." & DT.TableValue & ".InsertOnSubmit(" & Left(DT.TableValue, 3) & ")")
        sb.AppendLine("        db.SubmitChanges()")
        sb.AppendLine(" End Function")
        sb.AppendLine("#End Region '" & """" & " Insert " & DT.TableName & """")
        sb.AppendLine()
        Return sb.ToString
    End Function

#End Region

    
End Class
