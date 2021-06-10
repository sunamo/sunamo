using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;


public class CollectionWithoutDuplicates<T> : IDumpAsString
{
    /// <summary>
    /// 
    /// </summary>
    public List<T> c = null;
    public List<string> sr = null;
    bool? _allowNull = false;
    /// <summary>
    /// true = compareWithString
    /// false = !compareWithString
    /// null = allow null (can't compareWithString)
    /// </summary>
    public bool? allowNull
    {
        get => _allowNull;
        set
        {
            _allowNull = value;
            if (value.HasValue && value.Value)
            {
                sr = new List<string>(count);
            }
        }
    }

    public static bool br = false;

    bool _compareWithString = false;

    
    int count = 10000;

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
        this.count = count;
        c = new List<T>(count);
    }

    public CollectionWithoutDuplicates(IEnumerable<T> l)
    {
        c = new List<T>( l.ToList());
    }

    public bool Add(T t2)
    {
        bool result = false;

        var con = Contains(t2);
        if (con.HasValue)
        {
            if (!con.Value)
            {
                c.Add(t2);
                result = true;
            }
        }
        else
        {
            if (!allowNull.HasValue)
            {
                c.Add(t2);
                result = true;
            }
        }

        if (result)
        {
            if (IsComparingByString())
            {
                sr.Add(ts);
            }
        }

        return result;
    }

    bool IsComparingByString()
    {
        return allowNull.HasValue && allowNull.Value;
    }

    string ts = null;

    public bool? Contains(T t2 )
    {
        if (IsComparingByString())
        {
            ts = t2.ToString();
            return sr.Contains(ts);
        }
        else
        {
            if (!c.Contains(t2))
            {
                if (EqualityComparer<T>.Default.Equals(t2, default(T)))
                {
                    return null;
                }

                return false;
            }
        }
        return true;
    }

    public int AddWithIndex(T t2)
    {
        if (IsComparingByString())
        {
            if (Contains(t2).GetValueOrDefault())
            {
                // Will checkout below
            }
            else
            {
                Add(t2);
                return c.Count - 1;
            }
        }
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
        if (IsComparingByString())
        {
            return sr.IndexOf(path.ToString());
        }
        
        int vr = c.IndexOf(path);
        if (vr == -1)
        {
            c.Add(path);
            return c.Count - 1;
        }
        return vr;
    }

    List<T> wasNotAdded = new List<T>();

    /// <summary>
    /// If I want without checkink, use c.AddRange
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="withoutChecking"></param>
    public List<T> AddRange(IEnumerable<T> list)
    {
        wasNotAdded.Clear();
        foreach (var item in list)
        {
            if(!Add(item))
            {
                wasNotAdded.Add(item);
            }
        }
        return wasNotAdded;
    }

    public string DumpAsString(string operation, DumpAsStringHeaderArgs a)
    {
        return c.DumpAsString(operation, a);
    }
}