using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// Exists also SunamoDictionarySort - for SunamoDictionary type
/// </summary>
/// <typeparam name = "T"></typeparam>
/// <typeparam name = "U"></typeparam>
public partial class DictionarySort<T, U>
{
    public List<U> ReturnValues(Dictionary<T, U> sl)
    {
        List<U> vr = new List<U>();
        foreach (KeyValuePair<T, U> item in sl)
        {
            vr.Add(item.Value);
        }

        return vr;
    }

    public List<T> ReturnKeys(Dictionary<T, U> sl)
    {
        List<T> vr = new List<T>();
        foreach (KeyValuePair<T, U> item in sl)
        {
            vr.Add(item.Key);
        }

        return vr;
    }

    /// <summary>
    /// sezareno a->z, lomítko první, pak čísla, pak písmena - vše standardně. Porovnává se tak bez volání Reverse
    /// Sorted a->z, in 
    /// </summary>
    /// <param name = "sl"></param>
    public Dictionary<T, U> SortByKeysDesc(Dictionary<T, U> sl)
    {
        List<T> klice = ReturnKeys(sl);
        //List<U> hodnoty = VratHodnoty(sl);
        klice.Sort();
        Dictionary<T, U> vr = new Dictionary<T, U>();
        foreach (T item in klice)
        {
            vr.Add(item, sl[item]);
        }

        return vr;
    }

    /// <summary>
    /// sezareno a->z, lomítko první, pak čísla, pak písmena - vše standardně. Porovnává se tak bez volání Reverse
    /// </summary>
    /// <param name = "sl"></param>
    public Dictionary<T, U> SortByValuesDesc(Dictionary<T, U> sl)
    {
        List<T> klice = ReturnKeys(sl);
        List<U> hodnoty = ReturnValues(sl);
        hodnoty.Sort();
        Dictionary<T, U> vr = new Dictionary<T, U>();
        foreach (U item in hodnoty)
        {
            T t = KeyFromValue(vr.Count, sl, item);
            vr.Add(t, item);
        }

        return vr;
    }

    public T KeyFromValue(List<T> pridane, int p, Dictionary<T, U> sl, object item2)
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

            if (!pridane.Contains(item.Key))
            {
                if (item.Value.Equals(item2))
                {
                    return item.Key;
                }
            }
        //////////ObjectHelper.ci.VratTR(item.Key) + AllStrings.swda + ObjectHelper.ci.VratTR(item.Value));
        }

        foreach (KeyValuePair<T, U> item in l)
        {
            if (!pridane.Contains(item.Key))
            {
                if (item.Value.Equals(item2))
                {
                    return item.Key;
                }
            }
        }

        return default(T);
    }

    /// <summary>
    /// sezareno z->a, pak čísla od největších k nejmenším, lomítka až poté. Volá se reverse
    /// </summary>
    /// <param name = "sl"></param>
    public Dictionary<T, U> SortByKeysAsc(Dictionary<T, U> sl)
    {
        List<T> klice = ReturnKeys(sl);
        //List<U> hodnoty = VratHodnoty(sl);
        klice.Sort();
        klice.Reverse();
        Dictionary<T, U> vr = new Dictionary<T, U>();
        foreach (T item in klice)
        {
            vr.Add(item, sl[item]);
        }

        return vr;
    }

    public Dictionary<T, List<U>> RemoveWhereIsInValueOnly1Object(Dictionary<T, List<U>> sl)
    {
        Dictionary<T, List<U>> vr = new Dictionary<T, List<U>>();
        foreach (KeyValuePair<T, List<U>> item in sl)
        {
            if (item.Value.Count != 1)
            {
                vr.Add(item.Key, item.Value);
            }
        }

        return vr;
    }
}