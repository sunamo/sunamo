using System;
using System.Collections.Generic;
using System.Linq;
public class DictionaryHelper
{
    public static void IncrementOrCreate<T>(Dictionary<T, int> sl, T baseNazevTabulky)
    {
        if (sl.ContainsKey(baseNazevTabulky))
        {
            sl[baseNazevTabulky]++;
        }
        else
        {
            sl.Add(baseNazevTabulky, 1);
        }
    }

    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> slovnik)
    {
        Dictionary<T, U> tu = new Dictionary<T, U>();
        foreach (KeyValuePair<T, U> item in slovnik)
        {
            tu.Add(item.Key, item.Value);
        }
        return tu;
    }

    /// <summary>
    /// Pokud A1 bude obsahovat skupinu pod názvem A2, vložím do této skupiny prvek A3
    /// Jinak do A1 vytvořím novou skupinu s klíčem A2 s hodnotou A3
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="sl"></param>
    /// <param name="key"></param>
    /// <param name="p"></param>
    public static void AddOrCreate<Key, Value>(Dictionary<Key, List<Value>> sl, Key key, Value value)
    {
        if (sl.ContainsKey(key))
        {
            sl[key].Add(value);
        }
        else
        {
            List<Value> ad = new List<Value>();
            ad.Add(value);
            sl.Add(key, ad);
        }
    }

    /// <summary>
    /// In addition to method AddOrCreate, more is checking whether value in collection does not exists
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="sl"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> sl, Key key, Value value)
    {
        if (sl.ContainsKey(key))
        {
            if (!sl[key].Contains (value))
            {
                sl[key].Add(value);
            }
        }
        else
        {
            List<Value> ad = new List<Value>();
            ad.Add(value);
            sl.Add(key, ad);
        }
    }

    public static void AddOrPlus<T>(Dictionary<T, long> sl, T key, long p)
    {
        if (sl.ContainsKey(key))
        {
            sl[key] += p;
        }
        else
        {
            sl.Add(key, p);
        }
    }


    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> d)
    {
        List<string> vr = new List<string>(d.Count());
        foreach (var item in d)
        {
            vr.Add(item.Value.ToString());
        }
        return vr;
    }

    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<System.DateTime, int>> d)
    {
        List<string> vr = new List<string>(d.Count());
        foreach (var item in d)
        {
            vr.Add(item.Value.ToString());
        }
        return vr;
    }

    

    public static int AddToIndexAndReturnIncrementedInt<T>(int i, Dictionary<int, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }

    public static short AddToIndexAndReturnIncrementedShort<T>(short i, Dictionary<short, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }

    
}
