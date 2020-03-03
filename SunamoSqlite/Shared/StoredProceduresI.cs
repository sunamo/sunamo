using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

public class StoredProceduresI : IStoredProceduresI
    {
        private SQLiteConnection _conn = DatabaseLayer.conn;
        public static StoredProceduresI ci = new StoredProceduresI();

    public int SelectFindOutNumberOfRows(object tableName)
    {
        return 0;
    }

    public int InsertToTable(object tableName, object p, int iD, params object[] p2)
    {
        return 0;
    }

    public List<string> VratNazvySloupcuTabulky(string p)
        {
            List<string> vr = new List<string>();
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT sql FROM sqlite_master WHERE tbl_name = '{0}' AND type = 'table'", p), _conn);
            SQLiteDataReader dr = comm.ExecuteReader(CommandBehavior.SingleRow);
            string sql = null;
            object o = dr.GetValue(0);
            sql = o.ToString();
            string s = SH.Substring(sql, sql.IndexOf('(') + 1, sql.LastIndexOf(')') - 1);
            List<string> sloupce = SH.Split( s,',');
            for (int i = 0; i < sloupce.Count; i++)
            {
                List<string> g = SH.Split( sloupce[i],  " " );
                vr.Add(g[0]);
            }
            return vr;
        }

        public bool SelectExistsTable(string table, SQLiteConnection conn)
        {
            return ExecuteNonQuery("SELECT name FROM sqlite_master WHERE type='table' AND name=" + "'" + table + "';") != 0;
        }

        public DataTable GetDataTable(string sql)
        {
            SQLiteCommand comm = new SQLiteCommand(sql, _conn);
            return GetDataTable(comm);
        }

        public DataTable GetDataTable(SQLiteCommand comm)
        {
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(comm);


            adapter.Fill(dt);

            //DataTable dt = ds.Tables[0];
            return dt;
        }

    public int SelectID(string privacy1, string v, string privacy2)
    {
        return 0;
    }

    public DataTable GetDataTableAllRows(string p)
        {
            return GetDataTable("SELECT * FROM" + " " + p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupec"></param>
        /// <param name="hodnota"></param>
        public bool ContainsRowsInTableValue(string tabulka, string sloupec, object hodnota)
        {
            DataTable dt = GetDataTableSelective(tabulka, sloupec, hodnota);
            return dt.Rows.Count != 0;
        }

        /// <summary>
        /// G aktuální počet řádky v tabulce A1 po vložení.
        /// Do A2 se to zadává bez uvozovek.
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="nazvySloupcu"></param>
        /// <param name="sloupce"></param>
        public int InsertToTable(string tabulka, object v, string nazvySloupcu, params object[] sloupce)
        {
            string hodnoty = StoredProcedures.ci.GetValues(sloupce);
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("INSERT INTO {0} {1} VALUES {2}", tabulka, nazvySloupcu, StoredProcedures.ci.GetValues(sloupce)), _conn);
            comm.ExecuteNonQuery();
            return StoredProceduresI.ci.FindOutNumberOfRows(tabulka);
        }

        /// <summary>
        /// G všechny řádky z tabulky A1 kde sloupec A2 bude mít hodnotu A3.
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupec"></param>
        /// <param name="hodnota"></param>
        public DataTable GetDataTableSelective(string tabulka, string sloupec, object hodnota)
        {
            return GetDataTable("SELECT * FROM" + " " + tabulka + " " + "WHERE" + " " + sloupec + " = " + StoredProcedures.ci.ReplaceValueOnlyOne(hodnota));
        }

        private DataTable GetDataTableSelective(string tabulka, string sloupecID, int id, string hledanySloupec)
        {
            return GetDataTable("SELECT" + " " + hledanySloupec + " " + "FROM" + " " + tabulka + " " + "WHERE" + " " + sloupecID + " = " + StoredProcedures.ci.ReplaceValueOnlyOne(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupecHledaní"></param>
        /// <param name="sloupecVeKteremHledat"></param>
        /// <param name="hodnota"></param>
        public List<int> GetValueColumnInt(string tabulka, string sloupecHledaní, string sloupecVeKteremHledat, object hodnota)
        {
            string sql = SH.Format2("SELECT {0} FROM {1} WHERE {2} = {3}", sloupecHledaní, tabulka, sloupecVeKteremHledat, StoredProcedures.ci.ReplaceValueOnlyOne(hodnota));
            //SQLiteCommand comm = new SQLiteCommand(sql, conn);

            return GetValuesAllRowsInt(sql);

            //return vr;
        }

    public bool SelectExistsCombination(string comments, AB aB1, AB aB2)
    {
        return false;
    }

    private List<int> GetValuesAllRowsInt(string sql)
        {
            List<int> vr = new List<int>();
            SQLiteCommand comm = new SQLiteCommand(sql, _conn);
            DataTable dt = StoredProceduresI.ci.GetDataTable(comm);
            foreach (DataRow var in dt.Rows)
            {
                vr.Add(int.Parse(var.ItemArray[0].ToString()));
            }
            return vr;
        }

        private List<string> GetValuesAllRowsString(string sql)
        {
            List<string> vr = new List<string>();
            SQLiteCommand comm = new SQLiteCommand(sql, _conn);
            DataTable dt = StoredProceduresI.ci.GetDataTable(comm);
            foreach (DataRow var in dt.Rows)
            {
                vr.Add(var.ItemArray[0].ToString());
            }
            return vr;
        }

        /// <summary>
        /// ---Pozor, pokud bude více řádků vyhovovat A2,3, vrátí to zřejmě hodnoty od všech. +++Už ne, dělLm to přes tabulku a ne reader
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="nazevSloupce"></param>
        /// <param name="hodnotaSloupce"></param>
        public List<object> GetValuesAllRows(string tabulka, string nazevSloupce, object hodnotaSloupce)
        {
            List<object> vr = new List<object>();
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT * FROM {0} WHERE {1} = {2}", tabulka, nazevSloupce, StoredProcedures.ci.ReplaceValueOnlyOne(hodnotaSloupce)), _conn);


            return vr;
        }

        public List<string> FindValuesOfIDs(string tabulka, List<int> idFces)
        {
            List<string> vr = new List<string>();
            foreach (int var in idFces)
            {
                vr.Add(StoredProceduresI.ci.FindValueOfID(tabulka, var));
            }
            return vr;
        }

        public List<string> VratVsechnyHodnotySloupce(string tabulka, string sloupec)
        {
            //DataTable dt = UlozeneProceduryI.ci.VratDataTable();
            return GetValuesAllRowsString(SH.Format2("SELECT {0} FROM {1}", sloupec, tabulka));
        }

        /// <summary>
        /// A2 je kombinace klazulE WHERE
        /// </summary>
        /// <param name="p"></param>
        /// <param name="aB"></param>
        public bool ExistsCombination(string p, params AB[] aB)
        {
            string sql = SH.Format2("SELECT {0} FROM {1} {2}", aB[0].A, p, GeneratorSqLite.CombinedWhere(new ABC( aB)));
            DataTable dt = GetDataTable(sql);
            return dt.Rows.Count != 0;
        }

        /// <summary>
        /// Funguje pouze když je sloupec nazev na indexu 1.
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="id"></param>
        public string FindValueOfID(string tabulka, int id)
        {
            //SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT Nazev FROM {0} WHERE ID = {1}", tabulka, id));
            return GetElementDataTable(GetDataTableSelective(tabulka, "ID", id), 0, 1);
        }

        /// <summary>
        /// G A3. prvek v radku také indexu A2 tabulky A1.
        /// Může vrátit i null pokud dostanu null. Pokud řádek nebo sloupec nenalezne, vHhodí výjimku
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private string GetElementDataTable(DataTable dataTable, int radek, int sloupec)
        {
            if (dataTable.Rows.Count >= radek)
            {
                object[] o = null;

                o = dataTable.Rows[radek].ItemArray;


                if (o.Length >= sloupec)
                {
                    if (o[sloupec] == null)
                    {
                        return null;
                    }
                    else
                    {
                        return o[sloupec].ToString();
                    }
                }
            }
            throw new Exception("Zadan\u00E1 buNka nebyla nalezena");
        }

        public int ExecuteNonQuery(string p)
        {
            SQLiteCommand comm = new SQLiteCommand(p, _conn);
            return comm.ExecuteNonQuery();
        }

        public double UpdatePlusRealValue(string table, string sloupecKUpdate, double pridej, string sloupecID, int id)
        {
            double d = double.Parse(StoredProceduresI.ci.GetElementDataTable(table, sloupecID, id, sloupecKUpdate));
            double n = pridej;
            if (d != 0)
            {
                n = (d + pridej) / 2;
            }
            StoredProceduresI.ci.Update(table, sloupecID, id, sloupecKUpdate, n);
            return n;
        }

        private int Update(string table, string sloupecID, int id, string sloupecKUpdate, double n)
        {
            //
            string sql = SH.Format2("UPDATE {0} SET {1}={2} WHERE {3} = {4}", table, sloupecKUpdate, StoredProcedures.ci.ReplaceValueOnlyOne(n), sloupecID, StoredProcedures.ci.ReplaceValueOnlyOne(id));
            SQLiteCommand comm = new SQLiteCommand(sql, _conn);
            return comm.ExecuteNonQuery();
        }

        public string GetElementDataTable(string table, string sloupecID, int id, string hledanySloupec)
        {
            DataTable dt = GetDataTableSelective(table, sloupecID, id, hledanySloupec);
            return GetElementDataTable(dt, 0, 0);
        }



        public DataTable GetDataTable(string p, params string[] selectSloupce)
        {
            return GetDataTable(SH.Format2("SELECT {0} FROM {1}", StoredProcedures.ci.GetColumnsWithoutBracets(selectSloupce.ToList()), p));
        }

        public int FindOutID(string tabulka, string nazevSloupce, object hodnotaSloupce)
        {
            return 0;
        }

        public int FindOutNumberOfRows(string tabulka)
        {
            return 0;
        }

        public SQLiteCommand InsertRowTypeEnumIfNotExists(string tabulka, string nazev)
        {
            return null;
        }

        public SQLiteCommand DeleteTableIfExists(string nazevTabulky)
        {
            return null;
        }
    }