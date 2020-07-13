using System;
using System.Collections.Generic;
using System.Text;

public class FiltersNotTranslateAble
{
    private FiltersNotTranslateAble()
    {
        ending = CA.ToList<string>(NotTranslateAblePp, AssemblyInfo, Layer);
        contains = CA.ToList<string>(SunamoXlf, WithoutDep, Credentials, Interfaces, Enums);
    }

    public static FiltersNotTranslateAble Instance = new FiltersNotTranslateAble();
    public  string NotTranslateAblePp = "NotTranslateAble.cs";
    public  string AssemblyInfo = "AssemblyInfo.cs";
    public  string Layer = "Layer.cs";
    public  string EnigmaData = "EnigmaData.cs";

    /// <summary>
    /// Is good include the most files as is possible due to performamce
    /// </summary>
    public readonly List<string> ending = null;
    /// <summary>
    /// in XLF is not available sess coz is in sunamo
    /// </summary>
    public  string SunamoXlf = "sunamo\\Xlf";
    /// <summary>
    /// All which is WithoutDep cant have Xlf
    /// If yes, I couldn't have Xlf.web and Xlf
    /// </summary>
    public  string WithoutDep = "WithoutDep";
    public  string Credentials = "Credentials";
    public  string Interfaces = "Interfaces";
    public  string Enums = "Enums";
    public  string Attributes = "Attributes";

    public readonly List<string> contains = null;
}