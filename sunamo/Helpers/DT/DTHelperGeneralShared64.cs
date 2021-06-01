using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class DTHelperGeneral
{
    public static string ShortYear(int year)
    {
        var s = year.ToString();
        s = s.Substring(2, 2);
        return s;
    }

    public static string LongYear(string y)
    {
        var i = int.Parse(y);
        if (i <= 79)
        {
            return "20" + i;
        }
        else
        {
            return "19" + i;
        }
    }
}
