using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using sunamo;
using win.Helpers.Powershell;
using static W32;

/// <summary>
/// Provides access to NTFS junction points in .Net.
/// </summary>
public static class JunctionPoint
    {

    /// <summary>
    /// /H = Only files
    /// If exists, will rewrite.
    /// /J vytváří vždy adresář, jde pak dle toho poznat i ve FS
    /// /H pracuje adekvátně se soubory
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    
        private static SafeFileHandle OpenReparsePoint(string reparsePoint, EFileAccess accessMode)
        {
            SafeFileHandle reparsePointHandle = new SafeFileHandle(CreateFile(reparsePoint, accessMode,
                EFileShare.Read | EFileShare.Write | EFileShare.Delete,
                IntPtr.Zero, ECreationDisposition.OpenExisting,
                EFileAttributes.BackupSemantics | EFileAttributes.OpenReparsePoint, IntPtr.Zero), true);

            if (Marshal.GetLastWin32Error() != 0)
                ThrowLastWin32Error("Unable to open reparse point" + ".");

            return reparsePointHandle;
        }

        private static void ThrowLastWin32Error(string message)
        {
            throw new IOException(message, Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
        }
    }

