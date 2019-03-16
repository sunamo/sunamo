using System;
using System.Collections.Generic;

public partial class SearchingOnWeb{ 
public static string YouTube(string vyraz)
    {
        vyraz = vyraz.Replace(" ", "+");
        return SH.Format2("http://www.youtube.com/results?search_query={0}&aq=f", vyraz);
    }
}