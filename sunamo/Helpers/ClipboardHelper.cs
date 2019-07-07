using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using sunamo;
using sunamo.Generators.Text;

/// <summary>
/// Cant add another methods with Task and normal - methods have same signature, despite return were different
/// </summary>
public class ClipboardHelper 
{
    public  static IClipboardHelper Instance = null;
    public  static IClipboardHelperApps InstanceApps = null;

    private ClipboardHelper() { }

    public  static bool ContainsText()
    {
        if (Instance == null)
        {
            return InstanceApps.ContainsText().Result;
        }
        else
        {
            return Instance.ContainsText();
        }
    }

    public  static string GetText()
    {
        if (Instance == null)
        {
            return  InstanceApps.GetText().Result;
        }
        else
        {
            return Instance.GetText();
        }
    }

    public  static List<string> GetLinesAllWhitespaces()
    {

        var t =  GetText();


        return SH.SplitByWhiteSpaces(t);
    }

    public  static List<string> GetLines()
    {
#if !UNITTEST
        if (Instance == null)
        {
            return InstanceApps.GetLines().Result;
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

    public static void SetText(TextBuilder stringBuilder)
    {
        if (Instance == null)
        {
            InstanceApps.SetText(stringBuilder);
        }
        else
        {
            Instance.SetText(stringBuilder);
        }
    }

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