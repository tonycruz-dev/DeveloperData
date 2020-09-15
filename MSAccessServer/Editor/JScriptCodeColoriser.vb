Option Strict On
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Text.RegularExpressions

''' <summary>
''' Coloration syntaxique JavaScript
''' </summary>
''' <remarks>Cherchant une solution rapide pour les commentaires multi-lignes,
''' uniquement les commentaires mono-lignes seront colorés</remarks>
Friend Class JScriptCodeColoriser
    Implements IColorTokenProvider
    ''' <summary>
    ''' Définition des mots clés à utiliser
    ''' </summary>
    ''' <remarks></remarks>
    Private keywords As String = "abstract|break|byte|case|catch|char|class|const|continue|default|delete|do|" & _
    "double|else|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|" & _
    "interface|long|native|null|package|private|protected|public|reset|return|short|static|super|switch|" & _
    "synchronized|this|throw|transient|true|try|var|void|while|with|new"

    Private clkeywords As String = "Anchor|Applet|Area|Arguments|Array|Boolean|Button|Checkbox|" & _
    "Collection|Crypto|Date|Dictionary|Document|Drive|Drives|Element|Enumerator|Event|File|FileObject|" & _
    "FileSystemObject|FileUpload|Folder|Folders|Form|Frame|Global|Hidden|History|HTMLElement|Image|" & _
    "Infinity|Input|JavaArray|JavaClass|JavaObject|JavaPackage|JSObject|Layer|Link|Math|MimeType|Navigator|" & _
    "Number|Object|Option|Packages|Password|Plugin|PrivilegeManager|Random|RegExp|Screen|Select|String|Submit|" & _
    "Text|Textarea|URL|VBArray|Window|WScript"

    Public Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Integer = 0) As Collection(Of ColorToken) Implements IColorTokenProvider.GetColorTokens
        Dim tokens As New Collection(Of ColorToken) 'Défintion d'une nouvelle collection de ColorTokens
        If sourceCode Is Nothing OrElse sourceCode.Length = 0 Then Return tokens 'Si le code est vide, on retourne la collection vide

        'Définition de l'index de fin
        Dim endPos As Int32 = offset + sourceCode.Length
        'Déclaration d'un RegEx repérant les mots clés
        Dim keywordRegEx As New Regex("(?<=\s|{|}|\(|\)|,|\=|^)(" & keywords & ")\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Déclaration d'un RegEx repérant les mots clés secondaires
        Dim clkeywordRegEx As New Regex("(?<=\s|{|}|\(|\)|,|\=|^)(" & clkeywords & ")\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Déclaration d'un RegEx repérant les citations
        Dim quoteRegEx As New Regex("("".*?"")", RegexOptions.CultureInvariant)
        'Regex pour les commentaires
        Dim CommentsRegEx As New Regex("(/\*)(.*)(\*/)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

        If (sourceCode.IndexOf("""") > -1) Then
            Dim subStrings() As String = quoteRegEx.Split(sourceCode) 'Division des éléments
            For Each subString As String In subStrings
                If subString.StartsWith("""") Then 'Si celui-ci commence par un guillemet
                    tokens.Add(New ColorToken(offset, subString.Length, Color.DarkRed, Color.Empty))
                Else
                    'Coloration des mots-clés principaux
                    For Each mtch As Match In keywordRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
                    Next

                    'Coloration des mots-clés secondaires
                    For Each mtch As Match In clkeywordRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkBlue, Color.Empty))
                    Next

                    ' Coloration des commentaires
                    Dim idx As Int32 = subString.IndexOf("//")
                    If idx > -1 Then
                        tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.DarkGreen, Color.Empty))
                        Return tokens 'En VB, le commentaire va jusqu'à la fin de la ligne, on peut donc quitter
                    End If
                    For Each mtch As Match In CommentsRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkGreen, Color.Empty))
                    Next

                End If
                offset += subString.Length
            Next
        Else
            'Coloration des mots-clés principaux
            For Each mtch As Match In keywordRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Blue, Color.Empty))
            Next

            'Coloration des mots-clés secondaires
            For Each mtch As Match In clkeywordRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkBlue, Color.Empty))
            Next

            'Coloration des commentaires
            Dim idx As Int32 = sourceCode.IndexOf("//")
            If idx > -1 Then
                tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.DarkGreen, Color.Empty))
            End If
            For Each mtch As Match In CommentsRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.DarkGreen, Color.Empty))
            Next

        End If

        Return tokens
    End Function
End Class

