using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using sunamo.Data;
using System.Collections;
using System.Linq;
using sunamo.Constants;

public static partial class SF
{
    private static SerializeContentArgs s_contentArgs = new SerializeContentArgs();


    public static Dictionary<T1, T2> ToDictionary<T1, T2>(List<List<string>> l)
    {
        object s1 = BTS.MethodForParse<T1>();
        object s2 = BTS.MethodForParse<T2>();

        Func<string, T1> p1 = (Func<string, T1>)s1;
        Func<string, T2> p2 = (Func<string, T2>)s2;

        Dictionary <T1, T2> dict = new Dictionary<T1, T2>();

        T1 t1 = default(T1);
        T2 t2 = default(T2);

        foreach (var item in l)
        {
            t1 = p1.Invoke(item[0]);
            t2 = p2.Invoke(item[1]);
            dict.Add(t1, t2);
        }

        return dict;
    }

    public static string separatorString
    {
        get
        {
            return s_contentArgs.separatorString;
        }

        set
        {
            s_contentArgs.separatorString = value;
        }
    }

    public static string PrepareToSerializationExplicitString(IEnumerable o, string p1 = AllStrings.verbar)
    {
        var o3 = CA.ToListString(o);
        var o2 = CA.Trim(o3);
        string vr = SH.GetString(o, p1);
        return vr.Substring(0, vr.Length - p1.Length);
    }

    /// <summary>
    /// Same as PrepareToSerialization - return without last
    /// If need to combine string and IEnumerable, lets use CA.Join
    /// DateTime is format with DTHelperEn.ToString
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="o"></param>
    public static string PrepareToSerializationExplicit(IEnumerable o, char p1 = AllChars.verbar)
    {
        
        return PrepareToSerializationExplicitString(o, p1.ToString());
    }

    public static List<List<string>> GetAllElementsFile(string file, char oddelovaciZnak = AllChars.verbar)
    {
        List<string> header = null;
        var r = GetAllElementsFileAdvanced(file, out header, oddelovaciZnak);
        if (header.Count > 0)
        {
            r.Insert(0, header);
        }
        
        return r;
    }

    /// <summary>
    /// In result A1 is not 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="hlavicka"></param>
    /// <param name="oddelovaciZnak"></param>
    public static List<List<string>> GetAllElementsFileAdvanced(string file, out List<string> hlavicka, char oddelovaciZnak = AllChars.verbar)
    {
        hlavicka = new List<string>();
        string oz = oddelovaciZnak.ToString();
        List<List<string>> vr = new List<List<string>>();
        var lines = TF.ReadAllLines(file);
        lines = CA.Trim(lines);
        if (lines.Count > 0)
        {
            hlavicka = SF.GetAllElementsLine(lines[0], oddelovaciZnak);
            int musiByt = SH.OccurencesOfStringIn(lines[0], oz);
            //int nalezeno = 0;
            StringBuilder jedenRadek = new StringBuilder();
            for (int i = 1; i < lines.Count; i++)
            {
                //nalezeno += SH.OccurencesOfStringIn(lines[i], oz);
                jedenRadek.AppendLine(lines[i]);
                //if (nalezeno == musiByt)
                //{
                //nalezeno = 0;
                var columns = SF.GetAllElementsLine(jedenRadek.ToString(), oddelovaciZnak);
                jedenRadek.Clear();

                    vr.Add(columns);
                //}
            }
        }

        return vr;
    }

    public static void Dictionary<T1, T2>(string file, Dictionary<T1, T2> artists)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in artists)
        {
            sb.AppendLine(PrepareToSerialization2(item.Key, item.Value));
        }

        TF.SaveFile(sb.ToString(), file);
    }



    

/// <summary>
    /// Return with the last
    /// DateTime is format with DTHelperEn.ToString
    /// </summary>
    /// <param name="o"></param>
    /// <param name="separator"></param>
    public static string PrepareToSerialization3(IEnumerable<string> o)
    {
        return PrepareToSerializationWorker(o, false, dDeli);
    }

    

    /// <summary>
    /// Return without last
    /// DateTime is serialize always in english format
    /// Opposite method: DTHelperEn.ToString<>DTHelperEn.ParseDateTimeUSA
    /// </summary>
    /// <param name="pr"></param>
    public static string PrepareToSerialization2(params object[] pr)
    {
        var ts = CA.ToListString(pr);
        return PrepareToSerializationWorker(ts, true, separatorString);
    }

    

    /// <summary>
    /// Return without last
    /// If need to combine string and IEnumerable, lets use CA.Join
    /// </summary>
    /// <param name = "o"></param>
    public static string PrepareToSerialization2(IEnumerable<string> o, string separator = AllStrings.verbar)
    {
        return PrepareToSerializationWorker(o, true, separator);
    }

    



}