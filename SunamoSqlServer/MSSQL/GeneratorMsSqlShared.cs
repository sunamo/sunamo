using System.Text;
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Linq;

public partial class GeneratorMsSql{
    /// <summary>
    /// Its object, not AB, due to automatic renaming
    /// </summary>
    /// <param name="where"></param>
    public static string CombinedWhere(params AB[] where)
    {
        //var abc = where.Cast<AB>();
        return CombinedWhere(new ABC(where));
    }

        /// <summary>
        /// Na začátek přidá where pokud obsahuje A1 obsahoval nějaké prvky
        /// </summary>
        /// <param name="where"></param>
        public static string CombinedWhere(ABC where)
    {
        int pridavatOd = 0;
        return CombinedWhere(where, ref pridavatOd);
    }

    public static string CombinedWhereNotEquals(bool continuing, ref int pridavatOd, ABC whereIsNot)
    {
        if (whereIsNot != null)
        {
            StringBuilder sb = new StringBuilder();
            if (whereIsNot.Length != 0)
            {
                if (continuing)
                {
                    sb.Append(" AND ");
                }
                else
                {
                    sb.Append(" " + "WHERE" + " ");
                }
            }
            bool první = true;


            foreach (AB var in whereIsNot)
            {
                if (první)
                {
                    první = false;
                }
                else
                {
                    sb.Append(" AND ");
                }
                sb.Append(SH.Format2(" {0} != {1} ", var.A, "@p" + pridavatOd));
                pridavatOd++;
            }

            return sb.ToString();
        }
        return "";
    }

    /// <summary>
    /// po vytvoření comm je třeba ručně přidat idValue
    /// </summary>
    /// <param name="vracenySloupec"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idValue"></param>
    public static string SimpleWhereOneRow(string vracenySloupec, string table, string idColumnName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT TOP(1)" + " " + vracenySloupec);
        sb.Append(" " + "FROM" + " " + table);
        sb.Append(" " + "WHERE" + " ");
        sb.Append(SH.Format2(" {0} = @p0 ", idColumnName));
        return sb.ToString();
    }
    
    /// <summary>
    /// object hodnota se musí přidat pak k SqlCommand ručně pod @p0
    /// Vrátí pouze klazuli where
    /// </summary>
    /// <param name="p"></param>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public static string SimpleWhere(string sloupec)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" " + "WHERE" + " ");
        sb.Append(SH.Format2(" {0} = @p0 ", sloupec));
        return sb.ToString();
    }

    public static string SimpleWhere(string sloupec, int pocetJizPridanychParametru)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" " + "WHERE" + " ");
        sb.Append(SH.Format2(" {0} = @p{1} ", sloupec, pocetJizPridanychParametru));
        return sb.ToString();
    }

    public static string SimpleWhere(string columns, string tabulka, string sloupec)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT" + " " + columns);
        sb.Append(" " + "FROM" + " " + tabulka);
        sb.Append(" " + "WHERE" + " ");
        sb.Append(SH.Format2(" {0} = @p0 ", sloupec));
        return sb.ToString();
    }
    public static string OrderBy(string orderByColumn, SortOrder sortOrder)
    {
        if (sortOrder == SortOrder.Unspecified)
        {
            return "";
        }
        string vr = " " + "ORDER BY" + " " + orderByColumn;
        if (sortOrder == SortOrder.Ascending)
        {
            vr += " ASC";
        }
        else
        {
            vr += " " + "DESC";
        }
        return vr;
    }
    public static string SimpleSelectOneRow(string vracenySloupec, string table)
    {
        return "SELECT TOP(1) " + vracenySloupec + " FROM " + table;
    }

    /// <summary>
    /// Anything of args can be null
    /// Na začátek přidá where pokud obsahuje A1 obsahoval nějaké prvky
    /// </summary>
    /// <param name="where"></param>
    /// <param name="pridavatOd"></param>
    public static string CombinedWhere(ABC where, ref int pridavatOd)
    {
        StringBuilder sb = new StringBuilder();
        if (where != null)
        {
            if (where.Length > 0)
            {
                sb.Append(" " + "WHERE" + " ");
            }

            bool první = true;


            foreach (AB var in where)
            {
                if (první)
                {
                    první = false;
                }
                else
                {
                    sb.Append(" AND ");
                }
                sb.Append(SH.Format2(" {0} = {1} ", var.A, "@p" + pridavatOd));
                pridavatOd++;
            }

        }
        return sb.ToString();
    }

