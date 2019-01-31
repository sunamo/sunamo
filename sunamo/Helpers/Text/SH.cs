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

public static partial class SH
{
    

    public static string ReplaceWhiteSpacesAndTrim(string p)
    {
        return ReplaceWhiteSpaces(p).Trim();
    }

    public static string ReplaceWhiteSpaces(string p)
    {
        return ReplaceWhiteSpacesWithoutSpaces(p).Replace(" ", "");
    }

    public static string ReplaceWhiteSpacesWithoutSpaces(string p)
    {
        return ReplaceWhiteSpacesWithoutSpaces(p, "");
    }

    public static string ReplaceWhiteSpacesWithoutSpaces(string p, string replaceWith = "")
    {
        return p.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    /// <summary>
    /// If A1 is string, return A1
    /// If IEnumerable, return joined by comma
    /// For inner collection use CA.TwoDimensionParamsIntoOne
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ListToString(object value)
    {
        string text;
        var valueType = value.GetType();
        text = value.ToString();
        if (value is IEnumerable && valueType != Consts.tString && valueType != Consts.tStringBuilder)
        {
            var enumerable = CA.ToListString(value as IEnumerable);
            CA.Replace(enumerable, AllStrings.comma, AllStrings.space);
            text = SH.Join(AllChars.comma, enumerable);
        }
        return text;
    }

    public static string JoinDictionary(Dictionary<string, string> dict, string delimiterBetweenKeyAndValue, string delimAfter)
    {
        return JoinKeyValueCollection(dict.Keys, dict.Values, delimiterBetweenKeyAndValue, delimAfter);
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
        
        return SH.TrimEnd( sb.ToString(), delimAfter);
    }

    public static string Format(string status, object[] args)
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
            catch (Exception)
            {
                return status;
                
            }
        }
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

            dxsColons = SH.ReturnOccurencesOfString(sb.ToString(), colon + "  ");
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

