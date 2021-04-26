using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class D<T, U> : ISunamoDictionary<T, U>, IEnumerable
{
    public Action callWhenIsZeroElements;
    static Type type = typeof(D<T, U>);
    public Dictionary<T, U> d = new Dictionary<T, U>();
    public U this[T key] { 
        get {
            if (callWhenIsZeroElements != null)
            {
                if (Count == 0)
                {
                    callWhenIsZeroElements.Invoke();
                }
            }
            return d[key];

        } set => d[key] = value; }

    

    public ICollection<T> Keys => d.Keys;

    public ICollection<U> Values => d.Values;

    public int Count
    {
        get
        {
            return d.Count;
        }
    }

    public bool IsReadOnly => false;

    public void Add(T key, U value)
    {
        d.Add(key, value);
    }

    public void Add(KeyValuePair<T, U> item)
    {
        d.Add( item.Key, item.Value);
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
        return d.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return d.GetEnumerator();
    }
}