///// <summary>
//    /// Snaž se tuto metodu používat co nejméně, protože musí všechny parametry(kolekce) převést metodou ToArray() na pole
//    /// </summary>
//    /// <param name="where"></param>
//    /// <param name="isNotWhere"></param>
//    /// <param name="greaterThanWhere"></param>
//    /// <param name="lowerThanWhere"></param>
//    /// <returns></returns>
//    public static string CombinedWhere(ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere)
//    {
//        if (where == null)
//        {
//            where = new ABC();
//        }
//        if (isNotWhere == null)
//        {
//            isNotWhere = new ABC();
//        }
//        if (greaterThanWhere == null)
//        {
//            greaterThanWhere = new ABC();
//        }
//        if (lowerThanWhere == null)
//        {
//            lowerThanWhere = new ABC();
//        }
//        return CombinedWhere(where.ToArray(), isNotWhere.ToArray(), greaterThanWhere.ToArray(), lowerThanWhere.ToArray());
//    }

/// <summary>
    /// Jakýkoliv z A1-4 může být null, v takovém případě se pouze toto pole překosčí
    /// Poté se musejí přidat AB.B z A1 a až poté z A2
    /// </summary>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    public static string CombinedWhere(ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere, ABC whereOr = null)
    {
        bool asponNeco = false;
        if (where != null)
        {
            if (where.Length != 0)
            {
                asponNeco = true;
            }
        }

        if (!asponNeco)
        {
            if (isNotWhere != null)
            {
                if (isNotWhere.Length != 0)
                {
                    asponNeco = true;
                }
            }
        }
        if (!asponNeco)
        {
            if (greaterThanWhere != null)
            {
                if (greaterThanWhere.Length != 0)
                {
                    asponNeco = true;
                }
            }
        }
        if (!asponNeco)
        {
            if (lowerThanWhere != null)
            {
                if (lowerThanWhere.Length != 0)
                {
                    asponNeco = true;
                }
            }
        }
        if (!asponNeco)
        {
            if (whereOr != null)
            {
                if (whereOr.Length != 0)
                {
                    asponNeco = true;
                }
            }
        }
        if (asponNeco)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + "WHERE" + " ");
            bool první = true;
            int p = 0;
            if (where != null)
            {
                if (where.Count > 0)
                {
                    //
                    foreach (AB var in where)
                    {
                        if (první)
                        {
                            první = false;
                            
                        }
                        else
                        {
                            sb.Append(" AND ");
                        }
                        sb.Append(SH.Format2(" {0} = {1} ", var.A, "@p" + p));
                        p++;
                    }
                    
                }
            }
            if (isNotWhere != null)
            {
                if (isNotWhere.Count > 0)
                {
                    //
                    foreach (AB var in isNotWhere)
                    {
                        if (první)
                        {
                            první = false;
                            
                        }
                        else
                        {
                            sb.Append(" AND ");
                        }

                        if (SqlServerHelper.IsNull(var.B))
                        {
                            sb.Append(SH.Format2(" {0} is not null ", var.A));
                        }
                        else
                        {
                            sb.Append(SH.Format2(" {0} != {1} ", var.A, "@p" + p));
                        }

                        p++;
                    }
                    
                }
            }
            if (greaterThanWhere != null)
            {
                if (greaterThanWhere.Count > 0)
                {
                    //
                    foreach (AB var in greaterThanWhere)
                    {
                        if (první)
                        {
                            
                            první = false;
                        }
                        else
                        {
                            sb.Append(" AND ");
                        }
                        sb.Append(SH.Format2(" {0} > {1} ", var.A, "@p" + p));
                        p++;
                    }
                    //
                }
            }
            if (lowerThanWhere != null)
            {
                if (lowerThanWhere.Count > 0)
                {
                    //
                    foreach (AB var in lowerThanWhere)
                    {
                        if (první)
                        {
                            
                            první = false;
                        }
                        else
                        {
                            sb.Append(" AND ");
                        }
                        sb.Append(SH.Format2(" {0} < {1} ", var.A, "@p" + p));
                        p++;
                    }
                    
                }
            }
            první = true;
            if (whereOr != null)
            {
                if (whereOr.Count > 0)
                {
                    
                    foreach (AB var in whereOr)
                    {
                        if (první)
                        {
                            první = false;
                            
                            sb.Append(" AND ");
                            sb.Append(AllStrings.lb);
                        }
                        else
                        {
                            sb.Append(" OR ");
                        }
                        sb.Append(SH.Format2(" {0} = {1} ", var.A, "@p" + p));
                        p++;
                    }
                    sb.Append(AllStrings.rb);
                }
            }
            return sb.ToString();
        }
        return "";
    }

    public static string CombinedWhere(string tabulka, bool top1, string nazvySloupcu, params AB[] ab)
    {
        return CombinedWhere(tabulka, top1, nazvySloupcu, new ABC(ab));
    }
