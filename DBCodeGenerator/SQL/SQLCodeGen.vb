Imports System.Text
Imports DBExtenderLib

Public Class SQLCodeGen

    Public Shared Function CreateSelectWithLinq(ByVal DT As TableNameInfo) As String
        Dim Sp_SelectAll As New StringBuilder("SELECT ")
        Dim RecNum As Integer = 0

        Dim col = From c In DT.ListColumn Select c.ColumnName
        Dim count As Integer

        For Each row In col
            Sp_SelectAll.Append("[" & row & "]" & IIf(col.Count - 1 = count, "", ","))
            count += 1
        Next

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            Sp_SelectAll.Append(" From [" & DT.TableName & "] Order By [" & colPk.ColumnName & "]")
        Else
            Sp_SelectAll.Append(" From [" & DT.TableName & "]")
        End If
        Return Sp_SelectAll.ToString

    End Function
    Public Shared Function CreateSelectCommandTop10(ByVal DT As TableNameInfo) As String
        Dim sbSelect As New StringBuilder("SELECT TOP 1000 ")
        For Each col In DT.ListColumn
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            StrSelect &= " From [" & DT.TableName & "] Order By [" & colPk.ColumnName & "]"
        Else
            StrSelect &= " From [" & DT.TableName & "]"
        End If
        Return StrSelect
    End Function
    Public Shared Function CreateSelectCommandAll(ByVal DT As TableNameInfo) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In DT.ListColumn
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            StrSelect &= " From [" & DT.TableName & "] Order By [" & colPk.ColumnName & "]"
        Else
            StrSelect &= " From [" & DT.TableName & "]"
        End If
        Return StrSelect
    End Function
    Public Shared Function CreateSelectCommandAll(ByVal DT As TableNameInfo, cols As List(Of ColumnsInfo)) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In cols
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            If DT.SchemaTable <> "" Then
                StrSelect &= " From [" & DT.SchemaTable & "].[" & DT.TableName & "] Order By [" & colPk.ColumnName & "]"
            Else
                StrSelect &= " From [" & DT.TableName & "] Order By [" & colPk.ColumnName & "]"
            End If

        Else
            If DT.SchemaTable <> "" Then
                StrSelect &= " From [" & DT.SchemaTable & "].[" & DT.TableName & "]"
            Else
                StrSelect &= " From [" & DT.TableName & "]"
            End If

        End If
        Return StrSelect
    End Function
    Public Shared Function CreateSelectCommandAll(ByVal DT As TableNameInfo, cols As List(Of ColumnsInfo), CloseWhere As String) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In cols
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            If DT.SchemaTable <> "" Then
                StrSelect &= " From [" & DT.SchemaTable & "].[" & DT.TableName & "] " & CloseWhere & " Order By [" & colPk.ColumnName & "]"
            Else
                StrSelect &= " From [" & DT.TableName & "] " & CloseWhere & " Order By [" & colPk.ColumnName & "]"
            End If

        Else
            If DT.SchemaTable <> "" Then
                StrSelect &= " From [" & DT.SchemaTable & "].[" & DT.TableName & "]" & CloseWhere
            Else
                StrSelect &= " From [" & DT.TableName & "]" & CloseWhere
            End If

        End If
        Return StrSelect
    End Function
    Public Shared Function CreateSelectCommand(ByVal DT As TableNameInfo) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In DT.ListColumn
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            StrSelect &= " From [" & DT.TableName & "] Order By [" & colPk.ColumnName & "]"
        Else
            StrSelect &= " From [" & DT.TableName & "]"
        End If
        Return StrSelect
    End Function
    Public Shared Function CreateSelectByIdCommand(ByVal DT As TableNameInfo) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In DT.ListColumn
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        StrSelect &= " From [" & DT.TableName & "] WHERE " & colPk.ColumnName & "=@" & colPk.ColumnValue

        Return StrSelect
    End Function
    Public Shared Function CreateSelectFilter(ByVal DT As TableNameInfo, ByVal colSearch As ColumnsInfo) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In DT.ListColumn
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            StrSelect &= " From [" & DT.TableName & "] WHERE " & colSearch.ColumnName & " LIKE  '%{0}%' Order By [" & colPk.ColumnName & "]"
        Else
            StrSelect &= " From [" & DT.TableName & "] WHERE " & colSearch.ColumnName & " LIKE  '%{0}%'"
        End If

        Return " String.Format(" & """" & StrSelect & """" & "," & colSearch.ColumnName & ")"

    End Function
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

    Public Shared Function CreateInsertValues(ByVal DT As TableNameInfo) As String

        Dim sbInsert As New StringBuilder("INSERT INTO " & DT.TableName.ToLower & " (")


        Dim RecNum As Integer = 0

        Dim qc = From col In DT.ListColumn Where col.IsAutoincrement = False
                 Select col
        For Each col As ColumnsInfo In qc
            sbInsert.Append(col.ColumnName & ",")
        Next
        Dim mylastComar = sbInsert.ToString.LastIndexOf(",")
        Dim StrInsert = sbInsert.Remove(mylastComar, 1).ToString & ")"
        Return StrInsert.ToString
    End Function
    Public Shared Function CreateStoredProcedureSelectAll(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("CREATE PROCEDURE [dbo].[sp_" & DT.TableValue & "_GetAll] AS")
        sb.AppendLine("    BEGIN")
        sb.AppendLine("       set nocount on;")
        sb.AppendLine("       " & CreateSelectCommandAll(DT))
        sb.AppendLine("    End")
        Return sb.ToString
    End Function
    Public Shared Function CreateStoredProcedureSelectById(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        sb.AppendLine("CREATE PROCEDURE [dbo].[sp_" & DT.TableValue & "_GetById] AS")
        sb.AppendLine("    @" & colPk.ColumnValue & " " & colPk.TypeSQL)
        sb.AppendLine("    BEGIN")
        sb.AppendLine("       set nocount on;")
        sb.AppendLine("       " & CreateSelectByIdCommand(DT))
        sb.AppendLine("    End")

        Return sb.ToString
    End Function

    Public Shared Function CreateStoredProcedureDelete(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        sb.AppendLine("CREATE PROCEDURE [dbo].[sp_" & DT.TableValue & "_Delete] AS")
        sb.AppendLine("    @" & colPk.ColumnValue & " " & colPk.TypeSQL)
        sb.AppendLine("    BEGIN")
        sb.AppendLine("       set nocount on;")
        sb.AppendLine("       " & CreateSQLDELETECommandParam(DT))
        sb.AppendLine("    End")
        Return sb.ToString
    End Function
    Public Shared Function CreateStoredProcedureInsert(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        sb.AppendLine("CREATE PROCEDURE [dbo].[sp_" & DT.TableValue & "_Insert]")
        Dim sbParam As New StringBuilder()
        For Each col In DT.ListColumn
            sbParam.AppendLine("    @" & col.ColumnValue & " " & col.TypeSQL & ",")
        Next
        Dim mylastComar = sbParam.ToString.LastIndexOf(",")
        Dim StrParams = sbParam.Remove(mylastComar, 1).ToString

        sb.AppendLine("    " & StrParams)
        sb.AppendLine("    AS")
        sb.AppendLine("    BEGIN")
        sb.AppendLine("       set nocount on;")
        sb.AppendLine("       --DECLARE @InsertedRows AS TABLE (Id int);;")
        sb.AppendLine("       --insert into [dbo].[Sale]([CashierId],[SaleDate],[SubTotal],[Tax],[Total]) OUTPUT Inserted.Id INTO @InsertedRows;")
        sb.AppendLine("       --values(@CashierId,@SaleDate,@SubTotal,@Tax,@Total);")
        sb.AppendLine("       -- ")
        sb.AppendLine("       --  SELECT Id FROM @InsertedRows")
        sb.AppendLine("       " & CreateInsert(DT))
        sb.AppendLine("    END")
        Return sb.ToString
    End Function
    Public Shared Function CreateStoredProcedureUpdate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        sb.AppendLine("CREATE PROCEDURE [dbo].[sp_" & DT.TableValue & "_Update]")
        Dim sbParam As New StringBuilder()
        For Each col In DT.ListColumn
            sbParam.AppendLine("    @" & col.ColumnValue & " " & col.TypeSQL & ",")
        Next
        Dim mylastComar = sbParam.ToString.LastIndexOf(",")
        Dim StrParams = sbParam.Remove(mylastComar, 1).ToString

        sb.AppendLine("    " & StrParams)
        sb.AppendLine("    AS")
        sb.AppendLine("    BEGIN")
        sb.AppendLine("       set nocount on;")
        sb.AppendLine("       " & CreateUpdate(DT))
        sb.AppendLine("    END")
        Return sb.ToString
    End Function



End Class
