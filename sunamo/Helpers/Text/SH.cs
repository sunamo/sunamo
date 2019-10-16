﻿using sunamo;
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

                    if (s1C> maxChars)
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

                                    DebugLogger.Instance.WriteLine("dx", dx);
                                    DebugLogger.Instance.WriteLine("alreadyProcessed", alreadyProcessed);
                                    DebugLogger.Instance.WriteLine("dx-alreadyProcessed", dx-alreadyProcessed);

                                    if (dx > 1)
                                    {

                                        dx--;
                                    }

                                    //dx -= alreadyProcessed;


                                    if (d1)
                                    {
                                        DebugLogger.Instance.WriteLine("i", i);
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
                                        DebugLogger.Instance.WriteLine("beforeC", beforeC);
                                        DebugLogger.Instance.WriteLine("afterC", afterC);
                                        DebugLogger.Instance.WriteLine("s1C", s1C);
                                    }
                                    
                                    var ls = d[index];

                                    

                                    if (d1)
                                    {
                                        var bC = SH.OccurencesOfStringIn(before, "Tento odstavec má vice než 500 znaků.");
                                        var aC = SH.OccurencesOfStringIn(after, "Tento odstavec má vice než 500 znaků.");
                                        DebugLogger.Instance.WriteLine("bC", bC);
                                        DebugLogger.Instance.WriteLine("aC", aC);
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
                                        DebugLogger.Instance.WriteLine("dx1", dx - 1);
                                        DebugLogger.Instance.WriteLine("dx2", dx);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        
                        var ls = d[index];
                        s1 = s1.Replace(ls.Last(), string.Empty).Trim() ;
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
                Result =  true;
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

    public static IEnumerable<string> SplitAndKeep(this string s, string[] delims)
    {
        // delims allow only char[], not string[]

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
    /// <returns></returns>
    public static object CharsBeforeAndAfter(string text, string centerString, int centerIndex, int before, int after)
    {
        var b = centerIndex - before;
        var a = centerIndex + centerString.Length + after;

        if (ThisApp.check)
        {
        }

        StringBuilder sb = new StringBuilder();

        if (HasIndex(b, text, false))
        {
            sb.Append(text.Substring(b, before));
            sb.Append(AllStrings.space);
        }

        sb.Append(centerString);

        if (HasIndex(a, text, false))
        {
            sb.Append(text.Substring(a, after));
            sb.Append(AllStrings.space);
        }

        return sb.ToString();
    }



    /// <summary>
    /// Return index, therefore x-1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static int GetLineIndexFromCharIndex(string input, int pos)
    {
        var lineNumber = input.Take(pos).Count(c => c == '\n') + 1;
        return lineNumber - 1;
    }

    public static string ReplaceManyFromString(string input, string v, string delimiter)
    {
        string methodName = "ReplaceManyFromString";
        var l = SH.GetLines(v);
        foreach (var item in l)
        {
            var p = SH.Split(item, delimiter);
            CA.Trim(p);
            string from, to;
            from = to = null;

            if (p.Count > 0)
            {
                from = p[0];
            }
            else
            {
                ThrowExceptions.Custom(s_type, methodName, item + " " + "hasn't from");
            }

            if (p.Length() > 1)
            {
                to = p[1];
            }
            else
            {
                ThrowExceptions.Custom(s_type, methodName, item + " " + "hasn't to");
            }

            if (SH.IsWildcard(item))
            {
                Wildcard wc = new Wildcard(from);
                var occurences = wc.Matches(input);
                foreach (Match m in occurences)
                {
                    var result = m.Result("abc");
                    var groups = m.Groups;
                    var captues = m.Captures;
                    var value = m.Value;
                }
            }
            else
            {
                //Wildcard wildcard = new Wildcard();
                input = SH.ReplaceAll(input, to, from);
            }
        }

        return input;
    }

    public static bool ContainsNewLine(string between)
    {
        return between.Contains(AllChars.nl) || between.Contains(AllChars.cr);
    }

    public static bool ChangeEncodingProcessWrongCharacters(ref string c)
    {
        return ChangeEncodingProcessWrongCharacters(ref c, Encoding.GetEncoding("latin1"));
    }

    /// <summary>
    /// když je v souboru rozsypaný čaj, přečíst přes TF.ReadFile, převést přes SH.ChangeEncodingProcessWrongCharacters. Pokud u žádného není text smysluplný, je to beznadějně poškozené. 
    /// V opačném případě 10 kódování by mělo být v pořádku.
    /// </summary>
    /// <param name="c"></param>
    /// <param name="oldEncoding"></param>
    /// <returns></returns>
    public static bool ChangeEncodingProcessWrongCharacters(ref string c, Encoding oldEncoding)
    {
        if (IsValidISO(c))
        {
            var b = oldEncoding.GetBytes(c);
            c = Encoding.UTF8.GetString(b);
            return true;
        }
        else
        {
            // ý musí být před í, ě před č
            c = SH.ReplaceManyFromString(c, @"Ã©,ý
Ã½,ý
Ă˝,é
Å¥,š
Ĺ,ř
Ã¡,á
Åˆ,ň
Å¡,š
Ä›,ě
Å¯,ů
Å¾,ž
Ãº,ú
Å™,ř
Ã,í
Ä,č
", AllStrings.comma);
            return true;
        }
        return false;
    }

    public static string ReplaceWhiteSpacesAndTrim(string p)
    {
        return ReplaceWhiteSpaces(p).Trim();
    }

    /// <summary>
    /// By empty string
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ReplaceWhiteSpaces(string p)
    {
        return ReplaceWhiteSpaces(p, "");
    }

    public static string ReplaceWhiteSpaces(string p, string v)
    {
        return SH.Replace(ReplaceWhiteSpacesWithoutSpaces(p, v), v, AllStrings.space);
    }

    public static List<string> AddSpaceAfterFirstLetterForEveryAndSort(List<string> input)
    {
        CA.Trim(input);
        for (int i = 0; i < input.Count; i++)
        {
            input[i] = input[i].Insert(1, AllStrings.space);
        }
        input.Sort();
        return input;
    }

    public static string AddSpaceAndDontDuplicate(bool after, string text, string colon)
    {
        List<int> dxsColons = null;

        StringBuilder sb = new StringBuilder();
        sb.Append(text);
        if (after)
        {
            dxsColons = SH.ReturnOccurencesOfString(text, colon);

            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Insert(dxsColons[i] + 1, AllStrings.space);
            }

            dxsColons = SH.ReturnOccurencesOfString(sb.ToString(), colon + AllStrings.doubleSpace);
            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Remove(dxsColons[i] + 1, 1);
            }
        }
        else
        {
            dxsColons = SH.ReturnOccurencesOfString(text, colon);

            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Insert(dxsColons[i], AllStrings.space);
            }

            dxsColons = SH.ReturnOccurencesOfString(sb.ToString(), AllStrings.doubleSpace + colon);
            for (int i = dxsColons.Count - 1; i >= 0; i--)
            {
                sb.Remove(dxsColons[i], 1);
            }
        }
        return sb.ToString();
    }

    public static string CountOfItems(List<KeyValuePair<string, int>> counted)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in counted)
        {
            sb.AppendLine(item.Value + "x " + item.Key);
        }
        return sb.ToString();
    }

    public static string ReplaceAllCaseInsensitive(string vr, string zaCo, params string[] co)
    {
        foreach (var item in co)
        {
            if (zaCo.Contains(item))
            {
                throw new Exception("Nahrazovan\u00FD prvek" + " " + item + " " + "je prvkem j\u00EDm\u017E se nahrazuje" + " " + "" + " " + zaCo + AllStrings.dot);
            }
        }
        for (int i = 0; i < co.Length; i++)
        {
            vr = Regex.Replace(vr, co[i], zaCo, RegexOptions.IgnoreCase);
        }
        return vr;
    }

    public static string PrefixIfNotStartedWith(string http, string item)
    {
        if (!item.StartsWith(http))
        {
            return http + item;
        }
        return item;
    }



    public static string MultiWhitespaceLineToSingle(List<string> lines)
    {


        CA.Trim(lines);
        var str = SH.JoinNL(lines);
        var nl3 = SH.JoinTimes(3, Environment.NewLine);
        var nl2 = SH.JoinTimes(2, Environment.NewLine);
            

        while (str.Contains(nl3))
        {
            str = str.Replace(nl3, nl2);
        }

        return str;

        // Keep as is
        //return Regex.Replace(str, @"(\r\n)+", "\r\n\r\n", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        // Ódstranuje nám to 
        //return Regex.Replace(str, @"(\r\n){2,}", Environment.NewLine);

        //throw new Exception("NOT WORKING, IN FIRST DEBUG WITH UNIT TESTS AND THEN USE");

        //List<int> toRemove = new List<int>();
        //List<bool> isWhitespace = new List<bool>(l.Count);

        ////l.Add(false)

        //for (int i = 0; i < l.Count; i++)
        //{
        //    isWhitespace.Add(l[i].Trim() == string.Empty);

        //}

        //isWhitespace.Reverse();

        //for (int i = isWhitespace.Count - 1; i >= 0; i--)
        //{
        //    if (isWhitespace[i] && isWhitespace[i + 1])
        //    {
        //        l.RemoveAt(i+1);
        //    }
        //}


    }

    public static void IndentAsPreviousLine(List<string> lines)
    {
        string indentPrevious = string.Empty;
        string line = null;
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < lines.Count-1; i++)
        {
            line = lines[i];
            if (line.Length > 0)
            {
                if (!char.IsWhiteSpace(line[0]))
                {
                    lines[i] = indentPrevious + lines[i];
                }
                else
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
                    indentPrevious = sb.ToString();
                }
            }
        }
    }

    /// <summary>
    /// Whether A1 contains any from a3. a2 only logical chcek 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    public static bool ContainsLine(string item, bool checkInCaseOnlyOneString, params string[] contains)
    {
        bool hasLine = false;
        if (contains.Length == 1)
        {
            if (checkInCaseOnlyOneString)
            {
                hasLine = item.Contains(contains[0]);
            }
        }
        else
        {
            foreach (var c in contains)
            {
                if (item.Contains(c))
                {
                    hasLine = true;
                    break;
                }
            }
        }

        return hasLine;
    }


   

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
    /// <returns></returns>
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
    /// <returns></returns>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        return Contains(input, term, searchStrategy, true);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="us"></param>
    /// <param name="nameSolution"></param>
    /// <returns></returns>
    public static string RemoveAfterLast(char delimiter, string nameSolution)
    {
        int dex = nameSolution.LastIndexOf(delimiter);
        if (dex != -1)
        {
            string s = SH.Substring(nameSolution, 0, dex);
            return s;
        }
        return nameSolution;
    }



    public static string WordAfter(string input, string word)
    {
        input = SH.WrapWith(input, AllChars.space);

        int dex = input.IndexOf(word);

        int dex2 = input.IndexOf(AllChars.space, dex + 1);
        StringBuilder sb = new StringBuilder();
        if (dex2 != -1)
        {
            dex2++;
            for (int i = dex2; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch != AllChars.space)
                {
                    sb.Append(ch);
                }
                else
                {
                    break;
                }
            }
        }
        return sb.ToString();
    }

    public static string Leading(string v, Func<char, bool> isWhiteSpace)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in v)
        {
            if (isWhiteSpace.Invoke(item))
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

    public static bool IsOnIndex(string input, int dx, Func<char, bool> isWhiteSpace)
    {
        if (input.Length > dx)
        {
            return isWhiteSpace.Invoke(input[dx]);
        }
        return false;
    }

    public static int CountLines(string text)
    {
        return Regex.Matches(text, Environment.NewLine).Count;
    }

    public static bool HasLetter(string s)
    {
        foreach (var item in s)
        {
            if (char.IsLetter(item))
            {
                return true;
            }
        }
        return false;
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

    /// <summary>
    /// Return whether A1 contains all from A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="allWords"></param>
    /// <returns></returns>
    public static bool ContainsAll(string input, IEnumerable<string> allWords)
    {
        foreach (var item in allWords)
        {
            if (!input.Contains(item))
            {
                return false;
            }
        }
        return true;
    }



    public static string ReplaceWhiteSpacesExcludeSpaces(string p)
    {
        return p.Replace("\r", "").Replace("\n", "").Replace("\t", "");
    }

    public static string[] GetTextsBetween(string p, string after, string before)
    {
        List<string> vr = new List<string>();

        List<int> p2 = SH.ReturnOccurencesOfString(p, after);
        List<int> p3 = SH.ReturnOccurencesOfString(p, before);

        int min = Math.Min(p2.Count, p3.Count);
        int i1 = 0;
        int i2 = 0;

        for (; i1 < min; i1++, i2++)
        {
            int p2_2 = p2[i1];
            int p3_2 = p3[i2];

            if (p2_2 > p3_2)
            {
                i2--;
                continue;
            }

            int p2_3 = p2_2 + after.Length;
            int p3_3 = p3_2 - 1;
            // When I return between ( ), there must be +1 
            vr.Add(p.Substring(p2_3, p3_3 - p2_3 + 1).Trim());
        }

        return vr.ToArray();
    }

    public static string RemoveLastLetters(string v1, int v2)
    {
        if (v1.Length > v2)
        {
            return v1.Substring(0, v1.Length - v2);
        }
        return v1;
    }

    public static bool IsAllUnique(List<string> c)
    {
        throw new NotImplementedException();
    }

    public static string ReplaceAllDoubleSpaceToSingle(string arg)
    {
        while (arg.Contains(AllStrings.doubleSpace))
        {
            arg = SH.ReplaceAll2(arg, AllStrings.space, AllStrings.doubleSpace);
        }


        return arg;
    }

    /// <summary>
    /// ReplaceAllDoubleSpaceToSingle not working correctly while copy from webpage
    /// Split and join again
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    public static string ReplaceAllDoubleSpaceToSingle2(string arg)
    {
        var p = SH.SplitByWhiteSpaces(arg, true);

        return SH.Join(p, " ");
    }

    /// <summary>
    /// Pokud je A1 true, bere se z A2,3 menší počet prvků
    /// </summary>
    /// <param name="canBeDifferentCount"></param>
    /// <param name="typeDynamics"></param>
    /// <param name="tfd"></param>
    /// <returns></returns>
    public static bool AllHaveRightFormat(bool canBeDifferentCount, string[] typeDynamics, TextFormatData[] tfd)
    {
        if (!canBeDifferentCount)
        {
            if (typeDynamics.Length != tfd.Length)
            {
                throw new Exception("Mismatch count in input arrays of SH.AllHaveRightFormat()");
            }
        }

        int lowerCount = Math.Min(typeDynamics.Length, tfd.Length);
        for (int i = 0; i < lowerCount; i++)
        {
            if (!HasTextRightFormat(typeDynamics[i], tfd[i]))
            {
                return false;
            }
        }

        return true;
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




    public static bool HasCharRightFormat(char ch, CharFormatData cfd)
    {
        if (cfd.upper.HasValue)
        {
            if (cfd.upper.Value)
            {
                if (char.IsLower(ch))
                {
                    return false;
                }
            }
            else
            {
                if (char.IsUpper(ch))
                {
                    return false;
                }
            }
        }

        if (cfd.mustBe.Length != 0)
        {
            foreach (char item in cfd.mustBe)
            {
                if (item == ch)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string vcem2, string co, string zaCo, int overallCountOfA2)
    {
        Regex r = new Regex(co);

        //StringBuilder vcem = new StringBuilder(vcem2);
        int dex = vcem2.IndexOf(co);
        if (dex != -1)
        {
            return r.Replace(vcem2, zaCo, int.MaxValue, dex + co.Length);
            //return vcem.Replace(co, zaCo, dex + co.Length , overallCountOfA2 - 1 ).ToString();
        }

        return vcem2;
    }

    public static bool GetTextInLastSquareBracketsAndOther(string p, out string title, out string remix)
    {
        title = remix = null;
        p = p.Trim();
        if (p[p.Length - 1] != AllChars.rsf)
        {
            return false;
        }
        else
        {
            p = p.Substring(0, p.Length - 1);
        }

        int firstHranata = p.LastIndexOf(AllChars.lsf);

        if (firstHranata == -1)
        {
            return false;
        }
        else if (firstHranata != -1)
        {
            SplitByIndex(p, firstHranata, out title, out remix);
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="append"></param>
    /// <returns></returns>
    public static string AppendIfDontEndingWith(string text, string append)
    {
        if (text.EndsWith(append))
        {
            return text;
        }
        return text + append;
    }

    /// <summary>
    /// Před voláním této metody se musíš ujistit že A2 není úplně na konci
    /// </summary>
    /// <param name="p"></param>
    /// <param name="firstNormal"></param>
    /// <param name="title"></param>
    /// <param name="remix"></param>
    private static void SplitByIndex(string p, int firstNormal, out string title, out string remix)
    {
        title = p.Substring(0, firstNormal);
        remix = p.Substring(firstNormal + 1);
    }

    public static List<string> SplitAndReturnRegexMatches(string input, Regex r, params char[] del)
    {
        List<string> vr = new List<string>();
        var ds = SH.Split(input, del);
        foreach (var item in ds)
        {
            if (r.IsMatch(item))
            {
                vr.Add(item);
            }
        }
        return vr;
    }

    public static string RemoveBracketsWithTextCaseInsensitive(string vr, string zaCo, params string[] co)
    {
        vr = ReplaceAll(vr, AllStrings.lb, "( ");
        vr = ReplaceAll(vr, AllStrings.rsf, " ]");
        vr = ReplaceAll(vr, AllStrings.rb, " )");
        vr = ReplaceAll(vr, AllStrings.lsf, "[ ");
        for (int i = 0; i < co.Length; i++)
        {
            vr = Regex.Replace(vr, co[i], zaCo, RegexOptions.IgnoreCase);
        }
        return vr;
    }



    public static string RemoveBracketsWithoutText(string vr)
    {
        return SH.ReplaceAll(vr, "", "()", "[]");
    }

    public static string WithoutSpecialChars(string v, params char[] over)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in v)
        {
            if (!AllChars.specialChars.Contains(item) && !CA.IsEqualToAnyElement<char>(item, over))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }

    public static string RemoveBracketsFromStart(string vr)
    {
        while (true)
        {
            bool neco = false;
            if (vr.StartsWith(AllStrings.lb))
            {
                int ss = vr.IndexOf(AllStrings.rb);
                if (ss != -1 && ss != vr.Length - 1)
                {
                    neco = true;
                    vr = vr.Substring(ss + 1);
                }
            }
            else if (vr.StartsWith(AllStrings.lsf))
            {
                int ss = vr.IndexOf(AllStrings.rsf);
                if (ss != -1 && ss != vr.Length - 1)
                {
                    neco = true;
                    vr = vr.Substring(ss + 1);
                }
            }
            if (!neco)
            {
                break;
            }
        }
        return vr;
    }



    public static string RemoveLastCharIfIs(string slozka, char znak)
    {
        int a = slozka.Length - 1;
        if (slozka[a] == znak)
        {
            return slozka.Substring(0, a);
        }
        return slozka;
    }

    public static List<string> GetLinesList(string p)
    {
        return SH.Split(p, Environment.NewLine).ToList();
    }

    public static string GetStringNL(string[] list)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string item in list)
        {
            sb.AppendLine(item);
        }
        return sb.ToString();
    }

    public static string ReplaceFirstOccurences(string text, string co, string zaCo)
    {
        int fi = text.IndexOf(co);
        if (fi != -1)
        {
            text = ReplaceOnce(text, co, zaCo);
            text = text.Insert(fi, zaCo);
        }
        return text;
    }

    /// <summary>
    /// If A1 contains A2, return A2 and all following. Otherwise A1
    /// </summary>
    /// <param name="input"></param>
    /// <param name="returnFromString"></param>
    /// <returns></returns>
    public static string GetLastPartByString(string input, string returnFromString)
    {
        int dex = input.LastIndexOf(returnFromString);
        if (dex == -1)
        {
            return input;
        }
        int start = dex + returnFromString.Length;
        if (start < input.Length)
        {
            return input.Substring(start);
        }
        return input;
    }

    public static string ReplaceFirstOccurences(string v, string zaCo, string co, char maxToFirstChar)
    {
        int dexCo = v.IndexOf(co);
        if (dexCo == -1)
        {
            return v;
        }

        int dex = v.IndexOf(maxToFirstChar);
        if (dex == -1)
        {
            dex = v.Length;
        }

        if (dexCo > dex)
        {
            return v;
        }
        return SH.ReplaceOnce(v, co, zaCo);
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

    public static string AddEmptyLines(string content, int addRowsDuringScrolling)
    {
        var lines = SH.GetLines(content);
        for (int i = 0; i < addRowsDuringScrolling; i++)
        {
            lines.Add(string.Empty);
        }
        return SH.JoinNL(lines);
    }

    public static string ToCase(string v, bool? velkym)
    {
        if (velkym.HasValue)
        {
            if (velkym.Value)
            {
                return v.ToUpper();
            }
            else
            {
                return v.ToLower();
            }
        }
        return v;
    }


    public static bool EndsWithNumber(string nameSolution)
    {
        for (int i = 0; i < 10; i++)
        {
            if (nameSolution.EndsWith(i.ToString()))
            {
                return true;
            }
        }
        return false;
    }

    public static string TrimNumbersAtEnd(string nameSolution)
    {
        for (int i = nameSolution.Length - 1; i >= 0; i--)
        {
            bool replace = false;
            for (int n = 0; n < 10; n++)
            {
                if (nameSolution[i] == n.ToString()[0])
                {
                    replace = true;
                    nameSolution = nameSolution.Substring(0, nameSolution.Length - 1);
                    break;
                }
            }
            if (!replace)
            {
                return nameSolution;
            }
        }
        return nameSolution;
    }

    /// <summary>
    /// Výchozí byla metoda NullToStringOrEmpty
    /// OrNull pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string NullToStringOrNull(object v)
    {
        if (v == null)
        {
            return null;
        }
        return v.ToString();
    }

    public static string OnlyFirstCharUpper(string input)
    {
        return FirstCharUpper(input, true);
    }

    /// <summary>
    /// TODO: Zatím NEfunguje 100%ně, až někdy budeš mít chuť tak se můžeš pokusit tuto metodu opravit. Zatím ji nepoužívej, místo ní používej pomalejší ale funkční SplitToPartsFromEnd
    /// Vrátí null v případě že řetězec bude prázdný
    /// Pokud bude mít A1 méně částí než A2, vratí nenalezené části jako SE
    /// </summary>
    /// <param name="what"></param>
    /// <param name="parts"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    public static List<string> SplitToPartsFromEnd2(string what, int parts, params char[] deli)
    {
        List<int> indexyDelimiteru = new List<int>();
        foreach (var item in deli)
        {
            indexyDelimiteru.AddRange(SH.ReturnOccurencesOfString(what, item.ToString()));
        }
        //indexyDelimiteru.OrderBy(d => d);
        indexyDelimiteru.Sort();
        var s = SH.Split(what, deli);
        if (s.Count < parts)
        {
            //throw new Exception("");
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
            return s;
        }



        int parts2 = s.Count - parts - 1;
        //parts += povysit;
        if (parts < s.Count - 1)
        {
            parts++;
        }


        List<string> vr = new List<string>(parts);
        // Tady musí být 4 menší než 1, protože po 1. iteraci to bude 3,pak 2, pak 1
        for (; parts > parts2; parts--)
        {
            vr.Insert(0, s[parts]);
        }
        parts++;
        for (int i = 1; i < parts; i++)
        {
            vr[0] = s[i] + what[indexyDelimiteru[i]].ToString() + vr[0];

            //}
        }
        vr[0] = s[0] + what[indexyDelimiteru[0]].ToString() + vr[0];
        return vr;
    }

    public static string WrapWithIf(string value, string v, Func<string, string, bool> f)
    {
        if (f.Invoke(value, v))
        {
            return WrapWith(value, v);
        }
        return value;
    }







    private static bool IsUnicodeChar(UnicodeChars generic, char c)
    {
        switch (generic)
        {
            case UnicodeChars.Control:
                return char.IsControl(c);
            case UnicodeChars.HighSurrogate:
                return char.IsHighSurrogate(c);
            case UnicodeChars.Lower:
                return char.IsLower(c);
            case UnicodeChars.LowSurrogate:
                return char.IsLowSurrogate(c);
            case UnicodeChars.Number:
                return char.IsNumber(c);
            case UnicodeChars.Punctaction:
                return char.IsPunctuation(c);
            case UnicodeChars.Separator:
                return char.IsSeparator(c);
            case UnicodeChars.Surrogate:
                return char.IsSurrogate(c);
            case UnicodeChars.Symbol:
                return char.IsSymbol(c);
            case UnicodeChars.Upper:
                return char.IsUpper(c);
            case UnicodeChars.WhiteSpace:
                return char.IsWhiteSpace(c);
            case UnicodeChars.Special:
                return CharHelper.IsSpecial(c);
            case UnicodeChars.Generic:
                return CharHelper.IsGeneric(c);
            default:
                ThrowExceptions.NotImplementedCase(s_type, "IsUnicodeChar");
                return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="what"></param>
    /// <param name="parts"></param>
    /// <param name="deli"></param>
    /// <param name="addEmptyPaddingItems"></param>
    /// <param name="joinOverloadedPartsToLast"></param>
    /// <returns></returns>
    public static List<string> SplitToParts(string what, int parts, string deli, bool addEmptyPaddingItems /*, bool joinOverloadedPartsToLast - not used */)
    {
        var s = SH.Split(what, deli);
        if (s.Count < parts)
        {
            // Pokud je pocet ziskanych partu mensi, vlozim do zbytku prazdne retezce
            if (s.Count == 0)
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

    public static bool LastCharEquals(string input, char delimiter)
    {
        if (!string.IsNullOrEmpty(input))
        {
            return false;
        }
        char ch = input[input.Length - 1];
        if (ch == delimiter)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// In case that delimiter cannot be found, to A2,3 set null
    /// Before calling this method I must assure that A1 havent A4 on end
    /// </summary>
    /// <param name="input"></param>
    /// <param name="filePath2"></param>
    /// <param name="fileName"></param>
    /// <param name="backslash"></param>
    public static void SplitByLastCharToTwoParts(string input, out string filePath, out string fileName, char delimiter)
    {
        int dex = input.LastIndexOf(delimiter);
        if (dex != -1)
        {
            SH.SplitByIndex(input, dex, out filePath, out fileName);
        }
        else
        {
            filePath = null;
            fileName = null;
        }
    }









    static SH()
    {
        s_cs = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs";
        Init();
    }

    public static string GetLastWord(string p)
    {
        p = p.Trim();
        int dex = p.LastIndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(dex).Trim();
        }
        return "";
    }

    public static string GetWithoutLastWord(string p)
    {
        p = p.Trim();
        int dex = p.LastIndexOf(AllChars.space);
        if (dex != -1)
        {
            return p.Substring(0, dex);
        }
        return p;
    }

    public static bool IsWildcard(string text)
    {
        return SH.ContainsAny(text, false, CA.ToEnumerable(AllStrings.q, AllStrings.asterisk)).Count > 1;
    }



    public static string DeleteCharsOutOfAscii(string s)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char item in s)
        {
            int i = (int)item;
            if (i < 128)
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }





    /// <summary>
    /// Not working for czech, same as https://stackoverflow.com/a/249126
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(string text)
    {
        String normalizedString = text.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            switch (CharUnicodeInfo.GetUnicodeCategory(c))
            {
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    stringBuilder.Append(c);
                    break;
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.DashPunctuation:
                    stringBuilder.Append('_');
                    break;
            }
        }
        string result = stringBuilder.ToString();
        return String.Join("_", result.Split(new char[] { '_' }
            , StringSplitOptions.RemoveEmptyEntries)); // remove duplicate underscores
    }


    public static string ReplaceLastOccurenceOfString(string text, string co, string čím)
    {
        var roz = SplitNone(text, co);
        if (roz.Length() == 1)
        {
            return text.Replace(co, čím);
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < roz.Length() - 2; i++)
            {
                sb.Append(roz[i] + co);
            }

            sb.Append(roz[roz.Length() - 2]);
            sb.Append(čím);
            sb.Append(roz[roz.Length() - 1]);
            return sb.ToString();
        }
    }






    public static string JoinTimes(int times, string dds)
    {
        // Working just for char
        //return new String(dds, times);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < times; i++)
        {
            sb.Append(dds);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Use with general letters
    /// </summary>
    /// <param name="stringSplitOptions"></param>
    /// <param name="text"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    private static string[] SplitSpecial(StringSplitOptions stringSplitOptions, string text, params char[] deli)
    {
        if (deli == null || deli.Count() == 0)
        {
            if (s_cs)
            {
                throw new Exception("Nebyl specifikov\u00E1n delimiter");
            }
            else
            {
                throw new Exception("No delimiter determined");
            }
        }



        if (deli.Length == 1 && !SH.IsUnicodeChar(UnicodeChars.Generic, deli[0]))
        {
            return text.Split(deli, stringSplitOptions);
        }
        else
        {
            List<char> normal = new List<char>();
            List<char> generic = new List<char>();
            foreach (var item in deli)
            {
                if (SH.IsUnicodeChar(UnicodeChars.Generic, item))
                {
                    generic.Add(item);
                }
                else
                {
                    normal.Add(item);
                }
            }
            if (generic.Count > 0)
            {
                DebugCollection<string> splitted = new DebugCollection<string>();
                splitted.dontAllow.Add(string.Empty);
                if (normal.Count > 0)
                {
                    splitted.AddRange(text.Split(normal.ToArray(), stringSplitOptions).ToList());
                }
                else
                {
                    splitted.Add(text);
                }

                Predicate<char> predicate;

                foreach (var genericChar in generic)
                {
                    predicate = AllChars.ReturnRightPredicate(genericChar);
                    DebugCollection<string> splittedPart = new DebugCollection<string>();
                    splittedPart.dontAllow.Add(string.Empty);
                    for (int i = splitted.Count() - 1; i >= 0; i--)
                    {
                        var item2 = splitted[i];

                        splittedPart.Clear();

                        StringBuilder sb = new StringBuilder();
                        foreach (var item in item2)
                        {
                            if (predicate.Invoke(item))
                            {
                                sb.Append(item);
                            }
                            else
                            {
                                if (sb.Length != 0)
                                {
                                    splittedPart.Add(sb.ToString());
                                    sb.Clear();
                                }
                            }
                        }

                        int splittedPartCount = splittedPart.Count();
                        if (splittedPartCount > 1)
                        {
                            splitted.RemoveAt(i);
                            for (int y = splittedPartCount - 1; y >= 0; y--)
                            {
                                splitted.Insert(i, splittedPart[y]);
                            }
                        }
                        splitted.Add(sb.ToString());
                    }
                }
                return splitted.ToArray();
            }
            else
            {
                return text.Split(deli, stringSplitOptions);
            }
        }
    }

    public static string[] SplitSpecial(string text, params char[] deli)
    {
        return SplitSpecial(StringSplitOptions.RemoveEmptyEntries, text, deli);
    }

    public static string[] SplitSpecialNone(string text, params char[] deli)
    {
        return SplitSpecial(StringSplitOptions.None, text, deli);
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

    public static string StripFunctationsAndSymbols(string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char item in p)
        {
            if (!char.IsPunctuation(item) && !char.IsSymbol(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }

    public static List<int> SplitToIntListNone(string stringToSplit, params string[] delimiter)
    {
        List<int> nt = null;
        stringToSplit = stringToSplit.Trim();
        if (stringToSplit != "")
        {
            var f = SH.SplitNone(stringToSplit, delimiter);
            nt = new List<int>(f.Length());
            foreach (string item in f)
            {
                nt.Add(int.Parse(item));
            }
        }
        else
        {
            nt = new List<int>();
        }
        return nt;
    }

    public static string AdvancedTrim(string p)
    {
        return p.Replace(AllStrings.doubleSpace, AllStrings.space).Trim();
    }

    /// <summary>
    /// Vrátí SE když A1 bude null, pokud null nebude, trimuje ho
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string TrimIsNotNull(string p)
    {
        if (p != null)
        {
            return p.Trim();
        }
        return "";
    }

    public static char[] ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces(bool commaInclude)
    {
        List<char> l = new List<char>();
        l.AddRange(spaceAndPuntactionChars);
        if (!commaInclude)
        {
            l.Remove(AllChars.comma);
        }
        return l.ToArray();
    }

    public static string[] SplitBySpaceAndPunctuationChars(string s)
    {
        return s.Split(spaceAndPuntactionChars, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Vrátí mi v každém prvku index na které se nachází první znak a index na kterém se nachází poslední
    /// </summary>
    /// <param name="vcem"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static List<FromTo> ReturnOccurencesOfStringFromTo(string vcem, string co)
    {
        int l = co.Length;
        List<FromTo> Results = new List<FromTo>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            if (vcem.Substring(Index, co.Length) == co)
            {
                FromTo ft = new FromTo();
                ft.from = Index;
                ft.to = Index + l - 1;
                Results.Add(ft);
            }
        }
        return Results;
    }


    public static string GetWithoutFirstWord(string item2)
    {
        item2 = item2.Trim();
        //return item2.Substring(
        int dex = item2.IndexOf(AllChars.space);
        if (dex != -1)
        {
            return item2.Substring(dex + 1);
        }
        return item2;
    }

    public static int EndsWithIndex(string source, params string[] p2)
    {
        for (int i = 0; i < p2.Length; i++)
        {
            if (source.EndsWith(p2[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public static bool HasIndex(int p, string nahledy, bool throwExcWhenInvalidIndex = true)
    {
        if (p < 0)
        {
            if (throwExcWhenInvalidIndex)
            {
                throw new Exception("Chybn\u00FD parametr" + " " + "");
            }
            else
            {
                return false;
            }
        }
        if (nahledy.Length > p)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return A1 if wont find A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="searchFor"></param>
    /// <returns></returns>
    public static string GetToFirst(string input, string searchFor)
    {
        int indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1)
        {
            return input.Substring(0, indexOfChar + 1);
        }
        return input;
    }

    //public static string JoinStringParams(string name, params string[] labels) { return null; }
    //public static string JoinStringParams(object delimiter, params string[] parts) { return null; }
    //public static string JoinPairs(params object[] args){return null;} 

    //public static string JoinString(object delimiter, IEnumerable parts){return null;}
    //public static string Join(IEnumerable parts, object delimiter){return null;}
    //public static string JoinIEnumerable(object delimiter, IEnumerable parts){return null;}

    //public static string Join(char p, IList vsechnyFotkyVAlbu){return null;}
    //public static string Join(char p, int[] vsechnyFotkyVAlbu){return null;}
    //public static string Join(char name, params object[] labels){return null;}
    //public static string Join(List<string> labels, char name){return null;}

    //public static string JoinNL(IEnumerable p){return null;}
    //public static string JoinNL(params string[] p){return null;}

    //public static string JoinSpace(IEnumerable<string> nazev){return null;}
    //public static string JoinString(string name, IEnumerable labels){return null;}
    //public static string JoinStringExceptIndexes(string name, IEnumerable labels, params int[] v2){return null;}
    //public static string JoinMoreWords(char v, params string[] fields){return null;}
    //public static string JoinWithoutTrim(string p, IList ownedCatsLI){return null;}
    //public static string JoinIEnumerable(char name, IEnumerable labels){return null;}
    //public static string JoinWithoutEndTrimDelimiter(string name, params string[] labels){return null;}
    //public static string JoinFromIndex(int p, char delimiter, IEnumerable<string> tokeny){return null;}
    //public static string JoinFromIndex(int dex, string delimiter, IEnumerable<string> parts){return null;}
    //public static string JoinToIndex(int dex, string delimiter, IEnumerable<string> parts){return null;}
    //public static string JoinMakeUpTo2NumbersToZero(char p, params int[] args){return null;}
    //public static string JoinDictionary(Dictionary<string, string> dictionary, string v){return null;} 

    /* Result of refactoring Join methods:
     * params have only two:
     * Join
     * JoinString
     */




    public static string JoinStringParams(object delimiter, params string[] parts)
    {
        // TODO: Delete after all app working, has here method Join with same arguments
        return Join(delimiter, CA.ToEnumerable(parts));
    }





    /// <summary>
    /// Will be delete after final refactoring
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string Join(IEnumerable parts, object delimiter)
    {
        if (delimiter is string)
        {
            return Join(delimiter, parts);
        }
        // TODO: Delete after all app working, has flipped A1 and A2
        return Join(delimiter, parts);
    }

    // refaktorovat to tady, nemuzu zavolat params z IEnum . Teprve ve working method zkontroluji co je za typ a pripadne pretypuji



    /// <summary>
    /// If element will be number, wont wrap with qm.
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string JoinMoreWords(object delimiter, params string[] parts)
    {
        parts = CA.WrapWithIf(StringDelegates.IsNumber, true, AllStrings.space, AllStrings.qm, parts);
        return Join(delimiter, parts);
    }

    public static string JoinStringExceptIndexes(object delimiter, IEnumerable parts, params int[] v2)
    {
        string s = delimiter.ToString();
        StringBuilder sb = new StringBuilder();
        int i = -1;
        foreach (string item in parts)
        {
            i++;
            if (CA.IsEqualToAnyElement<int>(i, v2))
            {
                continue;
            }
            sb.Append(item + s);
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
    /// Ořeže poslední znak - delimiter
    /// </summary>
    /// <param name="dex"></param>
    /// <param name="delimiter2"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string JoinFromIndex(int dex, object delimiter2, IEnumerable parts)
    {
        string delimiter = delimiter2.ToString();
        StringBuilder sb = new StringBuilder();
        int i = 0;
        foreach (var item in parts)
        {
            if (i >= dex)
            {
                sb.Append(item + delimiter);
            }

            i++;
        }
        string vr = sb.ToString();
        return SH.SubstringLength(vr, 0, vr.Length - 1);
    }

    private static string SubstringLength(string vr, int from, int length)
    {
        if (HasIndex(from, vr))
        {
            if (HasIndex(length, vr))
            {
                return vr.Substring(from, length);
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// A1 won't be included
    /// </summary>
    /// <param name="dex"></param>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
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