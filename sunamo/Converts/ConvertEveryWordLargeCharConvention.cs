using System.Text;
using System;
public class ConvertEveryWordLargeCharConvention //: IConvertConvention
{
    static Type type = typeof(ConvertEveryWordLargeCharConvention);

    /// <summary>
    /// NI
    /// </summary>
    /// <param name="p"></param>
    public static string FromConvention(string p)
    {
        ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        return null;
    }

    /// <summary>
    /// hello world => Hello World
    /// Hello world => Hello World
    /// helloWorld => Hello World
    /// hello+world => Hello World
    /// hello+World => Hello World
    /// hello 12 world => Hello 12 World
    /// hello 21world => Hello 21 World
    /// Hello21world => Hello21 World
    /// </summary>
    /// <param name="p"></param>
    public static string ToConvention(string p)
    {
        p = p.ToLower();
        StringBuilder sb = new StringBuilder();
        bool dalsiVelke = true;
        foreach (char item in p)
        {
            if (dalsiVelke)
            {
                if (char.IsUpper(item))
                {
                    dalsiVelke = false;
                    sb.Append(AllChars.space);
                    sb.Append(item);
                    continue;
                }
                else if (char.IsLower(item))
                {
                    dalsiVelke = false;
                    if (sb.Length != 0)
                    {
                        if (!IsSpecialChar(sb[sb.Length - 1]))
                        {
                            sb.Append(AllChars.space);
                        }
                    }
                    sb.Append(char.ToUpper(item));
                    continue;
                }
                else if (IsSpecialChar(item))
                {
                    sb.Append(item);
                    continue;
                }
                else if (char.IsDigit(item))
                {
                    sb.Append(item);
                    continue;
                }
                else
                {
                    sb.Append(AllChars.space);
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                if (!char.IsUpper(sb[sb.Length - 1]))
                {
                    sb.Append(AllChars.space);
                }
                sb.Append(item);
            }
            else if (char.IsLower(item))
            {
                sb.Append(item);
            }
            else if (char.IsDigit(item))
            {
                dalsiVelke = true;
                sb.Append(item);
                continue;
            }
            else if (IsSpecialChar(item))
            {
                sb.Append(item);
                continue;
            }
            else
            {
                sb.Append(AllChars.space);
                dalsiVelke = true;
            }
        }
        string vr = sb.ToString().Trim();

        vr = SH.ReplaceAll(vr, AllStrings.space, AllStrings.doubleSpace);
        return vr;
    }

    private static bool IsSpecialChar(char item)
    {
        return CA.IsEqualToAnyElement<char>(item, AllChars.bs, AllChars.lb, AllChars.rb, AllChars.rsqb, AllChars.lsqb, AllChars.dot, AllChars.apostrophe);
    }
}