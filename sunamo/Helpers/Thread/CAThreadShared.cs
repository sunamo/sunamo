using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CAThread
{
    #region ToList to avoid StackOverflowException
    internal static List<object> ToList(IEnumerable e)
    {
        List<object> ls = new List<object>(e.Count());


        foreach (var item in e)
        {
            ls.Add(item);
        }

        return ls;
    }
    #endregion

    #region ToList to use in different threads
    internal static List<object> ToList(IEnumerable e, System.Windows.Threading.Dispatcher d)
    {
        List<object> ls = new List<object>(e.Count());

        d.Invoke(() =>
        {
            foreach (var item in e)
            {
                ls.Add(item);
            }
        }, System.Windows.Threading.DispatcherPriority.ContextIdle);

        return ls;
    } 
    #endregion
}
