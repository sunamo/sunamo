using System;
using System.Collections.Generic;
using System.Text;

public class UnindexableFilesPaths
{
    public string fileUnindexablePathParts;
    public string fileUnindexableFileNames;
    public string fileUnindexablePathEnds;
    public string fileUnindexablePathStarts;

    public UnindexableFilesPaths(string p)
    {
        var f = UnindexableFilesNames.ci;
        fileUnindexablePathParts = p + f.fileUnindexablePathParts;
        fileUnindexableFileNames = p + f.fileUnindexableFileNames;
        fileUnindexablePathEnds = p + f.fileUnindexablePathEnds;
        fileUnindexablePathStarts = p + f.fileUnindexablePathStarts;
    }
    public UnindexableFilesPaths()
    {

    }
}