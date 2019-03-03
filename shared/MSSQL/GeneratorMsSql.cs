using System.Text;

using System;

using System.Diagnostics;
using System.Data.SqlClient;

public partial class GeneratorMsSql
{

    public static string OutputDeleted(string sloupceJezVratit)
    {
        var cols = SH.Split(sloupceJezVratit, ",");
        if (cols.Count != 0)
        {
            cols = CA.Trim(cols);
            StringBuilder sb = new StringBuilder();
            sb.Append(" OUTPUT ");
            foreach (var item in cols)
            {
                sb.Append("DELETED." + item + ",");
            }
            return sb.ToString().TrimEnd(',');
        }
        else
        {
            return " ";
        }
    }

    public static string Insert4(int i2, string tabulka, int pocetSloupcu)
    {
        string hodnoty = GetValuesDirect(i2, pocetSloupcu);
        return SH.Format2("INSERT INTO {0} VALUES {1}", tabulka, hodnoty);
    }

    public static string GetValuesDirect(int i2, int to)
    {
        to += i2;
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        for (int i = i2; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + ",");
        }
        return sb.ToString().TrimEnd(',') + ")";
    }

    /// <summary>
    /// Vrátí i s parametry, nezapomeň tyto parametry pak přidat do příkazu
    /// </summary>
    /// <param name="sets"></param>
    /// <returns></returns>
    public static string CombinedSet(params AB[] sets)
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
                sb.Append(",");
            }
            sb.Append(SH.Format2(" {0} = @p" + p.ToString(), var.A));
            p++;
        }
        return sb.ToString();
    }



    public static string CombinedWhereNotEquals(bool continuing, ref int pridavatOd, AB[] whereIsNot)
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
                    sb.Append(" WHERE ");
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
    /// Pokud nechceš řadit podle určitého sloupce, dej do parametru orderBy null
    /// </summary>
    /// <param name="commandBeforeWhere"></param>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    /// <param name="greaterThanWhere"></param>
    /// <param name="lowerThanWhere"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    public static SqlCommand CombinedWhereCommand(string commandBeforeWhere, AB[] where, AB[] isNotWhere, AB[] greaterThanWhere, AB[] lowerThanWhere, string orderBy)
    {
        SqlCommand comm = new SqlCommand();
        StringBuilder sb = new StringBuilder();
        sb.Append(commandBeforeWhere);
        if (!commandBeforeWhere.Contains(" WHERE "))
        {
        sb.Append(" WHERE ");    
        }
        
        bool první = true;
        int p = 0;
        if (where != null)
        {
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
                MSStoredProceduresI.AddCommandParameter(comm, p, var.B);
                p++;
            }
        }
        if (isNotWhere != null)
        {
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
                sb.Append(SH.Format2(" {0} != {1} ", var.A, "@p" + p));
                MSStoredProceduresI.AddCommandParameter(comm, p, var.B);
                p++;
            }
        }
        if (greaterThanWhere != null)
        {
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
                MSStoredProceduresI.AddCommandParameter(comm, p, var.B);
                p++;
            }
        }
        if (lowerThanWhere != null)
        {
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
                MSStoredProceduresI.AddCommandParameter(comm, p, var.B);
                p++;
            }
        }
        if (orderBy != null)
        {
            sb.Append("ORDER BY " + orderBy + " ");
        }
        comm.CommandText = sb.ToString();
        return comm;
    }

    public static string CombinedWhereOR(AB[] where, ref int p)
    {
        StringBuilder sb = new StringBuilder();
        int pCopy = p;
        if (p == 0)
        {
            if (where.Length != 0)
            {
                sb.Append(" WHERE ");
            }
        }
        else
        {
            sb.Append("(");
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
            sb.Append(")");
        }
        return sb.ToString();
    }


    public static string CombinedWhereOR(AB[] where)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" WHERE ");
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

    /// <summary>
    /// object hodnota se musí přidat pak k SqlCommand ručně pod @p0
    /// Vrátí pouze klazuli where
    /// </summary>
    /// <param name="p"></param>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public static string SimpleWhere( string sloupec)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" WHERE ");
        sb.Append(SH.Format2(" {0} = @p0 ", sloupec));
        return sb.ToString();
    }

    public static string SimpleWhere(string sloupec, int pocetJizPridanychParametru)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" WHERE ");
        sb.Append(SH.Format2(" {0} = @p{1} ", sloupec, pocetJizPridanychParametru));
        return sb.ToString();
    }

    public static string SimpleWhere(string columns, string tabulka, string sloupec)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT " + columns);
        sb.Append(" FROM " + tabulka);
        sb.Append(" WHERE ");
        sb.Append(SH.Format2(" {0} = @p0 ", sloupec));
        return sb.ToString();
    }

    /// <summary>
    /// po vytvoření comm je třeba ručně přidat idValue
    /// </summary>
    /// <param name="vracenySloupec"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public static string SimpleWhereOneRow(string vracenySloupec, string table, string idColumnName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT TOP(1) " + vracenySloupec);
        sb.Append(" FROM " + table);
        sb.Append(" WHERE ");
        sb.Append(SH.Format2(" {0} = @p0 ", idColumnName));
        return sb.ToString();
    }



    public static string SimpleSelectOneRow(string vracenySloupec, string table)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT TOP(1) " + vracenySloupec);
        sb.Append(" FROM " + table);
        sb.Append(" ");
        return sb.ToString();
    }







    public static string OrderBy(string orderByColumn, SortOrder sortOrder)
    {
        if (sortOrder == SortOrder.Unspecified)
        {
            return "";
        }
        string vr = " ORDER BY " + orderByColumn;
        if (sortOrder == SortOrder.Ascending)
        {
            vr += " ASC";
        }
        else
        {
            vr += " DESC";
        }
        return vr;
    }
}

