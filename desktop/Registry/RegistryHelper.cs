
using Microsoft.Win32;
using System.IO;
using System;
using System.Reflection;

using System.Collections.Generic;

/// <summary>
/// Pomaha pracovat s registry pomoci nek. M.
/// Impl. IPrevedPpk pro prevody mezi PolozkaRegistru a RegistryKey
/// M IPrevedPpk zatim nejsou impl.
/// 
/// </summary>
public class RegistryHelper //: IRegistry //, IPrevedPpk<RegistryKey, PolozkaRegistru>
{
    #region DPP
    /// <summary>
    /// Znak, kterym se oddeluji tokeny v registru
    /// </summary>
    static string oddeloacRegistru = Path.AltDirectorySeparatorChar.ToString();
    #endregion

    #region -!Hotovo!-
    /// <summary>
    /// V A1 projde vsechny prvky a vrati o [] jejich cestu, nazev a hodnotu. 
     /// Hodnotu ne, leda ze by i nacitala sama PolozkaRegistru, coz nemam overene.
    /// Pokud je A2 true, nebudu ziskavat jen nazvy, ale i hodnoty.
    /// </summary>
    /// <param name="klic"></param>
    /// <param name="vsechnyHodnoty"></param>
    
    public static List<RegistryEntry> ConvertPpk(List<RegistryKey> uu)
    {
        List<RegistryEntry> ppk = new List<RegistryEntry>();
        foreach (RegistryKey var in uu)
        {
            ppk.Add(null);
        }
        return ppk;
    }

        #endregion 
    #endregion



    public static void GetHkeyAndPath(string p, out string hkey, out string key)
    {
         SH.GetPartsByLocation(out hkey, out key, p, AllChars.bs);
    }
}




