Option Strict On : Option Explicit On : Option Compare Binary : Option Infer On

Imports System.Text.RegularExpressions
Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.ObjectModel


Public Class SQLCodeColoriser
    Implements IColorTokenProvider
    Private keywords As String = _
                  "(?:\u0023)Region|(?:\u0023)End\s+Region|" & _
                   "(?:\u0023)If|(?:\u0023)Else|(?:\u0023)ElseIf|(?:\u0023)End|(?:\u0023)Const|" & _
                   "Custom\s+Event|" & _
                  "Option\s+Strict\s+Off|Option\s+Explicit\s+Off|Option\s+Strict|Option\s+Explicit|Option\s+Compare\s+Binary|Option\s+Compare\s+Text|" & _
                  "Alias|And|smalldatetime|Money|nvarchar|int|ntext|" & _
                  "ADD|ALL|ALTER|AND|ANY|AS|ASC|AUTHORIZATION|smallint|image|" & _
                  "CHAR|VARCHAR|BACKUP|BEGIN|BETWEEN|BREAK|BROWSE|BULK|BY|CASCADE|" & _
                  "CASE|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COALESCE|COLLATE|COLUMN|" & _
                  "INSER|UPDATE|DELETE|COMMIT|COMPUTE|CONSTRAINT|CONTAINS|CONTAINSTABLE|CONTINUE|CONVERT|" & _
                  "CREATE|CROSS|CURRENT|CURRENT_DATE|CURRENT_TIME|CURRENT_TIMESTAMP|" & _
                  "CURRENT_USER|CURSOR|DATABASE|DBCC|DEALLOCATE|DECLARE|DEFAULT|DELETE|DENY|" & _
                  "DESC|DISK|DISTINCT|DISTRIBUTED|DOUBLE|DROP|DUMMY|DUMP|ELSE|END|ERRLVL|ESCAPE|" & _
                  "EXCEPT|EXEC|EXECUTE|EXISTS|EXIT|FETCH|FILE|FILLFACTOR|FOR|FOREIGN|FREETEXT|" & _
                  "FREETEXTTABLE|FROM|FULL|FUNCTION|GOTO|GRANT|GROUP|HAVING|HOLDLOCK|IDENTITY|" & _
                  "IDENTITY_INSERT|IDENTITYCOL|IF|IN|INDEX|INNER|INSERT|INTERSECT|INTO|IS|JOIN|KEY|KILL|" & _
                  "LEFT|LIKE|LINENO|LOAD|NATIONAL|NOCHECK|NONCLUSTERED|NOT|NULL|NULLIF|OF|OFF|OFFSETS|" & _
                  "ON|OPEN|OPENDATASOURCE|OPENQUERY|OPENROWSET|OPENXML|OPTION|OR|ORDER|OUTER|OVER|" & _
                  "PERCENT|PLAN|PRECISION|PRIMARY|PRINT|PROC|PROCEDURE|PUBLIC|RAISERROR|READ|READTEXT|" & _
                  "RECONFIGURE|REFERENCES|REPLICATION|RESTORE|RESTRICT|RETURN|REVOKE|RIGHT|" & _
                  "ROLLBACK|ROWCOUNT|ROWGUIDCOL|RULE|SAVE|SCHEMA|SELECT|SESSION_USER|SET|SETUSER|" & _
                  "SHUTDOWN|NCHAR|SOME|STATISTICS|SYSTEM_USER|TABLE|TEXTSIZE|THEN|TO|TOP|TRAN|TRANSACTION|" & _
                  "TRIGGER|DATETIME|TRUNCATE|TSEQUAL|UNION|UNIQUE|UPDATE|UPDATETEXT|USE|USER|VALUES|VARYING|" & _
                  "VIEW|WAITFOR|WHEN|WHERE|WHILE|WITH|WRITETEXT|GO|"

    Public Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Integer = 0) As System.Collections.ObjectModel.Collection(Of ColorToken) Implements IColorTokenProvider.GetColorTokens
        'Dim tokens As New Collection(Of ColorToken)

        '' early exit 
        'If sourceCode Is Nothing OrElse sourceCode.Length = 0 Then Return tokens


        'Dim endPos As Int32 = offset + sourceCode.Length
        'Dim keywordRegEx As New Regex("(?<=\s|{|}|\(|\)|,|\=|^)(" & keywords & ")\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

        'Dim quoteRegEx As New Regex("("".*?"")", RegexOptions.CultureInvariant)


        'If (sourceCode.IndexOf("'") > -1) Then

        '    Dim subStrings() As String = quoteRegEx.Split(sourceCode)

        '    For Each subString As String In subStrings

        '        If subString.StartsWith("'") Then
        '            tokens.Add(New ColorToken(offset, subString.Length, Color.DarkRed, Color.Empty))

        '        Else

        '            For Each mtch As Match In keywordRegEx.Matches(subString)
        '                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
        '            Next

        '            Dim idx As Int32 = subString.IndexOf("--")

        '            If idx > -1 Then
        '                tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.DarkRed, Color.Empty))

        '                'Return tokens ' early exit
        '            End If

        '        End If

        '        offset += subString.Length

        '    Next

        'Else

        '    For Each mtch As Match In keywordRegEx.Matches(sourceCode)
        '        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
        '    Next

        '    Dim idx As Int32 = sourceCode.IndexOf("'"c)

        '    If idx > -1 Then
        '        tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.Red, Color.Empty))
        '    End If
        'End If

        'Return tokens



        Dim tokens As New Collection(Of ColorToken)

        ' early exit 
        If sourceCode Is Nothing OrElse sourceCode.Length = 0 Then Return tokens


        Dim endPos As Int32 = offset + sourceCode.Length
        Dim keywordRegEx As New Regex("(?<=\s|{|}|\(|\)|,|\=|^)(" & keywords & ")\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

        'Dim quoteRegEx As New Regex("(""(?:[^""]|"""")*"");?", RegexOptions.CultureInvariant)
        '                         ("(\"(?:[^\"]|\"\")*\");?");
        'Dim regex As New Regex("(""(?:[^""]|"""")*"");?")
        Dim quoteRegEx As New Regex("("".*?"")", RegexOptions.CultureInvariant)

        If (sourceCode.IndexOf("""") > -1) Then

            Dim subStrings() As String = quoteRegEx.Split(sourceCode)

            For Each subString As String In subStrings

                If subString.StartsWith("""") Then
                    tokens.Add(New ColorToken(offset, subString.Length, Color.DarkRed, Color.Empty))

                Else

                    For Each mtch As Match In keywordRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
                    Next

                    Dim idx As Int32 = subString.IndexOf("'"c)

                    If idx > -1 Then
                        tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.DarkRed, Color.Empty))

                        'Return tokens ' early exit
                    End If

                End If

                offset += subString.Length

            Next

        Else

            For Each mtch As Match In keywordRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
            Next

            Dim idx As Int32 = sourceCode.IndexOf("'"c)

            If idx > -1 Then
                tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.Red, Color.Empty))
            End If
        End If

        Return tokens
    End Function
End Class
