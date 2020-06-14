using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConvertSnakeConvention
{
    public static string ToConvention( string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }

    public static string FromConvention(string p)
    {
        var pa = SH.Split(p, AllChars.lowbar);
        CA.ToLower(pa);
        CA.FirstCharUpper(pa);
        return SH.Join(AllStrings.space, pa);
    }
    }