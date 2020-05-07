using Newtonsoft.Json;
using System.Xml;
using System.Collections.Generic;
using System;
using sunamo;
using sunamo.Essential;

public partial class SunamoJson{ 
public static string SerializeObject(object ab, bool intended = true)
    {
        Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None;
        if (intended)
        {
            formatting = Newtonsoft.Json.Formatting.Indented;
        }

        string dd = JsonConvert.SerializeObject(ab, formatting); //.Replace("\\\"", AllStrings.qm);
        List<char> ch = new List<char>(dd.ToCharArray());
        ch[0] = AllChars.bs;
        ch.Insert(1, AllChars.lcub);
        ch.Insert(ch.Count - 1, AllChars.rcub);
        ch[ch.Count - 1] = AllChars.bs;
        string vr = new string (ch.ToArray());
        return vr; //.Substring(1, vr.Length - 2);
    }
}