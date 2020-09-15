Imports DBExtenderLib
Imports MSAccessManager
Imports MSSqlManager

Public Class FormMain
    Private objToSave As VBServerManager
    Private SelectedDB As DatabaseNameInfo

    Private Sub TSBMsAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBMsAccess.Click
        Dim sqlDBForm As New FormAccessDatabase
        Dim yesOrNo As DialogResult

        yesOrNo = sqlDBForm.ShowDialog()
        If yesOrNo = Windows.Forms.DialogResult.OK Then
            If objToSave Is Nothing Then
                objToSave = VBServerManager.GetInstance
            End If
            Dim DbName As DatabaseNameInfo = sqlDBForm.GetSelectedDatabase
            objToSave.DatabaseList.Add(DbName)
            Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
            SqlEditorUserControl1.LoadDatabase(objToSave)
            VbTabUserControl1.LoadDatabase(objToSave)
            CsharpTabUserControl1.LoadDatabase(objToSave)
            DatabaseObject1.SelectedDB = DbName

            'VbLinqTabEditor1.LoadDatabase(objToSave)
            'AspUserControl1.LoadDatabase(objToSave)
            'UcTables1.LoadListTables(objToSave.DatabaseList(0))
        End If
    End Sub

    Private Sub DatabaseObject1_CreateSelecViewModel(Result As TableNameInfo, db As DatabaseNameInfo) Handles DatabaseObject1.CreateSelecViewModel
        CsharpTabUserControl1.CreateSelectCsharpViewModel(Result, db)
    End Sub

    Private Sub DatabaseObject1_CreateViewModel(Result As TableNameInfo, db As DatabaseNameInfo) Handles DatabaseObject1.CreateViewModel
        CsharpTabUserControl1.CreateCsharpViewModel(Result, db)
    End Sub

    Private Sub DatabaseObject1_ResultCreateFramewordClass(Result As DBExtenderLib.TableNameInfo, db As DBExtenderLib.DatabaseNameInfo) Handles DatabaseObject1.ResultCreateFramewordClass
        VbTabUserControl1.EntityFrameworkClass(Result, db)
    End Sub

    Private Sub DatabaseObject1_ResultEntityFrameworkConnectionString(db As DBExtenderLib.DatabaseNameInfo) Handles DatabaseObject1.ResultEntityFrameworkConnectionString
        VbTabUserControl1.EntityFrameworkConnectionString(db)
    End Sub

    Private Sub DatabaseObject1_ResultEntityFrameworkEntityContext(db As DBExtenderLib.DatabaseNameInfo) Handles DatabaseObject1.ResultEntityFrameworkEntityContext
        VbTabUserControl1.EntityFrameworkDatabaseContext(db)
    End Sub
    Private Sub VbTabUserControl1_RefresRia() Handles VbTabUserControl1.RefresRia
        Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
        SqlEditorUserControl1.LoadDatabase(objToSave)
        VbTabUserControl1.LoadDatabase(objToSave)
    End Sub
    Private Sub VbTabUserControl1_RefresValues() Handles VbTabUserControl1.RefresValues
        Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
        SqlEditorUserControl1.LoadDatabase(objToSave)
        VbTabUserControl1.LoadDatabase(objToSave)
    End Sub

    Private Sub TSBClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBClose.Click
        End
    End Sub

    Private Sub TSBOpenSqlDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBOpenSqlDB.Click
        Dim sqlDBForm As New SqlServerConnectionForm
        Dim yesOrNo As DialogResult

        yesOrNo = sqlDBForm.ShowDialog()
        If yesOrNo = Windows.Forms.DialogResult.OK Then
            If objToSave Is Nothing Then
                objToSave = VBServerManager.GetInstance
            End If
            Dim DbName As DatabaseNameInfo = sqlDBForm.GetSelectedDatabase
            objToSave.DatabaseList.Add(DbName)
            Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
            SqlEditorUserControl1.LoadDatabase(objToSave)
            VbTabUserControl1.LoadDatabase(objToSave)
            CsharpTabUserControl1.LoadDatabase(objToSave)
            DatabaseObject1.SelectedDB = DbName
            'VbLinqTabEditor1.LoadDatabase(objToSave)
            'AspUserControl1.LoadDatabase(objToSave)
            'UcTables1.LoadListTables(objToSave.DatabaseList(0))
        End If
    End Sub

    Private Sub TSBOpenSqlDBFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBOpenSqlDBFile.Click
        Dim sqlDBForm As New FormSQLFileAttached
        Dim yesOrNo As DialogResult

        yesOrNo = sqlDBForm.ShowDialog()
        If yesOrNo = Windows.Forms.DialogResult.OK Then
            If objToSave Is Nothing Then
                objToSave = VBServerManager.GetInstance
            End If
            Dim DbName As DatabaseNameInfo = sqlDBForm.GetSelectedDatabase
            objToSave.DatabaseList.Add(DbName)
            Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
            SqlEditorUserControl1.LoadDatabase(objToSave)
            VbTabUserControl1.LoadDatabase(objToSave)
            CsharpTabUserControl1.LoadDatabase(objToSave)
            DatabaseObject1.SelectedDB = DbName
            ' VbLinqTabEditor1.LoadDatabase(objToSave)
            'UcTables1.LoadListTables(objToSave.DatabaseList(0))
        End If
    End Sub

    Private Sub TSBOpenSqlCompact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBOpenSqlCompact.Click
        Dim frmMobile As New FormSqlMobile
        Dim yesOrNo As DialogResult
        yesOrNo = frmMobile.ShowDialog()
        If yesOrNo = Windows.Forms.DialogResult.OK Then
            If objToSave Is Nothing Then
                objToSave = VBServerManager.GetInstance
            End If
            Dim DbName As DatabaseNameInfo = frmMobile.GetSelectedDatabase
            objToSave.DatabaseList.Add(DbName)
            frmMobile.Close()
            Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
            SqlEditorUserControl1.LoadDatabase(objToSave)
            VbTabUserControl1.LoadDatabase(objToSave)
            CsharpTabUserControl1.LoadDatabase(objToSave)
            DatabaseObject1.SelectedDB = DbName
            'VbLinqTabEditor1.LoadDatabase(objToSave)
            'AspUserControl1.LoadDatabase(objToSave)
            'UcTables1.LoadListTables(objToSave.DatabaseList(0))
        End If
    End Sub

    Private Sub DatabaseObject1_ResultView(Result As TreeNodeView) Handles DatabaseObject1.ResultView
        VbTabUserControl1.CreateViewLinq(Result.View)
        CsharpTabUserControl1.CreateViewLinq(Result.View)
    End Sub

    Private Sub TSBSave_Click(sender As System.Object, e As System.EventArgs) Handles TSBSave.Click
        If objToSave IsNot Nothing Then
            Dim SaveDBInfo As New SaveFileDialog
            'txt files (*.txt)|*.txt|All files (*.*)|*.*"

            SaveDBInfo.Filter = "XML Files(*.xml)|*.xml"
            SaveDBInfo.FilterIndex = 2
            SaveDBInfo.InitialDirectory = "MyDocuments"
            SaveDBInfo.RestoreDirectory = True
            If SaveDBInfo.ShowDialog = Windows.Forms.DialogResult.OK Then
                'GlobalInfo.FileLocation = SaveDBInfo.FileName
                'Formatter.SaveBinaryData(objToSave, SaveDBInfo.FileName)
                SaveDBToXML.SaveDBasXML(SaveDBInfo.FileName, objToSave)
            End If
        End If
    End Sub

    Private Sub TSBOpen_Click(sender As System.Object, e As System.EventArgs) Handles TSBOpen.Click
        Dim OpenDBInfo As New OpenFileDialog
        OpenDBInfo.Filter = "XML Files(*.xml)|*.xml"
        OpenDBInfo.FilterIndex = 2
        OpenDBInfo.RestoreDirectory = True
        If OpenDBInfo.ShowDialog = Windows.Forms.DialogResult.OK Then
            If objToSave Is Nothing Then
                objToSave = VBServerManager.GetInstance
                SaveDBToXML.OpenDBFromXML(OpenDBInfo.FileName, objToSave)
                'GlobalInfo.FileLocation = OpenDBInfo.FileName
                Helperclass.DisplayTree(DatabaseObject1.DBTreeView, objToSave.DatabaseList)
                SqlEditorUserControl1.LoadDatabase(objToSave)
                VbTabUserControl1.LoadDatabase(objToSave)
                SqlEditorUserControl1.LoadDatabase(objToSave)
                VbTabUserControl1.LoadDatabase(objToSave)
                CsharpTabUserControl1.LoadDatabase(objToSave)
                Dim DbName As DatabaseNameInfo = objToSave.DatabaseList(0)
                DatabaseObject1.SelectedDB = DbName
           
            End If

        End If
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class