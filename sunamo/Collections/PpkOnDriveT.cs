﻿/// <summary>
/// Kolekce na retezce.
///  V kazdem programu doporucuji pouzivat jen jedinou instanci protoze jinak jinak se mohou ukoncoval nelogicky.
/// Tuto PPk uzivej jen na nacteni nebo uulozeni, ale nikdy ne soucasne.
/// </summary>


using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using sunamo;

public class PpkOnDrive<T> : PpkOnDriveBase<T> where T : IParser
{
    /// <summary>
    /// Nacte soubory.
    /// </summary>
    public override void Load()
    {
        if (FS.ExistsFile(soubor))
        {
            int dex = 0;
            foreach (string item in TF.ReadAllLines(soubor))
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                t.Parse(item);
                base.Add(t);
                dex++;
            }
        }
    }

    public PpkOnDrive(bool load) : base(load)
    {

    }

    public PpkOnDrive()
    {

    }

    public PpkOnDrive(string file2, bool load = true) : base(file2, load)
    {
    }

    public PpkOnDrive(string file, bool load, bool save) : base(file, load, save)
    {

    }

    public PpkOnDrive(bool open, bool load) : base(open, load)
    {

    }
}