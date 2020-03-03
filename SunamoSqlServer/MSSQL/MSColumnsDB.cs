
using System.Collections.Generic;
using System.Text;
using System;
using System.Data.SqlClient;
using System.Data;
using sunamo;

//using System.Activities;
public partial class MSColumnsDB : List<MSSloupecDB>
{
    bool signed = false;
    string derived = null;
    string replaceMSinMSStoredProceduresI = null;
    static string _tableNameField = "_tableName";
    public Dictionary<string, MSSloupecDB> dict = new Dictionary<string, MSSloupecDB>();

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

    public MSColumnsDB(bool signed, params MSSloupecDB[] p) : this(signed, null, null, p)
    {

    }

    public MSColumnsDB(params MSSloupecDB[] p) : this(false, null, null, p)
    {
    }

    /// <summary>
    /// A2 is name of site - part before Layer class
    /// A3 is Lyr_ etc.
    /// A4 is parameter which is inserted into GetSqlCreateTable.
    /// dynamicTables - whether is not desirable to create references to other tables. Good while test tables and apps, when I will it delete later.
    /// </summary>
    public static string GetSqlForClearAndCreateTables(Dictionary<string, MSColumnsDB> sl, string Mja, string tablePrefix, bool dynamicTables)
    {
        StringBuilder dropes = new StringBuilder();
        StringBuilder createTable = new StringBuilder();

        foreach (KeyValuePair<string, MSColumnsDB> item in sl)
        {
            if (!IsDynamic(item.Key, tablePrefix))
            {
                dropes.AppendLine("MSStoredProceduresI.ci.DropTableIfExists(\"" + item.Key + "\");");
            }
        }
        foreach (KeyValuePair<string, MSColumnsDB> item in sl)
        {
            if (!IsDynamic(item.Key, tablePrefix))
            {
                createTable.AppendLine(Mja + "Layer.s[\"" + item.Key + "\"].GetSqlCreateTable(\"" + item.Key + "\", " + dynamicTables.ToString().ToLower() + ").ExecuteNonQuery();");
            }
        }

        return dropes.ToString() + Environment.NewLine + createTable.ToString();
    }

    /// <summary>
    /// True if name of table after prefix contains another _
    /// </summary>
    /// <param name="p"></param>
    /// <param name="tablePrefix"></param>
    
    public SqlCommand GetSqlCreateTable(string nazevTabulky, string cs)
    {
        return GetSqlCreateTable( nazevTabulky, false, cs);
    }

    public static MSColumnsDB IDName(int p)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(" + p.ToString() + ")", false, true)
            );
    }

    public static MSColumnsDB IDNameShort(int p)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(" + p.ToString() + ")", false, true)
            );
    }

    public static MSColumnsDB IDNameTinyInt(int p)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(" + p.ToString() + ")", false, true)
            );
    }

    public static MSColumnsDB IntInt(string c1, string c2)
    {
        return new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, c1),
            MSSloupecDB.CI(SqlDbType2.Int, c2)
        );
    }
}
