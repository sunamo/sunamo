using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CollectionWithoutDuplicatesStringComparing<T> : CollectionWithoutDuplicatesBase<T >
{
    public CollectionWithoutDuplicatesStringComparing() : base()
    {

    }

    public CollectionWithoutDuplicatesStringComparing(int count) : base(count)
    {
    }

    public CollectionWithoutDuplicatesStringComparing(IEnumerable<T> l) : base(l)
    {

    }

    public override int AddWithIndex(T t2)
    {
            if (Contains(t2).GetValueOrDefault())
            {
                return sr.IndexOf(t2.ToString());
            }
            else
            {
                Add(t2);
                return c.Count - 1;
            }
    }

    public override bool? Contains(T t2)
    {
            ts = t2.ToString();
            return sr.Contains(ts);
        
    }

    public override int IndexOf(T path)
    {
            return sr.IndexOf(path.ToString());
    }

    protected override bool IsComparingByString()
    {
        return true;
    }
}
