'Option Strict On
'Option Explicit On


Imports System.Text.RegularExpressions
Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
'

''' <summary>
''' Code coloriser for the best language, VB.NET !!!
''' </summary>
''' <remarks>
''' uses simple regex.
'''  TODO: colors for comments, strings and keywords are hard coded. It might be nice to put that into a config file
''' </remarks>
Public Class ASPNetCodeColoriser
    Implements IColorTokenProvider

    Private keywords As String = _
              "asp:Content|" & _
              "asp:DropDownList|asp:Label|asp:TextBox|asp:Button|" & _
              "asp:LinkButton|" & _
              "asp:ImageButton|asp:HyperLink|asp:DropDownList|asp:ListBox|asp:CheckBox|asp:CheckBoxList|" & _
              "asp:RadioButton|asp:RadioButtonList|asp:Image|asp:ImageMap|" & _
              "asp:Table|asp:BulletedList|asp:HiddenField|asp:Literal|" & _
              "asp:Calendar|asp:AdRotator|asp:FileUpload|asp:Wizard|" & _
              "WizardSteps|asp:WizardStep|asp:Xml|asp:MultiView|" & _
              "asp:Panel|asp:PlaceHolder|asp:View|asp:Substitution|" & _
              "asp:Localize|asp:GridView|Columns|asp:BoundField|" & _
              "asp:CheckBoxField|asp:HyperLinkField|asp:ImageField|asp:ButtonField|" & _
              "asp:CommandField|asp:DataList|ItemTemplate|asp:ObjectDataSource|" & _
              "asp:DetailsView|DeleteParameters|asp:Parameter|UpdateParameters|" & _
              "InsertParameters|asp:FormView|EditItemTemplate|InsertItemTemplate>|" & _
              "asp:ListView|asp:Repeater|Double|Each|" & _
              "asp:DataPager|asp:SqlDataSource|asp:AccessDataSource|asp:LinqDataSource|" & _
              "asp:XmlDataSource|asp:SiteMapDataSource|asp:RequiredFieldValidator|asp:RangeValidator|" & _
              "asp:RegularExpressionValidator|asp:CompareValidator|asp:CustomValidator|asp:ValidationSummary|" & _
              "asp:Menu|asp:SiteMapPath|asp:TreeView|asp:Login|" & _
              "asp:LoginView|asp:PasswordRecovery|asp:LoginStatus|asp:LoginName|"

    Public Function GetColorTokens(ByVal sourceCode As String, Optional ByVal offset As Integer = 0) As System.Collections.ObjectModel.Collection(Of ColorToken) Implements IColorTokenProvider.GetColorTokens
        Dim tokens As New Collection(Of ColorToken)

        ' early exit 
        If sourceCode Is Nothing OrElse sourceCode.Length = 0 Then Return tokens


        Dim endPos As Int32 = offset + sourceCode.Length
        Dim keywordRegEx As New Regex("(<\/?)(?i:(?<element>(" & keywords & ")))", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Dim keywordRegEx As New Regex("(<\/?)(?i:(?<element>(" & keywords & ")))(\s(?<attr>.+?))*>", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        'Dim keywordRegEx As New Regex("(?<=\s|{|}|\(|\)|,|\=|^)(" & keywords & ")\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

        Dim quoteRegEx As New Regex("("".*?"")", RegexOptions.CultureInvariant)


        If (sourceCode.IndexOf("""") > -1) Then

            Dim subStrings() As String = quoteRegEx.Split(sourceCode)

            For Each subString As String In subStrings

                If subString.StartsWith("""") Then
                    tokens.Add(New ColorToken(offset, subString.Length, Color.Blue, Color.Empty))

                Else

                    For Each mtch As Match In keywordRegEx.Matches(subString)
                        tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Brown, Color.Empty))
                    Next

                    Dim idx As Int32 = subString.IndexOf("<!--")

                    If idx > -1 Then
                        tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.DarkGreen, Color.Empty))

                        Return tokens ' early exit
                    End If

                End If

                offset += subString.Length

            Next

        Else

            For Each mtch As Match In keywordRegEx.Matches(sourceCode)
                tokens.Add(New ColorToken(offset + mtch.Index, mtch.Length, Color.Brown, Color.Empty))
            Next

            Dim idx As Int32 = sourceCode.IndexOf("<!--")

            If idx > -1 Then
                tokens.Add(New ColorToken(offset + idx, endPos - offset - idx, Color.DarkGreen, Color.Empty))
            End If
        End If

        Return tokens
    End Function
End Class
