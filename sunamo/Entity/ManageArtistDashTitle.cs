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
    public static string ArtistAndTitleToUpper(string názevSouboru, string p)
    {
        char[] ch = názevSouboru.ToCharArray();
        ch[0] = char.ToUpper(názevSouboru[0]);
        int dex = názevSouboru.IndexOf(p);
        ch[dex + 1] = char.ToUpper(ch[dex + 1]);
        for (int i = 1; i < ch.Length; i++)
        {
            if (ch[i] == AllChars.space)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception ex)
                {
                }
            }
            else if (ch[i] == AllChars.dash)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception ex)
                {
                }
            }
            else if (ch[i] == AllChars.rsqb)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception ex)
                {
                }
            }
            else if (ch[i] == AllChars.lb)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception ex)
                {
                }
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(ch);
        return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "cimNahradit"></param>
    public static string ReplaceAllHyphensExceptTheFirst(string p, string cimNahradit)
    {
        int dex = p.IndexOf(AllChars.dash);
        p = p.Replace(AllChars.dash, AllChars.space);
        char[] j = p.ToCharArray();
        j[dex] = AllChars.dash;
        return new string(j);
    }

    /// <summary>
    /// IUN
    /// 
    /// </summary>
    /// <param name = "item"></param>
    public string GetTitle(string item)
    {
        string nazev, title = null;
        GetArtistTitle(item, out nazev, out title);
        return title;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "text"></param>
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