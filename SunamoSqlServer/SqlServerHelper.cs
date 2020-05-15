using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public partial class SqlServerHelper
{
  
    
    

    public static bool IsNull(object o)
    {
        return o == null || o == DBNull.Value;
    }

   public static Tuple<int, int> UnnormalizeNumber(int serie)
    {
        const int increaseAbout = 1000; 

        int l = int.MinValue;
        int h = l + increaseAbout;

        for (int i = 0; i < serie; i++)
        {
            l += increaseAbout;
            h += increaseAbout;
        }

        Tuple<int, int> d = new Tuple<int, int>(l, h);
        return d;
    }
}