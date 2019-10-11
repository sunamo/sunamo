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
                var after = SH.TextWithoutDiacritic(before);
                if (before != after)
                {
                    sb.Append(after);
                }
                
            }
        }

        return SH.ReplaceAll(sb.ToString(), AllStrings.space, AllStrings.doubleSpace);
    }
}