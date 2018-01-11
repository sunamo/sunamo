using System.Text.RegularExpressions;
public class RegexHelper
{
    public static Regex rHtmlScript = new Regex(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static Regex rHtmlComment = new Regex(@"<!--[^>]*>[\s\S]*?-->", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static Regex rYtVideoLink = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.Compiled);

    public static bool IsEmail(string email)
    {
        Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return r.IsMatch(email);
    }

    static Regex rgColor6 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}$");
    static Regex rgColor8 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}(?:[0-9a-fA-F]){2}$");
    public static bool IsColor(string entry)
    {
        entry = entry.Trim().TrimStart('#');
        if (entry.Length == 6)
        {
            return rgColor6.IsMatch(entry);
        }
        else if (entry.Length == 8)
        {
            return rgColor8.IsMatch(entry);
        }
        return false;
    }

    public static bool IsYtVideoUri(string text)
    {
        return rYtVideoLink.IsMatch(text);
    }

    public static string ReplacePlainUrlWithLinks(string plainText)
    {
        var html = Regex.Replace(plainText, @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+" +
                         @"\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?" +
                         @"([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*$",
                         "<a href=\"$1\">$1</a>");
        return html;
    }
}
