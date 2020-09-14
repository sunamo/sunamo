using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SunamoComparer
{
    public class Float : ISunamoComparer<float>
    {
        public static Float Instance = new Float();

        public int Desc(float x, float y)
        {
            return x.CompareTo(y) * -1;
        }

        public int Asc(float x, float y)
        {
            return x.CompareTo(y);
        }
    }


    public class StringLength : ISunamoComparer<string>
    {
        public static StringLength Instance = new StringLength();

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
}