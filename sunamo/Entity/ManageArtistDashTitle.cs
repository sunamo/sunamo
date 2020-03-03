using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Is used by more projects - for example MusicSorter, sunamo.cz, SunamoCzAdmin
/// </summary>
public partial class ManageArtistDashTitle
{
    /// <summary>
    /// První písmeno, písmena po AllChars.space a AllStrings.dash budou velkým.
    /// </summary>
    /// <param name = "názevSouboru"></param>
    /// <param name = "p"></param>
    
    public static string Reverse(string text)
    {
        List<string> d = SH.Split( text,AllChars.dash);
        string temp = d[0];
        d[0] = d[d.Count - 1];
        d[d.Count - 1] = temp;
        StringBuilder sb = new StringBuilder();
        foreach (string item in d)
        {
            sb.Append(item + AllStrings.dash);
        }

        return sb.ToString().TrimEnd(AllChars.dash);
    }
}