using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class RefreshingList<T> : IList<T>
    {
    List<T> l = null;
    List<T> sourceToRefresh = null;


    public RefreshingList(List<T> sourceToRefresh, int count)
    {
        this.sourceToRefresh = sourceToRefresh;
        l = new List<T>(count);
    }

    public RefreshingList(List<T> sourceToRefresh, IEnumerable<T> asA1)
    {
        this.sourceToRefresh = sourceToRefresh;
        l = new List<T>(asA1);
    }

    #region Is not in any interface

    public void Sort()
    {
        l.Sort();
    }
    #endregion

    public T this[int index] { get => l[index]; set => l[index] = value; }

    public int Count => l.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        l.Add(item);
    }

    public void Clear()
    {
        l.Clear();
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
    }

    public bool Remove(T item)
    {
        bool vr = l.Remove(item);
        RefreshIfEmpty();
        return vr;
    }

    public void RemoveAt(int index)
    {
        l.RemoveAt(index);
        RefreshIfEmpty();
    }

    private void RefreshIfEmpty()
    {
        if (l.Count == 0)
        {
            l = sourceToRefresh.ToList();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return l.GetEnumerator();
    }
}