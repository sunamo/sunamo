using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MultilineAsOneLine
{
    public const string r = "\r";
    public const string n = "\n";
    public const string rr = "\\r";

    public static string ConvertTo(string i)
    {
        return i.Replace(n, string.Empty).Replace(r, rr);
    }

    public static string ConvertFrom(string f)
    {
        return f.Replace(rr, r);
    }
}