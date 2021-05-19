using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class FsWatcherDictionary<T, U> : IDictionary<T, U>
{
    static Type type = typeof(FsWatcherDictionary<T, U>);
    Dictionary<T, U> d = new Dictionary<T, U>();

    public U this[T key] { 
        get 
        { if (d.ContainsKey(key))
                {
                return d[key];
            }
            return default(U);
            } 
            set 
                { d[key] = value; 
            } 
        }

    public ICollection<T> Keys => d.Keys;

    public ICollection<U> Values => d.Values;

    public int Count => d.Count;

    public bool IsReadOnly => false;

    public void Add(T key, U value)
    {
        lock (d)
        {
            if (!d.ContainsKey(key))
            {
                d.Add(key, value);
            }
        }
    }

    public void Add(KeyValuePair<T, U> item)
    {
        Add(item.Key, item.Value);
    }

    public void Clear()
    {
        d.Clear();
    }

    public bool Contains(KeyValuePair<T, U> item)
    {
        return d.Contains(item);
    }

    public bool ContainsKey(T key)
    {
        return d.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
    {
        DictionaryHelper.CopyTo<T, U>(d, array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
    {
        return d.GetEnumerator();
    }

    public bool Remove(T key)
    {
        return d.Remove(key);
    }

    public bool Remove(KeyValuePair<T, U> item)
    {
        return d.Remove(item.Key);
    }

    public bool TryGetValue(T key, out U value)
    {
        bool vr = d.TryGetValue(key, out value);
        return vr;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return d.GetEnumerator();
    }
}