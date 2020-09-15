Imports System.Text
Imports DBExtenderLib

Public Class TestWebAPI


    Public Shared Function WebAPITest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("  Imports Newtonsoft.Json")
        sb.AppendLine("  Imports System.Net")
        sb.AppendLine("  Imports System.Web.Http")
        sb.AppendLine("  Imports System.Web.Http.Description")
        sb.AppendLine("  Imports System.Web.Http.Results")

        sb.AppendLine("  <TestClass()> Public Class " & DT.TablePluralize & "ApiControllerTest")

        sb.AppendLine(GetAllDataTest(DT))
        sb.AppendLine(GetSigleItemTest(DT))
        sb.AppendLine(PostTest(DT))
        sb.AppendLine(PutTest(DT))
        sb.AppendLine(DeleteTest(DT))

        sb.AppendLine(" End Class")
        Return sb.ToString


    End Function
    Public Shared Function GetAllDataTest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("    <TestMethod()> Public Sub Get" & DT.TablePluralize & "_ShouldReturn_All_" & DT.TableSingularize & "()")
        sb.AppendLine("        Dim controller As New " & DT.TablePluralize & "ApiController(New " & DT.TablePluralize & "RepositoryTest)")
        sb.AppendLine("        Dim results As " & DT.TablePluralize & "ManagerVM = controller.Get" & DT.TablePluralize)
        sb.AppendLine("        Assert.IsNotNull(results)")
        sb.AppendLine("        Assert.AreEqual(1, results." & DT.TablePluralize & ".Count)")

        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function
    Public Shared Function GetSigleItemTest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("    <TestMethod()> Public Sub Get" & DT.TableSingularize & "ByID_ShouldReturn_Currect_" & DT.TableSingularize & "()")
        sb.AppendLine("        Dim controller As New " & DT.TablePluralize & "ApiController(New " & DT.TablePluralize & "RepositoryTest)")
        sb.AppendLine("        Dim result = TryCast(controller.Get" & DT.TableSingularize & "(" & "ANATR" & "), OkNegotiatedContentResult(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine("        Assert.IsNotNull(result)")
        sb.AppendLine("       ' Assert.AreEqual(" & "ANATR".QT & " , result.Content.customerID)")
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function

    Public Shared Function PostTest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("     <TestMethod()> Public Sub Post" & DT.TableSingularize & "_ShouldReturnCreatedAtRouteNegotiatedContentResult()")
        sb.AppendLine("         Dim controller As New " & DT.TablePluralize & "ApiController(New " & DT.TablePluralize & "RepositoryTest)")
        sb.AppendLine("         Dim vm As New " & DT.TableSingularize & "Vm()")
        sb.AppendLine("         'vm.customerID = " & "ANCDE".QT)
        sb.AppendLine("")
        sb.AppendLine(" ")
        sb.AppendLine("         Dim result = TryCast(controller.Post" & DT.TableSingularize & "(vm), CreatedAtRouteNegotiatedContentResult(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine(" ")
        sb.AppendLine("         Assert.IsNotNull(result)")
        sb.AppendLine("         Assert.AreEqual(result.RouteName," & "DefaultApi".QT & ")")
        sb.AppendLine("         Assert.AreEqual(result.RouteValues(" & "id".QT & "), result.Content.customerID)")

        sb.AppendLine(" ")
        sb.AppendLine(" '        Dim resultAdd = TryCast(controller.Get" & DT.TableSingularize & "(" & "ANCDE".QT & "), OkNegotiatedContentResult(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine(" '        Assert.IsNotNull(resultAdd)")
        sb.AppendLine(" '        Assert.AreEqual(" & "ANCDE".QT & ", resultAdd.Content.customerID)")
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function
    Public Shared Function PutTest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("     <TestMethod()> Public Sub Put" & DT.TableSingularize & "_ShouldReturnStatusCodeResult()")
        sb.AppendLine("         Dim controller As New " & DT.TablePluralize & "ApiController(New " & DT.TablePluralize & "RepositoryTest)")
        sb.AppendLine("         Dim item = Get" & DT.TablePluralize & "()")
        sb.AppendLine(" ")
        sb.AppendLine("         Dim result = TryCast(controller.PutCustomer(item.customerID, item), StatusCodeResult)")
        sb.AppendLine("         Assert.IsNotNull(result)")
        sb.AppendLine("         Assert.IsInstanceOfType(result, GetType(StatusCodeResult))")
        sb.AppendLine("         Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode)")
        sb.AppendLine(" ")
        sb.AppendLine("         'Dim resultAdd = TryCast(controller.Get" & DT.TableSingularize & "(" & "Tony".QT & "), OkNegotiatedContentResult(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine("         'Assert.IsNotNull(result)")
        sb.AppendLine("         'Assert.AreEqual(" & "Microcruz LTD".QT & ", resultAdd.Content.companyName)")
        sb.AppendLine("     End Sub")

        Return sb.ToString
    End Function
    Public Shared Function DeleteTest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("      <TestMethod()> Public Sub Delete" & DT.TableSingularize & "_ShouldReturnOkNegotiatedContentResult()")
        sb.AppendLine("          Dim controller As New " & DT.TablePluralize & "ApiController(New " & DT.TablePluralize & "RepositoryTest)")
        sb.AppendLine("  ")
        sb.AppendLine("          Dim item = Get" & DT.TablePluralize & "()")
        sb.AppendLine("  ")
        sb.AppendLine("          Dim result = TryCast(controller.Delete" & DT.TableSingularize & "(" & "Tony".QT & " ), OkNegotiatedContentResult(Of " & DT.TableSingularize & "Vm))")
        sb.AppendLine("  ")
        sb.AppendLine("          Assert.IsNotNull(result)")
        sb.AppendLine("          Assert.AreEqual(item.customerID, result.Content.customerID)")
        sb.AppendLine("      End Sub")
        sb.AppendLine("  ")

        Return sb.ToString
    End Function









#Region "Repository Test"


    Public Shared Function RepositoryTest(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder

        sb.AppendLine("  Imports System.IO")
        sb.AppendLine("  Imports Newtonsoft.Json")
        sb.AppendLine("   Public Class " & DT.TablePluralize & "RepositoryTest")
        sb.AppendLine("       Implements I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm)")
        sb.AppendLine("       Private List" & DT.TablePluralize & " As " & DT.TablePluralize & "ManagerVM")
        sb.AppendLine(GetDataByID(DT))
        sb.AppendLine(GetDataAll(DT))
        sb.AppendLine(AddData(DT))
        sb.AppendLine(RemoveData(DT))
        sb.AppendLine(SaveData(DT))
        sb.AppendLine(UpdateData(DT))
        sb.AppendLine(LoadTestData(DT))


        sb.AppendLine("   End Class")
        Return sb.ToString
    End Function

    Public Shared Function GetDataByID(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Function Get" & DT.TableSingularize & "(id As " & DT.GetPrimaryKey.TypeVB & ") As " & DT.TableSingularize & "Vm Implements I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm).Get" & DT.TableSingularize)
        sb.AppendLine("        If IsNothing(List" & DT.TablePluralize & ") Then")
        sb.AppendLine("            LoadData()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Dim vm As " & DT.TableSingularize & "Vm = List" & DT.TablePluralize & "." & DT.TablePluralize & ".Where(Function(p) p." & DT.GetPrimaryKey.ColumnValue & " = id).SingleOrDefault()")
        sb.AppendLine("        Return vm")
        sb.AppendLine("    End Function")
        Return sb.ToString
    End Function
    Public Shared Function GetDataAll(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Function Get" & DT.TablePluralize & "() As " & DT.TablePluralize & "ManagerVM Implements I" & DT.TablePluralize & "Repository(Of " & DT.TableSingularize & "Vm).Get" & DT.TablePluralize & "")
        sb.AppendLine("        If IsNothing(List" & DT.TablePluralize & ") Then")
        sb.AppendLine("            LoadData()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Return List" & DT.TablePluralize & "")
        sb.AppendLine("    End Function")
        Return sb.ToString
    End Function

    Public Shared Function AddData(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub Add(Item As " & DT.TableSingularize & "Vm) Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).Add")
        sb.AppendLine("        If IsNothing(List" & DT.TablePluralize & ") Then")
        sb.AppendLine("            LoadData()")
        sb.AppendLine("        End If")
        sb.AppendLine("        List" & DT.TablePluralize & "." & DT.TablePluralize & ".Add(Item)")
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function
    Public Shared Function RemoveData(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub RemoveItem(Item As " & DT.TableSingularize & "Vm) Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).RemoveItem")
        sb.AppendLine("        If IsNothing(List" & DT.TablePluralize & ") Then")
        sb.AppendLine("            LoadData()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Dim vm As " & DT.TableSingularize & "Vm = List" & DT.TablePluralize & "." & DT.TablePluralize & ".Where(Function(p) p." & DT.GetPrimaryKey.ColumnValue & " = Item." & DT.GetPrimaryKey.ColumnValue & " ).SingleOrDefault()")
        sb.AppendLine("        List" & DT.TablePluralize & "." & DT.TablePluralize & ".Remove(Item)")
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function

    Public Shared Function SaveData(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub Save() Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).Save")
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function
    Public Shared Function UpdateData(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Public Sub Update(Item As " & DT.TableSingularize & "Vm) Implements IRepositoryBase(Of " & DT.TableSingularize & "Vm).Update")
        sb.AppendLine("        If IsNothing(List" & DT.TablePluralize & ") Then")
        sb.AppendLine("            LoadData()")
        sb.AppendLine("        End If")
        sb.AppendLine("        Dim data As " & DT.TableSingularize & "Vm = List" & DT.TablePluralize & "." & DT.TablePluralize & ".Where(Function(p) p." & DT.GetPrimaryKey.ColumnValue & " = Item." & DT.GetPrimaryKey.ColumnValue & ").SingleOrDefault()")

        For Each col In DT.ListColumn
            sb.AppendLine("        data." & col.ColumnValue & " = Item." & col.ColumnValue.LowerTheFistChar)
        Next
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function
    Public Shared Function LoadTestData(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder
        sb.AppendLine("    Private Sub LoadData()")
        sb.AppendLine("        Dim path = " & ("D:\App Dev\Tony-Tools\VBSPANorthwind\VBSPANorthwind\JSON\" & DT.TablePluralize & ".json").QT)
        sb.AppendLine("        Dim json = My.Computer.FileSystem.ReadAllText(path).ToString")
        sb.AppendLine("        List" & DT.TablePluralize & " = JsonConvert.DeserializeObject(Of " & DT.TablePluralize & "ManagerVM)(json)")
        sb.AppendLine("    End Sub")
        Return sb.ToString
    End Function

#End Region

End Class
