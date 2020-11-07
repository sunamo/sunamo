using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using sunamo;

public class PpkOnDrive<T> : PpkOnDriveBase<T> where T : IParser
{
    public override void Load()
    {
        if (FS.ExistsFile(a.file))
        {
            int dex = 0;
            foreach (string item in TF.ReadAllLines(a.file))
            {
                T t = (T)Activator.CreateInstance(typeof(T));
                t.Parse(item);
                base.Add(t);
                dex++;
            }
        }
    }

    public PpkOnDrive(PpkOnDriveArgs a) : base(a)
    {

    }

    public PpkOnDrive(string file2, bool load = true) : base(new PpkOnDriveArgs { file = file2, load = load })
    {
    }

    public PpkOnDrive(string file, bool load, bool save) : base(new PpkOnDriveArgs { file = file, load = load, save = save })
    {
    }
}