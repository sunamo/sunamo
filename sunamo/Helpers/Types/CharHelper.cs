using System;
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

    public static string OnlyAccepted(string v, Func<char, bool> isDigit, bool not = false)
    {
        StringBuilder sb = new StringBuilder();
        bool result = false;
        foreach (var item in v)
        {
            result = isDigit.Invoke(item);
            if (not)
            {
                result = !result;
            }

            if (result)
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }

    public static string OnlyAccepted(string v, List< Func<char, bool>> isDigit, bool not = false)
    {
        StringBuilder sb = new StringBuilder();
        //bool result = true;
        foreach (var item in v)
        {
            foreach (var item2 in isDigit)
            {
                if (item2.Invoke(item))
                {
                    sb.Append(item);
                    break;
                }
            }
        }
        return sb.ToString();
    }
}