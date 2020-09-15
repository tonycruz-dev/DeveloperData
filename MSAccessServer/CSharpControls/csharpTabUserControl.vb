Imports DBExtenderLib
'Imports ManagLinqFile
Imports System.Text
Imports DBCodeGenerator
Imports ManagLinqFile
Imports System.IO

Public Class csharpTabUserControl
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
        VBCodeEditor.Text = CSModels.CreateCSClass(objTable)
    End Sub

    Private Sub ModelInotifyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModelInotifyToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'VBCodeEditor.Text = CSModels.CreateCSClass_INotifyPropertyChanged(objTable)
        VBCodeEditor.Text = CSClassManager.CreateCSClass2016_INotifyPropertyChanged(objTable)
    End Sub

    Private Sub VBClass2010SampleData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClass2010SampleData.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        objTable.Database = _SelectedDatabase
        VBCodeEditor.Text = CSClassManager.CSClass2010SampleData(objTable)
    End Sub
    Private Sub VBClass2010XmlSampleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClass2010XmlSampleToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
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
        VBCodeEditor.Text = CSModels.CreateJSonCSClass(objTable)
    End Sub

    Private Sub TSMVBRiaSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMVBRiaSelect.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSModels.SelecteToViewModel(objTable, _SelectedDatabase)

    End Sub

    Private Sub TSMVBRiaUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMVBRiaUpdate.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSClassManager.CSClassRead(objTable)

    End Sub

    Private Sub VBClassRiaInsertToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBClassRiaInsertToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSClassManager.CSClassUpdate(objTable)

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
        Dim ralateTables = objTable.GetForeignKey ' (_SelectedDatabase)
        Dim strTB As String = ""

        For Each t In ralateTables
            strTB &= t.RelatedTable & vbNewLine
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
        'Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'Try
        '    Dim frmwpf As New WPFWindFormDetails(objTable)
        '    frmwpf.Show()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
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
        'Dim frmcolumnCombo As New FormComboBoxTask(_SelectedDatabase, objTable)
        'frmcolumnCombo.ShowDialog()
        '_SelectedDatabase = frmcolumnCombo.Database
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
        VBCodeEditor.Text = LinqHelperCSharp.CS_DataContext(_SelectedDatabase)
    End Sub

    Private Sub CreateLinqTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateLinqTablesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = LinqHelperCSharp.CS_Tables(objTable)
    End Sub

    Private Sub CreateLinqRalatedTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateLinqRalatedTablesToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = LinqHelperCSharp.CS_TablesLinqTables(objTable, _SelectedDatabase)
    End Sub

    Private Sub TSBChangeTasbleQuery_Click(sender As System.Object, e As System.EventArgs) Handles TSBChangeTasbleQuery.Click
        TSCmbTables.ComboBox.DataSource = Nothing
        TSCmbTables.ComboBox.DataSource = _SelectedDatabase.ListViews
        TSCmbTables.ComboBox.DisplayMember = "TableName"
        TSCmbTables.ComboBox.ValueMember = "TableID"
    End Sub

    Private Sub CEntityClassToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CEntityClassToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSEntityClass.CreateVBClass2010(objTable, _SelectedDatabase)
    End Sub

    Private Sub CEntityConnectionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CEntityConnectionToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSEntityClass.CreateConnectionString(_SelectedDatabase)
    End Sub
    Private Sub CEntityContextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CEntityContextToolStripMenuItem.Click

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
        VBCodeEditor.Text = CSEntityClass.CreateVBDbContext(_SelectedDatabase, listtables)
    End Sub
    Private Sub CEntitYToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CEntitYToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSEntityClass.CreateEntityFreworkMaping(objTable, _SelectedDatabase)
    End Sub
    Private Sub CEntitySeedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CEntitySeedToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSEntityClass.CreateCustomDatabaseInitializer(_SelectedDatabase, objTable)
    End Sub
    Private Sub CSaveEntityDataToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CSaveEntityDataToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
       
        Dim listtables As List(Of TableNameInfo) = Nothing
        Dim projectName As String = ""
        Dim SavePath As String = ""
        Dim frmEntitySave As New FormGeneRateCsharpEntityframeworkFiles(_SelectedDatabase)
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
        writeToContext(projectName, SavePath, listtables)
        writeToClassAndConfig(projectName, SavePath, listtables)
        MessageBox.Show("Entity Created")
    End Sub
    Private Sub writeToContext(projectName As String, SavePath As String, listtables As List(Of TableNameInfo))
        Dim ResultsDataContext = CSEntityClass.CreateVBDbContext(_SelectedDatabase, listtables)
        Const TemplateEntityClass As String = "\Templates\Csharp\Entities\csharpEntityDataContext.txt"
        Dim csECFS As New FileStream(Application.StartupPath & TemplateEntityClass, FileMode.Open)
        Dim StrRead As String = ""
        Using sr As New StreamReader(csECFS)
            StrRead = sr.ReadToEnd
        End Using
        Dim SB As New StringBuilder(StrRead)
        SB.Replace("[DataContext]", ResultsDataContext)
        SB.Replace("[projectName]", projectName)
        Dim contextFileName = SavePath & "\" & _SelectedDatabase.DatabaseName & "Context.cs"
        My.Computer.FileSystem.WriteAllText(contextFileName, SB.ToString, False)
    End Sub
    Private Sub writeToClassAndConfig(projectName As String, SavePath As String, listtables As List(Of TableNameInfo))

        Const TemplateEntityClass As String = "\Templates\Csharp\Entities\csharpentityClass.txt"
        Const TemplateEntityConfig As String = "\Templates\Csharp\Entities\csharpentityConfiguration.txt"
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
            Dim Resultsclass = CSEntityClass.CreateVBClass2010(t, _SelectedDatabase)
            Dim sbClass As New StringBuilder(srClassResults)
            sbClass.Replace("[CLASSENTITY]", Resultsclass)
            sbClass.Replace("[projectName]", projectName)
            Dim contextFileName = SavePath & "\" & t.TableValue & ".cs"
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
            Dim ResultsConfig = CSEntityClass.CreateEntityFreworkMaping(t, _SelectedDatabase)
            Dim sbConfig As New StringBuilder(srConfigResults)
            sbConfig.Replace("[ConfigurationName]", ResultsConfig)
            sbConfig.Replace("[projectName]", projectName)
            Dim contextFileName = NewPath & "\" & t.TableValue & "Map.cs"
            My.Computer.FileSystem.WriteAllText(contextFileName, sbConfig.ToString, False)
        Next
    End Sub
    Private Sub CreateLinqFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateLinqFormToolStripMenuItem.Click
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
        Dim StrResults = LinkCSharpClassHelper.CS_DataContext(db) & vbNewLine
        For Each t In db.ListTable
            StrResults &= LinkCSharpClassHelper.CS_Tables(t, db, True) & vbNewLine
        Next

        Dim frmResults As New FormResultsDisplay
        frmResults.TextBoxResults.Text = StrResults
        frmResults.Show()
    End Sub

    Private Sub LinqContextDeleteInsertUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LinqContextDeleteInsertUpdateToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'VBCodeEditor.Text = HelperPartialClass.CreatePartialClass(_SelectedDatabase.DatabaseName, objTable)
        Dim frmResults As New FormResultsDisplay
        frmResults.TextBoxResults.Text = CSHelperPartialClass.CreatePartialClass(_SelectedDatabase.DatabaseName, objTable)
        frmResults.Show()
    End Sub

    Private Sub CJSONViewModelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CJSONViewModelToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSModels.SelecteJosnToViewModel(objTable, _SelectedDatabase)
    End Sub

    Private Sub WebAPIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebAPIToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSApiController.CreateApiController(objTable, _SelectedDatabase)
    End Sub

    Private Sub JavaScripRepositoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JavaScripRepositoryToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = Durandal.JavaScriptRepository(objTable)
    End Sub
    Public Sub CreateSelectCsharpViewModel(table As TableNameInfo, db As DatabaseNameInfo)
        VBCodeEditor.Text = CSModels.SelecteToDataViewModel(table, _SelectedDatabase)
    End Sub
    Public Sub CreateCsharpViewModel(table As TableNameInfo, db As DatabaseNameInfo)
        VBCodeEditor.Text = CSModels.CreateJSonCSClass(table)

    End Sub
    Public Sub CreateViewLinq(vw As ViewNameInfo)
        Dim objTable As TableNameInfo = TableNameInfo.CreateTableFromView(vw)
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        VBCodeEditor.Text = LinkCSharpClassHelper.CS_Tables(objTable, _SelectedDatabase, False)

    End Sub

    Private Sub CFormatedJsonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CFormatedJsonToolStripMenuItem.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSClassManager.CSClassToJson(objTable)
    End Sub

    Private Sub ToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem10.Click
        If _SelectedDatabase Is Nothing Then
            Exit Sub
        End If
        Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        VBCodeEditor.Text = CSModels.CreateDto(objTable, _SelectedDatabase)


        'If _SelectedDatabase Is Nothing Then
        '    Exit Sub
        'End If
        'Dim objTable As TableNameInfo = CType(TSCmbTables.ComboBox.SelectedItem, TableNameInfo)
        'Dim strTB As String = ""

        'For Each t In objTable.GetMasterTables(_SelectedDatabase)
        '    strTB &= t.RelateTableName & vbNewLine
        'Next
        'VBCodeEditor.Text = strTB
    End Sub
End Class
