using System;
using System.Text;

public partial class ManageArtistDashTitle
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name = "item"></param>
    /// <param name = "název"></param>
    /// <param name = "title"></param>
    /// <param name = "remix"></param>
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