using System;
using System.Runtime.InteropServices;

public class ClipboardHelper
    {
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
    }
