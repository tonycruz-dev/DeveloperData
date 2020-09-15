<Serializable()> Partial Public Class VBServerManager
    Private Shared oModel As VBServerManager
    Private Sub New()
    End Sub
    Public Shared Function GetInstance() As VBServerManager
        If oModel Is Nothing Then
            oModel = New VBServerManager
        End If
        Return oModel
    End Function
    Public Property DatabaseList As New List(Of DatabaseNameInfo)

    Public Property PathSaved As String

End Class
