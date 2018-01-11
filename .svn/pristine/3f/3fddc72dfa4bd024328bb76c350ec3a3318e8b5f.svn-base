using System.Collections.Generic;

using System.Web.UI;
using System.Linq;
using System;
public static class PageArgumentVerifier
{

    /// <summary>
    /// Vrátí nomralizované parametry, které se pak budou používat při každém dotazu na stránku
    /// U routed pages se zde nezadává žádný atribut, protože tyto stárnky nemají klasické argumenty v uri
    /// </summary>
    /// <param name="pas"></param>
    /// <returns></returns>
    public static PageArgumentName[] GetNormalizedParameters(params PageArgumentName[] pas)
    {
        if (pas.Length != 0)
        {
            SortedDictionary<string, PageArgumentName> d = new SortedDictionary<string, PageArgumentName>();
            foreach (var item in pas)
            {
                if (item.type == PageArgumentType.None)
                {
                    throw new Exception("Bylo předáno do metody GetNormalizedParameters type parametru None");
                }
                d.Add(item.value, item);
            }
            d.OrderBy(s => s.Key);
            return d.Values.ToArray();
        }
        return new PageArgumentName[0];
    }

    /// <summary>
    /// Zjistím ID webu a název stránky A1 do proměnných A1. Pokud se mi nepodaří zjistim, do A2 SE a G true
    /// Jinak vložím do A2 QS stránky bez poč. ? a vrátím false
    /// </summary>
    /// <param name="page"></param>
    /// <param name="ruq"></param>
    /// <returns></returns>
    static bool SetWriteRowsCommon(SunamoPage page, out string ruq)
    {
        byte idWeb = 0;
        string stranka = "";
        PageArgumentVerifier.GetIDWebAndNameOfPage(out idWeb, out stranka, page.Request.FilePath);

        if (stranka == "")
        {
            ruq = "";
            page.writeRows = false;
            return true;
        }
        page.IDWeb = idWeb;
        page.namePage = stranka;
        ruq = page.Request.Url.Query;
        if (ruq.Length != 0)
        {
            ruq = ruq.Substring(1);
        }
        return false;
    }

    /// <summary>
    /// Když A2 bude null, jen zapíšu A1.writeRows na true
    /// Když A2 nebude null, nastavím A1.writeRows na false, pokud nebude mít parametry dle A2
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pans"></param>
    public static void SetWriteRows(SunamoPage page, PageArgumentName[] pans)
    {
        if (pans != null)
        {
            string ruq = null;
            if (SetWriteRowsCommon(page, out ruq))
            {
                return;
            }
            page.args = GetNormalizeQS(ruq, pans);
            if (page.args == null)
            {
                page.writeRows = false;
                return;
            }
        }
        page.writeRows = true;

    }

    /// <summary>
    /// Vrátí null v případě že některý parametr nebude mít správnou hodnotu podle A2, nebo pokud nebude nalezen nějaký povinný parametr
    /// Vrátí seznam parametrů oddělených amp jako běžný QueryString
    /// Do A1 se už musí předat bez počátečního otazníku
    /// Pokud bude chybět nějaký povinný parametr nebo pokud bude nějaký parametr navíc, vrátím null
    /// </summary>
    /// <param name="args"></param>
    /// <param name="pas"></param>
    /// <returns></returns>
    public static string GetNormalizeQS(string args, PageArgumentName[] pas)
    {
        Dictionary<string, PageArgumentName> pa = new Dictionary<string, PageArgumentName>();
        foreach (var item in pas)
        {
            pa.Add(item.value, item);
        }
        if (args.Length != 0)
        {
            //Dictionary<decimal,
            List<string> vr = new List<string>();
            List<string> splited = new List<string>(SH.Split(args, '&'));
            splited.Sort();
            for (int i = splited.Count - 1; i >= 0; i--)
            {
                string item = splited[i];
            
            
                string value;
                string name;
                SH.GetPartsByLocation(out name, out value, item, '=');
                if (pa.ContainsKey(name))
                {
                    if (IsRightValue(pa[name].type, pa[name].availableValues, value))
                    {
                        // Ve parametru args SunamoPage budou pouze povinné parametry
                        if (pa[name].isCompulsory)
                        {
                            vr.Add(item);    
                        } 
                    }
                    else
                    {
                        return null;
                    }
                    pa.Remove(name);
                    splited.RemoveAt(i);
                }
            }
            // Pokud bude chybět nějaký povinný parametr, vrátím null
            foreach (var item in pa)
            {
                if (item.Value.isCompulsory)
                {
                    return null;
                }
            }
            // Pokud bude nějaký parametr navíc, vrátím taky null
            if (splited.Count != 0)
            {
                return null;
            }
            vr.Reverse();
            return SH.Join('&', vr.ToArray());
        }
        return "";
    }

