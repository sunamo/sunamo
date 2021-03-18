public static class SpecialFolders
{
    public static string MyDocuments(string path)
    {
        return @"d:\Documents\" + path.TrimStart(AllChars.bs);
    }

    public static string eMyDocuments(string path)
    {
        return @"e:\Documents\" + path.TrimStart(AllChars.bs);
    }
}