Imports DBExtenderLib
Imports System.Text

Public Class LinkVBClassHelper
    Public Shared Function VB_DataContext(db As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("Imports System.Data.Linq.Mapping")
        sb.AppendLine("Imports System.Data")
        sb.AppendLine("Imports System.ComponentModel")
        sb.AppendLine("Imports System.Data.Linq")
        sb.AppendLine("Imports System.Runtime.Serialization")
        sb.AppendLine("")
        sb.AppendLine("<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:=" & QT(db.DatabaseName) & ")>  _")
        sb.AppendLine(" Partial Public Class  " & db.DatabaseName & "DataContext")
        sb.AppendLine("  	Inherits System.Data.Linq.DataContext")
        sb.AppendLine("")
        sb.AppendLine("    Private Shared Iconnection As IDbConnection = New OleDb.OleDbConnection(My.Settings." & db.DatabaseName & "ConnectionString)")
        sb.AppendLine("   	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()")
        sb.AppendLine("  #Region " & QT("Extensibility Method Definitions"))
        sb.AppendLine("  Partial Private Sub OnCreated()")
        sb.AppendLine("  End Sub")
        sb.AppendLine()
        For Each DT In db.ListTable
            sb.AppendLine(" '  Partial Private Sub Insert" & DT.TableSingularize & "(instance As " & DT.TableSingularize & ")")
            sb.AppendLine(" '  End Sub")
            sb.AppendLine(" '  Partial Private Sub Update" & DT.TableSingularize & "(instance As " & DT.TableSingularize & ")")
            sb.AppendLine(" '  End Sub ")
            sb.AppendLine(" '  Partial Private Sub Delete" & DT.TableSingularize & "(instance As " & DT.TableSingularize & ")")
            sb.AppendLine(" '  End Sub ")
        Next
        sb.AppendLine("  #End Region ")
        sb.AppendLine()
        sb.AppendLine("     	Public Sub New()")
        sb.AppendLine("		       MyBase.New(Iconnection, mappingSource)")
        sb.AppendLine("		       OnCreated")
        sb.AppendLine("          End Sub")
        sb.AppendLine()
        sb.AppendLine("     	Public Sub New(ByVal connection As System.Data.IDbConnection)")
        sb.AppendLine("		       MyBase.New(connection, mappingSource)")
        sb.AppendLine("		       OnCreated")
        sb.AppendLine("          End Sub")
        sb.AppendLine()
        sb.AppendLine()
        For Each DT In db.ListTable
            sb.AppendLine("     Public ReadOnly Property " & DT.TablePluralize & "() As System.Data.Linq.Table(Of " & DT.TableSingularize & ")")
            sb.AppendLine("	        Get")
            sb.AppendLine("  		  Return Me.GetTable(Of " & DT.TableSingularize & ")")
            sb.AppendLine("   	   End Get")
            sb.AppendLine("     End Property")
        Next
        For Each DT In db.ListViews
            sb.AppendLine("  '   Public ReadOnly Property " & DT.ViewPluralize & "() As System.Data.Linq.Table(Of " & DT.ViewSingularize & ")")
            sb.AppendLine("	 '       Get")
            sb.AppendLine("  '		  Return Me.GetTable(Of " & DT.ViewSingularize & ")")
            sb.AppendLine("  ' 	   End Get")
            sb.AppendLine("  '   End Property")
        Next
        sb.AppendLine()
        sb.AppendLine("End Class")
        Return sb.ToString
    End Function
    Public Shared Function VB_Tables(tb As TableNameInfo, db As DatabaseNameInfo, LinkTables As Boolean) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("<Global.System.Data.Linq.Mapping.TableAttribute(Name:=" & QT(tb.TableValue) & "),  _")
        sb.AppendLine(" Global.System.Runtime.Serialization.DataContractAttribute()>  _")
        sb.AppendLine("   Partial Public Class " & tb.TableSingularize)
        sb.AppendLine("	    Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged")
        sb.AppendLine("")
        sb.AppendLine("  	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)")
        sb.AppendLine("")
        sb.AppendLine()
        Dim colPk = (From c In tb.ListColumn Where c.IsPrimary_Key = True Select c).ToList

        For Each pk In colPk
            sb.AppendLine("    Private _" & pk.ColumnValue & " As " & pk.TypeVB)
        Next
        Dim colNoPk = (From c In tb.ListColumn Where c.IsPrimary_Key = False Select c).ToList
        For Each pkn In colNoPk
            sb.AppendLine("    Private _" & pkn.ColumnValue & " As " & pkn.LinqVar)
        Next
        sb.AppendLine()
        Dim FkTb = tb.GetRalationalTables(db)
        Dim masterTables = tb.GetMasterTables(db)

        If LinkTables Then
            For Each R In FkTb
                sb.AppendLine("    Private _" & R.RelateTable.TablePluralize & " As EntitySet(Of " & R.RelateTable.TableSingularize & ")")
            Next
            sb.AppendLine()
            For Each R In masterTables
                sb.AppendLine("    Private _" & R.RelateTable.TableSingularize & " As EntityRef(Of " & R.RelateTable.TableSingularize & ")")
            Next
        End If

        sb.AppendLine()
        sb.AppendLine(" 	Private serializing As Boolean")
        sb.AppendLine("")
        sb.AppendLine("    #Region " & QT(" Extensibility Method Definitions"))
        sb.AppendLine("    Partial Private Sub OnLoaded()")
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        sb.AppendLine("    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)")
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        sb.AppendLine("    Partial Private Sub OnCreated()")
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        For Each R In tb.ListColumn
            sb.AppendLine("    Partial Private Sub On" & R.ColumnValue & "Changing(value As " & R.LinqVar & ")")
            sb.AppendLine("    End Sub")
            sb.AppendLine("    Partial Private Sub On" & R.ColumnValue & "Changed()")
            sb.AppendLine("    End Sub")
        Next
        sb.AppendLine("  #End Region ")
        sb.AppendLine()
        sb.AppendLine("	Public Sub New()")
        sb.AppendLine("		MyBase.New")
        sb.AppendLine("		Me.Initialize")
        sb.AppendLine("	End Sub")
        sb.AppendLine()
        Dim count As Integer = 1
        For Each pk In colPk

            sb.AppendLine(pk.SetColumnAttribut(count))
            sb.AppendLine("	Public Property " & pk.ColumnValue & "() As " & pk.TypeVB)
            sb.AppendLine("		Get")
            sb.AppendLine("			Return Me._" & pk.ColumnValue)
            sb.AppendLine("		End Get")
            sb.AppendLine("		Set")
            '                       If (Me._UnitsInStock.Equals(value) = false) Then
            sb.AppendLine("			" & pk.GetReturnProType)
            sb.AppendLine("				Me.On" & pk.ColumnValue & "Changing(value)")
            sb.AppendLine("				Me.SendPropertyChanging")
            sb.AppendLine("				Me._" & pk.ColumnValue & " = value")
            sb.AppendLine("				Me.SendPropertyChanged(" & QT(pk.ColumnValue) & ")")
            sb.AppendLine("				Me.On" & pk.ColumnValue & "Changed")
            sb.AppendLine("			End If")
            sb.AppendLine("		End Set")
            sb.AppendLine("	End Property")
            count = count + 1
        Next

        For Each R In colNoPk
            sb.AppendLine(R.SetColumnAttribut(count))
            sb.AppendLine("	Public Property " & R.ColumnValue & "() As " & R.LinqVar)
            sb.AppendLine("		Get")
            sb.AppendLine("			Return Me._" & R.ColumnValue)
            sb.AppendLine("		End Get")
            sb.AppendLine("		Set")
            '                       If (Me._UnitsInStock.Equals(value) = false) Then
            sb.AppendLine("			" & R.GetReturnProType)
            sb.AppendLine("				Me.On" & R.ColumnValue & "Changing(value)")
            sb.AppendLine("				Me.SendPropertyChanging")
            sb.AppendLine("				Me._" & R.ColumnValue & " = value")
            sb.AppendLine("				Me.SendPropertyChanged(" & QT(R.ColumnValue) & ")")
            sb.AppendLine("				Me.On" & R.ColumnValue & "Changed")
            sb.AppendLine("			End If")
            sb.AppendLine("		End Set")
            sb.AppendLine("	End Property")
            count = count + 1
        Next
        sb.AppendLine()
       
        If LinkTables Then
            For Each R In FkTb
                Dim rt = R
                sb.AppendLine(R.SetColumnAttributDetails(tb.TableValue, count))
                sb.AppendLine("   Public Property " & R.RelateTablePluralize & "() As EntitySet(Of  " & R.RelateTableSingularize & ")")
                sb.AppendLine("    Get")
                sb.AppendLine("        If (Me.serializing _")
                sb.AppendLine("           AndAlso (Me._" & R.RelateTablePluralize & ".HasLoadedOrAssignedValues = False)) Then")
                sb.AppendLine("            Return Nothing")
                sb.AppendLine("        End If")
                sb.AppendLine("        Return Me._" & R.RelateTablePluralize)
                sb.AppendLine("    End Get")
                sb.AppendLine("    Set(value As EntitySet(Of " & R.RelateTableSingularize & "))")
                sb.AppendLine("        Me._" & R.RelateTablePluralize & ".Assign(value)")
                sb.AppendLine("    End Set")
                sb.AppendLine("   End Property")
                count = count + 1
            Next
            sb.AppendLine()
            For Each R In masterTables
                sb.AppendLine(R.SetColumnAttributFKey(tb.TableValue, count))
                sb.AppendLine("   Public Property " & R.RelateTableSingularize & "() As " & R.RelateTableSingularize)
                sb.AppendLine("    Get")
                sb.AppendLine("        Return Me._" & R.RelateTableSingularize & ".Entity")
                sb.AppendLine("    End Get")
                sb.AppendLine("    Set(value As " & R.RelateTableSingularize & ")")
                sb.AppendLine("        Dim previousValue As " & R.RelateTableSingularize & " = Me._" & R.RelateTableSingularize & ".Entity")
                sb.AppendLine("        If ((Object.Equals(previousValue, value) = False) _")
                sb.AppendLine("           OrElse (Me._" & R.RelateTableSingularize & ".HasLoadedOrAssignedValue = False)) Then")
                sb.AppendLine("            Me.SendPropertyChanging()")
                sb.AppendLine("            If ((previousValue Is Nothing) _")
                sb.AppendLine("               = False) Then")
                sb.AppendLine("                Me._" & R.RelateTableSingularize & ".Entity = Nothing")
                sb.AppendLine("                previousValue." & tb.TablePluralize & ".Remove(Me)")
                sb.AppendLine("            End If")
                sb.AppendLine("            Me._" & R.RelateTableSingularize & ".Entity = value")
                sb.AppendLine("            If ((value Is Nothing) _")
                sb.AppendLine("               = False) Then")
                sb.AppendLine("                value." & tb.TablePluralize & ".Add(Me)")
                sb.AppendLine("                Me._" & R.ForeignKey & "  = value." & R.SelectedTableLinqKey)
                sb.AppendLine("            Else")
                sb.AppendLine("                Me._" & R.ForeignKey & " = CType(Nothing, " & R.ColumnTypeVB & ")")
                sb.AppendLine("            End If")
                sb.AppendLine("            Me.SendPropertyChanged(" & QT(R.RelateTableValue) & ")")
                sb.AppendLine("        End If")
                sb.AppendLine("    End Set")
                sb.AppendLine("  End Property")
                count = count + 1
            Next

        End If


        sb.AppendLine()
        sb.AppendLine("  	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging")
        sb.AppendLine()
        sb.AppendLine("  	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged")
        sb.AppendLine()
        sb.AppendLine("	Protected Overridable Sub SendPropertyChanging()")
        sb.AppendLine("		If ((Me.PropertyChangingEvent Is Nothing)  _")
        sb.AppendLine("					= false) Then ")
        sb.AppendLine("			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)")
        sb.AppendLine("		End If")
        sb.AppendLine("  End Sub")
        sb.AppendLine()
        sb.AppendLine("	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])")
        sb.AppendLine("		If ((Me.PropertyChangedEvent Is Nothing)  _")
        sb.AppendLine("					= false) Then ")
        sb.AppendLine("			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))")
        sb.AppendLine("		End If")
        sb.AppendLine("	End Sub")
        sb.AppendLine()
        If LinkTables Then
            For Each ft In FkTb
                sb.AppendLine("	Private Sub attach_" & ft.RelateTableValue & "(ByVal entity As " & ft.RelateTableSingularize & ")")
                sb.AppendLine("		Me.SendPropertyChanging")
                sb.AppendLine("		entity." & tb.TableSingularize & " = Me")
                sb.AppendLine("	End Sub")

                sb.AppendLine("	Private Sub detach_" & ft.RelateTableValue & "(ByVal entity As " & ft.RelateTableSingularize & ")")
                sb.AppendLine("		Me.SendPropertyChanging")
                sb.AppendLine("		entity." & tb.TableSingularize & " = Nothing")
                sb.AppendLine("	End Sub")
            Next
            sb.AppendLine()
        End If


        sb.AppendLine("	Private Sub Initialize()")
        If LinkTables Then
            For Each ft In FkTb
                sb.AppendLine("		Me._" & ft.RelateTablePluralize & " = New EntitySet(Of " & ft.RelateTableSingularize & ")(AddressOf Me.attach_" & ft.RelateTablePluralize & ", AddressOf Me.detach_" & ft.RelateTablePluralize & ")")
            Next
            For Each mt In masterTables
                sb.AppendLine("			Me._" & mt.RelateTableSingularize & " = CType(Nothing, EntityRef(Of " & mt.RelateTableSingularize & "))")
            Next
        End If

        sb.AppendLine("		OnCreated()")
        sb.AppendLine("	End Sub")
        sb.AppendLine()

        sb.AppendLine("	<Global.System.Runtime.Serialization.OnDeserializingAttribute(),  _ ")
        sb.AppendLine("	 Global.System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)>  _")
        sb.AppendLine("	Public Sub OnDeserializing(ByVal context As StreamingContext)")
        sb.AppendLine("		Me.Initialize ")
        sb.AppendLine("	End Sub")
        sb.AppendLine()
        sb.AppendLine("	<Global.System.Runtime.Serialization.OnSerializingAttribute(),  _ ")
        sb.AppendLine("	 Global.System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)>  _")
        sb.AppendLine("	Public Sub OnSerializing(ByVal context As StreamingContext) ")
        sb.AppendLine("		Me.serializing = true")
        sb.AppendLine("	End Sub")
        sb.AppendLine()
        sb.AppendLine("	<Global.System.Runtime.Serialization.OnSerializedAttribute(),  _ ")
        sb.AppendLine("	 Global.System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)>  _")
        sb.AppendLine("	Public Sub OnSerialized(ByVal context As StreamingContext)")
        sb.AppendLine("		Me.serializing = false")
        sb.AppendLine("	End Sub")
        sb.AppendLine()
        sb.AppendLine("End Class")
        Return sb.ToString
    End Function
End Class
