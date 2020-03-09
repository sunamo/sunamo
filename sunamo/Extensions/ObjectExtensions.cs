using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ObjectExtensions
{
    public static string GetStackTrace(this object o)
    {
        StackTrace st = new StackTrace();

        var v = st.ToString();
        var l = SH.GetLines(v);
        CA.Trim(l);
        l.RemoveAt(0);

        return SH.JoinNL(v);
    }
}