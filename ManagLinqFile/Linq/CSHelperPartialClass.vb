Imports DBExtenderLib
Imports System.Text

Public Class CSHelperPartialClass


    Public Shared Function CreatePartialClass(ByVal DBName As String, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine(" using System.Data;")
        sb.AppendLine(" using System.Collections.Generic;")
        sb.AppendLine(" using System.Reflection;")
        sb.AppendLine(" using System.Linq;")
        sb.AppendLine(" using System.Linq.Expressions;")
        sb.AppendLine(" using System.Runtime.Serialization;")
        sb.AppendLine(" using System.ComponentModel;")
        sb.AppendLine(" using System.Data.Linq.Mapping;")
        sb.AppendLine(" using System.Data.Linq;")
        sb.AppendLine(" using System;")
        sb.AppendLine(" using System.Data.OleDb;")


        Dim pColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault


        sb.AppendLine("  public partial class Class " & DBName & "Context")
        sb.AppendLine("#region " & DT.TableName.QT)
        sb.AppendLine("    partial void Insert" & DT.TableSingularize & "(" & DT.TableSingularize & " instance)")
        sb.AppendLine("    {")
        sb.AppendLine("        try")
        sb.AppendLine("         {")
        sb.AppendLine("            IDbCommand cmd = this.Connection.CreateCommand();")
        sb.AppendLine("            string StrInsert =" & CreateInsert(DT).QT & ";")
        sb.AppendLine("            cmd.Transaction = this.Transaction;")
        sb.AppendLine("            cmd.CommandText = StrInsert;")

        Dim RecNum As Integer = 0
        Dim qc = From col In DT.ListColumn Where col.IsAutoincrement = False _
                 Select col
        Dim StrLoad As String = ""

        For Each col As ColumnsInfo In qc
            If Not col.IsAutoincrement Then
                If col.TypeVB = "Date" Then

                    sb.AppendLine("  if (instance." & col.ColumnValue & " == null)")
                    sb.AppendLine("  {")
                    sb.AppendLine("	     OleDbParameter op" & col.ColumnValue & " = new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", DBNull.Value);")
                    sb.AppendLine("      cmd.Parameters.Add(op" & col.ColumnValue & ");")
                    sb.AppendLine("  }")
                    sb.AppendLine("  else ")
                    sb.AppendLine("  {")
                    sb.AppendLine("	     OleDbParameter op" & col.ColumnValue & " = new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", OleDbType.DBTimeStamp);")
                    sb.AppendLine(" 	 op" & col.ColumnValue & ".Value = instance." & col.ColumnValue & ".Value.Year +" & "/".QT & "+ instance." & col.ColumnValue & ".Value.Month +" & "/".QT & " + instance." & col.ColumnValue & ".Value.Day;")
                    sb.AppendLine(" 	cmd.Parameters.Add(op" & col.ColumnValue & ");")
                    sb.AppendLine("  }")
                Else
                    sb.AppendLine("  if (instance." & col.ColumnValue & " == null)")
                    sb.AppendLine("  {")
                    sb.AppendLine("	     cmd.Parameters.Add(new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", DBNull.Value));")
                    sb.AppendLine("  }")
                    sb.AppendLine("  else ")
                    sb.AppendLine("  {")
                    sb.AppendLine("	    cmd.Parameters.Add(new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", instance." & col.ColumnValue & "));")
                    sb.AppendLine("   }")
                End If
            End If
        Next

        sb.AppendLine("            cmd.ExecuteNonQuery();")
        sb.AppendLine("            cmd = this.Connection.CreateCommand();")
        sb.AppendLine("            cmd.Transaction = this.Transaction;")

        sb.AppendLine("            cmd.CommandText = " & "SELECT @@IDENTITY".QT & ";")
        If Not pColumn Is Nothing Then
            sb.AppendLine("            instance." & pColumn.ColumnValue & "= Convert.ToInt32(cmd.ExecuteScalar());")
        Else
            sb.AppendLine("'            instance.= Convert.ToInt32(cmd.ExecuteScalar());")
        End If
        sb.AppendLine("       }")
        sb.AppendLine("       catch (OleDbException ex)")
        sb.AppendLine("       {")
        sb.AppendLine("        }")
       
        sb.AppendLine("}")
        Dim UpdateColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = False Select c).ToList

        sb.AppendLine("    partial void Update" & DT.TableSingularize & "(" & DT.TableSingularize & " instance)")
        sb.AppendLine("           {")
        sb.AppendLine("             IDbCommand cmd = this.Connection.CreateCommand();")
        sb.AppendLine("             string SqlUpdate =" & CreateUpdate(DT).QT & ";")
        sb.AppendLine("             cmd.Transaction = this.Transaction;")
        sb.AppendLine("             cmd.CommandText = SqlUpdate;")

        For Each col As ColumnsInfo In UpdateColumn
            If Not col.IsAutoincrement Then
                If col.TypeVB = "Date" Then

                    sb.AppendLine("  if (instance." & col.ColumnValue & " == null)")
                    sb.AppendLine("    {")
                    sb.AppendLine("	     OleDbParameter op" & col.ColumnValue & " = new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", DBNull.Value);")
                    sb.AppendLine("      cmd.Parameters.Add(op" & col.ColumnValue & ");")
                    sb.AppendLine("     }")
                    sb.AppendLine("  else ")
                    sb.AppendLine("     {")
                    sb.AppendLine("	     OleDbParameter op" & col.ColumnValue & " = new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", OleDbType.DBTimeStamp);")
                    sb.AppendLine(" 	 op" & col.ColumnValue & ".Value = instance." & col.ColumnValue & ".Value.Year +" & "/".QT & "+ instance." & col.ColumnValue & ".Value.Month +" & "/".QT & " + instance." & col.ColumnValue & ".Value.Day;")
                    sb.AppendLine(" 	 cmd.Parameters.Add(op" & col.ColumnValue & ");")
                    sb.AppendLine("      }")
                Else
                    sb.AppendLine("   if (instance." & col.ColumnValue & " == null)")
                    sb.AppendLine("     {")
                    sb.AppendLine("	      cmd.Parameters.Add(new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", DBNull.Value));")
                    sb.AppendLine("      }")
                    sb.AppendLine("    else ")
                    sb.AppendLine("      {")
                    sb.AppendLine("	       cmd.Parameters.Add(new OleDbParameter(" & ("@" & col.ColumnValue).QT & ", instance." & col.ColumnValue & "));")
                    sb.AppendLine("      }")
                End If
            End If
        Next

        sb.AppendLine("            cmd.Parameters.Add(new OleDbParameter(" & ("@" & pColumn.ColumnValue).QT & ", instance." & pColumn.ColumnValue & "));")
        sb.AppendLine("            cmd.ExecuteNonQuery();")
        sb.AppendLine("     }")
        sb.AppendLine("    partial void Delete" & DT.TableSingularize & "(" & DT.TableSingularize & " instance)")
        sb.AppendLine("          {")
        sb.AppendLine("           string sqldelete =" & ("DELETE FROM [" & DT.TableName & "]  WHERE [" & pColumn.ColumnName & "] =  @" & pColumn.ColumnValue).QT & ";")
        sb.AppendLine("           IDbCommand cmd = this.Connection.CreateCommand();")
        sb.AppendLine("           cmd.Transaction = this.Transaction;")
        sb.AppendLine("           cmd.CommandText = sqldelete;")

        sb.AppendLine("         cmd.Parameters.Add(new OleDbParameter(" & ("@" & pColumn.ColumnValue).QT & ", instance." & pColumn.ColumnValue & "));")
        sb.AppendLine("         cmd.ExecuteNonQuery();")
        sb.AppendLine("    }")
        sb.AppendLine("#endregion")
        sb.AppendLine("}")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Friend Shared Function CreateInsert(ByVal DT As TableNameInfo) As String
        Dim StrSQLINSERT As New StringBuilder("INSERT INTO [" & DT.TableName & "] (")
        Dim StrSQLVALUES As New StringBuilder(" VALUES (")
        Dim R As ColumnsInfo
        Dim RecNum As Integer = 0
        Dim qc = From col In DT.ListColumn Where col.IsAutoincrement = False _
                 Select col

        RecNum = 0

        For Each R In qc

            If RecNum = 0 Then
                StrSQLINSERT.Append("[" & R.ColumnName & "]")
                StrSQLVALUES.Append("@" & R.ColumnValue)
            ElseIf RecNum = qc.Count - 1 Then
                StrSQLINSERT.Append(",[" & R.ColumnName & "])")
                StrSQLVALUES.Append(",@" & R.ColumnValue & ")")
            Else
                StrSQLINSERT.Append(",[" & R.ColumnName & "]")
                StrSQLVALUES.Append(",@" & R.ColumnValue)
            End If
            RecNum += 1
        Next
        Dim strResults As String = StrSQLINSERT.ToString & StrSQLVALUES.ToString
        Return strResults
    End Function
    Public Shared Function CreateUpdate(ByVal DT As TableNameInfo) As String
        Dim Qcol = From c In DT.ListColumn Where c.IsPrimary_Key = False Select c
        Dim sbUpdate As New StringBuilder("UPDATE ")
        sbUpdate.Append("[" & DT.TableName & "] SET ")
        For Each col In Qcol
            sbUpdate.Append("[" & col.ColumnName & "] = @" & col.ColumnValue & ",")
        Next
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sbUpdate.Append(" WHERE " & colPk.ColumnName & "=@" & colPk.ColumnValue)
        Dim mylastComar = sbUpdate.ToString.LastIndexOf(",")
        sbUpdate.Remove(mylastComar, 1)
        Return sbUpdate.ToString
    End Function
    Friend Shared Function CreateUpdate(ByVal DT As TableNameInfo, ByVal FieldID As ColumnsInfo) As String
        Dim StrUpdate As New StringBuilder("UPDATE " & DT.TableName)
        Dim RecNum As Integer = 0
        Dim SetColumn As Boolean = False
        For Each R As ColumnsInfo In DT.ListColumn
            If FieldID Is Nothing Then
                Return ""
            End If
            If FieldID.ColumnName = R.ColumnName Then
                If RecNum = 0 Then
                    SetColumn = True
                End If
                RecNum += 1
            Else
                If SetColumn Then
                    StrUpdate.Append(" SET [" & R.ColumnName & "] = " & "@" & R.ColumnValue & ",")
                    SetColumn = False
                    RecNum += 1
                Else
                    If RecNum = 0 Then
                        StrUpdate.Append(" SET [" & R.ColumnName & "] = " & "@" & R.ColumnValue & ",")
                        RecNum += 1
                    Else
                        If RecNum = DT.ListColumn.Count - 1 Then
                            StrUpdate.Append("[" & R.ColumnName & "] = " & "@" & R.ColumnValue)
                        Else
                            StrUpdate.Append("[" & R.ColumnName & "] = @" & R.ColumnValue & ",")
                            RecNum += 1
                        End If
                    End If
                End If

            End If
        Next
        StrUpdate.Append(" WHERE [" & FieldID.ColumnName & "] = " & "@" & FieldID.ColumnValue)
        Return StrUpdate.ToString
    End Function
    Friend Shared Function CreateDelete(ByVal DT As TableNameInfo, ByVal FieldID As ColumnsInfo) As String
        Dim strDelete As String
        strDelete = "DELETE FROM [" & DT.TableName & "]  WHERE [" & FieldID.ColumnName & "] = " & "@" & FieldID.ColumnValue
        Return strDelete
    End Function
End Class

