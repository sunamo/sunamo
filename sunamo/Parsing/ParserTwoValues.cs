using System;
using System.Collections.Generic;
using System.Text;


public class ParserTwoValues 
{
    public static string ToString(string delimiter, string a, string b)
    {
        return a + delimiter + b;
    }

    public static List<double> ParseDouble(string delimiter, string s)
    {
        return CA.ToNumber<double>(double.Parse, ParseString(delimiter, s));
    }

    public static List<string> ParseString(string delimiter, string s)
    {
        return SH.Split(s, delimiter);
    }
}