    /// <summary>
    /// Vrátí null v případě jakékoliv chyby.
    /// </summary>
    /// <param name="args"></param>
    /// <param name="pansRequiredAtLeastOne"></param>
    /// <param name="pansNotRequired"></param>
    /// <returns></returns>
    private static string GetNormalizeQS(string args, PageArgumentName[] pansRequiredAtLeastOne, PageArgumentName[] pansNotRequired)
    {
        Dictionary<string, PageArgumentName> pa = new Dictionary<string, PageArgumentName>();
        foreach (var item in pansRequiredAtLeastOne)
        {
            pa.Add(item.value, item);
        }

        Dictionary<string, PageArgumentName> paWhichDelete = new Dictionary<string, PageArgumentName>();
        foreach (var item in pansNotRequired)
        {
            paWhichDelete.Add(item.value, item);
        }
        if (args.Length != 0)
        {
            //Dictionary<decimal,
            List<string> vr = new List<string>();
            List<string> splited = new List<string>(SH.Split(args, '&'));
            splited.Sort();
            for (int i = splited.Count - 1; i >= 0; i--)
            {
                string item = splited[i];


                string value;
                string name;
                SH.GetPartsByLocation(out name, out value, item, '=');
                if (pa.ContainsKey(name))
                {
                    if (IsRightValue(pa[name].type, pa[name].availableValues, value))
                    {
                            vr.Add(item);
                        //}
                    }
                    else
                    {
                        return null;
                    }
                    pa.Remove(name);
                    splited.RemoveAt(i);
                }
                if (paWhichDelete.ContainsKey(name))
                {
                    pa.Remove(name);
                    splited.RemoveAt(i);
                }
            }
            if (splited.Count != 0)
            {
                return null;
            }
            vr.Reverse();
            return SH.Join('&', vr.ToArray());
        }
        return "";
    }

