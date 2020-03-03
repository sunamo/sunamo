using sunamo;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public partial class TF
{
    

    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        // Read the BOM
        var enc = GetEncoding(file);
        file.Dispose();
        return enc;
    }

    /// <summary>
    /// Dont working, with Air bank export return US-ascii / 1252, file has diacritic
    /// Atom with auto-encoding return ISO-8859-2 which is right
    /// </summary>
    /// <param name="file"></param>
    
    public static List<string> GetAllLines(string file)
    {
        List<string> lines = TF.GetLines<string,string>(file, null);
        List<string> linesPpk = new List<string>();
        for (int i = 0; i < lines.Count; i++)
        {
            string trim = lines[i].Trim();
            if (trim != "")
            {
                linesPpk.Add(trim);
            }
        }
        return linesPpk;
    }
}