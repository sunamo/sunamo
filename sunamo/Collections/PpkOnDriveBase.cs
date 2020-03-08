
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using sunamo;
/// <summary>
/// Checking whether string is already contained.
/// Kolekce na retezce.
///  V kazdem programu doporucuji pouzivat jen jedinou instanci protoze jinak jinak se mohou ukoncoval nelogicky.
/// Tuto PPk uzivej jen na nacteni nebo uulozeni, ale nikdy ne soucasne.
/// Musi to byt duplikovane, protoze string nededi od IParser a bez ni nemam jak pridat nove metody :-(
/// </summary>
public abstract class PpkOnDriveBase<T> : List<T>
{
    #region DPP
    protected PpkOnDriveArgs a = null;
    #endregion

    public  void RemoveAll()
    {
        TF.WriteAllText(a.file, string.Empty);
    }

    public abstract void Load();

    public void AddWithoutSave(T t)
    {
        if (!base.Contains(t))
        {
            base.Add(t);
        }
    }

    public  void Add(IEnumerable<T> prvek)
    {
        foreach (var item in prvek)
        {
            Add(item);
        }
    }

    public new bool Add(T prvek)
    {
        bool b = false;
        if (!base.Contains(prvek))
        {
            if (prvek.ToString().Trim() != string.Empty)
            {
                base.Add(prvek);
                b = true;
            }
            else
            {
                // keep on false
            }
        }
        else
        {
            // keep on false
        }

        if (a.save)
        {

            Save();
        }

        return b;
    }
    bool isSaving = false;

    /// <summary>
    /// Must use FileSystemWatcher, not FileSystemWatcher because its in sunamo, not desktop
    /// </summary>
    FileSystemWatcher w = null;

    #region base
    public PpkOnDriveBase(PpkOnDriveArgs a)
    {
        this.a = a;
        FS.CreateFileIfDoesntExists(a.file);
        Load(a.load);

        if (a.loadChangesFromDrive)
        {
            w = new FileSystemWatcher(FS.GetDirectoryName(a.file));
            w.Filter = a.file;
            w.Changed += W_Changed;
        }
    }

    private void W_Changed(object sender, FileSystemEventArgs e)
    {
        if (!isSaving)
        {
            Load();
        }
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
        isSaving = true;
        if (FS.ExistsFile(a.file))
        {
            File.Delete(a.file);
        }
        string obsah;
        obsah = ReturnContent();
        //TextovySoubor.ts.UlozSoubor(obsah, soubor);
        File.WriteAllText(a.file, obsah);
        isSaving = false;
    }

    /// <summary>
    /// 
    /// </summary>
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
    public override string ToString()
    {
        return ReturnContent();
    }
}