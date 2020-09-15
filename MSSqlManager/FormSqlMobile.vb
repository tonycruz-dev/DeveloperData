Imports System.Data.SqlServerCe
Imports System.IO
Imports DBExtenderLib

Public Class FormSqlMobile
    Private Delegate Sub AddToListDelegate(Of T)(ByRef list As List(Of T), ByVal dr As SqlCeDataReader)

    Private DBSelected As New DatabaseNameInfo
    Private StrFilepath As String
    Private StrFileName As String
    Private _connectionString As String
    Private QListForeignKeys As List(Of QueryForeignKey)

    Private Sub btnFindPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindPath.Click
        OpenFileDialog1.Filter = "SQL Server Compact Database(*.sdf)|*.sdf" '"Microsoft Office Access (*.mdb)(*.accdb)|*.mdb|*.accdb"
        OpenFileDialog1.FilterIndex = 2
        Dim result As Windows.Forms.DialogResult = OpenFileDialog1.ShowDialog()

        If result = Windows.Forms.DialogResult.OK Then
            StrFilepath = OpenFileDialog1.FileName
            StrFileName = OpenFileDialog1.SafeFileName
            txtFullLocation.Text = StrFilepath

        End If
    End Sub
    Private Sub btnProcessData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcessData.Click
        _connectionString = "Data Source=" & StrFilepath & "; Password =" & txtPassword.Text
        Me.BackgroundWorker1.RunWorkerAsync(_connectionString)
    End Sub
    Sub loadTable(ByVal objDB As DatabaseNameInfo)
        Dim counter As Integer = 0

        Dim listTables As New List(Of TableNameInfo)
        Using cnn As New SqlCeConnection(_connectionString)
            cnn.Open()
            Dim numRec As Integer = CountTableRecordNum(cnn)
            Using cmd As New SqlCeCommand("SELECT table_name FROM information_schema.tables WHERE TABLE_TYPE = N'TABLE'", cnn)
                Using dr As SqlCeDataReader = cmd.ExecuteReader
                    While dr.Read
                        listTables.Add(New TableNameInfo With {.Database = objDB, .SchemaTable = "", .StrConnection = _connectionString, .TableName = dr.Item("table_name"), .ListColumn = LoadFieldLinq(cnn, dr.Item("table_name"))})
                        counter += 1
                        Dim progress As Integer = counter / numRec * 100
                        Dim msg As String = progress & "% Table(" & dr.Item("table_name") & ")"
                        Me.BackgroundWorker1.ReportProgress(progress, msg)

                    End While

                End Using
            End Using
        End Using
        objDB.ListTable.AddRange(listTables)
    End Sub
    Private Function LoadFieldLinq(ByVal Cnn As SqlCeConnection, ByVal tableName As String) As List(Of ColumnsInfo)
        Dim listCol As New List(Of ColumnsInfo)
        Dim counter As Integer = 0
        Dim numRec As Integer = CountColumnRecordNum(Cnn, tableName)
        Using cmd As New SqlCeCommand("SELECT Column_name, is_nullable, data_type, character_maximum_length, numeric_precision, autoinc_increment, autoinc_seed, column_hasdefault, column_default, column_flags, numeric_scale FROM information_schema.columns WHERE table_name = '" & tableName & "' AND (column_name NOT LIKE '__sys%') ORDER BY ordinal_position ASC ", Cnn)
            Using dr As SqlCeDataReader = cmd.ExecuteReader
                While dr.Read
                    Dim col As New ColumnsInfo With {.ColumnName = dr.Item("Column_name"), _
                                                     .IsPrimary_Key = IsPrimaryKey(Cnn, tableName, dr.Item("Column_name")), _
                                                     .IsForeign_Key = isCompactForeignKey(tableName, dr.Item("Column_name")), _
                                                     .ForeignKey = GetForeignKey(tableName, dr.Item("Column_name")), _
                                                     .Size = IIf(dr.IsDBNull(3), 0, dr(3)), _
                                                     .TypeSQL = dr.Item("data_type"), _
                                                     .TypeVB = GetVBDataType(dr.Item("data_type")), _
                                                     .LinqVar = GetLinqVar(dr.Item("data_type"), dr.Item("is_nullable")), _
                                                     .TypeMySql = GetVBDataType(dr.Item("data_type"))}

                    listCol.Add(col)
                    counter += 1
                    Dim progress As Integer = counter / numRec * 100
                    Dim msg As String = progress & "% Column(" & col.ColumnName & ")"
                    Me.BackgroundWorker1.ReportProgress(progress, msg)

                End While
            End Using
        End Using
        Return listCol

    End Function
    Private Function CountColumnRecordNum(ByVal Cnn As SqlCeConnection, ByVal tableName As String) As Integer
        Dim count As Integer = 0
        Using cmd As New SqlCeCommand("SELECT Count(*) FROM information_schema.columns WHERE table_name = '" & tableName & "' AND (column_name NOT LIKE '__sys%') ", Cnn)
            Using dr As SqlCeDataReader = cmd.ExecuteReader
                While dr.Read
                    count = dr(0)
                End While
            End Using
        End Using
        Return count
    End Function
    Private Function CountTableRecordNum(ByVal Cnn As SqlCeConnection) As Integer
        Dim count As Integer = 0
        Using cmd As New SqlCeCommand("SELECT count(*) FROM information_schema.tables WHERE TABLE_TYPE = N'TABLE'", Cnn)
            Using dr As SqlCeDataReader = cmd.ExecuteReader
                While dr.Read
                    count = dr(0)
                End While
            End Using
        End Using
        Return count
    End Function
    Private Function IsPrimaryKey(ByVal Cnn As SqlCeConnection, ByVal TableName As String, ByVal ColumnName As String) As Boolean
        Using cmd As New SqlCeCommand("SELECT u.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS c INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON c.CONSTRAINT_NAME = u.CONSTRAINT_NAME where u.TABLE_NAME = '" & TableName & "' and c.CONSTRAINT_TYPE = 'PRIMARY KEY' and u.COLUMN_NAME ='" & ColumnName & "'", Cnn)
            Dim rn As SqlCeDataReader = cmd.ExecuteReader
            While rn.Read
                Return True
            End While
            Return False
        End Using
    End Function
    Private Function isCompactForeignKey(ByVal TableName As String, ByVal ColumnName As String) As Boolean
        If QListForeignKeys Is Nothing Then
            LoadForeignkey()
        End If
        Dim rfk = (From fk In QListForeignKeys Where fk.FKTableName = TableName And fk.FKColumnName = ColumnName).Count
        If rfk > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function GetForeignKey(ByVal TableName As String, ByVal ColumnName As String) As ForeignKeyInfo
        If isCompactForeignKey(TableName, ColumnName) Then
            Dim rfk = (From fk In QListForeignKeys Where fk.FKTableName = TableName And fk.FKColumnName = ColumnName _
                       Select New ForeignKeyInfo With {.ColumnName = fk.FKColumnName, _
                              .ForeignKeyName = fk.FKName, .RelatedTable = fk.FKFromTable, _
                              .RelatedColumnName = fk.FKFromTablecolumn}).FirstOrDefault
            Return rfk
        Else
            Dim objfk As ForeignKeyInfo = Nothing
            Return objfk
        End If
    End Function
    Private Sub LoadForeignkey()
        QListForeignKeys = New List(Of QueryForeignKey)
        Using cnn As New SqlCeConnection(_connectionString)
            cnn.Open()
            Using cmd As New SqlCeCommand("SELECT KCU1.TABLE_NAME AS FK_TABLE_NAME, KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME, KCU1.COLUMN_NAME AS FK_COLUMN_NAME, KCU2.TABLE_NAME AS UQ_TABLE_NAME, KCU2.CONSTRAINT_NAME AS UQ_CONSTRAINT_NAME, KCU2.COLUMN_NAME AS UQ_COLUMN_NAME, KCU2.ORDINAL_POSITION AS UQ_ORDINAL_POSITION, KCU1.ORDINAL_POSITION AS FK_ORDINAL_POSITION FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 ON KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 ON KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION ORDER BY FK_TABLE_NAME, FK_CONSTRAINT_NAME, FK_ORDINAL_POSITION", cnn)
                Using dr As SqlCeDataReader = cmd.ExecuteReader
                    While dr.Read
                        QListForeignKeys.Add(New QueryForeignKey With {.FKTableName = dr(0), .FKColumnName = dr(2), .FKFromTable = dr(3), .FKFromTablecolumn = dr(5)})
                    End While

                End Using
            End Using
        End Using

    End Sub
    Private Function ExecuteReader(Of T)(ByVal commandText As String, ByVal AddToListMethod As AddToListDelegate(Of T)) As List(Of T)
        Dim list As New List(Of T)()
        Using cn As New SqlCeConnection(_connectionString)
            cn.Open()
            Using cmd As New SqlCeCommand(commandText, cn)
                Using dr As SqlCeDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        AddToListMethod(list, dr)
                    End While

                End Using
            End Using
        End Using
        Return list
    End Function

    Private Function ExecuteDataTable(ByVal commandText As String) As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlCeConnection(_connectionString)
            cn.Open()
            Using cmd As New SqlCeCommand(commandText, cn)
                Using da As New SqlCeDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function
    Private Function ExecuteScalar(ByVal commandText As String) As Object
        Dim val As Object = Nothing
        Using cn As New SqlCeConnection(_connectionString)
            cn.Open()
            Using cmd As New SqlCeCommand(commandText, cn)
                val = cmd.ExecuteScalar()
            End Using
        End Using
        Return val
    End Function

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim objdb As New DatabaseNameInfo
        objdb.DatabaseName = StrFileName
        objdb.providerName = "System.Data.SqlServerCe"
        objdb.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerMobile
        objdb.SqlCompactConnection = _connectionString
        objdb.ConnectionString = _connectionString
        loadTable(objdb)
        e.Result = objdb

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
    Public ReadOnly Property GetSelectedDatabase() As DatabaseNameInfo
        Get
            Return DBSelected
        End Get
    End Property
    Public Function GetLinqVar(ByVal col As String, ByVal is_nullable As String) As String
        Dim newtype As String = ""
        If is_nullable = "YES" Then
            Select Case LCase(col)
                Case "numeric", "int", "bigint"
                    Return "System.Nullable(Of Integer)"
                Case "varchar", "ntext", "text", "Char"
                    Return "String"
                Case "bit"
                    Return "Boolean"
                Case "date", "datetime", "timestamp"
                    Return "System.Nullable(Of Date)"
                Case "smallInt"
                    Return "System.Nullable(Of Short)"
                Case "int", "bigint", "numeric"
                    Return "System.Nullable(Of Integer)"
                Case "smallInt"
                    Return "System.Nullable(Of Short)"
                Case "real"
                    Return "System.Nullable(Of Single)"
                Case "money"
                    Return "System.Nullable(Of Decimal)"
                Case "VarBinaryMax"
                    Return "Byte()"
                Case "image"
                    Return "Byte()"
                Case "UniqueIdentifier"
                    Return "GUID"
            End Select
        Else
            Select Case LCase(col)
                Case "numeric", "int", "bigint"
                    Return "System.Nullable(Of Integer)"
                Case "varchar", "ntext", "text", "Char"
                    Return "String"
                Case "bit"
                    Return "Boolean"
                Case "date", "datetime", "timestamp"
                    Return "Date"
                Case "smallInt"
                    Return "Short"

                Case "int", "bigint", "numeric"
                    Return "Integer"
                Case "smallInt"
                    Return "Short"
                Case "real"
                    Return "Single"
                Case "money"
                    Return "Decimal"
                Case "VarBinaryMax"
                    Return "Byte()"
                Case "image"
                    Return "Byte()"
                Case "UniqueIdentifier"
                    Return "GUID"
            End Select
        End If

        Return "String"

    End Function
End Class
Public Structure QueryForeignKey
    Public FKTableName As String
    Public FKName As String
    Public FKColumnName As String
    Public FKFromTable As String
    Public FKFromTablecolumn As String
End Structure


