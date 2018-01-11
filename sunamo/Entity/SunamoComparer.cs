using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using sunamo.Data;

public class SunamoComparerICompare
{
    public class StringLength 
    {
        public class Asc : IComparer<string>
        {
            ISunamoComparer<string> sc = null;

            public Asc(ISunamoComparer<string> sc)
            {
                this.sc = sc;
            }

            public int Compare(string x, string y)
            {
                return sc.Asc(x, y);
            }
        }
    }

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

    public class ItemWithCountComparer
    {
        public class Desc<T> : IComparer<ItemWithCount<T>>
        {
            ISunamoComparer<ItemWithCount<T>> sc = null;

            public Desc(ISunamoComparer<ItemWithCount<T>> sc)
            {
                this.sc = sc;
            }

            public int Compare(ItemWithCount<T> x, ItemWithCount<T> y)
            {
                return sc.Desc(x, y);
            }
        }

        public class Asc<T> : IComparer<ItemWithCount<T>>
        {
            ISunamoComparer<ItemWithCount<T>> sc = null;

            public Asc(ISunamoComparer<ItemWithCount<T>> sc)
            {
                this.sc = sc;
            }

            public int Compare(ItemWithCount<T> x, ItemWithCount<T> y)
            {
                return sc.Asc(x, y);
            }
        }
    }
}

public  class SunamoComparer
{
    public  class Integer : ISunamoComparer<int>
    {
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
        public int Desc(DateTime x, DateTime y)
        {
            return x.CompareTo(y) * -1;
        }

        public int Asc(DateTime x, DateTime y)
        {
            return x.CompareTo(y);
        }
    }

    public class Float : ISunamoComparer<float>
    {
        public int Desc(float x, float y)
        {
            return x.CompareTo(y) * -1;
        }

        public int Asc(float x, float y)
        {
            return x.CompareTo(y);
        }
    }

    public class IEnumerableCharLength : ISunamoComparer<IEnumerable<char>>
    {
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

    public class StringLength : ISunamoComparer<string>
    {
        public int Desc(string x, string y)
        {
            int a = x.Length;
            int b = y.Length;
            return a.CompareTo(b) * -1;
        }

        public int Asc(string x, string y)
        {
            int a = x.Length;
            int b = y.Length;
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
}
