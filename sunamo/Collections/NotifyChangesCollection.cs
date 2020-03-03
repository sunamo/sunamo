using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

/// <summary>
/// This is only one implement IList
/// </summary>
/// <typeparam name="T"></typeparam>
public class NotifyChangesCollection<T> : IList<T>
{
    /// <summary>
    /// Its collection due to use also ObservableCollection and so
    /// </summary>
    public Collection<T> l = null;
    public event Action<object, string, object> CollectionChanged;
    /// <summary>
    /// sender is chbl but in Tag are last clicked chb
    /// </summary>
    private object _sender;

    public bool onAdd = false;
    public bool onRemove = false;
    public bool onClear = false;

    public void EventOn(bool onAdd, bool onRemove, bool onClear)
    {
        this.onAdd = onAdd;
        this.onRemove = onRemove;
        this.onClear = onClear;
    }

    public NotifyChangesCollection(object sender, Collection<T> c)
    {
        _sender = sender;
        l = c;
    }

    public T this[int index] { get => l[index]; set => l[index] = value; }

    public int Count => l.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        l.Add(item);
        if (onAdd)
        {
            OnCollectionChanged(ListOperation.Add, item);
        }
    }

    public void Clear()
    {
        l.Clear();
        if (onClear)
        {
            OnCollectionChanged(ListOperation.Clear, null);
        }
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
            // sender is chbl but in Tag are last clicked chb
            CollectionChanged(_sender, op, data);
        }
    }
}