using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


public class NotifyChangesCollection<T> : IList<T>
{
    /// <summary>
    /// Its collection due to use also ObservableCollection and so
    /// </summary>
    public Collection<T> l = null;
    public event Action<object, string, object> CollectionChanged;
    object sender;

    public bool onAdd = true;
    public bool onRemove = true;
    public bool onClear = true;

    public void EventOn(bool onAdd, bool onRemove, bool onClear)
    {
        this.onAdd = onAdd;
        this.onRemove = onRemove;
        this.onClear = onClear;
    }

    public NotifyChangesCollection(object sender, Collection<T> c)
    {
        this.sender = sender;
        l = c;
    }

    public T this[int index] { get => l[index]; set => l[index] = value; }

    public int Count => l.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {

        if (onAdd)
        {
            l.Add(item); 
        }
         OnCollectionChanged(ListOperation.Add, item);
    }

    public void Clear()
    {
        if (onClear)
        {
            l.Clear(); 
        }
        OnCollectionChanged(ListOperation.Clear, null);
    }

    public bool Contains(T item)
    {
        return l.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
         l.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return l.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return l.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        l.Insert(index, item);
        if (onAdd)
        {
            OnCollectionChanged(ListOperation.Insert, item); 
        }
    }

    public bool Remove(T item)
    {
        bool vr = l.Remove(item);
        if (onRemove)
        {
            OnCollectionChanged(ListOperation.Remove, item); 
        }
        return vr;
    }

    public void RemoveAt(int index)
    {
         l.RemoveAt(index);
        OnCollectionChanged(ListOperation.RemoveAt, index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return l.GetEnumerator();
    }

    private void OnCollectionChanged(ListOperation op, object data)
    {
        OnCollectionChanged(op.ToString(), data);
    }

    public void OnCollectionChanged(string op, object data)
    {
        // Cant be null if I dont want save changes to drive
        if (CollectionChanged != null)
        {
            CollectionChanged(sender, op, data);
        }
        
    }
}

