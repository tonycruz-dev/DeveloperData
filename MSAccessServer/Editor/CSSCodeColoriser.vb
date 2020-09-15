Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Text.RegularExpressions

Public Class CSSCodeColoriser
    Implements IColorTokenProvider

    Private keywords As String = "azimuth|background-attachment|background-color|background-image|" & _
    "background-position|background-repeat|background|border-bottom-color|border-bottom-style|" & _
    "border-bottom-width|border-bottom|border-collapse|border-color|border-left-color|" & _
    "border-left-style|border-left-width|border-left|border-right-color|border-right-style|" & _
    "border-right-width|border-right|border-spacing|border-style|border-top-color|" & _
    "border-top-style|border-top-width|border-top|border-width|border|bottom|caption-side|" & _
    "clear|clip|color|content|counter-increment|counter-reset|cue-after|cue-before|cue|" & _
    "cursor|direction|display|elevation|empty-cells|float|font-family|font-size|font-size-adjust|" & _
    "font-stretch|font-style|font-variant|font-weight|font|height|left|letter-spacing|line-height|" & _
    "list-style|list-style-image|list-style-position|list-style-type|margin-bottom|margin-left|" & _
    "margin-right|margin-top|margin|marker-offset|marks|max-height|max-width|min-height|min-width|" & _
    "orphans|outline-color|outline-style|outline-width|outline|overflow|padding-bottom|padding-left|" & _
    "padding-right|padding-top|padding|page-break-after|page-break-before|page-break-inside|" & _
    "page|pause-after|pause-before|pause|pitch-range|pitch|play-during|position|quotes|richness|" & _
    "right|size|speak-header|speak-numeral|speak-punctuation|speak|speech-rate|stress|table-layout|" & _
    "text-align|text-decoration|text-indent|text-shadow|text-transform|top|unicode-bidi|vertical-align|" & _
    "visibility|voice-family|volume|white-space|windows|width|word-spacing|z-index"

    'Définition des mots clés type (X)HTML
    Private Htmlkeywords As String = "BODY|DIV|SPAN|TABLE|TD|TR|TEXTAREA|A:link|A:visited|A:active|FRAME|IFRAME|" & _
    "A:hover|A|INPUT|SELECT|TH|OL|UL|LI|BUTTON|OBJECT|LABEL|P|IMG|H1|H2|H3|H4|H5|PRE|CODE|FORM|FONT|OPTION"

    Public Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Integer = 0) As Collection(Of ColorToken) Implements IColorTokenProvider.GetColorTokens
        Dim tokens As New Collection(Of ColorToken) 'Nouvelle collection
        If sourceCode Is Nothing OrElse sourceCode.Length = 0 Then Return tokens 'Si le code est vide, on quitte
        Dim endPos As Int32 = offset + sourceCode.Length ' Définition de la position de fin

        'Nouveau RegEx pour les mots-clés HTML
        Dim htmlkeywordRegEx As New Regex("(}|\s|^)(" & Htmlkeywords & ")\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Regex pour les noms des classes
        Dim ClassesRegEx As New Regex("(\.)([a-zA-z0-9]*)\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Regex pour les mots-clés CSS
        Dim keywordRegEx As New Regex("(\s|{|^)(" & keywords & ")(^*:|^:|\s*:|\s:|:)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Regex pour les valeurs entrées
        Dim valuesRegEx As New Regex("(\s:|:)(.*)(;|}|\s;|\s})", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Regex pour les commentaires
        Dim CommentsRegEx As New Regex("(/\*|<!--)(.*)(\*/|-->)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

        'Recher des mots clés HTML
        For Each mtch As Match In htmlkeywordRegEx.Matches(sourceCode)
            tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkGoldenrod, Color.Empty))
        Next

        'Recherche des classes
        For Each mtch As Match In ClassesRegEx.Matches(sourceCode)
            tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkOrchid, Color.Empty))
        Next

        'Recherche des mots clés CSS
        For Each mtch As Match In keywordRegEx.Matches(sourceCode)
            tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkRed, Color.Empty))
        Next

        'Recherche des valeurs entrées
        For Each mtch As Match In valuesRegEx.Matches(sourceCode)
            tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
        Next

        'Recherche des commentaires
        For Each mtch As Match In CommentsRegEx.Matches(sourceCode)
            tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkGreen, Color.Empty))
        Next

        Return tokens
    End Function
End Class