            dxsColons = SH.ReturnOccurencesOfString(sb.ToString(), "  " + colon);
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
                throw new Exception("Nahrazovaný prvek " + item + " je prvkem jímž se nahrazuje + " + zaCo + ".");
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
        var result = string.Format(templateHandler, args);
        const string replacement = "{        }";
        result = SH.ReplaceAll2(result, replacement, "[]");
        result = SH.ReplaceAll2(result, "{", lsf);
        result = SH.ReplaceAll2(result, "}", rsf);
        result = SH.ReplaceAll2(result, replacement, "{}");
        return result;
    }

    public static string PostfixIfNotEmpty(string text, string postfix)
    {
        if (text.Length != 0)
        {
            return text + postfix;
        }
        return text;
    }

    /// <summary>
    /// A1 = search for exact occur. otherwise split both to words
    /// Control for string.Empty, because otherwise all results are true
    /// </summary>
    /// <param name="input"></param>
    /// <param name="what"></param>
    /// <returns></returns>
    public static bool Contains(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        if (term != "")
        {
            if (searchStrategy == SearchStrategy.ExactlyName)
            {
                return input == term;
            }
            else
            {
                if (searchStrategy == SearchStrategy.FixedSpace)
                {
                    return input.Contains(term);
                }
                else
                {
                    var allWords = SH.Split(term, AllStrings.space);
                    return SH.ContainsAll(input, allWords);
                }
            }
        }
        return false;
    }

    static Type type = typeof(SH);

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

    public static bool EndsWith(string input, string endsWith)
    {
        return input.EndsWith(endsWith);
    }

    public static string WrapWithQm(string commitMessage)
    {
        return SH.WrapWith(commitMessage, AllChars.qm);
    }

    public static string WordAfter(string input, string word)
    {
        input = SH.WrapWith(input, AllChars.space);
        
        int dex = input.IndexOf(word);
     
            int dex2 = input.IndexOf(AllChars.space, dex+1);
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

    public static string Format2(string template, params string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            template = SH.ReplaceOnce(template, "{" + i + "}", args[i]);
        }
        return template;
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

    public static string RemoveAfterFirst(string t, string ch)
    {
        int dex = t.IndexOf(ch);
        if (dex == -1 || dex == t.Length - 1)
        {
            return t;
        }

        return t.Remove(dex);
    }

    public static string RemoveAfterFirst(string t, char ch)
    {
        int dex = t.IndexOf(ch);
        if (dex == -1 || dex == t.Length -1)
        {
            return t;
        }
        
        return t.Substring(dex+1);
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
    /// Whether A1 contains any from a3. if a2 and A3 contains only 1 element, check for contains these first element
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
            vr.Add( p.Substring(p2_3, p3_3 - p2_3+1).Trim());
        }

        return vr.ToArray();
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

    public static string GetTextBetweenTwoChars(string p, int begin, int end)
    {
        // a(1) - 1,3
        return p.Substring(begin + 1, end - begin - 1);
        // originally
        //return p.Substring(begin+1, end - begin - 1);
    }

    public static string GetTextBetween(string p, string after, string before)
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
            vr = p.Substring(p2, p3 - p2+1).Trim();
        }
        else
        {
            ThrowExceptions.NotContains(type, "GetTextBetween", p, after, before);
        }

        return vr;
    }

    public static bool IsAllUnique(List<string> c)
    {
        throw new NotImplementedException();
    }

    public static string ReplaceAllDoubleSpaceToSingle(string arg)
    {
        return SH.ReplaceAll2(arg, " ", "  ");
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
        int tfdCountM1 = tfd.Count-1;

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
                isRightChar = CA.IsEqualToAnyElement<char>(r[ actualChar], actualFormatData.mustBe);
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
                if (CA.HasIndex(actualCharFormatData+1, tfd))
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
        if (p[p.Length - 1] != ']')
        {
            return false;
        }
        else
        {
            p = p.Substring(0, p.Length - 1);
        }

        int firstHranata = p.LastIndexOf('[');

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
        vr = ReplaceAll(vr, "(", "( ");
        vr = ReplaceAll(vr, "]", " ]");
        vr = ReplaceAll(vr, ")", " )");
        vr = ReplaceAll(vr, "[", "[ ");
        for (int i = 0; i < co.Length; i++)
        {
            vr = Regex.Replace(vr, co[i], zaCo, RegexOptions.IgnoreCase);
        }
        return vr;
    }

    public static string ReplaceDoubleSpacesAndTrim2(string innerText)
    {
        throw new NotImplementedException();
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
            if (vr.StartsWith("("))
            {
                int ss = vr.IndexOf(")");
                if (ss != -1 && ss != vr.Length - 1)
                {
                    neco = true;
                    vr = vr.Substring(ss + 1);
                }
            }
            else if (vr.StartsWith("["))
            {
                int ss = vr.IndexOf("]");
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

    public static string SubstringIfAvailable(string input, int lenght)
    {
        if (input.Length > lenght)
        {
            return input.Substring(0, lenght);
        }
        return input;
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

    public static string Replace(string t, string what, string forWhat)
    {
        return t.Replace(what, forWhat);
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

    

    public static string DoubleSpacesToSingle(string v)
    {
        return SH.ReplaceAll2(v, " ", "  ");
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

    
    static bool cs = false;

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
                    nameSolution= nameSolution.Substring(0, nameSolution.Length - 1);
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
    /// Tato metoda byla výchozí, jen se jmenovala NullToString
    /// OrEmpty pro odliseni od metody NullToStringOrEmpty
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string NullToStringOrEmpty(object v)
    {
        if (v == null)
        {
            return "";
        }
        return v.ToString();
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

    public static string OnlyFirstCharUpper(string input)
    {
        return FirstCharUpper(input, true);
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

    /// <summary>
    /// FUNGUJE ale může být pomalá, snaž se využívat co nejméně
    /// Pokud někde bude více delimiterů těsně za sebou, ve výsledku toto nebude, bude tam jen poslední delimiter v té řadě příklad z 1,.Par při delimiteru , a . bude 1.Par
    /// </summary>
    /// <param name="what"></param>
    /// <param name="parts"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    public static string[] SplitToPartsFromEnd(string what, int parts, params char[] deli)
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
                string ds = what[delimitersIndexes[i-1]].ToString();
                v[0] = vr[i] + ds + v[0];
            }
        }
        return v.ToArray();
    }

    public static string RemoveBracketsAndHisContent(string title, bool squareBrackets, bool parentheses, bool braces)
    {
        if (squareBrackets)
        {
            title = RemoveBetweenAndEdgeChars(title, '[', ']');
        }
        if (parentheses)
        {
            title = RemoveBetweenAndEdgeChars(title, '(', ')');
        }
        if (braces)
        {
            title = RemoveBetweenAndEdgeChars(title, '{', '}');
        }
        title = ReplaceAll(title, "", "  ").Trim();
        return title;
    }

    public static string RemoveBetweenAndEdgeChars(string s, char begin, char end)
    {
        Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
        return regex.Replace(s, string.Empty);
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
        #region Pokud bude mít A1 méně částí než A2, vratí nenalezené části jako SE
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
        #endregion



        #region old
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
        #endregion
    }

    public static string WrapWithIf(string value, string v, Func<string, string, bool> f)
    {
        if (f.Invoke(value, v))
        {
            return WrapWith(value, v);
        }
        return value;
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
                ThrowExceptions.NotImplementedCase(type, "IsUnicodeChar");
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

    

    

    

    static char[] spaceAndPuntactionCharsAndWhiteSpaces = null;

    static SH()
    {
        cs = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs";
        Init();
    }

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

    public static string GetString(IEnumerable o, string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in o)
        {
            sb.Append(SH.ListToString( item) + p);
        }
        return sb.ToString();
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
    /// Vrátí prázdný řetězec pokud nebude nalezena mezera.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GetFirstWord(string p)
    {
        p = p.Trim();
        int dex = p.IndexOf(' ');
        if (dex != -1)
        {
            return p.Substring(0, dex);
        }
        return "";
    }

    public static string GetLastWord(string p)
    {
        p = p.Trim();
        int dex = p.LastIndexOf(' ');
        if (dex != -1)
        {
            return p.Substring(dex).Trim();    
        }
        return "";
    }

    public static string GetWithoutLastWord(string p)
    {
        p = p.Trim();
        int dex = p.LastIndexOf(' ');
        if (dex != -1)
        {
            return p.Substring(0, dex);    
        }
        return p;
    }

    public static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
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

    static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        string escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        string escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, ".");
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        Regex reg = new Regex(pat);
        return reg.IsMatch(str);
    }

    

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static List<string> SplitAdvanced(string v, bool replaceNewLineBySpace, bool moreSpacesForOne, bool trim, bool escapeQuoations, params string[] deli)
    {
        var s = SH.Split(v, deli);
        if (replaceNewLineBySpace)
        {
            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceAll(s[i], " ", "\r", "\n", Environment.NewLine);
            }
        }
        if (moreSpacesForOne)
        {
            for (int i = 0; i < s.Count; i++)
            {
                s[i] = SH.ReplaceAll2(s[i], " ", "  ");
            }
        }
        if (trim)
        {
            s = CA.Trim(s);
        }
        if (escapeQuoations)
        {
            string rep = "\"";

            for (int i = 0; i < s.Count; i++)
            {
                    s[i] = SH.ReplaceFromEnd(s[i], "\\\"", rep);
                //}
            }
        }
        return s;
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
    /// G zda je jedinz znak v A1 s dia.
    /// </summary>
    /// <returns></returns>
    public static bool ContainsDiacritic(string slovo)
    {
        return slovo != SH.TextWithoutDiacritic(slovo);
    }

    public static string RemoveLastChar(string artist)
    {
        return artist.Substring(0, artist.Length - 1);
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

    public static string ReplaceLastOccurenceOfString(string text, string co, string čím)
    {
        string[] roz = SplitNone(text, co);
        if (roz.Length == 1)
        {
            return text.Replace(co, čím);
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < roz.Length - 2; i++)
            {
                sb.Append(roz[i] + co);
            }

            sb.Append(roz[roz.Length - 2]);
            sb.Append(čím);
            sb.Append(roz[roz.Length - 1]);
            return sb.ToString();
        }

    }

    #region Split
    /// <summary>
    /// With these 
    /// </summary>
    /// <param name="stringSplitOptions"></param>
    /// <param name="text"></param>
    /// <param name="deli"></param>
    /// <returns></returns>
    private static string[] Split(StringSplitOptions stringSplitOptions, string text, params char[] deli)
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

        return text.Split(deli, stringSplitOptions);
    }

    public static List<string> Split(string text, params char[] deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text, deli).ToList();
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

    public static string[] SplitNone(string text, params char[] deli)
    {
        return Split(StringSplitOptions.None, text, deli);
    }
    #endregion

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
            if (cs)
            {
                throw new Exception("Nebyl specifikován delimiter");
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

    public static string[] SplitNone(string text, params string[] deli)
    {
        return text.Split( deli, StringSplitOptions.None);
    }

    public static string FirstCharLower(string nazevPP)
    {
        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToLower() + sb;
    }

    public static string FirstCharuUpper(string nazevPP)
    {
        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
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
            if (SH.IsInLastXCharsTheseLetters(p, p_2, ' '))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;

                int napocitano = 0;
                for (int i = to - 1; i >= 0; i--)
                {
                    napocitano++;

                    if (d[i] == ' ')
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

    public static int OccurencesOfStringIn(string source, string p_2)
    {
        return source.Split(new string[] { p_2 }, StringSplitOptions.None).Length - 1;
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

    /// <summary>
    /// Stejná jako metoda ReplaceAll, ale bere si do A3 pouze jediný parametr, nikoliv jejich pole
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static string ReplaceAll2(string vstup, string zaCo, string co)
    {
        // needed, otherwise stack overflow

        if (zaCo.Contains(co))
        {
            throw new Exception("Nahrazovaný prvek je prvkem jímž se nahrazuje.");
        }
        if (co == zaCo)
        {
            throw new Exception("SH.ReplaceAll2 - parametry co a zaCo jsou stejné");
        }
        while (vstup.Contains(co))
        {
            vstup = vstup.Replace(co, zaCo);
        }

        return vstup;
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

    public static List<int> GetVariablesInString(string innerHtml)
    {
        return GetVariablesInString('{', '}', innerHtml);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ret"></param>
    /// <param name="pocetDo"></param>
    /// <returns></returns>
    public static List<int> GetVariablesInString(char p, char k, string innerHtml)
    {
        #region Původní metoda
        /// Vrátí mi formáty, které jsou v A1 od 0 do A2-1
        /// A1={0} {2} {3} A2=3 G=0,2
        #endregion

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

    public static bool ContainsVariable( string innerHtml)
    {
        return ContainsVariable('{', '}', innerHtml);
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
        return ReplaceVariables('{', '}', innerHtml, _dataBinding, actualRow);
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

    public static string TrimStart(string v, string s)
    {
        while (v.StartsWith(s))
        {
            v = v.Substring(s.Length);
        }
        return v;
    }

    public static string TrimStartAndEnd(string v, string s, string e)
    {
        v = TrimEnd(v, e);
        v = TrimStart(v, s);
        
        return v;
    }

    public static string Trim(string s, string args)
    {
        
        while (s.EndsWith(args))
        {
            s = s.Substring(0, s.Length - 1);
        }
        while (s.StartsWith(args))
        {
            s = s.Substring(2, s.Length - 2);
        }
        return s;
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

    public static List<int> SplitToIntListNone(string stringToSplit, params string[] delimiter)
    {
        List<int> nt = null;
        stringToSplit = stringToSplit.Trim();
        if (stringToSplit != "")
        {
            string[] f = SH.SplitNone(stringToSplit, delimiter);
            nt = new List<int>(f.Length); 
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
        return p.Replace("  ", " ").Trim();

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

    

    

    public static char[] spaceAndPuntactionChars = new char[] { ' ', '-', '.', ',', ';', ':', '!', '?', '–', '—', '‐', '…', '„', '“', '‚', '‘', '»', '«', '’', '\'', '(', ')', '[', ']', '{', '}', '〈', '〉', '<', '>', '/', '\\', '|', '”', '\"', '~', '°', '+', '@', '#', '$', '%', '^', '&', '*', '=', '_', 'ˇ', '¨', '¤', '÷', '×', '˝' };

    public static List<string> SplitByWhiteSpaces(string s)
    {
        return s.Split(AllChars.whiteSpacesChars.ToArray()).ToList();
    }

    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string s)
    {
        return s.Split(spaceAndPuntactionCharsAndWhiteSpaces).ToList();
    }

    public static char[] ReturnCharsForSplitBySpaceAndPunctuationCharsAndWhiteSpaces(bool comma)
    {
        List<char> l = new List<char>();
        l.AddRange(spaceAndPuntactionChars);
        if (!comma)
        {
            l.Remove(',');
        }
        return l.ToArray();
    }

    public static string[] SplitBySpaceAndPunctuationChars(string s)
    {
        return s.Split(spaceAndPuntactionChars, StringSplitOptions.RemoveEmptyEntries);
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

    public static List< string> RemoveDuplicatesNone(string p1, string delimiter)
    {
        string[] split = SH.SplitNone(p1, delimiter);
        return CA.RemoveDuplicitiesList<string>(new List<string>(split));
    }

    public static string GetWithoutFirstWord(string item2)
    {
        item2 = item2.Trim();
        //return item2.Substring(
        int dex = item2.IndexOf(' ');
        if (dex != -1)
        {
            return item2.Substring(dex + 1);
        }
        return item2;
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
            if (ch == ' ')
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {

                    leva.Insert(0, ts + " ");
                    if (leva.Length + " ".Length + ts.Length > naKazdeStrane)
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
        string l = slovo.ToString() + " " + leva.ToString().TrimEnd(' ');
        l = l.TrimEnd(' ');
        naKazdeStrane += naKazdeStrane - l.Length;
        slovo.Clear();
        // Počítám po pravé straně započítám i to středové písmenko
        for (int i = stred; i < celyObsah.Length; i++)
        {
            char ch = celyObsah[i];
            if (ch == ' ')
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {

                    prava.Append(" " + ts);
                    if (prava.Length + " ".Length + ts.Length > naKazdeStrane)
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
        
        string p = prava.ToString().TrimStart(' ') + " " + slovo.ToString();
        p = p.TrimStart(' ');
        string vr = "";
        if (celyObsah.Contains(l + " ") && celyObsah.Contains(" " + p))
        {
            vr = l + " " + p;
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

    public static bool HasIndex(int p, string nahledy)
    {
        if (p < 0)
        {
            throw new Exception("Chybný parametr p");
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

    public static string GetToFirstChar(string input, int indexOfChar)
    {
        if (indexOfChar != -1)
        {
            return input.Substring(0, indexOfChar + 1);
        }
        return input;
    }

    public static string TrimNewLineAndTab(string lyricsFirstOriginal)
    {
        return lyricsFirstOriginal.Replace("\t", " ").Replace("\r", " ").Replace("\n", " ").Replace("  ", " ");
    }

    #region Join - its chaos in it, previous interface
    #region Join
    //public static string JoinStringParams(string name, params string[] labels) { return null; }
    //public static string JoinStringParams(object delimiter, params string[] parts) { return null; }
    //public static string JoinPairs(params object[] args){return null;} 
    #endregion

    #region Join - Delete after all solutions working
    //public static string JoinString(object delimiter, IEnumerable parts){return null;}
    //public static string Join(IEnumerable parts, object delimiter){return null;}
    //public static string JoinIEnumerable(object delimiter, IEnumerable parts){return null;}
    #endregion Join - Delete after all solutions working

    #region Join
    //public static string Join(char p, IList vsechnyFotkyVAlbu){return null;}
    //public static string Join(char p, int[] vsechnyFotkyVAlbu){return null;}
    //public static string Join(char name, params object[] labels){return null;}
    //public static string Join(List<string> labels, char name){return null;}
    #endregion

    #region JoinNL
    //public static string JoinNL(IEnumerable p){return null;}
    //public static string JoinNL(params string[] p){return null;}
    #endregion

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
    #endregion

    #region Join
    /* Result of refactoring Join methods:
     * params have only two:
     * Join
     * JoinString
     */

    

    
    public static string JoinStringParams(object delimiter, params string[] parts)
    {
        // TODO: Delete after all app working, has here method Join with same arguments
        return Join(delimiter, CA.ToEnumerable( parts));
    }

    #region Join - Delete after all solutions working
    

    

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

    
    #endregion

    /// <summary>
    /// If element will be number, wont wrap with qm.
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string JoinMoreWords(object delimiter, params string[] parts)
    {
        parts = CA.WrapWithIf(StringDelegates.IsNumber, true, " ", "\"", parts);
        return Join(delimiter, parts);
    }

    #region With special inputs collections
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

    #region From to index
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
        return vr.Substring(0, vr.Length - 1);
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
    #endregion 
    #endregion

    #region Join with fixed letter
    #region JoinNL
    public static string JoinNL(IEnumerable parts)
    {
        return SH.JoinString(Environment.NewLine, parts);
    }

    public static string JoinNL(params string[] parts)
    {
        return SH.JoinString(Environment.NewLine, parts);
    }
    #endregion

    public static string JoinSpace(IEnumerable parts)
    {
        return SH.JoinString(AllStrings.space, parts);
    }
    #endregion

    #region Pairs, Dictionary
    public static string JoinDictionary(Dictionary<string, string> dictionary, string delimiter)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in dictionary)
        {
            sb.AppendLine(item.Key + delimiter + item.Value);
        }
        return sb.ToString();
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
    #endregion

    #region Only with numbers
    public static string JoinMakeUpTo2NumbersToZero(object p, params int[] parts)
    {
        List<string> na2Cislice = new List<string>();
        foreach (var item in parts)
        {
            na2Cislice.Add(DTHelper.MakeUpTo2NumbersToZero(item));
        }
        return JoinIEnumerable(p, na2Cislice);
    } 
    #endregion

    public static string JoinWithoutTrim(object p, IList parts)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object item in parts)
        {
            sb.Append(item.ToString() + p);
        }
        return sb.ToString();
    }

    public static string JoinWithoutEndTrimDelimiter(object name, params string[] parts)
    {
        // TODO: Delete after making all solutions working
        return JoinWithoutTrim(name, parts);
    }
    #endregion
}
