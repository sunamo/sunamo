using System.Runtime.CompilerServices;

public static partial class HtmlTemplates
{
    public const string htmlStartTitle = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"https://www.w3.org/1999/xhtml\" ><head><title>";
    /// <summary>
    /// Toto se muze pouzivat pouze kdyz nechces nic zadat do head, jinak pouzij ostatni konstanty zde
    /// </summary>
    public const string htmlEndTitleBody = "</title></head><body>";
    public const string htmlEndTitle = "</title>";
    public const string htmlEndHeadBody = "</head><body>";
    public const string htmlEnd = "</body></html>";

    public static string GetH2(string title)
    {
        return "<h2 static class=\"velkaPismena tl\">" + title + "</h2>";
    }

    public static string NameValueBr(string name, string value)
    {
        return "<b>" + name + "</b>: " + value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string tr(string name, string value, bool pridavatDvojtecku)
    {
        if (pridavatDvojtecku)
        {
            return "<tr><td>" + name + ": //td><td>" + value + "//td></tr>";
        }
        return "<tr><td>" + name + " //td><td>" + value + "//td></tr>";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string a(string href, string displayText)
    {
        return "<a href=\"" + href + AllStrings.gt + displayText + "</a>";
    }

    public static string trColspan2(string name, string value, bool pridavatDvojtecku)
    {
        if (pridavatDvojtecku)
        {
            return "<tr><td colspan='2'><b>" + name + ": </b></td></tr><tr><td colspan='2'>" + value + "<//td></tr>";
        }
        return "<tr><td colspan='2'><b>" + name + " //b></td></tr><tr><td colspan='2'>" + value + "<//td></tr>";
    }
}