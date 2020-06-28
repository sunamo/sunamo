using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AssertExtensions
{
    static Type type = typeof(AssertExtensions);
    public static void EqualTuple<T,U>(List< Tuple<T,U>> a, List<Tuple<T,U>> b)
    {
        if (a.Count != b.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.CountInAAndBIsNotEqual));
        }

        for (int i = 0; i < a.Count; i++)
        {
            if(!EqualityComparer<T>.Default.Equals( a[i].Item1 ,b[i].Item1) || !EqualityComparer<U>.Default.Equals( a[i].Item2, b[i].Item2))
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"a and b is not equal");
            }
        }

        
    }
}