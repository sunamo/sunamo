using System.Text;

using System;

using System.Diagnostics;
using System.Data.SqlClient;

public partial class GeneratorMsSql
{
    

    /// <summary>
    /// Pokud nechceš řadit podle určitého sloupce, dej do parametru orderBy null
    /// </summary>
    /// <param name="commandBeforeWhere"></param>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    /// <param name="greaterThanWhere"></param>
    /// <param name="lowerThanWhere"></param>
    /// <param name="orderBy"></param>
    public static SqlCommand CombinedWhereCommand(string commandBeforeWhere, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere, string orderBy)
    {
        SqlCommand comm = new SqlCommand();
        StringBuilder sb = new StringBuilder();
        sb.Append(commandBeforeWhere);
        if (!commandBeforeWhere.Contains(" " + "WHERE" + " "))
        {
        sb.Append(" " + "WHERE" + " ");    
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
                SqlOperations.AddCommandParameter(comm, p, var.B);
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
                SqlOperations.AddCommandParameter(comm, p, var.B);
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
                SqlOperations.AddCommandParameter(comm, p, var.B);
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
                SqlOperations.AddCommandParameter(comm, p, var.B);
                p++;
            }
        }
        if (orderBy != null)
        {
            sb.Append("ORDER BY" + " " + orderBy + AllStrings.space);
        }
        comm.CommandText = sb.ToString();
        return comm;
    }

    private static string GetTable(string commandBeforeWhere)
    {
        return null;
    }
}