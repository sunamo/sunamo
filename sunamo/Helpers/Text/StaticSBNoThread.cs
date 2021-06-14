using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StaticSBNoThread
{
    public static StringBuilder sb = new StringBuilder();

    public static void Clear()
    {
        sb.Clear();
    }

    public static void Append(string t)
    {
        sb.Append(t);
    }
}
