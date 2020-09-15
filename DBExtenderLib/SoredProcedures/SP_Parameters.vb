Partial Public Class SP_Parameters

    Public Property ParamiterID() As Integer

    Public Property Paramiter_DataType() As String

    Public Property ParamMaxLenght() As Integer

    Public Property ParamiterName() As String

    Public Overloads Function ToString() As String
        Return _ParamiterName
    End Function
End Class
