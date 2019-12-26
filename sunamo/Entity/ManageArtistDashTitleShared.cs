using System;
using System.Collections.Generic;
using System.Text;

public partial class ManageArtistDashTitle
{

    public static void GetArtistTitleRemix(string item, out string artist, out string song, out string remix)
    {
        var r = GetArtistTitleRemix(item);
        artist = r.Item1;
        song = r.Item2;
        remix = r.Item3;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "item"></param>
    /// <param name = "artist"></param>
    /// <param name = "song"></param>
    /// <param name = "remix"></param>
    public static Tuple<string,string,string> GetArtistTitleRemix(string item)
    {


        string artist;string song; string remix;
        string delimiter = SH.WrapWith(AllStrings.dash, AllChars.space);

        if (!item.Contains(delimiter))
        {
            delimiter = AllStrings.dash;
        }

        string[] toks = item.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        artist = song = "";
        if (toks.Length == 0)
        {
            artist = song = remix = "";
        }
        else if (toks.Length == 1)
        {
            artist = "";
            VratTitleRemix(toks[0], out song, out remix);
        }
        else
        {


            artist = toks[0];

            List<string> left, right;
            left = right = null;

            if (SH.ContainsBracket( artist, ref left, ref right))
            {
                if (left.Count -1 == right.Count )
                {
                    var closingBracket = SH.ClosingBracketFor(left[0]);
                    right.Add(closingBracket);
                    artist += closingBracket;
                }
                var between = SH.GetTextBetween(artist, left[0], right[0]);
                between = left[0] + between + right[0];
                item = item.Replace(between, string.Empty);
                item += " " + between;
                toks = item.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                artist = toks[0].Trim();
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < toks.Length; i++)
            {
                sb.Append(toks[i]);
            }

            VratTitleRemix(sb.ToString().TrimEnd(AllChars.dash), out song, out remix);
        }
        return new Tuple<string, string, string>(artist, song, remix);
    }

    /// <summary>
    /// NSN
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "title"></param>
    /// <param name = "remix"></param>
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
    /// <param name = "p"></param>
    /// <param name = "firstNormal"></param>
    /// <param name = "title"></param>
    /// <param name = "remix"></param>
    private static void VratRozdeleneByVcetne(string p, int firstNormal, out string title, out string remix)
    {
        title = p.Substring(0, firstNormal);
        remix = p.Substring(firstNormal);
    }
}