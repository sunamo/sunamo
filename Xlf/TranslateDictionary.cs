using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunamoExceptions;

public class TranslateDictionary : IDictionary<string, string>
{
    private static Type type = typeof(TranslateDictionary);

    public static string basePathSolution = null;

    private Dictionary<string, string> _d = new Dictionary<string, string>();
    private Langs _l = Langs.en;

    public TranslateDictionary(Langs l)
    {
        _l = l;
    }

    public string this[string key]
    {
        get
        {
            if (!_d.ContainsKey(key))
            {
                var k = XlfResourcesH.SaveResouresToRLSunamo(key);

                if (!_d.ContainsKey(key))
                {
                    //XlfResourcesH.initialized = false;
                    //XlfResourcesH.SaveResouresToRL(basePathSolution);
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), key + " is not in " + _l + " dictionary");
                    //return string.Empty;
                }
            }

            var value = _d[key];

            return value;
        }
        set => _d[key] = value;
    }

    public ICollection<string> Keys => _d.Keys;

    public ICollection<string> Values => _d.Values;

    public int Count => _d.Count;

    public bool IsReadOnly => false;

    public void Add(string key, string value)
    {
        _d.Add(key, value);
    }

    public void Add(KeyValuePair<string, string> item)
    {
        _d.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _d.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _d.ContainsKey(item.Key);
    }

    public bool ContainsKey(string key)
    {
      

        return _d.ContainsKey(key);
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
        foreach (var item in _d)
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
        return _d.GetEnumerator();
    }

    public bool Remove(string key)
    {
        return _d.Remove(key);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return _d.Remove(item.Key);
    }

    public bool TryGetValue(string key, out string value)
    {
        return _d.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _d.GetEnumerator();
    }
}