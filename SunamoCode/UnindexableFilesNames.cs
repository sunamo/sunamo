using System;
using System.Collections.Generic;
using System.Text;

public class UnindexableFilesNames
{
    public static UnindexableFilesNames ci = new UnindexableFilesNames();

    private UnindexableFilesNames()
    {

    }

    public string fileUnindexablePathParts = "unindexablePathParts.txt";
    public string fileUnindexableFileNames = "unindexableFileNames.txt";
    public string fileUnindexablePathEnds = "unindexablePathEnds.txt";
    public string fileUnindexablePathStarts = "unindexablePathStarts.txt";
}