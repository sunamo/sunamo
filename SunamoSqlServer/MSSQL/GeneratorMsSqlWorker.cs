using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GeneratorMsSqlWorker
{
    /// <summary>
    /// Pokud nechceš řadit podle určitého sloupce, dej do parametru orderBy null
    /// CombinedWhereCommand = as first is commandBeforeWhere, return SqlCommand
    /// CombinedWhere = return only where
    /// </summary>
    /// <param name="commandBeforeWhere"></param>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    /// <param name="greaterThanWhere"></param>
    /// <param name="lowerThanWhere"></param>
    /// <param name="orderBy"></param>
    public static SqlCommand CombinedWhereCommand(bool param, string commandBeforeWhere, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere, string orderBy)
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
        string val = null;

        if (where != null)
        {
            foreach (AB var in where)
            {
                AddParam(ref val, param, comm, ref p, var, sb, ref první, AllStrings.equals);
            }
        }

        if (isNotWhere != null)
        {
            foreach (AB var in isNotWhere)
            {
                AddParam(ref val, param, comm, ref p, var, sb, ref první, Consts.isNot);
            }
        }
        if (greaterThanWhere != null)
        {
            foreach (AB var in greaterThanWhere)
            {

                AddParam(ref val, param, comm, ref p, var, sb, ref první, AllStrings.gt);
            }
        }
        if (lowerThanWhere != null)
        {
            foreach (AB var in lowerThanWhere)
            {

                AddParam(ref val, param, comm, ref p, var, sb, ref první, AllStrings.lt);
            }
        }
        if (orderBy != null)
        {
            sb.Append("ORDER BY" + " " + orderBy + AllStrings.space);
        }
        comm.CommandText = sb.ToString();
        return comm;
    }

    private static void AddParam(ref string val, bool param, SqlCommand comm, ref int p, AB var, StringBuilder sb, ref bool první, string op, bool or = false, string appendWhenWillBeFirst = null)
    {
        if (první)
        {
            první = false;
            if (appendWhenWillBeFirst != null)
            {
                sb.Append(appendWhenWillBeFirst);
            }
        }
        else
        {
            if (or)
            {
                sb.Append(" OR ");
            }
            else
            {
                sb.Append(" AND ");
            }
        }

        if (param)
        {
            val = "@p" + p;
            if (comm != null)
            {
                MSStoredProceduresI.AddCommandParameter(comm, p, var.B);
            }
        }
        else
        {
            val = var.B.ToString();
        }

        if (op == Consts.isNot)
        {
            if (SqlServerHelper.IsNull(var.B))
            {
                sb.Append(SH.Format2(" {0} is not null ", var.A));
            }
            else
            {
                sb.Append(SH.Format2(" {0} != {1} ", var.A, "@p" + p));
            }
        }
        else
        {
            sb.Append(SH.Format2(" {0} " + op + " {1} ", var.A, val));
        }

        p++;
    }

    /// <summary>
    /// Jakýkoliv z A1-4 může být null, v takovém případě se pouze toto pole překosčí
    /// Poté se musejí přidat AB.B z A1 a až poté z A2
    /// </summary>
    /// <param name="where"></param>
    /// <param name="isNotWhere"></param>
    public static string CombinedWhere(bool param, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere, ABC whereOr = null)
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
            string val = null;
            if (where != null)
            {
                if (where.Count > 0)
                {
                    foreach (AB var in where)
                    {
                        //
                        AddParam(ref val, param, null, ref p, var, sb, ref první, AllStrings.equals);

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
                        AddParam(ref val, param, null, ref p, var, sb, ref první, Consts.isNot);
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
                        AddParam(ref val, param, null, ref p, var, sb, ref první, AllStrings.lt);
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
                        AddParam(ref val, param, null, ref p, var, sb, ref první, AllStrings.equals, true, AllStrings.lb);
                    }
                    sb.Append(AllStrings.rb);
                }
            }
            return sb.ToString();
        }
        return "";
    }
}
