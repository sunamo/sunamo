using sunamo;
using sunamo.Enums;
using System.Diagnostics;
using System.Globalization;
using sunamo.Constants;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Diacritics.Extensions;

public static partial class SH
{
    public const String diacritic = "áčďéěíňóšťúůýřžÁČĎÉĚÍŇÓŠŤÚŮÝŘŽ";
    static Type type = typeof(SH);

    public static string NullToStringOrDefault(object n)
    {
        return NullToStringOrDefault(n, null);
    }

    public static string NullToStringOrDefault(object n, string v)
    {
        if (v == null)
        {
            if (n == null)
            {
                v = "(null)";
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
        return " " + "(null)";
    }

    public static string MakeUpToXChars(int p, int p_2)
    {
        StringBuilder sb = new StringBuilder();
        string d = p.ToString();
        int doplnit = (p.ToString().Length - p_2) * -1;
        for (int i = 0; i < doplnit; i++)
        {
            sb.Append(0);
        }
        sb.Append(d);

        return sb.ToString();
    }

    /// <summary>
    /// This can be only one 
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string JoinIEnumerable(object delimiter, IEnumerable parts)
    {
        // TODO: Delete after all app working
        return JoinString(delimiter, parts);
    }

    /// <summary>
    /// Get null if count of getted parts was under A2.
    /// Automatically add empty padding items at end if got lower than A2
    /// Automatically join overloaded items to last by A2.
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <returns></returns>
    public static List<string> SplitToParts(string what, int parts, string deli)
    {
        var s = SH.Split(what, deli);
        if (s.Count < parts)
        {
            // Pokud je pocet ziskanych partu mensi, vlozim do zbytku prazdne retezce
            if (s.Count > 0)
            {
                List<string> vr2 = new List<string>();
                for (int i = 0; i < parts; i++)
                {
                    if (i < s.Count)
                    {
                        vr2.Add(s[i]);
                    }
                    else
                    {
                        vr2.Add("");
                    }
                }
                return vr2;
                //return new string[] { s[0] };
            }
            else
            {
                return null;
            }
        }
        else if (s.Count == parts)
        {
            // Pokud pocet ziskanych partu souhlasim presne, vratim jak je
            return s;
        }

        // Pokud je pocet ziskanych partu vetsi nez kolik ma byt, pripojim ty co josu navic do zbytku
        parts--;
        List<string> vr = new List<string>();
        for (int i = 0; i < s.Count; i++)
        {
            if (i < parts)
            {
                vr.Add(s[i]);
            }
            else if (i == parts)
            {
                vr.Add(s[i] + deli);
            }
            else if (i != s.Count - 1)
            {
                vr[parts] += s[i] + deli;
            }
            else
            {
                vr[parts] += s[i];
            }
        }
        return vr;
    }

    /// <summary>
    /// Will be delete after final refactoring
    /// Automaticky ořeže poslední A1
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string JoinString(object delimiter, IEnumerable parts)
    {
        // TODO: Delete after all app working, has here method Join with same arguments
        return Join(delimiter, parts);
    }

    #region Replace methods
    public static StringBuilder ReplaceAllSb(StringBuilder sb, string zaCo, params string[] co)
    {
        foreach (var item in co)
        {
            if (item == zaCo)
            {
                continue;
            }
            sb = sb.Replace(item, zaCo);

        }

        return sb;
    }

    public static string Replace(string t, string what, string forWhat)
    {
        return t.Replace(what, forWhat);
    }

    public static string ReplaceAll2(string vstup, string zaCo, string co, bool pairLines)
    {
        if (pairLines)
        {
            var from2 = SH.Split(co, Environment.NewLine);
            var to2 = SH.Split(zaCo, Environment.NewLine);
            ThrowExceptions.DifferentCountInLists(type, "ReplaceInAllFiles", "from2", from2, "to2", to2);

            return ReplaceAll2(vstup, co, zaCo);
        }
        else
        {
            return ReplaceAll2(vstup, zaCo, co);
        }
    }

    /// <summary>
    /// Stejná jako metoda ReplaceAll, ale bere si do A3 pouze jediný parametr, nikoliv jejich pole
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static string ReplaceAll2(string vstup, string zaCo, string co)
    {
        return vstup.Replace( co, zaCo);
    }

    public static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {
        if (string.IsNullOrEmpty(zaCo))
        {
            return vstup;
        }

        foreach (var item in co)
        {
            if (string.IsNullOrEmpty(item))
            {
                return vstup;
            }
        }

        foreach (var item in co)
        {
            vstup = vstup.Replace( item, zaCo);
        }
        return vstup;
    } 
    #endregion

    public static bool IsValidISO(string input)
    {
        // ISO-8859-1 je to samé jako latin1 https://en.wikipedia.org/wiki/ISO/IEC_8859-1
        byte[] bytes = Encoding.GetEncoding("ISO-8859" + "-" + "").GetBytes(input);
        String result = Encoding.GetEncoding("ISO-8859" + "-" + "").GetString(bytes);
        return String.Equals(input, result);
    }

    public static string ReplaceByIndex(string s, string zaCo, int v, int length)
    {
        s = s.Remove(v, length);
        if (zaCo != string.Empty)
        {
            s = s.Insert(v, zaCo);
        }

        return s;
    }

    
    

    public static string NormalizeString(string s)
    {
        if (s.Contains(AllChars.nbsp))
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in s)
            {
                if (item == AllChars.nbsp)
                {
                    sb.Append(AllChars.space);
                }
                else
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        return s;
    }

    

    public static List<int> ReturnOccurencesOfString(string vcem, string co)
    {
        vcem = NormalizeString(vcem);
        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            var subs = vcem.Substring(Index, co.Length);
            //DebugLogger.Instance.WriteLine(subs);
            // non-breaking space. &nbsp; code 160
            // 32 space
            char ch = subs[0];
            char ch2 = co[0];
            if (subs == AllStrings.space)
            {

            }
            if (subs == co)
                Results.Add(Index);
        }
        return Results;
    }

    /// <summary>
    /// A2 Must be string due to The call is ambiguous between the following methods or properties: 'SH.Join(object, IEnumerable)' and 'SH.Join(IEnumerable, object)'
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string Join(string delimiter, IEnumerable parts)
    {
        string s = delimiter.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (var item in parts)
        {
            
            sb.Append(SH.ListToString(item) + s);
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

    private static bool IsInFirstXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (int i = 0; i < pl; i++)
        {
            foreach (var item in letters)
            {
                if (p[i] == item)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static string ShortForLettersCount(string p, int p_2, out bool pridatTriTecky)
    {

        pridatTriTecky = false;
        // Vše tu funguje výborně
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (SH.IsInFirstXCharsTheseLetters(p, p_2, AllChars.space))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;

                int napocitano = 0;
                for (int i = 0; i < to; i++)
                {

                    napocitano++;

                    if (d[i] == AllChars.space)
                    {
                        if (napocitano >= p_2)
                        {
                            break;
                        }

                        dexMezery = i;
                    }
                }
                d = d.Substring(0, dexMezery + 1);
                if (d.Trim() != "")
                {
                    pridatTriTecky = true;
                    //d = d ;
                }
                return d;
                //}
            }
            else
            {
                pridatTriTecky = true;
                return p.Substring(0, p_2);
            }
        }

        return p;
    }

    public static bool ContainsOnlyCase(string between, bool upper)
    {
        foreach (var item in between)
        {
            if (upper)
            {
                if (!char.IsUpper(item))
                {
                    return false;
                }
            }
            else
            {
                if (!char.IsLower(item))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string Join(object delimiter, params object[] parts)
    {
        // JoinString point to Join with implementation
        return Join(delimiter.ToString(), CA.ToEnumerable(parts));
    }

    public static string ShortForLettersCount(string p, int p_2)
    {
        bool pridatTriTecky = false;
        return ShortForLettersCount(p, p_2, out pridatTriTecky);
    }

    public static string ReplaceOnce(string input, string what, string zaco)
    {

        if (input.Contains("na rozdíl od" + " " + ""))
        {

        }

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

    static bool initDiactitic = false;

    /// <summary>
    /// Another method is RemoveDiacritics
    /// G text bez dia A1.
    /// </summary>
    /// <param name="sDiakritik"></param>
    /// <returns></returns>
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
    /// Insert prefix starting with + 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string TelephonePrefixToBrackets(string v)
    {
        if (string.IsNullOrWhiteSpace(v))
        {
            return string.Empty;
        }
        v = NormalizeString(v);
        var p = SH.Split(v, AllStrings.space);
        p[0] = AllStrings.lb + p[0] + AllStrings.rb;
        return SH.Join(p, AllStrings.space);
    }

    /// <summary>
    /// Cannot be use on existing code - will corrupt them
    /// </summary>
    /// <param name="templateHandler"></param>
    /// <param name="lsf"></param>
    /// <param name="rsf"></param>
    /// <param name="id"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Format(string templateHandler, string lsf, string rsf, params string[] args)
    {
        var result = SH.Format2(templateHandler, args);
        const string replacement = "{        }";
        result = SH.ReplaceAll2(result, replacement, "[]");
        result = SH.ReplaceAll2(result, AllStrings.cbl, lsf);
        result = SH.ReplaceAll2(result, AllStrings.cbr, rsf);
        result = SH.ReplaceAll2(result, replacement, "{}");
        return result;
    }

    



/// <summary>
    /// Return which a3 is contained in A1. if a2 and A3 contains only 1 element, check for contains these first element
    /// If contains more elements, wasnts check
    /// Return elements from A3 which is contained
    /// </summary>
    /// <param name="item"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    public static List<string> ContainsAny(string item, bool checkInCaseOnlyOneString, IEnumerable<string> contains)
    {
        List<string>  founded = new List<string>();

        bool hasLine = false;
        if (contains.Count() == 1 && checkInCaseOnlyOneString)
        {
                hasLine = item.Contains(contains.ToList()[0]);
        }
        else
        {
            foreach (var c in contains)
            {
                if (item.Contains(c))
                {
                    hasLine = true;
                    founded.Add(c);
                }
            }
        }

        return founded;
    }

    public static string JoinNL(IEnumerable parts)
    {
        return SH.JoinString(Environment.NewLine, parts);
    }
public static string JoinNL(params string[] parts)
    {
        return SH.JoinString(Environment.NewLine, parts);
    }

public static bool ContainsVariable( string innerHtml)
    {
        return ContainsVariable(AllChars.cbl, AllChars.cbr, innerHtml);
    }
public static bool ContainsVariable(char p, char k, string innerHtml)
    {
        if (string.IsNullOrEmpty(innerHtml))
        {
            return false;
        }
        StringBuilder sbNepridano = new StringBuilder();
        StringBuilder sbPridano = new StringBuilder();
        bool inVariable = false;

        foreach (var item in innerHtml)
        {
            if (item == p)
            {
                inVariable = true;
                continue;
            }
            else if (item == k)
            {
                if (inVariable)
                {
                    inVariable = false;
                }
                int nt = 0;
                if (int.TryParse(sbNepridano.ToString(), out nt))
                {
                    return true;
                }
                else
                {
                    sbPridano.Append(p + sbNepridano.ToString() + k);
                }
                sbNepridano.Clear();
            }
            else if (inVariable)
            {
                sbNepridano.Append(item);
            }
            else
            {
                sbPridano.Append(item);
            }
        }
        return false;
    }

    

    public static string ReplaceVariables(string innerHtml, List<String[]> _dataBinding, int actualRow)
    {
        return ReplaceVariables(AllChars.cbl, AllChars.cbr, innerHtml, _dataBinding, actualRow);
    }
public static string ReplaceVariables(char p, char k, string innerHtml, List<String[]> _dataBinding, int actualRow)
    {
        StringBuilder sbNepridano = new StringBuilder();
        StringBuilder sbPridano = new StringBuilder();
        bool inVariable = false;

        foreach (var item in innerHtml)
        {
            if (item == p)
            {
                inVariable = true;
                continue;
            }
            else if (item == k)
            {
                if (inVariable)
                {
                    inVariable = false;
                }
                int nt = 0;
                if (int.TryParse(sbNepridano.ToString(), out nt))
                {
                    
                    // Zde přidat nahrazenou proměnnou
                    string v = _dataBinding[nt][actualRow];
                    sbPridano.Append(v);
                }
                else
                {
                    sbPridano.Append(p + sbNepridano.ToString() + k);
                }
                sbNepridano.Clear();
            }
            else if (inVariable)
            {
                sbNepridano.Append(item);
            }
            else
            {
                sbPridano.Append(item);
            }
        }
        return sbPridano.ToString();
    }



public static List<int> GetVariablesInString(string innerHtml)
    {
        return GetVariablesInString(AllChars.cbl, AllChars.cbr, innerHtml);
    }
/// <summary>
    /// 
    /// </summary>
    /// <param name="ret"></param>
    /// <param name="pocetDo"></param>
    /// <returns></returns>
    public static List<int> GetVariablesInString(char p, char k, string innerHtml)
    {
        /// Vrátí mi formáty, které jsou v A1 od 0 do A2-1
        /// A1={0} {2} {3} A2=3 G=0,2

        List<int> vr = new List<int>();
        StringBuilder sbNepridano = new StringBuilder();
        //StringBuilder sbPridano = new StringBuilder();
        bool inVariable = false;

        foreach (var item in innerHtml)
        {
            if (item == p)
            {
                inVariable = true;
                continue;
            }
            else if (item == k)
            {
                if (inVariable)
                {
                    inVariable = false;
                }
                int nt = 0;
                if (int.TryParse(sbNepridano.ToString(), out nt))
                {
                    vr.Add(nt);
                }
                
                sbNepridano.Clear();
            }
            else if (inVariable)
            {
                sbNepridano.Append(item);
            }
        }
        return vr;
    }

/// <summary>
    /// Really return list, for string join value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static List<string> RemoveDuplicates(string input, string delimiter)
    {
        var split = SH.Split(input, delimiter);
        return CA.RemoveDuplicitiesList(new List<string>(split));
    }

/// <summary>
    /// G zda je jedinz znak v A1 s dia.
    /// </summary>
    /// <returns></returns>
    public static bool ContainsDiacritic(string slovo)
    {
        return slovo != SH.TextWithoutDiacritic(slovo);
    }

    public static List<string> Split(string parametry, params object[] deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry, deli);
    }

    public static List<string> SplitNone(string text, params object[] deli)
    {
        return Split(StringSplitOptions.None, text, deli);
    }

    static bool cs = false;
    /// <summary>
    /// With these 
    /// </summary>
    /// <param name="stringSplitOptions"></param>
    /// <param name="text"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    private static List<string> Split(StringSplitOptions stringSplitOptions, string text, params object[] deli)
    {
        if (deli == null || deli.Count() == 0)
        {
            if (cs)
            {
                throw new Exception("Nebyl specifikován delimiter");
            }
            else
            {
                throw new Exception("No delimiter determined");
            }
        }


        var deli2 = CA.ToListString(deli).ToArray();
        var result = text.Split(deli2, stringSplitOptions).ToList();
        CA.RemoveStringsEmpty(result);
        return result;
    }


    /// <summary>
    /// Simply return from string.Format. SH.Format is more intelligent
    /// If input has curly bracket but isnt in right format, return A1. Otherwise apply string.Format. 
    /// SH.Format2 return string.Format always
    /// Wont working if contains {0} and another non-format replacement. For this case of use is there Format3
    /// </summary>
    /// <param name="template"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Format2(string status, params object[] args)
    {
        if (status.Contains(AllChars.cbl) && !status.Contains("{0}"))
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

    /// <summary>
    /// Manually replace every {i} 
    /// </summary>
    /// <param name="template"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Format3(string template, params object[] args)
    {
        // this was original implementation but dont know why isnt used string.format
        for (int i = 0; i < args.Length; i++)
        {
            template = SH.ReplaceAll2(template, args[i].ToString(), AllStrings.cbl + i + AllStrings.cbr);
        }
        return template;
    }

    public static string FirstCharLower(string nazevPP)
    {
        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToLower() + sb;
    }

public static string JoinPairs(params object[] parts)
    {
        return JoinPairs(AllStrings.sc, AllStrings.cs, parts);
    }
public static string JoinPairs(string firstDelimiter, string secondDelimiter, params object[] parts)
    {
        InitApp.TemplateLogger.NotEvenNumberOfElements(type, "JoinPairs", "args", parts);
        InitApp.TemplateLogger.AnyElementIsNull(type, "JoinPairs", "args", parts);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < parts.Length; i++)
        {
            sb.Append(parts[i++] + firstDelimiter);
            sb.Append(parts[i] + secondDelimiter);
        }
        return sb.ToString();
    }

    /// <summary>
    /// If A1 is string, return A1
    /// If IEnumerable, return joined by comma
    /// For inner collection use CA.TwoDimensionParamsIntoOne
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ListToString(object value, object delimiter = null)
    {
        var delimiterS = AllStrings.comma;
        if (delimiter != null)
        {
            delimiterS = delimiter.ToString();
        }
        if (value == null)
        {
            return "(null)";
        }
        string text;
        var valueType = value.GetType();
        text = value.ToString();
        if (value is IEnumerable && valueType != Consts.tString && valueType != Consts.tStringBuilder && !(value is IEnumerable<char>))
        {
            var enumerable = CA.ToListString(value as IEnumerable);
            CA.Replace(enumerable, delimiterS, AllStrings.space);
            text = SH.Join(delimiter, enumerable);
        }
        return text;
    }

    /// <summary>
    /// Musi mit sudy pocet prvku
    /// Pokud sudý [0], [2], ... bude mít aspoň jeden nebílý znak, pak se přidá lichý [1], [3] i sudý ve dvojicích. jinak nic
    /// </summary>
    /// <param name="className"></param>
    /// <param name="v1"></param>
    /// <param name="methodName"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static string ConcatIfBeforeHasValue(params string[] className)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < className.Length; i++)
        {
            string even = className[i];
            if (!string.IsNullOrWhiteSpace(even))
            {

                //string odd = 
                result.Append(even + className[++i]);
            }

        }
        return result.ToString();
    }



/// <summary>
    /// Snaž se tuto metodu využívat co nejméně, je zbytečná.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string Copy(string s)
    {
        return s;
    }

/// <summary>
    /// Pokud je poslední znak v A1 A2, odstraním ho
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ConvertPluralToSingleEn(string nazevTabulky)
    {
        if (nazevTabulky[nazevTabulky.Length - 1] == 's')
        {
            if (nazevTabulky[nazevTabulky.Length - 2] == 'e')
            {
                if (nazevTabulky[nazevTabulky.Length - 3] == 'i')
                {
                    return nazevTabulky.Substring(0, nazevTabulky.Length - 3) + "y";
                }
            }
            return nazevTabulky.Substring(0, nazevTabulky.Length - 1);
        }

        return nazevTabulky;
    }

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

public static string WrapWithQm(string commitMessage)
    {
        return SH.WrapWith(commitMessage, AllChars.qm);
    }

[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, string h)
    {
        return h + value + h;
    }
[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, char v)
    {
        // TODO: Make with StringBuilder, because of SH.WordAfter and so
        return WrapWith(value, v.ToString());
    }

/// <summary>
    /// Vše tu funguje výborně
    /// Metoda pokud chci vybrat ze textu A1 posledních p_2 znaků které jsou v celku(oddělené mezerami) a vložit před ně ...
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <returns></returns>
    public static string ShortForLettersCountThreeDots(string p, int p_2)
    {
        bool pridatTriTecky = false;
        string vr = ShortForLettersCount(p, p_2, out pridatTriTecky);
        if (pridatTriTecky)
        {
            vr += " ... ";
        }
        return vr;
    }

public static int OccurencesOfStringIn(string source, string p_2)
    {
        return source.Split(new string[] { p_2 }, StringSplitOptions.None).Length - 1;
    }

public static bool ContainsOtherChatThanLetterAndDigit(string p)
    {
        foreach (char item in p)
        {
            if (!char.IsLetterOrDigit(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Dont contains 
    /// </summary>
    public static char[] spaceAndPuntactionChars = new char[] { AllChars.space, AllChars.dash, AllChars.dot, AllChars.comma, AllChars.sc, AllChars.colon, AllChars.exclamation, AllChars.q, '–', '—', '‐', '…', '„', '“', '‚', '‘', '»', '«', '’', AllChars.bs, AllChars.lb, AllChars.rb, AllChars.lsf, AllChars.rsf, AllChars.cbl, AllChars.cbr, '〈', '〉', AllChars.lt, AllChars.gt, AllChars.slash, AllChars.bs, AllChars.pipe, '”', AllChars.qm, '~', '°', AllChars.plus, '@', '#', '$', AllChars.modulo, '^', '&', AllChars.asterisk, '=', AllChars.us, 'ˇ', '¨', '¤', '÷', '×', '˝' };

    public static void Init()
    {
        List<char> spaceAndPuntactionCharsAndWhiteSpacesList = new List<char>();
        spaceAndPuntactionCharsAndWhiteSpacesList.AddRange(spaceAndPuntactionChars);
        foreach (var item in AllChars.whiteSpacesChars)
        {
            if (!spaceAndPuntactionCharsAndWhiteSpacesList.Contains(item))
            {
                spaceAndPuntactionCharsAndWhiteSpacesList.Add(item);
            }
        }

        spaceAndPuntactionCharsAndWhiteSpaces = spaceAndPuntactionCharsAndWhiteSpacesList.ToArray();
    }

    static char[] spaceAndPuntactionCharsAndWhiteSpaces = null;
    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string s)
    {
        return s.Split(spaceAndPuntactionCharsAndWhiteSpaces).ToList();
    }

public static string GetOddIndexesOfWord(string hash)
    {
        int polovina = hash.Length / 2;
        polovina = (polovina / 2);
        polovina += polovina / 2;
        StringBuilder sb = new StringBuilder(polovina);
        int pricist = 2;
        for (int i = 0; i < hash.Length; i += pricist)
        {
            sb.Append(hash[i]);
        }
        return sb.ToString();
    }

public static string GetString(IEnumerable o, string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in o)
        {
            sb.Append(SH.ListToString( item) + p);
        }
        return sb.ToString();
    }

    public static bool IsNegation(ref string contains)
    {
        if (contains[0] == AllChars.exclamation)
        {
            contains = contains.Substring(1);
            return true;
        }
        return false;
    }

/// <summary>
    /// 
    /// </summary>
    /// <param name="pred"></param>
    /// <param name="za"></param>
    /// <param name="text"></param>
    /// <param name="or"></param>
    public static void GetPartsByLocation(out string pred, out string za, string text, char or)
    {
        int dex = text.IndexOf(or);
        SH.GetPartsByLocation(out pred, out za, text, dex);
    }
/// <summary>
    /// Into A1,2 never put null
    /// </summary>
    /// <param name="pred"></param>
    /// <param name="za"></param>
    /// <param name="text"></param>
    /// <param name="pozice"></param>
    public static void GetPartsByLocation(out string pred, out string za, string text, int pozice)
    {
        if (pozice == -1)
        {
            pred = text;
            za = "";
        }
        else
        {
            pred = text.Substring(0, pozice);
            za = text.Substring(pozice + 1);
        }

        
    }

public static string JoinMakeUpTo2NumbersToZero(object p, params int[] parts)
    {
        List<string> na2Cislice = new List<string>();
        foreach (var item in parts)
        {
            na2Cislice.Add(DTHelper.MakeUpTo2NumbersToZero(item));
        }
        return JoinIEnumerable(p, na2Cislice);
    }

public static string ReplaceOnceIfStartedWith(string what, string replaceWhat, string zaCo)
    {
        bool replaced;
        return ReplaceOnceIfStartedWith(what, replaceWhat, zaCo, out replaced);
    }
public static string ReplaceOnceIfStartedWith(string what, string replaceWhat, string zaCo, out bool replaced)
    {
        replaced = false;
        if (what.StartsWith(replaceWhat))
        {
            replaced = true;
            return SH.ReplaceOnce(what, replaceWhat, zaCo);
        }
        return what;
    }

public static string RemoveLastChar(string artist)
    {
        return artist.Substring(0, artist.Length - 1);
    }

/// <summary>
    /// Údajně detekuje i japonštinu a podpobné jazyky
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsChinese(string text)
    {
        var hiragana = GetCharsInRange(text, 0x3040, 0x309F);
        if (hiragana )
        {
            return true;
        }
        var katakana = GetCharsInRange(text, 0x30A0, 0x30FF);
        if (katakana )
        {
            return true;
        }
        var kanji = GetCharsInRange(text, 0x4E00, 0x9FBF);
        if (kanji )
        {
            return true;
        }

        if (text.Any(c => c >= 0x20000 && c <= 0xFA2D))
        {
            return true;
        }

        return false;
    }

public static string SubstringIfAvailable(string input, int lenght)
    {
        if (input.Length > lenght)
        {
            return input.Substring(0, lenght);
        }
        return input;
    }

/// <summary>
    /// Nevraci znaky na indexech ale zda nektere znaky maji rozsah char definovany v A2,3
    /// </summary>
    /// <param name="text"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static  bool GetCharsInRange(string text, int min, int max)
    {
        return text.Where(e => e >= min && e <= max).Count() != 0;
    }

public static string JoinWithoutTrim(object p, IList parts)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object item in parts)
        {
            sb.Append(item.ToString() + p);
        }
        return sb.ToString();
    }

/// <summary>
    /// 
    /// </summary>
    /// <param name="nazevPP"></param>
    /// <param name="only"></param>
    /// <returns></returns>
    public static string FirstCharUpper(string nazevPP, bool only = false)
    {
        if (nazevPP != null)
        {
            string sb = nazevPP.Substring(1);
            if (only)
            {
                sb = sb.ToLower();
            }
            return nazevPP[0].ToString().ToUpper() + sb;
        }
        return null;
    }

    /// <summary>
    /// POZOR, tato metoda se změnila, nyní automaticky přičítá k indexu od 1
    /// When I want to include delimiter, add to A3 +1
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="p"></param>
    /// <param name="p_3"></param>
    /// <returns></returns>
    public static string Substring(string sql, int indexFrom, int indexTo)
    {
        if (sql == null)
        {
            return null;
        }
        int tl = sql.Length;
        if (tl > indexFrom)
        {
            if (tl > indexTo)
            {
                return sql.Substring(indexFrom, indexTo - indexFrom);
            }
        }
        return null;
    }

public static List< string> RemoveDuplicatesNone(string p1, string delimiter)
    {
        var split = SH.SplitNone(p1, delimiter);
        return CA.RemoveDuplicitiesList<string>(split);
    }

public static string JoinSpace(IEnumerable parts)
    {
        return SH.JoinString(AllStrings.space, parts);
    }

public static string RemoveBracketsAndHisContent(string title, bool squareBrackets, bool parentheses, bool braces)
    {
        if (squareBrackets)
        {
            title = RemoveBetweenAndEdgeChars(title, AllChars.lsf, AllChars.rsf);
        }
        if (parentheses)
        {
            title = RemoveBetweenAndEdgeChars(title, AllChars.lb, AllChars.rb);
        }
        if (braces)
        {
            title = RemoveBetweenAndEdgeChars(title, AllChars.cbl, AllChars.cbr);
        }
        title = ReplaceAll(title, "", AllStrings.doubleSpace).Trim();
        return title;
    }

public static string RemoveBetweenAndEdgeChars(string s, char begin, char end)
    {
        Regex regex = new Regex(SH.Format2("\\{0}.*?\\{1}", begin, end));
        return regex.Replace(s, string.Empty);
    }

/// <summary>
    /// Je dobré před voláním této metody převést bílé znaky v A1 na mezery
    /// </summary>
    /// <param name="celyObsah"></param>
    /// <param name="stred"></param>
    /// <param name="naKazdeStrane"></param>
    /// <returns></returns>
    public static string XCharsBeforeAndAfterWholeWords(string celyObsah, int stred, int naKazdeStrane)
    {
        StringBuilder prava = new StringBuilder();
        StringBuilder slovo = new StringBuilder();
        
        // Teď to samé udělám i pro levou stranu
        StringBuilder leva = new StringBuilder();
        for (int i = stred - 1; i >= 0; i--)
        {
            char ch = celyObsah[i];
            if (ch == AllChars.space)
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {

                    leva.Insert(0, ts + AllStrings.space);
                    if (leva.Length + AllStrings.space.Length + ts.Length > naKazdeStrane)
                    {
                        break;
                    }
                    else
                    {
                        
                    }
                }
            }
            else
            {
                slovo.Insert(0, ch);
            }
        }
        string l = slovo.ToString() + AllStrings.space + leva.ToString().TrimEnd(AllChars.space);
        l = l.TrimEnd(AllChars.space);
        naKazdeStrane += naKazdeStrane - l.Length;
        slovo.Clear();
        // Počítám po pravé straně započítám i to středové písmenko
        for (int i = stred; i < celyObsah.Length; i++)
        {
            char ch = celyObsah[i];
            if (ch == AllChars.space)
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {

                    prava.Append(AllStrings.space + ts);
                    if (prava.Length + AllStrings.space.Length + ts.Length > naKazdeStrane)
                    {
                        break;
                    }
                    else
                    {
                        
                    }
                }
            }
            else
            {
                slovo.Append(ch);
            }
        }
        
        string p = prava.ToString().TrimStart(AllChars.space) + AllStrings.space + slovo.ToString();
        p = p.TrimStart(AllChars.space);
        string vr = "";
        if (celyObsah.Contains(l + AllStrings.space) && celyObsah.Contains(AllStrings.space + p))
        {
            vr = l + AllStrings.space + p;
        }
        else
        {
            vr = l + p;
        }
        return vr;
    }

/// <summary>
    /// Do výsledku zahranu i mezery a punktační znaménka 
    /// </summary>
    /// <param name="veta"></param>
    /// <returns></returns>
    public static string[] SplitBySpaceAndPunctuationCharsLeave(string veta)
    {
        List<string> vr = new List<string>();
        vr.Add("");
        foreach (var item in veta)
        {
            bool jeMezeraOrPunkce = false;
            foreach (var item2 in spaceAndPuntactionChars)
            {
                if (item == item2)
                {
                    jeMezeraOrPunkce = true;
                    break;
                }
            }

            if (jeMezeraOrPunkce)
            {
                if (vr[vr.Count - 1] == "")
                {
                vr[vr.Count - 1] += item.ToString();    
                }
                else
                {
                    vr.Add(item.ToString());
                }
                
                vr.Add("");
            }
            else
            {
                vr[vr.Count - 1] += item.ToString();
            }
        }
        return vr.ToArray();
    }

/// <summary>
    /// Vše tu funguje výborně
    /// G text z A1, ktery bude obsahovat max A2 písmen - ne slov, protože někdo tam může vložit příliš dlouhé slova a nevypadalo by to pak hezky.
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <returns></returns>
    public static string ShortForLettersCountThreeDotsReverse(string p, int p_2)
    {
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (SH.IsInLastXCharsTheseLetters(p, p_2, AllChars.space))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;

                int napocitano = 0;
                for (int i = to - 1; i >= 0; i--)
                {
                    napocitano++;

                    if (d[i] == AllChars.space)
                    {
                        if (napocitano >= p_2)
                        {
                            break;
                        }

                        dexMezery = i;
                    }
                }
                    d = d.Substring(dexMezery + 1);
                    if (d.Trim() != "")
                    {
                        d = " ... " + d;
                    }
                    return d;
                //}
            }
            else
            {
                return " ... " + p.Substring(p.Length - p_2);
            }
        }

        return p;
    }

public static List<FromToWord> ReturnOccurencesOfStringFromToWord(string celyObsah, params string[] hledaneSlova)
    {
        if (hledaneSlova == null || hledaneSlova.Length == 0)
        {
            return new List<FromToWord>();
           
        }
        celyObsah = celyObsah.ToLower();
        List<FromToWord> vr = new List<FromToWord>();
        int l = celyObsah.Length;
        for (int i = 0; i < l; i++)
        {
            foreach (string item in hledaneSlova)
            {
                bool vsechnoStejne = true;
                int pridat = 0;
                while (pridat < item.Length)
                {
                    
                    int dex = i + pridat;
                    if (l > dex)
                    {
                        if (celyObsah[dex] != item[pridat])
                        {
                            vsechnoStejne = false;
                            break;
                        }
                    }
                    else
                    {
                        vsechnoStejne = false;
                        break;
                    }
                    pridat++;
                }
                if (vsechnoStejne)
                {
                    FromToWord ftw = new FromToWord();
                    ftw.from = i;
                    ftw.to = i + pridat - 1;
                    ftw.word = item;
                    vr.Add(ftw);
                    i += pridat;
                    break;
                }
            }
        }
        return vr;
    }

private static bool IsInLastXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        
        for (int i = p.Length - 1; i >= pl; i--)
        {
            foreach (var item in letters)
            {
                if (p[i] == item)
                {
                    return true;
                }
            }
        }
        return false;

    }

public static string TrimNewLineAndTab(string lyricsFirstOriginal)
    {
        return lyricsFirstOriginal.Replace("\t", AllStrings.space).Replace("\r", AllStrings.space).Replace("\n", AllStrings.space).Replace(AllStrings.doubleSpace, AllStrings.space);
    }

public static List<string> SplitByWhiteSpaces(string s)
    {
        return s.Split(AllChars.whiteSpacesChars.ToArray()).ToList();
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

public static string DoubleSpacesToSingle(string v)
    {
        return SH.ReplaceAll2(v, AllStrings.space, AllStrings.doubleSpace);
    }

/// <summary>
    /// Originally named TrimWithEnd
    /// Pokud A1 končí na A2, ořežu A2
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ext"></param>
    /// <returns></returns>
    public static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext))
        {
            return name.Substring(0, name.Length - ext.Length);
        }
        return name;
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

