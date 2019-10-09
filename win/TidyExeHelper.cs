using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Management.Automation;
using System.Text;
using win.Helpers.Powershell;

public class TidyExeHelper
{
    public static string WriteTidyConfigToExecutableLocation()
    {
        var tidy_config = FS.GetFileInStartupPath("tidy_config.txt");

        if (!FS.ExistsFile(tidy_config))
        {
            EmbeddedResourcesH eh = new EmbeddedResourcesH(typeof(TidyExeHelper).Assembly, "win");

            var stream = eh.GetStream("Resources/tidy_config.txt");
            var content = BTS.StringFromStream(stream);

            TF.WriteAllText(tidy_config, content);
        }

        return tidy_config;
    }

    public static Tuple<FileInfo, string> GenerateMapInfo(string mapDirectory, string fileExtension)
    {
        var uniqueMapName = Guid.NewGuid().ToString();
        var fileName = Path.Combine(mapDirectory, Path.ChangeExtension(uniqueMapName, fileExtension));
        return Tuple.Create(new FileInfo(fileName), uniqueMapName);
    }

    public static void WriteToFile(Tuple<FileInfo, string> mapInfo, string input)
    {
        var max = int.MaxValue / 2;
        byte[] newValue = Encoding.UTF8.GetBytes(input);
        long capacity = newValue.Length + max;

        using (var mmf = MemoryMappedFile.CreateFromFile(mapInfo.Item1.FullName, FileMode.Create, mapInfo.Item2, capacity))
            WriteToFile(input, max, newValue, mmf);
    }

    private static void WriteToFile(string value, int max, byte[] newValue, MemoryMappedFile mmf)
    {
        using (var accesor = mmf.CreateViewAccessor())
        {
            byte[] newValueLength = BitConverter.GetBytes(value.Length);
            accesor.WriteArray(0, newValueLength, 0, newValueLength.Length);
            accesor.WriteArray(max, newValue, 0, newValue.Length);
        }
    }

    public static string FormatHtml(string input, string tidy_config)
    {
        var mapInfo = GenerateMapInfo(Path.GetTempPath(), ".txt");
        //WriteToFile(mapInfo, "abc");

        var random = RandomHelper.RandomString(5, false, false, true, false); 
        var temp = FS.Combine(System.IO.Path.GetTempPath(), random);
        MemoryMappedFile m = null;
        int max, capacity;
        max = capacity = 1024 * 1024 * 2;

        string mapName = mapInfo.Item2;

        //var max = int.MaxValue / 2;
        byte[] newValue = Encoding.UTF8.GetBytes(input);
         capacity = newValue.Length + max;

        //mapName  cant be identical like path
        //m = MemoryMappedFile.CreateFromFile(new FileStream( temp, FileMode.OpenOrCreate), temp, , MemoryMappedFileAccess.ReadWrite, null, HandleInheritability.Inheritable, true); 
        //m =  MemoryMappedFile.CreateFromFile(temp, FileMode.OpenOrCreate, mapName , capacity, MemoryMappedFileAccess.ReadWrite);
        //m = MemoryMappedFile.CreateNew(mapName, capacity);
        m = MemoryMappedFile.CreateFromFile(mapInfo.Item1.FullName, FileMode.Create, mapInfo.Item2, capacity);

        WriteToFile(input, max, newValue, m);

        PowershellRunner ps = new PowershellRunner();
        var comment = "tidy -config " + SH.WrapWithQm(tidy_config) + " -output " + SH.WrapWithQm(mapName) + " " + SH.WrapWithQm(mapName);
        //comment = "tidy -config \"d:\\pa\\tidy\\tidy_config.txt\" -output \"d:\\pa\\tidy\\1_Out.html\" \"d:\\pa\\tidy\\1.html\"";
        var result = PowershellRunner.Invoke(CA.ToListString(comment));

        if (result[0].Count > 0)
        {

        }

        

        string output = null;

        using (var accesor = m.CreateViewStream())
        {
            output = BTS.StringFromStream(accesor);
        }

        m.Dispose();

        return output;
    }
}

