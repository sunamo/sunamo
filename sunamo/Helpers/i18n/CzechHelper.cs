using sunamo.Essential;
using System;

public class CzechHelper
{
    static Type type = typeof(CzechHelper);
    

    public static string Dear(bool sex)
    {
        if (sex)
        {
            return "Mil\u00E1";
        }
        return "Mil\u00FD";
    }

    //
    public static string Esteemed(bool sex)
    {
        if (sex)
        {
            return "Vážená";
        }
        return "Vážený";
    }

    public static string Honorable(bool sex, string dear, string name)
    {
        string f = null;
        #region MyRegion
        //if (ThisApp.l == Langs.en)
        //{
        //    f = SunamoPageHelperSunamo.i18n(XlfKeys.Dear);
        //}
        //else if(ThisApp.l == Langs.cs)
        //{
        //    f = 
        //}
        //else
        //{
        //    ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), ThisApp.l);
        //}

        //f += AllStrings.space; 
        #endregion

        if (sex)
        {
            // its auto with dear 
            f= dear +AllStrings.space+ sess.i18n(XlfKeys.madam)+" " + name;
        }
        else
        {
            f= dear + AllStrings.space + sess.i18n(XlfKeys.sir)+" " + name;
        }

        return SH.FirstCharUpper(f);
    }

    public static bool GetSexFromSurname(string name)
    {
        // ová = á
        if (SH.EndsWithArray(name, "ova", "á"))
        {
            return true;
        }
        return false;
    }
}