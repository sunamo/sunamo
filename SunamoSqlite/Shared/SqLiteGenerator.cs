using System;
using System.Collections.Generic;
using System.Text;

namespace DocArch.SqLite
{
    static class GeneratorSqLite
    {
        internal static object CombinedWhere(AB[] aB)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" WHERE ");
            bool prvn� = true;
            foreach (AB var in aB)
            {
                if (prvn�)
                {
                    prvn� = false;
                }
                else
                {
                    sb.Append(" AND ");
                }
                sb.Append(SH.Format2(" {0} = {1} ", var.A, UlozeneProcedury.ci.VratHodnotuJednu(var.B)));
            }
            return sb.ToString();
        }
    }
}
