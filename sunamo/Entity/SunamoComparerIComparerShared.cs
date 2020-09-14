using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SunamoComparerICompare
{
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