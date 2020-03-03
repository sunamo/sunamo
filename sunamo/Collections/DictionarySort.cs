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