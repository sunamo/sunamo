using System.IO;
using System.Web.Hosting;

public class FileBasedTable
{
    public static byte[] GetSbf(string table, string column, int id)
    {
        string file = HostingEnvironment.ApplicationPhysicalPath + "_\\sbf\\" + table + AllStrings.bs + column + AllStrings.bs + id.ToString() + ".sbf";
        if (!FS.ExistsFile(file))
        {
            return new byte[0];
        }
        return File.ReadAllBytes(file);
    }

    public static void SetSbf(string table, string column, int id, byte[] value)
    {
        string file = HostingEnvironment.ApplicationPhysicalPath + "_\\sbf\\" + table + AllStrings.bs + column + AllStrings.bs + id.ToString() + ".sbf";
        File.WriteAllBytes(file, value);
    }

    public static void CreatePathsIfNotExistsSbf(string table, params string[] columns)
    {
        string tablePath = FS.Combine(HostingEnvironment.ApplicationPhysicalPath, AllStrings.us, "sbf", table);
        if (!FS.ExistsDirectory(tablePath))
        {
            Directory.CreateDirectory(tablePath);
        }
        foreach (string item in columns)
        {
            string columnPath = FS.Combine(tablePath, item);
            if (!FS.ExistsDirectory(columnPath))
            {
                Directory.CreateDirectory(columnPath);
            }
        }
    }

    public static void DeleteSbf(string table, string column, int id)
    {
        string file = HostingEnvironment.ApplicationPhysicalPath + "_\\sbf\\" + table + AllStrings.bs + column + AllStrings.bs + id.ToString() + ".stf";
        File.Delete(file);
    }

    
}
