using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class ConvertCamelConvention
{
    public static bool IsCamel(string r)
    {
        if (r.ToLower() == r)
        {
            return false;
        }
        var s = ToConvention(r);
        return s == r;
    }

    /// <summary>
    /// will include numbers
    /// </summary>
    /// <param name="p"></param>
    public static string ToConvention(string p)
    {
        return SH.FirstCharLower(ConvertPascalConventionWithNumbers.ToConvention(p));
    }
}


