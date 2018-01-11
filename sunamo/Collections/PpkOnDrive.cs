
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
/// <summary>
/// Kolekce na retezce.
///  V kazdem programu doporucuji pouzivat jen jedinou instanci protoze jinak jinak se mohou ukoncoval nelogicky.
/// Tuto PPk uzivej jen na nacteni nebo uulozeni, ale nikdy ne soucasne.
/// Mus� to b�t duplikovan�, proto�e string ned�d� od IParser a bez n� nem�m jak p�idat nov� metody :-(
/// </summary>
public class PpkOnDrive : List<string>
{
    #region DPP
    bool ukladat = true;
    /// <summary>
    /// 
    /// </summary>
    bool otevrit = false;
    /// <summary>
    /// Cesta, do ktere se uklada soubor.
    /// </summary>
    public string soubor = null;
    public event EmptyHandler Nahrane;
    /// <summary>
    /// Zda jiz byl soubor ulozen. Pracuje se s nim jen v D takze vzdy pujde ulozit maximalne 1 instance.
    /// </summary>
    static bool disp = false;
    #endregion

    public void AddWithoutSave(string t)
    {
        base.Add(t);
    }

    public new void Add(string prvek)
    {
        base.Add(prvek);
        if (ukladat)
        {
            Save();
        }

    }

    #region base
    /// <summary>
    /// Pokud A1, nacte ze souboru, takze pri ulozeni bude pripsan novy obsah.
    /// </summary>
    /// <param name="nacistZeSouboru"></param>
    public PpkOnDrive(bool nacistZeSouboru)
    {
        if (nacistZeSouboru)
        {
            Load();
        }
    }

    /// <summary>
    /// Ik. Nenacita z souboru, pri ukladani se tedy jeho obsah smaze.
    /// </summary>
    public PpkOnDrive()
    {

    }

    /// <summary>
    /// Zavede do ppk s ruznym obsahem dle souboru.
    /// </summary>
    /// <param name="soubor"></param>
    /// <param name="nacist"></param>
    public PpkOnDrive(string soubor2, bool nacist)
    {
        soubor = soubor2;
        if (nacist)
        {
            Load();
        }
    }

    /// <summary>
    /// Zavede do ppk s ruznym obsahem dle souboru.
    /// </summary>
    /// <param name="soubor"></param>
    /// <param name="nacist"></param>
    public PpkOnDrive(string soubor2, bool nacist, bool ukladat)
    {
        if (!File.Exists(soubor2))
        {
            File.WriteAllText(soubor2, "");
        }
        this.ukladat = ukladat;
        soubor = soubor2;
        Load(nacist);
    }

    public PpkOnDrive(bool otevrit, bool nacist)
    {
        this.otevrit = otevrit;
        Load(nacist);
    }
    #endregion

    /// <summary>
    /// Dle A1 ihned nacte
    /// </summary>
    /// <param name="nacist"></param>
    private void Load(bool nacist)
    {
        if (nacist)
        {
            Load();
        }
    }


    

    static string obsah = "";

    /// <summary>
    /// Nacte soubory.
    /// </summary>
    public void Load()
    {
        if (File.Exists(soubor))
        {

            this.AddRange(File.ReadAllLines(soubor));
        }
    }

    /// <summary>
    /// Ulozi oubor do std. nazvu souboru aplikace.
    /// </summary>
    public void Save()
    {
        if (File.Exists(soubor))
        {
            File.Delete(soubor);
        }
        string obsah;
        obsah = ReturnContent();
        //TextovySoubor.ts.UlozSoubor(obsah, soubor);
        File.WriteAllText(soubor, obsah);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string ReturnContent()
    {
        string obsah;
        StringBuilder sb = new StringBuilder();

        foreach (string var in this)
        {
            sb.AppendLine(var.ToString());
        }

        obsah = sb.ToString();
        return obsah;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return ReturnContent();
    }
}
