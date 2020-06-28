using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


public class CollectionWithoutDuplicates<T>
{
    
    public List<T> c = null;
    public bool allowNull = false;
    public static bool br = false;
    

    public CollectionWithoutDuplicates()
    {
        if (br)
        {
            System.Diagnostics.Debugger.Break();
        }
        c = new List<T>();
    }

    public CollectionWithoutDuplicates(int count)
    {
        c = new List<T>(count);
    }

    public CollectionWithoutDuplicates(IEnumerable<T> l)
    {
        c = new List<T>( l.ToList());
    }

    public bool Add(T t2)
    {
        var con = Contains(t2);
        if (con.HasValue)
        {
            if (con.Value)
            {
                c.Add(t2);
                return true;
            }
        }
        else
        {
            if (allowNull)
            {
                c.Add(t2);
                return true;
            }
        }
        return false;
    }


    public bool? Contains(T t2)
    {
        if (!c.Contains(t2))
        {
            if (EqualityComparer<T>.Default.Equals(t2, default(T)))
            {
                return null;
            }
            else
            {
                
            }
            return true;
        }
        return false;
    }

    public int AddWithIndex(T t2)
    {
        int vr = c.IndexOf(t2);
        if (vr == -1)
        {
            Add(t2);
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

    /// <summary>
    /// If I want without chechink, use c.AddRange
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="withoutChecking"></param>
    public void AddRange(IEnumerable<T> list)
    {
        foreach (var item in list)
        {
            Add(item);
        }
    }
}