using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Exc
{
    public static bool aspnet
    {
        get => SunamoExceptions.Exc.aspnet;
        set => SunamoExceptions.Exc.aspnet = value;
    }

    #region For easy copy in SunamoException project
    static bool first = true;
    static StringBuilder sb = new StringBuilder();

    /// <summary>
    /// Remove GetStackTrace (first line
    /// </summary>
    /// <returns></returns>
    public static string GetStackTrace(bool stopAtFirstSystem = false)
    {
        if (stopAtFirstSystem) 
        {
            if (first)
            {
                first = false;

                ClipboardHelper.SetText(AllStrings.dash);
            }
        }

        StackTrace st = new StackTrace();

        var v = st.ToString();
        var l = GetLines(v);
        Trim(l);
        l.RemoveAt(0);

        int i = 0;

        for (; i < l.Count; i++)
        {
            if (l[i].StartsWith(CsConsts.atSystemDot))
            {
                l = l.Take(i).ToList();
                l.Add(string.Empty);
                l.Add(string.Empty);

                break;
            }

            
        }

        return JoinNL(l);
    }

    public static bool _trimTestOnEnd = true;

    /// <summary>
    /// Print name of calling method, not GetCurrentMethod
    /// If is on end Test, will trim
    /// </summary>
    public static string CallingMethod(int v = 1)
    {
        StackTrace stackTrace = new StackTrace();
        MethodBase methodBase = stackTrace.GetFrame(v).GetMethod();
        var methodName = methodBase.Name;
        if (_trimTestOnEnd)
        {
            methodName = TrimEnd(methodName, XlfKeys.Test);
        }
        return methodName;
    }

    #region MyRegion


    public static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext))
        {
            return name.Substring(0, name.Length - ext.Length);
        }
        return name;
    }

    private static object lockObject = new object();

    private static string JoinNL(List<string> l)
    {
        sb.Clear();
        foreach (var item in l)
        {
            sb.AppendLine(item);
        }
        var r = string.Empty;
        lock (lockObject)
        {
            ;
            r = sb.ToString();
        }
        return r;
    }

    public static List<string> Trim(List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = l[i].Trim();
        }
        return l;
    }

    public static string MethodOfOccuredFromStackTrace(string exc)
    {
        var st = SH.FirstLine(exc);
        var dx = st.IndexOf(" in ");
        if (dx != -1)
        {
            st = SH.SubstringIfAvailable(st, dx);
        }
        return st;
        //
    }

    private static List<string> GetLines(string v)
    {
        var l = v.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        return l;
    }
    #endregion 
    #endregion
}