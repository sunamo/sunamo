using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnindexableFiles
{
    public static UnindexableFiles uf = new UnindexableFiles();

    private UnindexableFiles()
    {
    }

    public CollectionWithoutDuplicates<string> unindexablePathPartsFiles = new CollectionWithoutDuplicates<string>();
    public CollectionWithoutDuplicates<string> unindexableFileNamesFiles = new CollectionWithoutDuplicates<string>();
    public CollectionWithoutDuplicates<string> unindexablePathEndsFiles = new CollectionWithoutDuplicates<string>();
    public CollectionWithoutDuplicates<string> unindexablePathStartsFiles = new CollectionWithoutDuplicates<string>();
}