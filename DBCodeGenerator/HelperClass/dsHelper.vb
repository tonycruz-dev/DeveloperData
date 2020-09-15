Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class dsHelper

    Public Shared Function GetDataSet(ByVal strcnn As String, ByVal sqlstr As String) As DataSet

        'Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Access databases\Northwind.accdb
        'Data Source=TONYCRUZ-PC\SQLEXPRESS2008;Initial Catalog=Northwind;Integrated Security=True
        'Data Source=TONYCRUZ-PC\SQLEXPRESS2008;AttachDbFilename=E:\Access databases\NORTHWND.MDF;Integrated Security=True;User Instance=True

        If strcnn.Contains("Microsoft.ACE.OLEDB.12.0") Then

            Dim cnn As New OleDbConnection(strcnn)
            cnn.Open()
            Dim Ad As New OleDbDataAdapter
            Dim cmd As New OleDbCommand(sqlstr, cnn)
            Dim ds As DataSet = New DataSet()
            Ad.SelectCommand = cmd
            Ad.Fill(ds)
            Return ds
        Else
            Dim cnn As New SqlConnection(strcnn)
            cnn.Open()
            Dim Ad As New SqlDataAdapter
            Dim cmd As New SqlCommand(sqlstr, cnn)
            Dim ds As DataSet = New DataSet()
            Ad.SelectCommand = cmd
            Ad.Fill(ds)
            Return ds
        End If

    End Function
    Public Shared Function GetDataSet(ByVal strcnn As String, ByVal sqlstr As String, dbName As String, tableName As String) As DataSet

        'Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Access databases\Northwind.accdb
        'Data Source=TONYCRUZ-PC\SQLEXPRESS2008;Initial Catalog=Northwind;Integrated Security=True
        'Data Source=TONYCRUZ-PC\SQLEXPRESS2008;AttachDbFilename=E:\Access databases\NORTHWND.MDF;Integrated Security=True;User Instance=True

        If strcnn.Contains("Microsoft.ACE.OLEDB.12.0") Then

            Dim cnn As New OleDbConnection(strcnn)
            cnn.Open()
            Dim Ad As New OleDbDataAdapter
            Dim cmd As New OleDbCommand(sqlstr, cnn)
            Dim ds As DataSet = New DataSet()
            Ad.SelectCommand = cmd
            Ad.Fill(ds)
            Return ds
        Else
            Dim cnn As New SqlConnection(strcnn)
            cnn.Open()
            Dim Ad As New SqlDataAdapter
            Dim cmd As New SqlCommand(sqlstr, cnn)
            Dim ds As DataSet = New DataSet(dbName)
            Ad.SelectCommand = cmd
            Ad.Fill(ds)
            ds.Tables(0).TableName = tableName
            Return ds
        End If

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
    'Public Shared Function GetSQLCompactDataSet(ByVal strcnn As String, ByVal sqlstr As String) As DataSet
    '    Dim cnn As New SqlCeConnection(strcnn)
    '    cnn.Open()
    '    Dim Ad As New SqlCeDataAdapter
    '    Dim cmd As New SqlCeCommand(sqlstr, cnn)
    '    Dim ds As DataSet = New DataSet()
    '    Ad.SelectCommand = cmd
    '    Ad.Fill(ds)
    '    Return ds
    'End Function

End Class
