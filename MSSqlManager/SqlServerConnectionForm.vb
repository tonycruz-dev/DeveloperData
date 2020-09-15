Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports System.Windows.Forms
Imports DBExtenderLib

Public Class SqlServerConnectionForm
    Private SelectedSvr As Server
    Private svrCnn As ServerConnection
    Private DBSelected As New DatabaseNameInfo
    Private bError As Boolean
    Private SelectedServer As String
    Private Const SqlProvider As String = "System.Data.SqlClient"
    Private countTables As Integer
    Private countProcedures As Integer
    Private CountViews As Integer
    Private _db As Database
    Private _ConnectionString As String

    Private Sub SqlServerConnectionForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dt As DataTable = SmoApplication.EnumAvailableSqlServers(True)
        If (dt.Rows.Count > 0) Then
            For Each r As DataRow In dt.Rows
                Me.cmdServers.Items.Add(r("Name"))
            Next
            cmdServers.SelectedIndex = cmdServers.FindStringExact(Environment.MachineName)
            If (cmdServers.SelectedIndex < 0) Then
                cmdServers.SelectedIndex = 0
            End If

        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Close()
    End Sub

    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        Dim csr As Windows.Forms.Cursor = Nothing
        If Not svrCnn Is Nothing Then
            svrCnn = Nothing
        End If
        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            bError = False ' Assume no error


            ' Recreate connection if necessary
            If svrCnn Is Nothing Then
                svrCnn = New ServerConnection
            End If

            '    ' Fill in necessary information
            svrCnn.ServerInstance = cmdServers.Text
            SelectedServer = cmdServers.Text
            ' Setup capture and execute to be able to display script
            svrCnn.SqlExecutionModes = SqlExecutionModes.ExecuteAndCaptureSql
            svrCnn.ConnectTimeout = 15
            If Me.RBWindows.Checked = True Then
                ' Use Windows authentication
                svrCnn.LoginSecure = True


                _ConnectionString = "Data Source=" & SelectedServer & ";Initial Catalog=" & CBDatabase.Text & ";Integrated Security=True"

            Else
                ' Use SQL Server authentication
                svrCnn.LoginSecure = False
                svrCnn.Login = txtUserName.Text
                svrCnn.Password = txtPassword.Text
                
                _ConnectionString = "Data Source=" & SelectedServer & ";Initial Catalog=" & CBDatabase.Text & ";User ID=" & txtUserName.Text & ";Password=" & txtPassword.Text & ";"
                
            End If
            SelectedSvr = New Server(svrCnn)
            SelectedSvr.SetDefaultInitFields(GetType(Database), New String() {"IsSystemObject"})
            SelectedSvr.SetDefaultInitFields(GetType(Table), New String() {"IsSystemObject", "Schema"})
            SelectedSvr.SetDefaultInitFields(GetType(Microsoft.SqlServer.Management.Smo.View), New String() {"IsSystemObject", "Schema"})
            SelectedSvr.SetDefaultInitFields(GetType(StoredProcedure), New String() {"IsSystemObject"})
            SelectedSvr.SetDefaultInitFields(GetType(Column), New String() {"Name", "DataType", "SystemType", "Length", _
                    "NumericPrecision", "NumericScale", "Nullable", "InPrimaryKey", "Identity"})


            ' Limit the properties returned to just those that we use
            'SelectedSvr.SetDefaultInitFields(GetType(Database), New _
            '    String() {"Name", "IsSystemObject", "IsAccessible"})

            '' Limit the properties returned to just those that we use
            'SelectedSvr.SetDefaultInitFields(GetType(Table), New _
            '    String() {"Name", "CreateDate", "IsSystemObject"})

            '' Limit the properties returned to just those that we use
            'SelectedSvr.SetDefaultInitFields(GetType(Column), New _
            '    String() {"Name", "DataType", "SystemType", "Length", _
            '    "NumericPrecision", "NumericScale", "XmlSchemaNamespace", _
            '    "XmlSchemaNamespaceSchema", "DataTypeSchema", "Nullable", _
            '    "InPrimaryKey"})

            Dim objDBList As New List(Of DatabaseNameInfo)

            Dim dbcounter As Integer = 0
            For Each db As Database In SelectedSvr.Databases
                If Not db.IsSystemObject Then
                    Dim objDB As New DatabaseNameInfo
                    objDB.DatabaseID = dbcounter
                    objDB.DatabaseName = db.Name
                    objDB.ServerName = SelectedSvr.Name
                    objDBList.Add(objDB)
                    dbcounter += 1
                End If
            Next

            'Dim qdb = From dbinserver In SelectedSvr.Databases Select dbinserver

            'Dim qServer = From lstDB In qdb.
            '              Select lstDB
            Me.CBDatabase.DataSource = objDBList
            Me.CBDatabase.DisplayMember = "DatabaseName"
            Me.CBDatabase.ValueMember = "DatabaseName"





        Catch ex As ConnectionFailureException
            Dim emb As New Microsoft.SqlServer.MessageBox.ExceptionMessageBox(ex)
            emb.Show(Me)
            bError = True

        Catch ex As SmoException
            Dim emb As New Microsoft.SqlServer.MessageBox.ExceptionMessageBox(ex)
            emb.Show(Me)
            bError = True

        Finally
            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub

    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        _db = SelectedSvr.Databases.Item(Me.CBDatabase.SelectedValue)
        _ConnectionString = "Data Source=" & SelectedServer & ";Initial Catalog=" & CBDatabase.Text & ";Integrated Security=True"
        Dim oManageWorker As New ManageWorker(_db, DBSelected)
        Me.BackgroundWorker1.RunWorkerAsync(oManageWorker)

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim oManageWorker As ManageWorker = CType(e.Argument, ManageWorker)
        DBSelected = ProcessDatabase(oManageWorker.dbObject, oManageWorker.dbObject.Name, oManageWorker.DBSelected)
        e.Result = DBSelected
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Me.lblResults.Text = e.UserState.ToString()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        DBSelected = CType(e.Result, DatabaseNameInfo)
        ProgressBar1.Value = 1
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

