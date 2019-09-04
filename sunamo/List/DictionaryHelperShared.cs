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
    public static void AddOrCreate<Key, Value>(Dictionary<Key, List<Value>> sl, Key key, Value value, bool withoutDuplicitiesInValue = false)
    {
        AddOrCreate<Key, Value, object>(sl, key, value, withoutDuplicitiesInValue);
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
    public static void AddOrCreate<Key, Value, ColType>(Dictionary<Key, List<Value>> sl, Key key, Value value, bool withoutDuplicitiesInValue = false)
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


}