public class GuidHelper
{
    public static string RemoveDashes(string e)
    {
        return e.Replace(AllStrings.dash, "");
    }

    public static string AddDashes(string e)
    {
        if (e.Contains(AllStrings.dash))
        {
            return e;
        }
        e = e.Insert(8, AllStrings.dash);
        e = e.Insert(13, AllStrings.dash);
        e = e.Insert(18, AllStrings.dash);
        e = e.Insert(23, AllStrings.dash);
        return e;
    }
}