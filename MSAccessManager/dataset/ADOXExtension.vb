Imports System.Runtime.CompilerServices
Imports DBExtenderLib
Imports ADOX
Imports Microsoft.Office.Interop.Access
Imports Microsoft.Office.Interop.Access.Dao

Module ADOXExtension
    <Extension()> _
    Public Function IsRequared(ByVal columnObj As ADOX.Column) As Boolean
        Return IIf(columnObj.Properties("Nullable").Value, False, True)
    End Function
    <Extension()> _
    Public Function IsPrimaryKey(ByVal columnObj As ADOX.Column, ByVal TB As ADOX.Table) As Boolean
        Dim PriKey = From pk As ADOX.Key In TB.Keys _
                    Where pk.Type = ADOX.KeyTypeEnum.adKeyPrimary And pk.Columns(0).Name = columnObj.Name _
                    Select pk.Columns.Item(0)
        If PriKey.Count > 0 Then

            Return True
        Else
            Return False
        End If
    End Function
    <Extension()> _
    Public Function IsForeignKeyInfo(ByVal columnObj As ADOX.Column, ByVal TB As ADOX.Table) As Boolean
        Dim fkKey = From pk As ADOX.Key In TB.Keys _
                    Where pk.Type = ADOX.KeyTypeEnum.adKeyForeign And pk.Columns(0).Name = columnObj.Name _
                    Select pk.Columns.Item(0)
        If fkKey.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    <Extension()> _
    Public Function GetForeignKeyInfo(ByVal columnObj As ADOX.Column, ByVal TB As ADOX.Table) As ForeignKeyInfo
        Dim fkKey = From pk As ADOX.Key In TB.Keys _
                    Where pk.Type = ADOX.KeyTypeEnum.adKeyForeign _
                    Select pk

        Dim fkinfo As New ForeignKeyInfo

        For Each k In fkKey
            If columnObj.Name = k.Columns.Item(0).Name Then
                fkinfo.ColumnName = k.Columns.Item(0).Name
                fkinfo.ForeignKeyName = k.Name
                fkinfo.RelatedTable = k.RelatedTable
                fkinfo.RelatedColumnName = k.Columns.Item(0).RelatedColumn
                fkinfo.ColumnTypeCS = columnObj.GetLinqVarCSharp
                fkinfo.ColumnTypeVB = columnObj.GetLinqVar
                Return fkinfo
            End If
        Next
        Return fkinfo
    End Function
    <Extension()> _
    Public Function IsAutoincrement(ByVal columnObj As ADOX.Column) As Boolean
        Return IIf(columnObj.Properties("Autoincrement").Value, True, False)
    End Function
    <Extension()> _
    Public Function GetParamType(ByVal intType As ADODB.DataTypeEnum) As String
        Select Case intType
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarWChar, ADOX.DataTypeEnum.adVarWChar, _
                ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, ADOX.DataTypeEnum.adLongVarWChar
                Return "String"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "Integer"
            Case ADOX.DataTypeEnum.adInteger
                Return "Integer"
            Case ADOX.DataTypeEnum.adSingle
                Return "Single"
            Case ADOX.DataTypeEnum.adCurrency
                Return "Decimal"
            Case ADOX.DataTypeEnum.adNumeric
                Return "Integer"
            Case ADOX.DataTypeEnum.adBoolean
                Return "Boolean"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "Date"
            Case ADOX.DataTypeEnum.adDouble
                Return "Double"
            Case ADOX.DataTypeEnum.adDBDate, ADOX.DataTypeEnum.adDBTime
                Return "Date"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "Byte()"
        End Select
        Return "String"
    End Function
    <Extension()> _
    Public Function GetParamType(ByVal intType As ADOX.DataTypeEnum) As String
        Select Case intType
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarWChar, ADOX.DataTypeEnum.adVarWChar, _
                ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, ADOX.DataTypeEnum.adLongVarWChar
                Return "String"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "Integer"
            Case ADOX.DataTypeEnum.adInteger
                Return "Integer"
            Case ADOX.DataTypeEnum.adSingle
                Return "Single"
            Case ADOX.DataTypeEnum.adCurrency
                Return "Decimal"
            Case ADOX.DataTypeEnum.adNumeric
                Return "Integer"
            Case ADOX.DataTypeEnum.adBoolean
                Return "Boolean"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "Date"
            Case ADOX.DataTypeEnum.adDouble
                Return "Double"
            Case ADOX.DataTypeEnum.adDBDate, ADOX.DataTypeEnum.adDBTime
                Return "Date"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "Byte()"
        End Select
        Return "String"
    End Function
    <Extension()> _
    Public Function GetVBType(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarWChar, ADOX.DataTypeEnum.adVarWChar, _
                ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, ADOX.DataTypeEnum.adLongVarWChar
                Return "String"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "Integer"
            Case ADOX.DataTypeEnum.adInteger
                Return "Integer"
            Case ADOX.DataTypeEnum.adSingle
                Return "Single"
            Case ADOX.DataTypeEnum.adCurrency
                Return "Decimal"
            Case ADOX.DataTypeEnum.adNumeric
                Return "Integer"
            Case ADOX.DataTypeEnum.adBoolean
                Return "Boolean"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "Date"
            Case ADOX.DataTypeEnum.adDouble
                Return "Double"
            Case ADOX.DataTypeEnum.adDBDate, ADOX.DataTypeEnum.adDBTime
                Return "Date"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "Byte()"
                'Byte
            Case ADOX.DataTypeEnum.adBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "Byte()"
        End Select
        Return "String"
    End Function
    <Extension()> _
    Public Function GetSqlFieldType(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar
                Return "varchar"
            Case ADOX.DataTypeEnum.adVarChar
                Return "nvarChar"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "smallint"
            Case ADOX.DataTypeEnum.adInteger
                Return "Int"
            Case ADOX.DataTypeEnum.adSingle
                Return "real"
            Case ADOX.DataTypeEnum.adCurrency
                Return "money"
            Case ADOX.DataTypeEnum.adNumeric
                Return "SmallInt"
            Case ADOX.DataTypeEnum.adBoolean
                Return "bit"
            Case ADOX.DataTypeEnum.adDate
                Return "smalldatetime"
            Case ADOX.DataTypeEnum.adDBDate, ADOX.DataTypeEnum.adDBTime
                Return "smalldatetime"
            Case ADOX.DataTypeEnum.adDBTime
                Return "smalldatetime"
            Case ADOX.DataTypeEnum.adLongVarChar
                Return "nvarChar"
            Case ADOX.DataTypeEnum.adVarWChar
                Return "nvarChar"
            Case ADOX.DataTypeEnum.adLongVarWChar
                Return "nvarChar"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "image"
            Case ADOX.DataTypeEnum.adGUID
                Return "UniqueIdentifier"
            Case ADOX.DataTypeEnum.adDouble
                Return "TinyInt"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt, ADOX.DataTypeEnum.adLongVarBinary
                Return "Byte"
        End Select
        Return "varchar"
    End Function
    <Extension()> _
    Public Function GetSqlVBType(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarChar, _
                 ADOX.DataTypeEnum.adLongVarWChar, ADOX.DataTypeEnum.adVarWChar, _
                 ADOX.DataTypeEnum.adLongVarChar
                Return "String"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "Integer"
            Case ADOX.DataTypeEnum.adInteger
                Return "Integer"
            Case ADOX.DataTypeEnum.adSingle
                Return "Single"
            Case ADOX.DataTypeEnum.adCurrency
                Return "Decimal"
            Case ADOX.DataTypeEnum.adNumeric
                Return "Integer"
            Case ADOX.DataTypeEnum.adBoolean
                Return "Boolean"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                 ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "Date"
            Case ADOX.DataTypeEnum.adDouble
                Return "Double"
            Case ADOX.DataTypeEnum.adGUID
                Return "GUID"
            Case ADOX.DataTypeEnum.adBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt, ADOX.DataTypeEnum.adLongVarBinary
                Return "Byte()"
        End Select
        Return "String"
    End Function
    <Extension()> _
    Public Function GetMicrosoftAccess(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarChar, _
            ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, _
            ADOX.DataTypeEnum.adBSTR
                Return "Text"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "Number"
            Case ADOX.DataTypeEnum.adInteger
                Return "Number"
            Case ADOX.DataTypeEnum.adSingle
                Return "Number"
            Case ADOX.DataTypeEnum.adCurrency
                Return "Currency"
            Case ADOX.DataTypeEnum.adNumeric
                Return "Number"
            Case ADOX.DataTypeEnum.adBoolean
                Return "Boolean"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                  ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "Date"
            Case ADOX.DataTypeEnum.adDouble
                Return "Double"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "OlEOBJECT"
            Case ADOX.DataTypeEnum.adBinary
                Return "bit"
            Case ADOX.DataTypeEnum.adGUID
                Return "GUID"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "Byte()"
        End Select
        Return "Text"

    End Function
    <Extension()> _
    Public Function GetLinqVar(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarChar, _
            ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, _
            ADOX.DataTypeEnum.adBSTR
                Return "String"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "System.Nullable(Of Short)"
            Case ADOX.DataTypeEnum.adInteger
                Return "System.Nullable(Of Integer)"
            Case ADOX.DataTypeEnum.adSingle
                Return "System.Nullable(Of Single)"
            Case ADOX.DataTypeEnum.adCurrency
                Return "System.Nullable(Of Decimal)"
            Case ADOX.DataTypeEnum.adNumeric
                Return "System.Nullable(Of Integer)"
            Case ADOX.DataTypeEnum.adBoolean
                Return "Boolean"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                  ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "System.Nullable(Of Date)"
            Case ADOX.DataTypeEnum.adDouble
                Return "System.Nullable(Of Double)"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adBinary
                Return "Byte()"
            Case ADOX.DataTypeEnum.adGUID
                Return "GUID"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "Byte()"
        End Select
        Return "String"

    End Function
    <Extension()> _
    Public Function GetCSharp(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarChar, _
            ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, _
            ADOX.DataTypeEnum.adBSTR
                Return "string"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "short"
            Case ADOX.DataTypeEnum.adInteger, ADOX.DataTypeEnum.adNumeric
                Return "int"
            Case ADOX.DataTypeEnum.adSingle
                Return "float"
            Case ADOX.DataTypeEnum.adCurrency
                Return "decimal"
            Case ADOX.DataTypeEnum.adBoolean
                Return "bool"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                  ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "DateTime"
            Case ADOX.DataTypeEnum.adDouble
                Return "double"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "byte[]"
            Case ADOX.DataTypeEnum.adBinary
                Return "byte[]"
            Case ADOX.DataTypeEnum.adGUID
                Return "Guid"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "byte[]"
        End Select
        Return "string"

    End Function
    <Extension()> _
    Public Function GetLinqVarCSharp(ByVal intType As ADOX.Column) As String
        Select Case intType.Type
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarChar, _
            ADOX.DataTypeEnum.adLongVarChar, ADOX.DataTypeEnum.adVarWChar, _
            ADOX.DataTypeEnum.adBSTR
                Return "string"
            Case ADOX.DataTypeEnum.adSmallInt
                Return "System.Nullable<short>"
            Case ADOX.DataTypeEnum.adInteger, ADOX.DataTypeEnum.adNumeric
                Return "System.Nullable<int>"
            Case ADOX.DataTypeEnum.adSingle
                Return "System.Nullable<float>"
            Case ADOX.DataTypeEnum.adCurrency
                Return "System.Nullable<decimal>"
            Case ADOX.DataTypeEnum.adBoolean
                Return "bool"
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime, _
                  ADOX.DataTypeEnum.adDBTimeStamp, ADOX.DataTypeEnum.adDBDate
                Return "System.Nullable<System.DateTime>"
            Case ADOX.DataTypeEnum.adDouble
                Return "System.Nullable<double>"
            Case ADOX.DataTypeEnum.adLongVarBinary
                Return "byte[]"
            Case ADOX.DataTypeEnum.adBinary
                Return "byte[]"
            Case ADOX.DataTypeEnum.adGUID
                Return "Guid"
            Case ADOX.DataTypeEnum.adUnsignedTinyInt
                Return "byte[]"
        End Select
        Return "string"

    End Function
    <Extension()> Public Sub add(ByVal lstTabInfo As List(Of TableNameInfo),
                   ByVal dbInfo As DatabaseNameInfo, ByVal TBName As String,
                   ByVal StrConn As String, ByVal lstColl As List(Of ColumnsInfo))
        lstTabInfo.add(New TableNameInfo With
                       {.Database = dbInfo,
                       .TableName = TBName,
                       .StrConnection = StrConn,
                       .SchemaTable = "",
                       .ListColumn = lstColl}
                   )
    End Sub

    <Extension()> Public Sub add(ByVal lstColInfo As List(Of ColumnsInfo),
                      ByVal ObjTable As ADOX.Table, ByVal magAccess As ManageMsAccess)
        Dim ColLst As List(Of PreserveColumn) = GetTable(ObjTable.Name, magAccess.ConnectionString)
        For Each strColumn In ColLst
            Dim columnObj As ADOX.Column
            columnObj = ObjTable.Columns(strColumn.ColumnsName)
            lstColInfo.add(New ColumnsInfo With
                                 {.ColumnName = columnObj.Name,
                                .Size = columnObj.DefinedSize,
                                .IsRequared = columnObj.IsRequared,
                                .IsAutoincrement = columnObj.IsAutoincrement,
                                .IsPrimary_Key = columnObj.IsPrimaryKey(ObjTable),
                                .IsForeign_Key = columnObj.IsForeignKeyInfo(ObjTable),
                                .ForeignKey = IIf(columnObj.IsForeignKeyInfo(ObjTable) = True, columnObj.GetForeignKeyInfo(ObjTable), Nothing),
                                .TypeSQL = columnObj.GetSqlFieldType, .TypeVB = columnObj.GetSqlVBType,
                                 .VarCSharp = columnObj.GetCSharp,
                                .LinqVar = columnObj.GetLinqVar})
        Next


    End Sub
    Public Function GetListColumn(ByVal ObjTable As ADOX.Table, ByVal magAccess As ManageMsAccess) As List(Of ColumnsInfo)
        Dim lstColInfo As New List(Of ColumnsInfo)
        Dim ColLst As List(Of PreserveColumn) = GetTable(ObjTable.Name, magAccess.ConnectionString)
        For Each strColumn In ColLst
            Dim columnObj As ADOX.Column
            columnObj = ObjTable.Columns(strColumn.ColumnsName)
            lstColInfo.add(New ColumnsInfo With
                                 {.ColumnName = columnObj.Name,
                                .Size = columnObj.DefinedSize,
                                .TypeAccess = columnObj.GetMicrosoftAccess,
                                .TypeMySql = columnObj.GetMySqlDataType,
                                .IsRequared = columnObj.IsRequared,
                                .IsAutoincrement = columnObj.IsAutoincrement,
                                .IsPrimary_Key = columnObj.IsPrimaryKey(ObjTable),
                                .IsForeign_Key = columnObj.IsForeignKeyInfo(ObjTable),
                                .ForeignKey = IIf(columnObj.IsForeignKeyInfo(ObjTable) = True, columnObj.GetForeignKeyInfo(ObjTable), Nothing),
                                .TypeSQL = columnObj.GetSqlFieldType, .TypeVB = columnObj.GetSqlVBType,
                                .LinqVar = columnObj.GetLinqVar,
                                .VarCSharp = columnObj.GetCSharp,
                                .LinqVarCSharp = columnObj.GetLinqVarCSharp})
        Next

        Return lstColInfo
    End Function
    Public Function GetListColumnView(ByVal ObjTable As ADOX.Table, ByVal magAccess As ManageMsAccess) As List(Of ColumnsInfo)
        Dim lstColInfo As New List(Of ColumnsInfo)
        'Dim ColLst As List(Of PreserveColumn) = GetTableFromAccess(magAccess.DatabaseNameExt, ObjTable.Name)

        For Each columnObj As ADOX.Column In ObjTable.Columns
            lstColInfo.add(New ColumnsInfo With
                                 {.ColumnName = columnObj.Name,
                                .Size = columnObj.DefinedSize,
                                .TypeAccess = columnObj.GetMicrosoftAccess,
                                .TypeMySql = columnObj.GetMySqlDataType,
                                .TypeSQL = columnObj.GetSqlFieldType,
                                .IsRequared = False,
                                .IsAutoincrement = False,
                                .IsPrimary_Key = False,
                                .IsForeign_Key = False,
                                .ForeignKey = Nothing,
                                .LinqVar = columnObj.GetLinqVar,
                                .VarCSharp = columnObj.GetCSharp,
                                .LinqVarCSharp = columnObj.GetLinqVarCSharp})
        Next
        Return lstColInfo
    End Function
    <Extension()> _
    Public Function GetMySqlDataType(ByVal col As ADOX.Column) As String
        Dim length, precision, scal As Integer
        Dim newtype As String = ""


        length = col.DefinedSize
        precision = col.Precision
        scal = col.NumericScale
        If col.Type = ADOX.DataTypeEnum.adCurrency Then
            precision = 19
            scal = 4
        End If

        Select Case col.Type

            ' integers
            Case ADOX.DataTypeEnum.adTinyInt
                newtype = "TINYINT"
            Case ADOX.DataTypeEnum.adSmallInt
                newtype = "SMALLINT"
            Case ADOX.DataTypeEnum.adNumeric, ADOX.DataTypeEnum.adInteger
                newtype = "INT"
            Case ADOX.DataTypeEnum.adBigInt
                newtype = "BIGINT"
            Case ADOX.DataTypeEnum.adDouble
                newtype = "DOUBLE"
            Case ADOX.DataTypeEnum.adSingle
                newtype = "FLOAT"
            Case ADOX.DataTypeEnum.adDecimal, ADOX.DataTypeEnum.adNumeric, ADOX.DataTypeEnum.adCurrency
                newtype = "DECIMAL(" & precision & ", " & scal & ")"

                ' strings
            Case ADOX.DataTypeEnum.adChar, ADOX.DataTypeEnum.adVarWChar
                If col.DefinedSize < 255 Then
                    newtype = "CHAR(" & length & ")"
                Else
                    newtype = "TEXT"
                End If
            Case ADOX.DataTypeEnum.adLongVarChar
                If length < 255 Then
                    newtype = "VARCHAR(" & col.DefinedSize & ")"
                Else
                    newtype = "TEXT"
                End If
            Case ADOX.DataTypeEnum.adLongVarWChar
                newtype = "LONGTEXT"

                ' date/time
            Case ADOX.DataTypeEnum.adDate, ADOX.DataTypeEnum.adDBTime
                newtype = "DATETIME"
            Case ADOX.DataTypeEnum.adGUID
                newtype = "TINYBLOB"
            Case ADOX.DataTypeEnum.adVarBinary
                newtype = "BLOB"
            Case ADOX.DataTypeEnum.adLongVarBinary
                newtype = "LONGBLOB"

            Case Else
                ' unknown data type, not supported
                'Stop
        End Select
        Return newtype
    End Function



    Private Function GetTable(ByVal TableName As String, ByVal StrCnn As String) As List(Of PreserveColumn)
        Dim cnn As New ADODB.Connection
        cnn.Open(StrCnn)
        cnn.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Dim rsSchema As ADODB.Recordset
        rsSchema = cnn.OpenSchema(ADODB.SchemaEnum.adSchemaColumns, New Object() {Nothing, Nothing, TableName})
        rsSchema.Sort = "ORDINAL_POSITION"
        Dim lstFields As New List(Of PreserveColumn)
        While Not rsSchema.EOF
            lstFields.Add(New PreserveColumn With {.ColumnsName = rsSchema.Fields("COLUMN_NAME").Value})
            rsSchema.MoveNext()
        End While
        Return lstFields
    End Function
    Private Function GetTableFromAccess(ByVal DatabaseFile As String, ByVal ViewName As String) As List(Of PreserveColumn)
        Dim db As Dao.Database
        Dim dbE As New Dao.DBEngine
        Dim lstFields As New List(Of PreserveColumn)
        Dim dbEngine As New Microsoft.Office.Interop.Access.Dao.DBEngine
        db = dbEngine.OpenDatabase(DatabaseFile)
        Dim query As QueryDef

        With db
            query = .QueryDefs(ViewName)
            For Each r As Field In query.Fields
                lstFields.Add(New PreserveColumn With {.ColumnsName = r.Name})
            Next
        End With
        Return lstFields
    End Function
End Module

