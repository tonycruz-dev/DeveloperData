Imports DBExtenderLib
Imports System.Text
Public Class CSApiController
    Public Shared Function CreateApiController(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        Dim pColumn = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault


        sb.AppendLine("using System;")
        sb.AppendLine("using System.Collections.Generic;")
        sb.AppendLine("using System.Linq;")
        sb.AppendLine("using System.Net;")
        sb.AppendLine("using System.Net.Http;")
        sb.AppendLine("using System.Web.Http;")
        sb.AppendLine("using System.Data.Entity;")
        sb.AppendLine("using System.Data.Entity.Infrastructure;")

        sb.AppendLine("namespace MVC.Controllers")
        sb.AppendLine("{")
        sb.AppendLine("    public class " & DT.TablePluralize & "Controller : ApiController")
        sb.AppendLine("    {")
        sb.AppendLine("        private " & DB.DatabaseName & "Context db = new " & DB.DatabaseName & "Context();")
        sb.AppendLine("        // GET api/" & DT.TablePluralize)
        sb.AppendLine("        public IEnumerable<" & LowerTheFistChar(DT.TableSingularize) & "Vm> Get" & DT.TablePluralize & "()")
        sb.AppendLine("        {")
        sb.AppendLine("          " & SelecteGetAPIController(DT))
       
        sb.AppendLine("        }")
        sb.AppendLine()
        
        sb.AppendLine("        // GET api/" & DT.TablePluralize & "/5")
        sb.AppendLine("         [ResponseType(typeof(" & DT.TableSingularize.LowerTheFistChar & "Vm))]")
        sb.AppendLine("        public  IHttpActionResult Get" & DT.TablePluralize & "(" & pColumn.VarCSharp & " id)")
        sb.AppendLine("        {")
        sb.AppendLine("            " & DT.TableSingularize & " result = db." & DT.TablePluralize & ".Where(" & LowerTheFistChar(Left(DT.TableName, 3)) & " => " & LowerTheFistChar(Left(DT.TableName, 3)) & "." & pColumn.ColumnValue & " ==id).SingleOrDefault();")
        sb.AppendLine()
        sb.AppendLine("            if (result == null)")
        sb.AppendLine("            {")
        sb.AppendLine("                return NotFound();")
        sb.AppendLine("            }")
        sb.AppendLine("            var vm  = new " & DT.TableSingularize.LowerTheFistChar & "Vm()")
        sb.AppendLine("            {")
        Dim StrSelect As New StringBuilder
        For Each col In DT.ListColumn
            StrSelect.AppendLine("               " & col.ColumnValue.LowerTheFistChar & " = result." & col.ColumnValue & ",")
        Next
        Dim mylastComar = StrSelect.ToString.LastIndexOf(",")
        Dim resultSelect = StrSelect.Remove(mylastComar, 1).ToString
        sb.AppendLine(resultSelect)

        sb.AppendLine("            };")
        sb.AppendLine()
        sb.AppendLine("            return Ok(vm);")
        sb.AppendLine("        }")
        sb.AppendLine()
        sb.AppendLine("        // POST api/" & DT.TablePluralize)
        sb.AppendLine("         [ResponseType(typeof(" & DT.TableSingularize.LowerTheFistChar & "Vm))]")
        sb.AppendLine("        public IHttpActionResult Post" & DT.TableSingularize & "(" & LowerTheFistChar(DT.TableSingularize) & "Vm vm)")
        sb.AppendLine("        {")
        sb.AppendLine("            if (ModelState.IsValid)")
        sb.AppendLine("            {")
        sb.AppendLine("                " & DT.TableSingularize & " result = new " & DT.TableSingularize & "();")

        Dim sbPost As New StringBuilder
        For Each col In DT.ListColumn
            sbPost.AppendLine("                result." & col.ColumnValue & " = vm." & LowerTheFistChar(col.ColumnValue) & ";")
        Next

        sb.AppendLine(sbPost.ToString)

        sb.AppendLine()
        sb.AppendLine("                db." & DT.TablePluralize & ".InsertOnSubmit(result);")
        sb.AppendLine("                db.SubmitChanges();")
        sb.AppendLine("                vm." & DT.GetPrimaryKey.ColumnValue.LowerTheFistChar & " = result." & DT.GetPrimaryKey.ColumnValue & ";")
        sb.AppendLine("                return CreatedAtRoute(" & "DefaultApi".QT & ", new { id = result." & DT.GetPrimaryKey.ColumnValue & " }, vm);")
        sb.AppendLine("            }")
        sb.AppendLine("            else")
        sb.AppendLine("            {")
        sb.AppendLine("                return  BadRequest(ModelState);")
        sb.AppendLine("            }")
        sb.AppendLine("        }")
        sb.AppendLine()

        sb.AppendLine("        // PUT api/" & DT.TablePluralize & "/5")
        sb.AppendLine("        [ResponseType(typeof(" & DT.TableSingularize.LowerTheFistChar & "Vm))]")
        sb.AppendLine("        public IHttpActionResult Put" & DT.TableSingularize & "(" & pColumn.VarCSharp & "  id, " & LowerTheFistChar(DT.TableSingularize) & "Vm vm)")
        sb.AppendLine("        {")
        sb.AppendLine("            if (!ModelState.IsValid)")
        sb.AppendLine("            {")
        sb.AppendLine("               return BadRequest(ModelState);")
        sb.AppendLine("            }")
        sb.AppendLine("            if (id != vm." & pColumn.ColumnValue.LowerTheFistChar & ")")
        sb.AppendLine("            {")
        sb.AppendLine("                return BadRequest();")
        sb.AppendLine("            }")
        sb.AppendLine("            " & DT.TableSingularize & " result = db." & DT.TablePluralize & ".Where(" & LowerTheFistChar(Left(DT.TableName, 3)) & " => " & LowerTheFistChar(Left(DT.TableName, 3)) & "." & pColumn.ColumnValue & " ==id).SingleOrDefault();")
        sb.AppendLine()
        Dim sbput As New StringBuilder
        For Each col In DT.ListColumn
            sbput.AppendLine("                result." & col.ColumnValue & " = vm." & LowerTheFistChar(col.ColumnValue) & ";")
        Next
        sb.AppendLine(sbput.ToString)
        sb.AppendLine("                try")
        sb.AppendLine("                {")
        sb.AppendLine("                    db.SubmitChanges();")
        sb.AppendLine("                }")
        sb.AppendLine("                catch (DbUpdateConcurrencyException)")
        sb.AppendLine("                {")
        sb.AppendLine("                   return NotFound();")
        sb.AppendLine("                }")
        sb.AppendLine()
        sb.AppendLine("                return StatusCode(HttpStatusCode.NoContent);")
        sb.AppendLine()
        sb.AppendLine("        }")
        sb.AppendLine()
        sb.AppendLine("        // DELETE api//" & DT.TablePluralize & "/5")
        sb.AppendLine("        [ResponseType(typeof(" & DT.TableSingularize.LowerTheFistChar & "Vm))]")
        sb.AppendLine("        public IHttpActionResult Delete" & DT.TableSingularize & "(" & pColumn.VarCSharp & " id)")
        sb.AppendLine("        {")
        sb.AppendLine("            " & DT.TableSingularize & " result = db." & DT.TablePluralize & ".Where(" & LowerTheFistChar(Left(DT.TableName, 3)) & " => " & LowerTheFistChar(Left(DT.TableName, 3)) & "." & pColumn.ColumnValue & " ==id).SingleOrDefault();")

        sb.AppendLine("            if ( result == null)")
        sb.AppendLine("            {")
        sb.AppendLine("               return NotFound();")
        sb.AppendLine("            }")
        sb.AppendLine()
        sb.AppendLine("            db." & DT.TablePluralize & ".DeleteOnSubmit(result);")
        sb.AppendLine()
        sb.AppendLine("            try")
        sb.AppendLine("            {")
        sb.AppendLine("                db.SubmitChanges();")
        sb.AppendLine("            }")
        sb.AppendLine("            catch (DbUpdateConcurrencyException)")
        sb.AppendLine("            {")
        sb.AppendLine("                 return NotFound();")
        sb.AppendLine("            }")
        sb.AppendLine()
        sb.AppendLine("            return Ok(result);")
        sb.AppendLine("        }")
        sb.AppendLine("    }")
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function SelecteGetAPIController(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()


        sb.AppendLine(" var	Result" & DT.TableSingularize & " = (from " & Left(DT.TableName, 3) & " in db." & DT.TablePluralize & " orderby " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)

        Dim sbselect As New StringBuilder("     select new " & DT.TableSingularize.LowerTheFistChar & "Vm { " & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            If R.VarCSharp = "string" Then
                sbselect.AppendLine("         " & LowerTheFistChar(R.ColumnValue) & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            Else
                sbselect.AppendLine("         " & LowerTheFistChar(R.ColumnValue) & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            End If

        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList();"
        sb.AppendLine(StrSelect)
        sb.AppendLine("	return Result" & DT.TableSingularize & ";")
        sb.AppendLine()
        Return sb.ToString
    End Function

    Public Shared Function SelecteToViewModel(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("public List<" & DT.TableSingularize & "Vm> Get" & DT.TableSingularize & "Vm()")
        sb.AppendLine("{")
        sb.AppendLine("	List<" & DT.TableSingularize & "Vm> Result" & DT.TableSingularize & " = null;")
        sb.AppendLine("	" & DB.DatabaseName & "Context db = new " & DB.DatabaseName & "Context();")


        sb.AppendLine("	Result" & DT.TableSingularize & " = (from " & Left(DT.TableName, 3) & " in db." & DT.TablePluralize & " orderby " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)

        Dim sbselect As New StringBuilder("     select new " & LowerTheFistChar(DT.TableSingularize) & "Vm { " & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            If R.VarCSharp = "string" Then
                sbselect.AppendLine("         " & R.ColumnValue & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            Else
                sbselect.AppendLine("         " & R.ColumnValue & " =   (" & R.VarCSharp & ")" & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            End If

        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList();"
        sb.AppendLine(StrSelect)
        sb.AppendLine("	return Result" & DT.TableSingularize & ";")
        sb.AppendLine()
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Public Shared Function SelecteJosnToViewModel(ByVal DT As TableNameInfo, ByVal DB As DatabaseNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("public List<" & LowerTheFistChar(DT.TableSingularize) & "Vm> Get" & DT.TableSingularize & "Vm()")
        sb.AppendLine("{")
        sb.AppendLine("	List<" & LowerTheFistChar(DT.TableSingularize) & "Vm> Result" & LowerTheFistChar(DT.TableSingularize) & " = null;")
        sb.AppendLine("	" & DB.DatabaseName & "Context db = new " & DB.DatabaseName & "Context();")


        sb.AppendLine("	Result" & LowerTheFistChar(DT.TableSingularize) & " = (from " & Left(DT.TableName, 3) & " in db." & DT.TablePluralize & " orderby " & Left(DT.TableName, 3) & "." & DT.GetPrimaryKey.ColumnValue)

        Dim sbselect As New StringBuilder("     select new " & LowerTheFistChar(DT.TableSingularize) & "Vm { " & vbNewLine)
        For Each R As ColumnsInfo In DT.ListColumn
            If R.VarCSharp = "string" Then
                sbselect.AppendLine("         " & LowerTheFistChar(R.ColumnValue) & " =   " & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            Else
                sbselect.AppendLine("         " & LowerTheFistChar(R.ColumnValue) & " =   (" & R.VarCSharp & ")" & Left(DT.TableName, 3) & "." & R.ColumnValue & ", ")
            End If

        Next
        Dim mylastComar = sbselect.ToString.LastIndexOf(",")
        Dim StrSelect = sbselect.Remove(mylastComar, 1).ToString
        StrSelect = StrSelect & "         }).ToList();"
        sb.AppendLine(StrSelect)
        sb.AppendLine("	return Result" & LowerTheFistChar(DT.TableSingularize) & ";")
        sb.AppendLine()
        sb.AppendLine("}")
        Return sb.ToString
    End Function
    Shared Function LowerTheFistChar(str As String) As String
        Return Char.ToLower(str.Chars(0)) + str.Substring(1)
    End Function
End Class
