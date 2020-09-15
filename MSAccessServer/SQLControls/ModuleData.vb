Imports DBExtenderLib
Imports System.Runtime.CompilerServices
Imports System.Text

Public Module ModuleData
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
                MyStr = ByteToString(myPic)
                Return "Convert.FromBase64String(" & MyStr.Dquotes & ")"

        End Select

        Return """" & dr(R.ColumnName) & """"
    End Function
    <Extension()>
    Public Function Dquotes(_StrValue As String) As String
        Return """" & _StrValue & """"
    End Function
    Private Function ByteToString(ByVal Value As Byte()) As String
        Dim sb As New StringBuilder
        sb.Append(Convert.ToBase64String(Value))
        Return sb.ToString
    End Function
End Module
