using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static partial class RandomHelper
{
    private static Random s_rnd = new Random();

    public static T RandomElementOfCollectionT<T>(IList<T> ppk)
    {
        if (ppk.Count == 0)
        {
            return default(T);
        }
        int nt = RandomInt(ppk.Count);
        return ppk[nt];
    }

    /// <summary>
    /// better is take keys from dict and RandomElementOfCollection
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dict"></param>
    
    public static short RandomShort()
    {
        return (short)s_rnd.Next(0, short.MaxValue);
    }

public static bool RandomBool()
    {
        int nt = RandomInt(2);
        string pars = "";
        if (nt == 0)
        {
            pars = bool.FalseString;
        }
        else
        {
            pars = bool.TrueString;
        }
        return bool.Parse(pars);
    }

    public static DateTime RandomDateTime(int yearTo)
    {
        DateTime result = Consts.DateTimeMinVal;
         result = result.AddDays(RandomHelper.RandomDouble(1, 28));
        result = result.AddMonths(RandomHelper.RandomInt(1, 12));
        var yearTo2 = yearTo - DTConstants.yearStartUnixDate  ;
        result = result.AddYears(RandomHelper.RandomInt(1, yearTo2) + 70);

        result = result.AddHours(RandomDouble(1, 24));
        result = result.AddMinutes(RandomDouble(1, 60));
        result = result.AddSeconds(RandomDouble(1, 60));

        return result;
    }

    private static double RandomDouble(int v1, int v2)
    {
        return (double)RandomInt(v1, v2);
    }
}