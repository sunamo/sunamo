using System.IO;
using System.Web.Hosting;

public class FileBasedTable
{
    public static byte[] GetSbf(string table, string column, int id)
    {
        string file = HostingEnvironment.ApplicationPhysicalPath + "_\\sbf\\" + table + "\\" + column + "\\" + id.ToString() + ".sbf";
        return File.ReadAllBytes(file);
    }

    public static void SetSbf(string table, string column, int id, byte[] value)
    {
        string file = HostingEnvironment.ApplicationPhysicalPath + "_\\sbf\\" + table + "\\" + column + "\\" + id.ToString() + ".sbf";
        File.WriteAllBytes(file, value);
    }

    public static void CreatePathsIfNotExistsSbf(string table, params string[] columns)
    {
        string tablePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "_", "sbf", table);
        if (!Directory.Exists(tablePath))
        {
            Directory.CreateDirectory(tablePath);
        }
        foreach (string item in columns)
        {
            string columnPath = Path.Combine(tablePath, item);
            if (!Directory.Exists(columnPath))
            {
                Directory.CreateDirectory(columnPath);
            }
        }
    }

    public static void DeleteSbf(string table, string column, int id)
    {
        string file = HostingEnvironment.ApplicationPhysicalPath + "_\\sbf\\" + table + "\\" + column + "\\" + id.ToString() + ".stf";
        File.Delete(file);
    }

    
}
