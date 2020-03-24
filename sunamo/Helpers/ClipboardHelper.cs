using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using sunamo;
using sunamo.Generators.Text;

/// <summary>
/// Cant add another methods with void and normal - methods have same signature, despite return were different
/// </summary>
public class ClipboardHelper
{
    public static IClipboardHelper Instance = null;
    public static IClipboardHelperApps InstanceApps = null;

    private ClipboardHelper() { }

    public static bool ContainsText()
    {
        if (Instance == null)
        {
            return InstanceApps.ContainsText();
        }
        else
        {
            return Instance.ContainsText();
        }
    }

    public static string GetText()
    {
        if (Instance == null)
        {
            return InstanceApps.GetText();
        }
        else
        {
            return Instance.GetText();
        }
    }

    public static List<string> GetLinesAllWhitespaces()
    {
        var t = GetText();


        return SH.SplitByWhiteSpaces(t);
    }

    public static List<string> GetLines()
    {
#if !UNITTEST
        if (Instance == null)
        {
            return InstanceApps.GetLines();
        }
        else
        {
            return Instance.GetLines();
        }
#endif
    }



    public static void SetText(string s)
    {
#if !UNITTEST
        if (Instance == null)
        {
            InstanceApps.SetText(s);
        }
        else
        {
            Instance.SetText(s);
        }
#endif
    }

    public static void SetText2(string s)
    {
        if (Instance == null)
        {
            InstanceApps.SetText2(s);
        }
        else
        {
            Instance.SetText2(s);
        }
    }


    public static void GetFirstWordOfList()
    {
        if (Instance == null)
        {
            InstanceApps.GetFirstWordOfList();
        }
        else
        {
            Instance.GetFirstWordOfList();
        }
    }


    public static void SetList(List<string> d)
    {
        if (Instance == null)
        {
            InstanceApps.SetList(d);
        }
        else
        {
            Instance.SetList(d);
        }
    }

    public static void SetLines(IEnumerable lines)
    {
        if (Instance == null)
        {
            InstanceApps.SetLines(lines);
        }
        else
        {
            Instance.SetLines(lines);
        }
    }

    public static void CutFiles(params string[] selected)
    {
        if (Instance == null)
        {
            InstanceApps.CutFiles(selected);
        }
        else
        {
            Instance.CutFiles(selected);
        }
    }

    //public static void SetText(TextBuilder stringBuilder)
    //{
    //    if (Instance == null)
    //    {
    //        InstanceApps.SetText(stringBuilder);
    //    }
    //    else
    //    {
    //        Instance.SetText(stringBuilder);
    //    }
    //}

    public static void SetText3(string s)
    {
        if (Instance == null)
        {
            InstanceApps.SetText3(s);
        }
        else
        {
            Instance.SetText3(s);
        }
    }

    public static void SetText(StringBuilder stringBuilder)
    {
        if (Instance == null)
        {
            InstanceApps.SetText(stringBuilder.ToString());
        }
        else
        {
            Instance.SetText(stringBuilder.ToString());
        }
    }

    public static void SetDictionary<T1, T2>(Dictionary<T1, T2> charEntity, string delimiter)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in charEntity)
        {
            sb.AppendLine(item.Key + delimiter + item.Value);
        }

        ClipboardHelper.SetText(sb.ToString());
    }

    //public static string GetText()
    //{
    //    return Instance.GetText();
    //}

    //public static List<string> GetLines()
    //{
    //    return Instance.GetLines();
    //}

    //public static bool ContainsText()
    //{
    //    return Instance.ContainsText();
    //}
}