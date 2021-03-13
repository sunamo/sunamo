
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using sunamo;

public abstract class PpkOnDriveBase<T> : List<T>
{
    #region DPP
    protected PpkOnDriveArgs a = null;
    #endregion

    public  void RemoveAll()
    {
        Clear();
        TF.WriteAllText(a.file, string.Empty);
    }

    public new void Remove(T t)
    {
        base.Remove(t);
        
            Save();
        
    }

    public new void Clear()
    {
        base.Clear();
        Save();
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

        
            Save();
        

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

    private void Load(bool loadImmediately)
    {
        if (loadImmediately)
        {
            Load();
        }
    }

    public void Save()
    {
        if (a.save)
        {
            isSaving = true;
            bool removedOrNotExists = false;
            if (FS.ExistsFile(a.file))
            {
                removedOrNotExists = FS.TryDeleteFile(a.file);
            }

            if (removedOrNotExists)
            {
                string obsah;
                obsah = ReturnContent();
                FS.WriteAllTextWithExc(a.file, obsah);
                
            }
            isSaving = false;
        }
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