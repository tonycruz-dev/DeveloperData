Imports System.Text
Imports DBExtenderLib

Public Class PhoneDatabase
    Public Shared Function VBClassPhoneTable(ByVal DT As TableNameInfo) As String

        Dim strWriteModel As New StringBuilder()
        strWriteModel.AppendLine(" #Region " & DT.TableName.Dquotes)
        strWriteModel.AppendLine(" <Table()> ")
        strWriteModel.AppendLine("Public  Class " & DT.TableValue)

        strWriteModel.AppendLine("    Implements INotifyPropertyChanging, INotifyPropertyChanged")
        strWriteModel.AppendLine("    Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)")

        strWriteModel.AppendLine("")
        For Each R As ColumnsInfo In DT.ListColumn
            strWriteModel.AppendLine("    Private " & "_" & R.ColumnValue & " As " & R.LinqVar)
        Next
        strWriteModel.AppendLine("")




        Dim count As Integer = 0
        For Each R As ColumnsInfo In DT.ListColumn
            If R.IsPrimary_Key Then
                strWriteModel.AppendLine(R.SetColumnAttribut(count))
                strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.LinqVar)
                strWriteModel.AppendLine("         Get")
                strWriteModel.AppendLine("            Return Me._" & R.ColumnValue)
                strWriteModel.AppendLine("        End Get")
                strWriteModel.AppendLine("        Set(value As " & R.LinqVar & ")")
                strWriteModel.AppendLine("            If (Me._" & R.ColumnValue & ".Equals(value) = False) Then")
                strWriteModel.AppendLine("                Me.NotifyPropertyChanging(" & R.ColumnValue.Dquotes & ")")
                strWriteModel.AppendLine("                Me._" & R.ColumnValue & " = value")
                strWriteModel.AppendLine("                Me.NotifyPropertyChanged(" & R.ColumnValue.Dquotes & ")")
                strWriteModel.AppendLine("            End If")
                strWriteModel.AppendLine("        End Set")
                strWriteModel.AppendLine("    End Property")
                count = count + 1
            Else
                If R.IsRequared Then
                    strWriteModel.AppendLine(R.SetColumnAttribut(count))
                    strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.LinqVar)
                    strWriteModel.AppendLine("         Get")
                    strWriteModel.AppendLine("            Return Me._" & R.ColumnValue)
                    strWriteModel.AppendLine("        End Get")
                    strWriteModel.AppendLine("        Set(value As " & R.LinqVar & ")")
                    strWriteModel.AppendLine("            If (Me._" & R.ColumnValue & ".Equals(value) = False) Then")
                    strWriteModel.AppendLine("                Me.NotifyPropertyChanging(" & R.ColumnValue.Dquotes & ")")
                    strWriteModel.AppendLine("                Me._" & R.ColumnValue & " = value")
                    strWriteModel.AppendLine("                Me.NotifyPropertyChanged(" & R.ColumnValue.Dquotes & ")")
                    strWriteModel.AppendLine("            End If")
                    strWriteModel.AppendLine("        End Set")
                    strWriteModel.AppendLine("    End Property")
                Else
                    strWriteModel.AppendLine(R.SetColumnAttribut(count))
                    strWriteModel.AppendLine("    Public Property " & R.ColumnValue & " As " & R.LinqVar)
                    strWriteModel.AppendLine("         Get")
                    strWriteModel.AppendLine("            Return Me._" & R.ColumnValue)
                    strWriteModel.AppendLine("        End Get")
                    strWriteModel.AppendLine("        Set(value As " & R.LinqVar & ")")
                    strWriteModel.AppendLine("            If (Me._" & R.ColumnValue & ".Equals(value) = False) Then")
                    strWriteModel.AppendLine("                Me.NotifyPropertyChanging(" & R.ColumnValue.Dquotes & ")")
                    strWriteModel.AppendLine("                Me._" & R.ColumnValue & " = value")
                    strWriteModel.AppendLine("                Me.NotifyPropertyChanged(" & R.ColumnValue.Dquotes & ")")
                    strWriteModel.AppendLine("            End If")
                    strWriteModel.AppendLine("        End Set")
                    strWriteModel.AppendLine("    End Property")
                    strWriteModel.AppendLine()
                    count = count + 1
                End If
            End If

        Next

        strWriteModel.AppendLine("#Region " & Dquotes("INotifyPropertyChanged  Members"))

        strWriteModel.AppendLine("    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged")

        strWriteModel.AppendLine("    ' Used to notify that a property changed")
        strWriteModel.AppendLine("    Private Sub NotifyPropertyChanged(ByVal propertyName As String)")
        strWriteModel.AppendLine("        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))")
        strWriteModel.AppendLine("    End Sub ")

        strWriteModel.AppendLine(" #End Region")

        strWriteModel.AppendLine("#Region " & Dquotes("INotifyPropertyChanging Members"))

        strWriteModel.AppendLine("    Public Event PropertyChanging As PropertyChangingEventHandler Implements INotifyPropertyChanging.PropertyChanging")

        strWriteModel.AppendLine("    ' Used to notify that a property is about to change")
        strWriteModel.AppendLine("    Private Sub NotifyPropertyChanging(ByVal propertyName As String)")
        strWriteModel.AppendLine("       RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs(propertyName))")
        strWriteModel.AppendLine("    End Sub")
        strWriteModel.Append("End Class")
        strWriteModel.AppendLine("#End Region")
        Return strWriteModel.ToString

    End Function

End Class
