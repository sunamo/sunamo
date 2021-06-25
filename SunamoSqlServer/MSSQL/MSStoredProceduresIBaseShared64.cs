using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public partial class MSStoredProceduresIBase : SqlServerHelper
{
    public bool SelectExistsTable(string p, SqlConnection conn)
    {
        DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        return dt.Rows.Count != 0;
    }
    public int DropTableIfExists(string table)
    {
        if (SelectExistsTable(table))
        {
            return ExecuteNonQuery(new SqlCommand("DROP TABLE " + table));
        }
        return 0;
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

    public bool SelectExistsTable(string p)
    {
        using (var conn = new SqlConnection(Cs))
        {
            DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));

            conn.Close();
            List<string> o = null;

            foreach (DataRow item in dt.Rows)
            {
                foreach (var item2 in item.ItemArray)
                {
                    if (!SqlServerHelper.IsNullOrEmpty(item2))
                    {
                        if (item2.ToString() == p)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;// dt.Rows.Count != 0;
        }
    }

    public long SelectCount(string table)
    {
        return Convert.ToInt64(ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }

    /// <summary>
    /// Pokud chceš použít OrderBy, je tu metoda SelectDataTableLimitLastRows nebo SelectDataTableLimitLastRowsInnerJoin
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// Vyplň A2 na SE pokud chceš všechny sloupce
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", nazvySloupcu, tabulka) + GeneratorMsSql.CombinedWhere(new ABC(ab)));
        AddCommandParameterFromAbc(comm, ab);
        //NT
        return this.SelectDataTable(comm);
    }

    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, int limit, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP({2}) {0} FROM {1}", nazvySloupcu, tabulka, limit) + GeneratorMsSql.CombinedWhere(new ABC(ab)));
        AddCommandParameterFromAbc(comm, ab);
        //NT
        return this.SelectDataTable(comm);
    }

    public int UpdateOneRow(string table, string sloupecKUpdate, object n, params AB[] ab)
    {
        int pridavatOd = 1;
        string sql = string.Format("UPDATE TOP(1) {0} SET {1}=@p0", table, sloupecKUpdate) + GeneratorMsSql.CombinedWhere(new ABC(ab), ref pridavatOd);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, n);
        AddCommandParameteres(comm, 1, ab);
        return ExecuteNonQuery(comm);
    }

    public static int AddCommandParameteres(SqlCommand comm, int pocIndex, params AB[] hodnotyOdNuly)
    {
        foreach (var item in hodnotyOdNuly)
        {
            AddCommandParameter(comm, pocIndex, item.B);
            pocIndex++;
        }
        return pocIndex;
    }

    static Type type = typeof(MSStoredProceduresIBase);
    public static string table2 = null;
    public static string column2 = null;
    public static bool isNVarChar2 = false;
    public string _cs = null;
    public static Func<string, string, bool> IsNVarChar = null;
    /// <summary>
    /// false protože s tím byly problémy - seklo se mi to do té míry že nějaký web přestal odpovídat
    /// navíc to je relativně zbytečné, sám vidím jak se co rychle načítá
    /// udělat to na SQLite - ne, zkusím zapisovat jen nad 1000ms
    /// pokud by se opakovalo že některý web nepůjde načíst (nejpravděpodoněji lyr/app - mají nejvíce SQL dotazů), nastavit interval ještě vyšší
    /// později to mohu snížit a tímto způsobem to více a více optimalizovat
    /// </summary>
    public static bool measureTime = false;

    public static int waitMs = 0;


    string Cs
    {
        get
        {
            if (_cs != null)
            {
                return _cs;
            }

            return MSDatabaseLayer.cs;
        }
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

    /// <summary>
    /// A1 jsou hodnoty bez převedení AddCommandParameter nebo ReplaceValueOnlyOne
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="_params"></param>
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

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    public DataTable SelectDataTable(SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            var dt = SelectDataTable(conn, comm);

            conn.Close();
            return dt;
        }
    }

    public DataTable SelectDataTable(SqlConnection conn, SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);

        return dt;
    }

    /// <summary>
    /// a2 je X jako v příkazu @pX
    /// A3 cant be AB
    /// When returned value will be negative, into db was entered emtpy string - insert new with SQLServerPreparedStatement  https://stackoverflow.com/questions/9066549/nvarchar-max-gives-string-or-binary-data-would-be-truncated
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

        else if (o.GetType() == Types.tString || o.GetType() == Types.tChar)
        {
            string _ = o.ToString();

            if (IsNVarChar != null)
            {
                if (SqlServerHelper.GetTableAndColumn(comm.CommandText, ref table2, ref column2, i))
                {
                    isNVarChar2 = IsNVarChar.Invoke(table2, column2);

                    if (!isNVarChar2)
                    {
                        _ = MSStoredProceduresI.ConvertToVarChar(_);
                    }
                }
            }

            comm.Parameters.AddWithValue("@p" + i.ToString(), _);
        }
        else
        {
            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }

        ++i;
        return i;
    }


    /// <summary>
    /// For getting ID use SelectLastIDFromTableSigned2 (with 2 postfix)
    /// Tato metoda je vyjímečná, vkládá hodnoty signed, hodnotu kterou vložit si zjistí sám a vrátí ji.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    public long Insert2(string tabulka, string sloupecID, Type typSloupecID, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValuesDirect(sloupce.Length + 1);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        //bool totalLower = false;
        var l = SelectLastIDFromTableSigned2(tabulka, typSloupecID, sloupecID);

        long id = Convert.ToInt64(l);
        AddCommandParameter(comm, 0, id);
        for (int i = 0; i < sloupce.Length; i++)
        {
            AddCommandParameter(comm, i + 1, sloupce[i]);
        }
        ExecuteNonQuery(comm);
        return id;
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesShort(comm);
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
        comm.Connection.Close();
        comm.Connection.Dispose();
        return vr;
    }

    /// <summary>
    /// Not use
    /// return alwys int.MinValue
    /// 
    /// If no row was found, return max value
    /// Only place where can be called is in Insert2
    /// 
    /// Vrátí všechny hodnoty z sloupce A3 a pak počítá od A2.MinValue až narazí na hodnotu která v tabulce nebyla, tak ji vrátí
    /// Proto není potřeba vr nijak inkrementovat ani jinak měnit
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "V klazuli if v metodě MSStoredProceduresIBase.SelectLastIDFromTableSigned nebyl nalezen typ " + idt.FullName.ToString());
            return null;
        }
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesInt(comm);
    }

    private List<int> ReadValuesInt(SqlCommand comm)
    {
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
        comm.Connection.Close();
        comm.Connection.Dispose();
        return vr;
    }


    public List<long> SelectValuesOfColumnAllRowsLong(bool signed, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());

        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(new ABC(aB))));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesLong(comm);
    }

    /// <summary>
    /// MUST CALL conn.Close(); AFTER GET DATA
    /// </summary>
    /// <param name="comm"></param>
    private SqlDataReader ExecuteReader(SqlCommand comm)
    {
        if (measureTime)
        {
            StopwatchStaticSql.Start();
        }

        var conn = new SqlConnection(Cs);

        conn.Open();
        comm.Connection = conn;
        comm.CommandTimeout = SqlConsts.timeout;
        var result = comm.ExecuteReader(CommandBehavior.Default);

        if (measureTime)
        {
            if (StopwatchStaticSql.AboveLimit())
            {
                StopwatchStaticSql.StopAndPrintElapsed(SqlServerHelper.SqlCommandToTSQLText(comm));
            }
        }

        return result;
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
        comm.Connection.Close();
        comm.Connection.Dispose();
        return vr;
    }


    public long InsertSigned(string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        return Insert1(tabulka, idt, sloupecID, sloupce, true);
    }

    public object ExecuteScalar(string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }
        var result = ExecuteScalar(comm);
        return result;
    }

    /// <summary>
    /// Automaticky doplní connection
    /// </summary>
    /// <param name="comm"></param>
    public object ExecuteScalar(SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            if (measureTime)
            {
                StopwatchStaticSql.Start();
            }

            if (waitMs != 0)
            {
                Thread.Sleep(waitMs);
            }

            conn.Open();
            //SqlDbType.SmallDateTime;
            comm.Connection = conn;
            comm.CommandTimeout = SqlConsts.timeout;
            var result = comm.ExecuteScalar();
            conn.Close();

            if (measureTime)
            {
                if (StopwatchStaticSql.AboveLimit())
                {
                    StopwatchStaticSql.StopAndPrintElapsed(SqlServerHelper.SqlCommandToTSQLText(comm));
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Has signed, therefore can return values below -1
    /// 
    /// 
    /// Nevm jestli není jeblá - vrací mi ID jež následně budou existovat
    /// 
    /// Nedá se použít na desetinné typy
    /// Vrátí mi nejmenší volné číslo tabulky A1
    /// Pokud bude obsazene 1,3, vrátí až 4
    /// </summary>
    /// <param name="p"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <param name="totalLower"></param>
    public object SelectLastIDFromTableSigned(bool signed, string p, Type idt, string sloupecID, out bool totalLower)
    {
        totalLower = false;
        //// Snaž se vždy používat SelectLastIDFromTableSigned2 místo SelectLastIDFromTableSigned - ta mi vracela hodnotu která při přidávání poté již v DB existovala
        //return SelectLastIDFromTableSigned2(p, idt, sloupecID);

        string dd = ExecuteScalar(new SqlCommand("SELECT MAX(" + sloupecID + ") FROM " + p)).ToString();

        if (dd == "")
        {
            totalLower = true;
            object vr = 0;
            if (signed)
            {
                vr = BTS.GetMinValueForType(idt);
            }

            if (idt == Types.tShort)
            {
                //short s = (short)vr;
                return vr;
            }
            else if (idt == Types.tInt)
            {
                //int nt = (int)vr;
                return vr;
            }
            else if (idt == Types.tByte)
            {
                return vr;
            }
            else if (idt == Types.tLong)
            {
                //long lng = (long)vr;
                return vr;
            }
            else
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "V klazuli if v metodě MSStoredProceduresIBase.SelectLastIDFromTableSigned nebyl nalezen typ " + idt.FullName.ToString());
            }
        }

        // cant add anything here, otherwise tr.InsertToTable inserting 1,3,5,etc.
        byte add = 0;

        if (idt == typeof(Byte))
        {
            return Byte.Parse(dd) + add;
        }
        else if (idt == typeof(Int16))
        {
            return Int16.Parse(dd) + add;
        }
        else if (idt == typeof(Int32))
        {
            return Int32.Parse(dd) + add;
        }
        else if (idt == typeof(Int64))
        {
            return Int64.Parse(dd) + add;
        }
        else if (idt == typeof(SByte))
        {
            return SByte.Parse(dd) + add;
        }
        else if (idt == typeof(UInt16))
        {
            return UInt16.Parse(dd) + add;
        }
        else if (idt == typeof(UInt32))
        {
            return UInt32.Parse(dd) + add;
        }
        else if (idt == typeof(UInt64))
        {
            return UInt64.Parse(dd) + add;
        }
        //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepovolený nehodnotový typ v metodě GetMinValueForType") + 1;
        return decimal.Parse(dd) + 1;
    }


    private long Insert1(string tabulka, Type idt, string sloupecID, object[] sloupce, bool signed)
    {
        string hodnoty = MSDatabaseLayer.GetValuesDirect(sloupce.Length + 1);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        bool totalLower = false;

        object d = SelectLastIDFromTableSigned(signed, tabulka, idt, sloupecID, out totalLower);
        #region MyRegion
        int pricist = 0;
        if (!totalLower)
        {
            pricist = 1;
        }
        else if (idt == Types.tByte)
        {
            pricist = 1;
        }
        if (idt == typeof(Byte))
        {
            Byte b = Convert.ToByte(d);
            comm.Parameters.AddWithValue("@p0", b + pricist);
        }
        else if (idt == typeof(Int16))
        {
            Int16 i1 = Convert.ToInt16(d);
            comm.Parameters.AddWithValue("@p0", i1 + pricist);
        }
        else if (idt == typeof(Int32))
        {
            Int32 i2 = Convert.ToInt32(d);
            comm.Parameters.AddWithValue("@p0", i2 + pricist);
        }
        else if (idt == typeof(Int64))
        {
            Int64 i3 = Convert.ToInt64(d);
            comm.Parameters.AddWithValue("@p0", i3 + pricist);
        }
        int to = sloupce.Length + 1;
        for (int i = 1; i < to; i++)
        {
            object o = sloupce[i - 1];
            //var resutl = 
            AddCommandParameter(comm, i, o);
            //if (resutl < 0)
            //{
            //    SQLServerPreparedStatement
            //}
            //DateTime.Now.Month;
        }
        #endregion
        ExecuteNonQuery(comm);

        long vr = Convert.ToInt64(d);
        vr += pricist;
        return vr;
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

    /// <summary>
    /// Return count of rows affected
    /// </summary>
    /// <param name="comm"></param>
    public int ExecuteNonQuery(SqlCommand comm)
    {
        using (SqlConnection conn = new SqlConnection(Cs))
        {
            if (measureTime)
            {
                StopwatchStaticSql.Start();
            }

            conn.Open();
            comm.CommandTimeout = SqlConsts.timeout;
            comm.Connection = conn;

#if DEBUG
            PrintDebugParameters(comm);
#endif
            if (comm.CommandText.ToLower().StartsWith(SqlConsts.update))
            {

            }
            var result = comm.ExecuteNonQuery();
            conn.Close();

            if (measureTime)
            {
                if (StopwatchStaticSql.AboveLimit())
                {
                    StopwatchStaticSql.StopAndPrintElapsed(SqlServerHelper.SqlCommandToTSQLText(comm));
                }
            }

            return result;
        }
    }

    private void PrintDebugParameters(SqlCommand comm)
    {
        //foreach (SqlParameter item in comm.Parameters)
        //{
        //    //DebugLogger.DebugWriteLine(SH.NullToStringOrDefault( item.Value));
        //}
    }

    public bool DeleteOneRow(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(new ABC(where));
        SqlCommand comm = new SqlCommand("DELETE TOP(1) FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);

        return f == 1;
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
}
