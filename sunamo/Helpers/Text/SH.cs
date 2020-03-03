using sunamo;
using sunamo.Collections;
using sunamo.Constants;
using sunamo.Delegates;
using sunamo.Enums;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

public static partial class SH
{
    public static string ConvertWhitespaceToVisible(string t)
    {
        t = t.Replace(AllChars.tab, UnicodeWhiteToVisible.tab);
        t = t.Replace(AllChars.nl, UnicodeWhiteToVisible.newLine);
        t = t.Replace(AllChars.cr, UnicodeWhiteToVisible.carriageReturn);
        t = t.Replace(AllChars.space, UnicodeWhiteToVisible.space);
        return t;
    }
    public static string ReplaceAll3(IList<string> replaceFrom, IList<string> replaceTo, bool isMultilineWithVariousIndent, string content)
    {

        for (int i = 0; i < replaceFrom.Count; i++)
        {
            /*
              Vše zaměnit na 1 mezeru
              porovnat zaměněné a originál - namapovat co je mezi nimi
            */

            if (isMultilineWithVariousIndent)
            {
                var r = SH.SplitByWhiteSpaces(replaceFrom[i], true);

                var contentOneSpace = SH.SplitByWhiteSpaces(content, true);

                ////DebugLogger.Instance.WriteNumberedList("", contentOneSpace, true);

                // get indexes
                List<FromTo> equalRanges = CA.EqualRanges(contentOneSpace, r);

                if (equalRanges.Count == 0)
                {
                    return content;
                }

                int startDx = equalRanges.First().from;
                int endDx = equalRanges.Last().to;

                // všechny elementy z contentOneSpace namapované na content kde v něm začínají. 
                // index z nt odkazuje na content
                // proto musím vzít první a poslední index z equalRanges a k poslednímu přičíst contentOneSpace[last].Length
                List<int> nt = new List<int>(contentOneSpace.Count());

                int startFrom = 0;
                foreach (var item2 in contentOneSpace)
                {
                    var dx = content.IndexOf(item2, startFrom);
                    startFrom = dx + item2.Length;
                    nt.Add(dx);
                }

                StringBuilder replaceWhat = new StringBuilder();
                // Now I must iterate and add also white chars between
                //foreach (var ft in equalRanges)
                //{
                //    // Musím vzít index z nt
                //}

                int add = contentOneSpace[endDx].Length;
                startDx = nt[startDx];
                endDx = nt[endDx];
                endDx += add;

                var from2 = content.Substring(startDx, endDx - startDx);

                content = content.Replace(from2, replaceTo[i]);
            }
            else
            {
                if (SH.ContainsAny(content, false, replaceFrom).Count > 0)
                {
                    content = content.Replace(replaceFrom[i], replaceTo[i]);
                }
            }
        }

        return content;
    }

