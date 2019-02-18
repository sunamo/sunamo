using sunamo.Values;
using System;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;
using System.Linq;

public static partial class NH{ 
public static string MakeUpTo2NumbersToZero(int p)
    {
        string s = p.ToString();
        if (s.Length == 1)
        {
            return "0" + p;
        }
        return s;
    }

}