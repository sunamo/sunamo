using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// For sharing with Apps
/// </summary>
public partial class SqlServerHelper
{
    /// <summary>
    /// Tato hodnota byla založena aby používal všude v DB konzistentní datovou hodnotu, klidně může mít i hodnotu DT.MaxValue když to tak má být
    /// </summary>
    public static readonly DateTime DateTimeMinVal = new DateTime(1900, 1, 1);
    public static readonly DateTime DateTimeMaxVal = new DateTime(2079, 6, 6);

    
    
    

    public static string ConvertToVarCharPathOrFileName(string maybeUnicode)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in maybeUnicode)
        {
            if (s_availableCharsInVarCharWithoutDiacriticLetters.Contains(item) || SH.diacritic.IndexOf(item) != -1)
            {
                sb.Append(item);
            }
        }

        string vr = SH.ReplaceAll(sb.ToString(), AllStrings.space, AllStrings.doubleSpace).Trim();
        vr = SH.ReplaceAll(vr, AllStrings.bs, " \\");
        vr = SH.ReplaceAll(vr, AllStrings.bs, "\\ ");
        vr = SH.ReplaceAll(vr, AllStrings.slash, "/ ");
        vr = SH.ReplaceAll(vr, AllStrings.slash, " /");
        return vr;
    }
}