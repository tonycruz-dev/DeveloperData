Imports System.Text
Imports DBExtenderLib

Public Class LinqHelperClass


    Public Shared Function WriteLinqVar(ByVal DT As TableNameInfo, ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        For Each R In DT.ListColumn
            strWriteModel.AppendLine("    Private _" & R.ColumnValue & " As " & R.LinqVar)
        Next

        Dim Entityset = (From tr In linqDb.RelateTables Where tr.RelatedTableValue = DT.TableValue).ToList
        Dim EntityRef = (From tr In linqDb.RelateTables Where tr.TableValue = DT.TableValue).ToList

        For Each R In Entityset
            strWriteModel.AppendLine("    Private _" & R.TableValue & " As EntitySet(Of " & R.TableValue & "Info)")
        Next

        For Each R In EntityRef
            strWriteModel.AppendLine("    Private _" & R.RelatedTableValue & " As EntityRef(Of " & R.RelatedTableValue & "Info)")
        Next
        Return strWriteModel.ToString
    End Function
    Public Shared Function WriteLinqInitialize(ByVal DT As TableNameInfo, ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim sb As New StringBuilder
        Dim Entityset = (From tr In linqDb.RelateTables Where tr.RelatedTableValue = DT.TableValue).ToList
        Dim EntityRef = (From tr In linqDb.RelateTables Where tr.TableValue = DT.TableValue).ToList
        sb.AppendLine("Private Sub Initialize()")
        For Each R In Entityset
            sb.AppendLine("	Me._" & R.TableValue & "= New EntitySet(Of " & R.TableValue & "Info)(AddressOf Me.attach_" & R.TableValue & ", AddressOf Me.detach_" & R.TableValue & ")")
        Next
        For Each R In EntityRef
            sb.AppendLine("    Me._" & R.RelatedTableValue & " = CType(Nothing, EntityRef(Of " & R.RelatedTableValue & "Info))")
        Next
        sb.AppendLine(" OnCreated")
        sb.AppendLine(" End Sub")
        Return sb.ToString
    End Function

    '[LINQ_ATTACH_AND_DETACH_ENTITYSETPROPERTIES]
    Public Shared Function Linq_Attach_and_Detach_Properties(ByVal DT As TableNameInfo, ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim sb As New StringBuilder
        Dim Entityset = (From tr In linqDb.RelateTables Where tr.RelatedTableValue = DT.TableValue).ToList
        For Each R In Entityset
            sb.AppendLine(" Private Sub attach_" & R.TableValue & "(ByVal entity As " & R.TableValue & "Info)")
            sb.AppendLine("	     Me.SendPropertyChanging")
            sb.AppendLine("   	 entity." & R.RelatedTableValue & " = Me")
            sb.AppendLine(" End Sub")
            sb.AppendLine()
            sb.AppendLine(" Private Sub detach_" & R.TableValue & "(ByVal entity As " & R.TableValue & "Info)")
            sb.AppendLine("	   Me.SendPropertyChanging ")
            sb.AppendLine("       entity." & R.RelatedTableValue & " = Nothing")
            sb.AppendLine(" End Sub")
        Next
        Return sb.ToString
    End Function



    ' #Region "Extensibility Columns Method Definitions"
    Public Shared Function Extensibility_Columns_Method_Definitions(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        For Each R In DT.ListColumn
            sb.AppendLine("    Partial Private Sub On" & R.ColumnValue & "Changing(ByVal value As " & R.LinqVar & ")")
            sb.AppendLine("    End Sub")
            sb.AppendLine("    Partial Private Sub On" & R.ColumnValue & "Changed()")
            sb.AppendLine("    End Sub")
        Next

        Return sb.ToString
    End Function

    '[Extensibility_Method_Definitions]
    Public Shared Function Extensibility_Method_Definitions(ByVal db As LinqDatabaseNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        For Each DT In db.ListLinqTable
            strWriteModel.AppendLine("   Partial Private Sub Insert" & DT.TableValue & "(instance As " & DT.TableValue & "Info)")
            strWriteModel.AppendLine("   End Sub")
            strWriteModel.AppendLine("   Partial Private Sub Update" & DT.TableValue & "(instance As " & DT.TableValue & "Info)")
            strWriteModel.AppendLine("   End Sub ")
            strWriteModel.AppendLine("   Partial Private Sub Delete" & DT.TableValue & "(instance As " & DT.TableValue & "Info)")
            strWriteModel.AppendLine("   End Sub ")
        Next
        Return strWriteModel.ToString
    End Function

    Public Shared Function DataContextPropertyTables(ByVal db As LinqDatabaseNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        For Each DT In db.ListLinqTable
            strWriteModel.AppendLine("   Public ReadOnly Property " & DT.TableValue & "() As System.Data.Linq.Table(Of " & DT.TableValue & "Info)")
            strWriteModel.AppendLine("	       Get")
            strWriteModel.AppendLine("   		  Return Me.GetTable(Of " & DT.TableValue & "Info)")
            strWriteModel.AppendLine("   	   End Get")
            strWriteModel.AppendLine("      End Property")
        Next
        Return strWriteModel.ToString
    End Function

#Region "WriteLinqDataColumnsProperties"
    Public Shared Function WriteLinqDataColumnsProperties(ByVal DT As TableNameInfo, ByVal linqDb As LinqDatabaseNameInfo) As String
        Dim strWriteModel As New StringBuilder()
        Dim count As Integer = 1
        Dim Entityset = (From tr In linqDb.RelateTables Where tr.RelatedTableValue = DT.TableValue).ToList
        Dim EntityRef = (From tr In linqDb.RelateTables Where tr.TableValue = DT.TableValue).ToList


        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.Append(SetColumnAttribut(R, count))
            strWriteModel.AppendLine("    Public Property " & R.ColumnValue & "() As " & R.LinqVar)
            strWriteModel.AppendLine("        Get")
            strWriteModel.AppendLine("            Return _" & R.ColumnValue)
            strWriteModel.AppendLine("        End Get")
            strWriteModel.AppendLine("        Set(ByVal Value As " & R.LinqVar & ")")
            strWriteModel.AppendLine("               Me.On" & R.ColumnValue & "Changing(Value)")
            strWriteModel.AppendLine("               Me.SendPropertyChanging()")
            strWriteModel.AppendLine("               _" & R.ColumnValue & " = Value")
            strWriteModel.AppendLine("               Me.SendPropertyChanged(""" & R.ColumnValue & """" & ")")
            strWriteModel.AppendLine("               On" & R.ColumnValue & "Changed()")
            strWriteModel.AppendLine("        End Set")
            strWriteModel.AppendLine("    End Property")
            count += 1
        Next
        For Each R In Entityset
            strWriteModel.AppendLine(CreatePropertyEntitySet(R, count))
            count += 1
        Next

        For Each R In EntityRef
            strWriteModel.AppendLine(CreatePropertyEntityRef(R))
        Next

        Return strWriteModel.ToString
    End Function
#End Region


#Region "List of Help fuctions"
    Friend Shared Function SetColumnAttribut(ByVal col As ColumnsInfo, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" <Global.System.Data.Linq.Mapping.ColumnAttribute(Name:=" & """" & col.ColumnName & """" & ", Storage:=" & """" & "_" &
                      col.ColumnValue & """" & ", DbType:=" &
                      GetSQLType(col) & GetColumnnIsPrimaryKey(col) & GetColumnnCanBeNull(col) & "),")
        sb.AppendLine(" Global.System.Runtime.Serialization.DataMemberAttribute(Order:=" & ColNumber & ")> ")
        Return sb.ToString
    End Function
   
    Friend Shared Function SetColumnAttribut(ByVal col As ClassAssociation) As String
        Dim sb As New StringBuilder()
        If col.IsForeignKey Then
            sb.AppendLine("<Association(Name:=" & """" & col.NameAssociation & """" & ", Storage:=_" & GetColumnnName(col.Storage) & ", ThisKey:=""" & col.ThisKey & """" & ", OtherKey:=""" & col.OtherKey & """" & ", IsForeignKey:=True)> _ ")
        Else
            sb.AppendLine("<Association(Name:=" & """" & col.NameAssociation & """" & ", Storage:=_" & GetColumnnName(col.Storage) & ", ThisKey:=""" & col.ThisKey & """" & ", OtherKey:=""" & col.OtherKey & """" & ")> _ ")
        End If

        Return sb.ToString
    End Function
    Friend Shared Function GetColumnnName(ByVal ColumnName As String) As String
        Return """" & ColumnName & """"
    End Function
    Friend Shared Function GetColumnnName(ByVal col As ColumnsInfo) As String
        Return """" & col.ColumnName & """"
    End Function
    Friend Shared Function GetColumnnIsPrimaryKey(ByVal col As ColumnsInfo) As String
        If col.IsPrimary_Key And col.IsAutoincrement Then
            Return ",IsPrimaryKey:=True, IsDbGenerated:=True"
        ElseIf col.IsPrimary_Key Then
            Return ",IsPrimaryKey:=True"
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
            Return ",CanBeNull:=false"
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

    Friend Shared Function GetVBDataType(ByVal typeName As String) As String
        Select Case typeName
            Case "numeric", "int"
                Return "Integer"
            Case "nvarchar", "varchar", "ntext", "text"
                Return "String"
            Case "decimal"
                Return "Decimal"
            Case "money"
                Return "Decimal"
            Case "smallint"
                Return "Int16"
            Case "datetime"
                Return "Date"
            Case "byte", "image"
                Return "Byte()"
            Case "bit"
                Return "Boolean"
            Case "char"
                Return "String"
            Case "float"
                Return "Double"
            Case Else
                'Add other missing data types
        End Select
        Return "String"
    End Function


#End Region

   
#End Region


#Region "Customers List"
    Private Shared Function CreatePropertyEntitySet(ByVal Ent As LinqReatedTables, ByVal ColNumber As Integer) As String
        Dim sb As New StringBuilder()
        '<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Order_Order_Detail", Storage:="_Order_Details", ThisKey:="OrderID", OtherKey:="OrderID"),  _
        sb.Append("<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:=" & """" & Ent.ForeignKeyNameValue & """" & ", Storage:=" & """" & "_")
        sb.AppendLine(Ent.TableValue & """" & " , ThisKey:=" & """" & Ent.RelatedColumnName & """" & ", OtherKey:=" & """" & Ent.ColumnName & """" & "), ")
        sb.AppendLine("Global.System.Runtime.Serialization.DataMemberAttribute(Order:=" & ColNumber & ", EmitDefaultValue:=false)> ")
        sb.AppendLine("   Public Property " & Ent.TableValue & " () As EntitySet(Of " & Ent.TableValue & "Info)")
        sb.AppendLine("	  Get ")
        sb.AppendLine("		If (Me.serializing 	AndAlso (Me._" & Ent.TableValue & ".HasLoadedOrAssignedValues = false)) Then")
        sb.AppendLine("			Return Nothing")
        sb.AppendLine("		End If")
        sb.AppendLine("		Return Me._" & Ent.TableValue)
        sb.AppendLine("	  End Get")
        sb.AppendLine("   Set(ByVal value As EntitySet(Of " & Ent.TableValue & "Info))")
        sb.AppendLine("    Me._" & Ent.TableValue & ".Assign(value)")
        sb.AppendLine("  End Set")
        sb.AppendLine("End Property")
        Return sb.ToString
    End Function

    Private Shared Function CreatePropertyEntityRef(ByVal Ent As LinqReatedTables) As String
        Dim sb As New StringBuilder()
        '<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Order_Order_Detail", Storage:="_Order_Details", ThisKey:="OrderID", OtherKey:="OrderID"),  _

        sb.Append("<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:=" & """" & Ent.ForeignKeyNameValue & """" & ", Storage:=" & """" & "_")
        sb.AppendLine(Ent.RelatedTableValue & """" & " , ThisKey:=" & """" & Ent.ColumnName & """" & ", OtherKey:=" & """" & Ent.RelatedColumnName & """" & ")> ")
        sb.AppendLine("Public Property " & Ent.RelatedTableValue & " () As " & Ent.RelatedTableValue & "Info")
        sb.AppendLine("   Get")
        sb.AppendLine("		Return Me._" & Ent.RelatedTableValue & ".Entity")
        sb.AppendLine("	End Get")
        sb.AppendLine("	Set ")
        sb.AppendLine("		Dim previousValue As " & Ent.RelatedTableValue & "Info = Me._" & Ent.RelatedTableValue & ".Entity ")
        sb.AppendLine("		If ((Object.Equals(previousValue, value) = false)  _ ")
        sb.AppendLine("					OrElse (Me._" & Ent.RelatedTableValue & ".HasLoadedOrAssignedValue = false)) Then ")
        sb.AppendLine("			Me.SendPropertyChanging ")
        sb.AppendLine("			If ((previousValue Is Nothing)  _")
        sb.AppendLine("						= false) Then ")
        sb.AppendLine("				Me._" & Ent.RelatedTableValue & ".Entity = Nothing ")
        sb.AppendLine("				previousValue." & Ent.TableValue & ".Remove(Me) ")
        sb.AppendLine("			End If")
        sb.AppendLine("			Me._" & Ent.RelatedTableValue & ".Entity = value ")
        sb.AppendLine("			If ((value Is Nothing)  _")
        sb.AppendLine("						= false) Then")
        sb.AppendLine("				value." & Ent.TableValue & ".Add(Me)")
        sb.AppendLine("				Me._" & Ent.ColumnValue & " = value." & Ent.RelatedColumnValue)
        sb.AppendLine("			Else")
        If Ent.RelateColumn.LinqVar = "System.Nullable(Of Integer)" Then
            sb.AppendLine("				Me._" & Ent.ColumnValue & " = CType(Nothing, Nullable(Of Integer))")
        Else
            sb.AppendLine("				Me._" & Ent.ColumnValue & " = CType(Nothing, String)")

        End If

        sb.AppendLine("			End If ")
        sb.AppendLine("			Me.SendPropertyChanged(" & """" & Ent.RelatedTableValue & """" & ")")
        sb.AppendLine("		End If")
        sb.AppendLine("	End Set")
        sb.AppendLine("End Property")

        Return sb.ToString
    End Function
#End Region
End Class
