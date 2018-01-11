
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
class RegistryHelper //: IRegistry //, IPrevedPpk<RegistryKey, PolozkaRegistru>
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
    /// <returns></returns>
    public static RegistryEntry[] GetAllItemFromRegistryKeyInArray(RegistryKey klic, bool vsechnyHodnoty)
    {
        string klicS = klic.ToString();
        List<RegistryEntry> vratit = new List<RegistryEntry>();
        string[] strings = klic.GetValueNames();
        #region Polkud A2, vsechny je rek projdu a 
        if (vsechnyHodnoty)
        {
            throw new Exception("Nesm� se zad�vat s A2 true");
            //vratit.AddRange(VsechnyKlice(klic));
        } 
        #endregion
        #region Jinak ulozim jen aktualni
        else
        {
            foreach (string var in strings)
            {
                vratit.Add(new RegistryEntry(klicS, var));
            }
        } 
        #endregion

        return vratit.ToArray();
    }

     /// <summary>
     /// Rekurzivne projde vettev registru a G vsechny klice.
     /// </summary>
     /// <param name="adresar"></param>
     /// <returns></returns>
     /// <summary>
     /// G pole, ktere bude obsahovat podklice A1 v klici A1 A2.
     /// </summary>
     /// <param name="p"></param>
     /// <param name="adresar"></param>
     /// <returns></returns>
    public static IEnumerable<RegistryKey> CombinePathWithKeys(string[] p, RegistryKey adresar)
     {
         List<RegistryKey> l = new List<RegistryKey>();

         foreach (string var in p)
         {
             l.Add( adresar.OpenSubKey(var));
         }

         return l;
     }

     /// <summary>
     /// G hodnoty vsech polozek ktere byly nalezeny v A1 nerek vc. hodnoty.
     /// </summary>
     /// <returns></returns>
    public static RegistryEntry[] GetAllValues(string cesta)
     {
         List<RegistryEntry> o = new List<RegistryEntry>();
         RegistryKey rk = GetObjectRegistryKey(cesta);
         string rks = rk.ToString();

         string[] dd = rk.GetValueNames();
         foreach (string var in dd)
         {
             o.Add(new RegistryEntry(Registry.GetValue(rks, var, null),cesta, var));
         }
         return o.ToArray();
     }

     #region Zakladni
     /// <summary>
     /// G O RK, ktery uz ma nactenou cestu.
     /// </summary>
     /// <param name="cesta"></param>
     /// <returns></returns>
     /// <summary>
     /// G O RegistryKey, ktery reprezentuje nekterou z hlavnich vetvi A1 a do A2 ulozim zbytek.
     /// </summary>
     /// <param name="cesta"></param>
     /// <param name="zbylaCesta"></param>
     /// <returns></returns>
    public static RegistryKey GetObjectRegistryKey(string cesta)
     {
         string zbylaCesta = null;
         // TODO: Pridat kontrolu, zda tomu tak je
         string pred = null;
         SH.GetPartsByLocation(out pred, out zbylaCesta, cesta, cesta.IndexOf("\\"));
         string[] tokeny = SH.Split(zbylaCesta, "\\");
         Type pe = typeof(Registry);
         FieldInfo[] fi = pe.GetFields();
         RegistryKey vratit = null;
         foreach (FieldInfo var in fi)
         {
             // Protoze je staticka, zkusim uzit null
             object rko = var.GetValue(null);
             RegistryKey rk = (RegistryKey)rko;
             if (pred == rk.ToString())
             {
                 vratit = rk;
                 break;

             }
         }
         foreach (string item in tokeny)
         {
             vratit = vratit.CreateSubKey(item);
         }
         return vratit;
     } 
     #endregion


    #region IRegistry Members
    /// <summary>
    /// Do cesty A2, kde je zahr. i hodnota, zapisu A1.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cesta"></param>
    public static void SetValue(object value, string cesta)
    {
        string nazevObjektu = "";
        cesta = ExtractPathFromPath(out nazevObjektu, cesta);
        RegistryKey rk = GetObjectRegistryKey(cesta);
        rk.SetValue(nazevObjektu, value);
    }

    /// <summary>
    /// G hodnotu z komplexni cesty vc. hodnoty A1.
    /// </summary>
    /// <param name="cesta"></param>
    /// <returns></returns>
    public static object GetValue(string cesta)
    {
        string nazevObjektu = "";
        cesta = ExtractPathFromPath(out nazevObjektu, cesta);
        RegistryKey rk = GetObjectRegistryKey(cesta);
        return rk.GetValue(nazevObjektu);
    }
    #endregion

    #region Spojovani
    /// <summary>
    /// G spojene a1  a A2.
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static string CombinePaths(string s1, string s2)
    {
        return Path.Combine(s1, s2);
    }

    /// <summary>
    /// Otevre subklic A2 v A1.
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static RegistryKey CombinePaths(RegistryKey s1, string s2)
    {
        return s1.OpenSubKey(s2);
    }
    #endregion

    /// <summary>
    /// Rozdeli cestu a nazev hodnoty
    /// Pokud je hodnota spojena, tj, vc. hodnoty se kterou se ma spojit, vrati pouze klic k ni
    /// G cestu z A2 a do A1 ulozim zjistenou hodnotu. 
    /// M nekontroluje, zda se jedna o H a muze vratit posledni token cesty.
    /// </summary>
    /// <param name="hodnota"></param>
    /// <param name="?"></param>
    /// <returns></returns>
    static string ExtractPathFromPath(out string hodnota, string cesta)
    {
        int hodnotaOd = cesta.LastIndexOf(oddeloacRegistru);
        hodnota = cesta.Substring(hodnotaOd);
        string vratit = cesta.Substring(0, hodnotaOd);
        return vratit;
    }
    #endregion

    #region NI
    #region IPrevedPpk<RegistryKey,PolozkaRegistru> Members

    /// <summary>
    /// Prevede seznam PolozkaRegistru na seznam RegistryKey. 
    /// Neprevadi nic, naplni vse null
    /// </summary>
    /// <param name="uu"></param>
    /// <returns></returns>
    public static List<RegistryKey> ConvertPpk(List<RegistryEntry> uu)
    {
        List<RegistryKey> ppk = new List<RegistryKey>();
        foreach (RegistryEntry var in uu)
        {
            ppk.Add(null);
        }
        return ppk;
    }

    /// <summary>
    /// Prevede seznam PolozkaRegistru na seznam RegistryKey. 
    /// Neprevadi nic, naplni vse null
    /// </summary>
    /// <param name="uu"></param>
    /// <returns></returns>
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
         SH.GetPartsByLocation(out hkey, out key, p, '\\');
    }
}




