Imports DBExtenderLib
Imports System.IO
Imports System.Text

Public Class Helperclass

    Public Shared Sub DisplayTree(ByVal tv As TreeView, ByVal DBList As List(Of DatabaseNameInfo))
        Dim tnServer As New TreeNode("VB Server Provider")
        tnServer.ImageIndex = 14
        tnServer.SelectedImageIndex = 14
        tnServer.Expand()
        Dim tnDatabases As New TreeNode("Databases")
        tnDatabases.ImageIndex = 15
        tnDatabases.SelectedImageIndex = 15
        For Each db As DatabaseNameInfo In DBList
            Dim dbNode As TreeNodeDatabase
            If db.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftAccess Then
                dbNode = New TreeNodeDatabase(db)
                dbNode.ImageIndex = 17
                dbNode.SelectedImageIndex = 17
            ElseIf db.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServer Then
                dbNode = New TreeNodeDatabase(db)
                dbNode.ImageIndex = 13
                dbNode.SelectedImageIndex = 13
            ElseIf db.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerFile Then
                dbNode = New TreeNodeDatabase(db)
                dbNode.ImageIndex = 13
                dbNode.SelectedImageIndex = 13
            ElseIf db.SelectedTypeOfDatabase = DatabaseNameInfo.TypeOfDatabase.MicrosoftSqlServerMobile Then
                dbNode = New TreeNodeDatabase(db)
                dbNode.ImageIndex = 19
                dbNode.SelectedImageIndex = 19
            Else
                dbNode = New TreeNodeDatabase(db)
                dbNode.ImageIndex = 18
                dbNode.SelectedImageIndex = 18
            End If
            Dim tnTables As New TreeNode("Tables")


            Dim tnWebForms As New TreeNode("Web Applications")

            tnWebForms.ImageIndex = 15
            tnWebForms.SelectedImageIndex = 15


            tnTables.ImageIndex = 15
            tnTables.SelectedImageIndex = 15
            'tnTables.Expand()
            For Each tb As TableNameInfo In db.ListTable
                Dim tbNode As New TreeNodeTable(tb)

                tbNode.ImageIndex = 3
                tbNode.SelectedImageIndex = 3

                Dim NodeCol As New TreeNode("Columns")
                NodeCol.SelectedImageIndex = 15
                NodeCol.ImageIndex = 15

                For Each Col As ColumnsInfo In tb.ListColumn
                    Dim NodeColumn As New TreeNodeColumns(Col)
                    If Col.IsPrimary_Key Then
                        NodeColumn.ImageIndex = 12
                        NodeColumn.SelectedImageIndex = 12
                    ElseIf Col.TypeOfControl = "ComboBox" Then
                        NodeColumn.ImageIndex = 20
                        NodeColumn.SelectedImageIndex = 20
                        NodeColumn.Text = Col.ComboBox.DisplayColumns
                    ElseIf Col.IsForeign_Key Then
                        NodeColumn.ImageIndex = 16
                        NodeColumn.SelectedImageIndex = 16
                    Else
                        NodeColumn.ImageIndex = 4
                        NodeColumn.SelectedImageIndex = 4
                    End If
                    NodeCol.Nodes.Add(NodeColumn)
                Next
                tbNode.Nodes.Add(NodeCol)
                tnTables.Nodes.Add(tbNode)
            Next

            Dim tnSP As New TreeNode("Stored Procedures")
            tnSP.ImageIndex = 15
            tnSP.SelectedImageIndex = 15
            For Each SP As StoredProcedureNameInfo In db.ListSPro
                Dim tbSPNode As New TreeNodeProcedures(SP)
                tbSPNode.ImageIndex = 5
                tbSPNode.SelectedImageIndex = 5
                tnSP.Nodes.Add(tbSPNode)
            Next
            Dim tnViews As New TreeNode("Views")
            tnViews.ImageIndex = 15
            tnViews.SelectedImageIndex = 15
            For Each OView As ViewNameInfo In db.ListViews
                Dim tbViewNode As New TreeNodeView(OView)
                tbViewNode.ImageIndex = 6
                tbViewNode.SelectedImageIndex = 6
                Dim NodeCol As New TreeNode("Columns")
                NodeCol.SelectedImageIndex = 15
                NodeCol.ImageIndex = 15
                For Each Col As ColumnsInfo In OView.ViewColumns
                    Dim NodeColumn As New TreeNode(Col.ColumnName)
                    If Col.IsPrimary_Key Then
                        NodeColumn.ImageIndex = 12
                        NodeColumn.SelectedImageIndex = 12
                    ElseIf Col.IsForeign_Key Then
                        NodeColumn.ImageIndex = 16
                        NodeColumn.SelectedImageIndex = 16
                    Else
                        NodeColumn.ImageIndex = 4
                        NodeColumn.SelectedImageIndex = 4
                    End If

                    NodeCol.Nodes.Add(NodeColumn)
                Next
                tbViewNode.Nodes.Add(NodeCol)
                tnViews.Nodes.Add(tbViewNode)
            Next
            Dim tnWPF As New TreeNode("WPF Applications")
            tnWPF.ImageIndex = 15
            tnWPF.SelectedImageIndex = 15
            If Not db.WPFDatabaseContext.WPFContext Is Nothing Then
                Dim NodewpfContext As New TreeNodeWPFDataContext(db.WPFDatabaseContext)
                NodewpfContext.ImageIndex = 29
                NodewpfContext.SelectedImageIndex = 28
                Dim tnWpfTables As New TreeNode("Tables")
                tnWpfTables.ImageIndex = 15
                tnWpfTables.SelectedImageIndex = 15
                For Each tb As WPFTablesInfo In db.WPFDatabaseContext.ListwpfTable.Where(Function(pt) pt.TableType = "Table")
                    Dim tbNode As New TreeNodeWPFTable(tb)
                    tbNode.ImageIndex = 3
                    tbNode.SelectedImageIndex = 3
                    Dim nodewpfGrid As New TreeNodeWPFDatagridTable(tb)
                    nodewpfGrid.ImageIndex = 30
                    nodewpfGrid.SelectedImageIndex = 30
                    Dim NodeColGrid As New TreeNode("Columns")
                    NodeColGrid.SelectedImageIndex = 15
                    NodeColGrid.ImageIndex = 15
                    For Each Col In tb.DataGrigListColumn
                        Dim NodeColumn As New TreeNode(Col.ColumnName)
                        If Col.TypeOfControl = "ComboBox" Then
                            NodeColumn.ImageIndex = 20
                            NodeColumn.SelectedImageIndex = 20
                        Else
                            NodeColumn.ImageIndex = 4
                            NodeColumn.SelectedImageIndex = 4
                        End If
                        NodeColGrid.Nodes.Add(NodeColumn)
                    Next
                    nodewpfGrid.Nodes.Add(NodeColGrid)
                    tbNode.Nodes.Add(nodewpfGrid)
                    Dim nodewpfDetails As New TreeNodeWPFDetailsTable(tb)
                    nodewpfDetails.ImageIndex = 30
                    nodewpfDetails.SelectedImageIndex = 30

                    Dim NodeColDetails As New TreeNode("Columns")
                    NodeColDetails.SelectedImageIndex = 15
                    NodeColDetails.ImageIndex = 15
                    For Each Col In tb.DetailsListColumn
                        Dim NodeColumn As New TreeNode(Col.ColumnName)
                        If Col.TypeOfControl = "ComboBox" Then
                            NodeColumn.ImageIndex = 20
                            NodeColumn.SelectedImageIndex = 20
                        Else
                            NodeColumn.ImageIndex = 4
                            NodeColumn.SelectedImageIndex = 4
                        End If
                        NodeColDetails.Nodes.Add(NodeColumn)
                    Next
                    nodewpfDetails.Nodes.Add(NodeColDetails)

                    tbNode.Nodes.Add(nodewpfDetails)

                    tnWpfTables.Nodes.Add(tbNode)
                Next
                Dim tnWpfViews As New TreeNode("Views")
                tnWpfViews.ImageIndex = 15
                tnWpfViews.SelectedImageIndex = 15
                For Each tb As WPFTablesInfo In db.WPFDatabaseContext.ListwpfTable.Where(Function(pt) pt.TableType = "View")
                    Dim tbNode As New TreeNodeWPFTable(tb)
                    tbNode.ImageIndex = 6
                    tbNode.SelectedImageIndex = 6
                    Dim nodewpfGrid As New TreeNodeWPFDatagridTable(tb)
                    nodewpfGrid.ImageIndex = 30
                    nodewpfGrid.SelectedImageIndex = 30
                    Dim NodeColGrid As New TreeNode("Columns")
                    NodeColGrid.SelectedImageIndex = 15
                    NodeColGrid.ImageIndex = 15
                    For Each Col In tb.DataGrigListColumn
                        Dim NodeColumn As New TreeNode(Col.ColumnName)
                        If Col.TypeOfControl = "ComboBox" Then
                            NodeColumn.ImageIndex = 20
                            NodeColumn.SelectedImageIndex = 20
                        Else
                            NodeColumn.ImageIndex = 4
                            NodeColumn.SelectedImageIndex = 4
                        End If
                        NodeColGrid.Nodes.Add(NodeColumn)
                    Next
                    nodewpfGrid.Nodes.Add(NodeColGrid)
                    tbNode.Nodes.Add(nodewpfGrid)
                    Dim nodewpfDetails As New TreeNodeWPFDetailsTable(tb)
                    nodewpfDetails.ImageIndex = 30
                    nodewpfDetails.SelectedImageIndex = 30

                    Dim NodeColDetails As New TreeNode("Columns")
                    NodeColDetails.SelectedImageIndex = 15
                    NodeColDetails.ImageIndex = 15
                    For Each Col In tb.DetailsListColumn
                        Dim NodeColumn As New TreeNode(Col.ColumnName)
                        If Col.TypeOfControl = "ComboBox" Then
                            NodeColumn.ImageIndex = 20
                            NodeColumn.SelectedImageIndex = 20
                        Else
                            NodeColumn.ImageIndex = 4
                            NodeColumn.SelectedImageIndex = 4
                        End If
                        NodeColDetails.Nodes.Add(NodeColumn)
                    Next
                    nodewpfDetails.Nodes.Add(NodeColDetails)

                    tbNode.Nodes.Add(nodewpfDetails)

                    tnWpfViews.Nodes.Add(tbNode)
                Next

                NodewpfContext.Nodes.Add(tnWpfTables)
                NodewpfContext.Nodes.Add(tnWpfViews)
                tnWPF.Nodes.Add(NodewpfContext)
            End If



            Dim tnSilverLight As New TreeNode("Silverlight")
            tnSilverLight.ImageIndex = 15
            tnSilverLight.SelectedImageIndex = 15

            Dim LinqClassNode As New TreeNode("Linq DataContext")
            LinqClassNode.ImageIndex = 15
            LinqClassNode.SelectedImageIndex = 15
            For Each Lnq In db.ListLinqDatabase
                Dim NodeEntity As New TreeNodeLinqDataContext(Lnq)
                NodeEntity.ImageIndex = 23
                NodeEntity.SelectedImageIndex = 23
                Dim tnEntityTables As New TreeNode("Tables")
                tnEntityTables.ImageIndex = 15
                tnEntityTables.SelectedImageIndex = 15
                For Each tb As LinqTableNameinfo In Lnq.ListLinqTable.Where(Function(pt) pt.TableType = "Table")
                    Dim tbNode As New TreeNodeTableLinq(tb)
                    If tb.TableType = "Table" Then
                        tbNode.ImageIndex = 3
                        tbNode.SelectedImageIndex = 3
                    Else

                    End If
                    tnEntityTables.Nodes.Add(tbNode)
                Next
                Dim tnViewLinq As New TreeNode("Views")
                tnViewLinq.ImageIndex = 15
                tnViewLinq.SelectedImageIndex = 15
                For Each tb As LinqTableNameinfo In Lnq.ListLinqTable.Where(Function(pt) pt.TableType = "View")
                    Dim tbViewNode As New TreeNodeTableLinq(tb)
                    tbViewNode.ImageIndex = 6
                    tbViewNode.SelectedImageIndex = 6
                    tnViewLinq.Nodes.Add(tbViewNode)
                Next
                NodeEntity.Nodes.Add(tnEntityTables)
                NodeEntity.Nodes.Add(tnViewLinq)
                LinqClassNode.Nodes.Add(NodeEntity)
            Next
            Dim DomSerClassNode As New TreeNode("Domain Service")
            DomSerClassNode.ImageIndex = 15
            DomSerClassNode.SelectedImageIndex = 15
            For Each ds In db.ListDomainservices
                Dim Nodeds As New TreeNodeTableDomainClass(ds)
                Nodeds.ImageIndex = 22
                Nodeds.SelectedImageIndex = 22
                Dim tnEntityTables As New TreeNode("Tables")
                tnEntityTables.ImageIndex = 15
                tnEntityTables.SelectedImageIndex = 15
                For Each tb As TableNameInfo In ds.ListTable.Where(Function(pt) pt.TableType = "Table")
                    Dim tbNode As New TreeNodeTableDomainTables(tb)
                    tbNode.ImageIndex = 3
                    tbNode.SelectedImageIndex = 3
                    Dim lstFunct = (From fl In ds.ListFunctions Where fl.TableValue = tb.TableValue).SingleOrDefault
                    If Not lstFunct Is Nothing Then
                        Dim NodeCol As New TreeNode("Functions")
                        NodeCol.SelectedImageIndex = 15
                        NodeCol.ImageIndex = 15
                        tbNode.ListFunction = lstFunct


                        Dim NodeSelect As New TreeNodeFunctionSelectLinq(lstFunct)
                        NodeSelect.ImageIndex = 28
                        NodeSelect.SelectedImageIndex = 28

                        Dim NodeInsert As New TreeNodeFunctionInsertLinq(lstFunct)
                        NodeInsert.ImageIndex = 26
                        NodeInsert.SelectedImageIndex = 26

                        Dim NodeUpdate As New TreeNodeFunctionUpdateLinq(lstFunct)
                        NodeUpdate.ImageIndex = 27
                        NodeUpdate.SelectedImageIndex = 27

                        Dim NodeDelete As New TreeNodeFunctionDeleteLinq(lstFunct)
                        NodeDelete.ImageIndex = 25
                        NodeDelete.SelectedImageIndex = 25

                        Dim NodeSelectClass As New TreeNodeFunctionSelectSubClass(lstFunct)
                        NodeSelectClass.ImageIndex = 28
                        NodeSelectClass.SelectedImageIndex = 28


                        NodeCol.Nodes.Add(NodeSelect)
                        NodeCol.Nodes.Add(NodeInsert)
                        NodeCol.Nodes.Add(NodeUpdate)
                        NodeCol.Nodes.Add(NodeDelete)
                        NodeCol.Nodes.Add(NodeSelectClass)
                        tbNode.Nodes.Add(NodeCol)
                    End If



                    tnEntityTables.Nodes.Add(tbNode)
                Next
                Dim tnViewLinq As New TreeNode("Views")
                tnViewLinq.ImageIndex = 15
                tnViewLinq.SelectedImageIndex = 15
                For Each tb As TableNameInfo In ds.ListTable.Where(Function(pt) pt.TableType = "View")
                    Dim tbViewNode As New TreeNodeTableDomainTables(tb)
                    tbViewNode.ImageIndex = 6
                    tbViewNode.SelectedImageIndex = 6
                    Dim lstFunct = (From fl In ds.ListFunctions Where fl.TableValue = tb.TableValue).SingleOrDefault
                    If Not lstFunct Is Nothing Then
                        Dim NodeCol As New TreeNode("Functions")
                        NodeCol.SelectedImageIndex = 15
                        NodeCol.ImageIndex = 15
                        tbViewNode.ListFunction = lstFunct

                        Dim NodeSelect As New TreeNodeFunctionSelectLinq(lstFunct)
                        NodeSelect.ImageIndex = 28
                        NodeSelect.SelectedImageIndex = 28


                        NodeCol.Nodes.Add(NodeSelect)
                        tbViewNode.Nodes.Add(NodeCol)
                    End If
                    tnViewLinq.Nodes.Add(tbViewNode)
                Next
                Nodeds.Nodes.Add(tnEntityTables)
                Nodeds.Nodes.Add(tnViewLinq)
                DomSerClassNode.Nodes.Add(Nodeds)
            Next

            Dim RiaClassNode As New TreeNode("Ria Class Collections")
            RiaClassNode.ImageIndex = 15
            RiaClassNode.SelectedImageIndex = 15
            For Each ds In db.ListRiaContext
                Dim Nodeds As New TreeNodeRiaClassContent(ds)
                Nodeds.ImageIndex = 24
                Nodeds.SelectedImageIndex = 24
                Dim tnEntityTables As New TreeNode("Tables")
                tnEntityTables.ImageIndex = 15
                tnEntityTables.SelectedImageIndex = 15
                For Each tb As RiaClassInfo In ds.ListRiaClass.Where(Function(pt) pt.Table.TableType = "Table")
                    Dim tbNode As New TreeNodeRiaClass(tb)
                    tbNode.ImageIndex = 3
                    tbNode.SelectedImageIndex = 3
                    tnEntityTables.Nodes.Add(tbNode)
                Next
                Dim tnViewLinq As New TreeNode("Views")
                tnViewLinq.ImageIndex = 15
                tnViewLinq.SelectedImageIndex = 15
                For Each tb As RiaClassInfo In ds.ListRiaClass.Where(Function(pt) pt.Table.TableType = "View")
                    Dim tbViewNode As New TreeNodeRiaClass(tb)
                    tbViewNode.ImageIndex = 6
                    tbViewNode.SelectedImageIndex = 6
                    tnViewLinq.Nodes.Add(tbViewNode)
                Next
                Nodeds.Nodes.Add(tnEntityTables)
                Nodeds.Nodes.Add(tnViewLinq)
                RiaClassNode.Nodes.Add(Nodeds)
            Next
            dbNode.Nodes.Add(tnTables)
            dbNode.Nodes.Add(tnViews)
            dbNode.Nodes.Add(tnWPF)
            dbNode.Nodes.Add(tnSilverLight)
            dbNode.Nodes.Add(tnWebForms)
            dbNode.Nodes.Add(tnSP)
            dbNode.Nodes.Add(LinqClassNode)
            dbNode.Nodes.Add(DomSerClassNode)
            dbNode.Nodes.Add(RiaClassNode)
            dbNode.Expand()
            tnDatabases.Nodes.Add(dbNode)
            tnDatabases.Expand()
        Next

        tnServer.Nodes.Add(tnDatabases)
        tv.Nodes.Clear()
        tv.Nodes.Add(tnServer)



    End Sub
End Class
Public Class TemplateManager

    Public Shared Function OpenTemplates(ByVal FilePath As String) As String
        Dim fs As New FileStream(FilePath, FileMode.Open)
        Dim sr As New StreamReader(fs)
        Dim SB As New StringBuilder(sr.ReadToEnd)
        sr.Close()
        fs.Close()
        Return SB.ToString
    End Function

End Class