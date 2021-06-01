using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CAThread
{
    #region ToList to avoid StackOverflowException
    public static List<object> ToList(IEnumerable e)
    {
        List<object> ls = new List<object>(e.Count());


        foreach (var item in e)
        {
            ls.Add(item);
        }

        return ls;
    }
    #endregion
}
