Imports DBExtenderLib
Imports System.IO
Imports Microsoft.Office.Interop.Access
Imports Microsoft.Office.Interop.Access.Dao

Public Class FormAccessDatabase
    Private StrFilepath As String
    Private DBSelected As New DatabaseNameInfo
    Private DBResults As DatabaseNameInfo
    Private Const StrProvider As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
    Private Const StrProvider2007 As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
    Private IsAccess2007 As Boolean
    Dim countVies As Integer = 0
    Private oManageWorker As ManageMsAccess
    Private Sub ButtonBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBrowse.Click
        OpenFileDialog1.InitialDirectory = "My Documents"
        OpenFileDialog1.Filter = "Microsoft Access (*.mdb;*.accdb)|*.mdb;*.accdb"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        Dim result As Windows.Forms.DialogResult = OpenFileDialog1.ShowDialog()
        oManageWorker = New ManageMsAccess
        If result = Windows.Forms.DialogResult.OK Then
            oManageWorker.DatabasePath = OpenFileDialog1.FileName
            oManageWorker.DatabaseExt = Path.GetExtension(OpenFileDialog1.FileName)
            oManageWorker.FilePath = Path.GetDirectoryName(OpenFileDialog1.FileName)
            oManageWorker.DatabaseName = Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
            oManageWorker.DatabaseNameExt = OpenFileDialog1.FileName
            TextBoxDatabasePath.Text = Path.GetFileName(OpenFileDialog1.FileName)
            If oManageWorker.DatabaseExt = ".accdb" Then
                oManageWorker.IsAccess2007 = True
            Else
                oManageWorker.IsAccess2007 = False
            End If
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Me.BgWAccess.RunWorkerAsync(oManageWorker)
        Button1.Enabled = False
    End Sub

    Private Sub BgWAccess_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgWAccess.DoWork
        Dim oMana As ManageMsAccess = CType(e.Argument, ManageMsAccess)


        'DBSelected.DatabaseName = oMana.DatabaseNameExt
        'DBSelected.MSAccessPath = StrFilepath
        DBResults = ProcessDatabase(OpenConnection(oMana), oMana)
        e.Result = DBResults
    End Sub
#Region " Private Functions"
    Private Function OpenConnection(ByVal ma As ManageMsAccess) As ADOX.Catalog
        Dim db As New ADOX.Catalog
        Dim cnn As New ADODB.Connection

        If ma.IsAccess2007 Then
            cnn.Open(StrProvider2007 & ma.DatabasePath)
            db.ActiveConnection = cnn
            DBSelected.ConnectionString = StrProvider2007 & ma.DatabasePath
        Else
            cnn.Open(StrProvider & ma.DatabasePath)
            db.ActiveConnection = cnn
            DBSelected.ConnectionString = StrProvider & ma.DatabasePath
        End If
        Return db
    End Function
#End Region

    Public Function ProcessDatabase(ByVal DB As ADOX.Catalog, ByVal magAccess As ManageMsAccess) As DatabaseNameInfo
        Dim AccessDatabase As New DatabaseNameInfo
        AccessDatabase.ServerName = "Access Database"
        AccessDatabase.IntegratedSecurity = True
        AccessDatabase.DatabaseName = magAccess.DatabaseName
        AccessDatabase.DatabaseFileName = magAccess.DatabaseNameExt
        AccessDatabase.ConnectionString = magAccess.ConnectionString
        AccessDatabase.providerName = My.Settings.DBAccessProvider
        AccessDatabase.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftAccess
        AccessDatabase.IsAccess2007 = magAccess.IsAccess2007
        AccessDatabase.DatabaseFileName = magAccess.DatabaseNameExt
        AccessDatabase.ListTable = ProcessMsAccessTable(AccessDatabase.DatabaseName, DB, magAccess)
        AccessDatabase.MSAccessPath = magAccess.FilePath

        AccessDatabase.ListViews = ProcessMsAccessView(DB, magAccess)
        AccessDatabase.ListSPro = ProcessStoredProcedure(DB)
        Return AccessDatabase
    End Function

#Region "Process Table"
    Public Function ProcessMsAccessTable(ByVal StrDBName As String, ByVal dbName As ADOX.Catalog, ByVal magAccess As ManageMsAccess) As List(Of TableNameInfo)
        Dim counter As Integer = 0
        Dim ResultTables As New List(Of TableNameInfo)
        Dim TableResuls = From tb As ADOX.Table In
                          dbName.Tables Where
                          tb.Type <> "MSys" AndAlso
                          tb.Type = "TABLE"
                          Select tb

        For Each crntTable In TableResuls

            'Dim ColumnsList As New List(Of ColumnsInfo) From {{crntTable}}
            Dim objTB As New TableNameInfo With
                            {.TableName = crntTable.Name,
                             .SchemaTable = "",
                             .StrConnection = magAccess.ConnectionString,
                            .ListColumn = GetListColumn(crntTable, magAccess)
                            }
            counter += 1
            Dim progress As Integer = counter / dbName.Tables.Count * 100
            Dim msg As String = progress & "% Table(" & crntTable.Name & ")"
            Me.BgWAccess.ReportProgress(progress, msg)
            ResultTables.add(objTB)
        Next
        Return ResultTables

    End Function

    Private Function GetTable(ByVal TableName As String, ByVal StrCnn As String) As List(Of PreserveColumn)
        Dim cnn As New ADODB.Connection
        cnn.Open(StrCnn)
        cnn.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Dim rsSchema As ADODB.Recordset
        rsSchema = cnn.OpenSchema(ADODB.SchemaEnum.adSchemaColumns, New Object() {Nothing, Nothing, TableName})
        rsSchema.Sort = "ORDINAL_POSITION"
        Dim lstFields As New List(Of PreserveColumn)
        While Not rsSchema.EOF
            lstFields.Add(New PreserveColumn With {.ColumnsName = rsSchema.Fields("COLUMN_NAME").Value})
            rsSchema.MoveNext()
        End While
        Return lstFields
    End Function

