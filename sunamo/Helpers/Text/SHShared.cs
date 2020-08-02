using sunamo;
using sunamo.Enums;
using System.Diagnostics;
using System.Globalization;
using sunamo.Constants;
using sunamo.Essential;

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

    public static string Format34(string c, params object[] innerMain)
    {
        string formatted = null;

        try
        {
            formatted = SH.Format4(c, innerMain);
        }
        catch (Exception)
        {
        }

        try
        {
            formatted = SH.Format3(c, innerMain);
        }
        catch (Exception)
        {
        }

        return formatted;
    }

    public static bool ContainsOnly(string floorS, List<char> numericChars)
    {
        foreach (var item in floorS)
        {
            if (!numericChars.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    public static string RemoveAfterFirst(string v, Func<char, bool> isSpecial)
    {
        v = v.Trim();
        for (int i = 0; i < v.Length; i++)
        {
            if (isSpecial(v[i]))
            {
                return v.Substring(0, i);
            }
        }
        return v;
    }


    /// <summary>
    /// Nothing can be null
    /// </summary>
    /// <param name="content"></param>
    /// <param name="lines"></param>
    /// <param name="dx2"></param>
    /// <returns></returns>
    public static string GetLineFromCharIndex(string content, List<string> lines, int dx2)
    {
        var dx = GetLineIndexFromCharIndex(content, dx2);
        return lines[dx];
    }

    /// <summary>
    /// Return index, therefore x-1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="pos"></param>
    public static int GetLineIndexFromCharIndex(string input, int pos)
    {
        var lineNumber = input.Take(pos).Count(c => c == '\n') + 1;
        return lineNumber - 1;
    }

    public static string ReplaceVariables(string innerHtml, List<List<string>> _dataBinding, int actualRow)
    {
        return ReplaceVariables(AllChars.lcub, AllChars.rcub, innerHtml, _dataBinding, actualRow);
    }

    public const String diacritic = "\u00E1\u010D\u010F\u00E9\u011B\u00ED\u0148\u00F3\u0161\u0165\u00FA\u016F\u00FD\u0159\u017E\u00C1\u010C\u010E\u00C9\u011A\u00CD\u0147\u00D3\u0160\u0164\u00DA\u016E\u00DD\u0158\u017D";

    public static int AnotherOtherThanLetterOrDigit(string content, int v)
    {
        int i = v;
        for (; i < content.Length; i++)
        {
            if (!char.IsLetterOrDigit(content[i]))
            {
                //i--;
                return i;
            }
        }
        //i--;
        return i--;
    }

    public static string LastChars(string v1, int v2)
    {
        return v1.Substring(v1.Length - v2);

        //mystring.Substring(Math.Max(0, mystring.Length - 4));
    }

    public static List<string> SplitByLetterCount(string s, int c)
    {
        int sl = s.Length;
        int e = sl / c;
        int remain = sl % c; 
        if (remain != 0)
        {
            ThrowExceptions.Custom(null, type, Exc.CallingMethod(), sess.i18n(XlfKeys.NumbersOfLetters)+" " + s + " is not dividable with " + c);
        }

        List<string> ls = new List<string>(c);
        int from = 0;

        while(s.Length > from + c-2)
        {
            
                ls.Add(s.Substring(from, c));

                from += c;
                if (from == sl)
                {
                    break;
                }
            
            
        }
        return ls;
    }

    public static string TabToNewLine(string v)
    {
        //Environment.NewLine
        v = v.Replace("\t", "\r");
        var l = SH.GetLines(v);
        CA.Trim(l);
        CA.RemoveStringsEmpty(l);
        return SH.JoinNL(l);
    }

    internal static bool IsAllLower(string ext)
    {
        return IsAllLower(ext, char.IsLower);
    }

    private static bool IsAllLower(string ext, Func<char, bool> isLower)
    {
        for (int i = 0; i < ext.Length; i++)
        {
            if (!isLower(ext[i]))
            {
                return false;
            }
        }
        return true;
    }

    internal static bool IsAllUpper(string ext)
    {
        return IsAllLower(ext, char.IsUpper);
    }

    public static bool ContainsBracket(string t, bool mustBeLeftAndRight = false)
    {
        List<string> left, right;
        left = right = null;
        return ContainsBracket(t, ref left, ref right, mustBeLeftAndRight);
    }

    static SH()
    {
        s_cs = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs";
        Init();

        if (bracketsLeft == null)
        {
            bracketsLeft = new Dictionary<Brackets, string>();
            bracketsLeft.Add(Brackets.Curly, "{");
            bracketsLeft.Add(Brackets.Square, "[");
            bracketsLeft.Add(Brackets.Normal, "(");
            bracketsLeftList = CA.ToListChar( bracketsLeft.Values);

            bracketsRight = new Dictionary<Brackets, string>();
            bracketsRight.Add(Brackets.Curly, "}");
            bracketsRight.Add(Brackets.Square, "]");
            bracketsRight.Add(Brackets.Normal, ")");

            bracketsRightList = CA.ToListChar( bracketsRight.Values);

        }
    }

    public static List<int> TabOrSpaceNextTo(string input)
    {
        var tabs = SH.ReturnOccurencesOfString(input, AllStrings.tab);

        // nevím k čemu to tu je ale když jsem měl řetězec b nopCommerce\tSimplCommerce\tSmartStoreNET\tgrandnode\tKartris tak mi to vrátilo navíc o 2 \t kde nikdy nebyly

        //for (int i = 0; i < tabs.Count-1; i++)
        //{
        //    var dx = tabs[i] + 1;
        //    if (input[i] == AllChars.space)
        //    {
        //        tabs.Add(dx);
        //    }
        //}

        //for (int i = 1; i < tabs.Count; i++)
        //{
        //    var dx = tabs[i] - 1;
        //    if (input[i] == AllChars.space)
        //    {
        //        tabs.Add(dx);
        //    }
        //}
        return tabs;
    }

    public static List<string> SplitByIndexes(string input, List<int> bm)
    {
        List<string> d = new List<string>(bm.Count +1);
        bm.Sort();
        string before, after;
        before = input;

        for (int i = bm.Count - 1; i >= 0; i--)
        {
            SH.GetPartsByLocation(out before, out after, before, bm[i]);
            d.Leading(after);
        }

        SH.GetPartsByLocation(out before, out after, before, bm[0]);
        d.Leading(before);
        d.Reverse();
        return d;
    }

    /// <summary>
    /// IndexesOfChars - char
    /// ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="input"></param>
    /// <param name="whiteSpacesChars"></param>
    /// <returns></returns>
    public static List<int> IndexesOfChars(string input, List<char> whiteSpacesChars)
    {
        var dx = new List<int>();
        foreach (var item in whiteSpacesChars)
        {
            dx.AddRange(SH.ReturnOccurencesOfString(input, item.ToString()));
        }
        dx.Sort();
        return dx;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public static bool ContainsBracket(string t, ref List<string> left, ref List<string> right, bool mustBeLeftAndRight = false)
    {
        left = SH.ContainsAny(t, false, AllLists.leftBrackets);
        right = SH.ContainsAny(t, false, AllLists.rightBrackets);
        if (mustBeLeftAndRight)
        {
            if (left.Count > 0 && right.Count > 0)
            {
                return true;
            }
        }
        else
        {
            if (left.Count > 0 || right.Count > 0)
            {
                return true;
            }
        }
        
        
       
        return false;
    }

    

    public static string ClosingBracketFor(string v)
    {
        foreach (var item in bracketsLeft)
        {
            if (item.Value == v)
            {
                return bracketsRight[item.Key];
            }
        }

        ThrowExceptions.IsNotAllowed(null, type, Exc.CallingMethod(), v +" as bracket");
        return null;
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

    /// <summary>
    /// Get text after cz#cd => #cd
    /// </summary>
    /// <param name="item"></param>
    /// <param name="after"></param>
    public static string TextAfter(string item, string after)
    {
        var dex = item.IndexOf(after);
        if (dex != -1)
        {
            return item.Substring(dex + after.Length); 
        }
        return string.Empty;
    }

    public static string ReplaceAllExceptPrefixed(string t, string to, string from, string fromCannotBePrefixed)
    {
        var occ = SH.ReturnOccurencesOfString(t, from);
        for (int i = occ.Count - 1; i >= 0; i--)
        {
            var item = occ[i];
            var begin = item - fromCannotBePrefixed.Length;
            if (begin > -1)
            {
                var prefix = t.Substring(begin, fromCannotBePrefixed.Length);
                if (prefix != fromCannotBePrefixed)
                {
                    t = ReplaceByIndex(t, to, item, from.Length);
                }
            }
        }

        return t;
    }

    public static string PadRight(string empty, string newLine, int v)
    {
        StringBuilder sb = new StringBuilder(empty);
        for (int i = 0; i < v; i++)
        {
            sb.Append(newLine);
        }
        return sb.ToString();
    }

    public static void RemoveLastChar(StringBuilder sb)
    {
        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
        }
    }

    public static string RemoveUselessWhitespaces(string innerText)
    {
        var p = SH.SplitByWhiteSpaces(innerText, true);
        return SH.JoinSpace(p);
    }

    /// <summary>
    /// Is used in btnShortTextOfLyrics
    /// Short text but always keep whole paragraps
    /// Can be use also for non paragraph strings abcd->ab
    /// </summary>
    /// <param name="c"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string ShortToLengthByParagraph(string c, int maxLength)
    {
        //var delimiter = SH.PadRight(string.Empty, Environment.NewLine, 2);
        var p = SH.SplitByWhiteSpaces(c);
        

        while (c.Length + p.Count > maxLength)
        {
            if (p.Count > 1)
            {
                p.RemoveAt(p.Count - 1);
                c = SH.Join(AllStrings.space, p);
            }
            else
            {
                c = SH.SubstringIfAvailable( c, maxLength); break;
            }
        }

        if (maxLength < c.Length)
        {

        }

        return c;
    }

    public static string AddBeforeUpperChars(string text, char add, bool preserveAcronyms)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                if ((text[i - 1] != add && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(add);
            newText.Append(text[i]);
        }
        return newText.ToString();
    }

    private static Type type = typeof(SH);

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

    public static string TrimEndSpaces(string v)
    {
        return v.TrimEnd(AllChars.space);
    }

    public static Tuple<List<string>, List<string>> SplitFromReplaceManyFormatList(string input)
    {
        var t = SplitFromReplaceManyFormat(input);
        return new Tuple<List<string>, List<string>>(SH.GetLines(t.Item1), SH.GetLines(t.Item2));
    }

    public static Tuple<string, string> SplitFromReplaceManyFormat(string input)
    {
        StringBuilder to = new StringBuilder();
        StringBuilder from = new StringBuilder();

        if (input.Contains(Consts.transformTo))
        {

            var lines = SH.GetLines(input);
            foreach (var item in lines)
            {
                var p = SH.Split(item, Consts.transformTo);
                from.AppendLine(p[0]);
                to.AppendLine(p[1]);
            }
        }
        else
        {
            from.AppendLine(input);
        }


        return new Tuple<string, string>(from.ToString(), to.ToString());
        
    }

    internal static string ReplaceAllWhitecharsForSpace(string c)
    {
        foreach (var item in AllChars.whiteSpacesChars)
        {
            if (item != AllChars.space)
            {
                c = c.Replace(item, AllChars.space);
            }
        }

        return c;
    }

    /// <summary>
    /// Method is useless
    /// ReplaceMany firstly split into two strings
    /// Better is call SH.ReplaceAll2(input, to.ToString(), from.ToString(), true);
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string PrepareForReplaceMany(List<string> from, List<string> to)
    {
        return null;
    }

    public static string ReplaceMany(string input, string fromTo, bool removeEndingPairCharsWhenDontHaveStarting = true)
    {
        StringBuilder from = new StringBuilder();
        StringBuilder to = new StringBuilder();

        var l = SH.GetLines(fromTo);
        CA.RemoveStringsEmpty2(l);
        string delimiter = Consts.transformTo;

        List<string> replaceForEmpty = new List<string>();

        foreach (var item in l)
        {
            // Must be split, not splitNone
            // 'SH.ReplaceInAllFiles:  Different count elements in collection from2 - 4 vs. to2 - 3'
            var p = SH.Split( item, delimiter);
            if (p.Count == 1)
            {
                if (item.EndsWith(delimiter))
                {
                    replaceForEmpty.Add(p[0]);

                    continue;
                    //p.Add(string.Empty);
                }
                else
                {
                    //p.Insert(0, string.Empty);
                }
            }
            from.AppendLine(p[0]);
            to.AppendLine(p[1]);
        }

        string vr = SH.ReplaceAll2(input, to.ToString(), from.ToString(), true);

        foreach (var item in replaceForEmpty)
        {
            vr = vr.Replace(item, string.Empty);
        }

        if (removeEndingPairCharsWhenDontHaveStarting)
        {
            vr = SH.RemoveEndingPairCharsWhenDontHaveStarting(vr, AllStrings.lcub, AllStrings.rcub);
        }

        return vr;
    }

    public static T ToNumber<T>(Func<string, T> parse, string v)
    {
        return parse.Invoke(v);
    }

    public static string RemoveEndingPairCharsWhenDontHaveStarting(string vr, string cbl, string cbr)
    {
        string result = null;
        List<int> removeOnIndexes = new List<int>();

        var sb = new StringBuilder(vr);
        

        var occL = SH.ReturnOccurencesOfString(vr, cbl);
        var occR = SH.ReturnOccurencesOfString(vr, cbr);
        List<int> onlyLeft = null;
        List<int> onlyRight = null;


        var l = GetPairsStartAndEnd(occL, occR, ref onlyLeft, ref onlyRight);

        onlyLeft.AddRange(onlyRight);
        onlyLeft.Sort();

        for (int i = onlyLeft.Count - 1; i >= 0; i--)
        {
            sb.Remove(onlyLeft[i], 1);
        }

        //if (occL.Count == 0)
        //{
        //    result = vr.Replace(AllStrings.rcub, string.Empty);
        //}
        //else
        //{
        //    

        //    int left = -1;
        //    int right = -1;

        //    var onlyLeft = new List<int>();

        //    var pairs = SH.GetPairsStartAndEnd(occL, occR, ref onlyLeft);

        //    while (true)
        //    {
        //        if (occR.Count == 0)
        //        {
        //            break;
        //        }

        //        if (occL.Count == 0)
        //        {
        //            break;
        //        }

        //        left = occL.First();
        //        right = occR.First();

        //        if (right > left)
        //        {
        //            removeOnIndexes.Add(right);
        //            occR.RemoveAt(0);
        //        }
        //        else
        //        {
        //            // right, remove from right
        //            occR.RemoveAt(0);
        //        }
        //    }

        //    StringBuilder sb = new StringBuilder(vr);

        //    for (int i = removeOnIndexes.Count - 1; i >= 0; i--)
        //    {
        //        vr.Remove(removeOnIndexes[i], 1);
        //    }

        //    result = vr.ToLower();
        //} 

        return sb.ToString();
    }

    public static List<Tuple<int, int>> GetPairsStartAndEnd(List<int> occL, List<int> occR, ref List<int> onlyLeft, ref List<int> onlyRight)
    {
        var l = new List<Tuple<int, int>>();

        onlyLeft = occL.ToList();
        onlyRight = occR.ToList();

        for (int i = occR.Count - 1; i >= 0; i--)
        {
            int lastRight = occR[i];
            if (occL.Count == 0)
            {
                break;
            }
            var lastLeft = occL.Last();

            if (lastRight < lastLeft)
            {
                i++;
                // Na konci přebývá lastLeft

               // onlyLeft.Add(lastLeft);
               // I will remove it on end
                occL.RemoveAt(occL.Count - 1);
            }
            else
            {
                // když je lastLeft menší, znamená to že last right má svůj levý protějšek
                l.Add(new Tuple<int, int>( lastLeft, lastRight));
            }
        }

        occL = onlyLeft;

        //foreach (var item in l)
        //{
        //    occL.Remove(item.Item1);
        //}

        // occL = onlyLeft o pár řádků výše
        //onlyLeft.AddRange(occL);

        //l.Reverse();

        var addToAnotherCollection = new CollectionWithoutDuplicates<int>();
        var l2 = new List<Tuple<int, int>>();
        
        List<int> alreadyProcessedItem1 = new List<int>();
        for (int i = l.Count - 1; i >= 0; i--)
        {
            

            if (alreadyProcessedItem1.Contains(l[i].Item1))
            {
                addToAnotherCollection.Add(l[i].Item1);
                l2.Add(l[i]);
                l.RemoveAt(i);
                //continue;
            }


            alreadyProcessedItem1.Add(l[i].Item1);
        }

        //for (int i = l2.Count - 1; i >= 0; i--)
        //{
        //    if (l.Contains(l2[i]))
        //    {
        //        l2.RemoveAt(i);
        //    }
        //}

        foreach (var item in addToAnotherCollection.c)
        {
            var count = alreadyProcessedItem1.Where(d=> d==item).Count();
            //!alreadyProcessedItem1.Contains(item)

            if (count > 2)
            {


                var sele = l2.Where(d => d.Item1 == item).ToList();
                //for (int i = sele.Count() - 1; i >= 1; i--)
                //{
                //    l2.Remove(sele[i]);
                //}

                var dx2 = occL.IndexOf(sele[0].Item1);
                if (dx2 != -1)
                {
                    var dx3 = l.IndexOf(sele[0]);
                    l.Add(new Tuple<int, int>(occL[dx2 - 1], sele[0].Item2));
                }

            }
        }

        //l.AddRange(l2);

        occL.Sort();




        var result = l; //l.OrderByDescending(d => d.Item1).ToList();
        //

        List<int> alreadyProcessed = new List<int>();

        int dx = -1;

        for (int y = 0; y < result.Count; y++)
        {
            var item = result[y];
            var i = item.Item1;

            if (alreadyProcessed.Contains(i))
            {
                dx = occL.IndexOf(i);
                if (dx != -1)
                {
                    i = occL[dx - 1];
                    result[i] = new Tuple<int, int>(i, result[y -1].Item2);
                }
            }

            alreadyProcessed.Add(i);
        }

        

        onlyLeft =  occL;
        CA.RemoveDuplicitiesList(onlyLeft);
        CA.RemoveDuplicitiesList(onlyRight);

        foreach (var item in result)
        {
            onlyLeft.Remove(item.Item1);
            onlyRight.Remove(item.Item2);
        }

        result.Reverse();

        return result;
    }

    public static string TrimStartAndEnd(string target, Func<char, bool> startAllowed, Func<char, bool> endAllowed)
    {
        for (int i = 0; i < target.Length; i++)
        {
            if (!startAllowed.Invoke( target[i]))
            {
                target = target.Substring(1);
                i--;
            }
            else
            {
                break;
            }
        }

        for (int i = target.Length - 1; i >= 0; i--)
        {
            if (!startAllowed.Invoke(target[i]))
            {
                target = target.Remove(target.Length - 1,1);
                
            }
            else
            {
                break;
            }
        }
        return target;
    }

    public static string JoinSentences(bool addAfterLast, params string[] pDescription)
    {
        StringBuilder sb = new StringBuilder();
        string args = null;
        foreach (var item in pDescription)
        {
            var t = item.Trim();
            if (!string.IsNullOrEmpty(item))
            {
                sb.Append(item);
                if (!item.EndsWith(AllStrings.dotSpace))
                {
                    sb.Append(AllStrings.dotSpace);
                }
            }
        }

        var result = sb.ToString();

        if (!addAfterLast)
        {
            result = SH.TrimEnd(result, AllStrings.dotSpace);
        }
        return result;
    }

    public static string RepairQuotes(string c)
    {
        c = c.Replace(AllStrings.lq, AllStrings.qm);
        c = c.Replace(AllStrings.rq, AllStrings.qm);
        c = c.Replace(AllStrings.la, AllStrings.apostrophe);
        c = c.Replace(AllStrings.ra, AllStrings.apostrophe);
        return c;
    }

    static List<char> bracketsLeftList = null;
    static List<char> bracketsRightList = null;
    static Dictionary<Brackets, string> bracketsLeft = null;
    static Dictionary<Brackets, string> bracketsRight = null;

    public static string ReplaceBrackets(string item, Brackets from, Brackets to)
    {
        

        item = item.Replace(bracketsLeft[from], bracketsLeft[to]);
        item = item.Replace(bracketsRight[from], bracketsRight[to]);

        return item;
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

    public static bool IsNumbered(string v)
    {
        int i = 0;

        foreach (var item in v)
        {
            if (char.IsNumber(item))
            {
                i++;
                continue;
            }
            else if (item == AllChars.dot)
            {
                if (i > 0)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    

    /// <summary>
    /// Get null if count of getted parts was under A2.
    /// Automatically add empty padding items at end if got lower than A2
    /// Automatically join overloaded items to last by A2.
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
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

    public static string InsertEndingBracket(string songName, List<char> countStart, List<char> countEnd)
    {
        var min = Math.Min(countStart.Count, countEnd.Count);
        var max = Math.Max(countStart.Count, countEnd.Count);
        for (int i = min; i < max; i++)
        {
            songName += bracketsRight[SH.GetBracketFromBegin(countStart[i])];
        }
        return songName;
    }

    public static string PairsBracketToCompleteBlock(string input)
    {
        List<char> add = new List<char>();

        foreach (var item in input)
        {
            if (bracketsLeftList.Contains(item))
            {
                add.Add(item);
            }
            if (bracketsRightList.Contains(item))
            {
                Brackets b = GetBracketFromBegin(item);
                var dx = add.IndexOf(bracketsLeft[b][0]);
                if (dx != -1)
                {
                    add.RemoveAt(dx);
                }
                
            }
        }

        StringBuilder sb = new StringBuilder(input);

        if (add.Count > 0)
        {
            sb.AppendLine();

            for (int i = add.Count - 1; i >= 0; i--)
            {
                Brackets b = GetBracketFromBegin(add[i]);
                sb.Append(bracketsRight[b][0]);
            }
            sb.Append(AllChars.sc);
        }
        return sb.ToString();
    }

    private static Brackets GetBracketFromBegin(char v)
    {
        switch (v)
        {
            case '(':
                return Brackets.Normal;
            case '{':
                return Brackets.Curly;
            case '[':
                return Brackets.Square;
            case ')':
                return Brackets.Normal;
            case '}':
                return Brackets.Curly;
            case ']':
                return Brackets.Square;
            default:
                ThrowExceptions.NotImplementedCase(null, type, Exc.CallingMethod(), v);
                break;
        }

        return Brackets.Square;
    }

    public static List<char> IncludeBrackets(string s, bool starting)
    {
        List<char> containsBracket = new List<char>();

        if (starting)
        {
            foreach (var item in s)
            {
                if (CA.ContainsElement<char>( SH.bracketsLeftList, item))
                {
                    containsBracket.Add(item);
                }
            }
        }
        else
        {
            foreach (var item in s)
            {
                if (CA.ContainsElement<char>(SH.bracketsRightList, item))
                {
                    containsBracket.Add(item);
                }
            }
        }

        return containsBracket;
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

        switch (delimiter )
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

    /// <summary>
    /// Use simple c# replace
    /// </summary>
    /// <param name="t"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static string Replace(string t, string what, string forWhat)
    {
        return t.Replace( what, forWhat);
    }

    public static string KeepAfterLast(string searchQuery, string after)
    {
        var dx = searchQuery.LastIndexOf(after);
        if (dx != -1)
        {
            return SH.TrimStart( searchQuery.Substring(dx), after);
        }
        return searchQuery;
    }

    /// <summary>
    /// Overload is without bool pairLines
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    /// <param name="pairLines"></param>
    public static string ReplaceAll2(string vstup, string zaCo, string co, bool pairLines)
    {
        if (pairLines)
        {
            var from2 = SH.Split(co, Environment.NewLine);
            var to2 = SH.Split(zaCo, Environment.NewLine);
            ThrowExceptions.DifferentCountInLists(null, type, "ReplaceInAllFiles", @"from2", from2, "to2", to2);

            for (int i = 0; i < from2.Count; i++)
            {
                vstup = ReplaceAll2(vstup, to2[i], from2[i]);
            }

            return vstup;
        }
        else
        {
            return ReplaceAll2(vstup, zaCo, co);
        }
    }

    public static List<string> SplitAndKeepDelimiters(string originalString, IEnumerable ienu)
    {
        //var ienu = (IEnumerable)deli;
        var vr = Regex.Split(originalString, @"(?<=["+SH.Join("", ienu) + "])");
        return vr.ToList();
    }

    /// <summary>
    /// Stejná jako metoda ReplaceAll, ale bere si do A3 pouze jediný parametr, nikoliv jejich pole
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    public static string ReplaceAll2(string vstup, string zaCo, string co)
    {
        return vstup.Replace(co, zaCo);
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

    public static bool IsValidISO(string input)
    {
        // ISO-8859-1 je to samé jako latin1 https://en.wikipedia.org/wiki/ISO/IEC_8859-1
        byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
        String result = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
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

    public static StringBuilder ReplaceByIndex(StringBuilder s, string zaCo, int v, int length)
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


    /// <summary>
    /// IndexesOfChars - char
    /// ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="vcem"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static List<int> ReturnOccurencesOfString(string vcem, string co)
    {
        vcem = NormalizeString(vcem);
        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            var subs = vcem.Substring(Index, co.Length);
            ////////DebugLogger.Instance.WriteLine(subs);
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

    public static bool ContainsOnlyCase(string between, bool upper, bool ignoreOtherThanLetters = false)
    {
        foreach (var item in between)
        {
            if (upper)
            {
                if (!char.IsUpper(item))
                {
                    if (ignoreOtherThanLetters)
                    {
                        if (char.IsLower(item))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (!char.IsLower(item))
                {
                    if (ignoreOtherThanLetters)
                    {
                        if (char.IsLower(item))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    
    public static string ShortForLettersCount(string p, int p_2)
    {
        bool pridatTriTecky = false;
        return ShortForLettersCount(p, p_2, out pridatTriTecky);
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

    private static bool s_initDiactitic = false;



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
    /// Insert prefix starting with + 
    /// </summary>
    /// <param name="v"></param>
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
    /// Return which a3 is contained in A1. if a2 and A3 contains only 1 element, check for contains these first element
    /// If A3 contains more than 1 element, A2 is not used
    /// If contains more elements, wasnts check
    /// Return elements from A3 which is contained
    /// If don't contains, return zero element collection
    /// </summary>
    /// <param name="item"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    public static List<string> ContainsAny(string item, bool checkInCaseOnlyOneString, IEnumerable<string> contains)
    {
        List<string> founded = new List<string>();

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

    

    public static bool ContainsVariable(string innerHtml)
    {
        return ContainsVariable(AllChars.lcub, AllChars.rcub, innerHtml);
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




    public static string ReplaceVariables(char p, char k, string innerHtml, List<List<string>> _dataBinding, int actualRow)
    {
        StringBuilder sbNepridano = new StringBuilder();
        StringBuilder sbPridano = new StringBuilder();
        bool inVariable = false;
        if (innerHtml != null)
        {

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
        }
        return sbPridano.ToString();
    }



    public static List<int> GetVariablesInString(string innerHtml)
    {
        return GetVariablesInString(AllChars.lcub, AllChars.rcub, innerHtml);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ret"></param>
    /// <param name="pocetDo"></param>
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
    public static List<string> RemoveDuplicates(string input, string delimiter)
    {
        var split = SH.Split(input, delimiter);
        return CA.RemoveDuplicitiesList(new List<string>(split));
    }

    /// <summary>
    /// G zda je jedinz znak v A1 s dia.
    /// </summary>
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

    private static bool s_cs = false;
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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.NoDelimiterDetermined));   
        }

        var deli3 = CA.ToListString( CA.OneElementCollectionToMulti(deli));
        var result = text.Split(deli3.ToArray(), stringSplitOptions).ToList();
        CA.Trim(result);
        if (stringSplitOptions == StringSplitOptions.RemoveEmptyEntries)
        {
            CA.RemoveStringsEmpty(result);
        }

        return result;
    }

    /// <summary>
    /// Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    /// Format2 - use string.Format with error checking
    /// Format3 - Replace {x} with my code. Can be used with wildcard
    /// Format4 - use string.Format without error checking
    /// 
    /// Cannot be use on existing code - will corrupt them
    /// </summary>
    /// <param name="templateHandler"></param>
    /// <param name="lsf"></param>
    /// <param name="rsf"></param>
    /// <param name="id"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static string Format(string templateHandler, string lsf, string rsf, params string[] args)
    {
        var result = SH.Format2(templateHandler, args);
        const string replacement = "{        }";
        result = SH.ReplaceAll2(result, replacement, "[]");
        result = SH.ReplaceAll2(result, AllStrings.lcub, lsf);
        result = SH.ReplaceAll2(result, AllStrings.rcub, rsf);
        result = SH.ReplaceAll2(result, replacement, "{}");
        //result = SH.Format4(result, args);

        return result;
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

    /// <summary>
    /// Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    /// Format2 - use string.Format with error checking
    /// Format3 - Replace {x} with my code. Can be used with wildcard
    /// Format4 - use string.Format without error checking
    /// 
    /// Manually replace every {i} 
    /// </summary>
    /// <param name="template"></param>
    /// <param name="args"></param>
    public static string Format3(string template, params object[] args)
    {
        // this was original implementation but dont know why isnt used string.format
        for (int i = 0; i < args.Length; i++)
        {
            template = SH.ReplaceAll2(template, args[i].ToString(), AllStrings.lcub + i + AllStrings.rcub);
        }
        return template;
    }

    /// <summary>
    /// Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    /// Format2 - use string.Format with error checking
    /// Format3 - Replace {x} with my code. Can be used with wildcard
    /// Format4 - use string.Format without error checking
    /// 
    /// Call string.Format, nothing more
    /// use for special string formatting like {0:X2}
    /// </summary>
    /// <param name="v"></param>
    /// <param name="a"></param>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    public static string Format4(string v, params object[] o)
    {
        return string.Format(v, o);
    }

    public static string Format5(string templateHandler, string lsf, string rsf, params string[] args)
    {
        // this was original implementation but dont know why isnt used string.format
        for (int i = 0; i < args.Length; i++)
        {
            templateHandler = SH.ReplaceAll2(templateHandler, args[i].ToString(), lsf + i + rsf);
        }


        return templateHandler;
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
    /// This can be only one 
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    public static string JoinIEnumerable(object delimiter, IEnumerable parts)
    {
        // TODO: Delete after all app working
        return JoinString(delimiter, parts);
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
        var enu =CA.ToEnumerable(parts);
        if (delimiter is IEnumerable && delimiter.GetType() != Types.tString)
        {
            var ie = (IEnumerable)delimiter;

            if (ie.Count() > 1 && enu.Count() == 1)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), sess.i18n(XlfKeys.ProbablyWasCalledWithSwithechDelimiterAndParts));
            }
        }

        // JoinString point to Join with implementation
        return Join(delimiter.ToString(), enu);
    }

    public static string JoinNL(IEnumerable parts)
    {
        return SH.JoinString(Environment.NewLine, parts);
    }
    public static string JoinNL(params string[] parts)
    {
        return SH.JoinString(Environment.NewLine, parts);
    }

    public static string JoinPairs(params object[] parts)
    {
        return JoinPairs(AllStrings.sc, AllStrings.cs, parts);
    }

    public static string JoinChars(params char[] ch)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in ch)
        {
            sb.Append(item);
        }
        return sb.ToString();
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
    /// Musi mit sudy pocet prvku
    /// Pokud sudý [0], [2], ... bude mít aspoň jeden nebílý znak, pak se přidá lichý [1], [3] i sudý ve dvojicích. jinak nic
    /// </summary>
    /// <param name="className"></param>
    /// <param name="v1"></param>
    /// <param name="methodName"></param>
    /// <param name="v2"></param>
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
    public static string Copy(string s)
    {
        return s;
    }

    /// <summary>
    /// Pokud je poslední znak v A1 A2, odstraním ho
    /// </summary>
    /// <param name="nazevTabulky"></param>
    /// <param name="p"></param>
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

    public static string WrapWithQm(string commitMessage)
    {
        return SH.WrapWith(commitMessage, AllChars.qm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, string h, bool _trimWrapping = false)
    {
        return h + SH.Trim( value, h) + h;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, char v, bool _trimWrapping = false)
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
    public static char[] spaceAndPuntactionChars = new char[] { AllChars.space, AllChars.dash, AllChars.dot, AllChars.comma, AllChars.sc, AllChars.colon, AllChars.excl, AllChars.q, '\u2013', '\u2014', '\u2010', '\u2026', '\u201E', '\u201C', '\u201A', '\u2018', '\u00BB', '\u00AB', '\u2019', AllChars.bs, AllChars.lb, AllChars.rb, AllChars.rsqb, AllChars.lsqb, AllChars.lcub, AllChars.rcub, '\u3008', '\u3009', AllChars.lt, AllChars.gt, AllChars.slash, AllChars.bs, AllChars.verbar, '\u201D', AllChars.qm, '~', '\u00B0', AllChars.plus, '@', '#', '$', AllChars.percnt, '^', '&', AllChars.asterisk, '=', AllChars.lowbar, '\u02C7', '\u00A8', '\u00A4', '\u00F7', '\u00D7', '\u02DD' };

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
        List<char> a = null;
        //,' | ',
        // removed char 18 - it was recognized as control char
        a = CA.ToList<char>('�', 'ل' , '€', '™', ':', '´', '', '\'','ṗ','`','§','←','↑','¡' , '↓','³','©', '¿','ƒ','¸','¹','а','ﬂ','і','Н','е','б','с','х','л','Е','ў','р','о','п','ы','®','С','ч','т','ь','н','д','ж','к','я','О','в','ю','Э','М','м','и','з','Б','ц','ш','В','Т','г','э','у','Д','Я','П','А','щ','Ю','И','Г','У','К','Ч','Р','З','Л','Х','ф','Ж','Ц','','Ṩ','¬','既','然','愛','讓','你','找','到','對','的','我','下','去','如','此','真','切','其','他','都','無','所','求','早','就','懼','任','何','危','險','該','一','起','往','前','走','卻','放','手','果','能','時','間','折','返','回','那','天','們','分','開','會','改','變','個','答','案','˜','ả','ấ','प','ा','ठ','न','ह','ी','ं','म','ि','ल','','‒','♥','み','ん','な','最','高','あ','り','が','と','う','か','わ','い','Λ','歪','だ','身','体','叫','び','出','す','痛','つ','け','る','汚','世','界','っ','た','翼','飛','べ','ら','支','配','恐','れ','偽','善','者','て','捨','ち','ま','え','よ','犠','牲','傷','言','葉','届','く','願','叶','光','奪','絆','終','誓','忘','こ','の','壊','も','☆','★','¯','­','″','Ṗ','±','⁕','ا','ه','γ','ζ','μ','α','ε','υ','ω','£','♫','。','♯','⚄','∞','ʇ','∑','ʼ','ª','¦','ˆ','¥','ɛ','ɑ','̃','ŋ','','·','∈','','','‽','♦','','','','ب','ن','ی','د','م','ع','ک','گ','ر','ف','ز','و','چ','،','ق','ت','ح','‌','′','','Þ','ﬁ','º','•','س','ج','ژ','ص','ټ','ړ','ښ','ĕ','Ф','ԁ','ѕ','Ɩ','ο','ɡ','','‡','‐','，','¶','閽','抳','抦','抰','抮','抯','扵','抣','','','̌','́','','','','²','♪','御','味','方','贈','物','は','同','じ','波','神','様','使','ǐ','ǎ','א','נ','י','ח','ו','ש','ב','ע','ל','ך','כ','ה','מ','ד','ת','צ','ק','ר','ג','ם','פ','ן','ז','ס','이','흐','름','을','타','새','로','운','길','함','께','천','히','올','라','진','실','닿','는','순','간','느','껴','봐','눈','감','아','서','필','꿈','과','현','내','손','잡','으','면','Ɏ','‹','¢','ϟ','˘','→','ə','ǰ','ḥ','ʾ','ṭ','ʿ','ṣ','ḍ','Ṣ','‬','ŭ','Ŭ','Ṭ','Ɛ','ɔ','Ɔ','ŗ','ǧ','ġ','ḏ','þ','ṅ','ẏ','ṃ','ṇ','ʹ','ḹ','ң','Қ','қ','ḫ','ų','ŵ','ħ','Ħ','ṛ','ẓ','̲','̤','Ẕ','ŏ','ʻ','Ḥ','İ','ệ','ứ','ố','ư','ớ','ồ','ờ','ậ','ề','ế','ắ','ừ','ữ','ơ','ầ','µ','❌','き','し','む','‰','⟓','娘','子','汉','精','采','‎','Μ','Ι','Α','Ο','Τ','Ν','Β','Η','、','†','ა','ფ','ხ','ზ','უ','რ','ი','ს','მ','ღ','ე','ნ','ბ','ლ','ო','Ш','є','Щ','І','Ь','Є','Ы','우','리','지','구','를','사','랑','해','요','们','爱','地','球','һ','ν','持','上','げ','解','','ー','「','」','❤','〝','〟','姫','僕','未','来','ァ','♂','⇒','ッ','星','在','処','‪','‏','笑','容','兩','點','鐘','用','睫','毛','剪','接','每','舉','動','髮','飄','節','奏','像','是','獨','幫','伴','心','房','季','沒','有','春','秋','冬','炎','熱','夏','曬','臉','蛋','紅','晶','瑩','眼','眸','情','書','般','剔','透','交','給','來','拆','封','慢','著','作','定','格','輪','廓','品','嚐','互','緩','掌','控','完','美','瑕','火','候','忍','住','呼','吸','還','氣','相','投','即','不','邊','也','記','朦','朧','秒','喚','玫','瑰','香','漫','游','頭','要','成','功','闖','關','迷','宮','音','樂','噗','通','板','胸','懷','脈','調','保','證','跟','別','人','絕','與','眾','吻','家','普','侯','空','轉','陪','夢','受','寵','為','訂','做','專','屬','私','宇','宙','⁠','►','ι','λ','ς','ἴ','½','さ','で','に','ず','ど','へ','見','を','め','嘘','お','絶','妙','バ','ラ','ン','ス','首','皮','枚','ア','ダ','ル','ト','ツ','背','向','ば','ほ','合','離','そ','形','確','探','明','日','君','怖','ぐ','強','深','入','関','係','繋','糸','赤','せ','マ','ジ','ク','種','ウ','ソ','重','ね','術','立','尽','ぶ','声','エ','グ','感','涙','二','引','冷','独','ロ','リ','ナ','遠','寄','道','意','・','全','覚','カ','サ','ブ','タ','欠','落','オ','メ','シ','ョ','キ','ミ','イ','列','車','混','雑','コ','ュ','ニ','ケ','信','降','止','雨','響','曲','現','代','失','度','目','気','づ','ワ','デ','望','や','希','鏡','映','問','自','誤','魔','化','生','酷','鼓','景','色','掴','','♚','ℑ','ℵ','ℜ','̆','Ｉ','Ｍ','Ａ','Ｙ','Ｔ','̈','̊','外','国','','','¼','歌','｢','？','‿','Φ','Γ','Ε','Σ','Κ','Υ','Ξ','Χ','Π','Δ','Ρ','Θ','Ζ','Ω','Մ','ե','ն','ք','մ','ի','շ','տ','ա','պ','ր','լ','յ','ս','հ','ո','ղ','ւ','Ք','ձ','դ','Ս','բ','խ','գ','ց','կ','ծ','ռ','Հ','վ','Ա','զ','Դ','չ','ը','ժ','թ','է','օ','։','Գ','Վ','Ի','Տ','Բ','Ե','Օ','Թ','ջ','Ռ','￼','好','想','听','现','哪','里','当','睡','正','醒','承','了','对','思','念','闭','看','脸','已','经','算','清','时','间','让','己','忙','得','没','隙','否','则','脑','袋','面','特','别','多','和','个','太','阳','升','얼','어','붙','버','린','팔','찌','목','에','음','장','식','난','바','닷','물','다','섭','취','마','치','범','고','래','된','것','같','나','셀','수','없','돈','원','보','조','르','려','걸','니','방','울','의','온','몸','주','먹','석','굴','모','양','별','귀','끊','임','뿜','빛','들','은','친','피','날','하','늘','위','너','무','가','까','워','여','긴','소','공','포','증','불','안','정','통','절','대','오','않','뭄','명','품','전','부','달','쳐','본','두','쥐','봉','적','향','쏜','기','옥','근','저','멀','Ј','ј','ћ','љ','њ','ђ','','ᴏ','ʀ','ᴅ','΄','ḃ','ṫ','ṁ','ḋ','䨬','䳥','ߣ','ߠ','䲠','䨲','ߥ','詞','','','ي','ك','‫','','٧','ط','ّ','ሠ','ላ','ም','ለ','ኪ','؟','ܝ','ܘ','ܣ','ܦ','ܟ','ܢ','ܐ','χ','τ','σ','π','ρ','κ','η','δ','ξ','φ','ψ','β','θ','Ў','燦','爛','聖','夜','靜','ھ','ہ','ے','؛','慍','扴','慠','ǒ','ǔ','ǚ','蓝','（','棒');
        #region Added as int because its control chars and isBinary could return true => replace in files wouldnt working
        a.Add((char)1);
        a.Add((char)20);
        a.Add((char)159);
        a.Add((char)3);
        a.Add((char)2); 
        #endregion

        spaceAndPuntactionCharsAndWhiteSpacesList.AddRange(a);

        CA.RemoveDuplicitiesList<char>(spaceAndPuntactionCharsAndWhiteSpacesList);

        s_spaceAndPuntactionCharsAndWhiteSpaces = spaceAndPuntactionCharsAndWhiteSpacesList.ToArray();
    }

    private static char[] s_spaceAndPuntactionCharsAndWhiteSpaces = null;
    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string s)
    {
        return s.Split(s_spaceAndPuntactionCharsAndWhiteSpaces).ToList();
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
            sb.Append(SH.ListToString(item, p) + p);
        }
        return sb.ToString();
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
            if (text.Length > pozice+1)
            {
                za = text.Substring(pozice + 1);
            }
            else
            {
                za = string.Empty;
            }
        }
    }

    public static string JoinMakeUpTo2NumbersToZero(object p, params int[] parts)
    {
        List<string> na2Cislice = new List<string>();
        foreach (var item in parts)
        {
            na2Cislice.Add(NH.MakeUpTo2NumbersToZero(item));
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
    public static bool IsChinese(string text)
    {
        var hiragana = GetCharsInRange(text, 0x3040, 0x309F);
        if (hiragana)
        {
            return true;
        }
        var katakana = GetCharsInRange(text, 0x30A0, 0x30FF);
        if (katakana)
        {
            return true;
        }
        var kanji = GetCharsInRange(text, 0x4E00, 0x9FBF);
        if (kanji)
        {
            return true;
        }

        if (text.Any(c => c >= 0x20000 && c <= 0xFA2D))
        {
            return true;
        }

        return false;
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

    /// <summary>
    /// Nevraci znaky na indexech ale zda nektere znaky maji rozsah char definovany v A2,3
    /// </summary>
    /// <param name="text"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static bool GetCharsInRange(string text, int min, int max)
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

    public static string Substring(string sql, int indexFrom, int indexTo, bool returnInputIfInputIsShorterThanA3 = false)
    {
        return Substring(sql, indexFrom, indexTo, new SubstringArgs { returnInputIfInputIsShorterThanA3 = returnInputIfInputIsShorterThanA3 });
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

    public static List<string> RemoveDuplicatesNone(string p1, string delimiter)
    {
        var split = SH.SplitNone(p1, delimiter);
        return CA.RemoveDuplicitiesList<string>(split);
    }

    public static string JoinSpace(IEnumerable parts)
    {
        return SH.JoinString(AllStrings.space, parts);
    }

    /// <summary>
    /// Most of Love me like you do have in title - from Fifty shades of grey
    /// </summary>
    /// <param name="title"></param>
    /// <param name="squareBrackets"></param>
    /// <param name="parentheses"></param>
    /// <param name="braces"></param>
    /// <param name="afterSds"></param>
    public static string RemoveBracketsAndHisContent(string title, bool squareBrackets, bool parentheses, bool braces, bool afterSdsFrom)
    {
        if (squareBrackets)
        {
            title = RemoveBetweenAndEdgeChars(title, AllChars.rsqb, AllChars.lsqb);
        }
        if (parentheses)
        {
            title = RemoveBetweenAndEdgeChars(title, AllChars.lb, AllChars.rb);
        }
        if (braces)
        {
            title = RemoveBetweenAndEdgeChars(title, AllChars.lcub, AllChars.rcub);
        }
        if (afterSdsFrom)
        {
            var dex = title.IndexOf(" - from");
            if (dex == -1)
            {
                dex = title.IndexOf(SunamoNotTranslateAble.From);
            }
            if (dex != -1)
            {
                title = title.Substring(0, dex + 1);
            }
        }
        title = ReplaceAll(title, "", AllStrings.doubleSpace).Trim();
        return title;
    }

    /// <summary>
    /// A2,3 can be string or char
    /// </summary>
    /// <param name="s"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public static string RemoveBetweenAndEdgeChars(string s, object begin, object end)
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
    public static List<string> SplitBySpaceAndPunctuationCharsLeave(string veta)
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
        return vr;
    }

    /// <summary>
    /// Vše tu funguje výborně
    /// G text z A1, ktery bude obsahovat max A2 písmen - ne slov, protože někdo tam může vložit příliš dlouhé slova a nevypadalo by to pak hezky.
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
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

    //
    public static string TrimNewLineAndTab(string lyricsFirstOriginal, bool replaceDoubleSpaceForSingle = false)
    {
        var result = lyricsFirstOriginal.Replace("\t", AllStrings.space).Replace("\r", AllStrings.space).Replace("\n", AllStrings.space).Replace(AllStrings.doubleSpace, AllStrings.space);
        if (replaceDoubleSpaceForSingle)
        {
            result = SH.ReplaceAllDoubleSpaceToSingle(result);
        }
        return result;
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

    /// <summary>
    /// Replace AllChars.whiteSpacesChars with A2
    /// </summary>
    /// <param name="s"></param>
    /// <param name="forWhat"></param>
    /// <returns></returns>
    public static string ReplaceWhitespaces(string s, string forWhat)
    {
        foreach (var item in AllChars.whiteSpacesChars)
        {
            s = s.Replace(item.ToString(), forWhat);
        }

        return s;
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

    /// <summary>
    /// Work like everybody excepts, from a {b} c return b
    /// </summary>
    /// <param name="p"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public static string GetTextBetweenTwoChars(string p, int begin, int end)
    {
        if (end > begin)
        {
            // a(1) - 1,3
            return p.Substring(begin + 1, end - begin - 1);
            // originally
            //return p.Substring(begin+1, end - begin - 1);
        }
        return p;
    }

    public static string ReplaceWhiteSpacesWithoutSpaces(string p)
    {
        return ReplaceWhiteSpacesWithoutSpaces(p, "");
    }

    /// <summary>
    /// Replace r,n,t with A2
    /// </summary>
    /// <param name="p"></param>
    /// <param name="replaceWith"></param>
    /// <returns></returns>
    public static string ReplaceWhiteSpacesWithoutSpaces(string p, string replaceWith = "")
    {
        return p.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    public static List<string> SplitAdvanced(string v, bool replaceNewLineBySpace, bool moreSpacesForOne, bool _trim, bool escapeQuoations, params string[] deli)
    {
        var s = SH.Split(v, deli);
        if (replaceNewLineBySpace)
        {
            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceAll(s[i], AllStrings.space, "\r", @"\n", Environment.NewLine);
            }
        }
        if (moreSpacesForOne)
        {
            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceAll2(s[i], AllStrings.space, AllStrings.doubleSpace);
            }
        }
        if (_trim)
        {
            s = CA.Trim(s);
        }
        if (escapeQuoations)
        {
            string rep = AllStrings.qm;

            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceFromEnd(s[i], "\"", rep);
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

    /// <summary>
    /// Pokud něco nebude číslo, program vyvolá výjimku, protože parsuje metodou int.Parse
    /// </summary>
    /// <param name="stringToSplit"></param>
    /// <param name="delimiter"></param>
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
    /// If A1 dont have index A2, all chars
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="deli"></param>
    public static string GetFirstPartByLocation(string p1, char deli)
    {
        int dx = p1.IndexOf(deli);

        return GetFirstPartByLocation(p1, dx);
    }

    public static string GetFirstPartByLocation(string p1, int dx)
    {
        string p, z;
        p = p1;

        

        if (dx < p1.Length)
        {
            GetPartsByLocation(out p, out z, p1, dx);
        }
        
        return p;
    }

/// <summary>
    /// return whether A1 ends with anything with A2
    /// </summary>
    /// <param name="source"></param>
    /// <param name="p2"></param>
    public static bool EndsWithArray(string source, params string[] p2)
    {
        foreach (var item in p2)
        {
            if (source.EndsWith(item))
            {
                return true;
            }
        }
        return false;
    }

public static string GetTextBetween(string p, string after, string before, bool throwExceptionIfNotContains = true)
    {
        string vr = null;
        int p2 = p.IndexOf(after);
        int p3 = p.IndexOf(before);
        bool b2 = p2 != -1;
        bool b3 = p3 != -1;
        if (b2 && b3)
        {
            p2 += after.Length;
            p3 -= 1;
            // When I return between ( ), there must be +1 
            var length = p3 - p2 + 1;
            if (length < 1)
            {
                return p;
            }
            vr = p.Substring(p2, length).Trim();
        }
        else
        {
            if (throwExceptionIfNotContains)
            {
                ThrowExceptions.NotContains(null, type, "GetTextBetween", p, after, before);
            }
        }

        return vr;
    }

public static bool EndsWith(string input, string endsWith)
    {
        return input.EndsWith(endsWith);
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

public static string JoinDictionary(IDictionary<string, string> dict, string delimiterBetweenKeyAndValue, string delimAfter)
    {
        return JoinKeyValueCollection(dict.Keys, dict.Values, delimiterBetweenKeyAndValue, delimAfter);
    }
public static string JoinDictionary(Dictionary<string, string> dictionary, string delimiter)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in dictionary)
        {
            sb.AppendLine(item.Key + delimiter + item.Value);
        }
        return sb.ToString();
    }

public static string JoinKeyValueCollection(IEnumerable v1, IEnumerable v2, string delimiterBetweenKeyAndValue, string delimAfter)
    {
        StringBuilder sb = new StringBuilder();
        var v2List = new List<object>(v2.Count());
        foreach (var item in v2)
        {
            v2List.Add(item);
        }
        int i = 0;
        foreach (var item in v1)
        {
            sb.Append(item + delimiterBetweenKeyAndValue + v2List[i++] + delimAfter);
        }

        return SH.TrimEnd(sb.ToString(), delimAfter);
    }

public static bool RemovePrefix(ref string s, string v)
    {
        if (s.StartsWith(v))
        {
            s = s.Substring(v.Length);
            return true;
        }
        return false;
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

public static string GetToFirstChar(string input, int indexOfChar)
    {
        if (indexOfChar != -1)
        {
            return input.Substring(0, indexOfChar + 1);
        }
        return input;
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
    /// Tato metoda byla výchozí, jen se jmenovala NullToString
    /// OrEmpty pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="v"></param>
    public static string NullToStringOrEmpty(object v)
    {
        if (v == null)
        {
            return "";
        }
        var s = v.ToString();
        return s;
    }

public static bool ContainsFromEnd(string p1, char p2, out int ContainsFromEndResult)
    {
        for (int i = p1.Length - 1; i >= 0; i--)
        {
            if (p1[i] == p2)
            {
                ContainsFromEndResult = i;
                return true;
            }
        }
        ContainsFromEndResult = -1;
        return false;
    }

/// <summary>
    /// FUNGUJE ale může být pomalá, snaž se využívat co nejméně
    /// Pokud někde bude více delimiterů těsně za sebou, ve výsledku toto nebude, bude tam jen poslední delimiter v té řadě příklad z 1,.Par při delimiteru , a . bude 1.Par
    /// </summary>
    /// <param name="what"></param>
    /// <param name="parts"></param>
    /// <param name="deli"></param>
    public static List<string> SplitToPartsFromEnd(string what, int parts, params char[] deli)
    {
        List<char> chs = null;
        List<bool> bw = null;
        List<int> delimitersIndexes = null;
        SH.SplitCustom(what, out chs, out bw, out delimitersIndexes, deli);

        List<string> vr = new List<string>(parts);
        StringBuilder sb = new StringBuilder();
        for (int i = chs.Count - 1; i >= 0; i--)
        {
            if (!bw[i])
            {
                while (i != 0 && !bw[i - 1])
                {
                    i--;
                }
                string d = sb.ToString();
                sb.Clear();
                if (d != "")
                {
                    vr.Add(d);
                }
            }
            else
            {
                sb.Insert(0, chs[i]);
                //sb.Append(chs[i]);
            }
        }
        string d2 = sb.ToString();
        sb.Clear();
        if (d2 != "")
        {
            vr.Add(d2);
        }
        List<string> v = new List<string>(parts);
        for (int i = 0; i < vr.Count; i++)
        {
            if (v.Count != parts)
            {
                v.Insert(0, vr[i]);
            }
            else
            {
                string ds = what[delimitersIndexes[i - 1]].ToString();
                v[0] = vr[i] + ds + v[0];
            }
        }
        return v;
    }

/// <summary>
    /// V A2 vrátí jednotlivé znaky z A1, v A3 bude false, pokud znak v A2 bude delimiter, jinak True
    /// </summary>
    /// <param name="what"></param>
    /// <param name="chs"></param>
    /// <param name="bs"></param>
    /// <param name="reverse"></param>
    /// <param name="deli"></param>
    public static void SplitCustom(string what, out List<char> chs, out List<bool> bs, out List<int> delimitersIndexes, params char[] deli)
    {
        chs = new List<char>(what.Length);
        bs = new List<bool>(what.Length);
        delimitersIndexes = new List<int>(what.Length / 6);
        for (int i = 0; i < what.Length; i++)
        {
            bool isNotDeli = true;
            var ch = what[i];
            foreach (var item in deli)
            {
                if (item == ch)
                {
                    delimitersIndexes.Add(i);
                    isNotDeli = false;
                    break;
                }
            }
            chs.Add(ch);
            bs.Add(isNotDeli);
        }
        delimitersIndexes.Reverse();
    }

public static string FirstWhichIsNotEmpty(params string[] s)
    {
        foreach (var item in s)
        {
            if (item != "")
            {
                return item;
            }
        }
        return "";
    }

/// <summary>
    /// Whether A1 is under A2
    /// </summary>
    /// <param name="name"></param>
    /// <param name="mask"></param>
    public static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, AllChars.q, AllChars.asterisk);
    }

private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat)
        {
            return true;
        }

        string escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        string escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, AllStrings.dot);
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        Regex reg = new Regex(pat);
        return reg.IsMatch(str);
    }

/// <summary>
    /// 
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
    /// Return joined with space
    /// </summary>
    /// <param name="v"></param>
    public static string FirstCharOfEveryWordUpperDash(string v)
    {
        return FirstCharOfEveryWordUpper(v, AllChars.dash);
    }

    /// <summary>
    /// Return joined with space
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dash"></param>
    private static string FirstCharOfEveryWordUpper(string v, char dash)
    {
        var p = SH.Split(v, dash);
        CA.FirstCharUpper(p);
        return SH.JoinSpace(p);
    }

    /// <summary>
    /// FixedSpace - Contains
    /// AnySpaces - split input by spaces and A1 must contains all parts
    /// ExactlyName - Is exactly the same
    /// 
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="term"></param>
    /// <param name="enoughIsContainsAttribute"></param>
    /// <param name="caseSensitive"></param>
    public static bool Contains(string input, string term, bool enoughIsContainsAttribute, bool caseSensitive)
    {
        return Contains(input, term, enoughIsContainsAttribute ? SearchStrategy.AnySpaces : SearchStrategy.ExactlyName, caseSensitive);
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
    /// AnySpaces - split A2 by spaces and A1 must contains all parts
    /// ExactlyName - ==
    /// FixedSpace - simple contains
    /// 
    /// A1 = search for exact occur. otherwise split both to words
    /// Control for string.Empty, because otherwise all results are true
    /// </summary>
    /// <param name="input"></param>
    /// <param name="what"></param>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        return Contains(input, term, searchStrategy, true);
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
        else if(ccm == ContainsCompareMethod.WholeInput)
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

public static string PrefixIfNotStartedWith( string item, string http)
    {
        if (!item.StartsWith(http))
        {
            return http + item;
        }
        return item;
    }

    public static bool IsNullOrWhiteSpace(string s)
    {
        if (s != null)
        {
            s = s.Trim();
            return s == "";
        }
        return true;
    }

    public static string ReplaceRef(ref string resultStatus, string what, string forWhat)
    {
        resultStatus = resultStatus.Replace(what, forWhat);
        return resultStatus;
    }



public static bool HasTextRightFormat(string r, TextFormatData tfd)
    {
        if (tfd.trimBefore)
        {
            r = r.Trim();
        }

        int partsCount = tfd.Count;

        int actualCharFormatData = 0;
        CharFormatData actualFormatData = tfd[actualCharFormatData];
        CharFormatData followingFormatData = tfd[actualCharFormatData + 1];
        //int charCount = r.Length;
        //if (tfd.requiredLength != -1)
        //{
        //    if (r.Length != tfd.requiredLength)
        //    {
        //        return false;
        //    }
        //    charCount = Math.Min(r.Length, tfd.requiredLength);
        //}
        int actualChar = 0;
        int processed = 0;
        int from = actualFormatData.fromTo.from;
        int remains = actualFormatData.fromTo.to;
        int tfdCountM1 = tfd.Count - 1;

        while (true)
        {
            bool canBeAnyChar = CA.IsEmptyOrNull(actualFormatData.mustBe);
            bool isRightChar = false;
            if (canBeAnyChar)
            {
                isRightChar = true;
                remains--;
            }
            else
            {
                if (!CA.HasIndex(actualChar, r))
                {
                    return false;
                }
                isRightChar = CA.IsEqualToAnyElement<char>(r[actualChar], actualFormatData.mustBe);
                if (isRightChar && !canBeAnyChar)
                {
                    actualChar++;
                    processed++;
                    remains--;
                }
            }

            if (!isRightChar)
            {
                if (!CA.HasIndex(actualChar, r))
                {
                    return false;
                }
                isRightChar = CA.IsEqualToAnyElement<char>(r[actualChar], followingFormatData.mustBe);
                if (!isRightChar)
                {
                    return false;
                }
                if (remains != 0 && processed < from)
                {
                    return false;
                }
                if (isRightChar && !canBeAnyChar)
                {
                    actualCharFormatData++;
                    processed++;
                    actualChar++;

                    if (!CA.HasIndex(actualCharFormatData, tfd) && r.Length > actualChar)
                    {
                        return false;
                    }

                    actualFormatData = tfd[actualCharFormatData];
                    if (CA.HasIndex(actualCharFormatData + 1, tfd))
                    {
                        followingFormatData = tfd[actualCharFormatData + 1];
                    }
                    else
                    {
                        followingFormatData = CharFormatData.Templates.Any;
                    }

                    processed = 0;
                    remains = actualFormatData.fromTo.to;
                    remains--;
                }
            }

            if (remains == 0)
            {
                ++actualCharFormatData;
                if (!CA.HasIndex(actualCharFormatData, tfd) && r.Length > actualChar)
                {
                    return false;
                }
                actualFormatData = tfd[actualCharFormatData];
                if (CA.HasIndex(actualCharFormatData + 1, tfd))
                {
                    followingFormatData = tfd[actualCharFormatData + 1];
                }
                else
                {
                    followingFormatData = CharFormatData.Templates.Any;
                }

                processed = 0;
                remains = actualFormatData.fromTo.to;
            }

            //if (actualCharFormatData == tfdCountM1 && isRightChar && actualChar )
            //{
            //    break;
            //}
        }
    }

/// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="append"></param>
    public static string AppendIfDontEndingWith(string text, string append)
    {
        if (text.EndsWith(append))
        {
            return text;
        }
        return text + append;
    }

    public static string ReplaceAllDoubleSpaceToSingle(string text)
    {
        return ReplaceAllDoubleSpaceToSingle(text, false);
    }

public static string ReplaceAllDoubleSpaceToSingle(string text, bool alsoHtml = false)
    {
        text = SH.FromSpace160To32(ref text);

        if (alsoHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        while (text.Contains(AllStrings.doubleSpace))
        {
            text = SH.ReplaceAll2(text, AllStrings.space, AllStrings.doubleSpace);
        }

        // Here it was cycling, dont know why, therefore without while
        //while (text.Contains(AllStrings.doubleSpace16032))
        //{
            //text = SH.ReplaceAll2(text, AllStrings.space, AllStrings.doubleSpace16032);
        //}

        //while (text.Contains(AllStrings.doubleSpace32160))
        //{
            //text = SH.ReplaceAll2(text, AllStrings.space, AllStrings.doubleSpace32160);
        //}

        return text;
    }

public static string FromSpace160To32(ref string text)
    {
        text = Regex.Replace(text, @"\p{Z}", AllStrings.space);
        return text;
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
}