using sunamo.Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

public static class SH
{

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
        //input = input.Trim();
        input = SH.WrapWith(input, AllChars.space);
        
        int dex = input.IndexOf(word);
        
            // Add 1 because I will remove next word
        //    input = input.Substring(dex + word.Length + 1);
        //if (!input.Contains(AllChars.space))
        //{
        //    input += AllChars.space;
        //}
        
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

    /// <summary>
    /// Whether A1 contains any from a3. a2 only logical chcek 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="hasFirstEmptyLength"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    public static List<string> ContainsAny(string item, bool checkInCaseOnlyOneString, params string[] contains)
    {
        List<string>  founded = new List<string>();

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
                    founded.Add(c);
                }
            }
        }

        return founded;
    }

    /// <summary>
    /// not implemented
    /// Pokud sudý [0], [2], ... bude mít aspoň jeden nebílý znak, pak se přidá lichý [1], [3] i sudý ve dvojicích. jinak nic
    /// </summary>
    /// <param name="className"></param>
    /// <param name="v1"></param>
    /// <param name="methodName"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static string ConcatIfBeforeHasValue(params string[] className)
    {
        throw new NotImplementedException();
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
                vr.Add( p.Substring(p2_3, p3_3 - p2_3).Trim());
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
        return p.Substring(begin+1, end - begin - 1);
    }

    public static string GetTextBetween(string p, string after, string before)
    {
        string vr = null;
        int p2 = p.IndexOf(after);
        int p3 = p.IndexOf(before);
        if (p2 != -1 && p3 != -1)
        {
            p2 += after.Length;
            p3 -= 1;
            vr = p.Substring(p2, p3 - p2).Trim();
        }

        return vr;
    }

    public static bool IsAllUnique(List<string> c)
    {
        throw new NotImplementedException();
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

        int charCount = r.Length;
        if (tfd.requiredLength != -1)
        {
            if (r.Length != tfd.requiredLength)
            {
                return false;
            }
            charCount = Math.Min(r.Length, tfd.requiredLength);
        }

        for (int i = 0; i < charCount; i++)
        {
            if (!HasCharRightFormat(r[i], tfd[i]))
            {
                return false;
            }
        }

        return true;
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
        string[] ds = SH.Split(input, del);
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

    public static string SubstringIfAvailable(string p1, int p2)
    {
        if (p1.Length > p2)
        {
            return p1.Substring(0, p2);
        }
        return p1;
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

    public static string JoinWithoutEndTrimDelimiter(string name, params string[] labels)
    {
        string s = name.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (string item in labels)
        {
            sb.Append(item + s);
        }
        return sb.ToString();
    }

    public static string DoubleSpacesToSingle(string v)
    {
        return SH.ReplaceAll2(v, " ", "  ");
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

    public const String diacritic = "áčďéěíňóšťúůýřžÁČĎÉĚÍŇÓŠŤÚŮÝŘŽ";
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

    /// <summary>
    /// Ořeže poslední znak - delimiter
    /// </summary>
    /// <param name="p"></param>
    /// <param name="delimiter"></param>
    /// <param name="tokeny"></param>
    /// <returns></returns>
    public static string JoinFromIndex(int p, char delimiter, string[] tokeny)
    {
        string delimiter2 = delimiter.ToString();
        StringBuilder sb = new StringBuilder();
        for (int i = p; i < tokeny.Length; i++)
        {
            sb.Append(tokeny[i] + delimiter2);
        }
        string vr = sb.ToString();
        return vr.Substring(0, vr.Length - 1);
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

    public static string JoinSpace(List<string> nazev)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string item in nazev)
        {
            sb.Append(item + " ");
        }
        return sb.ToString().TrimEnd(' ');
    }

    public static string FirstCharUpper(string nazevPP)
    {
        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
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
    public static string[] SplitToPartsFromEnd2(string what, int parts, params char[] deli)
    {
        List<int> indexyDelimiteru = new List<int>();
        foreach (var item in deli)
        {
            indexyDelimiteru.AddRange(SH.ReturnOccurencesOfString(what, item.ToString()));
        }
        //indexyDelimiteru.OrderBy(d => d);
        indexyDelimiteru.Sort();
        string[] s = SH.Split(what, deli);
        #region Pokud bude mít A1 méně částí než A2, vratí nenalezené části jako SE
        if (s.Length < parts)
        {
            //throw new Exception("");
            if (s.Length > 0)
            {
                List<string> vr2 = new List<string>();
                for (int i = 0; i < parts; i++)
                {
                    if (i < s.Length)
                    {
                        vr2.Add(s[i]);
                    }
                    else
                    {
                        vr2.Add("");
                    }
                }
                return vr2.ToArray();
                //return new string[] { s[0] };
            }
            else
            {
                return null;
            }
        }
        else if (s.Length == parts)
        {
            return s;
        }
        #endregion



        #region old
        int parts2 = s.Length - parts - 1;
        //parts += povysit;
        if (parts < s.Length - 1)
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
        return vr.ToArray();
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

    public static string[] Split(string text, params char[] deli)
    {
        if (deli == null || deli.Length == 0)
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
        return text.Split(deli, StringSplitOptions.None);
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
    public static string[] SplitToParts(string what, int parts, string deli, bool addEmptyPaddingItems /*, bool joinOverloadedPartsToLast - not used */)
    {
        string[] s = SH.Split(what, deli);
        if (s.Length < parts)
        {
            // Pokud je pocet ziskanych partu mensi, vlozim do zbytku prazdne retezce
            if (s.Length == 0)
            {
                List<string> vr2 = new List<string>();
                for (int i = 0; i < parts; i++)
                {
                    if (i < s.Length)
                    {
                        vr2.Add(s[i]);
                    }
                    else
                    {
                        vr2.Add("");
                    }
                }
                return vr2.ToArray();
                //return new string[] { s[0] };
            }
            else
            {
                return null;
            }
        }
        else if (s.Length == parts)
        {
            // Pokud pocet ziskanych partu souhlasim presne, vratim jak je
            return s;
        }

        // Pokud je pocet ziskanych partu vetsi nez kolik ma byt, pripojim ty co josu navic do zbytku
        parts--;
        List<string> vr = new List<string>();
        for (int i = 0; i < s.Length; i++)
        {
            if (i < parts)
            {
                vr.Add(s[i]);
            }
            else if (i == parts)
            {
                vr.Add(s[i] + deli);
            }
            else if (i != s.Length - 1)
            {
                vr[parts] += s[i] + deli;
            }
            else
            {
                vr[parts] += s[i];
            }
        }
        return vr.ToArray();
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

    /// <summary>
    /// Get null if count of getted parts was under A2.
    /// Automatically add empty padding items at end if got lower than A2
    /// Automatically join overloaded items to last by A2.
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <returns></returns>
    public static string[] SplitToParts(string what, int parts, string deli)
    {
        string[] s = SH.Split(what, deli);
        if (s.Length < parts)
        {
            // Pokud je pocet ziskanych partu mensi, vlozim do zbytku prazdne retezce
            if (s.Length > 0)
            {
                List<string> vr2 = new List<string>();
                for (int i = 0; i < parts; i++)
                {
                    if (i < s.Length)
                    {
                        vr2.Add(s[i]);
                    }
                    else
                    {
                        vr2.Add("");
                    }
                }
                return vr2.ToArray();
                //return new string[] { s[0] };
            }
            else
            {
                return null;
            }
        }
        else if (s.Length == parts)
        {
            // Pokud pocet ziskanych partu souhlasim presne, vratim jak je
            return s;
        }

        // Pokud je pocet ziskanych partu vetsi nez kolik ma byt, pripojim ty co josu navic do zbytku
        parts--;
        List<string> vr = new List<string>();
        for (int i = 0; i < s.Length; i++)
        {
            if (i < parts)
            {
                vr.Add(s[i]);
            }
            else if (i == parts)
            {
                vr.Add(s[i] + deli);
            }
            else if (i != s.Length - 1)
            {
                vr[parts] += s[i] + deli;
            }
            else
            {
                vr[parts] += s[i];
            }
        }
        return vr.ToArray();
    }

    

    public static List<int> ReturnOccurencesOfString(string vcem, string co)
    {
        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            if (vcem.Substring(Index, co.Length) == co) Results.Add(Index);
        }
        return Results;
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

    public static string GetString(object[] o, string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in o)
        {
            sb.Append(item.ToString() + p);
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

    public static string ReplaceOnce(string input, string what, string zaco)
    {
        #region Varianta #1 - pomocí Regexu
        #endregion

        if (input.Contains("na rozdíl od 4"))
        {

        }

        if (what == "")
        {
            return input;
        }

        #region Varianta #2 - údajně rychlejší podle SO
        int pos = input.IndexOf(what);
        if (pos == -1)
        {
            return input;
        }
        return input.Substring(0, pos) + zaco + input.Substring(pos + what.Length);
        #endregion
    }

    /// <summary>
    /// G text bez dia A1.
    /// </summary>
    /// <param name="sDiakritik"></param>
    /// <returns></returns>
    public static string TextWithoutDiacritic(string sDiakritik)
    {
        return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(sDiakritik));
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

    public static string[] SplitAdvanced(string v, bool replaceNewLineBySpace, bool moreSpacesForOne, bool trim, bool escapeQuoations, params string[] deli)
    {
        string[] s = SH.Split(v, deli);
        if (replaceNewLineBySpace)
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = SH.ReplaceAll(s[i], " ", "\r", "\n", Environment.NewLine);
            }
        }
        if (moreSpacesForOne)
        {
            for (int i = 0; i < s.Length; i++)
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

            for (int i = 0; i < s.Length; i++)
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

    public static string ReplaceByIndex(string s, string zaCo, int v, int length)
    {
        s = s.Remove(v, length);
        s = s.Insert(v, zaCo);
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
    /// <param name="pozice"></param>
    /// <param name="or"></param>
    public static void GetPartsByLocation(out string pred, out string za, string pozice, char or)
    {
        int dex = pozice.IndexOf(or);
        SH.GetPartsByLocation(out pred, out za, pozice, dex);
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

    public static void GetPartsByLocation(out string pred, out string za, string or, int pozice)
    {
        if (pozice == -1)
        {
            pred = or;
            za = "";
        }
        else
        {
            pred = or.Substring(0, pozice);
            za = or.Substring(pozice + 1);
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

    public static string[] SplitNone(string text, params char[] deli)
    {
        if (deli == null || deli.Length == 0)
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
        return text.Split(deli, StringSplitOptions.None);
    }

    public static string[] SplitNone(string text, params string[] deli)
    {
        if (deli == null || deli.Length == 0)
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
        return text.Split(deli, StringSplitOptions.None);
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

    public static string ShortForLettersCount(string p, int p_2)
    {
        bool pridatTriTecky = false;
        return ShortForLettersCount(p, p_2, out pridatTriTecky);
    }

    private static string ShortForLettersCount(string p, int p_2, out bool pridatTriTecky)
    {
        #region MyRegion
        #endregion

        pridatTriTecky = false;
        // Vše tu funguje výborně
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (SH.IsInFirstXCharsTheseLetters(p, p_2, ' '))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;

                int napocitano = 0;
                for (int i = 0; i < to; i++)
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

    public static string[] Split(string vstup, params string[] deli)
    {
        if (vstup == "")
        {
            return new string[0];
        }
        if (deli == null || deli.Length == 0)
        {
            throw new Exception("No delimiter determined");
        }
        return vstup.Split(deli, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string JoinStringParams(string name, params string[] labels)
    {
        return JoinString(name, labels);
    }

    /// <summary>
    /// Automaticky ořeže poslední A1
    /// </summary>
    /// <param name="name"></param>
    /// <param name="labels"></param>
    /// <returns></returns>
    public static string JoinString(string name, IEnumerable labels)
    {
        string s = name.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (string item in labels)
        {
            sb.Append(item + s);
        }
        string d = sb.ToString();
        //return d.Remove(d.Length - (name.Length - 1), name.Length);
        int to = d.Length - name.Length;
        if (to > 0)
        {
            return d.Substring(0, to);
        }
        return d;
        //return d;
    }

    public static string JoinStringExceptIndexes(string name, IEnumerable labels, params int[] v2)
    {
        string s = name.ToString();
        StringBuilder sb = new StringBuilder();
        int i = -1;
        foreach (string item in labels)
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
        int to = d.Length - name.Length;
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
    /// <param name="name"></param>
    /// <param name="labels"></param>
    /// <returns></returns>
    public static string Join(List<string> labels, char name)
    {
        string s = name.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (string item in labels)
        {
            sb.Append(item + s);
        }
        string sb2 = sb.ToString();
        if (labels.Count != 0)
        {
            sb2 = sb2.Substring(0, sb2.Length - 1);
        }
        return sb2;
    }

    /// <summary>
    /// Automaticky ořeže poslední znad A1
    /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
    /// </summary>
    /// <param name="name"></param>
    /// <param name="labels"></param>
    /// <returns></returns>
    public static string Join(char name, params object[] labels)
    {
        string s = name.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (object item in labels)
        {
            sb.Append(item.ToString() + s);
        }
        string sb2 = sb.ToString();
        if (labels.Length != 0)
        {
            sb2 = sb2.Substring(0, sb2.Length - 1);
        }
        return sb2;
    }

    public static string JoinMoreWords(char v, params string[] fields)
    {
        fields = CA.WrapWithIf(StringDelegates.IsNumber, true, " ", "\"", fields);
        return Join(v, fields);
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

    public static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {

        #region Toto nefungovalo
        for (int i = 0; i < co.Length; i++)
        {
            string what = co[i];
            int whatLength = what.Length;
            List<int> nt = SH.ReturnOccurencesOfString(vstup, what);
            for (int y = nt.Count - 1; y>= 0; y--)
            {
                vstup = SH.ReplaceByIndex(vstup, zaCo, nt[y], whatLength);
            }
        }
        return vstup; 
        #endregion
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

    public static string JoinMakeUpTo2NumbersToZero(char p, params int[] args)
    {
        List<string> na2Cislice = new List<string>();
        foreach (var item in args)
        {
            na2Cislice.Add(DTHelper.MakeUpTo2NumbersToZero(item));
        }
        return JoinIEnumerable(p, na2Cislice);
    }

    /// <summary>
    /// Pokud něco nebude číslo, program vyvolá výjimku, protože parsuje metodou int.Parse
    /// </summary>
    /// <param name="stringToSplit"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static List<int> SplitToIntList(string stringToSplit, params string[] delimiter)
    {
        string[] f = SH.Split(stringToSplit, delimiter);
        List<int> nt = new List<int>(f.Length);
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

    public static string JoinNL(IEnumerable p)
    {
        return SH.JoinString(Environment.NewLine, p);
    }

    public static string JoinNL(params string[] p)
    {
        return SH.JoinString(Environment.NewLine, p);
    }

    /// <summary>
    /// Automaticky ořeže poslední A1
    /// </summary>
    /// <param name="p"></param>
    /// <param name="vsechnyFotkyVAlbu"></param>
    /// <returns></returns>
    public static string Join(char p, IList vsechnyFotkyVAlbu)
    {
        StringBuilder sb = new StringBuilder();
        int pf = vsechnyFotkyVAlbu.Count;
        if (pf == 0)
        {
            return "";
        }
        sb.Append(vsechnyFotkyVAlbu[0].ToString());
        if (pf > 1)
        {
            for (int i = 1; i < pf; i++)
            {
                sb.Append("," + vsechnyFotkyVAlbu[i].ToString());
            }
        }
        return sb.ToString();
    }

    public static char[] spaceAndPuntactionChars = new char[] { ' ', '-', '.', ',', ';', ':', '!', '?', '–', '—', '‐', '…', '„', '“', '‚', '‘', '»', '«', '’', '\'', '(', ')', '[', ']', '{', '}', '〈', '〉', '<', '>', '/', '\\', '|', '”', '\"', '~', '°', '+', '@', '#', '$', '%', '^', '&', '*', '=', '_', 'ˇ', '¨', '¤', '÷', '×', '˝' };

    public static string[] SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string s)
    {
        return s.Split(spaceAndPuntactionCharsAndWhiteSpaces);
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

    public static string Join(char p, int[] vsechnyFotkyVAlbu)
    {
        StringBuilder sb = new StringBuilder();
        int pf = vsechnyFotkyVAlbu.Length;
        if (pf == 0)
        {
            return "";
        }
        sb.Append(vsechnyFotkyVAlbu[0].ToString());
        if (pf > 1)
        {
            for (int i = 1; i < pf; i++)
            {
                sb.Append("," + vsechnyFotkyVAlbu[i].ToString());
            }
        }
        return sb.ToString();
    }

    public static string ReplaceOnceIfStartedWith(string what, string replaceWhat, string zaCo)
    {
        if (what.StartsWith(replaceWhat))
        {
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
        string[] split = SH.Split(input, delimiter);
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

    public static string JoinWithoutTrim(string p, IList ownedCatsLI)
    {
        StringBuilder sb = new StringBuilder();
        foreach (int item in ownedCatsLI)
        {
            sb.Append(item.ToString() + p);
        }
        return sb.ToString();
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

    public static string JoinIEnumerable(char name, IEnumerable labels)
    {
        string s = name.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (object item in labels)
        {
            sb.Append(item.ToString() + s);
        }
        string sb2 = sb.ToString();
        if (sb2.Length != 0)
        {
            sb2 = sb2.Substring(0, sb2.Length - 1);
        }
        return sb2;
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

    
}
