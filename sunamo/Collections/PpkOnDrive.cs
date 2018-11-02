
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
        if (File.Exists(soubor))
        {
            this.AddRange(File.ReadAllLines(soubor));
        }
    }
}