public static string GetTextBetweenTwoChars(string p, int begin, int end)
    {
        // a(1) - 1,3
        return p.Substring(begin + 1, end - begin - 1);
        // originally
        //return p.Substring(begin+1, end - begin - 1);
    }

public static string ReplaceWhiteSpacesWithoutSpaces(string p)
    {
        return ReplaceWhiteSpacesWithoutSpaces(p, "");
    }
public static string ReplaceWhiteSpacesWithoutSpaces(string p, string replaceWith = "")
    {
        return p.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

public static List<string> SplitAdvanced(string v, bool replaceNewLineBySpace, bool moreSpacesForOne, bool trim, bool escapeQuoations, params string[] deli)
    {
        var s = SH.Split(v, deli);
        if (replaceNewLineBySpace)
        {
            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceAll(s[i], AllStrings.space, "\r", "\n", Environment.NewLine);
            }
        }
        if (moreSpacesForOne)
        {
            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceAll2(s[i], AllStrings.space, AllStrings.doubleSpace);
            }
        }
        if (trim)
        {
            s = CA.Trim(s);
        }
        if (escapeQuoations)
        {
            string rep = AllStrings.qm;

            for (int i = 0; i < s.Count; i++)
            {
                    s[i] = SH.ReplaceFromEnd(s[i], "\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\"", rep);
                //}
            }
        }
        return s;
    }

