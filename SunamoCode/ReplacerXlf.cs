using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunamoCode;

/// <summary>
/// Dictionary as cache is good in database but not in ordinal c# app!
/// </summary>
public class ReplacerXlf
{
    public Dictionary<string, string> withWithoutUnderscore = new Dictionary<string, string>();
    static ReplacerXlf instance = null;
    List<string> val = null;

    private ReplacerXlf()
    {
        AllLists.InitHtmlEntitiesFullNames();

        val = AllLists.htmlEntitiesFullNames.Values.ToList();

        val.Sort(SunamoComparer.StringLength.Instance.Desc);
        CA.Prepend("_", val);
    }

    public string WithoutUnderscore(string s)
    {
        foreach (var item2 in val)
        {
            s = s.Replace(item2, string.Empty);
        }
        return s;
    }

    public static void AddKeys(List<string> k)
    {

    }

    public static void AddKeysXlfKeysIds()
    {
        List<string> ids = null;// XlfResourcesH.PathToXlfSunamo(Langs.en);
        var ids2 = new List<string>(ids);

        string content;
        string content2;
        content2 = null;

        AddKeys(ids);
    }

    public static ReplacerXlf Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ReplacerXlf();
            }

            return instance;
        }
    }
}