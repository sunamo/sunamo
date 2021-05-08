using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlMeasureTimeWorker
{
    public static event Action NeedNewFile;
    public static int writtenLines = 0;
    public static StreamWriter swSqlLog;

    public static void IncrementWrittenLines()
    {
        writtenLines++;
        if (writtenLines > 5000)
        {
            if (MSStoredProceduresI.measureTime)
            {
                NeedNewFile();
            }
        }
    }

    public static SqlMeasureTimeHelper mt = new SqlMeasureTimeHelper();

    public static void Init()
    {
        #region Must be after set up ThisApp.Name
        if (MSStoredProceduresI.measureTime)
        {
            mt.NewSw();
            NeedNewFile += ThisApp_NeedNewFile;
        }
        #endregion
    }

    private static void ThisApp_NeedNewFile()
    {
        mt.NewSw();
    }
}