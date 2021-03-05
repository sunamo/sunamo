using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CAThread
{
    internal static List<object> ToList(IEnumerable e, System.Windows.Threading.Dispatcher d)
    {
        List<object> ls = new List<object>(e.Count());
        if (d != null)
        {
            d.Invoke(() =>
            {
                foreach (var item in e)
                {
                    ls.Add(item);
                }
            }, System.Windows.Threading.DispatcherPriority.ContextIdle);
        }
        else
        {
            foreach (var item in e)
            {
                ls.Add(item);
            }
        }
        
        return ls;
    }
}
