using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class DictionaryHelper
{
    public static List<KeyValuePair<T, int>> CountOfItems<T>(List<T> streets)
    {
        Dictionary<T, int> pairs = new Dictionary<T, int>();
        foreach (var item in streets)
        {
            DictionaryHelper.AddOrPlus(pairs, item, 1);
        }

        var v = pairs.OrderByDescending(d => d.Value);
        var r = v.ToList();
        return r;
    }

    public static NTree<string> CreateTree(Dictionary<string, List<string>> d)
    {
        NTree<string> t = new NTree<string>(string.Empty);

        foreach (var item in d)
        {
            var child = t.AddChild(item.Key);

            foreach (var v in item.Value)
            {
                child.AddChild(v);
            }

            child.children = new LinkedList<NTree<string>>( child.children.Reverse());
        }

         

        return t;
    }

    public static void RemoveIfExists<T, U>(Dictionary<T, List<U>> st, T v)
    {
        if (st.ContainsKey(v))
        {
            st.Remove(v);
        }
    }

   

    public static Dictionary<T, List<U>> GroupByValues<U, T, ColType>(Dictionary<U, T> dictionary)
    {
        Dictionary<T, List<U>> result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary)
        {
            DictionaryHelper.AddOrCreate<T, U, ColType>(result, item.Value, item.Key);
        }

        return result;
    }

    public static List<T2> AggregateValues<T1, T2>(Dictionary<T2, List<T2>> lowCostNotFoundEurope)
    {
        List<T2> result = new List<T2>();
        foreach (var lcCountry in lowCostNotFoundEurope)
        {
            result.AddRange(lcCountry.Value);
        }

        return result;
    }

    /// <summary>
    /// Return p1 if exists key A2 with value no equal to A3
    /// </summary>
    /// <param name = "g"></param>
    private T FindIndexOfValue<T, U>(Dictionary<T, U> g, U p1, T p2)
    {
        foreach (KeyValuePair<T, U> var in g)
        {
            if (Comparer<U>.Default.Compare(var.Value, p1) == ComparerConsts.Higher && Comparer<T>.Default.Compare(var.Key, p2) == ComparerConsts.Lower)
            {
                return var.Key;
            }
        }

        return default(T);
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
    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> slovnik)
    {
        Dictionary<T, U> tu = new Dictionary<T, U>();
        foreach (KeyValuePair<T, U> item in slovnik)
        {
            tu.Add(item.Key, item.Value);
        }

        return tu;
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

    



    public static int AddToIndexAndReturnIncrementedInt<T>(int i, Dictionary<int, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }

    /// <summary>
    /// A2 can be null
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="airPlaneCompanies"></param>
    /// <param name="twoTimes"></param>
    /// <returns></returns>
    public static Dictionary<T1, T2> RemoveDuplicatedFromDictionaryByValues<T1, T2>(Dictionary<T1, T2> airPlaneCompanies,  Dictionary<T1, T2> twoTimes)
    {
        //twoTimes = new Dictionary<T1, T2>();
        CollectionWithoutDuplicates<T2> processed = new CollectionWithoutDuplicates<T2>();
        foreach (var item in airPlaneCompanies.Keys.ToList())
        {
            T2 value = airPlaneCompanies[item];
            if (!processed.Add(value))
            {
                if (twoTimes != null)
                {
                    twoTimes.Add(item, value);
                }
                
                airPlaneCompanies.Remove(item);
            }
        }

        return airPlaneCompanies;
    }

    public static int CountAllValues<Key, Value>(Dictionary<Key, List<Value>> fe)
    {
        int nt = 0;
        foreach (var item in fe)
        {
            nt += item.Value.Count();
        }

        return nt;
    }
}