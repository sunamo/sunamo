using System;
using System.Collections.Generic;
using System.Linq;
public class DictionaryHelper
{
    static Type type = typeof(DictionaryHelper);

    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values)
    {
        ThrowExceptions.DifferentCountInLists(type, "GetDictionary", "keys", keys.Count, "values", values.Count);
        Dictionary<Key, Value> result = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            result.Add(keys[i], values[i]);
        }
        return result;
    }

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

    public static Dictionary<T, List<U>> GroupByValues<U, T>(Dictionary<U, T> dictionary)
    {
        Dictionary<T, List<U>> result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary)
        {
            DictionaryHelper.AddOrCreate<T, U>(result, item.Value, item.Key);
        }
        return result;
    }

    /// <summary>
    /// Return p1 if exists key A2 with value no equal to A3
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    private T FindIndexOfValue<T, U>(Dictionary<T, U> g, U p1, T p2)
    {
        foreach (KeyValuePair<T, U> var in g)
        {
            if (Comparer<U>.Default.Compare( var.Value, p1) == ComparerHelper.Higher &&  Comparer<T>.Default.Compare( var.Key, p2) == ComparerHelper.Lower)
            {
                return var.Key;
            }
        }
        return default(T);
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

    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> sl, Key key, DateTime value)
    {
        TimeSpan ts = TimeSpan.FromTicks(value.Ticks);
        AddOrCreateTimeSpan<Key>(sl, key, ts);
    }

    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> sl, Key key, TimeSpan value)
    {
        if (sl.ContainsKey(key))
        {
            sl[key] = sl[key].Add(value);
        }
        else
        {
            sl.Add(key, value);
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

    public static void AddOrPlus<T>(Dictionary<T, int> sl, T key, int p)
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

    public static Value GetFirstItem<Value>(Dictionary<string, Value> dict)
    {
        foreach (var item in dict)
        {
            return item.Value;
        }
        return default(Value);
    }
}
