﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CharHelper
{
    public static bool IsSpecial(char c)
    {
        bool v = CA.IsEqualToAnyElement<char>(c, AllChars.specialChars);
        if (!v)
        {
            v = CA.IsEqualToAnyElement<char>(c, AllChars.specialChars2);
        }
        return v;
    }

    public static string OnlyDigits(string v)
    {
        return OnlyAccepted(v, char.IsDigit);
    }

    public static bool IsGeneric(char c)
    {
        return CA.IsEqualToAnyElement<char>(c, AllChars.generalChars);
    }

    private static string OnlyAccepted(string v, Func<char, bool> isDigit)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in v)
        {
            if (isDigit.Invoke(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }
}