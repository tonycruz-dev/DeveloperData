Imports System.Text
Imports DBExtenderLib

Public Class LinqSqlHelper
    Public Shared Function CreateSQLDELETECommandParam(ByVal DT As TableNameInfo) As String
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        Dim sbDelete As New StringBuilder("DELETE FROM " & DT.TableName & " WHERE " & colPk.ColumnName & " = @" & colPk.ColumnName)
        Return sbDelete.ToString
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
    Public Shared Function CreateInsert(ByVal DT As TableNameInfo) As String

        Dim sbInsert As New StringBuilder("INSERT INTO [" & DT.TableName & "](")
        Dim sbValue As New StringBuilder(" VALUES(")
        'Dim R As ColumnsInfo
        Dim RecNum As Integer = 0

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

End Class
