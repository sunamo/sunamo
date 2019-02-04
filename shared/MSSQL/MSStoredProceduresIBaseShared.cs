using sunamo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MSStoredProceduresIBase : SqlServerHelper
{
    public short SelectCellDataTableShortOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarShort(signed, comm);
    }

    /// <summary>
    /// Interně volá metodu SelectRowReader
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="nazvySloupcu"></param>
    /// <returns></returns>
    public object[] SelectSelectiveOneRow(string tabulka, string sloupecID, object id, string nazvySloupcu)
    {
        SqlCommand comm = new SqlCommand(SH.Format("SELECT TOP(1) {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return SelectRowReader(comm);
    }

    public object[] SelectOneRowForTableRow(string TableName, string nazevSloupce, object hodnotaSloupce)
    {
        // Index nemůže být ani pole bajtů ani null takže to je v pohodě
        DataTable dt = SelectDataTable("SELECT TOP(1) * FROM " + TableName + " WHERE " + nazevSloupce + " = @p0", hodnotaSloupce);
        if (dt.Rows.Count == 0)
        {
            return null; // CA.CreateEmptyArray(pocetSloupcu);
        }
        return dt.Rows[0].ItemArray;
    }




}
