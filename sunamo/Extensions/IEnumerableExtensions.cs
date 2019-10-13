using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static partial class IEnumerableExtensions
{
    public static int Length(this IEnumerable e)
    {
        return Count(e);
    }

    public static object FirstOrNull(this IEnumerable e)
    {
        if (e.Count() > 0)
        {
            foreach (var item in e)
            {
                return item;
            }
        }

        return null;
    }
}