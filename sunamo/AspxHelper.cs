using System;
using System.Collections.Generic;
using System.Text;


public class AspxHelper
{
    const string d1 = "<p class=\"chyba\" id=\"errors\" runat=\"server\" visible=\"false\" enableviewstate=\"false\">";

    const string d2 = "<p class=\"chyba\" runat=\"server\" id=\"errors\">";

    const string d3 = "<p class=\"chyba\" runat=\"server\" id=\"errors\" enableviewstate=\"false\">";

    const string d4 = "<p class=\"chyba\" runat=\"server\" id=\"errors\" visible=\"false\" enableviewstate=\"false\">";
    public const string p = "</p>";

    public static string CorrectEndingPString(string content)
    {
        var lines = SH.GetLines(content);
        CorrectEndingP(lines);
        return SH.JoinNL(lines);
    }

    public static void CorrectEndingP(string folder)
    {
        var files = FS.GetFiles(folder, "*.aspx", System.IO.SearchOption.AllDirectories);
        foreach (var item in files)
        {
            var lines = TF.ReadAllLines(item);

            CorrectEndingP(lines);

            TF.SaveLines(lines, item);
        }
    }

    public static void CorrectEndingP(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {

            var item = lines[i];
            if (item.Contains("id=\"errors") && item.Contains("class=\"chyba") && !item.Contains("</p>"))
            {
                lines[i] += "</p>";
                break;
            }
            if (item.Contains("id=\"errors") && item.Contains("class=\"error") && !item.Contains("</p>"))
            {
                lines[i] += "</p>";
                break;
            }
            if (item.Contains("id=\"pFooter") && !item.Contains("</p>"))
            {
                lines[i] += "</p>";
                break;
            }

        }
    }

    public static string MakeValidXhtml(string content)
    {
        string d = null;

        content = MakeValidXhtmlTemplate(content, d1, p);
        content = MakeValidXhtmlTemplate(content, d2, p);
        content = MakeValidXhtmlTemplate(content, d3, p);
        content = MakeValidXhtmlTemplate(content, d4, p);

        content = CorrectEndingPString(content);
        return content;
    }

    private static string MakeValidXhtmlTemplate(string content, string d, string p)
    {
        return SH.ReplaceOnce(content, d, d + p);
    }

    public static void CorrectEndingPFile(string file)
    {
        var lines = TF.ReadAllLines(file);
        CorrectEndingP(lines);
         TF.SaveLines(lines, file);
    }
}