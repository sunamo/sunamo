using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Core;
using sunamo;

/// <summary>
/// M�lo by tu v�echno fungovat, str�vil jsem nad t�m hezk�ch p�r hodin :(
/// </summary>
public class ZA
{
    #region IArchiv Members
    public static ZA zip = new ZA();

    /// <summary>
    /// Kdy� komprimuji celou slo�ku A1, vr�t� mi arch�v zip jm�na slo�ky A1 ve slo�ce ve kter� je A1.
    /// </summary>
    /// <param name="slozku"></param>
    
    private string OdstranZJmenaSouboru(string slozku, string p)
    {
        string c = FS.GetDirectoryName(slozku);
        string s = FS.GetFileName(slozku).Replace(p, "");
        return FS.Combine(c, s);
    }

    
    #endregion

    public void CreateArchive(string slozku)
    {
        CreateArchive(slozku, VratSouboryRek(slozku), VratJmenoSouboruZip(slozku));
    }

    private List<string> VratSouboryRek(string slozku)
    {
        return FS.GetFiles(slozku, AllStrings.asterisk, SearchOption.AllDirectories);
    }

    public void CreateArchive(string slozka, List<string> soubory)
    {
        CreateArchive(slozka, soubory, VratJmenoSouboruZip(slozka));
    }

    public void CreateArchive(string slozku, string souborZip)
    {
        CreateArchive(slozku, VratSouboryRek(slozku), souborZip);
    }

    public void ExtractArchive(string soubor)
    {
        ExtractArchive(soubor, OdstranZJmenaSouboru(soubor, ".zip"));
    }
}

