using System.Text;
using System;
public class ConvertEveryWordLargeCharConvention //: IConvertConvention
{
    /// <summary>
    /// NI
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
        return CA.IsEqualToAnyElement<char>(item, AllChars.bs, AllChars.lb, AllChars.rb, AllChars.lsf, AllChars.rsf, AllChars.dot, AllChars.apostrophe);
    }
}
