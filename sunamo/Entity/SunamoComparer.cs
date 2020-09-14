using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using sunamo.Data;



public partial class SunamoComparer
{
    public class Integer : ISunamoComparer<int>
    {
        public static Integer Instance = new Integer();

        public int Desc(int x, int y)
        {
            return x.CompareTo(y) * -1;
        }

        public int Asc(int x, int y)
        {
            return x.CompareTo(y);
        }
    }

    public class DT : ISunamoComparer<DateTime>
    {
        public static DT Instance = new DT();

        // ToList() here must be - sorted still contains reference to original collection
        public int Desc(DateTime x, DateTime y)
        {
            return x.CompareTo(y) * -1;
        }

        public int Asc(DateTime x, DateTime y)
        {
            return x.CompareTo(y);
        }
    }

    
    public class IEnumerableCharLength : ISunamoComparer<IEnumerable<char>>
    {
        public static IEnumerableCharLength Instance = new IEnumerableCharLength();

        public int Desc(IEnumerable<char> x, IEnumerable<char> y)
        {
            List<char> lx = new List<char>();

            foreach (var item in x)
            {
                lx.Add(item);
            }

            List<char> ly = new List<char>();
            foreach (var item in y)
            {
                ly.Add(item);
            }

            int a = lx.Count;
            int b = ly.Count;
            return a.CompareTo(b) * -1;
        }

        public int Asc(IEnumerable<char> x, IEnumerable<char> y)
        {
            List<char> lx = new List<char>();

            foreach (var item in x)
            {
                lx.Add(item);
            }

            List<char> ly = new List<char>();
            foreach (var item in y)
            {
                ly.Add(item);
            }

            int a = lx.Count;
            int b = ly.Count;
            return a.CompareTo(b);
        }
    }

    

    public class ItemWithCountSunamoComparer<T> : ISunamoComparer<ItemWithCount<T>>
    {
        public int Desc(ItemWithCount<T> x, ItemWithCount<T> y)
        {
            int a = x.count;
            int b = y.count;
            return a.CompareTo(b) * -1;
        }

        public int Asc(ItemWithCount<T> x, ItemWithCount<T> y)
        {
            int a = x.count;
            int b = y.count;
            return a.CompareTo(b);
        }
    }

    public class ItemWithDtSunamoComparer<T> : ISunamoComparer<IItemWithDt<T>>
    {
        public int Desc(IItemWithDt<T> x, IItemWithDt<T> y)
        {
            DateTime a = x.Dt;
            DateTime b = y.Dt;
            return a.CompareTo(b) * -1;
        }

        public int Asc(IItemWithDt<T> x, IItemWithDt<T> y)
        {
            DateTime a = x.Dt;
            DateTime b = y.Dt;
            return a.CompareTo(b);
        }
    }
}