#Region "Process Database"

    Public Function ProcessDatabase(ByVal DB As Database, ByVal ServerName As String, ByVal objDB As DatabaseNameInfo) As DatabaseNameInfo
        objDB.DatabaseName = DB.Name
        objDB.ServerName = SelectedServer
        objDB.IntegratedSecurity = True
        objDB.providerName = SqlProvider
        objDB.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServer
        objDB.ConnectionString = _ConnectionString
        countTables = 0
        countProcedures = 0
        CountViews = 0
        loadTableLinq(DB, objDB)
        loadSP_Procedures(DB, objDB)
        loadViews(DB, objDB)
        objDB.isDataLoad = True
        Return objDB
    End Function
    Sub loadTableLinq(ByVal dbName As Database, ByVal objDB As DatabaseNameInfo)
        Dim rtb = (From tb As Table In dbName.Tables Where tb.IsSystemObject = False _
                  Select New TableNameInfo With {.TableName = tb.Name, _
                                                 .SchemaTable = tb.Schema, _
                                                 .Database = objDB, _
                                                 .StrConnection = objDB.ConnectionString, _
                                                 .ListColumn = LoadFieldLinq(dbName, tb)}).AsEnumerable

        objDB.ListTable.AddRange(rtb)
    End Sub

    'Sub loadTable(ByVal dbName As Database, ByVal objDB As DatabaseNameInfo)
    Private Function LoadFieldLinq(ByVal dbName As Database, ByVal objTB As Table) As List(Of ColumnsInfo)

        Dim lstCol = (From col As Column In objTB.Columns _
                     Select New ColumnsInfo With { _
                     .ColumnName = col.Name,
                     .IsRequared = IIf(col.Nullable, False, True),
                     .IsPrimary_Key = col.InPrimaryKey,
                     .IsForeign_Key = col.IsForeignKey,
                     .ForeignKey = objTB.GetForeignKey(col.Name),
                     .Size = col.DataType.MaximumLength,
                     .TypeSQL = col.DataType.Name,
                     .TypeVB = GetVBDataType(col.DataType.Name),
                     .LinqVar = col.GetLinqVar,
                     .VarCSharp = GetCSharp(col),
                     .LinqVarCSharp = GetLinqVarCSharp(col),
                     .TypeMySql = GetMySqlDataType(col)}).ToList


        countTables += 1
        Dim progress As Integer = countTables / dbName.Tables.Count * 100
        Dim msg As String = progress & "% Table(" & objTB.Name & ")"
        Me.BackgroundWorker1.ReportProgress(progress, msg)
        Return lstCol

        'objTB.ForeignKeys
    End Function
    Private Sub loadSP_Procedures(ByVal dbName As Database, ByVal objDB As DatabaseNameInfo)
        Dim listSP = (From pro As StoredProcedure In dbName.StoredProcedures Where pro.IsSystemObject = False _
                     Select New StoredProcedureNameInfo With { _
                     .SP_Name = pro.Name, .SP_TextBody = pro.TextBody, _
                     .SP_TextHeader = pro.TextHeader, .SP_Parameters = LoadSPParameter(dbName, pro)}).ToList
        objDB.ListSPro.AddRange(listSP)
    End Sub

    Private Function LoadSPParameter(ByVal dbName As Database, ByVal objSP As StoredProcedure) As List(Of SP_Parameters)


        Dim spp = (From Param As StoredProcedureParameter In objSP.Parameters _
                  Select New SP_Parameters With { _
                  .ParamiterName = Param.Name, .ParamMaxLenght = Param.DataType.MaximumLength, _
                  .Paramiter_DataType = Param.DataType.Name}).ToList

        countProcedures += 1
        Dim progress As Integer = countProcedures / dbName.StoredProcedures.Count * 100
        Dim msg As String = progress & "% Procedure( " & objSP.Name & ")"
        Me.BackgroundWorker1.ReportProgress(progress, msg)

        Return spp

    End Function

    Private Sub loadViews(ByVal dbName As Database, ByVal objDB As DatabaseNameInfo)
        Dim counter As Integer = 0

        Dim qr = (From OView As Microsoft.SqlServer.Management.Smo.View In dbName.Views Where OView.IsSystemObject = False _
                 Select New ViewNameInfo With { _
                 .ViewName_Name = OView.Name, _
                .TextBody = OView.TextBody, _
                .TextHeader = OView.TextHeader, .ViewColumns = LoalViewColumns(dbName, OView)}).ToList
        objDB.ListViews.AddRange(qr)
    End Sub
    Private Function LoalViewColumns(ByVal dbName As Database, ByVal objTB As Microsoft.SqlServer.Management.Smo.View) As List(Of ColumnsInfo)

        Dim lstCol = (From Col As Column In objTB.Columns
                      Select New ColumnsInfo With {.ColumnName = Col.Name,
                                                  .Size = Col.DataType.MaximumLength.ToString,
                                                  .TypeSQL = Col.DataType.Name,
                                                  .TypeVB = GetVBDataType(Col.DataType.Name),
                                                  .IsRequared = Col.Nullable,
                                                  .LinqVar = Col.GetLinqVar}).ToList
        CountViews += 1
        Dim progress As Integer = CountViews / dbName.Views.Count * 100
        Dim msg As String = progress & "% View(" & objTB.Name & ")"
        Me.BackgroundWorker1.ReportProgress(progress, msg)
        Return lstCol

    End Function

    
#End Region
    Public ReadOnly Property GetSelectedDatabase() As DatabaseNameInfo
        Get
            Return DBSelected
        End Get
    End Property
End Class
Public Structure ManageWorker
    Private _dbObject As Database
    Private _DBSelected As DatabaseNameInfo
    Public Sub New(ByVal pdbObject As Database, ByVal dbSelected As DatabaseNameInfo)
        _dbObject = pdbObject
        _DBSelected = dbSelected
    End Sub
    Public ReadOnly Property dbObject() As Database
        Get
            Return _dbObject
        End Get
    End Property
    Public ReadOnly Property DBSelected() As DatabaseNameInfo
        Get
            Return _DBSelected
        End Get
    End Property
End Structure