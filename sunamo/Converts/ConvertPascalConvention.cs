using System.Text;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ConvertPascalConvention //: IConvertConvention
{
    /// <summary>
    /// A2 NSN
    /// </summary>
    /// <param name="p"></param>
    public static string FromConvention(string p, bool allLettersExceptFirstLower = true)
    {
        var r = Regex.Replace(p, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}").ToLower();
        if (char.IsLower( p[0]))
        {
            return SH.FirstCharLower(r);
        }
        return SH.FirstCharUpper(r);
    }

    /// <summary>
    /// Wont include numbers
    /// Převede na pascalskou konvenci, to znamená že tam budou pouze velké a malé písmena a 
    /// písmena za odebranými znaky budou velké.
    /// hello world = helloWorld
    /// Hello world = HelloWorld
    /// helloWorld = helloWorld
    /// </summary>
    /// <param name="p"></param>
    public static string ToConvention(string p)
    {
        StringBuilder sb = new StringBuilder();
        bool dalsiVelke = false;
        foreach (char item in p)
        {
            if (dalsiVelke)
            {
                if (char.IsUpper(item))
                {
                    dalsiVelke = false;
                    sb.Append(item);
                    continue;
                }
                else if (char.IsLower(item))
                {
                    dalsiVelke = false;
                    sb.Append(char.ToUpper(item));
                    continue;
                }
                else
                {
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                sb.Append(item);
            }
            else if (char.IsLower(item))
            {
                sb.Append(item);
            }
            else
            {
                dalsiVelke = true;
            }
        }
        return sb.ToString();
    }

    public static List<string> FromConvention(List<string> list, bool allLettersExceptFirstLower)
    {
        CA.Trim(list);
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = FromConvention(list[i], allLettersExceptFirstLower);
        }
        return list;
    }

    public static bool IsPascal(string r)
    {
        var s = ToConvention(r);
        return r == s;
    }
}

public class ConvertPascalConventionWithNumbers //: IConvertConvention
{
    /// <summary>
    /// NI
    /// </summary>
    /// <param name="p"></param>
    public static string FromConvention(string p)
    {
        return SH.FirstCharUpper(Regex.Replace(p, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}").ToLower());
    }

    public static bool IsPascalWithNumber(string r)
    {
        var s = ToConvention(r);
        return r == s;
    }

    /// <summary>
    /// Will include numbers
    /// Převede na pascalskou konvenci, to znamená že tam budou pouze velké a malé písmena a 
    /// písmena za odebranými znaky budou velké.
    /// hello world = helloWorld
    /// Hello world = HelloWorld
    /// helloWorld = helloWorld
    /// </summary>
    /// <param name="p"></param>
    public static string ToConvention(string p)
    {
        StringBuilder sb = new StringBuilder();
        bool dalsiVelke = false;
        foreach (char item in p)
        {
            if (dalsiVelke)
            {
                if (char.IsUpper(item))
                {
                    dalsiVelke = false;
                    sb.Append(item);
                    continue;
                }
                else if (char.IsLower(item))
                {
                    dalsiVelke = false;
                    sb.Append(char.ToUpper(item));
                    continue;
                }
                else if (char.IsDigit(item))
                {
                    dalsiVelke = true;
                    sb.Append(item);
                    continue;
                }
                else
                {
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                sb.Append(item);
            }
            else if (char.IsLower(item))
            {
                sb.Append(item);
            }
            else if (char.IsDigit(item))
            {
                sb.Append(item);
            }
            else
            {
                dalsiVelke = true;
            }
        }
        return sb.ToString().Trim();
    }
}