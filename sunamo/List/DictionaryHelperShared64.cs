﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class DictionaryHelper
{
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

    #region For easy copy
    /// <summary>
    /// Copy elements to A1 from A2
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public static void CopyTo<T, U>(Dictionary<T, U> _d, KeyValuePair<T, U>[] array, int arrayIndex)
    {
        array = new KeyValuePair<T, U>[_d.Count - arrayIndex + 1];

        int i = 0;
        bool add = false;
        foreach (var item in _d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
            {
                array[i] = new KeyValuePair<T, U>(item.Key, item.Value);
            }

            i++;
        }
    }

    public static void CopyTo<T, U>(List<KeyValuePair<T, U>> _d, KeyValuePair<T, U>[] array, int arrayIndex)
    {
        array = new KeyValuePair<T, U>[_d.Count - arrayIndex + 1];

        int i = 0;
        bool add = false;
        foreach (var item in _d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
            {
                array[i] = new KeyValuePair<T, U>(item.Key, item.Value);
            }

            i++;
        }
    }

    #endregion

    /// <summary>
    /// A3 is inner type of collection entries
    /// </summary>
    /// <typeparam name = "Key"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <typeparam name = "ColType"></typeparam>
    /// <param name = "sl"></param>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    public static void AddOrCreate<Key, Value, ColType>(IDictionary<Key, List<Value>> dict, Key key, Value value, bool withoutDuplicitiesInValue = false)
    {

        if (key is IEnumerable && typeof(ColType) != typeof(object))
        {
            IEnumerable<ColType> keyE = key as IEnumerable<ColType>;
            bool contains = false;
            foreach (var item in dict)
            {
                IEnumerable<ColType> keyD = item.Key as IEnumerable<ColType>;
                if (Enumerable.SequenceEqual<ColType>(keyD, keyE))
                {
                    contains = true;
                }
            }

            if (contains)
            {
                foreach (var item in dict)
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
                dict.Add(key, ad);
            }
        }
        else
        {
            bool add = true;
            lock (dict)
            {
                if (dict.ContainsKey(key))
                {
                    if (withoutDuplicitiesInValue)
                    {
                        if (dict[key].Contains(value))
                        {
                            add = false;
                        }
                    }

                    if (add)
                    {
                        var val = dict[key];

                        if (val != null)
                        {
                            val.Add(value);
                        }
                    }

                }
                else
                {

                    List<Value> ad = new List<Value>();
                    ad.Add(value);

                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, ad);
                    }
                    else
                    {
                        dict[key].Add(value);
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

    public static List<T2> AddOrCreate<T1, T2>(Dictionary<T1, List<T2>> b64Images, T1 idApp, Func<T1, List<T2>> base64ImagesOfApp)
    {
        if (!b64Images.ContainsKey(idApp))
        {
            var r = base64ImagesOfApp(idApp);
            b64Images.Add(idApp, r);
            return r;
        }
        return b64Images[idApp];
    }
}
