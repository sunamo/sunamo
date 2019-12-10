using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SqlServerHelper
{
    public static string ConvertToVarChar(string maybeUnicode)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in maybeUnicode)
        {
            var b1 = s_availableCharsInVarCharWithoutDiacriticLetters.Contains(item);
            var b2 = SH.diacritic.IndexOf(item) != -1;

            if (b1 || b2)
            {
                sb.Append(item);
            }
            else
            {
                string before = item.ToString();
                // Is use Diacritics package which allow pass only string, not char
                var after = SH.TextWithoutDiacritic(before);
                // if wont be here !char.IsWhiteSpace(item), it will strip newlines
                if (before != after || char.IsWhiteSpace(item))
                {
                    sb.Append(after);
                }
                
            }
        }

        var vr = SH.ReplaceAll(sb.ToString(), AllStrings.space, AllStrings.doubleSpace);
        return vr;
    }
}