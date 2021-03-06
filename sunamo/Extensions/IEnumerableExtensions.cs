using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

public static partial class IEnumerableExtensions
{
    public static int Length(this IEnumerable e)
    {
        return Count(e);
    }

    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
    {
        return source.Skip(Math.Max(0, source.Count() - N));
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

    public static object dFirstOrNull(this IEnumerable e, Dispatcher d)
    {
        if (e.Count() > 0)
        {
            var c = CAThread.ToList(e, d);
            return c.FirstOrDefault();
        }

        return null;
    } 
    #endregion

    public static IEnumerable<TSource> Where2<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        //source.ToList().Where(predicate); - StackOverflowExtension
        //return new List<TSource>(source).Where(predicate) ;
        return source.ToList().Where(predicate);
    }
}