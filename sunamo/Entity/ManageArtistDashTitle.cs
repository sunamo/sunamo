﻿using System;
using System.Text;

/// <summary>
/// Is used by more projects - for example MusicSorter, sunamo.cz, SunamoCzAdmin
/// </summary>
public class ManageArtistDashTitle
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="název"></param>
    /// <param name="title"></param>
    /// <param name="remix"></param>
    public static void GetArtistTitleRemix(string item, out string název, out string title, out string remix)
    {
        string[] toks = item.Split(new string[] { AllStrings.dash }, StringSplitOptions.RemoveEmptyEntries);
        název = title = "";
        if (toks.Length == 0)
        {
            název = title = remix = "";
        }
        else if (toks.Length == 1)
        {
            název = "";
            VratTitleRemix(toks[0], out title, out remix);
        }
        else
        {
            název = toks[0];
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < toks.Length; i++)
            {
                sb.Append(toks[i] + AllStrings.dash);
            }
            VratTitleRemix(sb.ToString().TrimEnd(AllChars.dash), out title, out remix);
        }
    }

    /// <summary>
    /// První písmeno, písmena po AllChars.space a AllStrings.dash budou velkým.
    /// </summary>
    /// <param name="názevSouboru"></param>
    /// <param name="p"></param>
    /// <returns></returns>
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
                catch (Exception)
                {
                }
            }
            else if (ch[i] == AllChars.dash)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception)
                {
                }
            }
            else if (ch[i] == AllChars.lsf)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception)
                {
                }
            }
            else if (ch[i] == AllChars.lb)
            {
                try
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
                catch (Exception)
                {
                }
            }
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(ch);
        return sb.ToString();
    }

    /// <summary>
    /// NSN
    /// </summary>
    /// <param name="p"></param>
    /// <param name="title"></param>
    /// <param name="remix"></param>
    private static void VratTitleRemix(string p, out string title, out string remix)
    {
        title = p;
        remix = "";
        int firstHranata = p.IndexOf(AllChars.lsf);
        int firstNormal = p.IndexOf(AllChars.lb);
        if (firstHranata == -1 && firstNormal != -1)
        {
            VratRozdeleneByVcetne(p, firstNormal, out title, out remix);
        }
        else if (firstHranata != -1 && firstNormal == -1)
        {
            VratRozdeleneByVcetne(p, firstHranata, out title, out remix);
        }
        else if (firstHranata != -1 && firstNormal != -1)
        {
            if (firstHranata < firstNormal)
            {
                VratRozdeleneByVcetne(p, firstNormal, out title, out remix);
            }
            else
            {
                VratRozdeleneByVcetne(p, firstHranata, out title, out remix);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="firstNormal"></param>
    /// <param name="title"></param>
    /// <param name="remix"></param>
    private static void VratRozdeleneByVcetne(string p, int firstNormal, out string title, out string remix)
    {
        title = p.Substring(0, firstNormal);
        remix = p.Substring(firstNormal);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="cimNahradit"></param>
    /// <returns></returns>
    public static string ReplaceAllHyphensExceptTheFirst(string p, string cimNahradit)
    {
        int dex = p.IndexOf(AllChars.dash);
        p = p.Replace(AllChars.dash, AllChars.space);
        char[] j = p.ToCharArray();
        j[dex] = AllChars.dash;
        return new string(j);
    }

    /// <summary>
    /// NSN
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public string GetTitle(string item)
    {
        string nazev, title = null;
        GetArtistTitle(item, out nazev, out title);
        return title;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="název"></param>
    /// <param name="title"></param>
    public static void GetArtistTitle(string item, out string název, out string title)
    {
        // Path.GetFileNameWithoutExtension()
        string[] toks = System.IO.Path.GetFileNameWithoutExtension( item).Split(new string[] { AllStrings.dash }, StringSplitOptions.RemoveEmptyEntries);
        název = title = "";
        if (toks.Length == 0)
        {
            název = title = "";
        }
        else if (toks.Length == 1)
        {
            název = "";
            title = toks[0];
        }
        else
        {
            název = toks[0];
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < toks.Length; i++)
            {
                sb.Append(toks[i] + AllStrings.dash);
            }
            title = sb.ToString().TrimEnd(AllChars.dash);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Reverse(string text)
    {
        string[] d = text.Split(AllChars.dash);
        string temp = d[0];
        d[0] = d[d.Length - 1];
        d[d.Length - 1] = temp;
        StringBuilder sb = new StringBuilder();
        foreach (string item in d)
        {
            sb.Append(item + AllStrings.dash);
        }
        return sb.ToString().TrimEnd(AllChars.dash);
    }

    /// <summary>
    /// 
    /// Získám interpreta
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string GetArtist(string item)
    {
        string nazev, title = null;
        GetArtistTitle(item, out nazev, out title);
        return nazev;
    }
}
