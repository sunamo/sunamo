using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MSColumnsDB : List<MSSloupecDB>
{
    static Type type = typeof(MSColumnsDB);
    bool signed = false;
    string derived = null;
    string replaceMSinMSStoredProceduresI = null;
    static string _tableNameField = "_tableName";
    public Dictionary<string, MSSloupecDB> dict = new Dictionary<string, MSSloupecDB>();

    /// <summary>
    /// Must call Close after return SqlCommand make work
    /// A2 pokud nechci aby se mi vytvářeli reference na ostatní tabulky. Vhodné při testování tabulek a programů, kdy je pak ještě budu mazat a znovu plnit.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables, string cs)
    {
        var conn = new SqlConnection(cs);

        conn.Open();
        var comm = GetSqlCreateTable(table, dynamicTables, conn);
        // Cant call CLose here, it return SqlCommand
        //conn.Close();
        return comm;

    }
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables, SqlConnection conn)
    {
        string sql = GeneratorMsSql.CreateTable(table, this, dynamicTables, conn);
        SqlCommand comm = new SqlCommand(sql, conn);

        return comm;
    }

    /// <summary>
    /// A1 od jakých rozhraní a tříd by měla být odvozena třída TableRow
    /// </summary>
    /// <param name="derived"></param>
    /// <param name="signed"></param>
    /// <param name="p"></param>
    public MSColumnsDB(string derived, bool signed, params MSSloupecDB[] p) : this(signed, derived, null, p)
    {
    }
    public int index = -1;
    public MSColumnsDB(bool signed, string derived, string replaceMSinMSStoredProceduresI, params MSSloupecDB[] p)
    {
        this.signed = signed;
        this.derived = derived;
        this.replaceMSinMSStoredProceduresI = replaceMSinMSStoredProceduresI;
        this.AddRange(p);
        FillDictionary();
    }

    private void FillDictionary()
    {
        foreach (var item in this)
        {
            var key = item.Name.Trim();
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, item);
            }
        }
    }

    public MSColumnsDB(bool signed, params MSSloupecDB[] p) : this(signed, null, null, p)
    {
    }
    public MSColumnsDB(params MSSloupecDB[] p) : this(false, null, null, p)
    {
    }
}
