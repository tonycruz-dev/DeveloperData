Imports System.Runtime.CompilerServices
Imports DBExtenderLib
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management

Module SqlExtentionMethods

    <Extension()> _
   Public Function GetForeignKey(ByVal ObjTB As Table, ByVal columnName As String) As ForeignKeyInfo
        Dim objfk As ForeignKeyInfo = Nothing
        Dim cfk = From fk As ForeignKey In ObjTB.ForeignKeys _
                   Select fk

        If Not cfk Is Nothing Then
            If cfk.Count > 0 Then
                For Each ck In cfk
                    For Each c In ck.Columns
                        If c.Name = columnName Then
                            objfk = New ForeignKeyInfo With { _
                            .ColumnName = c.Name, _
                             .ForeignKeyName = ck.ReferencedKey, _
                            .RelatedTable = ck.ReferencedTable, _
                            .RelatedColumnName = c.ReferencedColumn}
                            Return objfk
                        End If
                    Next
                Next
            End If
        End If

        Return objfk
    End Function
    <Extension()> _
    Public Function GetVBDataType(ByVal typeName As String) As String
        Select Case typeName
            Case "numeric", "int"
                Return "Integer"
            Case "nvarchar", "varchar", "ntext", "text"
                Return "String"
            Case "decimal"
                Return "Decimal"
            Case "money"
                Return "Decimal"
            Case "smallint"
                Return "Int16"
            Case "real"
                Return "Single"
            Case "datetime", "date", LCase("DATETIME2"), LCase("TIME"), LCase("DATETIMEOFFSET")
                Return "Date"
            Case "byte"
                Return "Byte"
            Case "bit"
                Return "Boolean"
            Case "char"
                Return "String"
            Case "float"
                Return "Double"
            Case Else
                'Add other missing data types
        End Select
        Return "String"
    End Function
    <Extension()> _
    Public Function GetControlType(ByVal typeName As String) As String
        Select Case typeName
            Case "numeric", "int", "nvarchar", "varchar", "ntext", "text", "Char"
                Return "TextBox"
            Case "money", "decimal", "Single", "Int16", "smallint", "byte", "Double"
                Return "TextBox"
            Case "datetime"
                Return "DateTimePicker"
            Case "bit"
                Return "CheckBox"
            Case "byte"
                Return "PictureBox"
            Case Else
                'Add other missing data types
        End Select
        Return "TextBox"
    End Function
    <Extension()> _
    Public Function GetMySqlDataType(ByVal col As Smo.Column) As String
        Dim length, precision, scal As Integer
        Dim newtype As String = ""


        length = col.DataType.MaximumLength
        precision = col.DataType.NumericPrecision
        scal = col.DataType.NumericScale
        If LCase(col.DataType.Name) = "money" Then
            precision = 19
            scal = 4
        ElseIf LCase(col.DataType.Name) = "smallmoney" Then
            precision = 10
            scal = 4
        End If

        Select Case LCase(col.DataType.Name)

            ' integers
            Case "bit", "tinyint"
                newtype = "TINYINT"
            Case "smallint"
                newtype = "SMALLINT"
            Case "int"
                newtype = "INT"
            Case "bigint"
                newtype = "BIGINT"

                ' floating points
            Case "float"
                newtype = "DOUBLE"
            Case "real"
                newtype = "FLOAT"
            Case "decimal", "numeric", "money", "smallmoney"
                newtype = "DECIMAL(" & precision & ", " & scal & ")"

                ' strings
            Case "char", "nchar"
                If length < 255 Then
                    newtype = "CHAR(" & length & ")"
                Else
                    newtype = "TEXT"
                End If
            Case "varchar", "nvarchar"
                If length < 255 Then
                    newtype = "VARCHAR(" & length & ")"
                Else
                    newtype = "TEXT"
                End If
            Case "text", "ntext"
                newtype = "LONGTEXT"

                ' date/time
            Case "datetime", "smalldatetime"
                newtype = "DATETIME"
            Case "timestamp"
                newtype = "TINYBLOB"

                ' binary and other
            Case "uniqueidentifier"
                newtype = "TINYBLOB"
            Case "binary", "varbinary"
                newtype = "BLOB"
            Case "image"
                newtype = "LONGBLOB"

            Case Else
                ' unknown data type, not supported
                'Stop
        End Select

        Return newtype
    End Function
    <Extension()> _
  Public Function GetLinqVar(ByVal col As Smo.Column) As String
        Dim newtype As String = ""
        If col.Nullable Then
            Select Case LCase(col.DataType.Name)
                Case "numeric", "int", "bigint"
                    Return "System.Nullable(Of Integer)"
                Case "varchar", "ntext", "text", "Char"
                    Return "String"
                Case "bit"
                    Return "Boolean"
                Case "date", "datetime", "timestamp"
                    Return "System.Nullable(Of Date)"
                Case "smallint"
                    Return "System.Nullable(Of Short)"
                Case "int", "bigint", "numeric"
                    Return "System.Nullable(Of Integer)"
                Case "smallint"
                    Return "System.Nullable(Of Short)"
                Case "real"
                    Return "System.Nullable(Of Single)"
                Case "money"
                    Return "System.Nullable(Of Decimal)"
                Case "varbinarymax"
                    Return "Byte()"
                Case "image"
                    Return "Byte()"
                Case "uniqueidentifier"
                    Return "GUID"
            End Select
        Else
            Select Case LCase(col.DataType.Name)
                Case "numeric", "int", "bigint"
                    Return " Integer"
                Case "varchar", "ntext", "text", "Char"
                    Return "String"
                Case "bit"
                    Return "Boolean"
                Case "date", "datetime", "timestamp"
                    Return "Date"
                Case "smallint"
                    Return "Short"
                Case "smallint"
                    Return "Short"
                Case "real"
                    Return "Single"
                Case "money"
                    Return "Decimal"
                Case "varbinarymax"
                    Return "Byte()"
                Case "image"
                    Return "Byte()"
                Case "uniqueidentifier"
                    Return "GUID"
            End Select
        End If

        Return "String"

    End Function

    <Extension()> _
    Public Function GetCSharp(ByVal col As Smo.Column) As String
        Dim newtype As String = ""
        If col.Nullable Then
            Select Case LCase(col.DataType.Name)
                Case "numeric".ToLower, "int".ToLower, "bigint".ToLower
                    Return "int"
                Case "varchar", "ntext", "text", "Char"
                    Return "string"
                Case "bit"
                    Return "bool"
                Case "date", "datetime", "timestamp"
                    Return "DateTime"
                Case "smallInt"
                    Return "short"
                Case "real"
                    Return "float"
                Case "money"
                    Return "decimal"
                Case "VarBinaryMax"
                    Return "byte[]"
                Case "image"
                    Return "byte[]"
                Case "UniqueIdentifier"
                    Return "Guid"
            End Select
        Else
            Select Case LCase(col.DataType.Name)
                Case "numeric".ToLower, "int".ToLower, "bigint".ToLower
                    Return "int"
                Case "varchar", "ntext", "text", "Char"
                    Return "string"
                Case "bit"
                    Return "bool"
                Case "date", "datetime", "timestamp"
                    Return "DateTime"
                Case "smallInt"
                    Return "short"
                Case "real"
                    Return "float"
                Case "money"
                    Return "decimal"
                Case "VarBinaryMax"
                    Return "byte[]"
                Case "image"
                    Return "byte[]"
                Case "UniqueIdentifier"
                    Return "Guid"
            End Select
        End If
        Return "string"

    End Function
    <Extension()> _
    Public Function GetLinqVarCSharp(ByVal col As Smo.Column) As String
        Dim newtype As String = ""
        If col.Nullable Then
            Select Case LCase(col.DataType.Name)
                Case "numeric".ToLower, "int".ToLower, "bigint".ToLower
                    Return "System.Nullable<int>"
                Case "varchar", "ntext", "text", "Char"
                    Return "string"
                Case "bit"
                    Return "bool"
                Case "date", "datetime", "timestamp"
                    Return "DateTime?"
                Case "smallInt"
                    Return "short?"
                Case "real"
                    Return "float?"
                Case "money"
                    Return "decimal?"
                Case "VarBinaryMax"
                    Return "byte[]"
                Case "image"
                    Return "byte[]"
                Case "UniqueIdentifier"
                    Return "Guid"
            End Select
        Else
            Select Case LCase(col.DataType.Name)
                Case "numeric".ToLower, "int".ToLower, "bigint".ToLower
                    Return "int"
                Case "varchar", "ntext", "text", "Char"
                    Return "string"
                Case "bit"
                    Return "bool"
                Case "date", "datetime", "timestamp"
                    Return "System.DateTime"
                Case "smallInt"
                    Return "short"
                Case "real"
                    Return "float"
                Case "money"
                    Return "decimal"
                Case "VarBinaryMax"
                    Return "byte[]"
                Case "image"
                    Return "byte[]"
                Case "UniqueIdentifier"
                    Return "Guid"
            End Select
        End If
        Return "string"

    End Function
End Module
