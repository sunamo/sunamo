using sunamo.Data;
using sunamo.Essential;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public partial class TF
{
    
    


    

  
    

    

    public static void Replace(string pathCsproj, string to, string from)
    {
        string content = TF.ReadFile(pathCsproj);
        content = content.Replace(to, from);
        TF.SaveFile(content, pathCsproj);
    }

    public static void PureFileOperation(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    {
        var content = TF.ReadFile(f);
        content = transformHtmlToMetro4.Invoke(content);
        TF.SaveFile(content, FS.InsertBetweenFileNameAndExtension( f, insertBetweenFilenameAndExtension));
    }

    public static void PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = TF.ReadFile(f).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);

        if (f == @"e:\Documents\vs\Projects\sunamo.cz\AppsX\PhotoCs.cs")
        {

        }

        if (String.Compare( content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));
            TF.SaveFile(content2, f);
        }
    }

    

 


    /// <summary>
    /// StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
    public static StreamReader TextReader(string file)
    {
        return  File.OpenText(file);
    }

    public static void WriteAllText(string file, StringBuilder sb)
    {
        WriteAllText(file, sb.ToString());
    }

    public static void WriteAllText(string file, string content, Encoding encoding)
    {
        WriteAllText<string, string>(file, content, encoding, null);
    }

    #region For easy copy
    public static List<byte> bomUtf8 = CA.ToList<byte>(239, 187, 191);

    public static void RemoveDoubleBomUtf8(string path)
    {
        var b = TF.ReadAllBytes(path);
        var to = b.Count > 5 ? 6 : b.Count;

        var isUtf8TwoTimes = true;

        for (int i = 3; i < to; i++)
        {
            if (bomUtf8[i - 3] != b[i])
            {
                isUtf8TwoTimes = false;
                break;
            }
        }

        b = b.Skip(3).ToList();
        TF.WriteAllBytes(path, b);
    }
    #endregion

    public static string ReadAllText(string path, Encoding enc)
    {
        if (isUsed != null)
        {
            if (isUsed.Invoke(path))
            {
                return string.Empty;
            }
        }

        if (enc == null)
        {
            return File.ReadAllText(path);
        }
        else
        {
            return File.ReadAllText(path, enc);
        }
    }
}