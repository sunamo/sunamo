using System.Runtime.CompilerServices;

public static partial class HtmlTemplates
{
    public static string Img(string src, string alt)
    {
        return $"<img src=\"{src}\" alt=\"{alt}\" />";
    }
    //public static string Img(string src)
    //{
    //    return $"<img src=\"{src}\" />";
    //}

public static string HiddenField(string id, string value)
    {
        string format = "<input type='hidden' id='" + id + "' value='" + value + "' />";
        return format;
        //HtmlInjection.InjectInternalToHead(page, format);
    }
}