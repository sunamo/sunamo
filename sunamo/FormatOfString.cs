using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Alternatives: TextFormatData
/// </summary>
public class FormatOfString
{
    /// <summary>
    /// A2 = {Width=|, Height=|}
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static List<string> GetParsedParts(string v1, string v2)
    {
        var vb = SH.Split(v2, AllStrings.verbar);

        if (vb[0] == v1)
        {
            return new List<string>();
        }

        if (SH.ContainsAll(v1, vb))
        {
            var result = SH.Split(v1, vb);
            return result;
        }

        return new List<string>();
    }

    public static bool HasFormat(string v1, string v2)
    {
        var vb = AllStrings.verbar;

        var countOfVerbar = SH.OccurencesOfStringIn(v2, vb);
        //countOfVerbar++;

        if (v2.StartsWith(vb))
        {
            countOfVerbar++;
        }
        if (v2.EndsWith(vb))
        {
            countOfVerbar++;
        }

        var p = GetParsedParts(v1, v2);
        return p.Count == countOfVerbar;
    }
}