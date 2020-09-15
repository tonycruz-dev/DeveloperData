
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Xml
Imports DBExtenderLib

Public Class SaveDBToXML
    Public Shared Function SaveDBasXML(ByVal SavePath As String, ByVal oManager As VBServerManager) As String

        Dim xmlval = <VBSpiderManager>
                         <%= From db In oManager.DatabaseList _
                             Select <Database Name=<%= db.Name %>>
                                        <Connection ConnectionString=<%= db.ConnectionString %>
                                            Provider=<%= db.providerName %>
                                            databasetype=<%= db.SelectedTypeOfDatabase.ToString %>/>
                                        <%= From tb In db.ListTable _
                                            Select <Table Name=<%= tb.TableName %> Member=<%= tb.TableName %>>
                                                       <Type Name=<%= tb.TableName %>>
                                                           <%= From col In tb.ListColumn _
                                                               Select <Column Name=<%= col.ColumnName %>
                                                                          Type=<%= col.LinqVar %>
                                                                          Size=<%= col.Size %>
                                                                          TypeSQL=<%= col.TypeSQL %>
                                                                          TypeVB=<%= col.TypeVB %>
                                                                          LinqVar=<%= col.LinqVar %>
                                                                          LinqVarCSharp=<%= col.LinqVarCSharp %>
                                                                          VarCSharp=<%= col.VarCSharp %>
                                                                          IsRequared=<%= col.IsRequared %>
                                                                          IsPrimary_Key=<%= col.IsPrimary_Key %>
                                                                          IsForeign_Key=<%= col.IsForeign_Key %>
                                                                          IsAutoincrement=<%= col.IsAutoincrement %>
                                                                          ForeignKeyColumnName=<%= GetForengKeyName(col) %>
                                                                          RelatedTable=<%= GetForengKeyRelatedTable(col) %>
                                                                          ForeignKeyName=<%= GetForengKeyForeignKeyName(col) %>
                                                                          ForeignRelatedColumnName=<%= GetForengKeyForeignRelatedColumnName(col) %>/> %>
                                                       </Type>
                                                   </Table> %>

                                        <%= From tb In db.ListViews _
                                            Select <View Name=<%= tb.ViewName_Name %>
                                                       Member=<%= tb.ViewName_Name %>>
                                                       <Type Name=<%= tb.ViewName_Name %>>
                                                           <%= From col In tb.ViewColumns _
                                                               Select <Column Name=<%= col.ColumnName %>
                                                                          Type=<%= col.LinqVar %>
                                                                          Size=<%= col.Size %>
                                                                          TypeSQL=<%= col.TypeSQL %>
                                                                          TypeVB=<%= col.TypeVB %>
                                                                          LinqVar=<%= col.LinqVar %>
                                                                          LinqVarCSharp=<%= col.LinqVarCSharp %>
                                                                          VarCSharp=<%= col.VarCSharp %>
                                                                          IsRequared=<%= col.IsRequared %>
                                                                          IsPrimary_Key=<%= col.IsPrimary_Key %>
                                                                          IsForeign_Key=<%= col.IsForeign_Key %>
                                                                          IsAutoincrement=<%= col.IsAutoincrement %>/> %>
                                                       </Type>
                                                       <ViewHeader><%= "<![CDATA[" & tb.TextHeader & "]]>" %></ViewHeader>
                                                       <ViewDetails><%= "<![CDATA[" & tb.TextBody & "]]>" %></ViewDetails>
                                                   </View> %>
                                        <%= From sp In db.ListSPro _
                                            Select <Proc Name=<%= sp.SP_Name %> Member=<%= sp.SP_Name %>>
                                                       <Type Name=<%= sp.SP_Name %>>
                                                           <%= From col In sp.SP_Parameters _
                                                               Select <Column Name=<%= col.ParamiterName %>
                                                                          Type=<%= col.Paramiter_DataType %>
                                                                          Size=<%= col.ParamMaxLenght %>/> %>
                                                       </Type>
                                                       <ProcHeader><%= "<![CDATA[" & sp.SP_TextHeader & "]]>" %></ProcHeader>
                                                       <ProcTextBody><%= "<![CDATA[" & sp.SP_TextBody & "]]>" %></ProcTextBody>
                                                   </Proc> %>

                                    </Database> %>
                     </VBSpiderManager>
        xmlval.Save(SavePath, SaveOptions.None)
        'Dim SB As New StringBuilder(xmlval.ToString)
        'SB.Replace("&lt;", "<")
        'SB.Replace("&gt;", ">")
        'My.Computer.FileSystem.WriteAllText(SavePath, SB.ToString, False)
        Return ""
    End Function
    Public Shared Sub OpenDBFromXML(ByVal FileName As String, ByVal MainObj As VBServerManager)

        'RunTestConnection(FileName)

        Dim xmldoc = XDocument.Load(FileName)


        Try
            Dim ListDB = (From xe In xmldoc...<Database> _
                          Select New DatabaseNameInfo _
                               With {.DatabaseName = xe.@Name, .SelectedTypeOfDatabase = GetTypeDatabse(xe.<Connection>.@databasetype), _
                                .DBPath = xe.<Connection>.@DBPath, _
                                .ConnectionString = xe...<Connection>.@ConnectionString, _
                                .ListTable = (From tb In xe.<Table> _
                                              Select New TableNameInfo _
                                              With {.TableName = tb.@Name, _
                                                    .StrConnection = xe...<Connection>.@ConnectionString, _
                                                    .ListColumn = (From col In tb.<Type>.<Column> _
                                                                     Select New ColumnsInfo With {.ColumnName = col.@Name,
                                                                                                  .LinqVar = col.@Type,
                                                                                                  .Size = col.@Size,
                                                                                                  .TypeSQL = col.@TypeSQL,
                                                                                                  .TypeVB = col.@TypeVB,
                                                                                                  .IsRequared = col.@IsRequared,
                                                                                                  .IsPrimary_Key = col.@IsPrimary_Key,
                                                                                                  .LinqVarCSharp = col.@LinqVarCSharp,
                                                                                                  .VarCSharp = col.@VarCSharp,
                                                                                                  .IsForeign_Key = col.@IsForeign_Key,
                                                                                                  .IsAutoincrement = col.@IsAutoincrement,
                                                                                                  .ForeignKey = New ForeignKeyInfo With {.ColumnName = col.@ForeignKeyColumnName,
                                                                                                                                         .ColumnTypeCS = col.@VarCSharp,
                                                                                                                                         .ColumnTypeVB = col.@TypeVB,
                                                                                                                                         .RelatedColumnName = col.@ForeignRelatedColumnName,
                                                                                                                                         .ForeignKeyName = col.@ForeignKeyName,
                                                                                                                                         .RelatedTable = col.@RelatedTable}}).ToList}).ToList, _
                             .ListViews = (From lv In xe.<View> _
                                               Select New ViewNameInfo With {.ViewName_Name = lv.@Name, _
                                                                             .TextBody = lv.<ViewDetails>.Value, _
                                                                             .TextHeader = lv.<ViewHeader>.Value, _
                                                                             .ViewColumns = (From col In lv.<Type>.<Column> _
                                                                                              Select New ColumnsInfo With {.ColumnName = col.@Name, _
                                                                                                  .Size = col.@Size,
                                                                                                  .TypeSQL = col.@TypeSQL,
                                                                                                  .LinqVar = col.@Type, _
                                                                                                  .TypeVB = col.@TypeVB,
                                                                                                  .IsRequared = col.@IsRequared, _
                                                                                                  .IsPrimary_Key = col.@IsPrimary_Key,
                                                                                                  .LinqVarCSharp = col.@LinqVarCSharp,
                                                                                                  .VarCSharp = col.@VarCSharp,
                                                                                                  .IsForeign_Key = col.@IsForeign_Key, _
                                                                                                  .IsAutoincrement = col.@IsAutoincrement}).ToList}).ToList, _
                             .ListSPro = (From lv In xe.<Proc> _
                                               Select New StoredProcedureNameInfo With {.SP_Name = lv.@Name, _
                                                                             .SP_TextHeader = lv.<ProcHeader>.Value, _
                                                                             .SP_TextBody = lv.<ProcTextBody>.Value, _
                                                                             .SP_Parameters = (From col In lv.<Type>.<Column> _
                                                                                              Select New SP_Parameters With {.ParamiterName = col.@Name, _
                                                                                                  .ParamMaxLenght = col.@Size, .Paramiter_DataType = col.@Type}).ToList}).ToList}).ToList

            For Each d In ListDB

                For Each t In d.ListTable
                    t.Database = d
                    For Each c In t.ListColumn
                        c.Table = t
                    Next
                Next
                For Each lv In d.ListViews
                    lv.Database = d
                Next
                MainObj.DatabaseList.Add(d)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Shared Function GetForengKeyName(ByVal col As ColumnsInfo) As String
        If Not col.ForeignKey Is Nothing Then
            Return col.ForeignKey.ColumnName
        Else
            Return ""
        End If
    End Function
    Private Shared Function GetForengKeyRelatedTable(ByVal col As ColumnsInfo) As String
        If Not col.ForeignKey Is Nothing Then
            Return col.ForeignKey.RelatedTable
        Else
            Return ""
        End If
    End Function
    Private Shared Function GetForengKeyForeignKeyName(ByVal col As ColumnsInfo) As String
        If Not col.ForeignKey Is Nothing Then
            Return col.ForeignKey.ForeignKeyName
        Else
            Return ""
        End If
    End Function
    Private Shared Function GetForengKeyForeignRelatedColumnName(ByVal col As ColumnsInfo) As String
        If Not col.ForeignKey Is Nothing Then
            Return col.ForeignKey.RelatedColumnName
        Else
            Return ""
        End If
    End Function
    Private Shared Function GetTypeDatabse(ByVal strVar As String) As DBExtenderLib.DatabaseNameInfo.TypeOfDatabase
        Select Case strVar
            Case "MicrosoftAccess"
                Return DatabaseNameInfo.TypeOfDatabase.MicrosoftAccess
            Case "MicrosoftSqlServer"
                Return DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServer
            Case "MicrosoftSqlServerFile"
                Return DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerFile
            Case "MicrosoftSqlServerMobile"
                Return DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerMobile
            Case Else
                Return DatabaseNameInfo.TypeOfDatabase.MySqlServer
        End Select


    End Function

    'col.ForeignKey.RelatedTable %> ForeignKeyName=<%= col.ForeignKey.ForeignKeyName

    Private Shared Sub RunTestConnection(ByVal FileName As String)
        Dim xmldoc = XDocument.Load(FileName)

        Dim ListDB = (From xe In xmldoc...<Database> _
                          Select New DatabaseNameInfo _
                               With {.DatabaseName = xe.<Database>.@Name, _
                                .DBPath = xe.<Connection>.@DBPath, _
                                .dbFileConnection = xe...<Connection>.@ConnectionString, _
                                .ListTable = (From tb In xe...<Table> _
                                              Select New TableNameInfo _
                                              With {.TableName = tb.@Name, _
                                                    .StrConnection = xe...<Connection>.@ConnectionString, _
                                                    .ListColumn = (From col In tb...<Column> _
                                                                     Select New ColumnsInfo With {.ColumnName = col.@Name, .LinqVar = col.@Type, _
                                                                                                  .Size = col.@Size, .TypeSQL = col.@TypeSQL, _
                                                                                                  .TypeVB = col.@TypeVB, .IsRequared = col.@IsRequared, _
                                                                                                  .IsPrimary_Key = col.@IsPrimary_Key, .IsForeign_Key = col.@IsForeign_Key, _
                                                                                                  .IsAutoincrement = col.@IsAutoincrement, _
                                                                                                  .ForeignKey = New ForeignKeyInfo With {.ColumnName = col.@ForeignKeyColumnName, .ForeignKeyName = col.@ForeignKeyName, .RelatedTable = col.@RelatedTable}}).ToList}).ToList})
        For Each db In ListDB
            Console.WriteLine(db.Name)
        Next
    End Sub
End Class
