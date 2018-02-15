using sunamo.Clipboard;
using System;
using System.Collections.Generic;
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
}
