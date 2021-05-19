using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetFilesMoreMascArgs
{
    public bool LoadFromFileWhenDebug = false;
    public string path;
    public string masc = "*";
    public SearchOption searchOption = SearchOption.TopDirectoryOnly;
    public bool deleteFromDriveWhenCannotBeResolved = false;
}