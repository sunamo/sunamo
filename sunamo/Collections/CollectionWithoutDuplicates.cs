using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CollectionWithoutDuplicates<T> : CollectionWithoutDuplicatesBase<T>
{
    public CollectionWithoutDuplicates() : base()
    {
        
    }

    public CollectionWithoutDuplicates(int count) : base(count)
    {
    }

    public CollectionWithoutDuplicates(IEnumerable<T> l) : base(l)
    {
        
    }

    public override int AddWithIndex(T t2)
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

    public override bool? Contains(T t2)
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

    public override int IndexOf(T path)
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

    protected override bool IsComparingByString()
    {
        return allowNull.HasValue && allowNull.Value;
    }
}
