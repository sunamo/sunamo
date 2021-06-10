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

    public void Reset()
    {
        sw.Reset();
    }

    public  void Start()
    {
        sw.Reset();
        sw.Start();
    }

    public const string takes = " takes ";

    public string lastMessage = null;

    /// <summary>
    /// Write ElapsedMilliseconds to debug, TSL. For more return long
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="p"></param>
    /// <param name="parametry"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation, string p, params object[] parametry)
    {
        var l = sw.ElapsedMilliseconds;
        sw.Reset();
        lastMessage = string.Format(operation + takes + l + "ms" + p, parametry);
        ThisApp.SetStatus(TypeOfMessage.Information, lastMessage);
#if DEBUG
        DebugLogger.Instance.WriteLine(lastMessage);
#endif 
        return l;
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
        var r = sw.ElapsedMilliseconds + "ms";
        sw.Reset();
        return r;
    }
}