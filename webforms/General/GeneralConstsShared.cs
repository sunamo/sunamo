using System;
using System.Collections.Generic;
using System.Globalization;

public static partial class GeneralConsts
{
    public const int tnWidth = 300;
    public const int tnHeight = 168;
    public const byte maxPocetPokusu = 10;

    public const string General = "General";
    public const string Me = "Me";
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
    public static readonly List<string> includeStyles = new List<string>(new string[] { StyleSheetPaths.cssShared, StyleSheetPaths.metroIcons, StyleSheetPaths.metro });
    // JavaScriptPaths.RequireJS  - byvavalo, uz nemuze byt protoze by se mi prilonkoval pres c# vstupni js soubor pro stranku
    public static readonly List<string> includeScripts = new List<string>(new string[] { JavaScriptPaths.jQuery  });
    //public static readonly List<string> includeScriptsAsync = new List<string>() { };
    public const string FbEventBaseUri = "https://www.facebook.com/events/";

    
}
