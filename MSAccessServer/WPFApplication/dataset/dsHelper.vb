Imports System.Data.OleDb
Imports System.Data
Imports DBExtenderLib
Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.Data.SqlClient

Public Class dsHelper

    Public Shared Function GetDataSet(ByVal sqlstr As String, ByVal strcnn As String) As DataSet
        Dim cnn As New OleDbConnection(strcnn)
        cnn.Open()
        Dim Ad As New OleDbDataAdapter
        Dim cmd As New OleDbCommand(sqlstr, cnn)
        Dim ds As DataSet = New DataSet()
        Ad.SelectCommand = cmd
        Ad.Fill(ds)
        Return ds
    End Function
    Public Shared Function GetSQLDataSet(ByVal strcnn As String, ByVal sqlstr As String) As DataSet
        Dim cnn As New SqlConnection(strcnn)
        cnn.Open()
        Dim Ad As New SqlDataAdapter
        Dim cmd As New SqlCommand(sqlstr, cnn)
        Dim ds As DataSet = New DataSet()
        Ad.SelectCommand = cmd
        Ad.Fill(ds)
        Return ds
    End Function
End Class
Public Module TbTxtention
    <Extension()> _
    Public Function GetSelectTable(ByVal DT As TableNameInfo) As String
        Dim sbSelect As New StringBuilder("SELECT ")
        For Each col In DT.ListColumn
            sbSelect.Append("[" & col.ColumnName & "],")
        Next
        Dim mylastComar = sbSelect.ToString.LastIndexOf(",")
        Dim StrSelect = sbSelect.Remove(mylastComar, 1).ToString

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        If Not colPk Is Nothing Then
            StrSelect &= " From [" & DT.TableName & "] Order By [" & colPk.ColumnName & "]"
        Else
            StrSelect &= " From [" & DT.TableName & "]"
        End If
        Return StrSelect
    End Function
End Module
