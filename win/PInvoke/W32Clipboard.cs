using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class W32
{
    public static Process ProcessHoldingClipboard()
    {
        Process theProc = null;

        IntPtr hwnd = GetOpenClipboardWindow();

        if (hwnd != IntPtr.Zero)
        {
            uint processId;
            uint threadId = GetWindowThreadProcessId(hwnd, out processId);

            Process[] procs = Process.GetProcesses();
            foreach (Process proc in procs)
            {
                IntPtr handle = proc.MainWindowHandle;

                if (handle == hwnd)
                {
                    theProc = proc;
                }
                else if (processId == proc.Id)
                {
                    theProc = proc;
                }
            }
        }

        return theProc;
    }
}
