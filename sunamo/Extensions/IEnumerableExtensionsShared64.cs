using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class IEnumerableExtensions
{
    public static int Count(this IEnumerable e)
    {
        return CA.Count(e);


    }

    public static int Length(this IEnumerable e)
    {
        return Count(e);
    }

    public static string DumpAsString(this IEnumerable ie, string operation, DumpAsStringHeaderArgs a)
    {
        return RH.DumpListAsStringOneLine(operation, ie, a);
    }

    #region Must be two coz in some projects is not Dispatcher
    public static object FirstOrNull(this IEnumerable e)
    {
        if (e.Count() > 0)
        {
            // Here cant call CA.ToList because in FirstOrNull is called in CA.ToList => StackOverflowException
            var c = CAThread.ToList(e);
            return c.FirstOrDefault();
        }

        return null;
    }

    #region Cant be first because then have priority than LINQ method
    //public static object First(this IEnumerable e)
    //{
    //    return FirstOrNull(e);
    //} 
    #endregion
    #endregion

}
