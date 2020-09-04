
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/// <summary>
/// Most NotTranslateAble class due to many regex and duplicated \
/// </summary>
public static class RegexHelper
{
    public static Regex rHtmlScript = new Regex(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static Regex rHtmlComment = new Regex(@"<!--[^>]*>[\s\S]*?-->", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static Regex rYtVideoLink = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.Compiled);
    public static Regex rBrTagCaseInsensitive = new Regex(@"<br\s*/?>");

    public static bool IsEmail(string email)
    {
        Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return r.IsMatch(email);
    }

    public static Regex rUri = new Regex(@"(https?://[^\s]+)");
    //static Regex rUriOnlyOutsideTags = new Regex("https?:\/\/[^\s]*|<\/?\w+\b(?=\s|>)(?:='[^']*'|="[^ "]*" |=[^ '"][^\s>]*|[^>])*>|\&nbsp;John|(John)/gi");
    //static Regex rUriOnlyOutsideTags = new Regex("(text|simple)(?![^<]*>|[^<>]*</)");
    // cant compile
    //static Regex rHtmlTag = new Regex(@"(?<==)["']?((?:.(?!["']?\\s+(?:\S+)=|[>"']))+.)["']?");
    public static Regex rHtmlTag = new Regex("<\\s*([A-Za-z])*?[^>]*/?>");

    public static Regex rgColor6 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}$");
    public static Regex rgColor8 = new Regex(@"^(?:[0-9a-fA-F]{3}){1,2}(?:[0-9a-fA-F]){2}$");
    public static Regex rPreTagWithContent = new Regex(@"<\s*pre[^>]*>(.*?)<\s*/\s*pre>", RegexOptions.Multiline);
    public static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
    public static Regex rImgTag = new Regex(@"<img\s+([^>]*)(.*?)[^>]*>");
    public static Regex rWpImgThumbnail = new Regex(@"(https?:\/\/([^\s]+)-([0-9]*)x([0-9]*).jpg)");
    public static Regex rNonPairXmlTagsUnvalid = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");

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

    /// <summary>
    /// Dont use, parse uri with regex is total naive . Use DOM parser
    /// Not working - keep in plain text, use ReplacePlainUrlWithLinks2
    /// </summary>
    /// <param name="plainText"></param>
    public static string ReplacePlainUrlWithLinks(string plainText)
    {
        var html = Regex.Replace(plainText, @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+" +
                         @"\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?" +
                         @"([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*$",
                         "<a href=\"$1\">$1</a>");
        return html;
    }

    public static bool IsUri(string text)
    {
        return rUri.IsMatch(text) && UH.HasHttpProtocol(text);
    }

    public static List<string> AllFromGroup(MatchCollection m, int v)
    {
        List<string> vr = new List<string>(m.Count);
        foreach (Match item in m)
        {
            vr.Add(item.Groups[v].Value);
        }
        return vr;
    }

    static RegexHelper()
    {
        // this one I unfortunately cant use because I use .net core 2.0, available from 2.1
        // from https://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx/ and https://gist.github.com/Haacked/7729259
        ////HtmlTagRegex.
        //RegexCompilationInfo[] compInfo =
        //{
        //        //HtmlTag Regex.
        //        new RegexCompilationInfo
        //        (
        //            @AllStrings.lt
        //            +    @"(?<endTag>/)?"    //Captures the / if this is an end tag.
        //            +    @"(?<tagname>\w+)"    //Captures TagName
        //            +    @AllStrings.lb                //Groups tag contents
        //            +        @"(\s+"            //Groups attributes
        //            +            @"(?<attName>\w+)"  //Attribute name
        //            +            @AllStrings.lb                //groups =value portion.
        //            +                @"\s*=\s*"            // = 
        //            +                @"(?:"        //Groups attribute "value" portion.
        //            +                    @"""(?<attVal>[^""]*)"""    // attVal='double quoted'
        //            +                    @"|'(?<attVal>[^']*)'"        // attVal='single quoted'
        //            +                    @"|(?<attVal>[^'"">\s]+)"    // attVal=urlnospaces
        //            +                @AllStrings.rb
        //            +            @")?"        //end optional att value portion.
        //            +        @")+\s*"        //One or more attribute pairs
        //            +        @"|\s*"            //Some white space.
        //            +    @AllStrings.rb
        //            + @"(?<completeTag>/)?>" //Captures the AllStrings.slash if this is a complete tag.
        //            , RegexOptions.IgnoreCase
        //            , "HtmlTagRegex"
        //            , "Haack.RegularExpressions"
        //            , true
        //        )
        //        ,
        //        // Matches double words.
        //        new RegexCompilationInfo
        //        (
        //            @"\b(\w+)\s+\1\b"
        //            , RegexOptions.None
        //            , "DoubleWordRegex"
        //            , "Haack.RegularExpressions", true
        //        )
        //    };
        //AssemblyName assemblyName = new AssemblyName();
        //assemblyName.Name = "Haack.RegularExpressions";
        //assemblyName.Version = new Version("1.0.0.0");
        //Regex.CompileToAssembly(compInfo, assemblyName);
    }
}