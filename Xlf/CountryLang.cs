using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region For easy copy
using System.Collections.Generic;
public class CountryLang
{
    public static Dictionary<Langs, string> d = new Dictionary<Langs, string>();

    static CountryLang()
    {
        Init();
    }

    public static void Init()
    {
        d.Add(Langs.en, "GB");
        d.Add(Langs.cs, "CZ");
    }
} 
#endregion