using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CompareFilesPaths
{
    public static string GetFile(CompareExt c, int i)
    {
        return @"e:\Documents\vs\Projects\_tests\CompareTwoFiles\CompareTwoFiles\" + c + @"\" + i + "." + c;
    }
}

public enum CompareExt
{
    cpp,
    aspx,
    cs,
    docs,
    html,
    js,
    json,
    txt
}