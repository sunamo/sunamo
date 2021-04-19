using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class VpsHelperSunamo
{
    #region For easy copy
    public const string path = @"c:\_";

    public static bool IsVps
    {
        get
        {
            var v = Directory.Exists(path);
            return v;
        }
    }
    #endregion
}