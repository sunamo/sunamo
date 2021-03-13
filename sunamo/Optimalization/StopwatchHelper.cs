using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using sunamo.Essential;

public class StopwatchHelper
{
    public  Stopwatch sw = new Stopwatch();

    public static string CalculateAverageOfTakes(string li)
    {
        var l = SH.GetLines(li);

        Dictionary<string, List<int>> d = new Dictionary<string, List<int>>();

        foreach (var item in l)
        {
            if (item.Contains(takes))
            {
                var d2 = SH.Split(item, takes);
                var tp = d2[1].Replace("ms", string.Empty);
                
                DictionaryHelper.AddOrCreate<string, int>(d, d2[0], int.Parse(tp));
            }
        }

        StringBuilder sb = new StringBuilder();
        foreach (var item in d)
        {
            sb.AppendLine(item.Key + " " + NH.Average<int>(item.Value) + "ms");
        }

        return sb.ToString();
    }

    public  void Start()
    {
        sw.Reset();
        sw.Start();
    }

    public const string takes = " takes ";

    /// <summary>
    /// Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="p"></param>
    /// <param name="parametry"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation, string p, params object[] parametry)
    {
        sw.Stop();
        string message = string.Format(operation + takes + sw.ElapsedMilliseconds + "ms" + p, parametry);
        ThisApp.SetStatus(TypeOfMessage.Information, message);;
#if DEBUG
        DebugLogger.Instance.WriteLine(message);
#endif 
        return sw.ElapsedMilliseconds;
    }

    public  long ElapsedMS
    {
        get
        {
            return sw.ElapsedMilliseconds;
        }
    }

    /// <summary>
    /// Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation)
    {
        return StopAndPrintElapsed(operation, string.Empty);
    }

    public string Stop()
    {
        sw.Stop();
        return sw.ElapsedMilliseconds + "ms";
    }
}