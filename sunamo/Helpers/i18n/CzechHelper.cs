public class CzechHelper
{
    public static string Dear(bool sex)
    {
        if (sex)
        {
            return "Mil\u00E1";
        }
        return "Mil\u00FD";
    }

    public static string Honorable(bool sex, string name)
    {
        if (sex)
        {
            return sess.i18n(XlfKeys.Madam)+" " + name;
        }
        else
        {
            return sess.i18n(XlfKeys.Sir)+" " + name;
        }
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