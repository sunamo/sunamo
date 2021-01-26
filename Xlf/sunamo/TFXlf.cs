using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TFXlf
{
    #region For easy copy
    public static List<byte> bomUtf8 = CAXlf.ToList<byte>(239, 187, 191);

    public static void RemoveDoubleBomUtf8(string path)
    {
        var b = File.ReadAllBytes(path).ToList();
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
        TFXlf.WriteAllBytes(path, b);
    }


    #endregion

    #region Only in *Xlf.cs
    public static void WriteAllBytes(string file, List<byte> b)
    {
        File.WriteAllBytes(file, b.ToArray());
    }
    #endregion
}