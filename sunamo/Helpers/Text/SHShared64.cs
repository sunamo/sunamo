using Diacritics.Extensions;
using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static partial class SH
{
    private static Type type = typeof(SH);
    public const String diacritic = "\u00E1\u010D\u010F\u00E9\u011B\u00ED\u0148\u00F3\u0161\u0165\u00FA\u016F\u00FD\u0159\u017E\u00C1\u010C\u010E\u00C9\u011A\u00CD\u0147\u00D3\u0160\u0164\u00DA\u016E\u00DD\u0158\u017D";

    public static string Substring(string sql, int indexFrom, int indexTo, bool returnInputIfInputIsShorterThanA3 = false)
    {
        return Substring(sql, indexFrom, indexTo, new SubstringArgs { returnInputIfInputIsShorterThanA3 = returnInputIfInputIsShorterThanA3 });
    }

    /// <summary>
    /// Trim from beginning and end of A1 substring A2
    /// </summary>
    /// <param name="s"></param>
    /// <param name="args"></param>
    public static string Trim(string s, string args)
    {
        s = TrimStart(s, args);
        s = TrimEnd(s, args);

        return s;
    }

    /// <summary>
    /// Remove with A2
    /// </summary>
    /// <param name="t"></param>
    /// <param name="ch"></param>
    public static string RemoveAfterFirst(string t, char ch)
    {
        int dex = t.IndexOf(ch);
        if (dex == -1 || dex == t.Length - 1)
        {
            return t;
        }

        return t.Substring(0, dex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, char v, bool _trimWrapping = false)
    {
        // TODO: Make with StringBuilder, because of SH.WordAfter and so
        return WrapWith(value, v.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, string h, bool _trimWrapping = false)
    {
        return h + SH.Trim(value, h) + h;
    }

    /// <summary>
    /// Remove also A2
    /// Don't trim
    /// </summary>
    /// <param name="t"></param>
    /// <param name="ch"></param>
    public static string RemoveAfterFirst(string t, string ch)
    {
        int dex = t.IndexOf(ch);
        if (dex == -1 || dex == t.Length - 1)
        {
            return t;
        }

        string vr = t.Remove(dex);
        return vr;
    }

    public static bool IsNegation(ref string contains)
    {
        if (contains[0] == AllChars.excl)
        {
            contains = contains.Substring(1);
            return true;
        }
        return false;
    }

    /// <summary>
    /// AnySpaces - split A2 by spaces and A1 must contains all parts
    /// ExactlyName - ==
    /// FixedSpace - simple contains
    /// </summary>
    /// <param name="input"></param>
    /// <param name="term"></param>
    /// <param name="searchStrategy"></param>
    /// <param name="caseSensitive"></param>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy, bool caseSensitive)
    {
        if (term != "")
        {
            if (searchStrategy == SearchStrategy.ExactlyName)
            {
                if (caseSensitive)
                {
                    return input == term;
                }
                else
                {
                    return input.ToLower() == term.ToLower();
                }
            }
            else
            {
                if (searchStrategy == SearchStrategy.FixedSpace)
                {
                    if (caseSensitive)
                    {
                        return input.Contains(term);
                    }
                    else
                    {
                        return input.ToLower().Contains(term.ToLower());
                    }
                }
                else
                {
                    if (caseSensitive)
                    {
                        var allWords = SH.Split(term, AllStrings.space);
                        return SH.ContainsAll(input, allWords);
                    }
                    else
                    {
                        var allWords = SH.Split(term, AllStrings.space);
                        CA.ToLower(allWords);
                        return SH.ContainsAll(input.ToLower(), allWords);
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Return whether A1 contains all from A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="allWords"></param>
    public static bool ContainsAll(string input, IEnumerable<string> allWords, ContainsCompareMethod ccm = ContainsCompareMethod.WholeInput)
    {
        if (ccm == ContainsCompareMethod.SplitToWords)
        {
            foreach (var item in allWords)
            {
                if (!input.Contains(item))
                {
                    return false;
                }
            }
        }
        else if (ccm == ContainsCompareMethod.Negations)
        {
            foreach (var item in allWords)
            {
                var c = item.ToString();
                if (!IsContained(input, ref c))
                {
                    return false;
                }
            }
        }
        else if (ccm == ContainsCompareMethod.WholeInput)
        {
            foreach (var item in allWords)
            {
                if (!input.Contains(item))
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Auto remove potentially first !
    /// </summary>
    /// <param name="item"></param>
    /// <param name="contains"></param>
    public static bool IsContained(string item, ref string contains)
    {
        bool negation = SH.IsNegation(ref contains);

        if (negation && item.Contains(contains))
        {
            return false;
        }
        else if (!negation && !item.Contains(contains))
        {
            return false;
        }

        return true;
    }

    public static bool EqualsOneOfThis(string p1, params string[] p2)
    {
        foreach (string item in p2)
        {
            if (p1 == item)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Convert \r\n to NewLine etc.
    /// 
    /// </summary>
    /// <param name="delimiter"></param>
    public static string ConvertTypedWhitespaceToString(string delimiter)
    {
        const string nl = @"
";

        switch (delimiter)
        {
            // must use \r\n, not Environment.NewLine (is not constant)
            case "\\r\\n":
            case "\\n":
            case "\\r":
                return nl;
            case "\\t":
                return "\t";
        }

        return delimiter;
    }

    public static string Replace(string t, string what, string forWhat)
    {
        return Replace(t, what, forWhat, false);
    }

    /// <summary>
    /// Use simple c# replace
    /// </summary>
    /// <param name="t"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static string Replace(string t, string what, string forWhat, bool a2CanBeAsA3 = false)
    {
        if (what == forWhat)
        {
            if (a2CanBeAsA3)
            {
                return t;
            }
            else
            {
                ThrowExceptions.IsTheSame(Exc.GetStackTrace(), type, Exc.CallingMethod(), "what", "forWhat");
            }
        }
        var r = t.Replace(what, forWhat);
        return r;
    }

   

    public static string GetString(IEnumerable o, string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in o)
        {
            sb.Append(SH.ListToString(item, p) + p);
        }
        return sb.ToString();
    }

    public static char GetFirstChar(string arg)
    {
        return arg[0];
    }

    public static bool IsNumber(string str, params char[] nextAllowedChars)
    {
        foreach (var item in str)
        {
            if (!char.IsNumber(item))
            {
                if (!CA.ContainsElement<char>(nextAllowedChars, item))
                {
                    return false;
                }
            }
        }

        return true;
    }
    public static string PrefixIfNotStartedWith(string item, string http)
    {
        if (!item.StartsWith(http))
        {
            return http + item;
        }
        return item;
    }

    public static string TrimStartAndEnd(string v, string s, string e)
    {
        v = TrimEnd(v, e);
        v = TrimStart(v, s);

        return v;
    }

    /// <summary>
    /// Trim all A2 from beginning A1
    /// </summary>
    /// <param name="v"></param>
    /// <param name="s"></param>
    public static string TrimStart(string v, string s)
    {
        while (v.StartsWith(s))
        {
            v = v.Substring(s.Length);
        }
        return v;
    }

    /// <summary>
    /// Trim all A2 from end A1
    /// Originally named TrimWithEnd
    /// Pokud A1 končí na A2, ořežu A2
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ext"></param>
    public static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext))
        {
            return name.Substring(0, name.Length - ext.Length);
        }
        return name;
    }

    /// <summary>
    /// POZOR, tato metoda se změnila, nyní automaticky přičítá k indexu od 1
    /// When I want to include delimiter, add to A3 +1
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="p"></param>
    /// <param name="p_3"></param>
    public static string Substring(string sql, int indexFrom, int indexTo, SubstringArgs a = null)
    {
        if (a == null)
        {
            a = SubstringArgs.Instance;
        }

        if (sql == null)
        {
            return null;
        }

        int tl = sql.Length;

        if (indexFrom > indexTo)
        {
            if (a.returnInputIfIndexFromIsLessThanIndexTo)
            {
                return sql;
            }
            else
            {
                ThrowExceptions.ArgumentOutOfRangeException(Exc.GetStackTrace(), type, Exc.CallingMethod(), "indexFrom", "indexFrom is lower than indexTo");
            }
        }

        if (tl > indexFrom)
        {
            if (tl > indexTo)
            {
                return sql.Substring(indexFrom, indexTo - indexFrom);
            }
            else
            {
                if (a.returnInputIfInputIsShorterThanA3)
                {
                    return sql;
                }
            }
        }
        // must return string.Empty, not null, because null cant be save to many of columns in db
        return string.Empty;
    }


    /// <summary>
    /// Another method is RemoveDiacritics
    /// G text bez dia A1.
    /// </summary>
    /// <param name="sDiakritik"></param>
    public static string TextWithoutDiacritic(string sDiakritik)
    {
        return sDiakritik.RemoveDiacritics();
        // but also with this don't throw exception but no working Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(sDiakritik));
        //if (!initDiactitic)
        //{
        //    System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        //    Encoding.RegisterProvider(provider);

        //    initDiactitic = true;
        //} 

        //originally was "ISO-8859-8" but not working in .net standard. 1252 is eqvivalent
        //return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(sDiakritik));

        // FormC - followed by replacement of sequences
        // As default using FormC
        //return sDiakritik.Normalize(NormalizationForm.FormC);

        //return RemoveDiacritics(sDiakritik);
    }

    /// <summary>
    /// Remove with char
    /// </summary>
    /// <param name="us"></param>
    /// <param name="nameSolution"></param>
    public static string RemoveAfterLast(object delimiter, string nameSolution)
    {
        int dex = nameSolution.LastIndexOf(delimiter.ToString());
        if (dex != -1)
        {
            string s = SH.Substring(nameSolution, 0, dex, null);
            return s;
        }
        return nameSolution;
    }

    /// <summary>
    /// Add postfix if text not ends with
    /// </summary>
    /// <param name="text"></param>
    /// <param name="postfix"></param>
    /// <returns></returns>
    public static string PostfixIfNotEmpty(string text, string postfix)
    {
        if (text.Length != 0)
        {
            if (!text.EndsWith(postfix))
            {
                return text + postfix;
            }
        }
        return text;
    }
    public static string ReplaceOnce(string input, string what, string zaco)
    {

        if (what == "")
        {
            return input;
        }

        int pos = input.IndexOf(what);
        if (pos == -1)
        {
            return input;
        }
        return input.Substring(0, pos) + zaco + input.Substring(pos + what.Length);
    }

    /// <summary>
    /// Vrátí prázdný řetězec pokud nebude nalezena mezera a A1
    /// 
    /// </summary>
    /// <param name="p"></param>
    public static string GetFirstWord(string p, bool returnEmptyWhenDontHaveLenght = true)
    {
        p = p.Trim();
        int dex = p.IndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(0, dex);
        }

        if (returnEmptyWhenDontHaveLenght)
        {
            return string.Empty;
        }
        return p;
    }

    public static bool ContainsOnly(string floorS, List<char> numericChars)
    {
        if (floorS.Length == 0)
        {
            return false;
        }

        foreach (var item in floorS)
        {
            if (!numericChars.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    public static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
        {
            return nazevPP.ToUpper();
        }
        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
    public static string FirstCharLower(string nazevPP)
    {
        if (nazevPP.Length < 2)
        {
            return nazevPP;
        }

        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToLower() + sb;
    }



    

    /// <summary>
    /// If A1 is string, return A1
    /// If IEnumerable, return joined by comma
    /// For inner collection use CA.TwoDimensionParamsIntoOne
    /// </summary>
    /// <param name="value"></param>
    public static string ListToString(object value, object delimiter = null)
    {
        var delimiterS = AllStrings.comma;
        if (delimiter != null)
        {
            delimiterS = delimiter.ToString();
        }
        if (value == null)
        {
            return Consts.nulled;
        }

        string text;
        var valueType = value.GetType();

        if (value is IEnumerable && valueType != Types.tString && valueType != Types.tStringBuilder && !(value is IEnumerable<char>))
        {
            if (delimiter == null)
            {
                delimiter = Environment.NewLine;
            }
            var enumerable = CA.ToListString(value as IEnumerable);
            // I dont know why is needed replace delimiterS(,) for space
            // This setting remove , before RoutedEventArgs etc.
            //CA.Replace(enumerable, delimiterS, AllStrings.space);
            text = SH.Join(delimiter, enumerable);
        }
        else if (valueType == Types.tDateTime)
        {
            text = DTHelperEn.ToString((DateTime)value);
        }
        else
        {
            text = value.ToString();
        }

        return text;
    }

    /// <summary>
    /// This can be only one 
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    public static string JoinIEnumerable(object delimiter, IEnumerable parts)
    {
        // TODO: Delete after all app working
        return JoinString(delimiter, parts);
    }//

    /// <summary>
    /// Not auto remove empty
    /// </summary>
    /// <param name="p"></param>
    public static List<string> GetLines(string p)
    {
        List<string> vr = new List<string>();
        StringReader sr = new StringReader(p);
        string f = null;
        while ((f = sr.ReadLine()) != null)
        {
            vr.Add(f);
        }
        return vr;
    }

    /// <summary>
    /// Will be delete after final refactoring
    /// Automaticky ořeže poslední A1
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    public static string JoinString(object delimiter, IEnumerable parts)
    {
        // TODO: Delete after all app working, has here method Join with same arguments
        return Join(delimiter, parts);
    }

    /// <summary>
    /// With these 
    /// </summary>
    /// <param name="stringSplitOptions"></param>
    /// <param name="text"></param>
    /// <param name="deli"></param>
    private static List<string> Split(StringSplitOptions stringSplitOptions, string text, params object[] deli)
    {
        if (deli == null || deli.Count() == 0)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), SunamoPageHelperSunamo.i18n(XlfKeys.NoDelimiterDetermined));
        }
        var ie = CA.OneElementCollectionToMulti(deli);
        var deli3 = CA.ToListString(ie);
        var result = text.Split(deli3.ToArray(), stringSplitOptions).ToList();
        CA.Trim(result);
        if (stringSplitOptions == StringSplitOptions.RemoveEmptyEntries)
        {
            CA.RemoveStringsEmpty(result);
        }

        return result;
    }

    
    public static List<string> Split(string parametry, params object[] deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry, deli);
    }

    public static List<string> SplitNone(string text, params object[] deli)
    {
        return Split(StringSplitOptions.None, text, deli);
    }

    public static List<string> SplitByWhiteSpaces(string s, bool removeEmpty = false)
    {
        List<string> r = null;
        if (removeEmpty)
        {
            r = Split(s, AllChars.whiteSpacesChars.ToArray()).ToList();
        }
        else
        {
            r = SplitNone(s, AllChars.whiteSpacesChars.ToArray()).ToList();
        }

        return r;
    }

    public static string JoinNL(IEnumerable parts, bool removeLastNl = false)
    {
         string nl = Environment.NewLine;
        var result = SH.JoinString(nl, parts);
        if (removeLastNl)
        {
            result = SH.TrimEnd(result, nl);
        }
        return result;
    }

    /// <summary>
    /// Will be delete after final refactoring
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    //private static string Join(IEnumerable parts, object delimiter)
    //{
    //    if (delimiter is string)
    //    {
    //        return Join(delimiter, parts);
    //    }
    //    // TODO: Delete after all app working, has flipped A1 and A2
    //    return Join(delimiter, parts);
    //}

    /// <summary>
    /// A2 Must be string due to The call is ambiguous between the following methods or properties: 'SH.Join(object, IEnumerable)' and 'SH.Join(IEnumerable, object)'
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    public static string Join(string delimiter, IEnumerable parts)
    {
        string s = delimiter.ToString();
        StringBuilder sb = new StringBuilder();
        if (parts.Count() == 1 && parts.FirstOrNull().GetType() == Types.tString)
        {
            sb.Append(SH.ListToString(parts.FirstOrNull()) + s);
        }
        else if (parts.GetType() == Types.tString)
        {
            return parts.ToString();
        }
        else
        {
            foreach (var item in parts)
            {
                sb.Append(SH.ListToString(item) + s);
            }
        }

        string d = sb.ToString();
        //return d.Remove(d.Length - (name.Length - 1), name.Length);
        int to = d.Length - s.Length;
        if (to > 0)
        {
            return d.Substring(0, to);
        }
        return d;
        //return d;
    }

    /// <summary>
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    public static string Join(object delimiter, params object[] parts)
    {
        var enu = CA.ToEnumerable(parts);
        if (delimiter is IEnumerable && delimiter.GetType() != Types.tString)
        {
            var ie = (IEnumerable)delimiter;

            if (ie.Count() > 1 && enu.Count() == 1)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), SunamoPageHelperSunamo.i18n(XlfKeys.ProbablyWasCalledWithSwithechDelimiterAndParts));
            }
        }

        // JoinString point to Join with implementation
        return Join(delimiter.ToString(), enu);
    }


    /// <summary>
    /// If null, return Consts.nulled
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static string NullToStringOrDefault(object n)
    {
        return NullToStringOrDefault(n, null);
    }

    /// <summary>
    /// If null, return Consts.nulled
    /// </summary>
    /// <param name="n"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string NullToStringOrDefault(object n, string v)
    {
        if (v == null)
        {
            if (n == null)
            {
                v = Consts.nulled;
            }
            else
            {
                v = n.ToString();
            }
        }
        if (n != null)
        {
            return AllStrings.space + v;
        }
        return " " + Consts.nulled;
    }


    /// <summary>
    /// Start at 0
    /// </summary>
    /// <param name="input"></param>
    /// <param name="lenght"></param>
    /// <returns></returns>
    public static string SubstringIfAvailable(string input, int lenght)
    {
        if (input.Length > lenght)
        {
            return input.Substring(0, lenght);
        }
        return input;
    }

    public static string FirstLine(string item)
    {
        var lines = SH.GetLines(item);
        if (lines.Count == 0)
        {
            return string.Empty;
        }
        return lines[0];
    }

    public static string JoinPairs(params object[] parts)
    {
        return JoinPairs(AllStrings.sc, AllStrings.cs, parts);
    }
    public static string JoinPairs(string firstDelimiter, string secondDelimiter, params object[] parts)
    {
        InitApp.TemplateLogger.NotEvenNumberOfElements(type, "JoinPairs", @"args", parts);
        InitApp.TemplateLogger.AnyElementIsNull(type, "JoinPairs", @"args", parts);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < parts.Length; i++)
        {
            sb.Append(parts[i++] + firstDelimiter);
            sb.Append(parts[i] + secondDelimiter);
        }
        return sb.ToString();
    }


    /// <summary>
    /// If you want to replace multiline content with various indent use SH.ReplaceAllDoubleSpaceToSingle2 to every variable which you are passed
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    public static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {
        //Stupid, zaCo can be null

        //if (string.IsNullOrEmpty(zaCo))
        //{
        //    return vstup;
        //}

        foreach (var item in co)
        {
            if (string.IsNullOrEmpty(item))
            {
                return vstup;
            }
        }

        foreach (var item in co)
        {
            vstup = vstup.Replace(item, zaCo);
        }
        return vstup;
    }

    public static bool TrimIfStartsWith(ref string s, string p)
    {
        if (s.StartsWith(p))
        {
            s = s.Substring(p.Length);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    /// Format2 - use string.Format with error checking
    /// Format3 - Replace {x} with my code. Can be used with wildcard
    /// Format4 - use string.Format without error checking
    /// 
    /// Try to use in minimum!! Better use Format3 which dont raise "Input string was in wrong format"
    /// 
    /// Simply return from string.Format. SH.Format is more intelligent
    /// If input has curly bracket but isnt in right format, return A1. Otherwise apply string.Format. 
    /// SH.Format2 return string.Format always
    /// Wont working if contains {0} and another non-format replacement. For this case of use is there Format3
    /// </summary>
    /// <param name="template"></param>
    /// <param name="args"></param>
    public static string Format2(string status, params object[] args)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return string.Empty;
        }
        if (status.Contains(AllChars.lcub) && !status.Contains("{0}"))
        {
            return status;
        }
        else
        {
            try
            {
                return string.Format(status, args);
            }
            catch (Exception ex)
            {
                return status;
            }
        }
    }
}
