﻿
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
/// <summary>
/// Kolekce na retezce.
///  V kazdem programu doporucuji pouzivat jen jedinou instanci protoze jinak jinak se mohou ukoncoval nelogicky.
/// Tuto PPk uzivej jen na nacteni nebo uulozeni, ale nikdy ne soucasne.
/// Musi to byt duplikovane, protoze string nededi od IParser a bez ni nemam jak pridat nove metody :-(
/// </summary>
public abstract class PpkOnDriveBase<T> : List<T>
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
    #endregion

    public abstract void Load();

    public void AddWithoutSave(T t)
    {
        if (!base.Contains(t))
        {
            base.Add(t);
        }
        
    }

    public new void Add(T prvek)
    {
        if (!base.Contains(prvek))
        {
            base.Add(prvek);
        }
        
        if (ukladat)
        {
            Save();
        }

    }

    #region base
    /// <summary>
    /// Pokud A1, nacte ze souboru, takze pri ulozeni bude pripsan novy obsah.
    /// </summary>
    /// <param name="load"></param>
    public PpkOnDriveBase(bool load)
    {
        if (load)
        {
            Load();
        }
    }

    /// <summary>
    /// Ik. Nenacita z souboru, pri ukladani se tedy jeho obsah smaze.
    /// </summary>
    public PpkOnDriveBase()
    {

    }

    /// <summary>
    /// Zavede do ppk s ruznym obsahem dle souboru.
    /// </summary>
    /// <param name="soubor"></param>
    /// <param name="load"></param>
    public PpkOnDriveBase(string file2, bool load = true)
    {
        soubor = file2;
        if (load)
        {
            Load();
        }
    }

    /// <summary>
    /// Zavede do ppk s ruznym obsahem dle souboru.
    /// </summary>
    /// <param name="soubor"></param>
    /// <param name="load"></param>
    public PpkOnDriveBase(string file, bool load, bool save)
    {
        if (!FS.ExistsFile(file))
        {
            File.WriteAllText(file, "");
        }
        this.ukladat = save;
        soubor = file;
        Load(load);
    }

    public PpkOnDriveBase(bool open, bool load)
    {
        this.otevrit = open;
        Load(load);
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



    /// <summary>
    /// Ulozi oubor do std. nazvu souboru aplikace.
    /// </summary>
    public void Save()
    {
        if (FS.ExistsFile(soubor))
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

        foreach (T var in this)
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
