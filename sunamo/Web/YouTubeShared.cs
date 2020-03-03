using sunamo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

public static partial class YouTube{ 
/// <summary>
    /// G null pokud se YT kód nepodaří získat
    /// </summary>
    /// <param name = "uri"></param>
    public static string ParseYtCode(string uri)
    {
        Regex regex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
        var match = regex.Match(uri);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return null;
    }
}