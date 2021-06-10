using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileTextLogger
{
    public StreamWriter sw = null;


    /// <summary>
    /// Buffer 1MB
    /// </summary>
    /// <param name="fn"></param>
    public FileTextLogger(string fn, int bufferInMb)
    {
        //FileStream fs = new FileStream(fn, FileMode.OpenOrCreate);

        // 1024 * 1024 *
        // cant use, could terminate itseltř
        //PH.ShutdownProcessWhichOccupyFileHandleExe(fn);
        sw = File.CreateText(fn);//, Encoding.UTF8,  1024 * bufferInMb);
        sw.AutoFlush = true;
        WriteNewLine(DateTime.Now.ToLongTimeString());
    }

    public void WriteNewLine(string l)
    {
        sw.WriteLine(l);
    }
}
