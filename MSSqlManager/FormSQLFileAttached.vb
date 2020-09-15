Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports DBExtenderLib
Imports System.IO
Public Class FormSQLFileAttached
    Private DBSelected As New DatabaseNameInfo
    Private StrFilepath As String
    Private StrFileName As String
    Private StrConnection As String
    Private Const SqlProvider As String = "System.Data.SqlClient"
    Private _db As Database
    Private CountTables As Integer = 0
    Private CountViews As Integer = 0
    Private CountProce As Integer = 0
    Private Sub btnFindPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindPath.Click

        OpenFileDialog1.Filter = "SQL Database(*.mdf)|*.mdf" '"Microsoft Office Access (*.mdb)(*.accdb)|*.mdb|*.accdb"
        OpenFileDialog1.FilterIndex = 2
        '(*.txt)|*.txt|All files (*.*)|*.*"
        Dim result As Windows.Forms.DialogResult = OpenFileDialog1.ShowDialog()

        If result = Windows.Forms.DialogResult.OK Then
            StrFilepath = OpenFileDialog1.FileName
            StrFileName = OpenFileDialog1.SafeFileName
            Dim IntStart As Integer = InStr(1, StrFilepath, ".")

            ' Dim mystention As String = Mid(StrFilepath, IntStart + 1, StrFilepath.Length)
            Dim mystention As String = Path.GetExtension(StrFileName)


            txtFullLocation.Text = StrFilepath
        End If



    End Sub

    Private Sub btnProcessData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcessData.Click

        If RBWindows.Checked Then
            StrConnection = "Data Source=.\SQLEXPRESS;AttachDbFilename=" & StrFilepath & ";Integrated Security=True;User Instance=True"
        Else
            StrConnection = "Data Source=.\SQLEXPRESS;AttachDbFilename=" & StrFilepath & ";User ID=" & txtUserName.Text & ";password=" & txtPassword.Text & ";User Instance=True"
        End If

        'User ID=MyUsername;password=MyPass;
        Using connection As New SqlConnection(StrConnection) '"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|Club.mdf;Integrated Security=True;User Instance=True")            ')

            connection.Open()

            Dim svrCnn As New ServerConnection(connection)

            Dim SelectedSvr = New Server(svrCnn) '"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\NORTHWND.MDF;Integrated Security=True;User Instance=True")
            SelectedSvr.SetDefaultInitFields(GetType(Database), New String() {"IsSystemObject"})
            SelectedSvr.SetDefaultInitFields(GetType(Table), New String() {"IsSystemObject", "Schema"})
            SelectedSvr.SetDefaultInitFields(GetType(View), New String() {"IsSystemObject", "Schema"})
            SelectedSvr.SetDefaultInitFields(GetType(StoredProcedure), New String() {"IsSystemObject"})
            SelectedSvr.SetDefaultInitFields(GetType(Column), New String() {"Name", "DataType", "SystemType", "Length", _
                    "NumericPrecision", "NumericScale", "Nullable", "InPrimaryKey", "Identity"})

            _db = SelectedSvr.Databases.Item(connection.Database)

            Dim oManageWorker As New ManageWorker(_db, DBSelected)
            Me.BackgroundWorker1.RunWorkerAsync(oManageWorker)

        End Using
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
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
        'DelDatabaseInfo.Invoke(DBSelected)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#Region "Process Database"

    Public Function ProcessDatabase(ByVal DB As Database, ByVal ServerName As String, ByVal objDB As DatabaseNameInfo) As DatabaseNameInfo
        objDB.DatabaseName = StrFileName
        objDB.ServerName = ServerName
        objDB.IntegratedSecurity = True
        objDB.dbFileConnection = StrConnection
        objDB.ConnectionString = StrConnection
        objDB.providerName = SqlProvider
        objDB.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerFile
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
                                                 .ListColumn = LoadFieldLinq(dbName, tb)}).ToList



        objDB.ListTable.AddRange(rtb)


    End Sub
    Private Function LoadFieldLinq(ByVal dbName As Database, ByVal objTB As Table) As List(Of ColumnsInfo)

        Dim lstCol = (From col As Column In objTB.Columns _
                     Select New ColumnsInfo With {.ColumnName = col.Name, _
                                                  .IsPrimary_Key = col.InPrimaryKey, _
                                                  .IsForeign_Key = col.IsForeignKey, _
                                                  .ForeignKey = GetForeignKey(objTB, col.Name), _
                                                  .Size = col.DataType.MaximumLength, _
                                                  .TypeSQL = col.DataType.Name, _
                                                  .TypeVB = GetVBDataType(col.DataType.Name),
                                                  .VarCSharp = GetCSharp(col),
                                                  .LinqVarCSharp = GetLinqVarCSharp(col),
                                                  .TypeMySql = GetMySqlDataType(col)}).ToList



        Dim progress As Integer = CountTables / dbName.Tables.Count * 100
        Dim msg As String = progress & "% Table(" & objTB.Name & ")"
        Me.BackgroundWorker1.ReportProgress(progress, msg)
        CountTables += 1
        Return lstCol

        'objTB.ForeignKeys
    End Function
    Private Function GetForeignKey(ByVal ObjTB As Table, ByVal columnName As String) As ForeignKeyInfo
        Dim objfk As ForeignKeyInfo = Nothing
        Dim cfk = From fk As ForeignKey In ObjTB.ForeignKeys _
                   Select fk

        If Not cfk Is Nothing Then
            If cfk.Count > 0 Then
                For Each ck In cfk
                    For Each c In ck.Columns
                        If c.Name = columnName Then
                            objfk = New ForeignKeyInfo With {.ColumnName = c.Name, .ForeignKeyName = ck.ReferencedKey, _
                                                             .RelatedTable = ck.ReferencedTable, _
                                                             .RelatedColumnName = c.ReferencedColumn}
                            Return objfk
                        End If
                    Next
                Next
            End If
        End If

        Return objfk
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


        CountProce += 1
        Dim progress As Integer = CountProce / dbName.StoredProcedures.Count * 100
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
        Dim counter As Integer = 0
        Dim lstCol = (From Col As Column In objTB.Columns _
                     Select New ColumnsInfo With {.ColumnName = Col.Name, _
                                                  .Size = Col.DataType.MaximumLength.ToString, _
                                                  .TypeSQL = Col.DataType.Name, _
                                                  .TypeVB = GetVBDataType(Col.DataType.Name), _
                                                  .IsRequared = Col.Nullable, _
                                                  .LinqVar = Col.GetLinqVar}).ToList
        CountViews += 1
        Dim progress As Integer = counter / dbName.Views.Count * 100
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