public static string CombinedWhere(string tabulka, bool top1, string nazvySloupcu, ABC ab)
    {
        string t1 = "";
        if (top1)
        {
            t1 = "TOP(1)" + " ";
        }
        return "SELECT" + " " + t1 + nazvySloupcu + " " + "FROM" + " " + tabulka + CombinedWhere(ab);
    }

/// <summary>
    /// Může vrátit null když tabulka bude existovat
    /// Výchozí pro A3 je true
    /// A3 - whether is not desirable to create references to other tables. Good while test tables and apps, when I will it delete later.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupce"></param>
    /// <param name="p"></param>
    public static string CreateTable(string table, object sloupce2,  bool dynamicTables, SqlConnection conn)
    {
        var sloupce = (MSColumnsDB)sloupce2;

        StringBuilder sb = new StringBuilder();
        bool exists = MSStoredProceduresI.ci.SelectExistsTable(table, conn);
        if (!exists)
        {
            sb.AppendFormat("CREATE TABLE {0}(", table);
            foreach (MSSloupecDB var in sloupce)
            {
                sb.Append(GeneratorMsSql.Column(var, table, dynamicTables) + AllStrings.comma);
            }
            string dd = sb.ToString();
            dd = dd.TrimEnd(AllChars.comma);
            string vr = dd + AllStrings.rb;
            //vr);
            return vr;
        }
        return null;
    }

/// <summary>
    /// A3 pokud nechci aby se mi vytvářeli reference na ostatní tabulky. Vhodné při testování tabulek a programů, kdy je pak ještě budu mazat a znovu plnit.
    /// </summary>
    /// <param name="var"></param>
    /// <param name="inTable"></param>
    /// <param name="dynamicTables"></param>
    private static string Column(MSSloupecDB var, string inTable, bool dynamicTables)
    {
        InstantSB sb = new InstantSB(AllStrings.space);

        sb.AddItem((object)var.Name);
        sb.AddItem((object)(var.Type + var.Delka));
        var t = var.Type;
        /*Tyto typy které používám nemůžou obsahovat text a collace je u nich zakázána
         * t == System.Data.SqlDbType.BigInt || 
         * t == System.Data.SqlDbType.Int ||
         * t == System.Data.SqlDbType.Bit || 
         * t == System.Data.SqlDbType.TinyInt || 
         * t == System.Data.SqlDbType.SmallInt || 
         * t == System.Data.SqlDbType.UniqueIdentifier || 
         * t == System.Data.SqlDbType.Date || 
         * t == System.Data.SqlDbType.SmallDateTime || 
         * t == System.Data.SqlDbType.Real ||  
         * t == System.Data.SqlDbType.Binary ||
         * t == System.Data.SqlDbType.Decimal ||
         */
        if (
            t == SqlDbType2.VarChar || 
            t == SqlDbType2.Char || 
            t == SqlDbType2.NVarChar || 
            t == SqlDbType2.NChar
            )
        {
            // Musí to být AI, protože když bych měl slovo è, SQL Server by mi vrátil že neexistuje ale když bych ho chtěl vložit, udělal by z něho "e" a vrátil by chybu
            sb.AddItem((object)"COLLATE Czech_CS_AS_KS_WS");    
        }
        
        if (!var.CanBeNull)
        {
            sb.AddItem((object)"NOT NULL");
        }
        if (var.PrimaryKey || var.MustBeUnique)
        {
            if (var.PrimaryKey)
            {
                sb.AddItem((object)"PRIMARY KEY");
            }
            else
            {
                sb.AddItem((object)"UNIQUE");
            }
        }
        if (var.IsNewId)
        {
            sb.AddItem((object)"DEFAULT(newid())");
            //sb.AddItem("DEFAULT newsequentialid()");
        }
        if (!dynamicTables)
        {
            if (var.referencesTable != null)
            {
                sb.AddItem((object)"CONSTRAINT");
                sb.AddItem((object)("fk_" + var.Name + AllStrings.lowbar + inTable + AllStrings.lowbar + var.referencesTable + AllStrings.lowbar + var.referencesColumn));
                sb.AddItem((object)"FOREIGN KEY REFERENCES");
                sb.AddItem((object)(var.referencesTable + AllStrings.lb + var.referencesColumn + AllStrings.rb));
            }
        }

        
        return sb.ToString();
    }

