using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;


public class StoredProceduresSqliteI : IStoredProceduresI
    {
        private SQLiteConnection _conn = DatabaseLayer.conn;
        public static StoredProceduresSqliteI ci = new StoredProceduresSqliteI();

        public string[] VratNazvySloupcuTabulky(string p)
        {
            List<string> vr = new List<string>();
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT sql FROM sqlite_master WHERE tbl_name = '{0}' AND type = 'table'", p), _conn);
            SQLiteDataReader dr = comm.ExecuteReader(CommandBehavior.SingleRow);
            string sql = null;
            object o = dr.GetValue(0);
            sql = o.ToString();
            string s = SH.Substring(sql, sql.IndexOf(AllChars.lb) + 1, sql.LastIndexOf(AllChars.rb) - 1);
            string[] sloupce = s.Split(AllChars.comma);
            for (int i = 0; i < sloupce.Length; i++)
            {
                string[] g = sloupce[i].Split(new string[] { AllStrings.space }, StringSplitOptions.RemoveEmptyEntries);
                vr.Add(g[0]);
            }
            return vr.ToArray();
        }



        public bool SelectExistsTable(string table, SQLiteConnection conn)
        {
            var val = ExecuteNonQuery("SELECT name FROM sqlite_master WHERE type='table' AND name=" + "'" + table + "';");
            return val != -1;
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

        public DataTable GetDataTableAllRows(string p)
        {
            return GetDataTable("SELECT * FROM" + " " + p);
        }

        public List<string> AllTables()
        {
            return GetValuesAllRowsString(@"SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupec"></param>
        /// <param name="hodnota"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        public void InsertToTable(string tabulka, string nazvySloupcu, params object[] sloupce)
        {
            string hodnoty = StoredProceduresSqlite.ci.GetValues(sloupce);
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("INSERT INTO {0} {1} VALUES {2}", tabulka, nazvySloupcu, StoredProceduresSqlite.ci.GetValues(sloupce)), _conn);
            comm.ExecuteNonQuery();
        }

        public void Insert4(string tableName, params object[] v1)
        {
            InsertToTable(tableName, string.Empty, v1);
        }

        /// <summary>
        /// G všechny řádky z tabulky A1 kde sloupec A2 bude mít hodnotu A3.
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupec"></param>
        /// <param name="hodnota"></param>
        /// <returns></returns>
        public DataTable GetDataTableSelective(string tabulka, string sloupec, object hodnota)
        {
            return GetDataTable("SELECT * FROM" + " " + tabulka + " " + "WHERE" + " " + sloupec + " = " + StoredProceduresSqlite.ci.ReplaceValueOnlyOne(hodnota));
        }

        private DataTable GetDataTableSelective(string tabulka, string sloupecID, int id, string hledanySloupec)
        {
            return GetDataTable("SELECT" + " " + hledanySloupec + " " + "FROM" + " " + tabulka + " " + "WHERE" + " " + sloupecID + " = " + StoredProceduresSqlite.ci.ReplaceValueOnlyOne(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupecHledaní"></param>
        /// <param name="sloupecVeKteremHledat"></param>
        /// <param name="hodnota"></param>
        /// <returns></returns>
        public List<int> GetValueColumnInt(string tabulka, string sloupecHledaní, string sloupecVeKteremHledat, object hodnota)
        {
            string sql = SH.Format2("SELECT {0} FROM {1} WHERE {2} = {3}", sloupecHledaní, tabulka, sloupecVeKteremHledat, StoredProceduresSqlite.ci.ReplaceValueOnlyOne(hodnota));
            //SQLiteCommand comm = new SQLiteCommand(sql, conn);

            return GetValuesAllRowsInt(sql);

            //return vr;
        }

        private List<int> GetValuesAllRowsInt(string sql)
        {
            List<int> vr = new List<int>();
            SQLiteCommand comm = new SQLiteCommand(sql, _conn);
            DataTable dt = StoredProceduresSqliteI.ci.GetDataTable(comm);
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
            DataTable dt = StoredProceduresSqliteI.ci.GetDataTable(comm);
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
        /// <returns></returns>
        public List<object> GetValuesAllRows(string tabulka, string nazevSloupce, object hodnotaSloupce)
        {
            List<object> vr = new List<object>();
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT * FROM {0} WHERE {1} = {2}", tabulka, nazevSloupce, StoredProceduresSqlite.ci.ReplaceValueOnlyOne(hodnotaSloupce)), _conn);


            return vr;
        }

        public List<string> FindValuesOfIDs(string tabulka, List<int> idFces)
        {
            List<string> vr = new List<string>();
            foreach (int var in idFces)
            {
                vr.Add(StoredProceduresSqliteI.ci.FindValueOfID(tabulka, var));
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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

        /// <summary>
        /// Is no rows will affect, return -1, not 0
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string p)
        {
            SQLiteCommand comm = new SQLiteCommand(p, _conn);
            return comm.ExecuteNonQuery();
        }

        public double UpdatePlusRealValue(string table, string sloupecKUpdate, double pridej, string sloupecID, int id)
        {
            double d = double.Parse(StoredProceduresSqliteI.ci.GetElementDataTable(table, sloupecID, id, sloupecKUpdate));
            double n = pridej;
            if (d != 0)
            {
                n = (d + pridej) / 2;
            }
            StoredProceduresSqliteI.ci.Update(table, sloupecID, id, sloupecKUpdate, n);
            return n;
        }

        private int Update(string table, string sloupecID, int id, string sloupecKUpdate, double n)
        {
            //
            string sql = SH.Format2("UPDATE {0} SET {1}={2} WHERE {3} = {4}", table, sloupecKUpdate, StoredProceduresSqlite.ci.ReplaceValueOnlyOne(n), sloupecID, StoredProceduresSqlite.ci.ReplaceValueOnlyOne(id));
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
            return GetDataTable(SH.Format2("SELECT {0} FROM {1}", StoredProceduresSqlite.ci.GetColumnsWithoutBracets(selectSloupce), p));
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