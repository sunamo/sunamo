using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HtmlStartingTags
{
    static Type type = typeof(HtmlStartingTags);

    public const string b = "<b>";
    public const string i = "<i>";
    public const string s = "<s>";

    public static string Get(string value)
    {
        var v = RH.GetValuesOfConsts(type, value);
        return v.First().Value;
    }
}