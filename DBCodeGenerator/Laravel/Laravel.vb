Imports System.Text
Imports DBExtenderLib
Public Class Laravel

    Public Shared Function CreateTableMigrate(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sb.AppendLine("    public function up()")
        sb.AppendLine("     {")
        sb.AppendLine("         Schema::create('" & DT.TablePluralize & "', function (Blueprint $table) {")
        sb.AppendLine("             $table->increments('" & colPk.ColumnValue & "');")
        sb.AppendLine("             $table->timestamps();")
        For Each col In DT.ListColumn
            sb.AppendLine("             $table->" & col.VarCSharp & "('" & col.ColumnValue & "');")
        Next
        sb.AppendLine("         });")
        sb.AppendLine("     }")
        sb.AppendLine("  php artisan make:model " & DT.TablePluralize & " -m")
        sb.AppendLine(" Route::post('/" & DT.TableSingularize & "', ['uses' => '" & DT.TableSingularize & "Controller@post" & DT.TableSingularize & "']);")
        sb.AppendLine(" Route::get('/" & DT.TablePluralize & "', ['uses' => '" & DT.TableSingularize & "Controller@get" & DT.TablePluralize & "']);")
        sb.AppendLine(" Route::put('/" & DT.TableSingularize & "/{id}', ['uses' => '" & DT.TableSingularize & "Controller@put" & DT.TableSingularize & "']);")
        sb.AppendLine(" Route::get('/" & DT.TableSingularize & "/{id}', ['uses' => '" & DT.TableSingularize & "Controller@get" & DT.TableSingularize & "']);")
        sb.AppendLine(" Route::delete('/" & DT.TableSingularize & "/{id}', ['uses' => '" & DT.TableSingularize & "Controller@delete" & DT.TableSingularize & "']);")



        Return sb.ToString
    End Function
    Public Shared Function CreateSeeder(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim sqlstr As String = SQLCodeGen.CreateSelectCommandAll(DT)
        Dim dsTB = dsHelper.GetDataSet(DT.StrConnection, sqlstr).Tables(0)

        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault


        sb.AppendLine("  Public Function run()")
        sb.AppendLine("    {")

        For Each r In dsTB.Rows
            'Dim sbConv As New StringBuilder
            sb.AppendLine("       $" & DT.TablePluralize.ToLower & " = new " & DT.TableSingularize & ";")
            For Each col In DT.ListColumn
                ' If col.ColumnValue <> colPk.ColumnValue Then
                sb.AppendLine("       $" & DT.TablePluralize.ToLower & "->" & col.ColumnValue & " = " & col.GetTypeData(r) & ";")
                '  End If
            Next
            sb.AppendLine("       $" & DT.TablePluralize.ToLower & "->save();")
            sb.AppendLine()
        Next



        Return sb.ToString
    End Function
    Public Shared Function CreateController(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        Dim colPk = (From c In DT.ListColumn Where c.IsPrimary_Key = True Select c).Take(1).SingleOrDefault

        sb.AppendLine("<?php")
        sb.AppendLine("")
        sb.AppendLine("Namespace App\Http\Controllers;")
        sb.AppendLine("")
        sb.AppendLine("use Illuminate\Http\Request;")
        sb.AppendLine("use App\" & DT.TableSingularize & ";")
        sb.AppendLine("class " & DT.TableSingularize & "Controller extends Controller")
        sb.AppendLine("{")
        sb.AppendLine("")
        sb.AppendLine("    public function get" & DT.TablePluralize & "()")
        sb.AppendLine("    {")
        sb.AppendLine("        $" & DT.TablePluralize.ToLower & " = " & DT.TableSingularize & "::all();")
        sb.AppendLine("        $response = [")
        sb.AppendLine("          '" & DT.TablePluralize.ToLower & "' => $" & DT.TableSingularize.ToLower)
        sb.AppendLine("        ];")
        sb.AppendLine("        return response() -> json($response, 200);")
        sb.AppendLine("    }")
        sb.AppendLine("")
        sb.AppendLine("    public function post" & DT.TableSingularize & "(Request $request)")
        sb.AppendLine("    {")
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & " = new " & DT.TableSingularize & "();")
        sb.AppendLine("")
        For Each col In DT.ListColumn
            If col.ColumnValue <> colPk.ColumnValue Then
                sb.AppendLine("             $" & DT.TableSingularize.ToLower & "->" & col.ColumnValue & " = $request->" & col.ColumnValue & ";")
            End If

        Next
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & "->save();")
        sb.AppendLine("        return response() -> json(['" & DT.TableSingularize.ToLower & "' => $" & DT.TableSingularize.ToLower & "], 201);")
        sb.AppendLine("    }")
        sb.AppendLine("")
        sb.AppendLine("")
        sb.AppendLine("    public function put" & DT.TableSingularize & "(Request $request, $id)")
        sb.AppendLine("    {")
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & " = " & DT.TableSingularize & "::find($id);")
        sb.AppendLine("        If (!$" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("            Return response() -> json(['message' => 'Document not found'], 404);")
        sb.AppendLine("        }")
        For Each col In DT.ListColumn
            If col.ColumnValue <> colPk.ColumnValue Then
                sb.AppendLine("             $" & DT.TableSingularize.ToLower & "->" & col.ColumnValue & " = $request->" & col.ColumnValue & ";")
            End If
        Next
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & "->save();")
        sb.AppendLine("        Return response() -> json(['" & DT.TableSingularize.ToLower & "' => $" & DT.TableSingularize.ToLower & "], 200);")
        sb.AppendLine("    }")
        sb.AppendLine("")
        sb.AppendLine("   public function delete" & DT.TableSingularize & "($id)")
        sb.AppendLine("    {")
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & " = " & DT.TableSingularize & "::find($id);")
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & "->delete();")
        sb.AppendLine("        Return response() -> json(['message' => '" & DT.TableSingularize & " deleted'], 200);")
        sb.AppendLine("    }")
        sb.AppendLine("    public function get" & DT.TableSingularize & "($id)")
        sb.AppendLine("    {")
        sb.AppendLine("        $" & DT.TableSingularize.ToLower & " = " & DT.TableSingularize & "::find($id);")
        sb.AppendLine("         If (!$" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("            Return response() -> json(['message' => 'Document not found'], 404);")
        sb.AppendLine("        }")
        sb.AppendLine("        Return response() -> json(['" & DT.TableSingularize.ToLower & "' => $" & DT.TableSingularize.ToLower & "], 200);")
        sb.AppendLine("    }")
        sb.AppendLine("")
        sb.AppendLine("}")

        Return sb.ToString
    End Function
End Class
