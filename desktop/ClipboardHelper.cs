using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class ClipboardHelper
    {
        public static void SetText(string v)
    {
        W32.OpenClipboard(IntPtr.Zero);
        var ptr = Marshal.StringToHGlobalUni(v);
        W32.SetClipboardData(13, ptr);
        W32.CloseClipboard();
        Marshal.FreeHGlobal(ptr);
    }
    }