#End Region

#Region "Process Views"
    Public Function ProcessMsAccessView(ByVal dbName As ADOX.Catalog, ByVal magAccess As ManageMsAccess) As List(Of ViewNameInfo)
        Dim counter As Integer = 0
        Dim resultViews As New List(Of ViewNameInfo)

        Dim qview = From lv As ADOX.Table In dbName.Tables
                    Where Microsoft.VisualBasic.Left(lv.Name, 4) <> "MSys" And
                    lv.Type = "VIEW"
                    Select lv

        For Each curViw In qview
            'Dim ColumnsList As New List(Of ColumnsInfo) From {curViw}
            Dim oView As New ViewNameInfo With
                {
                 .ViewName_Name = curViw.Name, _
                 .Schema_ViewName = "", _
                 .TextBody = GetViewBodyText(dbName, curViw.Name), _
                 .ViewColumns = GetListColumnView(curViw, magAccess)
                }
            resultViews.Add(oView)
            counter += 1
            Dim progress As Integer = counter / dbName.Views.Count * 100
            Dim msg As String = progress & "% View ( " & curViw.Name & " )"
            Me.BgWAccess.ReportProgress(progress, msg)
        Next
        Return resultViews
    End Function
    Public Function GetViewBodyText(ByVal dbName As ADOX.Catalog, ByVal ViewName As String) As String
        Dim cmd As ADODB.Command
        cmd = dbName.Views(ViewName).Command
        Return cmd.CommandText
    End Function

    Public Function GetViewBodyText(ByVal DatabaseName As String, ByVal ViewName As String) As String
        Dim db As Dao.Database
        Dim dbE As New Dao.DBEngine

        Dim dbEngine As New Microsoft.Office.Interop.Access.Dao.DBEngine
        db = dbEngine.OpenDatabase(DatabaseName)
        Dim query As QueryDef

        With db
            query = .QueryDefs(ViewName)
            For Each r As Field In query.Fields

            Next
        End With
        If Not query Is Nothing Then
            Return query.SQL
        Else
            Return " "
        End If

        
    End Function

#End Region

#Region "Process StoredProcedure"

    Public Function ProcessStoredProcedure(ByVal dbName As ADOX.Catalog) As List(Of StoredProcedureNameInfo)
        Dim ResultSP As New List(Of StoredProcedureNameInfo)
        Dim counter As Integer = 0

        Dim osp = From sp As ADOX.Procedure In dbName.Procedures _
                  Where Microsoft.VisualBasic.Left(sp.Name, 4) <> "MSys" AndAlso Microsoft.VisualBasic.Left(sp.Name, 3) <> "~sq"



        For Each curPro In osp
            Dim sp As New StoredProcedureNameInfo With {
                .SP_Name = curPro.Name, _
                .SP_TextBody = GetSPBodyText(dbName, curPro.Name)}

            ' .SP_Parameters = LoadSPParameter(dbName, curPro)}
            ' curPro.
            ResultSP.Add(sp)
            counter += 1
            Dim progress As Integer = counter / dbName.Views.Count * 100
            Dim msg As String = progress & "% View ( " & curPro.Name & " )"
            Me.BgWAccess.ReportProgress(progress, msg)
        Next
        Return ResultSP
    End Function

    Public Function GetSPBodyText(ByVal dbName As ADOX.Catalog, ByVal SPName As String) As String
        Dim cmd As ADODB.Command
        cmd = dbName.Procedures(SPName).Command  '  oView.Command
        Return cmd.CommandText
    End Function
    Private Function LoadSPParameter(ByVal dbName As ADOX.Catalog, ByVal objSP As ADOX.Procedure) As List(Of SP_Parameters)
        Dim cmd As ADODB.Command
        cmd = dbName.Procedures(objSP.Name).Command  '  oView.Command
        Dim qp = (From dpp As ADODB.Parameter In cmd.Parameters _
                 Select New SP_Parameters With { _
                  .ParamiterName = dpp.Name, _
                  .ParamMaxLenght = dpp.NumericScale,
                  .Paramiter_DataType = dpp.Type.GetParamType}).ToList
        Return qp
    End Function
#End Region

    Private Sub BgWAccess_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BgWAccess.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Me.lblResults.Text = e.UserState.ToString()
    End Sub

    Private Sub BgWAccess_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgWAccess.RunWorkerCompleted
        DBResults = CType(e.Result, DatabaseNameInfo)
        ProgressBar1.Value = 1
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Button1.Enabled = True
    End Sub
    Public ReadOnly Property GetSelectedDatabase() As DatabaseNameInfo
        Get
            Return DBResults
        End Get
    End Property

End Class