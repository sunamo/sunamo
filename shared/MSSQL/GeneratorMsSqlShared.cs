using System.Text;
using System;
using System.Diagnostics;
using System.Data.SqlClient;

public partial class GeneratorMsSql{ 
/// <summary>
    /// Na začátek přidá where pokud obsahuje A1 obsahoval nějaké prvky
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public static string CombinedWhere(AB[] where)
    {
        int pridavatOd = 0;
        return CombinedWhere(where, ref pridavatOd);
    }
/// <summary>
    /// Na začátek přidá where pokud obsahuje A1 obsahoval nějaké prvky
    /// </summary>
    /// <param name="where"></param>
    /// <param name="pridavatOd"></param>
    /// <returns></returns>
    public static string CombinedWhere(AB[] where, ref int pridavatOd)
    {
        StringBuilder sb = new StringBuilder();
        if (where != null)
        {
            if (where.Length > 0)
            {
                sb.Append(" WHERE ");
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
/// <summary>
    /// Snaž se tuto metodu používat co nejméně, protože musí všechny parametry(kolekce) převést metodou ToArray() na pole
    /// </summary>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    /// <param name="greaterThanWhere"></param>
    /// <param name="lowerThanWhere"></param>
    /// <returns></returns>
    public static string CombinedWhere(ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere)
    {
        if (where == null)
        {
            where = new ABC();
        }
        if (isNotWhere == null)
        {
            isNotWhere = new ABC();
        }
        if (greaterThanWhere == null)
        {
            greaterThanWhere = new ABC();
        }
        if (lowerThanWhere == null)
        {
            lowerThanWhere = new ABC();
        }
        return CombinedWhere(where.ToArray(), isNotWhere.ToArray(), greaterThanWhere.ToArray(), lowerThanWhere.ToArray());
    }
/// <summary>
    /// Jakýkoliv z A1-4 může být null, v takovém případě se pouze toto pole překosčí
    /// Poté se musejí přidat AB.B z A1 a až poté z A2
    /// </summary>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    /// <returns></returns>
    public static string CombinedWhere(AB[] where, AB[] isNotWhere, AB[] greaterThanWhere, AB[] lowerThanWhere)
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
        if (asponNeco)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" WHERE ");
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
                    p++;
                }
            }
            return sb.ToString();
        }
        return "";
    }
public static string CombinedWhere(string tabulka, bool top1, string nazvySloupcu, AB[] ab)
    {
        string t1 = "";
        if (top1)
        {
            t1 = "TOP(1) ";
        }
        return "SELECT " + t1 + nazvySloupcu + " FROM " + tabulka + CombinedWhere(ab);
    }
}