public static string GetValuesDirect(int i2, int to)
    {
        to += i2;
        StringBuilder sb = new StringBuilder();
        
        for (int i = i2; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + AllStrings.comma);
        }
        return sb.ToString().TrimEnd(AllChars.comma) + AllStrings.rb;
    }

    public static string CombinedSet(params AB[] sets)
    {
        return CombinedSet(new ABC(sets));
    }

/// <summary>
/// Vrátí i s parametry, nezapomeň tyto parametry pak přidat do příkazu
/// </summary>
/// <param name="sets"></param>
    public static string CombinedSet( ABC sets)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" SET ");
        bool první = true;
        int p = 0;
        foreach (AB var in sets)
        {
            if (první)
            {
                první = false;
            }
            else
            {
                // TODO: Zjistit si zda se tu skutečně dává AND
                sb.Append(AllStrings.comma);
            }
            sb.Append(SH.Format2(" {0} = @p" + p.ToString(), var.A));
            p++;
        }
        return sb.ToString();
    }

public static string CombinedWhereOR(ABC where, ref int p)
    {
        StringBuilder sb = new StringBuilder();
        int pCopy = p;
        if (p == 0)
        {
            if (where.Length != 0)
            {
                sb.Append(" " + "WHERE" + " ");
            }
        }
        else
        {
            
        }
        
        bool první = true;
        

        foreach (AB var in where)
        {
            if (první && p == 0)
            {
                první = false;
            }
            else
            {
                sb.Append(" OR ");
            }
            sb.Append(SH.Format2(" {0} = {1} ", var.A, "@p" + p));
            p++;
        }
        if (pCopy != 0)
        {
            
        }
        return sb.ToString();
    }

    public static ABC CombinedWhereOR(string id, params object[] where)
    {
        ABC abc = new ABC();

        var s = CA.ToListString(where);

        foreach (var item in s)
        {
            abc.Add(AB.Get(id, item));
        }

        return abc;
    }

    public static string CombinedWhereOR(ABC where)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" " + "WHERE" + " ");
        bool první = true;
        int p = 0;

        foreach (AB var in where)
        {
            if (první)
            {
                první = false;
            }
            else
            {
                sb.Append(" OR ");
            }

            sb.Append(SH.Format2(" {0} = {1} ", var.A, "@p" + p));
            p++;
        }

        return sb.ToString();
    }

public static string OutputDeleted(string sloupceJezVratit)
    {
        var cols = SH.Split(sloupceJezVratit, AllStrings.comma);
        if (cols.Count != 0)
        {
            cols = CA.Trim(cols);
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + "OUTPUT" + " ");
            foreach (var item in cols)
            {
                sb.Append("DELETED" + "." + item + AllStrings.comma);
            }
            return sb.ToString().TrimEnd(AllChars.comma);
        }
        else
        {
            return AllStrings.space;
        }
    }

public static string Insert4(int i2, string tabulka, int pocetSloupcu)
    {
        string hodnoty = GetValuesDirect(i2, pocetSloupcu);
        return SH.Format2("INSERT INTO {0} VALUES {1}", tabulka, hodnoty);
    }
}