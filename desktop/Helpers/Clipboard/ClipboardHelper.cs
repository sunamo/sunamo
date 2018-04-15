using sunamo.Clipboard;
using sunamo.Generators.Text;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

public class ClipboardHelper
    {
    #region Get,Set
    public static void SetText(string v)
    {
        ClipboardMonitor.monitor = null;
        ClipboardMonitor.afterSet = true;
        W32.OpenClipboard(IntPtr.Zero);
        var ptr = Marshal.StringToHGlobalUni(v);
        W32.SetClipboardData(13, ptr);
        W32.CloseClipboard();
        Marshal.FreeHGlobal(ptr);
    }

    public static string GetText()
    {
        return Clipboard.GetText();
    }

    public static List<string> GetLines()
    {
        return SH.GetLines( Clipboard.GetText());
    }
    #endregion

    public static void GetFirstWordOfList()
    {
        Console.WriteLine("Copy text to clipboard.");
        Console.ReadLine();

        StringBuilder sb = new StringBuilder();

        var text = ClipboardHelper.GetLines();
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

        ClipboardHelper.SetText(sb.ToString());
    }

    public static void SetList(List<string> d)
    {
        string s = SH.JoinNL(d);
        Clipboard.SetText(s);
    }

    public static void CutFiles(params string[] selected)
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

    public static void SetText(TextBuilder stringBuilder)
    {
        Clipboard.SetText(stringBuilder.ToString());
    }

    public static void SetText(StringBuilder stringBuilder)
    {
        Clipboard.SetText(stringBuilder.ToString());
    }
}
