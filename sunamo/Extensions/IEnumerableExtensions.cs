using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class IEnumerableExtensions
    {
        public static int Length(this IEnumerable e)
        {
            return Count(e);
        }

        public static int Count(this IEnumerable e)
        {
            if (e is IList)
            {
                return (e as IList).Count;
            }
            if (e is Array)
            {
                return (e as Array).Length;
            }
            int count = 0;
            foreach (var item in e)
            {
                count++;
            }
            return count;
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
