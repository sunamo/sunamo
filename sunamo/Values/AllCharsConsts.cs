using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class AllChars
{
    #region Generated with SunamoFramework.HtmlEntitiesForNonDigitsOrLetterChars
    public const char dollar = '$';
    public const char Hat = '^';
    public const char ast = '*';
    public const char quest = '?';
    public const char tilda = '~';

    public const char comma = ',';
    public const char period = '.';
    public const char colon = ':';
    public const char excl = '!';
    public const char apos = '\'';
    public const char rpar = ')';
    public const char lpar = '(';
    public const char sol = '/';
    public const char lowbar = '_';
    public const char lt = '<';
    /// <summary>
    /// skip in specialChars2 - already as equal
    /// </summary>
    public const char equals = '=';
    public const char gt = '>';
    public const char amp = '&';
    public const char lcub = '{';
    public const char rcub = '}';
    public const char lsqb = '[';
    public const char verbar = '|';
    public const char semi = ';';
    public const char commat = '@';
    public const char plus = '+';
    public const char rsqb = ']';
    public const char num = '#';
    public const char percnt = '%';
    public const char ndash = '–';
    public const char copy = '©';
    #endregion

    public static char space160 = (char)160;

    public const char la = '‘';
    public const char ra = '’';
    public const char st = '\0';

    #region MyRegion
    public const char lq = '“';
    public const char rq = '”';

    #region Generic chars
    public static readonly char notNumber;
    public const char zero = '0';
    #endregion

    #region Names here must be the same as in Consts
    public const char modulo = '%';
    /// <summary>
    /// char code 32
    /// </summary>
    public const char space = ' ';
    /// <summary>
    /// Look similar as 32 space
    /// </summary>
    public const char nbsp = (char)160;
    public const char dash = '-';

    #endregion

    public const char tab = '\t';
    public const char nl = '\n';
    public const char cr = '\r';
    public const char bs = '\\';
    public const char dot = '.';
    public const char asterisk = '*';
    public const char apostrophe = '\'';

    public const char sc = ';';
    /// <summary>
    /// quotation marks
    /// </summary>
    public const char qm = '"';

    /// <summary>
    /// Question
    /// </summary>
    public const char q = '?';
    /// <summary>
    /// Left bracket
    /// </summary>
    public const char lb = '(';
    public const char rb = ')';
    public const char slash = '/';
    /// <summary>
    /// backspace
    /// </summary>
    public const char bs2 = '\b';
    #endregion
    public static readonly List<char> specialChars = new List<char>(new char[] { excl, commat, num, dollar, percnt, Hat, amp, ast, quest, lowbar, tilda });
    /// <summary>
    /// 2020-07-4 added slash
    /// </summary>
    public static readonly List<char> specialChars2 = new List<char>(new char[] {  lq, rq, dash, la, ra,
    comma, period, colon, apos, rpar, sol, lt, gt, lcub, rcub, lsqb, verbar, semi, plus, rsqb,
        ndash, slash });
}
