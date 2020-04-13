using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SunamoExceptions
{ 
public class SH
{
    /// <summary>
    /// Will be delete after final refactoring
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    public static string Join(IEnumerable parts, object delimiter)
    {
        var d = delimiter.ToString();

        StringBuilder sb = new StringBuilder();
        foreach (var item in parts)
        {
            sb.Append(item.ToString() + d);
        }

        var vr = sb.ToString();
        return vr.Substring(0, vr.Length - d.Length);
    }

    }
}