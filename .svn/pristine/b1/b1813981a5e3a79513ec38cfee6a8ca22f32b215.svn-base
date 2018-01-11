
using System.Web;

public class SunamoRoutePage 
{
    public const string Up = "../";

    // TODO: RIGHTUP
    public static string GetRightUp(int CountUp)
    {
        return Up;
        
    }

    public static string GetRightUp(HttpRequest hr)
    {
        int CountUp = SH.OccurencesOfStringIn(hr.FilePath, "/");
        if (CountUp == 0 )
        {
            return "";
        }
        // Zatímco se StringBuilder bych musel vykonat 4 operace tak se string jen 2 při spojování 2 řetězců, i když možná 3 pokud bych počítal i smazání prvního Up
        string vr = Up;
        CountUp--;
        if (CountUp > 1)
        {
            for (int i = 1; i < CountUp; i++)
            {
                vr += Up;
            }
        }
        return vr;
    }

    public static string GetRightUpRoot(HttpRequest hr)
    {
        return "/";
        //return vr;
    }
}
