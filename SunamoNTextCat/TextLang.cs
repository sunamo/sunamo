﻿using NTextCat;
using sunamo.Essential;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

public class TextLang
{
    static Type type = typeof(TextLang);

    static EmbeddedResourcesH resources = null;
    const string Wiki280Profile = "Profiles/Wiki280.profile.xml";
    const string cs = "cs";
    const string en = "en";
    static bool initialized = false;

    public static string file = "";

    /// <summary>
    /// A1 can be null, then is use common file
    /// must be in _Loaded event
    /// </summary>
    public static void Init(string file2 = null)
    {
        if (!initialized)
        {
            file = file2;

            if (file == null)
            {
                file = AppData.ci.GetFileCommonSettings("ntextcat_probiality.txt");
            }

            var data = SF.GetAllElementsFile(file);

            foreach (var line in data)
            {
                //var elements = SF.GetAllElementsLine(line);
                TextLangIndexes tli = new TextLangIndexes(line);

                textLangIndexes.Add(tli.text, tli);
            }

            var ass = typeof(TextLang).Assembly;
            resources = new EmbeddedResourcesH(ass, ass.GetName().Name);
            initialized = true;
        }
    }

    /// <summary>
    /// Before use 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static NTextCatResult GetLangs(string text)
    {
        string methodName = "GetLangs";

        var factory = new RankedLanguageIdentifierFactory();

        

        var stream = resources.GetString(Wiki280Profile);

        

        var identifier = factory.Load(BTS.StreamFromString(stream));

        
        var languages = identifier.Identify(text);

        

        NTextCatResult result = new NTextCatResult();

        result.langInfos = languages.Select(d => d.Item1).ToList();
        result.probiability = languages.Select(d => d.Item2).ToList();

        return result;

        //var mostCertainLanguage = languages.FirstOrDefault();
        //if (mostCertainLanguage != null)
        //    return mostCertainLanguage.Item1.Iso639_3;
        //else
        //    return null;
    }

    static Dictionary<string, TextLangIndexes> textLangIndexes = new Dictionary<string, TextLangIndexes>();

    /// <summary>
    /// Must call Init() due to load determined words!
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsEnglish(string text)
    {
        if (SH.ContainsDiacritic(text))
        {
            return false;
        }

        double sumEn, sumCs;
        CalculateSumOfProbiality(text, out sumEn, out sumCs);

        return sumEn < sumCs;
    }

    private static void CalculateSumOfProbiality(string text, out double sumEn, out double sumCs)
    {
        var p = SH.SplitBySpaceAndPunctuationCharsAndWhiteSpaces(text);

        p = CA.ToLower(p);

        sumEn = 0;
        sumCs = 0;
        foreach (var item in p)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                continue;
            }

            double cs2 = 0;
            double en2 = 0;

            if (!textLangIndexes.ContainsKey(item))
            {
                var l = GetLangs(item);
                cs2 = IndexOf(l, cs);
                en2 = IndexOf(l, en);

                textLangIndexes.Add(item, new TextLangIndexes() { text = item, cs = cs2, en = en2 });

                SF.AppendToFile(file, SF.PrepareToSerialization2(item, cs2, en2));
            }
            else
            {
                var tli = textLangIndexes[item];
                cs2 = tli.cs;
                en2 = tli.en;
            }

            sumCs += cs2;
            sumEn += en2;
        }

        sumEn /= p.Count;
        sumCs /= p.Count;
    }

    private static int IndexOf(List<LanguageInfo> l, string en2)
    {
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i].Iso639_2T == en2)
            {
                return i;
            }
        }
        return int.MaxValue;
    }

    private static double IndexOf(NTextCatResult l, string en2)
    {
        for (int i = 0; i < l.langInfos.Count; i++)
        {
            if (l.langInfos[i].Iso639_2T == en2)
            {
                return l.probiability[i];
            }
        }
        return 0;
    }

    /// <summary>
    /// IF contains diacritic, return true
    /// Must call Init() due to load determined words!
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsCzech(string text)
    {
        if (text == "Hello")
        {
            return false;
        }

        if (SH.ContainsDiacritic(text))
        {
            return true;
        }

        double sumEn, sumCs;
        CalculateSumOfProbiality(text, out sumEn, out sumCs);

        return sumEn > sumCs;
    }
}