    public static string ConcatSpace(IEnumerable r)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string item in r)
        {
            sb.Append(item + AllStrings.space);
        }
        return sb.ToString();
    }
    public static bool IsNullOrWhiteSpaceRange(params string[] l)
    {
        foreach (string item in l)
        {
            if (IsNullOrWhiteSpace(item))
            {
                return true;
            }
        }
        return false;
    }

    public static string SplitParagraphToMaxChars(string text, int maxChars)
    {
        var parts = SH.Split(text, Environment.NewLine);
        List<List<string>> d = new List<List<string>>();

        foreach (var item in parts)
        {
            d.Add(CA.ToListString(item));
        }

        var index = -1;

        foreach (var item in d)
        {
            bool d1 = false;

            index++;

            List<string> copy = item.ToList();

            var s1 = item[0];
            var f = s1.Length;

            if (f > maxChars)
            {
                var dxDots = SH.ReturnOccurencesOfString(s1, AllStrings.dot);
                int i = 0;
                int dx = 0;
                int alreadyProcessed = 0;
                int alreadyTrimmed = 0;
                foreach (var dxDot2 in dxDots)
                {
                    i++;
                    var dxDot = dxDot2 - alreadyTrimmed;

                    int s1C = s1.Length;

                    if (s1C > maxChars)
                    {
                        if (i > 1)
                        {
                            if (dxDot > maxChars)
                            {
                                string delimitingChars = null;
                                if (IsEndOfSentence(dxDot, s1, out delimitingChars))
                                {
                                    string before, after;

                                    // Zde jsem dal -1 místo -2, vracelo mi to na začátku rok
                                    // Může mi to občas přetáhnout limit 250 znaků ale furt je to lepší než mít na začátku rok
                                    var ddx = dxDots[i - 1] + 1;
                                    ddx -= alreadyTrimmed;
                                    SH.GetPartsByLocation(out before, out after, s1, ddx);

                                    after = after.Trim();

                                    if (after == string.Empty)
                                    {
                                        after = "   ";
                                    }

                                    if (char.IsLower(after[0]))
                                    {
                                        continue;
                                    }

                                    //dx++;
                                    alreadyProcessed++;

                                    ////DebugLogger.Instance.WriteLine("dx", dx);
                                    ////DebugLogger.Instance.WriteLine("alreadyProcessed", alreadyProcessed);
                                    ////DebugLogger.Instance.WriteLine("dx-alreadyProcessed", dx - alreadyProcessed);

                                    if (dx > 1)
                                    {

                                        dx--;
                                    }

                                    //dx -= alreadyProcessed;


                                    if (d1)
                                    {
                                        ////DebugLogger.Instance.WriteLine("i", i);
                                    }




                                    s1 = s1.Substring(ddx);

                                    //after = delimitingChars + after;

                                    int beforeC, afterC;
                                    beforeC = before.Length;
                                    afterC = after.Length;
                                    s1C = s1.Length;

                                    alreadyTrimmed += beforeC;
                                    if (d1)
                                    {
                                        ////DebugLogger.Instance.WriteLine("beforeC", beforeC);
                                        ////DebugLogger.Instance.WriteLine("afterC", afterC);
                                        ////DebugLogger.Instance.WriteLine("s1C", s1C);
                                    }

                                    var ls = d[index];



                                    if (d1)
                                    {
                                        var bC = SH.OccurencesOfStringIn(before, "Tento odstavec má vice než 500 znaků.");
                                        var aC = SH.OccurencesOfStringIn(after, "Tento odstavec má vice než 500 znaků.");
                                        ////DebugLogger.Instance.WriteLine("bC", bC);
                                        ////DebugLogger.Instance.WriteLine("aC", aC);
                                    }

                                    if (dx < 0)
                                    {
                                        StringBuilder sb2 = new StringBuilder();

                                        foreach (var item3 in ls)
                                        {
                                            sb2.AppendLine(item3);
                                        }

                                        var txt = sb2.ToString();
                                        ClipboardHelper.SetText(txt);
                                        int r = 0;
                                    }

                                    ls.AddOrSet(dx, before);
                                    dx++;

                                    ls.AddOrSet(dx, after);
                                    dx++;

                                    if (d1)
                                    {
                                        ////DebugLogger.Instance.WriteLine("dx1", dx - 1);
                                        ////DebugLogger.Instance.WriteLine("dx2", dx);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                        var ls = d[index];
                        s1 = s1.Replace(ls.Last(), string.Empty).Trim();
                        if (s1 != string.Empty)
                        {
                            ls.AddOrSet(dx, s1);
                        }


                        break;
                    }
                }
            }
        }

        StringBuilder sb = new StringBuilder();

        foreach (var item in d)
        {
            foreach (var line in item)
            {
                Debug.WriteLine(line.Length);

                sb.AppendLine(line);

                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    static bool _result = false;

    public static bool Result
    {
        get
        {
            return _result;
        }
        set
        {
            _result = value;
        }
    }

    private static bool IsEndOfSentence(int dxDot, string s1, out string delimitingChars)
    {
        delimitingChars = null;
        var s = s1.Substring(dxDot);

        var c0 = s[0];
        char c1, c2;
        c1 = '@';
        c2 = '@';

        if (s.Length > 1)
        {
            c1 = s[1];
        }
        else
        {
            delimitingChars = s.Substring(0);
            Result = true;
        }

        if (s.Length > 2)
        {
            c2 = s[2];
        }
        else
        {
            delimitingChars = s.Substring(1);
            Result = true;
        }


        if (c1 == AllChars.space && char.IsUpper(c2))
        {
            delimitingChars = SH.JoinChars(c0, c1, c2);
            Result = true;
        }
        if (char.IsUpper(c1))
        {
            delimitingChars = SH.JoinChars(c0, c1);
            Result = true;
        }
        return Result;
    }


    public static string GetWhitespaceFromBeginning(StringBuilder sb, string line)
    {
        sb.Clear();
        foreach (var item in line)
        {
            if (char.IsWhiteSpace(item))
            {
                sb.Append(item);
            }
            else
            {
                break;
            }
        }
        return sb.ToString();
    }

    public static IEnumerable<string> SplitAndKeep(this string s, List<string> delims)
    {
        // delims allow only char[], not List<string>

        //int start = 0, index;
        //string selectedSeperator = null;
        //while ((index = s.IndexOfAny(delims, start, out selectedSeperator)) != -1)
        //{
        //    if (selectedSeperator == null)
        //        continue;
        //    if (index - start > 0)
        //        yield return s.Substring(start, index - start);
        //    yield return s.Substring(index, selectedSeperator.Length);
        //    start = index + selectedSeperator.Length;
        //}

        //if (start < s.Length)
        //{
        //    yield return s.Substring(start);
        //}

        return null;
    }

    public static bool IsCharOn(string item, int v, UnicodeChars number)
    {
        if (item.Length > v)
        {
            return IsUnicodeChar(number, item[v]);
        }
        return false;
    }

    /// <summary>
    /// A2 is use to calculate length of center
    /// </summary>
    /// <param name="text"></param>
    /// <param name="centerString"></param>
    /// <param name="centerIndex"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    
    public static string JoinToIndex(int dex, object delimiter2, IEnumerable parts)
    {
        string delimiter = delimiter2.ToString();
        StringBuilder sb = new StringBuilder();
        int i = 0;
        foreach (var item in parts)
        {
            if (i < dex)
            {
                sb.Append(item + delimiter);
            }

            i++;
        }
        string vr = sb.ToString();
        return vr.Substring(0, vr.Length - 1);
    }


    public static string JoinWithoutEndTrimDelimiter(object name, params string[] parts)
    {
        // TODO: Delete after making all solutions working
        return JoinWithoutTrim(name, parts);
    }
}