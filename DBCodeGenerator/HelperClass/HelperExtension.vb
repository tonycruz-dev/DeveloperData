Imports System.Runtime.CompilerServices
Imports DBExtenderLib
Imports System.Text

Module HelperExtension
    <Extension()>
    Public Function Dquotes(_StrValue As String) As String
        Return """" & _StrValue & """"
    End Function
    <Extension()>
    Public Function Squotes(_StrValue As String) As String
        Return "'" & _StrValue & "'"
    End Function
    <Extension()>
    Public Function GetConverterType(ByVal Column As ColumnsInfo) As String
        Select Case UCase(Column.TypeSQL)
            Case UCase("numeric"), UCase("int")
                Return "CInt(returnData(" & Dquotes(Column.ColumnName) & "))"
            Case UCase("nvarchar"), UCase("varchar"), UCase("ntext"), UCase("text"), UCase("char")
                Return "ReturnEmptyStringIfDbNull(returnData(" & Dquotes(Column.ColumnName) & "))"
            Case UCase("decimal")
                Return "CDec(returnData(" & Dquotes(Column.ColumnName) & "))"
            Case UCase("money")
                Return "(returnData(" & Dquotes(Column.ColumnName) & "))"
            Case UCase("smallint")
                Return "CInt(returnData(" & Column.ColumnName & "))"
            Case UCase("datetime")
                Return "ReturnEmptyStringIfDbNull(returnData(" & Dquotes(Column.ColumnName) & "))"
            Case UCase("byte")
                Return "returnData(" & Dquotes(Column.ColumnName) & ")"
            Case UCase("bit")
                Return "CBool(returnData(" & Column.ColumnName & "))"
            Case UCase("float")
                Return "CDbl(returnData(" & Column.ColumnName & "))"
            Case Else
                Return "ReturnEmptyStringIfDbNull(returnData(" & Dquotes(Column.ColumnName) & "))"
        End Select
        Return "String"
    End Function

    <Extension()>
    Public Function GetTypeDataToXmlFormat(ByVal R As ColumnsInfo, ByVal dr As DataRow) As String

        Select Case R.TypeSQL.ToLower
            Case "char", "nchar", "nvarchar", "ntext"
                If dr.IsNull(R.ColumnName) Then
                    Return ""
                Else
                    Return dr(R.ColumnName)
                End If

            Case "smallint", "int", "int identity", "real", "money", "numeric"
                If dr.IsNull(R.ColumnName) Then
                    Return 0
                End If
                Return dr(R.ColumnName)
            Case "bit"
                If dr.IsNull(R.ColumnName) Then
                    Return "false"
                End If
                If dr(R.ColumnName) = "False" Then
                    Return "false"
                Else
                    Return "true"
                End If
                Return dr(R.ColumnName)

            Case "datetime"
                If dr.IsNull(R.ColumnName) Then
                    Return ""
                Else
                    Dim DateVal As Date = dr(R.ColumnName)
                    Return Format(DateVal, "yyyy-MM-dd")
                End If

            Case "smalldatetime"
                If dr.IsNull(R.ColumnName) Then
                    Return ""
                Else
                    Return Format(dr(R.ColumnName), "yyyy-MM-dd")
                End If


            Case "image"
                Dim myPic() As Byte
                myPic = dr(R.ColumnName)
                Dim MyStr As String
                MyStr = ByteToString(myPic) 'Convert.ToSingle(myPic.ToString) ' 
                Return MyStr

        End Select

        Return dr(R.ColumnName).ToString
    End Function
    <Extension()>
    Public Function GetSqlTypeData(ByVal R As ColumnsInfo, ByVal dr As DataRow) As String
        'varchar(30)
        Select Case R.TypeSQL.ToLower
            Case "char", "nchar", "nvarchar", "ntext"
                If dr.IsNull(R.ColumnName) Then
                    Return "".Squotes
                Else
                    Return dr(R.ColumnName).ToString.Squotes
                End If

            Case "smallint", "int", "int identity", "real", "money", "numeric"
                If dr.IsNull(R.ColumnName) Then
                    Return 0
                End If
                Return dr(R.ColumnName).ToString

            Case "bit"
                Return dr(R.ColumnName)
            Case "datetime"
                If dr.IsNull(R.ColumnName) Then
                    Return "null"
                Else
                    Dim DateVal As Date = dr(R.ColumnName)
                    Return Format(dr(R.ColumnName), "yyyy-MM-dd").ToString.Squotes
                End If

            Case "smalldatetime"
                If dr.IsNull(R.ColumnName) Then
                    Return "null"
                Else
                    Return "DateTime.Parse(" & Format(dr(R.ColumnName), "yyyy-MM-dd").ToString.Squotes & ")"
                End If


            Case "image"
                Dim myPic() As Byte
                myPic = dr(R.ColumnName)
                Dim MyStr As String
                MyStr = ByteToString(myPic) 'Convert.ToSingle(myPic.ToString) ' 
                Return "Convert.FromBase64String(" & MyStr.Dquotes & ")"

        End Select

        Return """" & dr(R.ColumnName) & """"
    End Function
    <Extension()>
    Public Function GetTypeData(ByVal R As ColumnsInfo, ByVal dr As DataRow) As String
        'varchar(30)
        Select Case R.TypeSQL.ToLower
            Case "char", "nchar", "nvarchar", "ntext"
                If dr.IsNull(R.ColumnName) Then
                    Return "".Dquotes
                Else
                    Return dr(R.ColumnName).ToString.Dquotes
                End If

            Case "smallint", "int", "int identity", "real", "money", "numeric"
                If dr.IsNull(R.ColumnName) Then
                    Return 0
                End If
                Return dr(R.ColumnName).ToString

            Case "bit"
                Return dr(R.ColumnName)
            Case "datetime"
                If dr.IsNull(R.ColumnName) Then
                    Return "Nothing"
                Else
                    Dim DateVal As Date = dr(R.ColumnName)
                    Return "DateTime.Parse(" & Format(dr(R.ColumnName), "yyyy-MM-dd").ToString.Dquotes & ")"
                End If

            Case "smalldatetime"
                If dr.IsNull(R.ColumnName) Then
                    Return "Nothing"
                Else
                    Return "DateTime.Parse(" & Format(dr(R.ColumnName), "yyyy-MM-dd").ToString.Dquotes & ")"
                End If


            Case "image"
                Dim myPic() As Byte
                myPic = dr(R.ColumnName)
                Dim MyStr As String
                MyStr = ByteToString(myPic) 'Convert.ToSingle(myPic.ToString) ' 
                Return "Convert.FromBase64String(" & MyStr.Dquotes & ")"

        End Select

        Return """" & dr(R.ColumnName) & """"
    End Function
    <Extension()>
    Public Function CopyDataFromXml(ByVal R As ColumnsInfo) As String
        Select Case R.TypeSQL.ToLower
            Case "char", "nchar", "nvarchar", "ntext"
                Return "CStr(xe.<" & R.ColumnValue & ">.Value)"
            Case "int", "int identity", "numeric", "smallint"
                Return "Cint(xe.<" & R.ColumnValue & ">.Value)"
            Case "real"
                Return "CSng(xe.<" & R.ColumnValue & ">.Value)"
            Case "money"
                Return "Cdec(xe.<" & R.ColumnValue & ">.Value)"
            Case "bit"
                Return "CBool(xe.<" & R.ColumnValue & ">.Value)"
            Case "datetime", "smalldatetime"
                Return "CDate(xe.<" & R.ColumnValue & ">.Value)"
            Case "image"
                Return "Convert.FromBase64String(xe.<" & R.ColumnValue & ">.Value)"
        End Select
        Return "CStr(xe.<" & R.ColumnValue & ">.Value)"
    End Function


    Private Function ByteToString(ByVal Value As Byte()) As String
        Dim sb As New StringBuilder
        sb.Append(Convert.ToBase64String(Value))
        Return sb.ToString
    End Function

    <Extension()> _
    Public Function ToProperCase(ByVal the_string As String) As _
    String
        ' If there are 0 or 1 characters, just return the
        ' string.
        If (the_string Is Nothing) Then Return the_string
        If (the_string.Length < 2) Then Return _
            the_string.ToUpper()

        ' Start with the first character.
        Dim result As String = the_string.Substring(0, _
            1).ToUpper()

        ' Add the remaining characters.
        For i As Integer = 1 To the_string.Length - 1
            If (Char.IsUpper(the_string(i))) Then result &= " "
            result &= the_string(i)
        Next i

        Return result
    End Function
    '<Extension()>
    'Public Function LowerTheFistChar(str As String) As String
    '    Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    'End Function
End Module