    private static bool IsRightValue(PageArgumentType t, string[] availableValues, string value)
    {
        if (availableValues != null)
        {
            return CA.Contains(value, availableValues);
        }
        if (t == PageArgumentType.String)
        {
            
            // Pokud budu používat řetězec v QS, budu muset nevolat nastavovat writeRows ještě po ověření zda QS směřuje na existující položku v DB
            return true;           
        }
        else if(t == PageArgumentType.Int32)
        {
            Int32 d = 0;
            if (Int32.TryParse(value, out d))
            {
                
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.UInt32)
        {
            UInt32 d = 0;
            if (UInt32.TryParse(value, out d))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Int16)
        {
            Int16 d = 0;
            if (Int16.TryParse(value, out d))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.UInt16)
        {
            UInt16 d = 0;
            if (UInt16.TryParse(value, out d))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Int64)
        {
            Int64 d = 0;
            if (Int64.TryParse(value, out d))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.UInt64)
        {
            UInt64 d = 0;
            if (UInt64.TryParse(value, out d))
            {
                return true;
            }
            return false;
        }
        else if(t == PageArgumentType.Single)
        {
            Single s = 0;
            if (Single.TryParse(value, out s))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Double)
        {
            Double s = 0;
            if (Double.TryParse(value, out s))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Char)
        {
            if (value.Length == 1)
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Boolean)
        {
            if (value == true.ToString() || value == false.ToString())
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Decimal)
        {
            decimal d = 0;
            if (decimal.TryParse(value, out d))
            {
                return true;
            }
            return false;
        }
        else if(t == PageArgumentType.Byte)
        {
            Byte b = 0;
            if (Byte.TryParse(value, out b))
            {
                return true;
            }
            return false;
        }
        else if (t == PageArgumentType.Sbyte)
        {
            SByte s = 0;
            if (SByte.TryParse(value, out s)) 
            {
                return true;
            }
            return false;
        }
        throw new Exception("Neimplementovaný typ v metodě PageArgumentVerifier.IsRightValue");
        return false;
    }

    /// <summary>
    /// Do A1 se zadává Page.Request.FilePath neboli např. /Kocicky/Photo.aspx (bez argumentů) - lepší to je ale bez toho počátečního lomítka
    /// </summary>
    /// <param name="IDWeb"></param>
    /// <param name="stranka"></param>
    /// <param name="sunamoPage"></param>
    public static void GetIDWebAndNameOfPage(out byte IDWeb, out string stranka, string df)
    {
        if (df[0] == '/')
        {
            df = df.Substring(1);
        }
        string[] tokeny = SH.Split(df, "/");
        if (tokeny[0].Length == 3)
        {
            IDWeb = (byte)((MySitesShort)Enum.Parse(typeof(MySitesShort), tokeny[0], true));
        }
        else
        {
            IDWeb = MySitesConverter.ConvertFrom(tokeny[0].ToLower());
        }
        
        //stranka = tokeny[0];
        bool route = false;
        MySitesShort mss= ((MySitesShort)IDWeb);
        if (mss == MySitesShort.App || mss == MySitesShort.Wmc)
        {
            route = true; 
        }
        if (!route)
        {
            if (tokeny.Length == 2)
            {
                //IDWeb = MySitesConverter.ConvertFrom(tokeny[0].ToLower());
                stranka = tokeny[1].ToLower();
                return;
            }
            else if (tokeny.Length == 1)
            {
                stranka = tokeny[0].ToLower();

                //stranka = stranka.ToLower();
                bool obsahujeTecku = false;
                for (int i = stranka.Length - 1; i >= 0; i--)
                {
                    if (stranka[i] == '.')
                    {
                        obsahujeTecku = true;
                        break;
                    }
                }
                if (!obsahujeTecku)
                {
                    // Kocicky/
                    IDWeb = MySitesConverter.ConvertFrom(stranka);
                    stranka = "default.aspx";
                }
                else
                {
                    IDWeb = 8;
                }
                return;
            }
            IDWeb = 8;
            stranka = "";
        }
        tokeny = SH.SplitToParts(df, 2, "/");
        stranka = tokeny[1].ToLower();
    }




    public static void SetWriteRows(SunamoPage page, PageArgumentName[] pansRequiredAtLeastOne, PageArgumentName[] pansNotRequired)
    {
        if (pansRequiredAtLeastOne != null && pansNotRequired != null)
        {
            string ruq = null;
            if (SetWriteRowsCommon(page, out ruq))
            {
                return;
            }
            // Vrátí null v případě že stránka bude obsahovat některý ze speciálních parametrů, které se do DB neukládají(contextkey=, atd.)
            page.args = GetNormalizeQS(ruq, pansRequiredAtLeastOne, pansNotRequired);
            if (page.args == null)
            {
                page.writeRows = false;
                return;
            }
        }
        page.writeRows = true;
    }
}

public class PageArgumentName
{

    /// <summary>
    /// A2 je samozřejmě název parametru(například idCat)
    /// A3 zda je atribut povinný pro WriteRows,
    /// Pokud je ve stránce jen 1 pole pans, zahrnou se do parametrů URI pouze ty, které mají A3
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isCompulsory"></param>
    public PageArgumentName(PageArgumentType type, string value, bool isCompulsory)
    {
        this.type = type;
        this.value = value;
        this.isCompulsory = isCompulsory;
    }

    /// <summary>
    /// Pokud A1 je String, A2 může být jakékoliv.
    /// Pokud A1 je String, a chceš specifikovat pouze konkrétní povolené hodnoty, vypiš je do A4 oodělené čárkou. Pokud by bylo A4 null a A1 String, budou povolené všechny hodnoty.
    /// A2 je samozřejmě název parametru(například idCat)
    /// A3 zda je atribut povinný pro WriteRows
    /// Pokud je ve stránce jen 1 pole pans, zahrnou se do parametrů URI pouze ty, které mají A3
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isCompulsory"></param>
    /// <param name="availableValues"></param>
    public PageArgumentName(PageArgumentType type, string value, bool isCompulsory, string availableValues) : this(type, value, isCompulsory)
    {
        this.availableValues = SH.Split(availableValues, ',');
    }

    public PageArgumentType type = PageArgumentType.None;
    public string value = "";
    public bool isCompulsory = false;
    public string[] availableValues = null;
    public static PageArgumentName[] EmptyArray = PageArgumentVerifier.GetNormalizedParameters();
}

public class PageArgument
{
    public PageArgument(PageArgumentType type, string value)
    {
        this.type = type;
        this.value = value;
    }

    public PageArgumentType type = PageArgumentType.None;
    public string value = "";
}

