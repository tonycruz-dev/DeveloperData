Imports DBExtenderLib
Imports System.Text
Imports System.Data.Entity.Design.PluralizationServices
Imports System.Globalization

Public Class EntityFramework



    Public Shared Function CreateVBClass2010(ByVal DT As TableNameInfo, db As DatabaseNameInfo) As String
        Dim efsb As New StringBuilder()
        Dim FkTb = DT.GetRalationalTables(db)
        Dim masterTables = DT.GetMasterTables(db)


        efsb.AppendLine("Public Class " & DT.TableSingularize)
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).ToList
        If FkTb.Count > 0 Then
            efsb.AppendLine("    Public Sub New()")

            For Each R In FkTb
                efsb.AppendLine("        me." & (R.RelateTable.TablePluralize) & " = New List(Of " & (R.RelateTable.TableSingularize) & ")")
            Next
            efsb.AppendLine("    End Sub")
        End If

        If Not colPk Is Nothing Then
            For Each pk In colPk
                efsb.AppendLine("    Public Property " & pk.ColumnValue & " As " & pk.TypeVB)
            Next

        End If

        Dim PropCreate = From c In DT.ListColumn Where c.IsPrimary_Key = False Select c

        For Each R As ColumnsInfo In PropCreate
            efsb.AppendLine("    Public Property " & R.ColumnValue & " As " & R.LinqVar)
        Next

        For Each R In FkTb
            efsb.AppendLine("    Public Overridable Property " & R.RelateTable.TablePluralize & " As ICollection (Of " & R.RelateTable.TableSingularize & ")")
        Next
        efsb.AppendLine()
        For Each R In masterTables
            efsb.AppendLine("     Public Overridable Property " & R.RelateTable.TableSingularize & " As " & R.RelateTable.TableSingularize)
        Next
        efsb.AppendLine("End Class")
        Return efsb.ToString
    End Function

    Public Shared Function CreateConnectionString(db As DatabaseNameInfo)
        Dim Cnnsb As New StringBuilder()
        Cnnsb.AppendLine("<connectionStrings>")
        Cnnsb.AppendLine("   <add name=" & (db.DatabaseName & "Context").QT)
        Cnnsb.AppendLine("    providerName=" & "System.Data.SqlClient".QT)
        Cnnsb.AppendLine("    connectionString=" & db.ConnectionString.QT & "/>")
        Cnnsb.AppendLine("</connectionStrings>")
        Return Cnnsb.ToString
    End Function
    'db As DatabaseNameInfo, tables As List(Of TableNameInfo)
    Public Shared Function CreateVBDbContext(db As DatabaseNameInfo, tables As List(Of TableNameInfo)) As String
        Dim efsb As New StringBuilder()
        efsb.AppendLine("Partial Public Class " & db.DatabaseName & "Context")
        efsb.AppendLine("    Inherits DbContext")
        efsb.AppendLine("    Shared Sub New()")
        efsb.AppendLine("        Database.SetInitializer(Of " & db.DatabaseName & "Context)(Nothing)")
        efsb.AppendLine("    End Sub")
        efsb.AppendLine()
        efsb.AppendLine()

        efsb.AppendLine("    Public Sub New()")
        efsb.AppendLine("        MyBase.New(" & ("Name=" & db.DatabaseName & "Context").QT & ")")
        efsb.AppendLine("    End Sub")
        efsb.AppendLine()
        efsb.AppendLine()


        For Each tb As TableNameInfo In tables
            efsb.AppendLine("    Public Property " & tb.TablePluralize & " As DbSet (Of " & tb.TableSingularize & ")")
        Next
        efsb.AppendLine()
        efsb.AppendLine()

        efsb.AppendLine("    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)")
        For Each tb As TableNameInfo In tables
            efsb.AppendLine("        modelBuilder.Configurations.Add(New " & tb.TableSingularize & "Map())")
        Next
        efsb.AppendLine("    End Sub")

        efsb.AppendLine()
        efsb.AppendLine()
        efsb.AppendLine("End Class")
        Return efsb.ToString
    End Function
    Public Shared Function CreateScaffoldControllerContext(db As DatabaseNameInfo) As String
        Dim efsb As New StringBuilder()
        For Each tb As TableNameInfo In db.ListTable
            efsb.AppendLine("Scaffold Controller -ModelType " & tb.TableValue & " -ControllerName " & tb.TableValue & "Controller -DbContextType " & db.DatabaseName & "Context -Repository -Force")
        Next
        Return efsb.ToString
    End Function
    'Scaffold Controller -ModelType Furniture -ControllerName FurnitureController -DbContextType DBWebAgencyContext -Repository -Force
    Public Shared Function CreateEntityFreworkMaping(ByVal DT As TableNameInfo, db As DatabaseNameInfo) As String
        Dim efsb As New StringBuilder()
        'Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))


        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c)


      

        efsb.AppendLine("Public Class " & DT.TableSingularize & "Map")
        efsb.AppendLine("    Inherits EntityTypeConfiguration(Of " & DT.TableSingularize & ")")
        efsb.AppendLine("    Public Sub New()")
        efsb.AppendLine("      '  Primary Key")

        ' Me.HasKey(Function(t) New {t.OrderID, t.ProductID})
        If Not colPk Is Nothing Then
            If colPk.Count > 1 Then
                Dim sb As New StringBuilder
                sb.Append("        Me.HasKey(Function(t) New With { ")
                For Each c In colPk
                    sb.Append("t." & c.ColumnValue & ",")
                Next
                Dim mylastComar = sb.ToString.LastIndexOf(",")
                efsb.AppendLine(sb.Remove(mylastComar, 1).ToString & "})")
            Else
                efsb.AppendLine("        Me.HasKey(Function(t) t." & colPk(0).ColumnValue & ")")
            End If

        End If
        efsb.AppendLine("        'Properties")

        For Each R As ColumnsInfo In DT.ListColumn
            If R.TypeSQL.ToLower = "nchar" Or R.TypeSQL.ToLower = "varchar" Or R.TypeSQL.ToLower = "nvarchar" Then
                efsb.Append("    Me.[Property](Function(t) t." & R.ColumnValue & ")")
                If R.IsRequared Then
                    efsb.Append(".IsRequired()")
                End If
                efsb.Append(".HasMaxLength(" & R.Size & ")")
            ElseIf R.IsRequared Then
                efsb.Append("    Me.[Property](Function(t) t." & R.ColumnValue & ")")
                efsb.Append(".IsRequired()")
            End If
            efsb.AppendLine()
        Next
        efsb.AppendLine("        'Table & Column Mappings")
        efsb.AppendLine("        Me.ToTable(" & DT.TableValue.QT & ")")
        For Each R As ColumnsInfo In DT.ListColumn
            efsb.AppendLine("        Me.[Property](Function(t) t." & R.ColumnValue & ").HasColumnName(" & R.ColumnName.QT & ")")
        Next
        Dim masterTables = DT.GetMasterTables(db)
        If masterTables.Count > 0 Then
            efsb.AppendLine("  'Relationships")
        End If
        '                    Me.HasOptional(Function(t) t.Supplier).WithMany(Function(t) t.Products).HasForeignKey(Function(d) d.SupplierID)
        For Each R In masterTables
            efsb.AppendLine("       Me.HasOptional(Function(t) t." & R.RelateTableSingularize & ").WithMany(Function(t) t." & DT.TablePluralize & ").HasForeignKey(Function(d) d." & R.ForeignKey & ")")
        Next
        efsb.AppendLine()


        efsb.AppendLine("    End Sub")
        efsb.AppendLine(" End Class")

        Return efsb.ToString
    End Function
    Public Shared Function CreateCustomDatabaseInitializer(db As DatabaseNameInfo, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT)
        Dim dsTB As DataTable = Nothing
        If DT.StrConnection.Contains("Microsoft.ACE.OLEDB") Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
            '  GetDataSet(ByVal strcnn As String, ByVal sqlstr As String, dbName As String, tableName As String) As DataSet
        End If

        sb.AppendLine(" Public Class CustomDatabaseInitializer")
        sb.AppendLine("     Inherits DropCreateDatabaseIfModelChanges(Of " & db.DatabaseName & "Context)")
        sb.AppendLine(" ' CreateDatabaseIfNotExists (Of " & db.DatabaseName & "Context)")


        sb.AppendLine("  Protected Overrides Sub Seed(context As " & db.DatabaseName & "Context)")

        For Each r In dsTB.Rows

            sb.AppendLine("   context." & DT.TablePluralize & ".Add(New " & DT.TableSingularize & " With {")
            'context.Customers.Add(New Customer() With { _
            Dim sbConv As New StringBuilder
            For Each col In DT.ListColumn
                sbConv.AppendLine("                             ." & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "                             })")
        Next
        sb.AppendLine("MyBase.Seed(context)")
        sb.AppendLine("context.SaveChanges ")
        sb.AppendLine("      End Sub")
        sb.AppendLine("End Class")
        Return sb.ToString
    End Function

    Public Shared Function SeedInitializer(db As DatabaseNameInfo, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT)
        Dim dsTB As DataTable = Nothing
        If DT.Database.IsAccess2007 Or DT.Database.IsAccess2003 Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
        End If

        For Each r In dsTB.Rows
            sb.AppendLine("   context." & DT.TablePluralize & ".Add(New " & DT.TableSingularize & " With {")
            Dim sbConv As New StringBuilder
            For Each col In DT.ListColumn
                sbConv.AppendLine("                             ." & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "                             })")
        Next
        sb.AppendLine()
        Return sb.ToString
    End Function

End Class
