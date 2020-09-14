using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SunamoComparerICompare
{


    public class IEnumerableCharCountAsc<T> : IComparer<T> where T : IEnumerable<char>
    {
        public int Compare(T x, T y)
        {
            int a = 0;
            int b = 0;

            foreach (var item in x)
            {
                a++;
            }


            foreach (var item in y)
            {
                b++;
            }


            return a.CompareTo(b);
        }
    }

    public class ItemWithDtComparer
    {
        public class Desc<T> : IComparer<ItemWithDt<T>>
        {
            private ISunamoComparer<ItemWithDt<T>> _sc = null;

            public Desc(ISunamoComparer<ItemWithDt<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(ItemWithDt<T> x, ItemWithDt<T> y)
            {
                return _sc.Desc(x, y);
            }
        }

        public class Asc<T> : IComparer<IItemWithDt<T>>
        {
            private ISunamoComparer<IItemWithDt<T>> _sc = null;

            public Asc(ISunamoComparer<IItemWithDt<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(IItemWithDt<T> x, IItemWithDt<T> y)
            {
                return _sc.Asc(x, y);
            }
        }
    }

    /// <summary>
    /// Usage:vr.Sort(new SunamoComparerICompare.ItemWithCountComparer.Desc<string>(new SunamoComparer.ItemWithCountSunamoComparer<string>()));
    /// </summary>
    public class ItemWithCountComparer
    {
        public class Desc<T> : IComparer<ItemWithCount<T>>
        {
            private ISunamoComparer<ItemWithCount<T>> _sc = null;

            public Desc(ISunamoComparer<ItemWithCount<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(ItemWithCount<T> x, ItemWithCount<T> y)
            {
                return _sc.Desc(x, y);
            }
        }

        public class Asc<T> : IComparer<ItemWithCount<T>>
        {
            private ISunamoComparer<ItemWithCount<T>> _sc = null;

            public Asc(ISunamoComparer<ItemWithCount<T>> sc)
            {
                _sc = sc;
            }

            public int Compare(ItemWithCount<T> x, ItemWithCount<T> y)
            {
                return _sc.Asc(x, y);
            }
        }
    }
}