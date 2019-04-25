using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class JavaScriptGeneratorWebForms
    {
    /// <summary>
    /// To co vrátí handler vrátí i tato metoda
    /// Neinjektuje přímo do stránky ale vrací JS který teprve pak vloží
    /// </summary>
    /// <param name="nameOfFunction"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string JQueryAjaxForHandler(MySitesShort ms, int countUp, string nameOfFunction, params string[] args)
    {
        string pridat = "";
        //pridat = "Handler.ashx";
        return @"function " + nameOfFunction + "(" + SH.Join(',', args) + ") {" + Environment.NewLine + "return ajaxGet(" + QSHelperWebForms.VratQSSimple2(ms, countUp, nameOfFunction + pridat, args) + ");" + Environment.NewLine + "}";
    }
}