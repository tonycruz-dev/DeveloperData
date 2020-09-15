Imports System.Configuration
Imports System.Data.SqlServerCe
Public Class dsHelper


    Public Shared Function GetDataSet(ByVal sqlstr As String, ByVal strcnn As String) As DataSet
        Dim cnn As New SqlClient.SqlConnection(strcnn)
        cnn.Open()
        Dim Ad As New SqlClient.SqlDataAdapter
        Dim cmd As New SqlClient.SqlCommand(sqlstr, cnn)
        Dim ds As DataSet = New DataSet()
        Ad.SelectCommand = cmd
        Ad.Fill(ds)
        Return ds
    End Function
    Public Shared Function GetCompactDataTable(ByVal commandText As String, ByVal connectionString As String) As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlCeConnection(connectionString)
            cn.Open()
            Using cmd As New SqlCeCommand(commandText, cn)
                Using da As New SqlCeDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function


End Class
