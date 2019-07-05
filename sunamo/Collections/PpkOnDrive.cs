
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class PpkOnDrive : PpkOnDriveBase<string>
{
    /// <summary>
    /// Nacte soubory.
    /// </summary>
    public override void Load()
    {
        if (FS.ExistsFile(soubor))
        {
            this.AddRange(File.ReadAllLines(soubor));
        }
    }
}