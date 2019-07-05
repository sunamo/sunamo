public static class SpecialFolders
{
    public static string MyDocuments(string path)
    {
        return @"D:\Documents\" + path.TrimStart(AllChars.bs);
    }
}
