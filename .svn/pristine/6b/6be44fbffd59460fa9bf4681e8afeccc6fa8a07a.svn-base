using System;
using System.Collections.Generic;



/// <summary>
/// Summary description for VyhledavaniNaWebu
/// </summary>
public class SearchingOnWeb
{
    public static string ReplaceOperators(string vstup)
    {
        return SH.ReplaceAll(vstup, "", "OR", "+", "-", "\"", "*");
    }

    public static string YouTube(string vyraz)
    {
        vyraz = vyraz.Replace(" ", "+");
        return string.Format("http://www.youtube.com/results?search_query={0}&aq=f", vyraz);
    }
}
