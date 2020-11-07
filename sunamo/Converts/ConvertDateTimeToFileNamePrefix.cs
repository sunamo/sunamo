using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Sister class to ConvertDateTimeToFileNamePostfix
/// </summary>
public class ConvertDateTimeToFileNamePrefix
{
    private static char s_delimiter = AllChars.lowbar;

    /// <summary>
    /// Convert from date to filename without ext
    ///  If A1 will contains delimiter (now _), it won't be replaced by space. If its on end, its succifient while parsing use SH.SplitToPartsFromEnd 
    /// </summary>
    public static string ToConvention(string prefix, DateTime dt, bool time)
    {
        //prefix = SH.ReplaceAll(prefix, AllStrings.space, AllStrings.lowbar);
        return prefix + s_delimiter + DTHelper.DateTimeToFileName(dt, time);
    }

    public static DateTime? FromConvention(string fnwoe, bool time)
    {
        string prefix = "";
        return DTHelper.FileNameToDateTimePrefix(fnwoe, time, out prefix);
    }
}