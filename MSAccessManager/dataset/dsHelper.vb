Imports System.Data.OleDb
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
End Class
