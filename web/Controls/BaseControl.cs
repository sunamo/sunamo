using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Summary description for BaseControl
/// </summary>
public abstract class BaseControl
{
    /// <summary>
    /// Závisí na každém prvku, zda bude tuto vlastnost renderovat, v této třídě se nerenderuje nic
    /// </summary>
    public string Class = null;
    /// <summary>
    /// Závisí na každém prvku, zda bude tuto vlastnost renderovat, v této třídě se nerenderuje nic
    /// </summary>
    public string ID = null;
    /// <summary>
    /// Závisí na každém prvku, zda bude tuto vlastnost renderovat, v této třídě se nerenderuje nic
    /// </summary>
    public string Style = null;
    /// <summary>
    /// Záleží na odvozené třídě zda bude tento atribut využívat k naplnění libovolné proměnné, zde se tento delegát nijak nevyužívá
    /// </summary>
    public StringStringHandler Delegate = null;
    /// <summary>
    /// U Anchoru je toto atribut href. může se mu zadat pouze číslo v složených závorkách - on pak toto číslo dosadí do atributu href. Použitelné pouze tehdy, pokud 
    /// </summary>
    public string DelegateArgs = null;
    /// <summary>
    /// Metoda do které se předá index řádku, aby mohla vzít ten správný řádek z string[][] a vyvodit z něho hodnoty
    /// </summary>
    /// <returns></returns>
    public abstract string Render(int actualRow, List<String[]> _dataBinding);
    public List<String[]> _dataBinding = null;
    public int actualRow = -1;
    /// <summary>
    /// Pokud bych chtěl někdy znovu zavést, vyhledat v celém řešení na repeteDuringRow a odkomentovat
    /// </summary>
    //public bool repeteDuringRow = false;
    /// <summary>
    /// Znak { - počáteční
    /// </summary>
    public static char p = '{';
    /// <summary>
    /// Znak } - ukončovací
    /// </summary>
    public static char k = '}';
    

	public BaseControl()
	{
	}

    /// <summary>
    /// Renderování pomocí HtmlDocument
    /// Metoda, kterou musí vždy zavolat metoda Render. Právě proto je taky chráněná, aby ji nemohl volat "kde kdo"
    /// </summary>
    /// <param name="hg"></param>
    /// <returns></returns>
    protected abstract string RenderHtml(HtmlGenerator hg);

    /// <summary>
    /// Renderování pomocí delegátu
    /// Uloží do proměnné A2 hodnotu z delegátu Delegate do kterého vloží DelegateArgs(který může být i proměnný {x}) a vrátí výstup metody RenderHtml(HtmlGenerator)
    /// Slouží k tomu, když chceš delegát použít pro libovolnou proměnnou
    /// </summary>
    /// <param name="hg"></param>
    /// <param name="src"></param>
    /// <returns></returns>
    protected string CallDelegate(HtmlGenerator hg, ref string src)
    {
        //string vr = "";
        if (actualRow != -1)
        {
            string parametry = DelegateArgs;
            string[] s = null;
            string oddelovac = "";
            if (parametry.Contains(','))
            {
                s = SH.Split(parametry, ',');
                oddelovac = ",";
            }
            else if (parametry.Contains('|'))
            {
                s = SH.Split(parametry, '|');
                oddelovac = "|";
            }
            else
            {
                src = Delegate.Invoke(_dataBinding[int.Parse(parametry.Substring(1, parametry.Length - 2))][actualRow]);
                return RenderHtml(hg);
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in s)
            {
                if (item.StartsWith("{") && item.EndsWith("}"))
                {
                    sb.Append(_dataBinding[int.Parse(item.Substring(1, item.Length - 2))][actualRow] + oddelovac);    
                }
                else
                {
                    sb.Append(item + oddelovac);
                }
            }
            src = Delegate.Invoke(sb.ToString());
            return RenderHtml(hg);
        }
            return "";
    }

    
}
