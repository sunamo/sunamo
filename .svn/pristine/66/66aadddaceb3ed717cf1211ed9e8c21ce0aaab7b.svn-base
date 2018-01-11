using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    /// <summary>
    /// Nejjednodušší slovník, který obsahuje vnitřně 2 kolekce. Slouží k co nejsnažšímu získaní klíčů nebo hodnot v jasných typech. 
    /// Možnost odvodit další třídy od této a přidat rozšířenou funkcionalitu.
    /// </summary>
public class ParallerDictionaryBase<T, U>
{
    protected List<T> keys = new List<T>();
    protected List<U> values = new List<U>();

    public List<T> KeysAsList()
    {
        return keys.ToList();
    }

    public List<U> ValuesAsList()
    {
        return values.ToList();
    }
}

/// <summary>
/// Poskytuje např. operace na bázi LINQ
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
public class ParallerDictionary<T, U> : ParallerDictionaryBase<T, U>
{
    public void Add(T t, U u)
    {
        keys.Add(t);
        values.Add(u);
    }

    public void Insert(int dex, T t, U u)
    {
        keys.Insert(dex, t);
        values.Insert(dex, u);
    }

    
}

/// <summary>
/// Poskytuje např. operace na bázi LINQ
/// HOdí se spíše pro menší kolekce, protože stále iretuje aby získal zpět objekt ParallelDictionaryExtended
/// V budoucnu napsat vlastní třídy na bázi DictionarySort
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
public class ParallerDictionaryExtended<T, U> : ParallerDictionaryBase<T, U>
{
    SortedDictionary<T, U> dict = new SortedDictionary<T, U>();

    public void Add(T t, U u)
    {
        keys.Add(t);
        values.Add(u);
        dict.Add(t, u);
    }

    public void Insert(int dex, T t, U u)
    {
        keys.Insert(dex, t);
        values.Insert(dex, u);
    }

    public ParallerDictionaryExtended<T, U> OrderByDescending(Func<KeyValuePair<T, U>, T> keySelector)
    {
        IOrderedEnumerable<KeyValuePair<T, U>> ioe = dict.OrderByDescending(keySelector);
        ParallerDictionaryExtended<T, U> vr = new ParallerDictionaryExtended<T, U>();

        foreach (var item in ioe)
        {
            vr.keys.Add(item.Key);
            vr.values.Add(item.Value);
            vr.dict.Add(item.Key, item.Value);
        }

        return vr;
    }

    public ParallerDictionaryExtended<T, U> OrderBy(Func<KeyValuePair<T, U>, T> keySelector)
    {
        IOrderedEnumerable<KeyValuePair<T, U>> ioe = dict.OrderBy(keySelector);
        ParallerDictionaryExtended<T, U> vr = new ParallerDictionaryExtended<T, U>();

        foreach (var item in ioe)
        {
            vr.keys.Add(item.Key);
            vr.values.Add(item.Value);
            vr.dict.Add(item.Key, item.Value);
        }

        return vr;
    }
}

