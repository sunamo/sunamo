
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using TextCopy;
//using System.Windows;

/// <summary>
/// Must be in win due to ClipboardMonitor which uses W32 methods
/// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
/// Use in ClipboardAsync and ClipboardHelperWin only System.Windows.Forms, not System.Windows which have very similar interface.
/// </summary>
public class ClipboardHelperCore : IClipboardHelper
{
    public const uint CF_UNICODETEXT = 13U;
    public const uint CF_DSPTEXT = 0x0081;
    public const uint CF_LOCALE = 16U;
    public const uint CF_OEMTEXT = 7U;
    public const uint CF_TEXT = 1U;

    IClipboardMonitor clipboardMonitor = null;
    public static ClipboardHelperCore Instance = new ClipboardHelperCore();

    private ClipboardHelperCore()
    {
        clipboardMonitor = ClipboardMonitorCore.Instance;
    }

    #region Get,Set
    /// <summary>
    /// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
    /// not working if was pasted into visual studio (but code yes), created SetText2
    /// </summary>
    /// <param name="v"></param>
    public void SetText(string v)
    {
        Clipboard.SetText(v);
    }

    /// <summary>
    /// Use here only managed method! I could avoid reinstall Windows (RepairJpn). Use only managed also for working with formats.
    /// </summary>
    /// <returns></returns>
    public string GetText()
    {
        return TextCopy.Clipboard.GetText();
    }

    public void SetText2(string s)
    {
        NotImplemented();
    }

    #endregion

    public List<string> GetLines()
    {
        return GetLinesList(GetText());
    }

    private List<string> GetLinesList(string p)
    {
        return p.Split( Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public void SetList(List<string> d)
    {
        SetLines(d);
    }

    public void SetLines(IEnumerable<string> lines)
    {
        string s = JoinNL(lines);
        SetText(s);
    }

    private string JoinNL(IEnumerable<string> lines)
    {
        return string.Join(Environment.NewLine, lines);
    }

    public void CutFiles(params string[] selected)
    {
        NotImplemented();
    }

    private void NotImplemented()
    {
        throw new Exception("Text coppy allow working only with text");
    }

    //public void SetText(TextBuilder stringBuilder)
    //{
    //    SetText(stringBuilder.ToString());
    //}

    public void SetText(StringBuilder stringBuilder)
    {
        SetText(stringBuilder.ToString());
    }

    public void SetText3(string s)
    {
    }

    public bool ContainsText()
    {
        return true;
    }

    public void GetFirstWordOfList()
    {
        NotImplemented();
    }

    public void SetLines(IEnumerable lines)
    {
        SetLines(lines.Cast<string>());
    }
}
