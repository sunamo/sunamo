using NTextCat;
using sunamo.Essential;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

public class TextLang
{
    

    static EmbeddedResourcesH resources = null;
    const string Wiki280Profile = "Profiles/Wiki280.profile.xml";
    const string cs = "cs";
    const string en = "en";

    /// <summary>
    /// must be in _Loaded event
    /// </summary>
    public static void Init()
    {
        var ass = typeof(TextLang).Assembly;
        resources = new EmbeddedResourcesH(ass, ass.GetName().Name);
    }

    /// <summary>
    /// Before use 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static List<LanguageInfo> GetLangs(string text)
    {
        var factory = new RankedLanguageIdentifierFactory();
        var stream = resources.GetString(Wiki280Profile);
        var identifier = factory.Load(BTS.StreamFromString(stream));
        var languages = identifier.Identify(text);
        

        DebugLogger.Instance.DumpObject("", languages, DumpProvider.Yaml);

        return languages.Select(d => d.Item1).ToList();

        //var mostCertainLanguage = languages.FirstOrDefault();
        //if (mostCertainLanguage != null)
        //    return mostCertainLanguage.Item1.Iso639_3;
        //else
        //    return null;
    }

    public static bool IsEnglish(string text)
    {
        if (SH.ContainsDiacritic( text))
        {
            return false;
        }
        var l = GetLangs(text);
        var cs2 = IndexOf(l, cs);
        var en2 = IndexOf(l, en);
        return en2 < cs2;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsCzech(string text)
    {
        DebugLogger.Instance.WriteLine("Encoding preregistered");
        if (SH.ContainsDiacritic(text))
        {
            return true;
        }

        var l = GetLangs(text);
        var cs2 = IndexOf(l, cs);
        var en2 = IndexOf(l, en);
        return en2 > cs2;
    }
}

