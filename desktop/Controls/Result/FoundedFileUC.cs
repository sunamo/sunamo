using desktop.Controls.Result;
using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

public class FoundedFileUC : FoundedResultUC
{
    public FoundedFileUC(string filePath, TUList<string, Brush> p, int serie) : base(filePath, p, serie)
    {

    }

    public string fileFullPath
    {
        get { return base.fileFullPath; }
        set { base.fileFullPath = value; }
    }

    public string file
    {
        get { return base.file; }
        set { base.file = value; }
    }

    
}