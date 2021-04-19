using System;
using System.Collections.Generic;
using System.Text;

public class UnindexableHelper
{
    public static Unindexable unindexable = null;

    public static PpkOnDrive unindexablePathParts => unindexable.unindexablePathParts;
    public static PpkOnDrive unindexableFileNames => unindexable.unindexableFileNames;
    public static PpkOnDrive unindexablePathEnds => unindexable.unindexablePathEnds;
    public static PpkOnDrive unindexablePathStarts => unindexable.unindexablePathStarts;

    public static void Load(UnindexableFilesPaths f)
    {
        unindexable = new Unindexable();
        unindexable.unindexablePathParts = new PpkOnDrive(f.fileUnindexablePathParts);
        unindexable.unindexableFileNames = new PpkOnDrive(f.fileUnindexableFileNames);
        unindexable.unindexablePathEnds = new PpkOnDrive(f.fileUnindexablePathEnds);
        unindexable.unindexablePathStarts = new PpkOnDrive(f.fileUnindexablePathStarts);
    }

    public static bool IsToIndexedFolder(string d)
    {
        
        if (unindexablePathParts.TrueForAll(e => !d.Contains(e)))
        {
            if (unindexablePathStarts.TrueForAll(e => !d.StartsWith(e)))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsToIndexed(string d, string fn, Func<string, bool> sci_IsIndexed)
    {
        //Checking for sth for which is checking in SourceCodeIndexerRoslyn.ProcessFile
        if (unindexablePathEnds.TrueForAll(e => !d.EndsWith(e)))
        {
            if (unindexableFileNames.TrueForAll(e => !fn.Contains(e)))
            {
                if (sci_IsIndexed == null)
                {
                    return true;
                }
                else
                {
                    return sci_IsIndexed(d);
                }
            }
        }
        return false;
    }
}