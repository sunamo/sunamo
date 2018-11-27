
using sunamo.Generators.Text;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using win;
// if app isnt STA, raise exception
//using System.Windows;
// if app isnt STA, return empty
//using System.Windows.Forms;

/// <summary>
/// Use in ClipboardAsync and ClipboardHelperWin only System.Windows.Forms, not System.Windows which have very similar interface.
/// </summary>
public class ClipboardHelperWin : IClipboardHelper
{
    IClipboardMonitor clipboardMonitor = null;
    public static ClipboardHelperWin Instance = new ClipboardHelperWin();

    private ClipboardHelperWin()
    {

    }

    #region Get,Set
    public void SetText(string v)
    {
        if (clipboardMonitor != null)
        {
            clipboardMonitor.monitor = null;
            clipboardMonitor.afterSet = true;
        }

        W32.OpenClipboard(IntPtr.Zero);
        var ptr = Marshal.StringToHGlobalUni(v);
        W32.SetClipboardData(13, ptr);
        W32.CloseClipboard();
        Marshal.FreeHGlobal(ptr);
    }

    public string GetText()
    {
        ClipboardAsync ca = new ClipboardAsync();
        string s = ca.GetText();
        return s;
        //return Clipboard.GetText();
    }

    

    public List<string> GetLines()
    {
        var text = GetText();
        return SH.GetLines(text);
    }
    #endregion

    public void GetFirstWordOfList()
    {
        Console.WriteLine("Copy text to clipboard.");
        Console.ReadLine();

        StringBuilder sb = new StringBuilder();

        var text = GetLines();
        foreach (var item in text)
        {
            string t = item.Trim();

            if (t.EndsWith(":"))
            {
                sb.AppendLine(item);
            }
            else if (t == "")
            {
                sb.AppendLine(t);
            }
            else
            {
                sb.AppendLine(SH.GetFirstWord(t));
            }
        }

        SetText(sb.ToString());
    }

    public void SetList(List<string> d)
    {
        SetLines(d);
    }

    public void SetLines(List<string> lines)
    {
        string s = SH.JoinNL(lines);
        SetText(s);
    }

    public void CutFiles(params string[] selected)
    {
        byte[] moveEffect = { 2, 0, 0, 0 };
        MemoryStream dropEffect = new MemoryStream();
        dropEffect.Write(moveEffect, 0, moveEffect.Length);

        StringCollection filestToCut = new StringCollection();
        filestToCut.AddRange(selected);

        DataObject data = new DataObject("Preferred DropEffect", dropEffect);
        data.SetFileDropList(filestToCut);

        Clipboard.Clear();
        Clipboard.SetDataObject(data, true);
    }

    public void SetText(TextBuilder stringBuilder)
    {
        SetText(stringBuilder.ToString());
    }

    public void SetText(StringBuilder stringBuilder)
    {
        SetText(stringBuilder.ToString());
    }


}
