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
            if (s_availableCharsInVarCharWithoutDiacriticLetters.Contains(item) || SH.diacritic.IndexOf(item) != -1)
            {
                sb.Append(item);
            }
            else
            {
                sb.Append(SH.TextWithoutDiacritic(item.ToString()));
            }
        }

        return SH.ReplaceAll(sb.ToString(), AllStrings.space, AllStrings.doubleSpace);
    }
}