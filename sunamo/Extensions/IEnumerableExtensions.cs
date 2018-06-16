using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Extension class can't be in namespace
public static class IEnumerableExtensions
    {
        public static int Count(this IEnumerable e)
        {
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

    public static bool Contains(this IEnumerable e, object o)
    {
        char ch;
        foreach (var item in e)
        {
            if ( item.Equals( o))
            {
                return true;
            }
        }
        return false;
    }

    public static void ForEach(this IEnumerable e, Action<string> action)
    {
        foreach (var item in e)
        {
            action.Invoke(item.ToString());
        }
        //List<string> s;
        //s.ForEach()
    }
}

