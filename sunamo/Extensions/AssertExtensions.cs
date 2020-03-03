using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AssertExtensions
{
    public static void EqualTuple<T,U>(List< Tuple<T,U>> a, List<Tuple<T,U>> b)
    {
        if (a.Count != b.Count)
        {
            throw new Exception("Count in a and b is not equal");
        }

        for (int i = 0; i < a.Count; i++)
        {
            if(!EqualityComparer<T>.Default.Equals( a[i].Item1 ,b[i].Item1) || !EqualityComparer<U>.Default.Equals( a[i].Item2, b[i].Item2))
            {
                throw new Exception("a and b is not equal");
            }
        }

        
    }
}