
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

/// <summary>
/// Checking whether string is already contained.
/// </summary>
public class PpkOnDrive : PpkOnDriveBase<string>
{
    static PpkOnDrive wroteOnDrive = null;
    internal static PpkOnDrive WroteOnDrive
    {
        get
        {
            if (wroteOnDrive == null)
            {
                wroteOnDrive = new PpkOnDrive(AppData.ci.GetFile(AppFolders.Logs, "WrittenFiles.txt"));
            }
            return wroteOnDrive;
        }
    }

    /// <summary>
    /// Nacte soubory.
    /// </summary>
    public override void Load()
    {
        if (FS.ExistsFile(soubor))
        {
            this.AddRange(TF.ReadAllLines(soubor));

            CA.RemoveStringsEmpty2(this);
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