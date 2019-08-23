using System;
using System.Collections.Generic;
using System.Text;


public static class StringBuilderExtensions
{
    public static void Trim(this StringBuilder sb)
    {
        var length = sb.Length;
        for (int i = length - 1; i >= 0; i--)
        {
            if (char.IsWhiteSpace( sb[i]))
            {
                sb.Remove(i, 1);
            }
            else
            {
                break;
            }
        }

        length = sb.Length;
        for (int i = 0; i < length; i++)
        {
            if (char.IsWhiteSpace(sb[i]))
            {
                sb.Remove(i, 1);
            }
            else
            {
                break;
            }
        }
    }
}

