using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DictionaryWithList<T, U> : IDictionary<T, U>
{
    public Action callWhenIsZeroElements;
    static Type type = typeof(DictionaryWithList<T, U>);
    List<KeyValuePair<T, U>> tu = new List<KeyValuePair<T, U>>();

    public U this[T key] { get 
        {
            if (callWhenIsZeroElements != null)
            {
                if (Count == 0)
                {
                    callWhenIsZeroElements.Invoke();
                }
            }

            foreach (var item in tu)
            {
                if (EqualityComparer<T>.Default.Equals( item.Key, key))
                {
                    return item.Value;
                }
            }

            return default(U);
        }  
        set 
        {

            for (int i = 0; i < tu.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(tu[i].Key, key))
                {
                    tu[i] = new KeyValuePair<T, U>(tu[i].Key, value);
                    return;
                }
            }
            Add(key, value);
        } 
    }

    public ICollection<T> Keys {
        get
        {
            List<T> u = new List<T>(tu.Count);
            foreach (var item in tu)
            {
                u.Add(item.Key);
            }
            return u;
        }
    }

    public ICollection<U> Values
    {
        get
        {
            List<U> u = new List<U>(tu.Count);
            foreach (var item in tu)
            {
                u.Add(item.Value);
            }
            return u;
        }
    }

    public int Count => tu.Count;

    public bool IsReadOnly => false;

    public void Add(T key, U value)
    {
        tu.Add(new KeyValuePair<T, U>(key, value));
    }

    public void Add(KeyValuePair<T, U> item)
    {
        tu.Add(item);
    }

    public void Clear()
    {
        tu.Clear();
    }

    public bool Contains(KeyValuePair<T, U> item)
    {
        return ContainsKey(item.Key);
    }

    public bool ContainsKey(T key)
    {
        
        foreach (var item in tu)
        {
            return true;
        }
        return false;
    }

    public void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
    {
        DictionaryHelper.CopyTo<T, U>(tu, array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
    {
        return tu.GetEnumerator();
    }

    public bool Remove(T key)
    {
        for (int i = 0; i < tu.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(tu[i].Key, key))
            {
                tu.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public bool Remove(KeyValuePair<T, U> item)
    {
        return Remove(item.Key) ;
    }

    public bool TryGetValue(T key, out U value)
    {
        value = default(U);
        for (int i = 0; i < tu.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(tu[i].Key, key))
            {
                value = tu[i].Value;
                return true;
            }
        }
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return tu.GetEnumerator();
    }
}