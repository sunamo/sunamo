using System;
using System.Collections.Generic;
using System.Text;


public class AllStrings
{
    /// <summary>
    /// Question mark
    /// </summary>
    public const string q = "?";
    /// <summary>
    /// double dots
    /// </summary>
    public const string dd = "..";
    /// <summary>
    /// double dots slash
    /// </summary>
    public const string dds = "../";
    public const string ds = "./";
    public const string slashAsterisk = "/*";
    public const string apostrophe = "'";

    public const string lt = "<";
    public const string gt = ">";


    /// <summary>
    /// left square fowl [
    /// </summary>
    public const string lsf = "[";
    /// <summary>
    /// right square fowl ]
    /// </summary>
    public const string rsf = "]";

    public const string pipe = "|";

    public const string cbl = "{";
    public const string cbr = "}";

    public const string space = " ";
    public const string dash = "-";
    public const string colon = ":";
    public const string us = "_";

    /// <summary>
    /// ORDINAL BRACKET
    /// </summary>
    public const string lb = "(";
    public const string rb = ")";

    public const string tab = "\t";
    public const string nl = "\n";
    public const string cr = "\r";
    public const string bs = "\\";
    public const string slash = "/";

    public const string comma = ",";
    public const string dot = ".";
    public const string asterisk = "*";


    /// <summary>
    /// semicolon
    /// </summary>
    public const string sc = ";";
    public const string qm = "\"";
    /// <summary>
    /// space wrapped dash
    /// </summary>
    public const string swda = " - ";
    public const string doubleSpace = "  ";
    /// <summary>
    /// " - "
    /// </summary>
    public static string swd = " - ";
    /// <summary>
    /// comma space
    /// </summary>
    public static string cs = ", ";
    /// <summary>
    /// colon space
    /// </summary>
    public static string cs2 = ": ";
    public static string plus;
    public static string doubleSpace32160 = AllStrings.space + AllStrings.space160;
    public static string doubleSpace16032 = AllStrings.space160 + AllStrings.space;
    public static string space160 = AllChars.space160.ToString();
    public const string bs2 = "\b";
    public const string hashtag = "#";
    public const string equal = "=";
    public const string ampersand = "&";

    public const string lq = "“";
    public const string rq = "”";
    public const string at = "@";
    public const string la = "‘";
    public const string ra = "’";

    internal static string DoubleSpace32160 { get => doubleSpace32160; set => doubleSpace32160 = value; }
}
