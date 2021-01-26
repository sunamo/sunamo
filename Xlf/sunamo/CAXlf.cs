using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CAXlf
{
    #region Only in *Xlf.cs
    public static List<T> ToList<T>(params T[] t)
    {
        return new List<T>(t);
    } 
    #endregion
}