using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PowershellParser
{
    static Type type = typeof(PowershellParser);



    public static List<string> ParseToParts(string d, string charWhichIsNotContained)
    {
        if (d.Contains(charWhichIsNotContained))
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), d + " contains " + charWhichIsNotContained);
        }

        StringBuilder sb = new StringBuilder(d);
        var b = SH.ValuesBetweenQuotes(d, true);
        foreach (var item in b)
        {
            sb = sb.Replace(item, item.Replace( AllStrings.space, charWhichIsNotContained));
        }

        var p = SH.Split(sb.ToString(), AllStrings.space);
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = p[i].Replace(charWhichIsNotContained, AllStrings.space);
        }

        return p;
    }
}
