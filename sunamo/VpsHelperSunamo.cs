using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class VpsHelperSunamo
{
    
    public const string ip = "46.36.40.198";
    public const string ipMyPoda = "85.135.38.18";

    public static string LocationOfSqlBackup(string s)
    {
        var p = @"c:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\"+s+".bak";
        return p;
    }

    public static string SunamoSln()
    {
        if (IsVps)
        {
            return @"c:\_\sunamo\";
        }
        else
        {
            return @"e:\Documents\vs\Projects\sunamo\";
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
            return @"e:\Documents\vs\Projects\sunamo.cz\";
        }
    }

    public static string SunamoProject()
    {
        return FS.Combine(SunamoSln(), "sunamo");
    }

    
}