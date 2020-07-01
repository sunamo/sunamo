using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

public class VpsHelperSunamo
{
    public const string path = @"c:\_";
    public const string ip = "46.36.40.198";
    public const string ipMyPoda = "85.135.38.18";

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

    public static string SunamoCzSln()
    {
        if (IsVps)
        {
            return @"c:\_\sunamo.cz\";
        }
        else
        {
            return @"d:\Documents\Visual Studio 2017\Projects\sunamo.cz\";
        }
    }

    public static string SunamoProject()
    {
        return FS.Combine(SunamoSln(), "sunamo");
    }

    
}