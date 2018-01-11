using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class WaitingManagerAspx
{
    private static Dictionary<int, Hashtable> _results = new Dictionary<int, Hashtable>();

    /// <summary>
    /// Stará metoda, získavá result ve Stringu.
    /// Pokud se objekt nepodaří získat, vrátí SE
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetResult(int idUzivatele, int idPage)
    {
        if (_results.ContainsKey(idUzivatele))
        {
            Hashtable ht = _results[idUzivatele];
            if (ht.ContainsKey(idPage))
            {
                return Convert.ToString(_results[idPage]);    
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return String.Empty;
        }
    }

    /// <summary>
    /// Nová metoda, získává objekt jako výsledek který lze přetypovat na libovolný jiný typ - například přímo do chybové zprávy(HtmlGenericControl).
    /// Pokud se objekt nepodaří získat, vrátí null
    /// Před touto metodou se musí zavolat Contains, a tato metoda vč. Remove se může vykonávat použe když vrátí True
    /// Po této metodě se musí zavolat Remove
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static object GetResultObject(int idUzivatele, int idPage)
    {
        if (_results.ContainsKey(idUzivatele))
        {
            Hashtable ht = _results[idUzivatele];
            if (ht.ContainsKey(idPage))
            {
                return ht[idPage];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static void Add(int idUzivatele, int idPage, object value)
    {
        Hashtable ht = null;
        if (_results.ContainsKey(idUzivatele))
        {
            ht = _results[idUzivatele];
        }
        else
        {
            Hashtable h = new Hashtable();
            _results.Add(idUzivatele, h);
            ht = h;
        }
        if (ht.ContainsKey(idPage))
        {
            ht[idPage] = value;
        }
        else
        {
            ht.Add(idPage, value);
        }
    }

    public static bool Contains(int idUzivatele, int idPage)
    {
        if (_results.ContainsKey(idUzivatele))
        {
            return _results[idUzivatele].ContainsKey(idPage);
        }
        return false;
    }

    /// <summary>
    /// Nekontroluje na to zda položka existuje, prostě ji jednoduše odstraní.
    /// </summary>
    /// <param name="idUzivatele"></param>
    /// <param name="idPage"></param>
    public static void Remove(int idUzivatele, int idPage)
    {
        _results[idUzivatele].Remove(idPage);
    }
}
