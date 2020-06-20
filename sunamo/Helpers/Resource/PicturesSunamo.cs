using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PicturesSunamo
{
    public static List<string> GetPicturesFiles(string path)
    {
        var masc = SH.Join(AllStrings.semi, AllLists.BasicImageExtensions);
        return FS.GetFiles(path, masc, SearchOption.TopDirectoryOnly);
    }
}