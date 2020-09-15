Imports System.Runtime.CompilerServices
Imports DBExtenderLib

Module ModuleHelper
    <Extension()>
    Public Function QT(Value As String) As String
        Return """" & Value & """"
    End Function
    <Extension()>
    Public Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function

    <Extension>
    Public Function GetLaravelType(col As ColumnsInfo) As String
        'bigIncrements
        Select Case col.TypeMySql.ToUpper
            Case "BIGINT "
                Return "bigIncrements(" & col.ColumnValue & ")"
            Case "BLOB"
                Return "binary(" & col.ColumnValue & ")"
            Case "BOOLEAN "
                Return "boolean('" & col.ColumnValue & "')"
            Case "CHAR "
                Return "string(" & col.ColumnValue & ")"
            Case "CHAR "
                Return "string(" & col.ColumnValue & ")"
            Case "CHAR "
                Return "string(" & col.ColumnValue & ")"
            Case "CHAR "
                Return "string(" & col.ColumnValue & ")"
            Case "CHAR "
                Return "string(" & col.ColumnValue & ")"
            Case Else
                Return "bigIncrements(" & col.ColumnValue & ")"
        End Select
    End Function
End Module
