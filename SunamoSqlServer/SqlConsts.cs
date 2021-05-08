using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlConsts
{
    public const int timeout = 950;
    public const string dbo = "dbo";
}

public class SqlMeasureTimeHelper
{
    bool mt = false;
    int waitMs = 0;
    bool forceIsVps = false;
    static string fn = null;

    static Type type = typeof(Type);

    public  void NewSw()
    {
        if (!MSStoredProceduresI.measureTime)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "MSStoredProceduresI.measureTime must be true to run NewSw");
        }

        if (VpsHelperSunamo.IsVps || MSStoredProceduresI.forceIsVps)
        {
            var bufferInKb = 65536 / 8; //8192
            string pathSqlLog = PathSqlLog();
            StreamWriter sw = null;
            while (true)
            {
                try
                {
                    sw = new StreamWriter(pathSqlLog, true, Encoding.Default, bufferInKb);

                    break;
                }
                catch (Exception ex)
                {
                    pathSqlLog = PathSqlLog();
                }
            }
            SqlMeasureTimeWorker.swSqlLog = sw;
            SqlMeasureTimeWorker.swSqlLog.AutoFlush = true;
            SqlMeasureTimeWorker.writtenLines = 0;
        }
    }

    private  string PathSqlLog()
    {
        var dt = DateTime.Now;
        string r  = FS.GetFileSeries(AppData.ci.GetFolder(AppFolders.Logs), fn + $"{dt.Year}-{dt.Month}-{dt.Day}", ".txt");
        return r;
    }

    public void Before(bool measureTime, int waitMs, bool forceIsVps, string fn2 = "Sql.txt")
    {
         mt = MSStoredProceduresI.measureTime;
         waitMs = MSStoredProceduresI.waitMs;
         forceIsVps = MSStoredProceduresI.forceIsVps;

        fn = fn2;

        MSStoredProceduresI.measureTime = true;
        MSStoredProceduresI.waitMs = 0; //StopwatchStaticSql.maxMs + 100;
        MSStoredProceduresI.forceIsVps = true;

        NewSw();
    }

    public void After()
    {
        MSStoredProceduresI.waitMs = waitMs;
        MSStoredProceduresI.measureTime = mt;
        MSStoredProceduresI.forceIsVps = forceIsVps;
    }
}