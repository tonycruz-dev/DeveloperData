Imports DBExtenderLib
Imports System.Text
Imports System.Data.Entity.Design.PluralizationServices
Imports System.Globalization


Public Class CSEntityClass
    Public Shared Function CreateVBClass2010(ByVal DT As TableNameInfo, db As DatabaseNameInfo) As String
        Dim efsb As New StringBuilder()
        Dim FkTb = DT.GetRalationalTables(db)
        Dim masterTables = DT.GetMasterTables(db)


        efsb.AppendLine("public class " & DT.TableSingularize)
        efsb.AppendLine(" {")
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault
        efsb.AppendLine("    public " & DT.TableSingularize & "()")
        efsb.AppendLine("    {")
        For Each R In FkTb
            efsb.AppendLine("        this." & (R.RelateTable.TablePluralize) & " = new List<" & (R.RelateTable.TableSingularize) & ">();")
            'this.Order_Details = new List<Order_Detail>();
        Next
        efsb.AppendLine("    }")
        'If Not colPk Is Nothing Then
        '    efsb.AppendLine("    Public Property " & colPk.ColumnValue & " As " & colPk.TypeVB)
        'End If

        ' Dim PropCreate = From c In DT.ListColumn Where c.IsPrimary_Key = False Select c

        For Each R As ColumnsInfo In DT.ListColumn
            efsb.AppendLine("    public " & R.LinqVarCSharp & " " & R.ColumnValue & " { get; set; } ")
            'public int ProductID { get; set; }
        Next

        For Each R In FkTb
            efsb.AppendLine("    public virtual  ICollection<" & R.RelateTable.TableSingularize & ">" & R.RelateTable.TablePluralize & " { get; set; }")
            '                    public virtual ICollection<Order_Detail> Order_Details { get; set; }
        Next
        efsb.AppendLine()
        For Each R In masterTables
            efsb.AppendLine("     public virtual " & R.RelateTable.TableSingularize & " " & R.RelateTable.TableSingularize & " { get; set; }")
            'public virtual Supplier Supplier { get; set; }
        Next
        efsb.AppendLine("}")
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
    Public Shared Function CreateVBDbContext(db As DatabaseNameInfo, tables As List(Of TableNameInfo)) As String
        Dim efsb As New StringBuilder()
        efsb.AppendLine(" public class " & db.DatabaseName & "Context : DbContext")
        efsb.AppendLine("{")
        efsb.AppendLine("    static " & db.DatabaseName & "Context()")
        efsb.AppendLine("         {")
        efsb.AppendLine("            Database.SetInitializer< " & db.DatabaseName & "Context>(null);")
        efsb.AppendLine("          {")
        efsb.AppendLine()
        efsb.AppendLine()

        efsb.AppendLine("     public " & db.DatabaseName & "Context()")
        efsb.AppendLine("        : base(" & ("Name=" & db.DatabaseName & "Context").QT & ")")
        efsb.AppendLine("    {")
        efsb.AppendLine("      }")
        '                     Public NorthwindContext()
        '                    : base("Name=NorthwindContext")
        '                       {
        '                       }
        efsb.AppendLine()
        efsb.AppendLine()


        For Each tb As TableNameInfo In tables
            efsb.AppendLine("    public DbSet<" & tb.TableSingularize & ">  " & tb.TablePluralize & " { get; set; }")
            ' public DbSet<Category> Categories { get; set; }
        Next
        efsb.AppendLine()
        efsb.AppendLine()

        efsb.AppendLine("    protected override void OnModelCreating(DbModelBuilder modelBuilder)")
        efsb.AppendLine("    {")
        For Each tb As TableNameInfo In tables
            efsb.AppendLine("        modelBuilder.Configurations.Add(new " & tb.TableSingularize & "Map());")
        Next
        efsb.AppendLine("    }")

        efsb.AppendLine()
        efsb.AppendLine()
        efsb.AppendLine("}")
        Return efsb.ToString
    End Function

    Public Shared Function CreateEntityFreworkMaping(ByVal DT As TableNameInfo, db As DatabaseNameInfo) As String
        Dim efsb As New StringBuilder()
        'Dim ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))

        efsb.AppendLine("'using System.Data.Entity.ModelConfiguration")
        efsb.AppendLine("'using Models")


        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c)



        'public class LandlordMap : EntityTypeConfiguration<Landlord>
        efsb.AppendLine("public class " & DT.TableSingularize & "Map : EntityTypeConfiguration<" & DT.TableSingularize & ">")
        efsb.AppendLine("    {")
        efsb.AppendLine("    public " & DT.TableSingularize & "Map()")
        efsb.AppendLine("      {")
        If Not colPk Is Nothing Then
            For Each c In colPk
                efsb.AppendLine("    this.HasKey(t=> t." & c.ColumnValue & ");")
                If c.IsAutoincrement Then
                    efsb.AppendLine("this.Property(t >= t." & c.ColumnValue & ")")
                    efsb.AppendLine("        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);")
                End If
            Next

        End If


        efsb.AppendLine("//        Properties")

        For Each R As ColumnsInfo In DT.ListColumn
            If R.TypeVB = "String" Then
                efsb.Append("    this.Property(t => t." & R.ColumnValue & ")")
                ' this.Property(t => t.Landlord2)
                ' .HasMaxLength(50);
                If R.IsRequared Then
                    efsb.Append(".IsRequired()")
                End If
                efsb.Append(".HasMaxLength(" & R.Size & ");")
            ElseIf R.IsRequared Then
                efsb.Append("    this.Property(t=> t." & R.ColumnValue & ")")
                If R.IsRequared Then
                    efsb.Append(".IsRequired();")
                End If
            End If
            efsb.AppendLine()
        Next
        efsb.AppendLine("//        Table & Column Mappings")
        efsb.AppendLine("        this.ToTable(" & DT.TableValue.QT & ");")
        For Each R As ColumnsInfo In DT.ListColumn
            efsb.AppendLine("        this.Property(t => t." & R.ColumnValue & ").HasColumnName(" & R.ColumnName.QT & ");")
        Next
        Dim masterTables = DT.GetMasterTables(db)
        If masterTables.Count > 0 Then
            efsb.AppendLine("   //  Relationships")
        End If

        For Each R In masterTables
            efsb.AppendLine("this.HasOptional(t=> t." & R.RelateTableSingularize & ").WithMany(t=> t." & DT.TablePluralize & ").HasForeignKey(d => d." & R.ForeignKey & ");")
        Next
        efsb.AppendLine()


        efsb.AppendLine("    }")
        efsb.AppendLine(" }")

        Return efsb.ToString
    End Function
    Public Shared Function CreateCustomDatabaseInitializer(db As DatabaseNameInfo, ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT)
        Dim dsTB As DataTable = Nothing
        If DT.Database.IsAccess2007 Or DT.Database.IsAccess2003 Then
            dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)
        Else
            dsTB = dsHelper.GetSQLDataSet(DT.StrConnection, sqlstr).Tables(0)
            '  GetDataSet(ByVal strcnn As String, ByVal sqlstr As String, dbName As String, tableName As String) As DataSet
        End If

        sb.AppendLine(" public class CustomDatabaseInitializer : DropCreateDatabaseIfModelChanges<" & db.DatabaseName & "Context>")
        sb.AppendLine("// CreateDatabaseIfNotExists <" & db.DatabaseName & "Context>")
        sb.AppendLine("{")
        sb.AppendLine("   protected override void Seed(" & db.DatabaseName & "Context context)")
        sb.AppendLine("      {")


        'List<Customers> ListCustomers = new List<Customers>();
        For Each r In dsTB.Rows
            'context.Homes.Add(home);
            sb.Append("   context." & DT.TablePluralize & ".Add(new " & DT.TableSingularize & "  {")
            Dim sbConv As New StringBuilder
            For Each col In DT.ListColumn
                sbConv.Append(" " & col.ColumnValue & "  = " & col.GetTypeData(r) & ",")
            Next
            Dim mylastComar = sbConv.ToString.LastIndexOf(",")
            sb.AppendLine(sbConv.Remove(mylastComar, 1).ToString & "});")
        Next
        sb.AppendLine("base.Seed(context);")
        sb.AppendLine("      }")
        sb.AppendLine("}")
        Return sb.ToString
    End Function
End Class
