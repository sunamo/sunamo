using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConvertDateTimeToFileNamePostfix
{
    private static char s_delimiter = AllChars.lowbar;

    /// <summary>
    /// Convert from date to filename without ext
    /// If A1 will contains delimiter (now _), it won't be replaced by space. If its on end, its succifient while parsing use SH.SplitToParts
    /// </summary>
    public static string ToConvention(string postfix, DateTime dt, bool time)
    {
        //postfix = SH.ReplaceAll(postfix, AllStrings.space, AllStrings.lowbar);
        return DTHelper.DateTimeToFileName(dt, time) + s_delimiter + postfix;
    }

    /// <summary>
    /// It's used if you don't want to get postfix, if yes, use DTHelper.FileNameToDateTimePostfix
    /// </summary>
    /// <param name="fnwoe"></param>
    public static DateTime? FromConvention(string fnwoe, bool time)
    {
        string postfix = "";
        return DTHelper.FileNameToDateTimePostfix(fnwoe, time, out postfix);
    }
}