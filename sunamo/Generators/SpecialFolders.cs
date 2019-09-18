public static class SpecialFolders
{
    public static string MyDocuments(string path)
    {
        return @"d:\Documents\" + path.TrimStart(AllChars.bs);
    }
}
