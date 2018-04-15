using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;

public class MSStoredProceduresIBase : SqlServerHelper
{
    public DataTable DeleteAllSmallerThanWithOutput(string TableName, string sloupceJezVratit, string nameColumnSmallerThan, object valueColumnSmallerThan, AB[] whereIs, AB[] whereIsNot)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnSmallerThan, valueColumnSmallerThan));
        string whereS = GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + GeneratorMsSql.OutputDeleted(sloupceJezVratit) + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, whereSmallerThan);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    

    public DataTable DeleteWithOutput(string TableName, string sloupceJezVratit, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + GeneratorMsSql.OutputDeleted(sloupceJezVratit) + GeneratorMsSql.SimpleWhere(idColumn));
        AddCommandParameter(comm, 0, idValue);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    public bool HasAnyValue(string table, string columnName, string iDColumnName, int idColumnValue)
    {
        return SelectCellDataTableStringOneRow(table, columnName, iDColumnName, idColumnValue) != "";
    }

    

    public class Parse
    {
        public class DateTime
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            public System.DateTime ParseDateTimeMinVal(string p)
            {
                System.DateTime p2;
                if (System.DateTime.TryParse(p, out p2))
                {
                    return p2;
                }
                return MSStoredProceduresI.DateTimeMinVal;
            }
        }
    }

    public int SelectID(string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        return SelectID(false, tabulka, nazevSloupce, hodnotaSloupce);
    }

    SqlConnection _conn = null;
    public SqlConnection conn
    {
        get
        {
            if (_conn == null)
            {
                return MSDatabaseLayer.conn;
            }
            return _conn;
        }
        set
        {
            _conn = value;
        }
    }
    
    /// <summary>
    /// Tato hodnota byla založena aby používal všude v DB konzistentní datovou hodnotu, klidně může mít i hodnotu DT.MaxValue když to tak má být
    /// </summary>
    public static readonly DateTime DateTimeMinVal = new DateTime(1900, 1, 1);
    public static readonly DateTime DateTimeMaxVal = new DateTime(2079, 6, 6);

    protected MSStoredProceduresIBase()
    {

    }

    public void RepairConnection()
    {
        SqlConnection.ClearAllPools();
        conn.Close();

    }
    public MSStoredProceduresIBase(SqlConnection conn)
    {
        this.conn = conn;
    }

    

    

    #region Statické prvky třídy
    /// <summary>
    /// a2 je X jako v příkazu @pX
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="i"></param>
    /// <param name="o"></param>
    public static int AddCommandParameter(SqlCommand comm, int i, object o)
    {
        if (o == null || o.GetType() == DBNull.Value.GetType())
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = "@p" + i.ToString();
            p.Value = DBNull.Value;
            comm.Parameters.Add(p);
        }
            else if (o.GetType() == typeof(byte[]))
            {
                // Pokud chcete uložit pole bajtů, musíte nejdřív vytvořit parametr s typem v DB(já používám vždy Image) a teprve pak nastavit hodnotu
                SqlParameter param = comm.Parameters.Add("@p" + i.ToString(), SqlDbType.Binary);
                param.Value = o;
            }

        else if (o.GetType() == Consts.tString || o.GetType() == Consts.tChar)
        {
            string _ = o.ToString();
                comm.Parameters.AddWithValue("@p" + i.ToString(), MSStoredProceduresI.ConvertToVarChar(_));
        }
        else
        {
                comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }

        ++i;
        return i;
    }

    

    /// <summary>
    /// Počítá od nuly
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    private static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
    }

    /// <summary>
    /// Počítá od nuly
    /// Mohu volat i s A2 null, v takovém případě se nevykoná žádný kód
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    private static int AddCommandParameterFromAbc(SqlCommand comm, ABC where, int i)
    {
        if (where != null)
        {
            for (var i2 = 0; i2 < where.Count; i2++)
            {
                AddCommandParameter(comm, i, where[i2].B);
                i++;
            }
        }
        return i;
    }
    /// <summary>
    /// Libovolné z hodnot A2 až A5 může být null, protože se to postupuje metodě AddCommandParameteresArrays
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    /// <param name="greaterThanWhere"></param>
    /// <param name="lowerThanWhere"></param>
    private static void AddCommandParameteresCombinedArrays(SqlCommand comm, int i, AB[] where, AB[] isNotWhere, AB[] greaterThanWhere, AB[] lowerThanWhere)
    {
        AddCommandParameteresArrays(comm, i, CA.ToArrayT<AB[]>(where, isNotWhere,greaterThanWhere,lowerThanWhere));
    }

    private static void AddCommandParameteresCombinedArrays(SqlCommand comm, int i2, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere)
    {
        int l = CA.GetLength(where);
        l += CA.GetLength(isNotWhere);
        l += CA.GetLength(greaterThanWhere);
        l += CA.GetLength(lowerThanWhere);
        AB[] ab = new AB[l];
        int dex = 0;
        if (where != null)
        {
            for (int i = 0; i < where.Count; i++)
            {
                ab[dex++] = where[i];
            }
        }
        if (isNotWhere != null)
        {
            for (int i = 0; i < isNotWhere.Count; i++)
            {
                ab[dex++] = isNotWhere[i];
            }
        }
        if (greaterThanWhere != null)
        {
            for (int i = 0; i < greaterThanWhere.Count; i++)
            {
                ab[dex++] = greaterThanWhere[i];
            }
        }
        if (lowerThanWhere != null)
        {
            for (int i = 0; i < lowerThanWhere.Count; i++)
            {
                ab[dex++] = lowerThanWhere[i];
            }
        }
        AddCommandParameteresArrays(comm, i2, ab);
    }

    

    /// <summary>
    /// Bude se počítat od nuly
    /// Některé z vnitřních polí může být null
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    private static void AddCommandParameteresArrays(SqlCommand comm, int i, params AB[][] where)
    {
        //int i = 0;
        foreach (var item in where)
        {
            if (item != null)
            {
                foreach (var item2 in item)
                {
                    i = AddCommandParameter(comm, i, item2.B);
                }
            }
        }
    }

    private int AddCommandParameteres(SqlCommand comm, int pocIndex, object[] hodnotyOdNuly)
    {
        foreach (var item in hodnotyOdNuly)
        {
            AddCommandParameter(comm, pocIndex, item);
            pocIndex++;
        }
        return pocIndex;
    }

    public static int AddCommandParameteres(SqlCommand comm, int pocIndex, AB[] aWhere)
    {
        foreach (var item in aWhere)
        {
            AddCommandParameter(comm, pocIndex, item.B);
            pocIndex++;
        }
        return pocIndex;
    }


    #endregion

    #region DataTableToList
    public List<bool> DataTableToListBool(DataTable dataTable, int dex)
    {
        List<bool> vr = new List<bool>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add((bool)item.ItemArray[dex]);
            //vr.Add(bool.Parse(item.ItemArray[dex].ToString()));
        }
        return vr;
    }

    public List<short> DataTableToListShort(DataTable dataTable, int p)
    {
        List<short> vr = new List<short>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add((short)item.ItemArray[p]);
            //vr.Add(bool.Parse(item.ItemArray[dex].ToString()));
        }
        return vr;
    }

    public List<int> DataTableToListInt(DataTable dataTable, int p)
    {
        List<int> vr = new List<int>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add((int)item.ItemArray[p]);
            //vr.Add(bool.Parse(item.ItemArray[dex].ToString()));
        }
        return vr;
    }

    public List<string> DataTableToListString(DataTable dataTable, int dex)
    {
        List<string> vr = new List<string>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add(item.ItemArray[dex].ToString());
        }
        return vr;
    }
    #endregion

    #region Delete
    /// <summary>
    /// Maže všechny řádky, ne jen jeden.
    /// </summary>
    public int Delete(string table, string sloupec, object id)
    {
        return ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE {1} = @p0", table, sloupec), id);
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// Vrátí zda byl vymazán alespoň jeden řádek
    /// 
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public int Delete(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);

        return f;
    }

    public int DeleteAllSmallerThan(string TableName, string nameColumnSmallerThan, object valueColumnSmallerThan, params AB[] where)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnSmallerThan, valueColumnSmallerThan));
        string whereS = GeneratorMsSql.CombinedWhere(where, null, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, null, whereSmallerThan);
        int f = ExecuteNonQuery(comm);

        return f;
    }

    public List<int> SelectAllInColumnLargerThanInt(string TableName, string columnReturn, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnLargerThan, valueColumnLargerThan));
        string whereS = GeneratorMsSql.CombinedWhere(where, null, whereSmallerThan, null);
        SqlCommand comm = new SqlCommand("select " +columnReturn+" FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, whereSmallerThan, null);
        return ReadValuesInt(comm);
    }

    public int DeleteAllLargerThan(string TableName, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnLargerThan, valueColumnLargerThan));
        string whereS = GeneratorMsSql.CombinedWhere(where, null, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, whereSmallerThan, null);
        int f = ExecuteNonQuery(comm);

        return f;
    }

    public int DeleteOneRow(string table, string sloupec, object id)
    {
        return ExecuteNonQuery(string.Format("DELETE TOP(1) FROM {0} WHERE {1} = @p0", table, sloupec), id);
    }

    public bool DeleteOneRow(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand("DELETE TOP(1) FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);

        return f == 1;
    }

    /// <summary>
    /// Pouýžívá se když chceš odstranit více řádků najednou pomocí AB. Nedá se použít pokud aspoň na jednom řádku potřebuješ AND
    /// </summary>
    /// <param name="p"></param>
    /// <param name="aB"></param>
    /// <returns></returns>
    public int DeleteOR(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhereOR(where);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);

        return f;
    }
    #endregion

    #region Drop tables
    public void DropAllTables()
    {
        List<string> dd = SelectGetAllTablesInDB();
        foreach (string item in dd)
        {
            ExecuteNonQuery(new SqlCommand("DROP TABLE " + item));
        }
    }

    public void DropAndCreateTable(string p, Dictionary<string, MSColumnsDB> dictionary)
    {
        if (dictionary.ContainsKey(p))
        {
            DropTableIfExists(p);
            dictionary[p].GetSqlCreateTable(p, true, conn).ExecuteNonQuery();
        }
    }

    public void DropAndCreateTable(string p,  MSColumnsDB msc)
    {
        
            DropTableIfExists(p);
            msc.GetSqlCreateTable(p, false, conn).ExecuteNonQuery();
        
    }

    public void DropAndCreateTable2(string p, Dictionary<string, MSColumnsDB> dictionary)
    {
        if (dictionary.ContainsKey(p))
        {
            DropTableIfExists(p+ "2");
            dictionary[p].GetSqlCreateTable(p + "2").ExecuteNonQuery();
        }
    }

    public int DropTableIfExists(string table)
    {
        if (SelectExistsTable(table))
        {
            return ExecuteNonQuery(new SqlCommand("DROP TABLE " + table));
        }
        return 0;
    }
    #endregion

    #region Execute
    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataTable SelectDataTable(SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);
        return dt;
    }
    

    /// <summary>
    /// A1 jsou hodnoty bez převedení AddCommandParameter nebo ReplaceValueOnlyOne
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    private DataTable SelectDataTable(string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }
        return SelectDataTable(comm);
        //return SelectDataTable(string.Format(sql, _params));
    }

    

    public int ExecuteNonQuery(SqlCommand comm)
    {
        comm.Connection = conn;
        return comm.ExecuteNonQuery();
    }

    public int ExecuteNonQuery(string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }
        return ExecuteNonQuery(comm);
    }

    private SqlDataReader ExecuteReader(SqlCommand comm)
    {
        comm.Connection = conn;
        return comm.ExecuteReader(CommandBehavior.Default);

    }

    /// <summary>
    /// Automaticky doplní connection
    /// </summary>
    /// <param name="comm"></param>
    /// <returns></returns>
    public object ExecuteScalar(SqlCommand comm)
    {
        //SqlDbType.SmallDateTime;
        comm.Connection = conn;
        return comm.ExecuteScalar();
    }

    public object ExecuteScalar(string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }
        return ExecuteScalar(comm);
    }

    private bool ExecuteScalarBool(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return false;
        }
        return Convert.ToBoolean( o);
    }

    private byte ExecuteScalarByte(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return 0;
        }
        return Convert.ToByte(o);
    }

    private DateTime ExecuteScalarDateTime(DateTime getIfNotFound, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null || o == DBNull.Value)
        {
            return getIfNotFound;
        }
        return Convert.ToDateTime( o);
    }

    private float ExecuteScalarFloat(bool signed, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return float.MaxValue;
                //return short.MaxValue;
            }
            else
            {
                return -1;
            }
        }
        return Convert.ToSingle( o);
    }

    private int ExecuteScalarInt(bool signed, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return int.MaxValue;
            }
            else
            {
                return -1;
            }
        }
        return Convert.ToInt32( o);
    }

    private long ExecuteScalarLong(bool signed, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return long.MaxValue;
            }
            else
            {
                return -1;
            }
        }
        return Convert.ToInt64( o);
    }

    private bool? ExecuteScalarNullableBool(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return null;
        }
        return Convert.ToBoolean( o);
    }

    private short ExecuteScalarShort(bool signed, SqlCommand comm)
    {
        var o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return short.MaxValue;
            }
            else
            {
                return -1;
            }
        }
        return Convert.ToInt16( o);
    }

    private string ExecuteScalarString(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return "";
        }
        else if (o == DBNull.Value)
        {
            return "";
        }
        return o.ToString().TrimEnd(' ');
    }
    #endregion

    #region Insert
    /// <summary>
    /// 1
    /// Do této metody se vkládají hodnoty bez ID
    /// Vrátí mi nejmenší volné číslo tabulky A1
    /// Pokud bude obsazene 1,3, vrátí až 4
    /// ID se počítá jako v Sqlite - tedy od 1 
    /// A2 je zde proto aby se mohlo určit poslední index a ten inkrementovat a na ten vložit. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když ID sloupec má nějaký speciální název, např. IDUsers
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public long Insert(string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        bool signed = false;

        return Insert1(tabulka, idt, sloupecID, sloupce, signed);
    }

    public long InsertSigned(string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        return Insert1(tabulka, idt, sloupecID, sloupce, true);
    }

    private long Insert1(string tabulka, Type idt, string sloupecID, object[] sloupce, bool signed)
    {
        string hodnoty = MSDatabaseLayer.GetValuesDirect(sloupce.Length + 1);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        bool totalLower = false;
        object d = SelectLastIDFromTableSigned(signed, tabulka, idt, sloupecID, out totalLower);
        int pricist = 0;
        if (!totalLower)
        {
            pricist = 1;
        }
        else if(idt == Consts.tByte)
        {
            pricist = 1;
        }
        if (idt == typeof(Byte))
        {
            Byte b =  Convert.ToByte( d);
            comm.Parameters.AddWithValue("@p0", b + pricist);
        }
        else if (idt == typeof(Int16))
        {
            Int16 i1 = Convert.ToInt16( d);
            comm.Parameters.AddWithValue("@p0", i1 + pricist);
        }
        else if (idt == typeof(Int32))
        {
            Int32 i2 = Convert.ToInt32( d);
            comm.Parameters.AddWithValue("@p0", i2 + pricist);
        }
        else if (idt == typeof(Int64))
        {
            Int64 i3 = Convert.ToInt64( d);
            comm.Parameters.AddWithValue("@p0", i3 + pricist);
        }
        int to = sloupce.Length + 1;
        for (int i = 1; i < to; i++)
        {
            object o = sloupce[i - 1];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        ExecuteNonQuery(comm);

        long vr = Convert.ToInt64( d);
        vr += pricist;
        return vr;
    }

    /// <summary>
    /// 2
    /// Tato metoda je vyjímečná, vkládá hodnoty signed, hodnotu kterou vložit si zjistí sám a vrátí ji.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public long Insert2(string tabulka, string sloupecID, Type typSloupecID, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValuesDirect(sloupce.Length + 1);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        //bool totalLower = false;
        var l = SelectLastIDFromTableSigned2(tabulka, typSloupecID, sloupecID);

        long id = Convert.ToInt64( l);
        AddCommandParameter(comm, 0, id);
        for (int i = 0; i < sloupce.Length; i++)
        {
            AddCommandParameter(comm, i + 1, sloupce[i]);
        }
        ExecuteNonQuery(comm);
        return id;
    }


    /// <summary>
    /// 3
    /// A2 může být ID nebo cokoliv začínající na ID(ID*)
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever A2 už nesmí být v A3
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public void Insert3(string tabulka, long IDUsers, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(CA.JoinVariableAndArray(IDUsers, sloupce));

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        comm.Parameters.AddWithValue("@p0", IDUsers);
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i + 1, o);
            //DateTime.Now.Month;
        }
        ExecuteNonQuery(comm);
    }

        /// <summary>
        /// Stjená jako 3, jen ID* je v A2 se všemi
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupce"></param>
    public void Insert4(string tabulka, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        ExecuteNonQuery(comm);
        //return Convert.ToInt64( sloupce[0]);
    }

    public long Insert5(string table, string nazvySloupcu, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} {2} VALUES {1}", table, hodnoty, nazvySloupcu));

        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        ExecuteNonQuery(comm);
        return Convert.ToInt64( sloupce[0]);
    }

    /// <summary>
    /// Jediná metoda kde můžeš specifikovat sloupce do kterých chceš vložit
    /// Sloupec který nevkládáš musí být auto_increment
    /// ÏD si pak musíš zjistit sám pomocí nějaké identifikátoru - například sloupce Uri
    /// </summary>
    /// <param name="table"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public void Insert6(string table, string nazvySloupcu, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} {2} VALUES {1}", table, hodnoty, nazvySloupcu.Replace("(", "(newid(),")));

        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        ExecuteNonQuery(comm);
        //return Convert.ToInt64( sloupce[0]);
    }


    public int InsertRowTypeEnum(string tabulka, string nazev)
    {
        int vr = SelectFindOutNumberOfRows(tabulka) + 1;
        SqlCommand c = new SqlCommand(string.Format("INSERT INTO {0} (ID, Name) VALUES (@p0, @p1)", tabulka));
        AddCommandParameter(c, 0, vr);
        AddCommandParameter(c, 1, nazev);
        ExecuteNonQuery(c);
        return vr;
    }

    /// <summary>
    /// Raději používej metodu s 3/2A sloupecID, pokud používáš v tabulce sloupce ID, které se nejmenují ID
    /// Sloupec u kterého se bude určovat poslední index a ten inkrementovat a na ten vkládat je ID
    /// Používej tehdy když ID sloupec má nějaký standardní název, Tedy ID, ne IDUsers atd.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public Guid InsertToRowGuid(string tabulka, params object[] sloupce)
    {
        return InsertToRowGuid2(tabulka, "ID", sloupce);
    }

    /// <summary>
    /// Do této metody se vkládají hodnoty bez ID
    /// ID se počítá jako v Sqlite - tedy od 1 
    /// A2 je zde proto aby se mohlo určit poslední index a ten inkrementovat a na ten vložit. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když ID sloupec má nějaký speciální název, např. IDUsers
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public Guid InsertToRowGuid2(string tabulka, string sloupecID, params object[] sloupce)
    {
        int hodnotyLenght = sloupce.Length + 1;
        string hodnoty = MSDatabaseLayer.GetValuesDirect(hodnotyLenght);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        for (int i = 1; i < hodnotyLenght; i++)
        {
            object o = sloupce[i - 1];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        Guid vr = SelectNewId();
        AddCommandParameter(comm, 0, vr);

        ExecuteNonQuery(comm);
        return vr;
    }

    /// <summary>
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public void InsertToRowGuid3(string tabulka, Guid IDUsers, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(CA.JoinVariableAndArray(IDUsers, sloupce));

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        comm.Parameters.AddWithValue("@p0", IDUsers);
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i + 1, o);
            //DateTime.Now.Month;
        }
        ExecuteNonQuery(comm);
    }
    #endregion

    #region SelectColumn
    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, string whereSloupec, object whereHodnota, string orderBy = "")
    {
        if (orderBy != "")
        {
            orderBy = " " + orderBy;
        }
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.SimpleWhere(whereSloupec) + orderBy);
        AddCommandParameter(comm, 0, whereHodnota);
        return ReadValuesString(comm);
    }

    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, ABC where, ABC whereIsNot, string orderBy = "")
    {
        if (orderBy != "")
        {
            orderBy = " " + orderBy;
        }
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(where, whereIsNot, null, null) + orderBy);
        AddCommandParameteresCombinedArrays(comm, 0, where, whereIsNot, null, null);
        return ReadValuesString(comm);
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesInt(comm);
    }

    public IList SelectValuesOfColumnAllRowsNumeric(string tabulka, string sloupec, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1}", sloupec, tabulka));
        object[] o = SelectRowReader(comm);
        if (o == null)
        {
            return new List<long>();
        }
        Type t = o[0].GetType();
        comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameteres(comm, 0, ab);
        if (t == Consts.tInt)
        {
            //snt = SqlNumericType.Int;
            return ReadValuesInt(comm);
        }
        else if (t == Consts.tLong)
        {
            //snt = SqlNumericType.Long;
            return ReadValuesLong(comm);
        }
        else if (t == Consts.tShort)
        {
            //snt = SqlNumericType.Short;
            return ReadValuesShort(comm);
        }
        return ReadValuesByte(comm);
    }

    public IList SelectValuesOfColumnAllRowsNumeric(string tabulka, string sloupec)
    {
        return SelectValuesOfColumnAllRowsNumeric(tabulka, sloupec, new AB[0]);
    }



    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesShort(comm);
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesShort(comm);
    }


    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, int limit, string sloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP("+limit+") {0} FROM {1} WHERE {2} = @p0", sloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesShort(comm);
    }


    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, int limit, string sloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + limit + ") {0} FROM {1} WHERE {2} = @p0", sloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesInt(comm);
    }

    public int RandomValueFromColumnInt(string table, string column)
    {
        return ExecuteScalarInt(true, new SqlCommand("select " + column + " from " + table + " where " + column + " in (select top 1 " + column + " from " + table + " order by newid())"));
    }

    public short RandomValueFromColumnShort(string table, string column)
    {
        return ExecuteScalarShort(true, new SqlCommand("select " + column + " from " + table + " where " + column + " in (select top 1 " + column + " from " + table + " order by newid())"));
    }

    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka,  string sloupec, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs.ToArray(), whereIsNot.ToArray(), null, null);
        return ReadValuesShort(comm);
    }
    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs.ToArray(), whereIsNot.ToArray(), null, null);
        return ReadValuesInt(comm);
    }

    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec, AB[] whereIs, AB[] whereIsNot, AB[] greaterThanWhere, AB[] lowerThanWhere)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, greaterThanWhere, lowerThanWhere));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, greaterThanWhere, lowerThanWhere);
        return ReadValuesInt(comm);
    }
    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", sloupec, tabulka, GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameterFromAbc(comm, ab);
        return ReadValuesShort(comm);
    }
    
    /// <summary>
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsStringTrim
    /// </summary>
    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesString(comm);
    }

    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec, string idn, object idv)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.SimpleWhere(idn));
        AddCommandParameter(comm, 0, idv);
        return ReadValuesStringTrim(comm);
    }

    /// <summary>
    /// Tato metoda má navíc možnost specifikovat simple where.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string hledanySloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.SimpleWhere(idColumn)));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesInt(comm);
    }

    public List<long> SelectValuesOfColumnAllRowsLong(bool signed, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());

        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB)));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesLong(comm);
    }

    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB)));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesInt(comm);
    }

    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, int maxRows, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());

        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + maxRows + ") {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB)));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesInt(comm);
    }

    /// <summary>
    /// Pokud bude buňka DBNull, nebudu ukládat do G nic
    /// </summary>
    /// <param name="table"></param>
    /// <param name="returnColumns"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public List<DateTime> SelectValuesOfColumnAllRowsDateTime(string table, string returnColumns, params AB[] where)
    {
        string hodnoty = MSDatabaseLayer.GetValues(where.ToArray());

        List<DateTime> vr = new List<DateTime>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", returnColumns, table, GeneratorMsSql.CombinedWhere(where)));
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
        return ReadValuesDateTime(comm);
    }

    /// <summary>
    /// Pokud řádek ve sloupci A2 má hodnotu DBNull.Value, zapíšu do výsledku 0
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="aB1"></param>
    /// <param name="aB2"></param>
    /// <returns></returns>
    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, object vetsiNez, object mensiNez, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());
        //"OrderVerse"
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB, null, new ABC(AB.Get(hledanySloupec, vetsiNez)).ToArray(), new ABC(AB.Get(hledanySloupec, mensiNez)).ToArray())));
        int i = 0;
        for (; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        i = AddCommandParameter(comm, i, vetsiNez);
        i = AddCommandParameter(comm, i, mensiNez);
        return ReadValuesByte(comm);
    }

    /// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt která je úplně stejná
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecHledaný"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnInt(bool signed, string tabulka, string sloupecHledaný, params AB[] abc)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", sloupecHledaný, tabulka, GeneratorMsSql.CombinedWhere(abc)));
        for (int i = 0; i < abc.Length; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        return ReadValuesInt(comm);
    }

    public List<byte> SelectValuesOfColumnByte(string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        // SQLiteDataReader je třída zásadně pro práci s jedním řádkem výsledků, ne s 2mi a více !!
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        return ReadValuesByte(comm);
    }

    /// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt, která je úplně stejná
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecHledaný"></param>
    /// <param name="sloupecVeKteremHledat"></param>
    /// <param name="hodnota"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnInt(bool signed, string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        return ReadValuesInt(comm);
    }

    private List<int> ReadValuesInt(SqlCommand comm)
    {
        #region Pomocí SqlDataReader
        List<int> vr = new List<int>();
        SqlDataReader r = null;
        r = ExecuteReader(comm);
        if (r.HasRows)
        {
            while (r.Read())
            {
                int o = r.GetInt32(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
        #endregion
    }

    public List<string> ReadValuesStringTrim(SqlCommand comm)
    {
        List<string> vr = new List<string>();
        SqlDataReader r = ExecuteReader(comm); ;

        if (r.HasRows)
        {
            while (r.Read())
            {
                string o = r.GetString(0).TrimEnd(' ');
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
    }

    private List<string> ReadValuesString(SqlCommand comm)
    {
        List<string> vr = new List<string>();
        SqlDataReader  r = ExecuteReader(comm); ;
        
        if (r.HasRows)
        {
            while (r.Read())
            {
                string o = r.GetString(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
    }

    private List<byte> ReadValuesByte(SqlCommand comm)
    {
        List<byte> vr = new List<byte>();
        SqlDataReader r = ExecuteReader(comm); ;

        if (r.HasRows)
        {
            while (r.Read())
            {
                byte o = r.GetByte(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
    }

    private List<DateTime> ReadValuesDateTime(SqlCommand comm)
    {
        List<DateTime> vr = new List<DateTime>();
        SqlDataReader r = ExecuteReader(comm);

        if (r.HasRows)
        {
            while (r.Read())
            {
                DateTime o = r.GetDateTime(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
    }

    private List<long> ReadValuesLong(SqlCommand comm)
    {
        List<long> vr = new List<long>();
        SqlDataReader r = ExecuteReader(comm);

        if (r.HasRows)
        {
            while (r.Read())
            {
                long o = r.GetInt64(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
    }

    private List<short> ReadValuesShort(SqlCommand comm)
    {
        List<short> vr = new List<short>();
        SqlDataReader r = ExecuteReader(comm);

        if (r.HasRows)
        {
            while (r.Read())
            {
                short o = r.GetInt16(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        return vr;
    }
    #endregion

    #region Práce s tabulkami a databází
    /// <summary>
    /// Vymže tabulku A1 a přejmenuje tabulku A1+"2" na A1
    /// </summary>
    /// <param name="table"></param>
    public void sp_rename(string table)
    {
        DropTableIfExists(table);
        SqlCommand comm = new SqlCommand("EXEC sp_rename 'dbo." + table + "2', '" + table + "'");
        ExecuteNonQuery(comm);
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectGetAllTablesInDB()
    {
        List<string> vr = new List<string>();
        DataTable dt = SelectDataTableSelective("INFORMATION_SCHEMA.TABLES", "TABLE_NAME", "TABLE_TYPE", "BASE TABLE");
        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[0].ToString());
        }
        return vr;
    }

    

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectColumnsNamesOfTable(string p)
    {
        List<string> vr = new List<string>();

        DataTable dt = SelectDataTableSelective("INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME", "TABLE_NAME", p);
        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[0].ToString());
        }
        return vr;
    }

    public bool SelectExistsTable(string p)
    {
        DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        return dt.Rows.Count != 0;
    }
    private DataTable SelectDataTable(SqlConnection conn, string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }
        return SelectDataTable(conn, comm);
        //return SelectDataTable(string.Format(sql, _params));
    }
    public DataTable SelectDataTable(SqlConnection conn, SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);
        return dt;
    }
    public bool SelectExistsTable(string p, SqlConnection conn)
    {
        DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        return dt.Rows.Count != 0;
    }
    #endregion

    #region SelectTable
    /// <summary>
    /// Pokud chceš použít OrderBy, je tu metoda SelectDataTableLimitLastRows nebo SelectDataTableLimitLastRowsInnerJoin
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// Vyplň A2 na SE pokud chceš všechny sloupce
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", nazvySloupcu, tabulka) + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameterFromAbc(comm, ab);
        //NT
        return this.SelectDataTable(comm);
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return this.SelectDataTable(comm);
    }

    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id, string orderByColumn, SortOrder sortOrder)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID) + GeneratorMsSql.OrderBy(orderByColumn, sortOrder));
        AddCommandParameter(comm, 0, id);
        //NT
        return this.SelectDataTable(comm);
    }

    /// <summary>
    /// 
    /// </summary>
    public DataTable SelectDataTableLimit(string tableName, int limit, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") * FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }

    

    /// <summary>
    /// A2 je sloupec na který se prohledává pro A3
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="textPartOfCity"></param>
    /// <param name="nazvySloupcu"></param>
    /// <returns></returns>
    public DataTable SelectDataTableSelectiveLikeContains(string tabulka, string nazvySloupcu, string sloupecID, string textPartOfCity)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} LIKE '%' + @p0 + '%'", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, textPartOfCity);
        //NT
        return this.SelectDataTable(comm);
    }

    public DataTable SelectAllRowsOfColumnsAB(string table, string vratit, ABC abObsahuje, ABC abNeobsahuje, ABC abVetsiNez, ABC abMensiNez)
    {
        string sql = "SELECT " + vratit + " FROM " + table;
        sql += GeneratorMsSql.CombinedWhere(abObsahuje, abNeobsahuje, abVetsiNez, abMensiNez);
        SqlCommand comm = new SqlCommand(sql);

        int i = 0;
        i = AddCommandParameterFromAbc(comm, abObsahuje, i);
        i = AddCommandParameterFromAbc(comm, abNeobsahuje, i);
        i = AddCommandParameterFromAbc(comm, abVetsiNez, i);
        AddCommandParameterFromAbc(comm, abVetsiNez, i);

        return SelectDataTable(comm);
    }

    public DataTable SelectDataTableSelective(string table, string vraceneSloupce, AB[] where, AB[] whereIsNot)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT " + vraceneSloupce);
        sb.Append(" FROM " + table);
        int dd = 0;
        sb.Append(GeneratorMsSql.CombinedWhere(where, ref dd));
        sb.Append(GeneratorMsSql.CombinedWhereNotEquals(where != null, ref dd, whereIsNot));
        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        //AddCommandParameter(comm, 0, idColumnValue);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    public DataTable SelectDataTableSelective(string table, string vraceneSloupce, AB[] where, AB[] whereIsNot, AB[] greaterThan, AB[] lowerThan)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT " + vraceneSloupce);
        sb.Append(" FROM " + table);
        sb.Append(GeneratorMsSql.CombinedWhere(where, whereIsNot, greaterThan, lowerThan));

        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresArrays(comm, 0, where, whereIsNot, greaterThan, lowerThan);
        //AddCommandParameter(comm, 0, idColumnValue);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecOrder"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRows(string tableName, int limit, string columns, string sloupecOrder, params AB[] where)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(where) + " ORDER BY " + sloupecOrder + " DESC");
        AddCommandParameteres(comm, 0, where);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecOrder"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRows(string tableName, int limit, string columns, string sloupecOrder, AB[] whereIs, AB[] whereIsNot, AB[] whereGreaterThan, AB[] whereLowerThan)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, whereGreaterThan, whereLowerThan) + " ORDER BY " + sloupecOrder + " DESC");
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, whereGreaterThan, whereLowerThan);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// 2
    /// </summary>
    public DataTable SelectAllRowsOfColumns(string p, string selectSloupce)
    {
        return SelectDataTable(string.Format("SELECT {0} FROM {1}", selectSloupce, p));
    }

    public DataTable SelectAllRowsOfColumns(string p, string ziskaneSloupce, string idColumnName, object idColumnValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} ", ziskaneSloupce, p) + GeneratorMsSql.SimpleWhere(idColumnName));
        AddCommandParameter(comm, 0, idColumnValue);
        return SelectDataTable(comm);
    }
    public DataTable SelectAllRowsOfColumns(string p, string ziskaneSloupce, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} ", ziskaneSloupce, p) + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameteres(comm, 0, ab);
        return SelectDataTable(comm);
    }
    /// <summary>
    /// Vrátí mi všechny položky ze sloupce 
    /// </summary>
    public DataTable SelectGreaterThan(string tableName, string tableColumn, object hodnotaOd)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT * FROM {0} WHERE {1} > @p0", tableName, tableColumn));
        AddCommandParameter(comm, 0, hodnotaOd);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Tato metoda má přívlastek Columns protože v ní jde specifikovat sloupce které má metoda vrátit
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupce"></param>
    /// <param name="sloupecWhere"></param>
    /// <param name="hodnotaWhere"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitColumns(string tableName, int limit, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + sloupce + " FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }



    #endregion

    #region SelectTableInnerJoin
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params object[] fixniHodnotyOdNuly)
    {
        return SelectDataTable("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly, fixniHodnotyOdNuly);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// </summary>
    /// <param name="tableFromWithShortVersion"></param>
    /// <param name="tableJoinWithShortVersion"></param>
    /// <param name="sloupceJezZiskavat"></param>
    /// <param name="onKlazuleOdNuly"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecPodleKterehoRadit"></param>
    /// <param name="whereIs"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRowsInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, int limit, string sloupecPodleKterehoRadit, AB[] whereIs, AB[] whereIsNot, params object[] hodnotyOdNuly)
    {
        SqlCommand comm = new SqlCommand("select TOP(" + limit.ToString() + ") " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null) + " ORDER BY " + sloupecPodleKterehoRadit + " DESC");
        AddCommandParameteres(comm, 0, hodnotyOdNuly);
        AddCommandParameteresCombinedArrays(comm, hodnotyOdNuly.Length, whereIs, whereIsNot, null, null);

        return SelectDataTable(comm);
    }

    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, AB[] whereIs, AB[] whereIsNot)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);

        return SelectDataTable(comm);
    }

    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// Tato metoda zde musí být, jinak vzniká chyba No mapping exists from object type AB to a known managed provider native type.
    /// </summary>
    /// <param name="tableFromWithShortVersion"></param>
    /// <param name="tableJoinWithShortVersion"></param>
    /// <param name="sloupceJezZiskavat"></param>
    /// <param name="onKlazuleOdNuly"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(where));
        AddCommandParameterFromAbc(comm, where);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// POkud nechceš používat reader, který furt nefugnuje, použij metodu SelectOneRowInnerJoin, má úplně stejnou hlavičku a jen funguje s DataTable
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public object[] SelectOneRowInnerJoinReader(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(where));
        AddCommandParameterFromAbc(comm, where);
        return SelectRowReader(comm);
    }

    public object[] SelectOneRowInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(where));
        AddCommandParameterFromAbc(comm, where);
        DataTable dt = SelectDataTable(comm);
        if (dt.Rows.Count == 0)
        {
            return null; // CA.CreateEmptyArray(pocetSloupcu);
        }
        return dt.Rows[0].ItemArray;
    }

    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly);
        //AddCommandParameterFromAbc(comm, where);
        return SelectDataTable(comm);
    }


    #endregion

    #region Update
    public void UpdateValuesCombinationCombinedWhere(string TableName, AB[] sets, AB[] where)
    {
        string setString = GeneratorMsSql.CombinedSet(sets);
        string whereString = GeneratorMsSql.CombinedWhere(where);
        int indexParametrWhere = sets.Length + 1;
        SqlCommand comm = new SqlCommand(string.Format("UPDATE {0} {1} WHERE {2}", TableName, setString, whereString));
        for (int i = 0; i < indexParametrWhere; i++)
        {
            // V takových případech se nikdy nepokoušej násobit, protože to vždy končí špatně
            AddCommandParameter(comm, i, sets[i].B);
        }
        indexParametrWhere--;
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, indexParametrWhere++, where[i].B);
        }
        // NT-Při úpravách uprav i UpdateValuesCombination
        ExecuteNonQuery(comm);
    }

    /// <summary>
    /// Pokud se řádek nepodaří najít, vrátí -1
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="pridej"></param>
    /// <returns></returns>
    public float UpdateRealValue(string table, string sloupecID, int id, string sloupecKUpdate, float pridej)
    {
        float d = SelectCellDataTableFloatOneRow(true, table, sloupecKUpdate, AB.Get(sloupecID, id));
        if (d != 0 && d != -1 && d != float.MaxValue)
        {
            pridej = (d + pridej) / 2;
        }
        else
        {
            // Zde to má být prázdné
            return -1;
        }
        Update(table, sloupecKUpdate, pridej, sloupecID, id);
        return pridej;
    }

    /// <summary>
    /// Updatuje pouze 1 řádek
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="updateColumn"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public bool UpdateSwitchBool(string tablename, string updateColumn, string idColumn, object idValue)
    {
        bool vr = !SelectCellDataTableBoolOneRow(tablename, idColumn, idValue, updateColumn);
        UpdateOneRow(tablename, updateColumn, vr, idColumn, idValue);
        return vr;
    }

    /// <summary>
    /// 
    /// </summary>
    public int UpdateAppendStringValue(string tableName, string sloupecID, object hodnotaID, string sloupecAppend, string hodnotaAppend)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecAppend, sloupecID, hodnotaID);
        aktual += hodnotaAppend;
        return UpdateOneRow(tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
    }

    /// <summary>
    /// Vrátí nohou hodnotu v DB
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="pridej"></param>
    /// <returns></returns>
    public int UpdateSumIntValue(string table, string sloupecID, object id, string sloupecKUpdate, int pridej)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, AB.Get(sloupecID, id));
        if (d == int.MaxValue)
        {
            return d;
        }
        int n = pridej + d;
        Update(table, sloupecKUpdate, n, sloupecID, id);
        return n;
    }

    public long UpdateSumLongValue(string table, string sloupecID, object id, string sloupecKUpdate, int pridej)
    {
        long d = SelectCellDataTableLongOneRow(false, table, sloupecID, id, sloupecKUpdate);
        long n = pridej + d;
        Update(table, sloupecKUpdate, n, sloupecID, id);
        return n;
    }

    /// <summary>
    /// Nahrazuje ve všech řádcích
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public short UpdateMinusShortValue(string table, string sloupecKUpdate, short odeber, params AB[] abc)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecKUpdate, abc);
        if (d == short.MinValue)
        {
            return d;
        }

        odeber = (short)(d - odeber);
        Update(table, sloupecKUpdate, odeber, abc);
        return odeber;
    }

    /// <summary>
    /// Vrací normalizovaný short
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public ushort UpdateMinusNormalizedShortValue(string table, string sloupecKUpdate, ushort odeber, params AB[] abc)
    {
        ushort d = NormalizeNumbers.NormalizeShort(SelectCellDataTableShortOneRow(true, table, sloupecKUpdate, abc));
        if (d == NormalizeNumbers.NormalizeShort( short.MinValue))
        {
            return d;
        }

        odeber = (ushort)(d - odeber);
        Update(table, sloupecKUpdate, odeber, abc);
        return odeber;
    }

    public short UpdateMinusShortValue(string table, string sloupecKUpdate, short odeber, string sloupecID, object hodnotaID)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == short.MaxValue)
        {
            odeber = 0;
        }
        else
        {
            odeber = (short)(d - odeber);
        }
        Update(table, sloupecKUpdate, odeber, sloupecID, hodnotaID);
        return odeber;
    }

    

    public int UpdatePlusIntValue(string table, string sloupecKUpdate, int pridej, params AB[] abc)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, abc);
        if (d == int.MaxValue)
        {
            return d;
        }
        int n = pridej;
        n = d + pridej;
        Update(table, sloupecKUpdate, n, abc);
        return n;
    }

    public int UpdatePlusIntValue(string table, string sloupecKUpdate, int pridej, string sloupecID, object hodnotaID)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);

        if (d == int.MaxValue)
        {
            return d;
        }
        int n = pridej;
        n = d + pridej;
        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    /// <summary>
    /// Aktualizuje všechny řádky
    /// Vrátí novou zapsanou hodnotu
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public int UpdateMinusIntValue(string table, string sloupecKUpdate, int odeber, params AB[] abc)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, abc);
        if (d == int.MaxValue)
        {
            return d;
        }
        int n = odeber;
        n = d - odeber;

        Update(table, sloupecKUpdate, n, abc);
        return n;
    }

    public int UpdateMinusIntValue(string table, string sloupecKUpdate, int odeber, string sloupecID, object hodnotaID)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);
        if (d == int.MinValue)
        {
            return d;
        }
        int n = odeber;
        n = d - odeber;

        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    public long UpdateMinusLongValue(string table, string sloupecKUpdate, long odeber, string sloupecID, object hodnotaID)
    {
        long d = SelectCellDataTableLongOneRow(true, table, sloupecID, hodnotaID, sloupecKUpdate);

        if (d == long.MaxValue)
        {
            return d;
        }
        long n = odeber;
        n = d - odeber;

        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    public short UpdatePlusShortValue(string table, string sloupecKUpdate, short pridej, string sloupecID, object hodnotaID)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == short.MaxValue)
        {
            return d;
        }
        short n = pridej;
        n = (short)(d + pridej);
        //}
        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    public byte UpdateMinusByteValue(string table, string sloupecKUpdate, byte pridej, string sloupecID, object hodnotaID)
    {
        byte d = SelectCellDataTableByteOneRow(table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == 255)
        {
            return d;
        }
        else
        {
            pridej = (byte)(d + pridej);
        }
        Update(table, sloupecKUpdate, pridej, sloupecID, hodnotaID);
        return pridej;
    }

    /// <summary>
    /// Pouze když hodnota nebude existovat, přidá ji znovu
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="sloupecID"></param>
    /// <param name="hodnotaID"></param>
    /// <param name="sloupecAppend"></param>
    /// <param name="hodnotaAppend"></param>
    /// <returns></returns>
    public int UpdateAppendStringValueCheckExistsOneRow(string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecAppend, sloupecID, hodnotaID);

        List<string> d = new List<string>(SH.Split(aktual, ","));
        if (!d.Contains(hodnotaAppend))
        {
            aktual += hodnotaAppend + ",";
            string save = SH.Join(',', d.ToArray());

            return UpdateOneRow(tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
        }
        return 0;
    }
    /// <summary>
    /// Pokud nenajde, vrátí DateTime.MinValue
    /// Do A4 zadej DateTime.MinValue pokud nevíš - je to původní hodnota
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public DateTime SelectMaxDateTime(string table, string column, AB[] whereIs, AB[] whereIsNot, DateTime getIfNotFound)
    {
        SqlCommand comm = new SqlCommand("SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return ExecuteScalarDateTime(getIfNotFound, comm);
    }

    public void DropAndCreateTable(string p, MSColumnsDB msc, SqlConnection conn)
    {
        DropTableIfExists(p);
        msc.GetSqlCreateTable(p, false, conn).ExecuteNonQuery();
    }

    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string sloupec, int maxRows, AB[] whereIs, AB[] whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP({2}) {0} FROM {1}", sloupec, tabulka, maxRows) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        MSStoredProceduresI.AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return ReadValuesInt(comm);
    }
    public DataTable SelectDateTableGroupBy(string table, string columns, string groupByColumns)
    {
        string sql = "select " + columns + " from " + table + " group by " + groupByColumns;
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        return SelectDataTable(comm);
    }
    /// <summary>
    /// 
    /// </summary>
    public int UpdateCutStringValue(string tableName, string sloupecCut, string hodnotaCut, string sloupecID, object hodnotaID)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecCut, sloupecID, hodnotaID);

        List<string> d = new List<string>(SH.Split(aktual, ","));
        d.Remove(hodnotaCut);
        string save = SH.JoinWithoutTrim(",", d);
        return UpdateOneRow(tableName, sloupecCut, save, sloupecID, hodnotaID);
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public int Update(string table, string sloupecKUpdate, object n, string sloupecID, object id)
    {
        string sql = string.Format("UPDATE {0} SET {1}=@p0 WHERE {2} = @p1", table, sloupecKUpdate, sloupecID);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, n);
        AddCommandParameter(comm, 1, id);
        //SqlException: String or binary data would be truncated.
        return ExecuteNonQuery(comm);
    }

    /// <summary>
    /// Conn přidá automaticky
    /// Název metody je sice OneRow ale updatuje to libovolný počet řádků které to najde pomocí where - je to moje interní pojmenování aby mě to někdy trklo, možná později přijdu na způsob jak updatovat jen jeden řádek.
    /// </summary>
    public int UpdateOneRow(string table, string sloupecKUpdate, object n, string sloupecID, object id)
    {
        string sql = string.Format("UPDATE TOP(1) {0} SET {1}=@p1 WHERE {2} = @p2", table, sloupecKUpdate, sloupecID);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 1, n);
        AddCommandParameter(comm, 2, id);
        return ExecuteNonQuery(comm);
    }

    public int UpdateOneRow(string table, string sloupecKUpdate, object n, params AB[] ab)
    {
        int pridavatOd = 1;
        string sql = string.Format("UPDATE TOP(1) {0} SET {1}=@p0", table, sloupecKUpdate) + GeneratorMsSql.CombinedWhere(ab, ref pridavatOd);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, n);
        AddCommandParameteres(comm, 1, ab);
        return ExecuteNonQuery(comm);
    }

    private int Update(string table, string sloupecKUpdate, int n, AB[] abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, sloupecKUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, n);
        for (int i = 0; i < parametrSet; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        int vr = ExecuteNonQuery(comm);
        return vr;
    }

    public int UpdateWhereIsLowerThan(string table, string columnToUpdate, object newValue, string columnLowerThan, DateTime valueLowerThan, params AB[] where)
    {
        AB[] lowerThan = CA.ToArrayT<AB>(AB.Get(columnLowerThan, valueLowerThan));
        int parametrSet = lowerThan.Length + 1;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(where, null, null, lowerThan));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, null, lowerThan);
        
        int vr = ExecuteNonQuery(comm);
        return vr;
    }

    public int Update(string table, string columnToUpdate, object newValue, params AB[] abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);
        for (int i = 0; i < parametrSet; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        int vr = ExecuteNonQuery(comm);
        return vr;
    }

    public int UpdateMany(string table, string columnID, object valueID, params AB[] update)
    {
        int vr = 0;
        foreach (AB item in update)
        {
            
            string sql = string.Format("UPDATE {0} SET {1} = @p0 {2}", table, item.A, GeneratorMsSql.SimpleWhere("ID", 1));
            SqlCommand comm = new SqlCommand(sql);
            AddCommandParameter(comm, 0, item.B);
            AddCommandParameter(comm, 1, valueID);
            
              vr += ExecuteNonQuery(comm);
        }
        return vr;
    }

    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params object[] setsNameValue)
    {
        ABC abc = new ABC(setsNameValue);
        UpdateValuesCombination(TableName, nameOfColumn, valueOfColumn, abc.ToArray());
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params AB[] sets)
    {
        string setString = GeneratorMsSql.CombinedSet(sets);
        //int pocetParametruSets = sets.Length;
        int indexParametrWhere = sets.Length;
        SqlCommand comm = new SqlCommand(string.Format("UPDATE {0} {1} WHERE {2}={3}", TableName, setString, nameOfColumn, "@p" + (indexParametrWhere).ToString()));
        for (int i = 0; i < indexParametrWhere; i++)
        {
            // V takových případech se nikdy nepokoušej násobit, protože to vždy končí špatně
            AddCommandParameter(comm, i, sets[i].B);
        }
        AddCommandParameter(comm, indexParametrWhere, valueOfColumn);
        // NT-Při úpravách uprav i UpdateValuesCombinationCombinedWhere
        ExecuteNonQuery(comm);
    }
    #endregion

    #region SelectLastIDFromTable+Min/Max
    /// <summary>
    /// Pracuje jako signed.
    /// Vrací skutečně nejvyšší ID, proto když chceš pomocí ní ukládat do DB, musíš si to číslo inkrementovat
    /// Ignoruje vynechaná čísla. Žádná hodnota v sloupci A2 nebyla nalezena, vrátí long.MaxValue
    /// </summary>
    /// <param name="p"></param>
    /// <param name="sloupecID"></param>
    /// <returns></returns>
    public long SelectLastIDFromTable(string p, string sloupecID)
    {
        return ExecuteScalarLong(true, new SqlCommand("SELECT MAX(" + sloupecID + ") FROM " + p));
    }

    /// <summary>
    /// Vrátí všechny hodnoty z sloupce A3 a pak počítá od A2.MinValue až narazí na hodnotu která v tabulce nebyla, tak ji vrátí
    /// Proto není potřeba vr nijak inkrementovat ani jinak měnit
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <returns></returns>
    public object SelectLastIDFromTableSigned2(string table, Type idt, string sloupecID)
    {
        if (idt == typeof(short))
        {
            short vratit = short.MaxValue;
            List<short> all = SelectValuesOfColumnAllRowsShort(table, sloupecID);
            //all.Sort();
            for (short i = short.MinValue; i < short.MaxValue; i++)
            {
                if (!all.Contains(i))
                {
                    return i;
                }
            }
            return vratit;
        }
        else if (idt == typeof(int))
        {
            int vratit = int.MaxValue;
            List<int> all = SelectValuesOfColumnAllRowsInt(table, sloupecID);
            //all.Sort();
            for (int i = int.MinValue; i < int.MaxValue; i++)
            {
                if (!all.Contains(i))
                {
                    return i;
                }
            }
            return vratit;
        }
        else if (idt == typeof(long))
        {
            long vratit = long.MaxValue;
            List<long> all = SelectValuesOfColumnAllRowsLong(true, table, sloupecID);
            //all.Sort();
            for (long i = long.MinValue; i < long.MaxValue; i++)
            {
                if (!all.Contains(i))
                {
                    return i;
                }
            }
            return vratit;
        }
        else
        {
            throw new Exception("V klazuli if v metodě MSStoredProceduresIBase.SelectLastIDFromTableSigned nebyl nalezen typ " + idt.FullName.ToString());
        }
    }

    /// <summary>
    /// Nedá se použít na desetinné typy
    /// Vrátí mi nejmenší volné číslo tabulky A1
    /// Pokud bude obsazene 1,3, vrátí až 4
    /// </summary>
    /// <param name="p"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <param name="totalLower"></param>
    /// <returns></returns>
    public object SelectLastIDFromTableSigned(bool signed, string p, Type idt, string sloupecID, out bool totalLower)
    {
        totalLower = false;
        string dd = ExecuteScalar(new SqlCommand("SELECT MAX(" + sloupecID + ") FROM " + p)).ToString();
        if (dd == "")
        {
            totalLower = true;
            object vr = 0;
            if (signed)
            {
                vr = BTS.GetMinValueForType(idt);
            }

            if (idt == Consts.tShort)
            {
                //short s = (short)vr;
                return vr;
            }
            else if (idt == Consts.tInt)
            {
                //int nt = (int)vr;
                return vr;
            }
            else if (idt == Consts.tByte)
            {
                return vr;
            }
            else if (idt == Consts.tLong)
            {
                //long lng = (long)vr;
                return vr;
            }
            else
            {
                throw new Exception("V klazuli if v metodě MSStoredProceduresIBase.SelectLastIDFromTableSigned nebyl nalezen typ " + idt.FullName.ToString());
            }
        }
        if (idt == typeof(Byte))
        {
            return Byte.Parse(dd);
        }
        else if (idt == typeof(Int16))
        {
            return Int16.Parse(dd);
        }
        else if (idt == typeof(Int32))
        {
            return Int32.Parse(dd);
        }
        else if (idt == typeof(Int64))
        {
            return Int64.Parse(dd);
        }
        else if (idt == typeof(SByte))
        {
            return SByte.Parse(dd);
        }
        else if (idt == typeof(UInt16))
        {
            return UInt16.Parse(dd);
        }
        else if (idt == typeof(UInt32))
        {
            return UInt32.Parse(dd);
        }
        else if (idt == typeof(UInt64))
        {
            return UInt64.Parse(dd);
        }
        //throw new Exception("Nepovolený nehodnotový typ v metodě GetMinValueForType");
        return decimal.Parse(dd);
    }

    public int SelectFirstAvailableIntIndex(bool signed, string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            if (signed)
            {
                return int.MinValue;
            }
            else
            {
                return 0;
            }
        }
        // TODO: Zde to pak nahradit metodou, která toto bude dělat přímo v databázi a vrátí mi pouze výsledek
        List<int> allIDs = SelectValuesOfColumnAllRowsInt(table, column);
        allIDs.Sort();
        int to = allIDs[allIDs.Count - 1];
        int y = 0;
        int i = int.MinValue;
        for (; i < to; i++)
        {
            if (allIDs[y] != i)
            {
                return i;
            }
            y++;
        }
        return ++i;
    }

    public short SelectFirstAvailableShortIndex(bool signed, string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            if (signed)
            {
                return short.MinValue;
            }
            else
            {
                return 0;
            }
        }
        // TODO: Zde to pak nahradit metodou, která toto bude dělat přímo v databázi a vrátí mi pouze výsledek
        List<short> allIDs = SelectValuesOfColumnAllRowsShort(table, column);
        allIDs.Sort();
        short to = allIDs[allIDs.Count - 1];
        int y = 0;
        short i = short.MinValue;
        for (; i < to; i++)
        {
            if (allIDs[y] != i)
            {
                return i;
            }
            y++;
        }
        return ++i;
    }

    public short SelectMaxShortMinValue(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return short.MinValue;
        }
        return ExecuteScalarShort(true, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
    }

    /// <summary>
    /// Vrací int.MaxValue pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxInt, která vrací 0
    /// To co vrátí tato metoda můžeš vždy jen inkrementovat a vložit do tabulky
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public int SelectMaxIntMinValue(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return int.MinValue;
        }
        return ExecuteScalarInt(true, new SqlCommand( "SELECT MAX(" + column + ") FROM " + table));
    }

    public DateTime SelectMaxDateTime(string table, string column, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand("SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameteres(comm, 0, ab);
        return ExecuteScalarDateTime(DateTime.MinValue, comm);
    }

    /// <summary>
    /// Vrací 0 pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxIntMinValue, která vrací int.MinValue
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public int SelectMaxInt(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return 0;
        }
        return ExecuteScalarInt(true, new SqlCommand( "SELECT MAX(" + column + ") FROM " + table));
    }

    public int SelectMaxIntMinValue(string table, string sloupec, params AB[] aB)
    {
        SqlCommand comm = new SqlCommand("SELECT MAX(" + sloupec + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB));
        AddCommandParameteres(comm, 0, aB);
        return ExecuteScalarInt(true, comm);
    }

    public short SelectMinShortMinValue(string table, string sloupec, params AB[] aB)
    {
        SqlCommand comm = new SqlCommand("SELECT MIN(" + sloupec + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB));
        AddCommandParameteres(comm, 0, aB);
        return ExecuteScalarShort(true, comm);
    }

    public byte SelectMaxByte(string table, string column, params AB[] aB)
    {
        if (SelectCount(table) == 0)
        {
            return 0;
        }
        ABC abc = new ABC(aB);
        return Convert.ToByte( ExecuteScalar("SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB), abc.OnlyBs()));
    }

    public int SelectMinInt(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return 0;
        }
        return ExecuteScalarInt(true, new SqlCommand( "SELECT MIN(" + column + ") FROM " + table));
    }


    #endregion

    #region Pomocné metody
    public Guid SelectNewId()
    {
        // NEWSEQUENTIALID() zde nemůžu použít, to se může pouze při vytváření nové tabulky
        return new Guid(ExecuteScalar("SELECT NEWID()").ToString());
    }


    #endregion

    #region Ostatní select
    public long SelectCount(string table)
    {
        return Convert.ToInt64( ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }

    public long SelectCountOrMinusOne(string table)
    {
        if (!SelectExistsTable(table))
        {
            return -1;
        }
        return Convert.ToInt64(ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }

    public long SelectCount(string table, params AB[] abc)
    {
        SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM " + table + GeneratorMsSql.CombinedWhere(abc));
        AddCommandParameteres(comm, 0, abc);
        return Convert.ToInt64( ExecuteScalar(comm));
    }

    public List<long> SelectGroupByLong(string table, string GroupByColumn, params AB[] where)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        AddCommandParameterFromAbc(comm, where);
        return ReadValuesLong(comm);
    }

    public List<int> SelectGroupByInt(string table, string GroupByColumn, params AB[] where)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        AddCommandParameterFromAbc(comm, where);
        return ReadValuesInt(comm);
    }

    /// <summary>
    /// Vrátí z řádků který je označen jako group by vždy jen 1 řádek
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="GroupByColumn"></param>
    /// <param name="IDColumnName"></param>
    /// <param name="IDColumnValue"></param>
    /// <returns></returns>
    public List<short> SelectGroupByShort(bool signed, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.SimpleWhere(IDColumnName) + " group by " + GroupByColumn;
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, IDColumnValue);
        return ReadValuesShort(comm);
    }

    public List<long> SelectGroupByLong(bool signed, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.SimpleWhere(IDColumnName) + " group by " + GroupByColumn;
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, IDColumnValue);
        return ReadValuesLong(comm);
    }

    /// <summary>
    /// Zjištuje to ze všech řádků v databázi.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="table"></param>
    /// <param name="idEntity"></param>
    /// <returns></returns>
    public uint SelectSumOfViewCount(string table, long idEntity)
    {
        SqlCommand comm = new SqlCommand("select SUM(ViewCount) from " + table + " where EntityID = @p0");
        //SqlCommand comm2 = new SqlCommand("select count(*) as AccValue from PageViews where Date <= @p0 AND Date >= @p1 AND IDPage = @p2");
        AddCommandParameter(comm, 0, idEntity);

        object o = ExecuteScalar(comm);
        if (o == null || o == DBNull.Value)
        {
            return 0;
        }
        return Convert.ToUInt32( o);
    }

    #region SelectSumOfViewByPeriod
    #endregion

    public int SelectSum(string table, string columnToSum, params AB[] aB)
    {
        //DataTable dt = SelectDataTableSelectiveCombination()
        List<int> nt = SelectValuesOfColumnAllRowsInt(true, table, columnToSum, aB);
        return nt.Sum();
    }

    public int SelectSumByte(string table, string columnToSum, params AB[] aB)
    {
        int vr = 0;
        List<byte> nt = SelectValuesOfColumnAllRowsByte( table, columnToSum, aB);
        foreach (var item in nt)
        {
            vr += item;
        }
        return vr;
    }

    /// <summary>
    /// Tuto metodu nepoužívej například po vkládání, když chceš zjistit ID posledního řádku, protože když tam bude něco smazaného , tak to budeš mít o to posunuté !!
    /// 
    /// </summary>
    public int SelectFindOutNumberOfRows(string tabulka)
    {
        SqlCommand comm = new SqlCommand("SELECT Count(*) FROM " + tabulka);
        //comm.Transaction = tran;
        return Convert.ToInt32( ExecuteScalar(comm));
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectNamesOfIDs(string tabulka, List<int> idFces)
    {
        List<string> vr = new List<string>();
        foreach (int var in idFces)
        {
            vr.Add(SelectNameOfID(tabulka, var));
        }
        return vr;
    }

    /// <summary>
    /// 
    /// </summary>
    public int SelectID(bool signed, string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        SqlCommand c = new SqlCommand(string.Format("SELECT (ID) FROM {0} WHERE {1} = @p0", tabulka, nazevSloupce));
        AddCommandParameter(c, 0, hodnotaSloupce);
        return ExecuteScalarInt(signed, c);
    }

    /// <summary>
    /// Vrátí SE, když nebude nalezena 
    /// </summary>
    public string SelectNameOfID(string tabulka, long id)
    {
        return SelectCellDataTableStringOneRow(tabulka, "Name", "ID", id);
    }

    public string SelectNameOfID(string tabulka, long id, string nameColumnID)
    {
        return SelectCellDataTableStringOneRow(tabulka, "Name", nameColumnID, id);
    }

    public string SelectNameOfIDOrSE(string tabulka, string idColumnName, int id)
    {
        return SelectCellDataTableStringOneRow(tabulka, "Name", idColumnName, id);
    }
    #endregion

    #region SelectRow
    public object[] SelectRowReaderLimit(string tableName, int limit, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + sloupce + " FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectRowReader(comm);
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
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return SelectRowReader(comm);
    }



    public object[] SelectRowReader(string tabulka, string sloupecID, object id, string nazvySloupcu)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return SelectRowReader(comm);
    }

    /// <summary>
    /// Vrátí null, pokud výsledek nebude mít žádné řádky
    /// </summary>
    /// <param name="comm"></param>
    /// <returns></returns>
    private object[] SelectRowReader(SqlCommand comm)
    {
        SqlDataReader r = ExecuteReader(comm);

        if (r.HasRows)
        {
            object[] o = new object[r.VisibleFieldCount];
            r.Read();
            for (int i = 0; i < r.VisibleFieldCount; i++)
            {

                o[i] = r.GetValue(i);
            }


            return o;
        }
        return null;
    }

    /// <summary>
    /// Vrátí null pokud žádný takový řádek nebude nalezen
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vratit"></param>
    /// <param name="ab"></param>
    /// <returns></returns>
    public object[] SelectSelectiveOneRow(string table, string vratit, params AB[] ab)
    {
        string sql = "SELECT TOP(1) " + vratit + " FROM " + table;
        sql += GeneratorMsSql.CombinedWhere(ab);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, ab);
        return SelectRowReader(comm);
    }
    #endregion

    #region SelectExists
    /// <summary>
    /// 
    /// </summary>
    public bool SelectExistsCombination(string p, params AB[] aB)
    {
        string sql = string.Format("SELECT {0} FROM {1} {2}", aB[0].A, p, GeneratorMsSql.CombinedWhere(aB));
        ABC abc = new ABC(aB);
        return ExecuteScalar(sql, abc.OnlyBs()) != null;
    }

    public bool SelectExistsCombination(string p, AB[] where, AB[] whereIsNot)
    {
        int dd = 0;
        string sql = string.Format("SELECT {0} FROM {1} {2} {3}", where[0].A, p, GeneratorMsSql.CombinedWhere(where, ref dd), GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot));
        int pridatNa = 0;
        SqlCommand comm = new SqlCommand(sql);
        foreach (var item in where)
        {
            pridatNa = AddCommandParameter(comm, pridatNa, item.B);
        }
        foreach (var item in whereIsNot)
        {
            pridatNa = AddCommandParameter(comm, pridatNa, item.B);
        }
        return ExecuteScalar(comm) != null;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool SelectExists(string tabulka, string sloupec, object hodnota)
    {
        string sql = string.Format("SELECT TOP(1) {0} FROM {1} {2}", sloupec, tabulka, GeneratorMsSql.SimpleWhere(sloupec));
        return ExecuteScalar(sql, hodnota) != null;
    }


    #endregion

    #region SelectCell
    public short SelectCellDataTableShortOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarShort(signed, comm);
    }

    /// <summary>
    /// G -1 když se žádný takový řádek nepodaří najít
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, ABC whereIs, ABC whereIsNot)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null);
        SqlCommand comm = new SqlCommand(sql);
        int dalsi = AddCommandParameterFromAbc(comm, whereIs, 0);
        AddCommandParameterFromAbc(comm, whereIsNot, dalsi);
        return ExecuteScalarInt(signed, comm);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public long SelectCellDataTableLongOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarLong(signed, comm);
    }

    /// <summary>
    /// Vrátí -1 pokud žádný takový řádek nenalezne pokud !A1 enbo int.MaxValue pokud A1
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarInt(signed, comm);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public long SelectCellDataTableLongOneRow(bool signed, string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarLong(true, comm);
    }

    public DateTime SelectCellDataTableDateTimeOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue, DateTime getIfNotFound)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarDateTime(getIfNotFound, comm);
    }

    /// <summary>
    /// Vrátí DateTimeMinVal, pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public DateTime SelectCellDataTableDateTimeOneRow(string table, string vracenySloupec, DateTime getIfNotFound, AB[] where, AB[] whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, idColumnValue);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarDateTime(getIfNotFound, comm);
    }

    /// <summary>
    /// Vrátí null pokud se řádek nepodaří najít
    /// A3 nebo A4 může být null
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public bool? SelectCellDataTableNullableBoolOneRow(string table, string vracenySloupec, AB[] where, AB[] whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarNullableBool(comm);
    }

    public bool SelectCellDataTableBoolOneRow(string table, string vracenySloupec, AB[] where, AB[] whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarBool(comm);
    }

    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    public byte SelectCellDataTableByteOneRow(string table, string vracenySloupec, params AB[] where)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table);
        sql += GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, where);
        return ExecuteScalarByte(comm);
    }

    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);

        return ExecuteScalarInt(signed, comm);
    }

    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public byte SelectCellDataTableByteOneRow(string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarByte(comm);
    }

    public bool SelectCellDataTableBoolOneRow(string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarBool(comm);
    }

    public bool? SelectCellDataTableNullableBoolOneRow(string table, string vracenySloupec, params AB[] abc)
    {
        string sql = "SELECT TOP(1) " + vracenySloupec + " FROM " + table + " " + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteres(comm, 0, abc);
        return ExecuteScalarNullableBool(comm);
    }

    /// <summary>
    /// V případě nenalezení vrátí -1 pokud !A1, jinak short.MaxValue
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public short SelectCellDataTableShortOneRow(bool signed, string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarShort(signed, comm);
    }

    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public float SelectCellDataTableFloatOneRow(bool signed, string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarFloat(signed, comm);
    }

    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public float SelectCellDataTableFloatOneRow(bool signed, string table, string vracenySloupec, params AB[] ab)
    {
        string sql = GeneratorMsSql.CombinedWhere(table, true, vracenySloupec, ab);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, ab);
        return ExecuteScalarFloat(signed, comm);
    }

    public object SelectCellDataTableObjectOneRow(string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalar(comm);
    }

    /// <summary>
    /// Vykonává metodou ExecuteScalar. Ta pokud vrátí null, metoda vrátí "". To je taky rozdíl oproti metodě SelectCellDataTableStringOneRowABC.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="ab"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(GeneratorMsSql.CombinedWhere(tabulka, true, nazvySloupcu, ab));
        AddCommandParameterFromAbc(comm, ab);
        return ExecuteScalarString(comm);
    }

    

    public string SelectCellDataTableStringOneLastRow(string table, string vracenySloupec, string orderByDesc, string idColumnName, object idColumnValue)
    {
        //SELECT TOP 1 * FROM table_Name ORDER BY unique_column DESC
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql + string.Format(" ORDER BY {0} DESC", orderByDesc));
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarString(comm);
    }

    /// <summary>
    /// Vrátí SE v případě že řádek nebude nalezen, nikdy nevrací null.
    /// Automaticky vytrimuje
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarString(comm);
    }

    /// <summary>
    /// A4 může být null, A3 nikoliv
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string table, string vracenySloupec, AB[] where, AB[] whereIsNot)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT TOP(1) " + vracenySloupec);
        sb.Append(" FROM " + table);
        int dd = 0;
        sb.Append(GeneratorMsSql.CombinedWhere(where, ref dd));

        sb.Append(GeneratorMsSql.CombinedWhereNotEquals(where.Length != 0, ref dd, whereIsNot));
        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);

        return ExecuteScalarString(comm);
    }
    #endregion

    #region Určené k smazání
    /// <summary>
    /// Nepoužívat a smazat!!!
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="whereSloupec"></param>
    /// <param name="whereValue"></param>
    /// <returns></returns>
    public DataTable SelectDataTableAllRows(string TableName, string whereSloupec, object whereValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT * FROM {0} {1}", TableName, GeneratorMsSql.SimpleWhere(whereSloupec)));
        AddCommandParameter(comm, 0, whereValue);
        //NTd
        return this.SelectDataTable(comm);
    }

    /// <summary>
    /// Nepužívat a smazat!!!
    /// </summary>
    public DataTable SelectDataTableAllRows(string table)
    {
        return SelectDataTable("SELECT * FROM " + table);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A3.
    /// Tato metoda vrací celé řádky z tabulky, Je zde stejně pojmenovaná metoda s A4, kde můžeš specifikovat sloupce které chceš vrátit.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecID"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRows(string tableName, int limit, string sloupecID, params AB[] abc)
    {
        return SelectDataTableLimitLastRows(tableName, limit, "*", sloupecID, abc);
    }

    /// <summary>
    /// Nepoužívat a smazat !!!
    /// Vrátí null když nenalezne žádný řádek
    /// </summary>
    public object[] SelectOneRow(string TableName, string nazevSloupce, object hodnotaSloupce)
    {
        // Index nemůže být ani pole bajtů ani null takže to je v pohodě
        DataTable dt = SelectDataTable("SELECT TOP(1) * FROM " + TableName + " WHERE " + nazevSloupce + " = @p0", hodnotaSloupce);
        if (dt.Rows.Count == 0)
        {
            return null; // CA.CreateEmptyArray(pocetSloupcu);
        }
        return dt.Rows[0].ItemArray;
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

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsString
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec)
    {
        //List<string> vr = new List<string>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesStringTrim(comm);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(comm);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, int limit, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP("+limit+") {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(comm);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// </summary>
    public DataTable SelectDataTableLimit(string tableName, int limit)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") * FROM " + tableName);
        //AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }

    #endregion

    
}
