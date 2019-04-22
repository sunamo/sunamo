using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace DocArch.SqLite
{
    public class UlozeneProceduryI : IUlozeneProceduryI
    {
        SQLiteConnection conn = DatabaseLayer.conn;
        public static UlozeneProceduryI ci = new UlozeneProceduryI();






        public string[] VratNazvySloupcuTabulky(string p)
        {
            List<string> vr = new List<string>();
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT sql FROM sqlite_master WHERE tbl_name = '{0}' AND type = 'table'", p), conn);
            SQLiteDataReader dr = comm.ExecuteReader(CommandBehavior.SingleRow);
            string sql = null;
            object o = dr.GetValue(0);
            sql = o.ToString();
            string s = SH.Substring(sql, sql.IndexOf('(') + 1, sql.LastIndexOf(')') - 1);
            string[] sloupce = s.Split(',');
            for (int i = 0; i < sloupce.Length; i++)
            {
                string[] g = sloupce[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                vr.Add(g[0]);
            }
            return vr.ToArray();
        }

        public DataTable GetDataTable(string sql)
        {

            SQLiteCommand comm = new SQLiteCommand(sql, conn);
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
            return GetDataTable("SELECT * FROM " + p);
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
        /// G aktu�ln� po�et ��dk� v tabulce A1 po vlo�en�.
        /// Do A2 se to zad�v� bez uvozovek.
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="nazvySloupcu"></param>
        /// <param name="sloupce"></param>
        /// <returns></returns>
        public int InsertToTable(string tabulka, string nazvySloupcu, params object[] sloupce)
        {
            string hodnoty = UlozeneProcedury.ci.GetValues(sloupce);
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("INSERT INTO {0} {1} VALUES {2}", tabulka, nazvySloupcu, UlozeneProcedury.ci.GetValues(sloupce)), conn);
            comm.ExecuteNonQuery();
            return UlozeneProceduryI.ci.FindOutNumberOfRows(tabulka);
        }

        /// <summary>
        /// G v�echny ��dky z tabulky A1 kde sloupec A2 bude m�t hodnotu A3.
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupec"></param>
        /// <param name="hodnota"></param>
        /// <returns></returns>
        public DataTable GetDataTableSelective(string tabulka, string sloupec, object hodnota)
        {

            return GetDataTable("SELECT * FROM " + tabulka + " WHERE " + sloupec + " = " + UlozeneProcedury.ci.ReplaceValueOnlyOne(hodnota));
        }

        private DataTable GetDataTableSelective(string tabulka, string sloupecID, int id, string hledanySloupec)
        {
            return GetDataTable("SELECT " + hledanySloupec + " FROM " + tabulka + " WHERE " + sloupecID + " = " + UlozeneProcedury.ci.ReplaceValueOnlyOne(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabulka"></param>
        /// <param name="sloupecHledan�"></param>
        /// <param name="sloupecVeKteremHledat"></param>
        /// <param name="hodnota"></param>
        /// <returns></returns>
        public List<int> GetValueColumnInt(string tabulka, string sloupecHledan�, string sloupecVeKteremHledat, object hodnota)
        {
            string sql = SH.Format2("SELECT {0} FROM {1} WHERE {2} = {3}", sloupecHledan�, tabulka, sloupecVeKteremHledat, UlozeneProcedury.ci.ReplaceValueOnlyOne(hodnota));
            //SQLiteCommand comm = new SQLiteCommand(sql, conn);

            return GetValuesAllRowsInt(sql);

            //return vr;
        }

        private List<int> GetValuesAllRowsInt(string sql)
        {
            List<int> vr = new List<int>();
            SQLiteCommand comm = new SQLiteCommand(sql, conn);
            DataTable dt = UlozeneProceduryI.ci.GetDataTable(comm);
            foreach (DataRow var in dt.Rows)
            {
                vr.Add(int.Parse(var.ItemArray[0].ToString()));
            }
            return vr;
        }

        private List<string> GetValuesAllRowsString(string sql)
        {
            List<string> vr = new List<string>();
            SQLiteCommand comm = new SQLiteCommand(sql, conn);
            DataTable dt = UlozeneProceduryI.ci.GetDataTable(comm);
            foreach (DataRow var in dt.Rows)
            {
                vr.Add(var.ItemArray[0].ToString());
            }
            return vr;
        }

        /// <summary>
        /// ---Pozor, pokud bude v�ce ��dk� vyhovovat A2,3, vr�t� to z�ejm� hodnoty od v�ech. +++U� ne, d�l�m to p�es tabulku a ne reader
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="nazevSloupce"></param>
        /// <param name="hodnotaSloupce"></param>
        /// <returns></returns>
        public List<object> GetValuesAllRows(string tabulka, string nazevSloupce, object hodnotaSloupce)
        {
            List<object> vr = new List<object>();
            SQLiteCommand comm = new SQLiteCommand(SH.Format2("SELECT * FROM {0} WHERE {1} = {2}", tabulka, nazevSloupce, UlozeneProcedury.ci.ReplaceValueOnlyOne(hodnotaSloupce)), conn);


            return vr;
        }

        public List<string> FindValuesOfIDs(string tabulka, List<int> idFces)
        {
            List<string> vr = new List<string>();
            foreach (int var in idFces)
            {
                vr.Add(UlozeneProceduryI.ci.FindValueOfID(tabulka, var));
            }
            return vr;
        }

        public List<string> VratVsechnyHodnotySloupce(string tabulka, string sloupec)
        {
            //DataTable dt = UlozeneProceduryI.ci.VratDataTable();
            return GetValuesAllRowsString(SH.Format2("SELECT {0} FROM {1}", sloupec, tabulka));
        }

        /// <summary>
        /// A2 je kombinace klazul� WHERE
        /// </summary>
        /// <param name="p"></param>
        /// <param name="aB"></param>
        /// <returns></returns>
        public bool ExistsCombination(string p, params AB[] aB)
        {
            string sql = SH.Format2("SELECT {0} FROM {1} {2}", aB[0].A, p, GeneratorSqLite.CombinedWhere(aB));
            DataTable dt = GetDataTable(sql);
            return dt.Rows.Count != 0;
        }

        /// <summary>
        /// Funguje pouze kdy� je sloupec nazev na indexu 1.
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
        /// G A3. prvek v radku tak� indexu A2 tabulky A1.
        /// M��e vr�tit i null pokud dostanu null. Pokud ��dek nebo sloupec nenalezne, v�hod� v�jimku
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
            throw new Exception("Zadan� bu�ka nebyla nalezena");
        }

        internal int ExecuteNonQuery(string p)
        {
            SQLiteCommand comm = new SQLiteCommand(p, conn);
            return comm.ExecuteNonQuery();
        }

        public double UpdateRealValue(string table, string sloupecID, int id, string sloupecKUpdate, double pridej)
        {
            double d = double.Parse(UlozeneProceduryI.ci.GetElementDataTable(table, sloupecID, id, sloupecKUpdate));
            double n = pridej;
            if (d != 0)
            {
                n = (d + pridej) / 2;
            }
            UlozeneProceduryI.ci.Update(table, sloupecID, id, sloupecKUpdate, n);
            return n;
        }

        private int Update(string table, string sloupecID, int id, string sloupecKUpdate, double n)
        {
            //
            string sql = SH.Format2("UPDATE {0} SET {1}={2} WHERE {3} = {4}", table, sloupecKUpdate, UlozeneProcedury.ci.ReplaceValueOnlyOne(n), sloupecID, UlozeneProcedury.ci.ReplaceValueOnlyOne(id));
            SQLiteCommand comm = new SQLiteCommand(sql, conn);
            return comm.ExecuteNonQuery();
        }

        public string GetElementDataTable(string table, string sloupecID, int id, string hledanySloupec)
        {
            DataTable dt = GetDataTableSelective(table, sloupecID, id, hledanySloupec);
            return GetElementDataTable(dt, 0, 0);
        }



        internal DataTable GetDataTable(string p, params string[] selectSloupce)
        {
            return GetDataTable(SH.Format2("SELECT {0} FROM {1}", UlozeneProcedury.ci.GetColumnsWithoutBracets(selectSloupce), p));
        }
    }
}
