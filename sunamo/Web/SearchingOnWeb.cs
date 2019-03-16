using System;
using System.Collections.Generic;
/// <summary>
/// Summary description for VyhledavaniNaWebu
/// </summary>
public partial class SearchingOnWeb
{
    public static string ReplaceOperators(string vstup)
    {
        return SH.ReplaceAll(vstup, "", "OR", "+", "-", "\"", "*");
    }

}