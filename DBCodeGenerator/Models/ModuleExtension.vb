﻿Imports System.Runtime.CompilerServices
Imports DBExtenderLib
Imports System.Text

Module ModuleExtension
    <Extension()> _
    Public Function SetColumnAttribut(ByVal col As ColumnsInfo, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()

        '	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OrderID", 
        '   AutoSync:=AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true),  _
        '   Global.System.Runtime.Serialization.DataMemberAttribute(Order:=1)>  _

        sb.Append(" <Column(Name:=" & col.GetColumnnName &
                      ", Storage:=" & col.GetStorageName & col.GetAutoSyncColumn & ", DbType:=" & col.GetSQLType & col.GetColumnnIsPrimaryKey & col.GetColumnnCanBeNull & ")>")
        Return sb.ToString
    End Function
    <Extension()> _
    Public Function SetColumnAttributFKey(ByVal rtb As RelationalTable, TableName As String, Num As Integer) As String
        ' Friend Shared Function SetColumnAttribut(ByVal tbName As String, relatedTable As RelationalTable, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        ' <Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Shipper_Order", Storage:="_Shipper", ThisKey:="ShipVia", OtherKey:="ShipperID", IsForeignKey:=True)> _
        sb.Append("<Association(Name:=" & (TableName & "_" & rtb.RelateTableValue).GetTableName & ", Storage:=" & rtb.RelateTableValue.GetStorageName & ", ThisKey:=" & rtb.ForeignKey.GetTableName & ", OtherKey:=" & rtb.SelectedTableLinqKey.GetTableName & ", IsForeignKey:=True)> _ ")
        Return sb.ToString
    End Function
    <Extension()> _
    Public Function SetColumnAttribut(ByVal rtb As RelationalTable, TableName As String, Num As Integer) As String
        ' Friend Shared Function SetColumnAttribut(ByVal tbName As String, relatedTable As RelationalTable, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        ' <Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Shipper_Order", Storage:="_Shipper", ThisKey:="ShipVia", OtherKey:="ShipperID", IsForeignKey:=True)> _
        sb.AppendLine("<Association(Name:=" & (TableName & "_" & rtb.RelateTableValue).GetTableName & ", Storage:=" & rtb.RelateTableValue.GetStorageName & ", ThisKey:=" & rtb.ForeignKey.GetTableName & ", OtherKey:=" & rtb.SelectedTableLinqKey.GetTableName & "), ")
        sb.Append("Global.System.Runtime.Serialization.DataMemberAttribute(Order:=" & Num & ", EmitDefaultValue:=False)> _")
        Return sb.ToString
    End Function
    'Friend Shared Function SetColumnAttribut(ByVal tbName As String, relatedTable As RelationalTable, ByVal ColNumber As Integer) As String
    '    Dim sb As New StringBuilder()
    '    'If col.IsForeignKey Then
    '    '[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_Product", Storage="_Products", ThisKey="CategoryID", OtherKey="CategoryID")]
    '    '[global::System.Runtime.Serialization.DataMemberAttribute(Order=5, EmitDefaultValue=false)]
    '    '[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_Product", Storage="_Category", ThisKey="CategoryID", OtherKey="CategoryID", IsForeignKey=true)]
    '    sb.AppendLine("[global::System.Data.Linq.Mapping.AssociationAttribute(Name=" & QT(tbName & "_" & relatedTable.RelateTableValue) & ", Storage=" & QT("_" & relatedTable.RelateTableValue) & ", ThisKey=" & QT(relatedTable.SelectedTableLinqKey) & ", OtherKey=" & QT(relatedTable.ForeignKey) & ")]")
    '    sb.AppendLine("[global::System.Runtime.Serialization.DataMemberAttribute(Order=" & ColNumber & ", EmitDefaultValue=false)]")


    '    Return sb.ToString
    'End Function
    <Extension()>
    Private Function GetStorageName(col As ColumnsInfo) As String
        Return """" & "_" & col.ColumnName & """"
    End Function
    <Extension()>
    Private Function GetStorageName(TableName As String) As String
        Return """" & "_" & TableName & """"
    End Function
    <Extension()>
    Public Function GetColumnnName(ByVal ColumnName As String) As String
        Return """" & ColumnName & """"
    End Function
    <Extension()>
    Public Function GetTableName(ByVal TableName As String) As String
        Return """" & TableName & """"
    End Function
    <Extension()>
    Private Function GetAutoSyncColumn(col As ColumnsInfo) As String
        If col.IsAutoincrement Then
            Return ",AutoSync:=AutoSync.OnInsert"
        Else
            Return ""
        End If
    End Function
    <Extension()> _
    Public Function GetColumnnName(ByVal col As ColumnsInfo) As String
        Return """" & col.ColumnName & """"
    End Function
    <Extension()> _
    Public Function GetColumnnIsPrimaryKey(ByVal col As ColumnsInfo) As String
        If col.IsPrimary_Key And col.IsAutoincrement Then
            Return ",IsPrimaryKey:=True, IsDbGenerated:=True"
        ElseIf col.IsPrimary_Key Then
            Return ",IsPrimaryKey:=True"
        Else
            Return ""
        End If
    End Function
    <Extension()> _
    Public Function GetTypeLinqVar(ByVal col As ColumnsInfo) As String
        If col.IsPrimary_Key Then
            Return col.TypeVB
        ElseIf col.IsRequared Then
            Return col.TypeVB
        Else
            Return col.LinqVar
        End If
    End Function
    <Extension()>
    Public Function GetColumnnCanBeNull(ByVal col As ColumnsInfo) As String
        If col.IsRequared Then
            Return ",CanBeNull:=false"
        Else
            Return ""
        End If
    End Function
    <Extension()>
    Public Function IsColumnnsRequared(ByVal col As ColumnsInfo) As Boolean
        If col.IsRequared Then
            Return True
        Else
            Return False
        End If
    End Function
    <Extension()> _
    Public Function GetSQLType(ByVal col As ColumnsInfo) As String
        Select Case col.TypeSQL.ToLower
            Case "char"
                If col.IsRequared Then
                    Return "Char(" & col.Size & ") NOT NULL".QT
                Else
                    Return "Char(" & col.Size & ")".QT
                End If

            Case "nchar"
                If col.IsRequared Then
                    Return "nchar(" & col.Size & ") NOT NULL ".QT
                Else
                    Return "nchar(" & col.Size & ")".QT
                End If

            Case "nvarchar"
                If col.IsRequared Then
                    Return "NVarChar(" & col.Size & ") NOT NULL ".QT
                Else
                    Return "NVarChar(" & col.Size & ")".QT
                End If

            Case "ntext"
                Return "Ntext".QT
            Case "smallint"
                Return "SmallInt".QT
            Case "int"
                If col.IsAutoincrement Then
                    Return "Int NOT NULL IDENTITY".QT
                Else
                    Return "Int".QT
                End If
            Case "int identity"
                Return "Int NOT NULL IDENTITY".QT
            Case "real"
                If col.IsRequared Then
                    Return "Real NOT NULL ".QT
                Else
                    Return "Real".QT
                End If

            Case "money"
                If col.IsRequared Then
                    Return "Money NOT NULL".QT
                Else
                    Return """" & "Money" & """"
                End If

            Case "numeric"
                Return "Int".QT
            Case "bit"
                If col.IsRequared Then
                    Return "Bit NOT NULL".QT
                Else
                    Return "bit".QT
                End If
            Case "byte"
                If col.IsRequared Then
                    Return "Byte NOT NULL".QT
                Else
                    Return "Byte".QT
                End If
            Case "smallint"
                If col.IsRequared Then
                    Return "smallint NOT NULL".QT
                Else
                    Return "smallint".QT
                End If
            Case "tinyint"
                If col.IsRequared Then
                    Return "TinyInt NOT NULL".QT
                Else
                    Return "TinyInt".QT
                End If
            Case "datetime", "smalldatetime"
                Return "DateTime".QT
            Case "image"
                Return "Image".QT

        End Select

        Return "nvarchar(" & col.Size & ")".QT
    End Function
    <Extension()> _
    Public Function GetReturnProType(ByVal col As ColumnsInfo) As String
        Select Case col.TypeVB
            Case "String"
                Return "If (String.Equals(Me._" & col.ColumnValue & "  , value) = false) Then "
            Case "Integer", "Decimal", "Short", "Single", "Date", "Double", "Byte()"
                'If (Me._SupplierID.Equals(value) = false) Then
                Return "If (Me._" & col.ColumnValue & ".Equals(value) = false) Then"
            Case "Boolean"
                Return "  If ((Me._" & col.ColumnValue & " = Value) = False) Then"
            Case Else
                Return "If (String.Equals(Me._" & col.ColumnValue & "  , value) = false) Then "
        End Select
    End Function
    Private Function QT(Value As String) As String
        Return """" & Value & """"
    End Function

End Module
