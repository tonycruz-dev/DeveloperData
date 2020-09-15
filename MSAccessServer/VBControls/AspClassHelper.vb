Imports System.IO
Imports DBExtenderLib
Imports System.Text

Public Class AspClassHelper

    Public Shared Function CreateDBFromTemplate(ByVal DT As TableNameInfo, ByVal DGName As String) As String
        Dim DGTemplate As String = "\Templates\GridView.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)
        Dim _BoundField As String = ProcessBoundField(DT)
        SB.Replace("[TABLENAME]", DT.TableValue)
        SB.Replace("[BoundField]", _BoundField)
        SB.Replace("[ColumnID]", DT.GetPrimaryKey.ColumnName)
        sr.Close()
        Return SB.ToString()
    End Function
    Private Shared Function ProcessBoundField(ByVal DT As TableNameInfo) As String
        Dim SB As New StringBuilder
        Dim CountField As Integer = 0
        For Each R As ColumnsInfo In DT.ListColumn

            If R.TypeVB = "Boolean" Then
                If CountField = 0 Then
                    SB.AppendLine("<asp:CheckBoxField  DataField=" & """" & R.ColumnName & """" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                Else
                    SB.AppendLine("            <asp:CheckBoxField  DataField=" & """" & R.ColumnName & """" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                End If

            ElseIf R.TypeVB = "Decimal" Then
                If CountField = 0 Then
                    SB.AppendLine("<asp:BoundField DataField=" & """" & R.ColumnName & """" & " DataFormatString=" & """{0:c}""" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                Else
                    SB.AppendLine("            <asp:BoundField DataField=" & """" & R.ColumnName & """" & " DataFormatString=" & """{0:c}""" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                End If
            ElseIf R.TypeVB = "Date" Then
                If CountField = 0 Then
                    SB.AppendLine("<asp:BoundField DataField=" & """" & R.ColumnName & """" & " DataFormatString=" & """{0:d}""" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                Else
                    SB.AppendLine("            <asp:BoundField DataField=" & """" & R.ColumnName & """" & " DataFormatString=" & """{0:d}""" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                End If

            Else
                If CountField = 0 Then
                    SB.AppendLine("<asp:BoundField DataField=" & """" & R.ColumnName & """" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                Else
                    SB.AppendLine("            <asp:BoundField DataField=" & """" & R.ColumnName & """" & " HeaderText=" & """" & R.ColumnName & """" & " />")
                End If

            End If
            CountField += 1
        Next
        Return SB.ToString
    End Function


    Public Shared Function CreateTemplateTableCreate(ByVal DT As TableNameInfo, ByVal DBName As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("        <table>")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("           <tr>")
            sb.AppendLine("               <td >")
            sb.AppendLine("               " & Col.ColumnName & " :")
            sb.AppendLine("             </td>")
            sb.AppendLine("                  <td>")
            sb.AppendLine("                   <asp:TextBox ID=" & StrQtt("Text" & Col.ColumnValue) & " runat=" & StrQtt("server") & "  Text=" & "'<%# Bind(" & """" & Col.ColumnValue & """" & ") %>' />")
            sb.AppendLine("                 </td>")
            sb.AppendLine("           </tr>")
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

        sb.AppendLine(" ")
        sb.AppendLine(" ")

        sb.AppendLine("   Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load")
        sb.AppendLine("    If Not Page.IsPostBack Then")
        sb.AppendLine("     Dim db As New " & DBName & "DataContext(GetCnn)")

        sb.AppendLine("     Dim qc = (From " & Left(DT.TableValue, 3) & " In db." & DT.TableValue & " Where " & Left(DT.TableValue, 3) & "." & DT.GetPrimaryKey.ColumnValue & " = _" & DT.TableValue & "." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault")
        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        Text" & Col.ColumnValue & ".Text = qc." & Col.ColumnValue)
        Next
        sb.AppendLine("    End If")
        sb.AppendLine(" End Sub")

        sb.AppendLine(" Protected Sub ImageButton_Submit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Submit.Click")
        sb.AppendLine("    Try")
        sb.AppendLine("        Dim " & Left(DT.TableValue, 2) & " As New " & DT.TableValue)
        sb.AppendLine("        " & Left(DT.TableValue, 2) & "." & DT.GetPrimaryKey.ColumnName & "  = CInt(Request.QueryString(" & StrQtt(DT.GetPrimaryKey.ColumnName) & "))")

        For Each Col As ColumnsInfo In DT.ListColumn
            sb.AppendLine("        " & Left(DT.TableValue, 2) & "." & Col.ColumnName & " = HttpUtility.HtmlEncode(Text" & Col.ColumnName & ".Text)")
        Next

        sb.AppendLine("        Dim objbll As New " & DT.TableValue & "Management")
        sb.AppendLine("        objbll.Update" & DT.TableValue & "(" & Left(DT.TableValue, 2) & " )")
        sb.AppendLine("        Response.Redirect(" & StrQtt(DT.TableValue & "List.aspx)"))
        sb.AppendLine("    Catch ex As Exception")
        sb.AppendLine("    End Try")
        sb.AppendLine(" End Sub")


        Return sb.ToString



    End Function
    Public Shared Function StrQtt(ByVal strVar As String) As String
        Return """" & strVar & """"
    End Function
End Class
