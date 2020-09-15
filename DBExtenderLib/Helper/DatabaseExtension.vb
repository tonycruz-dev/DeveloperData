Imports System.Runtime.CompilerServices

Public Module DatabaseExtension

    <Extension()>
    Public Function GetRalatedTables(ByVal TB As TableNameInfo, ByVal DB As DatabaseNameInfo) As List(Of TableNameInfo)
        Dim lstRelateTables As New List(Of TableNameInfo)

        For Each ctb In DB.ListTable
            Dim col = (From c In ctb.ListColumn Where c.IsForeign_Key = True).ToList

            If col.Count > 0 Then
                Dim colFK = (From c In col Where c.ForeignKey.RelatedTable = TB.TableValue).SingleOrDefault
                If Not colFK Is Nothing Then
                    lstRelateTables.Add(ctb)
                End If
            End If
        Next
        Return lstRelateTables
    End Function

    <Extension()>
    Public Function TablesAsMasterTable(ByVal TB As TableNameInfo, ByVal DB As DatabaseNameInfo) As Boolean
        Dim lisMasters As New List(Of RelationalTable)

        Dim qcpk = (From pk In TB.ListColumn Where pk.IsForeign_Key = True
                    Select pk.ForeignKey).ToList
        If qcpk.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    <Extension()>
    Public Function GetMasterTables(ByVal TB As TableNameInfo, ByVal DB As DatabaseNameInfo) As List(Of RelationalTable)
        Dim lisMasters As New List(Of RelationalTable)

        Dim qcpk = (From pk In TB.ListColumn Where pk.IsForeign_Key = True
                    Select pk.ForeignKey).ToList
        If qcpk.Count > 0 Then
            For Each ft In qcpk
                Dim selft = ft
                Dim resultRelateTables = (From rt In DB.ListTable Where rt.TableName = selft.RelatedTable).SingleOrDefault
                If resultRelateTables IsNot Nothing Then
                    Dim rtb = New RelationalTable With {.RelateTable = resultRelateTables,
                                                                            .ForeignKey = ft.ColumnName,
                                                                            .SelectedTableLinqKey = ft.RelatedColumnName,
                                                                            .SelectedTableName = TB.TableName,
                                                                            .SelectedTable = TB,
                                                                            .ColumnType = ft.ColumnTypeCS,
                                                                             .ColumnTypeCS = ft.ColumnTypeCS,
                                                                            .ColumnTypeVB = ft.ColumnTypeVB,
                                                                            .RelateTableName = resultRelateTables.TableName}
                    lisMasters.Add(rtb)
                End If

            Next
        End If


        Return lisMasters
    End Function
    <Extension()>
    Public Function GetRalationalTables(ByVal TB As TableNameInfo, ByVal DB As DatabaseNameInfo) As List(Of RelationalTable)
        Dim lstRelateTables As New List(Of RelationalTable)

        For Each ctb In DB.ListTable
            Dim col = (From c In ctb.ListColumn Where c.IsForeign_Key = True).ToList

            If col.Count > 0 Then
                Dim colFK = (From c In col Where c.ForeignKey.RelatedTable = TB.TableValue).ToList
                If Not colFK Is Nothing Then
                    For Each cfk In colFK
                        Dim rtb = New RelationalTable With {.RelateTable = ctb,
                                                            .ForeignKey = cfk.ForeignKey.ColumnName,
                                                            .SelectedTableLinqKey = cfk.ForeignKey.RelatedColumnName,
                                                            .SelectedTableName = TB.TableType,
                                                            .SelectedTable = TB,
                                                            .ColumnTypeVB = cfk.ForeignKey.ColumnTypeVB,
                                                            .ColumnTypeCS = cfk.ForeignKey.ColumnTypeCS,
                                                            .RelateTableName = ctb.TableName}
                        lstRelateTables.Add(rtb)
                    Next
                End If
            End If
        Next
        Return lstRelateTables
    End Function
    <Extension()>
    Public Function CopyTable(ByVal tv As TableNameInfo) As TableNameInfo
        Dim tb As New TableNameInfo With {.TableName = tv.TableName,
                                          .SchemaTable = tv.SchemaTable,
                                          .StrConnection = tv.StrConnection,
                                          .TableType = "Table"}
        For Each col In tv.ListColumn
            tb.ListColumn.Add(col)
        Next
        Return tb
    End Function
    <Extension()>
    Public Function CopyColumn(ByVal Col As ColumnsInfo) As wpfColumnInfo
        Dim _Col As New wpfColumnInfo With {.ColumnName = Col.ColumnName,
                                            .Size = Col.Size,
                                            .TypeSQL = Col.TypeSQL,
                                            .TypeVB = Col.TypeVB,
                                            .IsRequared = Col.IsRequared,
                                            .IsPrimary_Key = Col.IsPrimary_Key,
                                            .IsForeign_Key = Col.IsForeign_Key,
                                            .TypeOfControl = GetWpfControlField(Col),
                                            .IsAutoincrement = Col.IsAutoincrement,
                                            .LinqVar = Col.LinqVar}
        Return _Col
    End Function
    <Extension()>
    Private Function GetWpfControlField(ByVal col As ColumnsInfo) As String

        Select Case col.TypeSQL.ToLower
            Case "nvarchar", "int", "smallint", "real", "money", "uniqueIdentifier", "tinyint"
                Return "TextBox"
            Case "bit"
                Return "CheckBox"
            Case "smalldatetime", "datetime", "date"
                Return "DatePicker"
            Case "image", "byte"
                Return "Image"
            Case Else
                Return "TextBox"

        End Select
        'CheckBox()
        'ComboBox
        'DatePicker()
        'Image()
        'Label()
        'TextBlock()
        'TextBox()
    End Function
    <Extension()>
    Public Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function
End Module
