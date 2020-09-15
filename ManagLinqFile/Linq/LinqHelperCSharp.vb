Imports System.Text
Imports DBExtenderLib

Public Class LinqHelperCSharp
    Private Shared Function QT(ByVal Value As String) As String
        Return """" & Value & """"
    End Function
    Public Shared Function CS_DataContext(db As DatabaseNameInfo) As String

        Dim sb As New StringBuilder()
        sb.AppendLine("[global::System.Data.Linq.Mapping.DatabaseAttribute(Name=" & QT("Northwind") & ")]")
        sb.AppendLine("public partial class " & db.DatabaseName & "DBDataContext : System.Data.Linq.DataContext")
        sb.AppendLine(" {")

        sb.AppendLine(" 	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();")
        sb.AppendLine("    private static IDbConnection Iconnection = new System.Data.OleDb.OleDbConnection(ApplicationName.Properties.Settings.Default.." & db.DatabaseName & "ConnectionString);")

        sb.AppendLine("   #region Extensibility Method Definitions")
        sb.AppendLine("   partial void OnCreated();")
        For Each tb In db.ListTable

            sb.AppendLine("  // partial void Insert" & tb.TableSingularize & "(" & tb.TableSingularize & " instance);")
            sb.AppendLine("  // partial void Update" & tb.TableSingularize & "(" & tb.TableSingularize & " instance);")
            sb.AppendLine("  // partial void Delete" & tb.TableSingularize & "(" & tb.TableSingularize & " instance);")

        Next
        sb.AppendLine("#endregion")
        sb.AppendLine("	public " & db.DatabaseName & "DBDataContext() :")
        sb.AppendLine("       base(Iconnection, mappingSource)")
        sb.AppendLine("	{")
        sb.AppendLine("		OnCreated();")
        sb.AppendLine("	}")


        For Each tb In db.ListTable
            sb.AppendLine(" // 	public System.Data.Linq.Table<" & tb.TableSingularize & "> " & tb.TablePluralize)
            sb.AppendLine("	//{")
            sb.AppendLine("	//	get")
            sb.AppendLine("	//	{")
            sb.AppendLine("	//		return this.GetTable<" & tb.TableSingularize & ">();")
            sb.AppendLine("	//	}")
            sb.AppendLine(" //}")
        Next
        sb.AppendLine("	}")
        Return sb.ToString


    End Function
    Public Shared Function CS_Tables(tb As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("[global::System.Data.Linq.Mapping.TableAttribute(Name=" & QT(tb.TableName) & ")]")
        sb.AppendLine("[global::System.Runtime.Serialization.DataContractAttribute()]")
        sb.AppendLine("public partial class " & tb.TableSingularize & "   : INotifyPropertyChanging, INotifyPropertyChanged")
        sb.AppendLine(" {")

        sb.AppendLine("	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);")

        For Each R In tb.ListColumn
            sb.AppendLine("    private " & R.LinqVarCSharp & " _" & R.ColumnValue & ";")
        Next
        '	private EntitySet<Product> _Products;

        sb.AppendLine("	private bool serializing;")

        sb.AppendLine("   #region Extensibility Method Definitions")
        sb.AppendLine("   partial void OnLoaded();")
        sb.AppendLine("   partial void OnValidate(System.Data.Linq.ChangeAction action);")
        sb.AppendLine("   partial void OnCreated();")
        For Each R In tb.ListColumn
            sb.AppendLine("    partial void On" & R.ColumnValue & "Changing(" & R.LinqVarCSharp & " Value);")
            sb.AppendLine("    partial void On" & R.ColumnValue & "Changed();")
        Next
        sb.AppendLine("   #endregion")

        sb.AppendLine("       public " & tb.TableSingularize & "()")
        sb.AppendLine("	{")
        sb.AppendLine("		this.Initialize();")
        sb.AppendLine("	}")
        Dim count As Integer = 1
        For Each R In tb.ListColumn
            sb.AppendLine(SetColumnAttribut(R, count))
            sb.AppendLine("	public " & R.LinqVarCSharp & " " & R.ColumnName)
            sb.AppendLine("	{")
            sb.AppendLine("		get")
            sb.AppendLine("		{")
            sb.AppendLine("			return this._" & R.ColumnName & ";")
            sb.AppendLine("		}")
            sb.AppendLine("		set")
            sb.AppendLine("		{")
            sb.AppendLine("			if ((this._" & R.ColumnName & " != value))")
            sb.AppendLine(" 			{")
            sb.AppendLine("				this.On" & R.ColumnName & "Changing(value);")
            sb.AppendLine("				this.SendPropertyChanging();")
            sb.AppendLine("				this._" & R.ColumnName & " = value;")
            sb.AppendLine("				this.SendPropertyChanged(" & QT(R.ColumnName) & ");")
            sb.AppendLine("				this.On" & R.ColumnName & "Changed();")
            sb.AppendLine("			}")
            sb.AppendLine("		}")
            sb.AppendLine("	}")

            count += 1
        Next


        

       

        sb.AppendLine("	public event PropertyChangingEventHandler PropertyChanging;")

        sb.AppendLine("	public event PropertyChangedEventHandler PropertyChanged;")

        sb.AppendLine("	protected virtual void SendPropertyChanging()")
        sb.AppendLine("	{")
        sb.AppendLine("		if ((this.PropertyChanging != null))")
        sb.AppendLine("		{")
        sb.AppendLine("			this.PropertyChanging(this, emptyChangingEventArgs);")
        sb.AppendLine("		}")
        sb.AppendLine("	}")

        sb.AppendLine("	protected virtual void SendPropertyChanged(String propertyName)")
        sb.AppendLine("	{")
        sb.AppendLine("		if ((this.PropertyChanged != null))")
        sb.AppendLine("		{")
        sb.AppendLine("			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));")
        sb.AppendLine("		}")
        sb.AppendLine("	}")

       

        sb.AppendLine("	private void Initialize()")
        sb.AppendLine("	{")
        sb.AppendLine("		OnCreated();")
        sb.AppendLine("	}")

        sb.AppendLine("	[global::System.Runtime.Serialization.OnDeserializingAttribute()]")
        sb.AppendLine("	[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]")
        sb.AppendLine("	public void OnDeserializing(StreamingContext context)")
        sb.AppendLine("	{")
        sb.AppendLine("		this.Initialize();")
        sb.AppendLine("	}")

        sb.AppendLine("	[global::System.Runtime.Serialization.OnSerializingAttribute()]")
        sb.AppendLine("	[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]")
        sb.AppendLine("	public void OnSerializing(StreamingContext context)")
        sb.AppendLine("	{")
        sb.AppendLine("		this.serializing = true;")
        sb.AppendLine("	}")

        sb.AppendLine("	[global::System.Runtime.Serialization.OnSerializedAttribute()]")
        sb.AppendLine("	[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]")
        sb.AppendLine("	public void OnSerialized(StreamingContext context)")
        sb.AppendLine("	{")
        sb.AppendLine("		this.serializing = false;")
        sb.AppendLine("	}")
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function WriteLinqVar(ByVal DT As TableNameInfo, ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        For Each R In DT.ListColumn
            strWriteModel.AppendLine("    private " & R.LinqVarCSharp & " _" & R.ColumnValue)
        Next

        Dim Entityset = (From tr In linqDb.RelateTables Where tr.RelatedTableValue = DT.TableValue).ToList
        Dim EntityRef = (From tr In linqDb.RelateTables Where tr.TableValue = DT.TableValue).ToList

        For Each R In Entityset
            'Private _Order_Details As EntitySet(Of Order_Detail)
            strWriteModel.AppendLine("     private EntitySet<" & R.TableValue & "> _" & R.TableValue & ";")
        Next

        For Each R In EntityRef
            'Private _Customer As EntityRef(Of Customer)
            strWriteModel.AppendLine("     private EntityRef<" & R.RelatedTableValue & "> _" & R.RelatedTableValue & ";")
        Next
        Return strWriteModel.ToString
    End Function
    Public Shared Function WriteLinqInitialize(ByVal DT As TableNameInfo, ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim sb As New StringBuilder
        Dim Entityset = (From tr In linqDb.RelateTables Where tr.RelatedTableValue = DT.TableValue).ToList
        Dim EntityRef = (From tr In linqDb.RelateTables Where tr.TableValue = DT.TableValue).ToList

        '      Public Order()
        '{
        '	this._Order_Details = new EntitySet<Order_Detail>(new Action<Order_Detail>(this.attach_Order_Details), new Action<Order_Detail>(this.detach_Order_Details));
        '	this._Customer = default(EntityRef<Customer>);
        '	this._Employee = default(EntityRef<Employee>);
        '	this._Shipper = default(EntityRef<Shipper>);
        '	OnCreated();
        '}

        sb.AppendLine("Private Sub Initialize()")
        For Each R In Entityset
            'Private _Order_Details As EntitySet(Of Order_Detail)
            ' Me._Order_Details = New EntitySet(Of Order_Detail)(AddressOf Me.attach_Order_Details, AddressOf Me.detach_Order_Details)
            sb.AppendLine("	Me._" & R.TableValue & "= New EntitySet(Of " & R.TableValue & "Info)(AddressOf Me.attach_" & R.TableValue & ", AddressOf Me.detach_" & R.TableValue & ")")
        Next
        For Each R In EntityRef
            'Private _Customer As EntityRef(Of Customer)
            sb.AppendLine("    Me._" & R.RelatedTableValue & " = CType(Nothing, EntityRef(Of " & R.RelatedTableValue & "Info))")
            '	OnCreated
        Next
        sb.AppendLine(" OnCreated")
        sb.AppendLine(" End Sub")
        Return sb.ToString
    End Function

    ' #Region "Extensibility Columns Method Definitions"
    Public Shared Function Extensibility_Columns_Method_Definitions(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        For Each R In DT.ListColumn
            sb.AppendLine("    partial void On" & R.ColumnValue & "Changing(" & R.LinqVarCSharp & " value);")
            sb.AppendLine("    End Sub")
            sb.AppendLine("    partial void On" & R.ColumnValue & "Changed()")
            sb.AppendLine("    End Sub")
        Next

        Return sb.ToString
    End Function


    Friend Shared Function SetColumnAttribut(ByVal col As ColumnsInfo, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        ' '	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]

        sb.AppendLine(" [global::System.Data.Linq.Mapping.ColumnAttribute(Name=" & """" & col.ColumnName & """" & ", Storage=" & """" & "_" &
                      col.ColumnValue & """" & ", DbType=" &
                      GetSQLType(col) & GetColumnnIsPrimaryKey(col) & GetColumnnCanBeNull(col) & ")]")
        sb.AppendLine(" [global::System.Runtime.Serialization.DataMemberAttribute(Order=" & ColNumber & ")] ")
        Return sb.ToString
    End Function

    Friend Shared Function SetColumnAttribut(ByVal tbName As String, relatedTable As RelationalTable, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        
        sb.AppendLine("[global::System.Data.Linq.Mapping.AssociationAttribute(Name=" & QT(tbName & "_" & relatedTable.RelateTableValue) & ", Storage=" & QT("_" & relatedTable.RelateTableSingularize) & ", ThisKey=" & QT(relatedTable.SelectedTableLinqKey) & ", OtherKey=" & QT(relatedTable.ForeignKey) & ")]")
        sb.AppendLine("[global::System.Runtime.Serialization.DataMemberAttribute(Order=" & ColNumber & ", EmitDefaultValue=false)]")


        Return sb.ToString
    End Function
    Friend Shared Function GetColumnnName(ByVal ColumnName As String) As String
        Return ColumnName.QT
    End Function
    Friend Shared Function GetColumnnName(ByVal col As ColumnsInfo) As String
        Return col.ColumnName.QT
    End Function
    Friend Shared Function GetColumnnIsPrimaryKey(ByVal col As ColumnsInfo) As String
        If col.IsPrimary_Key And col.IsAutoincrement Then
            Return ",IsPrimaryKey=true, IsDbGenerated=true"
        ElseIf col.IsPrimary_Key Then
            Return ",IsPrimaryKey=true"
        Else
            Return ""
        End If
    End Function

    Friend Shared Function GetTypeLinqVar(ByVal col As ColumnsInfo) As String
        If col.IsPrimary_Key Then
            Return col.TypeVB
        ElseIf col.IsRequared Then
            Return col.TypeVB
        Else
            Return col.LinqVar
        End If
    End Function
    Friend Shared Function GetColumnnCanBeNull(ByVal col As ColumnsInfo) As String
        If col.IsRequared Then
            Return ",CanBeNull=false"
        Else
            Return ""
        End If
    End Function

#Region " GetSQLType"
    Friend Shared Function GetSQLType(ByVal col As ColumnsInfo) As String
        Select Case col.TypeSQL.ToLower
            Case "char"
                If col.IsRequared Then
                    Return """" & "Char(" & col.Size & ") NOT NULL" & """"
                Else
                    Return """" & "Char(" & col.Size & ")" & """"
                End If

            Case "nchar"
                If col.IsRequared Then
                    Return """" & "nchar(" & col.Size & ") NOT NULL " & """"
                Else
                    Return """" & "nchar(" & col.Size & ")" & """"
                End If

            Case "nvarchar"
                If col.IsRequared Then
                    Return """" & "NVarChar(" & col.Size & ") NOT NULL " & """"
                Else
                    Return """" & "NVarChar(" & col.Size & ")" & """"
                End If

            Case "ntext"
                Return """" & "Ntext" & """"
            Case "smallint"
                Return """" & "SmallInt" & """"
            Case "int"
                If col.IsAutoincrement Then
                    Return """" & "Int NOT NULL IDENTITY" & """"
                Else
                    Return """" & "Int" & """"
                End If
            Case "int identity"
                Return """" & "Int NOT NULL IDENTITY" & """"
            Case "real"
                If col.IsRequared Then
                    Return """" & "Real NOT NULL " & """"
                Else
                    Return """" & "Real" & """"
                End If

            Case "money"
                If col.IsRequared Then
                    Return """" & "Money NOT NULL" & """"
                Else
                    Return """" & "Money" & """"
                End If

            Case "numeric"
                Return """" & "Int" & """"
            Case "bit"
                If col.IsRequared Then
                    Return """" & "Bit NOT NULL" & """"
                Else
                    Return """" & "bit" & """"
                End If
            Case "byte"
                If col.IsRequared Then
                    Return """" & "Byte NOT NULL" & """"
                Else
                    Return """" & "Byte" & """"
                End If
            Case "smallint"
                If col.IsRequared Then
                    Return """" & "smallint NOT NULL" & """"
                Else
                    Return """" & "smallint" & """"
                End If
            Case "tinyint"
                If col.IsRequared Then
                    Return """" & "TinyInt NOT NULL" & """"
                Else
                    Return """" & "TinyInt" & """"
                End If
            Case "datetime", "smalldatetime"
                Return """" & "DateTime" & """"
            Case "image"
                Return """" & "Image" & """"

        End Select

        Return """" & "nvarchar(" & col.Size & ")"
    End Function
#End Region


    Public Shared Function CS_TablesLinqTables(tb As TableNameInfo, db As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        Dim FkTb = tb.GetRalationalTables(db)
        Dim masterTables = tb.GetMasterTables(db)

        sb.AppendLine("[global::System.Data.Linq.Mapping.TableAttribute(Name=" & QT(tb.TableName) & ")]")
        sb.AppendLine("[global::System.Runtime.Serialization.DataContractAttribute()]")
        sb.AppendLine("public partial class " & tb.TableSingularize & "   : INotifyPropertyChanging, INotifyPropertyChanged")
        sb.AppendLine(" {")

        sb.AppendLine("	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);")

        For Each R In tb.ListColumn
            sb.AppendLine("    private " & R.LinqVarCSharp & " _" & R.ColumnValue & ";")
        Next
        '	private EntitySet<Product> _Products;


        For Each ft In FkTb
            sb.AppendLine("	private EntitySet<" & ft.RelateTable.TableSingularize & "> _" & ft.RelateTable.TablePluralize & ";")
        Next

        For Each mt In masterTables
            sb.AppendLine("	private EntityRef<" & mt.RelateTable.TableSingularize & "> _" & mt.RelateTable.TablePluralize & ";")

        Next

        
        sb.AppendLine("	private bool serializing;")

        sb.AppendLine("   #region Extensibility Method Definitions")
        sb.AppendLine("   partial void OnLoaded();")
        sb.AppendLine("   partial void OnValidate(System.Data.Linq.ChangeAction action);")
        sb.AppendLine("   partial void OnCreated();")
        For Each R In tb.ListColumn
            sb.AppendLine("    partial void On" & R.ColumnValue & "Changing(" & R.LinqVarCSharp & " Value);")
            sb.AppendLine("    partial void On" & R.ColumnValue & "Changed();")
        Next
        sb.AppendLine("   #endregion")

        sb.AppendLine("       public " & tb.TableValue & "()")
        sb.AppendLine("	{")
        sb.AppendLine("		this.Initialize();")
        sb.AppendLine("	}")
        Dim count As Integer = 1
        For Each R In tb.ListColumn
            sb.AppendLine(SetColumnAttribut(R, count))
            sb.AppendLine("	public " & R.LinqVarCSharp & " " & R.ColumnName)
            sb.AppendLine("	{")
            sb.AppendLine("		get")
            sb.AppendLine("		{")
            sb.AppendLine("			return this._" & R.ColumnName & ";")
            sb.AppendLine("		}")
            sb.AppendLine("		set")
            sb.AppendLine("		{")
            sb.AppendLine("			if ((this._" & R.ColumnName & " != value))")
            sb.AppendLine(" 			{")
            sb.AppendLine("				this.On" & R.ColumnName & "Changing(value);")
            sb.AppendLine("				this.SendPropertyChanging();")
            sb.AppendLine("				this._" & R.ColumnName & " = value;")
            sb.AppendLine("				this.SendPropertyChanged(" & QT(R.ColumnName) & ");")
            sb.AppendLine("				this.On" & R.ColumnName & "Changed();")
            sb.AppendLine("			}")
            sb.AppendLine("		}")
            sb.AppendLine("	}")

            count += 1
        Next


        For Each ft In FkTb
            sb.AppendLine(SetColumnAttribut(tb.TableValue, ft, count))
            sb.AppendLine(" 	public EntitySet<" & ft.RelateTableSingularize & "> " & ft.RelateTablePluralize)
            sb.AppendLine("	{")
            sb.AppendLine("		get")
            sb.AppendLine("		{")
            sb.AppendLine("			if ((this.serializing ")
            sb.AppendLine("						&& (this._" & ft.RelateTablePluralize & ".HasLoadedOrAssignedValues == false)))")
            sb.AppendLine("			{")
            sb.AppendLine("				return null;")
            sb.AppendLine("			}")
            sb.AppendLine("			return this._" & ft.RelateTablePluralize & ";")
            sb.AppendLine("		}")
            sb.AppendLine("		set")
            sb.AppendLine("		{")
            sb.AppendLine("			this._" & ft.RelateTablePluralize & ".Assign(value);")
            sb.AppendLine("		}")
            sb.AppendLine("	}")

            count += 1
        Next
        
        For Each mt In masterTables
            sb.AppendLine(SetColumnAttribut(tb.TableValue, mt, count))
            sb.AppendLine(" public " & mt.RelateTableSingularize & " " & mt.RelateTableSingularize)
            sb.AppendLine(" {")
            sb.AppendLine("	get")
            sb.AppendLine("	{")
            sb.AppendLine("		return this._" & mt.RelateTableSingularize & ".Entity;")
            sb.AppendLine("	}")
            sb.AppendLine("	set")
            sb.AppendLine("	{")
            sb.AppendLine("		" & mt.RelateTableSingularize & " previousValue = this._" & mt.RelateTableSingularize & ".Entity;")
            sb.AppendLine("		if (((previousValue != value) ")
            sb.AppendLine("					|| (this._" & mt.RelateTableSingularize & ".HasLoadedOrAssignedValue == false)))")
            sb.AppendLine("		{")
            sb.AppendLine("			this.SendPropertyChanging();")
            sb.AppendLine("			if ((previousValue != null))")
            sb.AppendLine("			{")
            sb.AppendLine("				this._" & mt.RelateTableSingularize & ".Entity = null;")
            sb.AppendLine("				previousValue." & tb.TablePluralize & ".Remove(this);")
            sb.AppendLine("			}")
            sb.AppendLine("			this._" & mt.RelateTableSingularize & ".Entity = value;")
            sb.AppendLine("			if ((value != null))")
            sb.AppendLine("			{")
            sb.AppendLine("				value." & tb.TablePluralize & ".Add(this);")
            sb.AppendLine("				this._" & mt.SelectedTableLinqKey & " = value." & mt.SelectedTableLinqKey & ";")
            sb.AppendLine("			}")
            sb.AppendLine("              else")
            sb.AppendLine("			{")
            sb.AppendLine("				this._" & mt.SelectedTableLinqKey & " = default(" & mt.ColumnType & ");")
            sb.AppendLine("			}")
            sb.AppendLine("			this.SendPropertyChanged(" & QT(mt.RelateTableSingularize) & ");")
            sb.AppendLine("		 }")
            sb.AppendLine(" 	}")
            sb.AppendLine(" }")
        Next
        sb.AppendLine("	public event PropertyChangingEventHandler PropertyChanging;")

        sb.AppendLine("	public event PropertyChangedEventHandler PropertyChanged;")

        sb.AppendLine("	protected virtual void SendPropertyChanging()")
        sb.AppendLine("	{")
        sb.AppendLine("		if ((this.PropertyChanging != null))")
        sb.AppendLine("		{")
        sb.AppendLine("			this.PropertyChanging(this, emptyChangingEventArgs);")
        sb.AppendLine("		}")
        sb.AppendLine("	}")

        sb.AppendLine("	protected virtual void SendPropertyChanged(String propertyName)")
        sb.AppendLine("	{")
        sb.AppendLine("		if ((this.PropertyChanged != null))")
        sb.AppendLine("		{")
        sb.AppendLine("			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));")
        sb.AppendLine("		}")
        sb.AppendLine("	}")

        For Each ft In FkTb
            sb.AppendLine("	private void attach_" & ft.RelateTablePluralize & "(" & ft.RelateTableSingularize & " entity)")
            sb.AppendLine("	{")
            sb.AppendLine("		this.SendPropertyChanging();")
            sb.AppendLine("	entity." & tb.TableSingularize & " = this;")
            sb.AppendLine("	}")
            sb.AppendLine("")
            sb.AppendLine("	private void detach_" & ft.RelateTablePluralize & "(" & ft.RelateTableSingularize & " entity)")
            sb.AppendLine("	{")
            sb.AppendLine("		this.SendPropertyChanging();")
            sb.AppendLine("		entity." & tb.TableSingularize & " = null;")
            sb.AppendLine("	}")
        Next


        sb.AppendLine("	private void Initialize()")
        sb.AppendLine("	{")
        For Each ft In FkTb

            sb.AppendLine("		this._" & ft.RelateTablePluralize & " = new EntitySet<" & ft.RelateTableSingularize & ">(new Action<" & ft.RelateTableSingularize & ">(this.attach_" & ft.RelateTablePluralize & "), new Action<" & ft.RelateTableSingularize & ">(this.detach_" & ft.RelateTablePluralize & "));")
        Next
        For Each mt In masterTables
            sb.AppendLine("    this._" & mt.RelateTableSingularize & " = default(EntityRef<" & mt.RelateTableSingularize & ">);")
        Next
        sb.AppendLine("		OnCreated();")
        sb.AppendLine("	}")

        sb.AppendLine("	[global::System.Runtime.Serialization.OnDeserializingAttribute()]")
        sb.AppendLine("	[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]")
        sb.AppendLine("	public void OnDeserializing(StreamingContext context)")
        sb.AppendLine("	{")
        sb.AppendLine("		this.Initialize();")
        sb.AppendLine("	}")

        sb.AppendLine("	[global::System.Runtime.Serialization.OnSerializingAttribute()]")
        sb.AppendLine("	[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]")
        sb.AppendLine("	public void OnSerializing(StreamingContext context)")
        sb.AppendLine("	{")
        sb.AppendLine("		this.serializing = true;")
        sb.AppendLine("	}")

        sb.AppendLine("	[global::System.Runtime.Serialization.OnSerializedAttribute()]")
        sb.AppendLine("	[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]")
        sb.AppendLine("	public void OnSerialized(StreamingContext context)")
        sb.AppendLine("	{")
        sb.AppendLine("		this.serializing = false;")
        sb.AppendLine("	}")
        sb.AppendLine("}")
        Return sb.ToString
    End Function
End Class
