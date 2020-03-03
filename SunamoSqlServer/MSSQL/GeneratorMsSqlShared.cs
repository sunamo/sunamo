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
            sb.Append(AllStrings.lb);
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
            sb.Append(AllStrings.rb);
        }
        return sb.ToString();
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