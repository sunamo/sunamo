using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class DictionaryHelper
{
    private static Type s_type = typeof(DictionaryHelper);

    public static Value GetFirstItemValue<Key,Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict)
        {
            return item.Value;
        }

        return default(Value);
    }

    public static Key GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict)
        {
            return item.Key;
        }

        return default(Key);
    }

    public static short AddToIndexAndReturnIncrementedShort<T>(short i, Dictionary<short, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }

    

    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values)
    {
        ThrowExceptions.DifferentCountInLists(s_type, "GetDictionary", "keys", keys.Count, "values", values.Count);
        Dictionary<Key, Value> result = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            result.Add(keys[i], values[i]);
        }

        return result;
    }

    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string p, params string[] d1)
    {
        var sp = SH.Split(p, d1);
        return GetDictionaryByKeyValueInString<string>(sp);
    }

    public static Dictionary<U,T> SwitchKeyAndValue<T,U>(Dictionary<T, U> dictionary)
    {
        Dictionary<U, T> d = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary)
        {
            d.Add(item.Value, item.Key);
        }
        return d;
    }

    /// <summary>
    /// If exists index A2, set to it A3
    /// if don't, add with A3
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="qs"></param>
    /// <param name="k"></param>
    /// <param name="v"></param>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> qs, T1 k, T2 v)
    {
        if (qs.ContainsKey(k))
        {
            qs[k] = v;
        }
        else
        {
            qs.Add(k, v);
        }
    }

    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> p)
    {
        var methodName = RH.CallingMethod();
        ThrowExceptions.IsOdd(s_type, methodName, "p", p);

        Dictionary<T, T> result = new Dictionary<T, T>();
        for (int i = 0; i < p.Count; i++)
        {
            result.Add(p[i], p[++i]);
        }
        return result;
    }

/// <summary>
    /// Pokud A1 bude obsahovat skupinu pod názvem A2, vložím do této skupiny prvek A3
    /// Jinak do A1 vytvořím novou skupinu s klíčem A2 s hodnotou A3
    /// </summary>
    /// <typeparam name = "Key"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <param name = "sl"></param>
    /// <param name = "key"></param>
    /// <param name = "p"></param>
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> sl, Key key, Value value, bool withoutDuplicitiesInValue = false)
    {
        AddOrCreate<Key, Value, object>(sl, key, value, withoutDuplicitiesInValue);
    }

    internal static Dictionary<T1, T2> GetDictionaryFromIEnumerable<T1, T2>(IEnumerable<KeyValuePair<T1, T2>> enumerable)
    {
        Dictionary<T1, T2> d = new Dictionary<T1, T2>();
        foreach (var item in enumerable)
        {
            d.Add(item.Key, item.Value);
        }
        return d;
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIEnumerable<T1, T2>(orderedEnumerable);
    }

    /// <summary>
    /// A3 is inner type of collection entries
    /// </summary>
    /// <typeparam name = "Key"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <typeparam name = "ColType"></typeparam>
    /// <param name = "sl"></param>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    public static void AddOrCreate<Key, Value, ColType>(IDictionary<Key, List<Value>> sl, Key key, Value value, bool withoutDuplicitiesInValue = false)
    {
        //T, byte[]
        if (key is IEnumerable && typeof(ColType) != typeof(object))
        {
            IEnumerable<ColType> keyE = key as IEnumerable<ColType>;
            bool contains = false;
            foreach (var item in sl)
            {
                IEnumerable<ColType> keyD = item.Key as IEnumerable<ColType>;
                if (Enumerable.SequenceEqual<ColType>(keyD, keyE))
                {
                    contains = true;
                }
            }

            if (contains)
            {
                foreach (var item in sl)
                {
                    IEnumerable<ColType> keyD = item.Key as IEnumerable<ColType>;
                    if (Enumerable.SequenceEqual<ColType>(keyD, keyE))
                    {
                        if (withoutDuplicitiesInValue)
                        {
                            if (item.Value.Contains(value))
                            {
                                return;

                            }
                        }
                        item.Value.Add(value);
                    }
                }
            }
            else
            {


                List<Value> ad = new List<Value>();
                ad.Add(value);

                

                sl.Add(key, ad);
            }
        }
        else
        {
            bool add = true;
            lock (sl)
            {
                if (sl.ContainsKey(key))
                {
                    if (withoutDuplicitiesInValue)
                    {
                        if (sl[key].Contains(value))
                        {
                            add = false;

                        }
                    }

                    if (add)
                    {
                        var val = sl[key];

                        //if (Comparer<Value>.Default.Compare(val, default(List<Value>)))
                        //{
                        if (val != null)
                        {
                            val.Add(value);
                        }
                            
                        //}
                    }

                }
                else
                {
                    List<Value> ad = new List<Value>();
                    ad.Add(value);
                    // Must be Checking again due to FileSystemWatcher in SourceCodeIndexer
                    if (!sl.ContainsKey(key))
                    {
                        sl.Add(key, ad);
                    }
                    else
                    {
                        sl[key].Add(value);
                    }
                }
            }
        }
    }

/// <summary>
    /// In addition to method AddOrCreate, more is checking whether value in collection does not exists
    /// </summary>
    /// <typeparam name = "Key"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <param name = "sl"></param>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> sl, Key key, Value value)
    {
        if (sl.ContainsKey(key))
        {
            if (!sl[key].Contains(value))
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



public static List<string> GetListStringFromDictionary(Dictionary<string, string> p)
    {
        List<string> vr = new List<string>();

        foreach (var item in p)
        {
            vr.Add(item.Key);
            vr.Add(item.Value);
        }

        return vr;
    }

    public static void AddOrSet(Dictionary<string, string> qs, string k, string v)
    {
        if (qs.ContainsKey(k))
        {
            qs[k] = v;
        }
        else
        {
            qs.Add(k, v);
        }
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

public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> d)
    {
        List<string> vr = new List<string>(d.Count());
        foreach (var item in d)
        {
            vr.Add(item.Value.ToString());
        }

        return vr;
    }
}