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