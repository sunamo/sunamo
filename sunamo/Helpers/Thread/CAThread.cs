using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CAThread
{
    public static List<string> ToListString(IEnumerable e)
    {
        List<string> ls = new List<string>(e.Count());
        foreach (var item in e)
        {
            ls.Add(item.ToString());
        }
        return ls;
    }

    internal static List<object> ToList(IEnumerable e)
    {
        List<object> ls = new List<object>(e.Count());
        foreach (var item in e)
        {
            ls.Add(item);
        }
        return ls;
    }
}
