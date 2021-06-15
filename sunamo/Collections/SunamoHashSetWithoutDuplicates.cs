using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SunamoHashSetWithoutDuplicates<T>
{
    public HashSet<T> c = null;

    public SunamoHashSetWithoutDuplicates()
    {
        c = new HashSet<T>();
    }

    public SunamoHashSetWithoutDuplicates(int duplCount)
    {
        c = new HashSet<T>(duplCount);
    }

    public List<T> AddRange(IEnumerable<T> e, ProgressState clpb)
    {
        List<T> d = new List<T>();
        foreach (var item in e)
        {
            if (clpb.isRegistered)
            {
                clpb.OnAnotherSong();
            }
            
            if (!c.Contains(item))
            {
                c.Add(item);
            }
            else
            {
                d.Add(item);
            }
        }
        return d;
    }
}
