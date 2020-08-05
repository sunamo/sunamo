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


    public static void Mail(HtmlGenerator sb)
    {
        
        sb.WriteTagWithAttrs("a", "href", "mailto:radek.jancik@sunamo.cz");
        sb.WriteRaw("radek.jancik@sunamo.cz");
        sb.TerminateTag("a");
    }

public static string HiddenField(string id, string value)
    {
        string format = "<input type='hidden' id='" + id + "' value='" + value + "' />";
        return format;
        //HtmlInjection.InjectInternalToHead(page, format);
    }
}