using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParserEventLog
{
    /// <summary>
    /// Return string, not whole EventRecord due to must work in using
    /// </summary>
    /// <param name="path"></param>
    
    public static List<string> ParseDescription(string path)
    {
        CollectionWithoutDuplicates<string> result = new CollectionWithoutDuplicates<string>();
        using (var reader = new EventLogReader(path, PathType.FilePath))
        {
            EventRecord record;
            while ((record = reader.ReadEvent()) != null)
            {
                using (record)
                {
                    //Console.WriteLine("{0} {1}: {2}", record.TimeCreated, record.LevelDisplayName, record.FormatDescription());
                    result.Add(record.FormatDescription());
                }
            }
        }

        return result.c;
    }
}