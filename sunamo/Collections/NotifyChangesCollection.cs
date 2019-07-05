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
    public event VoidObject CollectionChanged;
    object sender;

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
         l.Add(item);
         OnCollectionChanged();
    }

    public void Clear()
    {
        l.Clear();
        OnCollectionChanged();
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
        OnCollectionChanged();
    }

    public bool Remove(T item)
    {
        bool vr = l.Remove(item);
        OnCollectionChanged();
        return vr;
    }

    public void RemoveAt(int index)
    {
         l.RemoveAt(index);
        OnCollectionChanged();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return l.GetEnumerator();
    }

    public void OnCollectionChanged()
    {
        // Cant be null if I dont want save changes to drive
        if (CollectionChanged != null)
        {
            CollectionChanged(sender);
        }
        
    }
}

