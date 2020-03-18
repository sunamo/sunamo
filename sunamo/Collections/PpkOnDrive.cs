
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
    public static PpkOnDrive WroteOnDrive
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
        if (FS.ExistsFile(a.file))
        {
            this.Clear();
            this.AddRange(TF.ReadAllLines(a.file));

            CA.RemoveStringsEmpty2(this);
        }
    }

    public PpkOnDrive(PpkOnDriveArgs a) : base(a)
    {
    }

    public PpkOnDrive(string file2, bool load = true) : base(new PpkOnDriveArgs { file = file2, load= load  })
    {
    }

    public PpkOnDrive(string file, bool load, bool save) : base(new PpkOnDriveArgs { file = file, load = load, save = save })
    {
    }
}