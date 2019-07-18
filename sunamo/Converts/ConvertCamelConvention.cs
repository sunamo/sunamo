using System;
using System.Collections.Generic;
using System.Text;


public class ConvertCamelConvention
{
    public static bool IsCamel(string r)
    {
        if (r.ToLower() == r)
        {
            return false;
        }
        var s = ToConvention(r);
        return s ==r ;
    }

    /// <summary>
    /// will include numbers
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ToConvention(string p)
    {
        return SH.FirstCharLower(ConvertPascalConvention.ToConvention(p));
    }
}


public class ConvertCamelConventionWithNumbers
{
    public static bool IsCamelWithNumber(string r)
    {
        if (r.ToLower() == r && !r.Contains(" "))
        {
            return true;
        }
        var s = ToConvention( r);
        
        return s == r;
    }

    /// <summary>
    /// wont include numbers
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ToConvention(string p)
    {
        return SH.FirstCharLower(ConvertPascalConventionWithNumbers.ToConvention(p));
    }
}

