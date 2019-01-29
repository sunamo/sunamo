using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class TF
{
    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE. 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadFile(string s)
    {
        FS.MakeUncLongPath(ref s);
        if (FS.ExistsFile(s))
        {
            return File.ReadAllText(s, Encoding.UTF8);
        }
        else
        {
            File.WriteAllText(s, "", Encoding.UTF8);
        }

        return "";
    }
}
