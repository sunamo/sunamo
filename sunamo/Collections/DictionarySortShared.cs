using System.Collections.Generic;
using System.Diagnostics;

public partial class DictionarySort<T, U> 
{ 
public T KeyFromValue(Dictionary<T, U> sl, U item2)
    {
        foreach (KeyValuePair<T, U> item in sl)
        {
            if (item.Value.Equals(item2))
            {
                return item.Key;
            }
        }

        return default(T);
    }

   
    /// <summary>
    /// A1 je index od kterého prohledávat
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "sl"></param>
    /// <param name = "item"></param>
    public T KeyFromValue(int p, Dictionary<T, U> sl, object item2)
    {
        int i = -1;
        List<KeyValuePair<T, U>> l = new List<KeyValuePair<T, U>>();
        foreach (KeyValuePair<T, U> item in sl)
        {
            i++;
            if (i < p)
            {
                l.Add(item);
                continue;
            }

            if (item.Value.Equals(item2))
            {
                return item.Key;
            }
        }

        // Lépe jsem to tu nedokázal vymyslet :-(
        foreach (KeyValuePair<T, U> item in l)
        {
            if (item.Value.Equals(item2))
            {
                return item.Key;
            }
        }

        return default(T);
    }
}