public static string TrimStartAndEnd(string v, string s, string e)
    {
        v = TrimEnd(v, e);
        v = TrimStart(v, s);
        
        return v;
    }

public static string TrimStart(string v, string s)
    {
        while (v.StartsWith(s))
        {
            v = v.Substring(s.Length);
        }
        return v;
    }

public static string ReplaceFromEnd(string s, string zaCo, string co)
    {
        List<int> occ = SH.ReturnOccurencesOfString(s, co);
        for (int i = occ.Count - 1; i >= 0; i--)
        {
            s = SH.ReplaceByIndex(s, zaCo, occ[i], co.Length);
        }
        return s;
    }

/// <summary>
    /// Vrátí prázdný řetězec pokud nebude nalezena mezera.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GetFirstWord(string p)
    {
        p = p.Trim();
        int dex = p.IndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(0, dex);
        }
        return "";
    }

/// <summary>
    /// Pokud něco nebude číslo, program vyvolá výjimku, protože parsuje metodou int.Parse
    /// </summary>
    /// <param name="stringToSplit"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static List<int> SplitToIntList(string stringToSplit, params string[] delimiter)
    {
        var f = SH.Split(stringToSplit, delimiter);
        List<int> nt = new List<int>(f.Count);
        foreach (string item in f)
        {
            nt.Add(int.Parse(item));
        }
        return nt;
    }

/// <summary>
    /// Oddělovač může být pouze jediný znak, protože se to pak předává do metody s parametrem int!
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    public static string GetFirstPartByLocation(string p1, char deli)
    {
        string p, z;
        GetPartsByLocation(out p, out z, p1, p1.IndexOf(deli));
        return p;
    }
}