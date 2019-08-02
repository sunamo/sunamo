using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Is here dont mix RL and RLData with intellisense
/// </summary>
public static class RLData
{
    // In case of serious problem I can use TranslateDictionary
    public static TranslateDictionary en = new TranslateDictionary(Langs.en);
    public static TranslateDictionary cs = new TranslateDictionary(Langs.cs);

    public static string EnPostColon(string key)
    {
        return en[key] + AllStrings.colon;
    }
}

public class TranslateDictionary : IDictionary<string, string>
{
    static Type type = typeof(TranslateDictionary);

    public static string basePathSolution = null;

    Dictionary<string, string> d = new Dictionary<string, string>();
    Langs l = Langs.en;

    public TranslateDictionary(Langs l)
    {
        this.l = l;
    }

    public string this[string key]
    {
        get  
            {

            if (!d.ContainsKey(key))
            {
                //XlfResourcesH.initialized = false;
                //XlfResourcesH.SaveResouresToRL(basePathSolution);
                ThrowExceptions.Custom(type, RH.CallingMethod(), key + " is not in " + l + " dictionary");
                //return string.Empty;
            }
            return d[key];
        } set => d[key] = value; }

    public ICollection<string> Keys => d.Keys;

    public ICollection<string> Values => d.Values;

    public int Count => d.Count;

    public bool IsReadOnly => false;

    public void Add(string key, string value)
    {
        d.Add(key, value);
    }

    public void Add(KeyValuePair<string, string> item)
    {
        d.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        d.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return d.ContainsKey(item.Key);
    }

    public bool ContainsKey(string key)
    {
        return d.ContainsKey(key);
    }

    /// <summary>
    /// Copy elements to A1 from A2
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        array = new KeyValuePair<string, string>[Count - arrayIndex + 1];

        int i = 0;
        bool add = false;
        foreach (var item in d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
            {
                array[i] = new KeyValuePair<string, string>(item.Key, item.Value);
            }

            i++;
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return d.GetEnumerator();
    }

    public bool Remove(string key)
    {
        return d.Remove(key);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return d.Remove(item.Key);
    }

    public bool TryGetValue(string key, out string value)
    {
        return d.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return d.GetEnumerator();
    }
}