Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Text.RegularExpressions

''' <summary>
''' Coloration syntaxique XML
''' </summary>
''' <remarks>Réalisée par Romagny13</remarks>
Friend Class XMLCodeColoriser
    Implements IColorTokenProvider

    Private keywords As String = "<\?[^\?]*\?>|\<[a-z-A-Z]+\s|<[/]?\s?[a-z-A-Z-0-9]+>|<[a-z-A-Z-0-9]+\s?\/?>" '<\?[^>]+\?> ""[^\?]*""
    Private xsl_key As String = "(xsl|xs)|xsd|\!DOCTYPE\s|xml\s|xmlns\:|xmlns\=|standalone\=|encoding\=|\-stylesheet|version\=|\smatch\=|template|stylesheet|value-of\s|for[-]each|select\=|\:apply\-templates|\:apply-imports|\:attribute|\:attribute-set|\:call-template|\:choose|\:comment|\:copy|\:copy-of|\:decimal-format|\:element|\:fallback|\:for-each|\:if|\:import|\:include|\:key|\:message|\:namespace-alias|\:number|\:otherwise|\:output|\:param|\:preserve-space|\:processing-instruction|ms\:script|\:sort|\:strip-space|\:stylesheet|\:transform|\:text|\:variable|\:when|\:with-param|current|element-available|format-number|function-available|generate-id|node-set|system-property|unparsed-entity-uri|\:output" & "\:element|\:complexType|\:simpleType|\:sequence|\:choice|\:restriction|\:enumeration|\:pattern|\:schema|msdata|\:Ordinal\=|\:IsDataSet=|\:Locale\=|\:simpleContent|\:extension|\:ColumnName\="
    Private balise As String = "\<\/|\/?>|\<\??|\?\>"
    Private attributs As String = "\s[a-z-A-Z]*\="".*?"""
    Private val_attribute As String = """.*?"""

    Public Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Integer = 0) As Collection(Of ColorToken) Implements IColorTokenProvider.GetColorTokens
        Dim tokens As New Collection(Of ColorToken)
        Dim endPos As Int32

        If sourceCode Is Nothing OrElse sourceCode.Length = 0 Then
            Return tokens
        End If

        endPos = offset + sourceCode.Length

        Dim keywordRegEx As New Regex(keywords, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        Dim keyword_xsl As New Regex(xsl_key, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        Dim keyword_balise As New Regex(balise, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        Dim keyword_attribute As New Regex(attributs, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        Dim keyword_val_attribute As New Regex(val_attribute, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        Dim quoteRegEx As New Regex("(<>"".*?""<>)", RegexOptions.CultureInvariant) '("(""<.*?>"")"
        Dim CommentsRegEx As New Regex("(<!--)(.*)(-->)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

        If (sourceCode.IndexOf("""") > -1) Then
            ' Séparation de la ligne
            Dim subStrings() As String = quoteRegEx.Split(sourceCode)

            ' On parcourt chaque element splitté selon l'espace
            For Each subString As String In subStrings

                '> commence par un guillemet " (valeurs attributs)
                If subString.StartsWith("""") Then

                    tokens.Add(New ColorToken(offset, subString.Length, Color.Blue, Color.Empty))

                Else
                    'COLORATION BALISES et contenu HTML
                    For Each mtch As Match In keywordRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkRed, Color.Empty))
                    Next

                    ' Mots réservés XSL
                    For Each mt As Match In keyword_xsl.Matches(subString)
                        tokens.Add(New ColorToken(offset + mt.Index, mt.Length, Color.DarkCyan, Color.Empty))
                    Next

                    ' BALISES < > </ /> ?
                    For Each mtbalise As Match In keyword_balise.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtbalise.Index, mtbalise.Length, Color.DarkBlue, Color.Empty))
                    Next

                    ' Attributs
                    For Each mtbalise As Match In keyword_attribute.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtbalise.Index, mtbalise.Length, Color.Red, Color.Empty))
                    Next

                    ' Valeurs des attributs (entre guillemets)
                    For Each mtbalise As Match In keyword_val_attribute.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtbalise.Index, mtbalise.Length, Color.Blue, Color.Empty))
                    Next

                    'Coloration des commentaires
                    For Each mtch As Match In CommentsRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkGreen, Color.Empty))
                    Next

                End If

                offset += subString.Length

            Next

        Else

            'COLORATION BALISES et contenu HTML
            For Each mtch As Match In keywordRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkRed, Color.Empty))
            Next

            ' Mots réservés XSL
            For Each mt As Match In keyword_xsl.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mt.Index, mt.Length, Color.DarkCyan, Color.Empty))
            Next

            ' BALISES < > </ /> ? etc.
            For Each mtbalise As Match In keyword_balise.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtbalise.Index, mtbalise.Length, Color.DarkBlue, Color.Empty))
            Next

            ' Attributs
            For Each mtbalise As Match In keyword_attribute.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtbalise.Index, mtbalise.Length, Color.Red, Color.Empty))
            Next

            ' Valeurs des attributs (entre guillemets)
            For Each mtbalise As Match In keyword_val_attribute.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtbalise.Index, mtbalise.Length, Color.Blue, Color.Empty))
            Next

            ' Coloration des commentaires
            For Each mtch As Match In CommentsRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkGreen, Color.Empty))
            Next

        End If

        Return tokens
    End Function
End Class


