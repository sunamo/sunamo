using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class VpsHelperSunamo
{
    public const string path = @"c:\_";

    public static bool IsVps
    {
        get
        {
            return FS.ExistsDirectory(path);
        }
    }

    public static string SunamoSln()
    {
        if (IsVps)
        {
            return @"c:\_\sunamo\";
        }
        else
        {
            return @"d:\Documents\Visual Studio 2017\Projects\sunamo\";
        }
    }

    public static string SunamoProject()
    {
        return FS.Combine(SunamoSln(), "sunamo");
    }
}