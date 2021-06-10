using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SunamoComparerICompare
{
    /// <summary>
    /// Asc - is always default. Dont create any new classes anymore. When want desc, use reverse!
    /// </summary>
    public class DT : IComparer<DateTime>
    {
        public static DT Instance = new DT();

        private DT()
        {

        }

        public int Compare(DateTime a, DateTime b)
        {
            if (a > b)
            {
                return NumConsts.one;
            }
            else if (a < b)
            {
                return NumConsts.mOne;
            }
            else
            {
                return NumConsts.zeroInt;
            }
        }
    }

    /// <summary>
    /// Asc - is always default. Dont create any new classes anymore. When want desc, use reverse!
    /// </summary>
    public class Integer : IComparer<int>
    {
        public int Compare(int a, int b)
        {
            if (a > b)
            {
                return NumConsts.one;
            }
            else if (a < b)
            {
                return NumConsts.mOne;
            }
            else
            {
                return NumConsts.zeroInt;
            }
        }
    }

    public class StringLength
    {
        public class Asc : IComparer<string>
        {
            private ISunamoComparer<string> _sc = null;

            /// <summary>
            /// As parameter I can insert SunamoComparer.IEnumerableCharLength or SunamoComparer.StringLength
            /// </summary>
            /// <param name="sc"></param>
            public Asc(ISunamoComparer<string> sc)
            {
                _sc = sc;
            }


            public int Compare(string x, string y)
            {
                return _sc.Asc(x, y);
            }
        }

        public class Desc : IComparer<string>
        {
            private ISunamoComparer<string> _sc = null;

            /// <summary>
            /// As parameter I can insert SunamoComparer.IEnumerableCharLength or SunamoComparer.StringLength
            /// </summary>
            /// <param name="sc"></param>
            public Desc(ISunamoComparer<string> sc)
            {
                _sc = sc;
            }


            public int Compare(string x, string y)
            {
                return _sc.Desc(x, y);
            }
        }
    }
}