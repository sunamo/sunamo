using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class IEnumerableExtensions{ 
public static int Count(this IEnumerable e)
    {
        if (e == null)
        {
            return 0;
        }

        if (e is IList)
        {
            return (e as IList).Count;
        }

        if (e is Array)
        {
            return (e as Array).Length;
        }

        int count = 0;

        foreach (var item in e)
        {
            count++;
        }

        return count;
    }
}