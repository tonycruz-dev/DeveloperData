Imports DBExtenderLib
'Imports ManagLinqFile
Imports System.Text
Imports DBCodeGenerator
Imports ManagLinqFile
Imports System.IO

Public Class vbTabUserControl
    Private oManager As VBServerManager
    Private _SelectedDatabase As DatabaseNameInfo
    Private LinqDB As LinqDatabaseNameInfo
    Public Event DBChnges(ByVal dsValue As DatabaseNameInfo)
    Public Event DisplayLinqValues(ByVal Results As String)
    Public Event RefresValues()
    Public Event RefresRia()
    ' Private WithEvents wpfapps As WPFApplicationManager.Application
    Public Sub LoadDatabase(ByVal objToSave As VBServerManager)
        oManager = objToSave

        'Me.BSDatabase.DataSource = objToSave.DatabaseList
        TSCmbDatabase.ComboBox.DataSource = Nothing
        TSCmbDatabase.ComboBox.DataSource = oManager.DatabaseList
        TSCmbDatabase.ComboBox.DisplayMember = "DatabaseName"
        TSCmbDatabase.ComboBox.ValueMember = "DatabaseID"

        _SelectedDatabase = oManager.DatabaseList.Item(0)

        TSCmbTables.ComboBox.DataSource = Nothing
        TSCmbTables.ComboBox.DataSource = _SelectedDatabase.ListTable
        TSCmbTables.ComboBox.DisplayMember = "TableName"
        TSCmbTables.ComboBox.ValueMember = "TableID"
    End Sub
    Public Sub displayRsults(ByVal Results As String)
        VBCodeEditor.Text = Results
    End Sub

    Private Sub VBModelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBModelToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = VBClassManager.CreateVBClass2010(objTable)
    End Sub

    Private Sub ModelInotifyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModelInotifyToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = VBClassManager.CreateVBClass2010_INotifyPropertyChanged(objTable)
    End Sub

    Private Sub VBClass2010SampleData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClass2010SampleData.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = VBClassManager.VBClass2010SampleData(objTable)
    End Sub
    Private Sub VBClass2010XmlSampleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClass2010XmlSampleToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = VBClassManager.VBClass2010XMLSample(objTable)
    End Sub
    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        VBCodeEditor.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        VBCodeEditor.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        VBCodeEditor.Paste()
    End Sub
    Private Sub VBClassRia2010_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClassRia2010.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageRiaClass.VBClassRia2010(objTable, _SelectedDatabase)
    End Sub

    Private Sub TSMVBRiaSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMVBRiaSelect.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageRiaClass.RiaSelect(objTable, _SelectedDatabase.DatabaseName)

    End Sub

    Private Sub TSMVBRiaUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMVBRiaUpdate.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageRiaClass.RiaUpdate(objTable, _SelectedDatabase.DatabaseName)

    End Sub

    Private Sub VBClassRiaInsertToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClassRiaInsertToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageRiaClass.RiaInsert(objTable, _SelectedDatabase.DatabaseName)

    End Sub

    Private Sub TestClassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestClassToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageRiaClass.RiaSelectManager(objTable, _SelectedDatabase)

        'Dim ralateTables = objTable.GetRalationalTables(_SelectedDatabase)
        'Dim strTB As String = ""

        'For Each t In ralateTables
        '    strTB &= t.RelateTableName & vbNewLine
        '    strTB &= t.SelectedTableLinqKey & vbNewLine
        '    strTB &= t.ForeignKey & vbNewLine
        'Next
        'VBCodeEditor.Text = strTB 'ManageRiaClass.RiaInsert(objTable, _SelectedDatabase.DatabaseName)

    End Sub

    Private Sub TestClassFKToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestClassFKToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim strTB As String = ""

        For Each t In objTable.GetMasterTables(_SelectedDatabase)
            strTB &= t.RelateTableName & vbNewLine
        Next

        Dim ralateTables = objTable.GetRalationalTables(_SelectedDatabase)
        For Each t In ralateTables
            strTB &= t.RelateTableValue & vbNewLine
        Next
        VBCodeEditor.Text = strTB






    End Sub
    Private Sub VBCodeEditor_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles VBCodeEditor.DragDrop
        VBCodeEditor.Text = ""
        If e.Data.GetDataPresent(GetType(TreeNodeTableDomainTables)) Then
            Dim ONode As TreeNodeTableDomainTables = CType(e.Data.GetData(GetType(TreeNodeTableDomainTables)), TreeNodeTableDomainTables)
            VBCodeEditor.Text = ONode.ListFunction.FunctionSelect & vbNewLine &
                ONode.ListFunction.FunctionInsert & vbNewLine &
                ONode.ListFunction.FunctionUpdate & vbNewLine &
                ONode.ListFunction.FunctionDelete & vbNewLine &
                ONode.ListFunction.FunctionSelectSubClass
        End If

        If e.Data.GetDataPresent(GetType(TreeNodeViewLinq)) Then
            Dim ONode As TreeNodeViewLinq = CType(e.Data.GetData(GetType(TreeNodeViewLinq)), TreeNodeViewLinq)
            'VBCodeEditor.Text = ONode.SP_Procedure.SP_TextHeader
            'VBCodeEditor.Text &= ONode.SP_Procedure.SP_TextBody
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeRiaClass)) Then
            Dim ONode As TreeNodeRiaClass = CType(e.Data.GetData(GetType(TreeNodeRiaClass)), TreeNodeRiaClass)
            VBCodeEditor.Text = ONode.Table.ComplexClass
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeFunctionInsertLinq)) Then
            Dim ONode As TreeNodeFunctionInsertLinq = CType(e.Data.GetData(GetType(TreeNodeFunctionInsertLinq)), TreeNodeFunctionInsertLinq)
            VBCodeEditor.Text = ONode.Table.FunctionInsert
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeFunctionUpdateLinq)) Then
            Dim ONode As TreeNodeFunctionUpdateLinq = CType(e.Data.GetData(GetType(TreeNodeFunctionUpdateLinq)), TreeNodeFunctionUpdateLinq)
            VBCodeEditor.Text = ONode.Table.FunctionUpdate
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeFunctionDeleteLinq)) Then
            Dim ONode As TreeNodeFunctionDeleteLinq = CType(e.Data.GetData(GetType(TreeNodeFunctionDeleteLinq)), TreeNodeFunctionDeleteLinq)
            VBCodeEditor.Text = ONode.Table.FunctionDelete
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeFunctionSelectLinq)) Then
            Dim ONode As TreeNodeFunctionSelectLinq = CType(e.Data.GetData(GetType(TreeNodeFunctionSelectLinq)), TreeNodeFunctionSelectLinq)
            VBCodeEditor.Text = ONode.Table.FunctionDelete
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeFunctionSelectSubClass)) Then
            Dim ONode As TreeNodeFunctionSelectSubClass = CType(e.Data.GetData(GetType(TreeNodeFunctionSelectSubClass)), TreeNodeFunctionSelectSubClass)
            VBCodeEditor.Text = ONode.Table.FunctionSelectSubClass
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeTableDomainClass)) Then
            Dim ONode As TreeNodeTableDomainClass = CType(e.Data.GetData(GetType(TreeNodeTableDomainClass)), TreeNodeTableDomainClass)
            VBCodeEditor.Text = ONode.DomainService.DomainServiceContext
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeRiaClassContent)) Then
            Dim ONode As TreeNodeRiaClassContent = CType(e.Data.GetData(GetType(TreeNodeRiaClassContent)), TreeNodeRiaClassContent)
            VBCodeEditor.Text = ONode.RiaContent.ClassContent
        End If
        If e.Data.GetDataPresent(GetType(TreeNodeTableLinq)) Then
            Dim ONode As TreeNodeTableLinq = CType(e.Data.GetData(GetType(TreeNodeTableLinq)), TreeNodeTableLinq)
            VBCodeEditor.Text = ONode.Table.TableClass & vbNewLine & ONode.Table.ClassManager

        End If
        If e.Data.GetDataPresent(GetType(TreeNodeWPFDetailsTable)) Then
            Dim ONode As TreeNodeWPFDetailsTable = CType(e.Data.GetData(GetType(TreeNodeWPFDetailsTable)), TreeNodeWPFDetailsTable)
            VBCodeEditor.Text = ONode.Table.WPFFileContent & vbNewLine & ONode.Table.VBFileContent

        End If
        If e.Data.GetDataPresent(GetType(TreeNodeWPFDatagridTable)) Then
            Dim ONode As TreeNodeWPFDatagridTable = CType(e.Data.GetData(GetType(TreeNodeWPFDatagridTable)), TreeNodeWPFDatagridTable)
            VBCodeEditor.Text = ONode.Table.WPFFileContent & vbNewLine & ONode.Table.VBFileContent

        End If
        'TreeNodeTableDomainClass
    End Sub

    Private Sub VBCodeEditor_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles VBCodeEditor.DragEnter
        If e.Data.GetDataPresent(GetType(TreeNodeTableLinq)) Or
            e.Data.GetDataPresent(GetType(TreeNodeViewLinq)) Or
            e.Data.GetDataPresent(GetType(TreeNodeRiaClass)) Or
            e.Data.GetDataPresent(GetType(TreeNodeFunctionInsertLinq)) Or
            e.Data.GetDataPresent(GetType(TreeNodeFunctionUpdateLinq)) Or
            e.Data.GetDataPresent(GetType(TreeNodeFunctionDeleteLinq)) Or
            e.Data.GetDataPresent(GetType(TreeNodeTableDomainTables)) Or
            e.Data.GetDataPresent(GetType(TreeNodeTableLinq)) Or
            e.Data.GetDataPresent(GetType(TreeNodeTableDomainClass)) Or
            e.Data.GetDataPresent(GetType(TreeNodeRiaClassContent)) Or
            e.Data.GetDataPresent(GetType(TreeNodeFunctionSelectSubClass)) Or
            e.Data.GetDataPresent(GetType(TreeNodeWPFDatagridTable)) Or
            e.Data.GetDataPresent(GetType(TreeNodeWPFDetailsTable)) Or
            e.Data.GetDataPresent(GetType(TreeNodeFunctionSelectLinq)) Then
            e.Effect = DragDropEffects.Copy
            '
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub LinqDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinqDisplayToolStripMenuItem.Click
        If LinqDB Is Nothing Then
            Exit Sub
        End If

        Dim tmpDataContext As String = Application.StartupPath & "\Templates\DataContextTemplate.txt"
        Dim contextTemplate As String = TemplateManager.OpenTemplates(tmpDataContext)
        Dim sb As New StringBuilder(contextTemplate)
        sb.Replace("[DATABASENAME]", _SelectedDatabase.DatabaseName)
        sb.Replace("[Extensibility_Method_Definitions]", LinqHelperClass.Extensibility_Method_Definitions(LinqDB))
        sb.Replace("[DataContextPropertyTables]", LinqHelperClass.DataContextPropertyTables(LinqDB))
        sb.AppendLine()
        sb.AppendLine()
        Dim sbTable As New StringBuilder()
        Dim tmpLinqTableTemplate As String = Application.StartupPath & "\Templates\LinqTableTemplate.txt"
        Dim LinqTableTemplate As String = TemplateManager.OpenTemplates(tmpLinqTableTemplate)

        For Each lt In LinqDB.ListLinqTable
            sbTable.AppendLine(LinqTableTemplate)
            sbTable.Replace("[TABLENAME]", lt.TableName)
            sbTable.Replace("[TABLEVALUE]", lt.TableValue)
            sbTable.Replace("[WRITE_LINQ_VAR]", LinqHelperClass.WriteLinqVar(lt, LinqDB))
            sbTable.AppendLine()
            sbTable.Replace("[COLUMNS_CHANGE_Extensibility_Method_Definitions]", LinqHelperClass.Extensibility_Columns_Method_Definitions(lt))
            sbTable.AppendLine()
            sbTable.Replace("[INITIALIZE_LINQ_ENTITY]", LinqHelperClass.WriteLinqInitialize(lt, LinqDB))
            sbTable.Replace("[LINQ_PROPERTIES]", LinqHelperClass.WriteLinqDataColumnsProperties(lt, LinqDB))
            sbTable.Replace("[LINQ_ATTACH_AND_DETACH_ENTITYSETPROPERTIES]", LinqHelperClass.Linq_Attach_and_Detach_Properties(lt, LinqDB))
        Next
        sb.AppendLine(sbTable.ToString)
        VBCodeEditor.Text = sb.ToString
    End Sub

    Private Sub LinqDataManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinqDataManagerToolStripMenuItem.Click
        If LinqDB Is Nothing Then
            Exit Sub
        End If
        Dim sb As New StringBuilder()
        sb.AppendLine(HelperPartialClass.CreateMainPartialClass(LinqDB))
        VBCodeEditor.Text = sb.ToString
    End Sub

    Private Sub MenuListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuListToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim sb As New StringBuilder()
        sb.AppendLine(HelpClass.GetListDBMenus(_SelectedDatabase))
        VBCodeEditor.Text = sb.ToString
    End Sub

    Private Sub PageListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageListToolStripMenuItem.Click
        If LinqDB Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)



        Dim tmpDataContext As String = Application.StartupPath & "\Templates\SilverlightPagelist.txt"
        Dim contextTemplate As String = TemplateManager.OpenTemplates(tmpDataContext)
        Dim sb As New StringBuilder(contextTemplate)

        sb.Replace("[APPLICATIONNAME]", LinqDB.ProjectName)
        sb.Replace("[TABLENAME]", objTable.TableValue)
        sb.Replace("[DataGrid.Columns]", ManageSilverlight.CreateDatagridColumns(objTable))
        sb.AppendLine()
        sb.AppendLine()

        VBCodeEditor.Text = sb.ToString



    End Sub

    Private Sub RunWPFAppsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunWPFAppsToolStripMenuItem.Click
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Try
            'Dim frmwpf As New WPFWindFormDatagrid(objTable)
            'frmwpf.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub CreateWPFFomsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateWPFFomsToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        _SelectedDatabase = CType(TSCmbDatabase.ComboBox.SelectedItem, DatabaseNameInfo)
        Dim Frmwpfdesiner As New FormWPFDesigner(_SelectedDatabase)
        If Frmwpfdesiner.ShowDialog = DialogResult.OK Then
            'Dim wpfcnt = ma
            _SelectedDatabase.WPFDatabaseContext = Frmwpfdesiner.WPFDatabaseContext
            'Dim domsrv = ManageRiaClass.CreateDomainServicesClass(_SelectedDatabase, LinqDB)
            RaiseEvent RefresValues()
        End If
    End Sub
    Private Sub TSCmbDatabase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSCmbDatabase.SelectedIndexChanged
        _SelectedDatabase = CType(TSCmbDatabase.ComboBox.SelectedItem, DatabaseNameInfo)
        If Not _SelectedDatabase Is Nothing Then
            TSCmbTables.ComboBox.DataSource = _SelectedDatabase.ListTable
            ' loadIconselected()
        End If
    End Sub

    Private Sub AddDomainServicesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDomainServicesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        If LinqDB Is Nothing Then
            Exit Sub
        End If
        Dim domsrv = ManageRiaClass.CreateDomainServicesClass(_SelectedDatabase, LinqDB)
        If _SelectedDatabase.ListDomainservices.Count > 0 Then
            _SelectedDatabase.ListDomainservices.RemoveRange(1, 1)
            _SelectedDatabase.ListDomainservices.Add(domsrv)
        Else
            _SelectedDatabase.ListDomainservices.Add(domsrv)
        End If
        RaiseEvent RefresRia()
    End Sub

    Private Sub TSBEditDomainServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TSBSaveDomainServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CreateRiaCollectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateRiaCollectionToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        If LinqDB Is Nothing Then
            Exit Sub
        End If
        Dim Riacnt = ManageRiaClass.CreateRiaContext(_SelectedDatabase, LinqDB)
        If _SelectedDatabase.ListRiaContext.Count > 0 Then
            _SelectedDatabase.ListRiaContext.RemoveRange(1, 1)
            _SelectedDatabase.ListRiaContext.Add(Riacnt)
        Else
            _SelectedDatabase.ListRiaContext.Add(Riacnt)
        End If
        RaiseEvent RefresRia()
    End Sub

    Private Sub LinqSaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinqSaveToolStripMenuItem.Click
        If LinqDB Is Nothing Then
            Exit Sub
        End If
        Try
            Dim tmpDataContext As String = Application.StartupPath & "\Templates\DataContextTemplate.txt"
            Dim contextTemplate As String = TemplateManager.OpenTemplates(tmpDataContext)
            Dim sb As New StringBuilder(contextTemplate)
            sb.Replace("[DATABASENAME]", _SelectedDatabase.DatabaseName)
            sb.Replace("[Extensibility_Method_Definitions]", LinqHelperClass.Extensibility_Method_Definitions(LinqDB))
            sb.Replace("[DataContextPropertyTables]", LinqHelperClass.DataContextPropertyTables(LinqDB))
            sb.AppendLine()
            sb.AppendLine()
            Dim sbTable As New StringBuilder()
            Dim tmpLinqTableTemplate As String = Application.StartupPath & "\Templates\LinqTableTemplate.txt"
            Dim LinqTableTemplate As String = TemplateManager.OpenTemplates(tmpLinqTableTemplate)

            For Each lt In LinqDB.ListLinqTable
                sbTable.AppendLine(LinqTableTemplate)
                sbTable.Replace("[TABLENAME]", lt.TableName)
                sbTable.Replace("[TABLEVALUE]", lt.TableValue)
                'TABLEVALUE
                sbTable.Replace("[WRITE_LINQ_VAR]", LinqHelperClass.WriteLinqVar(lt, LinqDB))
                sbTable.AppendLine()
                sbTable.Replace("[COLUMNS_CHANGE_Extensibility_Method_Definitions]", LinqHelperClass.Extensibility_Columns_Method_Definitions(lt))
                sbTable.AppendLine()
                sbTable.Replace("[INITIALIZE_LINQ_ENTITY]", LinqHelperClass.WriteLinqInitialize(lt, LinqDB))
                sbTable.Replace("[LINQ_PROPERTIES]", LinqHelperClass.WriteLinqDataColumnsProperties(lt, LinqDB))
                sbTable.Replace("[LINQ_ATTACH_AND_DETACH_ENTITYSETPROPERTIES]", LinqHelperClass.Linq_Attach_and_Detach_Properties(lt, LinqDB))
            Next
            sb.AppendLine(sbTable.ToString)

            SaveFileDialog1.Filter = "Visul basic vbFile|*.vb"
            SaveFileDialog1.InitialDirectory = LinqDB.SaveLocation
            'SaveFileDialog1.FileName = LinqDB.LinqDatabaseName & ".vb"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sb.ToString, True)
            End If
            MessageBox.Show("File Saved")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinqEditCollectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinqEditCollectionToolStripMenuItem.Click
        If Not LinqDB Is Nothing Then
            Dim FrmLinqDesigner As New FormLinqDesigner(_SelectedDatabase, LinqDB)
            If FrmLinqDesigner.ShowDialog() = DialogResult.OK Then
                LinqDB = FrmLinqDesigner.LinqDatabaseName
                If _SelectedDatabase.ListLinqDatabase.Count > 0 Then
                    _SelectedDatabase.ListLinqDatabase.Clear()
                    _SelectedDatabase.ListLinqDatabase.Add(LinqDB)
                Else
                    _SelectedDatabase.ListLinqDatabase.Add(LinqDB)
                End If
                RaiseEvent RefresValues()
            End If

        End If
    End Sub

    Private Sub CreateLinqToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateLinqToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim FrmLinqDesigner As New FormLinqDesigner(_SelectedDatabase)
        If FrmLinqDesigner.ShowDialog() = DialogResult.OK Then
            LinqDB = FrmLinqDesigner.LinqDatabaseName

            If _SelectedDatabase.ListLinqDatabase.Count > 0 Then
                _SelectedDatabase.ListLinqDatabase.RemoveRange(1, 1)
                _SelectedDatabase.ListLinqDatabase.Add(LinqDB)
                If FrmLinqDesigner.CBCreateDomainservices.Checked Then
                    Dim domsrv = ManageRiaClass.CreateDomainServicesClass(_SelectedDatabase, LinqDB)
                    If _SelectedDatabase.ListDomainservices.Count > 0 Then
                        _SelectedDatabase.ListDomainservices.RemoveRange(1, 1)
                        _SelectedDatabase.ListDomainservices.Add(domsrv)
                    End If
                End If
                If FrmLinqDesigner.CBCreateRiaClass.Checked Then
                    Dim Riacnt = ManageRiaClass.CreateRiaContext(_SelectedDatabase, LinqDB)
                    If _SelectedDatabase.ListRiaContext.Count > 0 Then
                        _SelectedDatabase.ListRiaContext.RemoveRange(1, 1)
                        _SelectedDatabase.ListRiaContext.Add(Riacnt)
                    End If
                End If

            Else
                _SelectedDatabase.ListLinqDatabase.Add(LinqDB)
                If FrmLinqDesigner.CBCreateDomainservices.Checked Then
                    Dim domsrv = ManageRiaClass.CreateDomainServicesClass(_SelectedDatabase, LinqDB)
                    _SelectedDatabase.ListDomainservices.Add(domsrv)
                End If
                If FrmLinqDesigner.CBCreateRiaClass.Checked Then
                    Dim Riacnt = ManageRiaClass.CreateRiaContext(_SelectedDatabase, LinqDB)
                    _SelectedDatabase.ListRiaContext.Add(Riacnt)
                End If
            End If
            HelperLinqTable.CreateLinqDatacontextServicesClass(LinqDB)
            RaiseEvent RefresValues()
        End If
    End Sub
    Private Sub TSBSetAppsCombobox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBSetAppsCombobox.Click
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim frmcolumnCombo As New FormComboBoxTask(_SelectedDatabase, objTable)
        frmcolumnCombo.ShowDialog()
        _SelectedDatabase = frmcolumnCombo.Database
        RaiseEvent RefresRia()
    End Sub
    Private Sub ASPFormViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ASPFormViewToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageASP.CreateFormViewEdit(objTable)
    End Sub
    Private Sub ASPClassObjectDataSourceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ASPClassObjectDataSourceToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageASP.CreateClassObjectDataSource(objTable, _SelectedDatabase.DatabaseName)
    End Sub
    Private Sub ClassInfoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClassInfoToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.CreateVBClass2010(objTable)
    End Sub

    Private Sub ClassInfoSelectToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClassInfoSelectToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.InfoSelect(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub ClassInfoUpdateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClassInfoUpdateToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.InfoUpdate(objTable, _SelectedDatabase.DatabaseName)

    End Sub

    Private Sub ClassInfoDeleteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClassInfoDeleteToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.InfoDelete(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub ClassInfoInsertToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClassInfoInsertToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.InfoInsert(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub ASPGridViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ASPGridViewToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = AspClassHelper.CreateDBFromTemplate(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub ASPGridInsertToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ASPGridInsertToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = AspClassHelper.CreateTemplateTableCreate(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub CreateDataContextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateDataContextToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        VBCodeEditor.Text = LinqHelperVB.VB_DataContext(_SelectedDatabase)   '  AspClassHelper.CreateTemplateTableCreate(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub CreateLinqTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateLinqTablesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = LinqHelperVB.VB_Tables(objTable, _SelectedDatabase, False)
    End Sub

    Private Sub CreateLinqRalatedTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateLinqRalatedTablesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = LinqHelperVB.VB_Tables(objTable, _SelectedDatabase, True)
    End Sub

    Private Sub LinqContextDeleteInsertUpdateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LinqContextDeleteInsertUpdateToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'VBCodeEditor.Text = HelperPartialClass.CreatePartialClass(_SelectedDatabase.DatabaseName, objTable)
        Dim frmResults As New FormResultsDisplay
        frmResults.TextBoxResults.Text = HelperPartialClass.CreatePartialClass(_SelectedDatabase.DatabaseName, objTable)
        frmResults.Show()
    End Sub
    Public Sub CreateViewLinq(vw As ViewNameInfo)
        Dim objTable As TableNameInfo = TableNameInfo.CreateTableFromView(vw)
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        VBCodeEditor.Text = LinqHelperVB.VB_Tables(objTable, _SelectedDatabase, False)

    End Sub

    Private Sub VBClass2010FullCollectionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles VBClass2010FullCollectionToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'objTable.Database = _SelectedDatabase

        Dim frmTable As New FormSelectTables(_SelectedDatabase)
        Dim cols As List(Of ColumnsInfo) = Nothing
        Dim isOK = frmTable.ShowDialog()
        Dim selectedTable As TableNameInfo = Nothing

        If isOK = DialogResult.OK Then
            selectedTable = frmTable.SelectedTable

            cols = frmTable.listSelectedColumns
            frmTable.Close()
        Else
            frmTable.Close()
            Exit Sub
        End If
        VBCodeEditor.Text = VBClassManager.VBClass2010CollectionSample(selectedTable, cols)
    End Sub

    Private Sub PhoneLocalDBTableToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PhoneLocalDBTableToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = PhoneDatabase.VBClassPhoneTable(objTable)
    End Sub

    Private Sub VBClass2010SqliteCollectionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles VBClass2010SqliteCollectionToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = HelpClass.GetFullSqliteListCollection(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub ReadFromFormToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReadFromFormToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.WriteToTextBox(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub WriteToTextBoxToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles WriteToTextBoxToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageInfoClass.ReadFromTextBox(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub EntityFrameworkClassToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntityFrameworkClassToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = EntityFramework.CreateVBClass2010(objTable, _SelectedDatabase)
    End Sub

    Private Sub EntityFrameworkConnectionStringToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntityFrameworkConnectionStringToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        VBCodeEditor.Text = EntityFramework.CreateConnectionString(_SelectedDatabase)
    End Sub

    Private Sub EntityFrameworkCreateTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntityFrameworkCreateTablesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If

        Dim frmTable As New FormListTablesSelect(_SelectedDatabase)
        Dim listtables As List(Of TableNameInfo) = Nothing
        Dim isOK = frmTable.ShowDialog()

        If isOK = DialogResult.OK Then
            listtables = frmTable.listSelectedTables
            frmTable.Close()
        Else
            frmTable.Close()
            Exit Sub
        End If
        'VBCodeEditor.Text = CSEntityClass.CreateVBDbContext(_SelectedDatabase, listtables)
        VBCodeEditor.Text = EntityFramework.CreateVBDbContext(_SelectedDatabase, listtables)
    End Sub
    Public Sub EntityFrameworkClass(objTable As TableNameInfo, db As DatabaseNameInfo)
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        VBCodeEditor.Text = EntityFramework.CreateVBClass2010(objTable, db)
    End Sub
    Public Sub EntityFrameworkConnectionString(db As DatabaseNameInfo)
        VBCodeEditor.Text = EntityFramework.CreateConnectionString(db)
    End Sub
    Public Sub EntityFrameworkDatabaseContext(db As DatabaseNameInfo)
        ' VBCodeEditor.Text = EntityFramework.CreateVBDbContext(db, listtables)
    End Sub

    Private Sub DataGridToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DataGridToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageSilverlight.CreateWPFDatagridColumns(objTable)

    End Sub

    Private Sub MVCJqueryTemplateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCJqueryTemplateToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = MVCJqueryHelper.TableListTemplate(objTable)
    End Sub

    Private Sub MVCTableDetailsTemplateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCTableDetailsTemplateToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = MVCJqueryHelper.TableDetailsTemplate(objTable)
    End Sub

    Private Sub MVCMVCControllerTemplateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCMVCControllerTemplateToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = MVCJqueryHelper.MVCControllerTemplate(objTable, _SelectedDatabase.DatabaseName)
    End Sub

    Private Sub MVCJQueryTemplateToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles MVCJQueryTemplateToolStripMenuItem1.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = MVCJqueryHelper.JQueryTemplate(objTable)
    End Sub

    Private Sub CreateScaffoldControllerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateScaffoldControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        VBCodeEditor.Text = EntityFramework.CreateScaffoldControllerContext(_SelectedDatabase)
    End Sub

    Private Sub MVCVMClassToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCVMClassToolStripMenuItem.Click
        'CreateVMClass
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = MVCJqueryHelper.CreateVMClass(objTable)
    End Sub

    Private Sub TestTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestTablesToolStripMenuItem.Click
        'CreateVMClass
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        Dim DGTemplate As String = "\Templates\ASP\Maintemplate.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)


        Dim _MAINPAGELIST As String = JSManager.GetHtmlTableList(objTable)
        SB.Replace("[TABLE]", objTable.TableValue)
        SB.Replace("[MAINPAGELIST]", _MAINPAGELIST)
        'SB.Replace("[ColumnID]", objTable.GetPrimaryKey.ColumnName)
        sr.Close()
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub TestExternalTableToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestExternalTableToolStripMenuItem.Click
        'CreateVMClass
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = JSManager.TableListTemplate(objTable)
    End Sub

    Private Sub TestExternalEditToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestExternalEditToolStripMenuItem.Click
        'CreateVMClass
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = JSManager.TableAddOrEditTemplate(objTable)
    End Sub

    Private Sub TestJSSaveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestJSSaveToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim Results = JSManager.JQueryTemplate(objTable) & vbNewLine & JSManager.JQuerySaveTemplate(objTable)
        VBCodeEditor.Text = Results
    End Sub

    Private Sub TestExternalDetailsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestExternalDetailsToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = JSManager.TableDetailsTemplate(objTable)
    End Sub

    Private Sub TestDeleteTableToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TestDeleteTableToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim Results = JSManager.TableDeleteTemplate(objTable)
        VBCodeEditor.Text = Results
    End Sub

    Private Sub JavaScriptForTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles JavaScriptForTablesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim Results = JavaScriptTable.JQueryTableTemplate(objTable)
        VBCodeEditor.Text = Results
    End Sub

    Private Sub CallWebservicesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CallWebservicesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim Results = webservices_asms.MVCControllerTemplate(objTable, _SelectedDatabase.DatabaseName)
        VBCodeEditor.Text = Results
    End Sub

    Private Sub SaveTableToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveTableToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim frmsave As New FormSaveLocation(objTable)
        frmsave.ShowDialog()
    End Sub

    Private Sub MVCFormEditOrInsertToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCFormEditOrInsertToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim Results = MVCControls.MVCTableEditTemplate(objTable)
        VBCodeEditor.Text = Results
    End Sub

    Private Sub EditMVCToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EditMVCToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim ResultsColumnsNames = MVCControls.MVCTableListNameTemplate(objTable)
        Dim ResultsColumnsValues = MVCControls.MVCTableListValuesTemplate(objTable)


        Dim DGTemplate As String = "\Templates\MVCAJAXListPage.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)

        SB.Replace("[TABLE-VALUE]", objTable.TableValue)
        SB.Replace("[PRIMARYKEY]", objTable.GetPrimaryKey.ColumnName)
        SB.Replace("[LISTTABLECOLUMNSNAMES]", ResultsColumnsNames)
        SB.Replace("[LISTTABLECOLUMNSVALUES]", ResultsColumnsValues)

        sr.Close()
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub MVCAJAXLIstToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCAJAXLIstToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim ResultsColumnsNames = MVCControls.MVCTableListNameTemplate(objTable)
        Dim ResultsColumnsValues = MVCControls.MVCTableListValuesTemplate(objTable)


        Dim DGTemplate As String = "\Templates\MVCAJAXListPage.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)

        SB.Replace("[TABLE-VALUE]", objTable.TableValue)
        SB.Replace("[PRIMARYKEY]", objTable.GetPrimaryKey.ColumnName)
        SB.Replace("[LISTTABLECOLUMNSNAMES]", ResultsColumnsNames)
        SB.Replace("[LISTTABLECOLUMNSVALUES]", ResultsColumnsValues)

        sr.Close()
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub MVCAjaxEditToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCAjaxEditToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim ResultsColumnsNames = MVCControls.MVCTableListNameTemplate(objTable)
        Dim ResultsColumnsValues = MVCControls.MVCTableListValuesTemplate(objTable)


        Dim DGTemplate As String = "\Templates\MVCAJAXListPage.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)

        SB.Replace("[TABLE-VALUE]", objTable.TableValue)
        SB.Replace("[PRIMARYKEY]", objTable.GetPrimaryKey.ColumnName)
        SB.Replace("[LISTTABLECOLUMNSNAMES]", ResultsColumnsNames)
        SB.Replace("[LISTTABLECOLUMNSVALUES]", ResultsColumnsValues)

        sr.Close()
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub MVCAjaxControllerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MVCAjaxControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'Dim ResultsColumnsNames = MVCControls.MVCTableListNameTemplate(objTable)
        'Dim ResultsColumnsValues = MVCControls.MVCTableListValuesTemplate(objTable)


        Dim DGTemplate As String = "\Templates\MVCAJAXController.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)

        SB.Replace("[TABLE-VALUE]", objTable.TableValue)
        SB.Replace("[PRIMARYKEY]", objTable.GetPrimaryKey.ColumnName)
        'SB.Replace("[LISTTABLECOLUMNSNAMES]", ResultsColumnsNames)
        'SB.Replace("[LISTTABLECOLUMNSVALUES]", ResultsColumnsValues)

        sr.Close()
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub MappingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MappingToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = EntityFramework.CreateEntityFreworkMaping(objTable, _SelectedDatabase)
    End Sub

    Private Sub XAMLGridToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles XAMLGridToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = ManageSilverlight.CreateFrmgrid(objTable)
    End Sub

    Private Sub JSModelToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles JSModelToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.JavaScriptManager.CreateFormViewEdit(objTable)
    End Sub

    Private Sub AngularTableListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AngularTableListToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.angularJS.GetHtmlTableList(objTable)
    End Sub

    Private Sub AngularControllerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AngularControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.angularJS.GetTableController(objTable)
    End Sub

    Private Sub AngularControllerByIDToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AngularControllerByIDToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.angularJS.GetTableByIDController(objTable)
    End Sub

    Private Sub AngularServerDBToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AngularServerDBToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.angularJS.GetTableServer(objTable)
    End Sub

    Private Sub VBClass2010FullJsonToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles VBClass2010FullJsonToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase

        Dim frmTable As New FormSelectTables(_SelectedDatabase)
        Dim cols As List(Of ColumnsInfo) = Nothing
        Dim isOK = frmTable.ShowDialog()

        If isOK = DialogResult.OK Then
            objTable = frmTable.SelectedTable
            cols = frmTable.listSelectedColumns
            frmTable.Close()
        Else
            frmTable.Close()
            Exit Sub
        End If
        Dim DisplayTable As New TableNameInfo
        DisplayTable.Database = _SelectedDatabase
        DisplayTable.StrConnection = objTable.StrConnection
        DisplayTable.SchemaTable = objTable.SchemaTable
        DisplayTable.TableName = objTable.TableName
        For Each c In cols
            DisplayTable.AddColumn(c)
        Next
        ' VBCodeEditor.Text = VBClassManager.VBClass2010JsonSample(objTable, cols)
        Dim frmResults As New FormResultsDisplay
        frmResults.TextBoxResults.Text = HelpClass.GetXMLToJSON(DisplayTable, cols)
        frmResults.Show()
    End Sub

    Private Sub AngularFormToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AngularFormToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.angularJS.GetHtmlFormNew(objTable)
    End Sub

    Private Sub KnockoutTableListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KnockoutTableListToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = DBCodeGenerator.knockout.GetHtmlTableList(objTable)
    End Sub

    Private Sub ListBoxToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ListBoxToolStripMenuItem.Click

    End Sub

    Private Sub ClassWithTablesNameToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClassWithTablesNameToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = VBClassManager.CreateVBClassWithTable2010_INotifyPropertyChanged(objTable)
    End Sub

    Private Sub LinkSelecteveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LinkSelecteveToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If

        Dim listtables As List(Of TableNameInfo) = Nothing
        Dim projectName As String = ""
        Dim SavePath As String = ""
        Dim frmEntitySave As New FormGeneRateCsharpEntityframeworkFiles(_SelectedDatabase)
        Dim isOK = frmEntitySave.ShowDialog()

        If isOK = DialogResult.OK Then
            SavePath = Application.StartupPath & "\LinqClasses\" & _SelectedDatabase.DatabaseName & "DataContext.vb"
            projectName = frmEntitySave.ProjectName
            listtables = frmEntitySave.listSelectedTables
            frmEntitySave.Close()
        Else
            frmEntitySave.Close()
            Exit Sub
        End If
        Dim db As New DatabaseNameInfo
        db.ConnectionString = _SelectedDatabase.ConnectionString
        db.DatabaseName = _SelectedDatabase.DatabaseName
        For Each tblist In listtables
            db.ListTable.Add(tblist)
        Next
        Dim StrResults = LinkVBClassHelper.VB_DataContext(db) & vbNewLine
        For Each t In db.ListTable
            StrResults &= LinkVBClassHelper.VB_Tables(t, db, True) & vbNewLine
        Next
        'VBCodeEditor.Text = StrResults
        ' " & db.DatabaseName & "DataContext"
        writeToContext(SavePath, StrResults)

        MessageBox.Show("Entity Created Location " & SavePath)

    End Sub
    Private Sub writeToContext(SavePath As String, Content As String)
        My.Computer.FileSystem.WriteAllText(SavePath, Content, False)

    End Sub
   
    Private Sub EntiyMappingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntiyMappingToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = EntityFramework.CreateEntityFreworkMaping(objTable, _SelectedDatabase)
    End Sub

    Private Sub EntitySeedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EntitySeedToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        writeToSeed(_SelectedDatabase, objTable)

        ' VBCodeEditor.Text = EntityFramework.CreateCustomDatabaseInitializer(_SelectedDatabase, objTable)
    End Sub

    Private Sub SaveEntityFilesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveEntityFilesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If

        Dim listtables As List(Of TableNameInfo) = Nothing
        Dim projectName As String = ""
        Dim SavePath As String = ""
        Dim frmEntitySave As New FormGeneRateCsharpEntityframeworkFiles(_SelectedDatabase)
        frmEntitySave.PathLocation = Application.StartupPath & "\EntityFrameworkClasses\VB\"
        Dim isOK = frmEntitySave.ShowDialog()

        If isOK = DialogResult.OK Then
            SavePath = frmEntitySave.PathLocation
            projectName = frmEntitySave.ProjectName
            listtables = frmEntitySave.listSelectedTables
            frmEntitySave.Close()
        Else
            frmEntitySave.Close()
            Exit Sub
        End If
        Dim db As New DatabaseNameInfo
        db.ConnectionString = _SelectedDatabase.ConnectionString
        db.DatabaseName = _SelectedDatabase.DatabaseName
        Dim listDependency = listtables
        For Each tblist In listtables
            db.ListTable.Add(tblist)
        Next

        writeToContext(db, SavePath, listtables)
        writeToClassAndConfig(db, SavePath, listtables)
        ' writeToSeed(db, SavePath, listtables)
        MessageBox.Show("Entity Created")
    End Sub
    Private Sub writeToContext(db As DatabaseNameInfo, SavePath As String, listtables As List(Of TableNameInfo))
        Dim ResultsDataContext = EntityFramework.CreateVBDbContext(db, listtables)
        Const TemplateEntityClass As String = "\Templates\VB\Entity\VBEntityDataContext.txt"
        Dim csECFS As New FileStream(Application.StartupPath & TemplateEntityClass, FileMode.Open)
        Dim StrRead As String = ""
        Using sr As New StreamReader(csECFS)
            StrRead = sr.ReadToEnd
        End Using
        Dim SB As New StringBuilder(StrRead)
        SB.Replace("[DataContext]", ResultsDataContext)
        Dim contextFileName = SavePath & "\" & _SelectedDatabase.DatabaseName & "Context.vb"
        My.Computer.FileSystem.WriteAllText(contextFileName, SB.ToString, False)
    End Sub

    Private Sub writeToClassAndConfig(db As DatabaseNameInfo, SavePath As String, listtables As List(Of TableNameInfo))

        Const TemplateEntityClass As String = "\Templates\VB\Entity\VBEntityClass.txt"
        Const TemplateEntityConfig As String = "\Templates\VB\Entity\VBEntityConfiguration.txt"
        Dim fsClass As New FileStream(Application.StartupPath & TemplateEntityClass, FileMode.Open)
        Dim srClassResults As String = ""
        Dim srConfigResults As String = ""
        Using srclass As New StreamReader(fsClass)
            srClassResults = srclass.ReadToEnd
        End Using
        fsClass.Close()

        Dim fsConfig As New FileStream(Application.StartupPath & TemplateEntityConfig, FileMode.Open)
        Using srConfig As New StreamReader(fsConfig)
            srConfigResults = srConfig.ReadToEnd
        End Using
        fsConfig.Close()
        For Each t In listtables
            Dim Resultsclass = EntityFramework.CreateVBClass2010(t, db)
            Dim sbClass As New StringBuilder(srClassResults)
            sbClass.Replace("[CLASSENTITY]", Resultsclass)
            Dim contextFileName = SavePath & "\" & t.TableValue & ".vb"
            My.Computer.FileSystem.WriteAllText(contextFileName, sbClass.ToString, False)
        Next
        Dim NewPath As String = ""

        If My.Computer.FileSystem.DirectoryExists(SavePath & "\Mapping") Then
            NewPath = SavePath & "\Mapping"
        Else
            My.Computer.FileSystem.CreateDirectory(SavePath & "\Mapping")
            NewPath = SavePath & "\Mapping"
        End If
        For Each t In listtables
            Dim ResultsConfig = EntityFramework.CreateEntityFreworkMaping(t, db)
            Dim sbConfig As New StringBuilder(srConfigResults)
            sbConfig.Replace("[ConfigurationName]", ResultsConfig)
            Dim contextFileName = NewPath & "\" & t.TableValue & "Map.vb"
            My.Computer.FileSystem.WriteAllText(contextFileName, sbConfig.ToString, False)
        Next



    End Sub

    Private Sub writeToSeed(db As DatabaseNameInfo, dt As TableNameInfo)

        Const TemplateEntityClass As String = "\Templates\VB\Entity\CustomDatabaseInitializer.txt"
        Dim csECFS As New FileStream(Application.StartupPath & TemplateEntityClass, FileMode.Open)
        Dim StrRead As String = ""
        Using sr As New StreamReader(csECFS)
            StrRead = sr.ReadToEnd
        End Using
        Dim SB As New StringBuilder(StrRead)
        SB.Replace("[DATABASE]", db.DatabaseName)


        Dim SeedList = EntityFramework.SeedInitializer(db, dt)

        SB.Replace("[SEEDCONTECT]", SeedList)
        '
        Dim contextFileName = Application.StartupPath & "\EntityFrameworkClasses\VB\Mapping\CustomDatabaseInitializer.vb"
        My.Computer.FileSystem.WriteAllText(contextFileName, SB.ToString, False)
    End Sub
    Dim newOrderListTables As New List(Of TableNameInfo)

    Private Function SeedTableslist(db As DatabaseNameInfo) As List(Of TableNameInfo)
        Dim resultNewSeedTable As New List(Of TableNameInfo)
        Dim listdep As New List(Of ManageTableDependency)

        Dim MastResults = From tb In db.ListTable Where tb.TablesAsMasterTable(db) = False
        Dim DetailsResults = From tb In db.ListTable Where tb.TablesAsMasterTable(db) = True
        For Each mt In MastResults
            resultNewSeedTable.Add(mt)
            listdep.Add(New ManageTableDependency With {.TableName = mt.TableName, .DependOn = "Master"})

        Next
        For Each dt In DetailsResults
            Dim mastTable = dt.GetMasterTables(db)
            For Each rmt In mastTable
                If IsSeedTableOnTheList(rmt.RelateTableName, resultNewSeedTable) Then
                    resultNewSeedTable.Add(dt)
                    listdep.Add(New ManageTableDependency With {.TableName = dt.TableName, .DependOn = rmt.RelateTableName})
                Else
                    listdep.Add(New ManageTableDependency With {.TableName = dt.TableName, .DependOn = rmt.RelateTableName})
                End If
            Next

        Next

        Return newOrderListTables


        'For Each st In db.ListTable
        '    Dim results = st.GetMasterTables(db)

        '    If results.Count = 0 Then

        '        Dim isInList = newOrderListTables.Where(Function(t) t.TableValue = st.TableValue).SingleOrDefault
        '        If isInList Is Nothing Then
        '            newOrderListTables.Add(st)
        '        End If

        '    End If

        'Next

    End Function
    Private Function IsSeedTableOnTheList(_tableValue As String, _ListTables As List(Of TableNameInfo)) As Boolean
        Dim results = _ListTables.Where(Function(st) st.TableValue = _tableValue).SingleOrDefault
        If results IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ToolStripMenuItemLinqSelecteveDisplat_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItemLinqSelecteveDisplat.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If

        Dim listtables As List(Of TableNameInfo) = Nothing
        Dim projectName As String = ""
        Dim SavePath As String = ""
        Dim frmEntitySave As New FormGeneRateCsharpEntityframeworkFiles(_SelectedDatabase)
        Dim isOK = frmEntitySave.ShowDialog()

        If isOK = DialogResult.OK Then
            SavePath = Application.StartupPath & "\LinqClasses\" & _SelectedDatabase.DatabaseName & "DataContext.vb"
            projectName = frmEntitySave.ProjectName
            listtables = frmEntitySave.listSelectedTables
            frmEntitySave.Close()
        Else
            frmEntitySave.Close()
            Exit Sub
        End If
        Dim db As New DatabaseNameInfo
        db.ConnectionString = _SelectedDatabase.ConnectionString
        db.DatabaseName = _SelectedDatabase.DatabaseName
        For Each tblist In listtables
            db.ListTable.Add(tblist)
        Next
        Dim StrResults = LinkVBClassHelper.VB_DataContext(db) & vbNewLine
        For Each t In db.ListTable
            StrResults &= LinkVBClassHelper.VB_Tables(t, db, True) & vbNewLine
        Next

        Dim frmResults As New FormResultsDisplay
        frmResults.TextBoxResults.Text = StrResults
        frmResults.Show()

    End Sub

    Private Sub KnockoutFormsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KnockoutFormsToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = knockout.GetHtmlForm(objTable)
    End Sub

    Private Sub KnockoutMasterDetailsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KnockoutMasterDetailsToolStripMenuItem.Click
        Dim contextFileName = Application.StartupPath & "\Templates\Knockout\MasterDetailsTemplate.txt"
        Dim fsClass As New FileStream(contextFileName, FileMode.Open)
        Dim srKockoutMasterDetails As String = ""
        Using srclass As New StreamReader(fsClass)
            srKockoutMasterDetails = srclass.ReadToEnd
        End Using
        fsClass.Close()

        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim tblist = knockout.GetHtmlTableList(objTable)
        Dim frmData = knockout.GetHtmlForm(objTable)
        Dim SB As New StringBuilder(srKockoutMasterDetails)
        SB.Replace("[$MASTERTABLE]", tblist)
        SB.Replace("[$DETAILSTABLE]", frmData)
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub KnockoutClassToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KnockoutClassToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = knockout.GetKoClass(objTable)
    End Sub

    Private Sub VBClassJsonViewsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles VBClassJsonViewsToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase

        Dim frmTable As New FormSelectViews(_SelectedDatabase)
        Dim cols As List(Of ColumnsInfo) = Nothing
        Dim isOK = frmTable.ShowDialog()

        If isOK = DialogResult.OK Then
            Dim viewToTable = frmTable.SelectedViews
            objTable = TableNameInfo.CreateTableFromView(viewToTable)
            cols = frmTable.listSelectedColumns
            frmTable.Close()
        Else
            frmTable.Close()
            Exit Sub
        End If
        Dim DisplayTable As New TableNameInfo
        DisplayTable.Database = _SelectedDatabase
        DisplayTable.StrConnection = _SelectedDatabase.ConnectionString
        DisplayTable.SchemaTable = objTable.SchemaTable
        DisplayTable.TableName = objTable.TableName
        For Each c In cols
            DisplayTable.AddColumn(c)
        Next
        ' VBCodeEditor.Text = VBClassManager.VBClass2010JsonSample(objTable, cols)
        Dim frmResults As New FormResultsDisplay
        frmResults.TextBoxResults.Text = HelpClass.GetXMLToJSON(DisplayTable, cols)
        frmResults.Show()
    End Sub

    Private Sub KockoutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KockoutToolStripMenuItem.Click
        Dim contextFileName = Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
        Dim fsClass As New FileStream(contextFileName, FileMode.Open)
        Dim srKockoutMasterDetails As String = ""
        Using srclass As New StreamReader(fsClass)
            srKockoutMasterDetails = srclass.ReadToEnd
        End Using
        fsClass.Close()

        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim SB As New StringBuilder(srKockoutMasterDetails)
        SB.Replace("[$tableName]", objTable.TableValue)
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub DurandalBaseHtmlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DurandalBaseHtmlToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim wpfForm As New ManageWizard.ManageWis1
        wpfForm.SetData = _SelectedDatabase
        Dim result = wpfForm.ShowDialog()
        Dim SaveLocation = wpfForm.VM.SaveLocation
        Dim tb = wpfForm.VM.TableToProcess
        wpfForm.Close()

        Dim tmpLocal = Application.StartupPath & "\Templates\Knockout\demoTemp.txt"
        Dim ft = DurandalHelper.GetTemplateFile(tmpLocal)
        Dim SB As New StringBuilder(ft)
        SB.Replace("[$tableName]", tb.TableValue)

        Dim fileName = tb.TableValue & ".html"
        Dim savePath = SaveLocation & "\views\" & tb.TableValue.ToLower & "\"
        Dim saveData = DurandalHelper.SaveLocation(fileName, SB.ToString, savePath)


        tmpLocal = Application.StartupPath & "\Templates\Knockout\demoVMTemp.txt"
        ft = DurandalHelper.GetTemplateFile(tmpLocal)
        SB = New StringBuilder(ft)
        SB.Replace("[$tableName]", tb.TableValue)

        fileName = tb.TableValue & ".js"
        savePath = SaveLocation & "\viewmodels\" & tb.TableValue.ToLower & "\"
        saveData = DurandalHelper.SaveLocation(fileName, SB.ToString, savePath)
        SB.AppendLine()
        SB.AppendLine("'{ route: '" & tb.TableValue & "', moduleId: 'viewmodels/" & tb.TableValue.ToLower & "/" & tb.TableValue.ToLower & "', nav: true },")
        VBCodeEditor.Text = SB.ToString

    End Sub

    Private Sub DurandalBaseViewModelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DurandalBaseViewModelToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim wpfForm As New ManageWizard.ManageWis1
        wpfForm.SetData = _SelectedDatabase
        Dim result = wpfForm.ShowDialog()
        Dim SaveLocation = wpfForm.VM.SaveLocation
        Dim vm = wpfForm.VM
        wpfForm.Close()
        If result = False Then
            Exit Sub
        End If

     

        Dim tmpLocal = Application.StartupPath & "\Templates\Knockout\SimpleKoTemplate.txt"
        Dim ft = DurandalHelper.GetTemplateFile(tmpLocal)

        tmpLocal = Application.StartupPath & "\Templates\Knockout\tableDataContext.txt"
        ft = DurandalHelper.GetTemplateFile(tmpLocal)
        Dim SBctx As New StringBuilder(ft)
        SBctx.Replace("[$tableName]", vm.Table.TableName)
        SBctx.Replace("[$tableKey]", vm.ColumnKey)
        SBctx.Replace("[$tableNamelow]", vm.Table.TableName.ToLower)
        SBctx.Replace("[$tableNameSingle]", vm.Table.TableSingularize)
        SBctx.Replace("[$tableNameSingleLow]", vm.Table.TableSingularize)
        '
        
        SBctx.AppendLine()
        SBctx.AppendLine("'{ route: '" & vm.Table.TableName & "', moduleId: 'viewmodels/" & vm.Table.TableName.ToLower & "/" & vm.Table.TableName.ToLower & "', nav: true },")
        VBCodeEditor.Text = VBCodeEditor.Text & SBctx.ToString
    End Sub

    Private Sub KnockoutHtmlTemplateMasteDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KnockoutHtmlTemplateMasteDetailsToolStripMenuItem.Click
        Dim contextFileName = Application.StartupPath & "\Templates\Knockout\MasterDetailsTmp.txt"
        Dim fsClass As New FileStream(contextFileName, FileMode.Open)
        Dim srKockoutMasterDetails As String = ""
        Using srclass As New StreamReader(fsClass)
            srKockoutMasterDetails = srclass.ReadToEnd
        End Using
        fsClass.Close()

        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim tblist = knockout.GetHtmlTableList(objTable)
        Dim frmData = knockout.GetHtmlDataForm(objTable)
        Dim SB As New StringBuilder(srKockoutMasterDetails)
        SB.Replace("[$MASTERTABLE]", tblist)
        SB.Replace("[$DETAILSTABLE]", frmData)
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub KnockoutScriptAddNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KnockoutScriptAddNewToolStripMenuItem.Click
        Dim contextFileName = Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
        Dim fsClass As New FileStream(contextFileName, FileMode.Open)
        Dim srKockoutMasterDetails As String = ""
        Using srclass As New StreamReader(fsClass)
            srKockoutMasterDetails = srclass.ReadToEnd
        End Using
        fsClass.Close()

        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim SB As New StringBuilder(srKockoutMasterDetails)
        SB.Replace("[$tableName]", objTable.TableValue)
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub KnockoutServerManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KnockoutServerManagerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim wpfForm As New ManageWizard.ManageWis1
        wpfForm.SetData = _SelectedDatabase
        Dim result = wpfForm.ShowDialog()
        Dim SaveLocation = wpfForm.VM.SaveLocation
        Dim tb = wpfForm.VM.Table
        wpfForm.Close()
        If result = False Then
            Exit Sub
        End If
        Dim tmpLocal = Application.StartupPath & "\Templates\Knockout\demoTemp.txt"
        Dim ft = DurandalHelper.GetTemplateFile(tmpLocal)
        Dim SB As New StringBuilder(ft)
        'Dim tblist = knockout.GetHtmlTableList(tb)
        'Dim frmData = knockout.GetHtmlForm(tb)
        SB.Replace("[$tableName]", tb.TableName)

        Dim fileName = tb.TableName & ".html"
        Dim savePath = SaveLocation & "\views\" & tb.TableName.ToLower & "\"
        Dim saveData = DurandalHelper.SaveLocation(fileName, SB.ToString, savePath)
        ' VBCodeEditor.Text = SB.ToString

        tmpLocal = Application.StartupPath & "\Templates\Knockout\demoVMTemp.txt"
        ft = DurandalHelper.GetTemplateFile(tmpLocal)
        SB = New StringBuilder(ft)
        'Dim tblist = knockout.GetHtmlTableList(tb)
        'Dim frmData = knockout.GetHtmlForm(tb)
        SB.Replace("[$tableName]", tb.TableName)

        fileName = tb.TableName.ToLower & ".js"
        savePath = SaveLocation & "\viewmodels\" & tb.TableName.ToLower & "\"
        saveData = DurandalHelper.SaveLocation(fileName, SB.ToString, savePath)
        '{ route: 'caustomers', moduleId: 'viewmodels/customers/customers', nav: true },
        SB.AppendLine()
        SB.AppendLine("'{ route: '" & tb.TableName & "', moduleId: 'viewmodels/" & tb.TableName.ToLower & "/" & tb.TableName.ToLower & "', nav: true },")
        VBCodeEditor.Text = SB.ToString

    End Sub
    Private Sub CreateMasterDetailsVM(SaveLocation As String, tb As TableNameInfo)
        Dim contextFileName = Application.StartupPath & "\Templates\Knockout\vmTemplate.txt"
        Dim fsClass As New FileStream(contextFileName, FileMode.Open)
        Dim srKockoutMasterDetails As String = ""
        Using srclass As New StreamReader(fsClass)
            srKockoutMasterDetails = srclass.ReadToEnd
        End Using
        fsClass.Close()


        Dim SB As New StringBuilder(srKockoutMasterDetails)
        SB.Replace("[$tableName]", tb.TableValue)
        Dim location = SaveLocation & "\" & tb.TableValue & ".js"
        My.Computer.FileSystem.WriteAllText(location, SB.ToString, False)
    End Sub

    Private Sub CreateModelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateModelsToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase

        Dim frmTable As New FormSelectViews(_SelectedDatabase)
        Dim cols As List(Of ColumnsInfo) = Nothing
        Dim isOK = frmTable.ShowDialog()

        If isOK = DialogResult.OK Then
            Dim viewToTable = frmTable.SelectedViews
            objTable = TableNameInfo.CreateTableFromView(viewToTable)
            cols = frmTable.listSelectedColumns
            frmTable.Close()
        Else
            frmTable.Close()
            Exit Sub
        End If
        VBCodeEditor.Text = knockout.GetKoClassView(objTable)
    End Sub

    Private Sub JSonClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JSonClassToolStripMenuItem.Click
        ' ManageInfoClass.CreateJSonVBClass
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = ManageInfoClass.CreateJSonVBClass(objTable)
    End Sub

    Private Sub MVCControllersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MVCControllersToolStripMenuItem.Click
        ' ManageInfoClass.CreateJSonVBClass
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = MVC5Controller.MVC5Template(_SelectedDatabase, objTable)
    End Sub

    Private Sub MVCAPIRepositoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MVCAPIRepositoryToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = MVC5Controller.MVC5WebAPITemplate(_SelectedDatabase, objTable)
    End Sub

    Private Sub MVCIRepositoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MVCIRepositoryToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = IRepository.CreateIRepository(objTable)

    End Sub

    Private Sub MVCDataRepositoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MVCDataRepositoryToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = DataRepository.MVCDataRepository(_SelectedDatabase, objTable)
    End Sub

    Private Sub TestMVCControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestMVCControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = TestWebAPI.WebAPITest(objTable)
    End Sub

    Private Sub TestMVCRepositoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestMVCRepositoryToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = TestWebAPI.RepositoryTest(objTable)
    End Sub

    Private Sub KnockoutClassReadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KnockoutClassReadToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = knockout.GetKoReadClass(objTable)
    End Sub

    Private Sub AngularToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AngularToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateSimpleAngularApp(objTable)
    End Sub

    Private Sub SimpleControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimpleControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateSimpleController(objTable)
    End Sub

    Private Sub SimpleDirectiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimpleDirectiveToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateBasicDirectives(objTable)
    End Sub

    Private Sub SimpleServicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimpleServicesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateBasicServices(objTable)
    End Sub

    Private Sub AdvanceAppToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AdvanceAppToolStripMenuItem1.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateAdvanceAngularApp(objTable)
    End Sub

    Private Sub AdvanceControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvanceControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateComplexController(objTable)
    End Sub

    Private Sub AdvanceDirectiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvanceDirectiveToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateDirectives(objTable)
    End Sub

    Private Sub AdvanceServicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvanceServicesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateAdvanceServices(objTable)
    End Sub

    Private Sub MedServicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MedServicesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = AngularManager.CreateMedServices(objTable)
    End Sub

    Private Sub AureliaClassListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AureliaClassListToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = Aurelia.AureliaClassList(objTable)

    End Sub

    Private Sub AureliaTemplateListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AureliaTemplateListToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If

        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        Dim DGTemplate As String = "\Templates\Aurelia\AureliaList.txt"
        Dim BllFile As New FileStream(Application.StartupPath & DGTemplate, FileMode.Open)
        Dim sr As New StreamReader(BllFile)
        Dim StrRead As String = sr.ReadToEnd
        Dim SB As New StringBuilder(StrRead)


        Dim _MAINPAGELIST As String = Aurelia.GetAureliaTemplateList(objTable)
        SB.Replace("[TABLENAME]", objTable.TableValue)
        SB.Replace("[TABLE_LIST]", _MAINPAGELIST)

        '[TABLE_LIST]
        '[TABLENAME]
        'Dim resultListTable = Aurelia.GetAureliaTemplateList(objTable)
        'Dim result = 
        VBCodeEditor.Text = SB.ToString
    End Sub

    Private Sub SaveFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveFileToolStripMenuItem.Click

    End Sub

    Private Sub AureliaClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AureliaClassToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = Aurelia.AureliaClassValidation(objTable)
    End Sub

    Private Sub AureliaFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AureliaFormToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = Aurelia.GetAureliaTemplateForm(objTable)
    End Sub

    Private Sub LaravelCreateMigrationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaravelCreateMigrationToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = Laravel.CreateTableMigrate(objTable)
    End Sub

    Private Sub LaravelCreateSeedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaravelCreateSeedToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = Laravel.CreateSeeder(objTable)
    End Sub

    Private Sub LaravelControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaravelControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = Laravel.CreateController(objTable)
    End Sub

    Private Sub AsycRepoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsycRepoToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = DataRepositorySync.RepositoryGetList(objTable)
    End Sub

    Private Sub WebApiControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebApiControllerToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = WebApiController.MVCTableEditTemplate(objTable)
    End Sub

    Private Sub AngularClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AngularClassToolStripMenuItem.Click

        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)

        VBCodeEditor.Text = DBCodeGenerator.angularJS.CreateTypeScripClass(objTable)
    End Sub
End Class
