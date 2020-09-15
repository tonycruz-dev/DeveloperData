Imports DBExtenderLib
Imports System.Text

Public Class HelperPartialClass

    Public Shared Function CreateMainPartialClass(ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("  Partial Public Class " & linqDb.DatabaseName & "Context")
        Dim ListTb = (From stb In linqDb.ListTable Where stb.TableType = "Table" Select stb).ToList
        Try
            For Each DT In ListTb
                Dim pColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

                sb.AppendLine("'-------------------------------------------------------------")
                sb.AppendLine("#Region " & (DT.TableName & " Table Management ".QT))
                sb.AppendLine("'-------------------------------------------------------------")
                sb.AppendLine("    Private Sub Insert" & DT.TableSingularize & "(ByVal instance As " & DT.TableSingularize & ")")
                sb.AppendLine("        Try")
                sb.AppendLine("            Dim cmd As IDbCommand = Me.Connection.CreateCommand")
                sb.AppendLine("            Dim StrInsert As String =" & CreateInsert(DT).QT)
                sb.AppendLine("            cmd.Transaction = Me.Transaction")
                sb.AppendLine("            cmd.CommandText = StrInsert")

                Dim RecNum As Integer = 0
                Dim qc = From col In DT.ListColumn Where col.IsAutoincrement = False _
                         Select col

                Dim StrLoad As String = ""
                For Each col As ColumnsInfo In qc
                    sb.AppendLine("            cmd.Parameters.Add(New OleDbParameter(" & ("@" & col.ColumnValue).QT & ", instance." & col.ColumnValue & "))")
                Next
                sb.AppendLine("            cmd.ExecuteNonQuery()")
                sb.AppendLine("            cmd = Me.Connection.CreateCommand")
                sb.AppendLine("            cmd.Transaction = Me.Transaction")
                sb.AppendLine("            cmd.CommandText = " & "SELECT @@IDENTITY".QT)
                sb.AppendLine("            instance." & pColumn.ColumnName & "= Convert.ToInt32(cmd.ExecuteScalar())")
                sb.AppendLine("        Catch ex As OleDbException")
                sb.AppendLine("            MsgBox(ex.Message)")
                sb.AppendLine("        End Try")
                sb.AppendLine("    End Sub")

                Dim UpdateColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = False Select c).ToList
                sb.AppendLine("    Private Sub Update" & DT.TablePluralize & "(ByVal instance As " & DT.TableSingularize & ")")
                sb.AppendLine("        Dim cmd As IDbCommand = Me.Connection.CreateCommand")
                sb.AppendLine("        Dim SqlUpdate As String = " & (CreateUpdate(DT).QT))
                sb.AppendLine("        cmd.Transaction = Me.Transaction")
                sb.AppendLine("        cmd.CommandText = SqlUpdate")
                For Each col As ColumnsInfo In UpdateColumn
                    If Not col.IsAutoincrement Then
                        If col.TypeVB = "Date" Then
                            sb.AppendLine("         Dim op" & col.ColumnValue & " As New OleDbParameter(" & """" & "@" & col.ColumnValue & """" & ", OleDbType.DBTimeStamp)")
                           
                            sb.Append("         op" & col.ColumnValue & ".Value = instance." & col.ColumnValue & ".Value.Year " & "&" & """" & "/" & """" & " & ")
                            sb.Append("instance." & col.ColumnValue & ".Value.Month &" & """" & "/" & """" & " & ")
                            sb.AppendLine("instance." & col.ColumnValue & ".Value.Day")
                            sb.AppendLine("         cmd.Parameters.Add(op" & col.ColumnValue & ")")
                        Else
                            sb.AppendLine("            cmd.Parameters.Add(New OleDbParameter(" & ("@" & col.ColumnValue).QT & ", instance." & col.ColumnValue & "))")

                        End If
                    End If
                Next
                sb.AppendLine("            cmd.Parameters.Add(New OleDbParameter(" & ("@" & pColumn.ColumnValue).QT & ", instance." & pColumn.ColumnValue & "))")
                sb.AppendLine("         cmd.ExecuteNonQuery()")
                sb.AppendLine("     End Sub")
                sb.AppendLine("    Private Sub Delete" & DT.TableSingularize & "(ByVal instance As " & DT.TableSingularize & ")")
                sb.AppendLine("        Dim sqldelete As String =" & ("DELETE FROM [" & DT.TableName & "]  WHERE [" & pColumn.ColumnValue & "] = @" & pColumn.ColumnValue).QT)
                sb.AppendLine("         Dim cmd As IDbCommand = Me.Connection.CreateCommand")
                sb.AppendLine("         cmd.Transaction = Me.Transaction")
                sb.AppendLine("         cmd.CommandText = sqldelete")
                sb.AppendLine("         cmd.Parameters.Add(New OleDbParameter(" & ("@" & pColumn.ColumnValue).QT & ", instance." & pColumn.ColumnValue & "))")
                sb.AppendLine("        cmd.ExecuteNonQuery()")
                sb.AppendLine("    End Sub")
                sb.AppendLine()
                sb.AppendLine("#End Region")
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        sb.AppendLine("End Class")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Public Shared Function CreatePartialClass(ByVal DBName As String, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim pColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sb.AppendLine("  Partial Public Class " & DBName & "Context")
        sb.AppendLine("#Region " & DT.TableName.QT)
        sb.AppendLine("    Private Sub Insert" & DT.TableSingularize & "(ByVal instance As " & DT.TableSingularize & ")")
        sb.AppendLine("        Try")
        sb.AppendLine("            Dim cmd As IDbCommand = Me.Connection.CreateCommand")
        sb.AppendLine("            Dim StrInsert As String =" & CreateInsert(DT).QT)
        sb.AppendLine("            cmd.Transaction = Me.Transaction")
        sb.AppendLine("            cmd.CommandText = StrInsert")

        Dim RecNum As Integer = 0
        Dim qc = From col In DT.ListColumn Where col.IsAutoincrement = False _
                 Select col
        Dim StrLoad As String = ""
       
        For Each col As ColumnsInfo In qc
            If Not col.IsAutoincrement Then
                If col.TypeVB = "Date" Then

                    sb.AppendLine("        If instance." & col.ColumnValue & " Is Nothing Then")
                    sb.AppendLine("            Dim op" & col.ColumnValue & " As New OleDbParameter(" & ("@" & col.ColumnValue).QT & " , DBNull.Value)")
                    sb.AppendLine("            cmd.Parameters.Add(op" & col.ColumnValue & ")")
                    sb.AppendLine("        Else")
                    sb.AppendLine("            Dim op" & col.ColumnValue & " As New OleDbParameter(" & ("@" & col.ColumnValue).QT & ", OleDbType.DBTimeStamp)")
                    sb.AppendLine("            op" & col.ColumnValue & ".Value = instance." & col.ColumnValue & ".Value.Year &" & "/".QT & "& instance." & col.ColumnValue & ".Value.Month & " & "/".QT & " & instance." & col.ColumnValue & ".Value.Day")
                    sb.AppendLine("            cmd.Parameters.Add(op" & col.ColumnValue & ")")
                    sb.AppendLine("      End If")



                Else
                    sb.AppendLine("   If instance." & col.ColumnValue & " Is Nothing Then")
                    sb.AppendLine("      cmd.Parameters.Add(New OleDbParameter(" & ("@" & col.ColumnValue).QT & " , DBNull.Value))")
                    sb.AppendLine("   Else")
                    sb.AppendLine("      cmd.Parameters.Add(New OleDbParameter(" & ("@" & col.ColumnValue).QT & ", instance." & col.ColumnValue & "))")
                    sb.AppendLine("   End If")

                End If
            End If
        Next

        sb.AppendLine("            cmd.ExecuteNonQuery()")
        sb.AppendLine("            cmd = Me.Connection.CreateCommand")
        sb.AppendLine("            cmd.Transaction = Me.Transaction")
        sb.AppendLine("            cmd.CommandText = " & "SELECT @@IDENTITY".QT)
        If Not pColumn Is Nothing Then
            sb.AppendLine("            instance." & pColumn.ColumnValue & "= Convert.ToInt32(cmd.ExecuteScalar())")
        Else
            sb.AppendLine("'            instance.= Convert.ToInt32(cmd.ExecuteScalar())")

        End If

        sb.AppendLine("        Catch ex As OleDbException")
        sb.AppendLine("            MsgBox(ex.Message)")
        sb.AppendLine("        End Try")
        sb.AppendLine("    End Sub")
        Dim UpdateColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = False Select c).ToList

        sb.AppendLine("    Private Sub Update" & DT.TableSingularize & "(ByVal instance As " & DT.TableSingularize & ")")
        sb.AppendLine("        Dim cmd As IDbCommand = Me.Connection.CreateCommand")
        sb.AppendLine("        Dim SqlUpdate As String = " & CreateUpdate(DT).QT)
        sb.AppendLine("        cmd.Transaction = Me.Transaction")
        sb.AppendLine("        cmd.CommandText = SqlUpdate")
        For Each col As ColumnsInfo In UpdateColumn
            If Not col.IsAutoincrement Then
                If col.TypeVB = "Date" Then

                    sb.AppendLine("        If instance." & col.ColumnValue & " Is Nothing Then")
                    sb.AppendLine("            Dim op" & col.ColumnValue & " As New OleDbParameter(" & ("@" & col.ColumnValue).QT & " , DBNull.Value)")
                    sb.AppendLine("            cmd.Parameters.Add(op" & col.ColumnValue & ")")
                    sb.AppendLine("        Else")
                    sb.AppendLine("            Dim op" & col.ColumnValue & " As New OleDbParameter(" & ("@" & col.ColumnValue).QT & ", OleDbType.DBTimeStamp)")
                    sb.AppendLine("            op" & col.ColumnValue & ".Value = instance." & col.ColumnValue & ".Value.Year &" & "/".QT & "& instance." & col.ColumnValue & ".Value.Month & " & "/".QT & " & instance." & col.ColumnValue & ".Value.Day")
                    sb.AppendLine("            cmd.Parameters.Add(op" & col.ColumnValue & ")")
                    sb.AppendLine("        End If")



                Else
                    sb.AppendLine("   If instance." & col.ColumnValue & " Is Nothing Then")
                    sb.AppendLine("        cmd.Parameters.Add(New OleDbParameter(" & ("@" & col.ColumnValue).QT & " , DBNull.Value))")
                    sb.AppendLine("   Else")
                    sb.AppendLine("       cmd.Parameters.Add(New OleDbParameter(" & ("@" & col.ColumnValue).QT & ", instance." & col.ColumnValue & "))")
                    sb.AppendLine("   End If")

                End If
            End If
        Next
        sb.AppendLine("            cmd.Parameters.Add(New OleDbParameter(" & """" & "@" & pColumn.ColumnValue & """" & ", instance." & pColumn.ColumnValue & "))")
        sb.AppendLine("         cmd.ExecuteNonQuery()")
        sb.AppendLine("     End Sub")
        sb.AppendLine("    Private Sub Delete" & DT.TableSingularize & "(ByVal instance As " & DT.TableSingularize & ")")
        sb.AppendLine("        Dim sqldelete As String =" & ("DELETE FROM [" & DT.TableName & "]  WHERE [" & pColumn.ColumnName & "] = @" & pColumn.ColumnValue).QT)
        sb.AppendLine("         Dim cmd As IDbCommand = Me.Connection.CreateCommand")
        sb.AppendLine("         cmd.Transaction = Me.Transaction")
        sb.AppendLine("         cmd.CommandText = sqldelete")
        sb.AppendLine("         cmd.Parameters.Add(New OleDbParameter(""" & "@" & pColumn.ColumnValue & """" & ", instance." & pColumn.ColumnValue & "))")
        sb.AppendLine("        cmd.ExecuteNonQuery()")
        sb.AppendLine("    End Sub")
        sb.AppendLine("#End Region ")
        sb.AppendLine("End Class")
        sb.AppendLine()
        Return sb.ToString
    End Function
    Friend Shared Function CreateInsert(ByVal DT As TableNameInfo) As String
        Dim sbInsert As New StringBuilder("INSERT INTO [" & DT.TableName & "](")
        Dim sbValue As New StringBuilder(" VALUES(")


        Dim qc = From col In DT.ListColumn Where col.IsAutoincrement = False _
                 Select col
        For Each col As ColumnsInfo In qc
            sbInsert.Append("[" & col.ColumnName & "],")
            sbValue.Append("@" & col.ColumnValue & ",")
        Next
        Dim mylastComar = sbInsert.ToString.LastIndexOf(",")
        Dim StrInsert = sbInsert.Remove(mylastComar, 1).ToString & ")"
        mylastComar = sbValue.ToString.LastIndexOf(",")
        Dim StrValue = sbValue.Remove(mylastComar, 1).ToString & ")"
        Return StrInsert.ToString & StrValue.ToString

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

