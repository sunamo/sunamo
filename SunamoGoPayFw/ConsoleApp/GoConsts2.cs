using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GoConsts2
{
    public const string returnUri = "https://apps.sunamo.cz/GoPayReturn.aspx";
    public const string notifyUri = "https://apps.sunamo.cz/GoPayNotification.aspx";

    public static string clientID = null;
    public static string ClientSecret = null;
    public static long goID = long.MaxValue;
    public static string apiUri = null;

    public static void Testing()
    {
        clientID = "1607564662";
        ClientSecret = "C4wQrtHp";
        goID = 8700421323;
        apiUri = "https://gw.sandbox.gopay.com/api";
    }
}