using System;
using System.Collections.Generic;
using System.Text;

public class FiltersNotTranslateAble
{
    public static string NotTranslateAblePp = "NotTranslateAble.cs";
    public static string AssemblyInfo = "AssemblyInfo.cs";
    public static string Layer = "Layer.cs";
    public static string EnigmaData = "EnigmaData.cs";

    /// <summary>
    /// Is good include the most files as is possible due to performamce
    /// </summary>
    public static readonly List<string> ending = CA.ToList<string>(NotTranslateAblePp, AssemblyInfo, Layer);


    /// <summary>
    /// in XLF is not available sess coz is in sunamo
    /// </summary>
    public const string SunamoXlf = "sunamo\\Xlf";
    /// <summary>
    /// All which is WithoutDep cant have Xlf
    /// If yes, I couldn't have Xlf.web and Xlf
    /// </summary>
    public const string WithoutDep = "WithoutDep";
    public const string Credentials = "Credentials";
    public const string Interfaces = "Interfaces";
    public const string Enums = "Enums";
    public const string Attributes = "Attributes";


    public static readonly List<string> contains = CA.ToList<string>( SunamoXlf, WithoutDep, Credentials, Interfaces, Enums);
}