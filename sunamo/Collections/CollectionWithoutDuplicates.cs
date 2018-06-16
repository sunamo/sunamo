using System;
using System.Collections.Generic;
using System.Windows;

public class CollectionWithoutDuplicates<T>
{
    public List<T> c = new List<T>();

    public void Add(T t2)
    {
        if (!c.Contains(t2))
        {
            c.Add(t2);
        }
        
    }

    public int AddWithIndex(T t2)
    {
        int vr = c.IndexOf(t2);
        if (vr == -1)
        {
            c.Add(t2);
            return c.Count - 1;
        }
        return vr;
    }

    public int IndexOf(T path)
    {
        int vr = c.IndexOf(path);
        if (vr == -1)
        {
            c.Add(path);
            return c.Count - 1;
        }
        return vr;
    }

    
}
