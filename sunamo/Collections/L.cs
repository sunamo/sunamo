using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Can be derived because new keyword
/// </summary>
/// <typeparam name="T"></typeparam>
public class L<T> : List<T>
{
    public int Length => Count;
    public T defIfNotFoundIndex = default(T);
    public L()
    {
    }

    public L(IEnumerable<T> collection) : base(collection)
    {
    }

    public L(int capacity) : base(capacity)
    {
    }

    public L<T> ToList()
    {
        return this;
    }

    /// <summary>
    /// Before use is needed set up defIfNotFoundIndex
    /// </summary>
    /// <param name="i"></param>
    public new T this[int i]
    {
        set
        {
            base[i] = value;
        }
        get
        {
            if (CA.HasIndex(i, this))
            {
                return base[i];
            }
            //return CSharpHelperSunamo.DefaultValueForType()
            return defIfNotFoundIndex;
        }
    }
}