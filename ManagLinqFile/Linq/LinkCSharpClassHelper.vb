Imports DBExtenderLib
Imports System.Text

Public Class LinkCSharpClassHelper
    Public Shared Function CS_DataContext(db As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("using System.Data;")
        sb.AppendLine("using System.Collections.Generic;")
        sb.AppendLine("using System.Reflection;")
        sb.AppendLine("using System.Linq;")
        sb.AppendLine("using System.Linq.Expressions;")
        sb.AppendLine("using System.Runtime.Serialization;")
        sb.AppendLine("using System.ComponentModel;")
        sb.AppendLine("using System.Data.Linq.Mapping;")
        sb.AppendLine("using System.Data.Linq;")
        sb.AppendLine("using System;")
        sb.AppendLine("")

        sb.AppendLine("[global::System.Data.Linq.Mapping.DatabaseAttribute(Name=" & QT("Northwind") & ")]")
        sb.AppendLine(" public partial class " & db.DatabaseName & "DBDataContext : System.Data.Linq.DataContext")
        sb.AppendLine(" {")

        sb.AppendLine("    private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();")
        sb.AppendLine("    private static IDbConnection Iconnection = new System.Data.OleDb.OleDbConnection(ApplicationName.Properties.Settings.Default.." & db.DatabaseName & "ConnectionString);")

        sb.AppendLine("   #region Extensibility Method Definitions")
        sb.AppendLine("   partial void OnCreated();")
        For Each tb In db.ListTable

            sb.AppendLine("   partial void Insert" & tb.TableSingularize & "(" & tb.TableSingularize & " instance);")
            sb.AppendLine("   partial void Update" & tb.TableSingularize & "(" & tb.TableSingularize & " instance);")
            sb.AppendLine("   partial void Delete" & tb.TableSingularize & "(" & tb.TableSingularize & " instance);")

        Next
        sb.AppendLine("#endregion")
        sb.AppendLine("	public " & db.DatabaseName & "DBDataContext() :")
        sb.AppendLine("       base(Iconnection, mappingSource)")
        sb.AppendLine("	{")
        sb.AppendLine("		OnCreated();")
        sb.AppendLine("	}")


        For Each tb In db.ListTable
            sb.AppendLine("  	public System.Data.Linq.Table<" & tb.TableSingularize & "> " & tb.TablePluralize)
            sb.AppendLine("	{")
            sb.AppendLine("		get")
            sb.AppendLine("		{")
            sb.AppendLine("			return this.GetTable<" & tb.TableSingularize & ">();")
            sb.AppendLine("		}")
            sb.AppendLine(" }")
        Next
        sb.AppendLine("	}")
        Return sb.ToString


    End Function
    Public Shared Function CS_Tables(tb As TableNameInfo, db As DatabaseNameInfo, LinkTables As Boolean) As String

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
        If LinkTables Then
            For Each ft In FkTb
                sb.AppendLine("	private EntitySet<" & ft.RelateTable.TableSingularize & "> _" & ft.RelateTable.TablePluralize & ";")
            Next

            For Each mt In masterTables
                sb.AppendLine("	private EntityRef<" & mt.RelateTable.TableSingularize & "> _" & mt.RelateTable.TableSingularize & ";")

            Next
        End If


        sb.AppendLine("	  private bool serializing;")
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
        If LinkTables Then
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
                sb.AppendLine(SetColumnMasterAttribut(tb.TableValue, mt, count))
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
                sb.AppendLine("				this._" & mt.ForeignKey & " = value." & mt.SelectedTableLinqKey & ";")
                sb.AppendLine("			}")
                sb.AppendLine("              else")
                sb.AppendLine("			{")
                sb.AppendLine("				this._" & mt.ForeignKey & " = default(" & mt.ColumnType & ");")
                sb.AppendLine("			}")
                sb.AppendLine("			this.SendPropertyChanged(" & QT(mt.RelateTableSingularize) & ");")
                sb.AppendLine("		 }")
                sb.AppendLine(" 	}")
                sb.AppendLine(" }")
            Next
        End If


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
        If LinkTables Then
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
        End If



        sb.AppendLine("	private void Initialize()")
        sb.AppendLine("	{")
        If LinkTables Then
            For Each ft In FkTb

                sb.AppendLine("		this._" & ft.RelateTablePluralize & " = new EntitySet<" & ft.RelateTableSingularize & ">(new Action<" & ft.RelateTableSingularize & ">(this.attach_" & ft.RelateTablePluralize & "), new Action<" & ft.RelateTableSingularize & ">(this.detach_" & ft.RelateTablePluralize & "));")
            Next
            For Each mt In masterTables
                sb.AppendLine("    this._" & mt.RelateTableSingularize & " = default(EntityRef<" & mt.RelateTableSingularize & ">);")
            Next
        End If

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
    Friend Shared Function SetColumnAttribut(ByVal col As ColumnsInfo, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        ' '	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]

        sb.AppendLine(" [global::System.Data.Linq.Mapping.ColumnAttribute(Name=" & col.ColumnName.QT & ", Storage=" & """" & "_" &
                      col.ColumnValue & """" & ", DbType=" &
                      GetSQLType(col) & GetColumnnIsCSPrimaryKey(col) & GetColumnnCSCanBeNull(col) & ")]")
        sb.AppendLine(" [global::System.Runtime.Serialization.DataMemberAttribute(Order=" & ColNumber & ")] ")
        Return sb.ToString
    End Function

    Friend Shared Function SetColumnAttribut(ByVal tbName As String, relatedTable As RelationalTable, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("[global::System.Data.Linq.Mapping.AssociationAttribute(Name=" & QT(tbName & "_" & relatedTable.RelateTableValue) & ", Storage=" & QT("_" & relatedTable.RelateTablePluralize) & ", ThisKey=" & QT(relatedTable.ForeignKey) & ", OtherKey=" & QT(relatedTable.SelectedTableLinqKey) & ")]")
        sb.AppendLine("[global::System.Runtime.Serialization.DataMemberAttribute(Order=" & ColNumber & ", EmitDefaultValue=false)]")


        Return sb.ToString
    End Function
    Friend Shared Function SetColumnMasterAttribut(ByVal tbName As String, relatedTable As RelationalTable, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("[global::System.Data.Linq.Mapping.AssociationAttribute(Name=" & QT(tbName & "_" & relatedTable.RelateTableValue) & ", Storage=" & QT("_" & relatedTable.RelateTableSingularize) & ", ThisKey=" & QT(relatedTable.ForeignKey) & ", OtherKey=" & QT(relatedTable.SelectedTableLinqKey) & ")]")
        sb.AppendLine("[global::System.Runtime.Serialization.DataMemberAttribute(Order=" & ColNumber & ", EmitDefaultValue=false)]")


        Return sb.ToString
    End Function
    Friend Shared Function GetColumnnName(ByVal ColumnName As String) As String
        Return ColumnName.QT
    End Function
    Friend Shared Function GetColumnnName(ByVal col As ColumnsInfo) As String
        Return col.ColumnName.QT
    End Function
End Class
