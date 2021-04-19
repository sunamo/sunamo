using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetFilesEveryFolderArgs : GetFilesBaseArgs
{
    public bool usePbTime = false;
    public Action<double> InsertPbTime = null;
    public Action<string> UpdateTbPb = null;

    public bool usePb = false;
    public Action<double> InsertPb= null;

    public Action DoneOnePercent;
    // return false if no to indexed. otherwise true
    public Func<string, bool> FilterFoundedFiles;
    public Func<string, bool> FilterFoundedFolders;
}