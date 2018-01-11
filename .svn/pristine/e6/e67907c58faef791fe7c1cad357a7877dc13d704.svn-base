using HtmlAgilityPack;
using sunamo;
using sunamo.Helpers;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class UlozTo
{
    public async static Task<Uri> GetVideoUri(string niceUri)
    {
        string html = await HttpClientHelper.GetResponseText(niceUri, HttpMethod.Get, new HttpRequestData());

        HtmlDocument hd = new HtmlDocument();
        hd.LoadHtml(html);

        SortedDictionary<int, Uri> quality = new SortedDictionary<int, Uri>();

        var videoQualityButtonsNode = HtmlHelper.ReturnTagWithAttr(hd.DocumentNode, "span", "id", "videoQualityButtons");
        foreach (var qualityNode in videoQualityButtonsNode.ChildNodes)
        {
            string q = qualityNode.InnerHtml.Replace("p", "");
            int nt = 0;
            if (!int.TryParse(q, out nt))
            {
                continue;
            }

            string qUri = qualityNode.GetAttributeValue("onclick", "");
            if (qUri == "")
            {
                continue;
            }
            qUri = qUri.Replace("hraj('", "").Replace("',$(this));", "");

            try
            {
                quality.Add(nt, new Uri(qUri));
            }
            catch (Exception)
            {
            }
        }

        var sorted = quality.OrderByDescending(d => d.Key);
        Uri uri = null;
        foreach (var item in sorted)
        {
            uri = item.Value;
            break;
        }
        return uri;
    }

    public static UlozToMediaUriParts Parse(string uri)
    {


        string[] sp = SH.Split(uri, "/", "?", "=", ".", "&");
        if (sp.Length > 10)
        {
            StringBuilder errors = new StringBuilder();
            UlozToMediaUriParts ut = new UlozToMediaUriParts();
            ut.server = sp[1];
            if (ut.server.Length > 4)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "Server", ut.server));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "Server", ut.server));
            }
            ut.part1 = sp[4];
            if (ut.part1.Length > 1)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "Part1", ut.part1));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "Part1", ut.part1));
            }
            ut.part2 = sp[5];
            if (ut.part2.Length >1)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "Part2", ut.part2));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "Part2", ut.part2));
            }
            ut.part3 = sp[6];
            if (ut.part3.Length > 1)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "Part3", ut.part3));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "Part3", ut.part3));
            }
            ut.fileCode = sp[7];
            if (ut.fileCode.Length > 32)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "FileCode", ut.fileCode));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "FileCode", ut.fileCode));
            }
            ut.maxQuality = sp[8];
            if (ut.maxQuality.Length > 3)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "MaxQuality", ut.maxQuality));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "MaxQuality", ut.maxQuality));
            }
            ut.ext = sp[9];
            if (ut.ext.Length > 3)
            {
                errors.Append(HtmlGenerator2.DetailStatic("red", "Ext", ut.ext));
            }
            else
            {
                errors.Append(HtmlGenerator2.DetailStatic("green", "Ext", ut.ext));
            }
            int nt = 0;
            if (!int.TryParse(sp[11], out nt))
            {
                
                errors.Append(HtmlGenerator2.DetailStatic("red", "FileID", ut.fileId));
            }
            else
            {
                ut.fileId = nt;
                errors.Append(HtmlGenerator2.DetailStatic("green", "FileID", ut.fileId));
            }
            ut.htmlErrors = errors.ToString();
            return ut;
            //http://thv1.uloz.to/a/1/7/a17627ad6321ad0739b2f7401b8dce6e.480.mp4?fileId=43429293&amp;_ga=1.65666158.1775030816.1442328201
        }
        return null;
    }

    public static string ToUri(UlozToMediaUriParts ut)
    {
        return "http://"+ut.server+".uloz.to/"+ut.part1+"/"+ut.part2+"/"+ut.part3+"/"+ut.fileCode+"."+ut.maxQuality+"."+ut.ext+"?fileId=" + ut.fileId;
    }

    public static string GetNiceUri(bool live, string code, string name)
    {
        string liveS = "";
        if (live)
        {
            liveS = "live/";
        }
        return "http://uloz.to/" + liveS + code + "/" + name;
    }

    /// <summary>
    /// U A1 se musí jednat o adresu Uri
    /// </summary>
    /// <param name="niceUri"></param>
    /// <returns></returns>
    public static UlozToNiceUriParts ParseNiceUri(string niceUri)
    {
        List<string> sp = new List<string>( SH.Split( UH.GetPageNameFromUri(new Uri(niceUri)), "/"));
        string live = "live";
        if (sp[0] == live)
        {
            sp.RemoveAt(0);
        }
        UlozToNiceUriParts vr = new UlozToNiceUriParts { code = sp[0], name = sp[1] };
        return vr;
    }
}

public class UlozToNiceUriParts
{
    public string code = "";
    public string name = "";
}

public class UlozToMediaUriParts
{
    public string server = "";
    public string part1 = "";
    public string part2 = "";
    public string part3 = "";
    
    public string fileCode = "";
    public string maxQuality = "";
    public string ext = "";
    public int fileId = 0;

    public string htmlErrors = "";
}
