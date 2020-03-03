using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SunamoPointExtensions
{
    public static System.Windows.Point ToSystemWindows(this SunamoPoint ss)
    {
        return new System.Windows.Point(ss.X, ss.Y);
    }
}