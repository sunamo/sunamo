using System;
using System.Collections.Generic;
using System.Globalization;

public static partial class GeneralConsts
{
    public const int tnWidth = 300;
    public const int tnHeight = 168;
    public const byte maxPocetPokusu = 10;


    public const string passwordSql = "84GS9gaA_z";


    public static readonly DateTime dt111 = new DateTime(1, 1, 1);
    public static readonly CultureInfo CultureInfoCzech = new CultureInfo("cs-CZ");
    /// <summary>
    /// Přesně 1024MB
    /// </summary>
    public static readonly int OneGb = 1073741824;
    /// <summary>
    /// Přesně 512MB
    /// </summary>
    public static int HalfGb = 536870912;
    public const int maxFileCountOnAccount = 10000;
    //"css/Web.css",
    public static readonly List<string> includeStyles = new List<string>(new string[] { "Shared/css/Shared.css", "Content/metro-icons.css", "Content/metro.css" });
    public static readonly List<string> includeScripts = new List<string>(new string[] { "ts/Me/Shared.js", "ts/su.js", "Scripts/jquery.min.js", "wwwroot/lib/requirejs/requirejs.js" });
    //public static readonly List<string> includeScriptsAsync = new List<string>() { };
    public const string FbEventBaseUri = "https://www.facebook.com/events/";

    
}
