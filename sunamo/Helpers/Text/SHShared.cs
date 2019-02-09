﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static partial class SH
{
    public const String diacritic = "áčďéěíňóšťúůýřžÁČĎÉĚÍŇÓŠŤÚŮÝŘŽ";


    

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

    public static List<string> Split(string text, params string[] deli)
    {
        return text.Split(deli, StringSplitOptions.RemoveEmptyEntries).ToList();
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

    public static List<string> Split(string parametry, char v)
    {
        return Split(parametry, v.ToString());
    }

    /// <summary>
    /// In difference with ReplaceAll2, A3 is params
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {

        for (int i = 0; i < co.Length; i++)
        {
            string what = co[i];
            int whatLength = what.Length;
            List<int> nt = SH.ReturnOccurencesOfString(vstup, what);
            for (int y = nt.Count - 1; y >= 0; y--)
            {
                vstup = SH.ReplaceByIndex(vstup, zaCo, nt[y], whatLength);
            }
        }
        return vstup;
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

    public static List<int> ReturnOccurencesOfString(string vcem, string co)
    {
        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            if (vcem.Substring(Index, co.Length) == co) Results.Add(Index);
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
            sb.Append(item.ToString() + s);
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

        if (input.Contains("na rozdíl od 4"))
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

    /// <summary>
    /// G text bez dia A1.
    /// </summary>
    /// <param name="sDiakritik"></param>
    /// <returns></returns>
    public static string TextWithoutDiacritic(string sDiakritik)
    {
        return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(sDiakritik));
    }





public static string Format(string status, params object[] args)
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
        var result = SH.Format(templateHandler, args);
        const string replacement = "{        }";
        result = SH.ReplaceAll2(result, replacement, "[]");
        result = SH.ReplaceAll2(result, "{", lsf);
        result = SH.ReplaceAll2(result, "}", rsf);
        result = SH.ReplaceAll2(result, replacement, "{}");
        return result;
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
}