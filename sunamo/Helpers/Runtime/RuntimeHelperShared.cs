using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Windows;


public partial class RuntimeHelper{ 
public static bool IsAdminUser()
    {
        return FS.ExistsDirectory(@"d:\vs\sunamo\");
    }

    /// <summary>
    /// Remove GetStackTrace (first line
    /// </summary>
    /// <returns></returns>
    public static string GetStackTrace()
    {
        StackTrace st = new StackTrace();

        var v = st.ToString();
        var l = SH.GetLines(v);
        CA.Trim(l);
        l.RemoveAt(0);

        return SH.JoinNL(v);
    }
}