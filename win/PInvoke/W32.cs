using System.Runtime.InteropServices;
using System;
using sunamo.PInvoke;

/// <summary>
/// 
/// SetLastError = true should be specified always, then I can get error value from Marshal.GetLastWin32Error. 
/// https://stackoverflow.com/a/17918729/9327173
/// </summary>
public class W32
{
    #region ClipboardMonitor
    /// <summary>
    /// Places the given window in the system-maintained clipboard format listener list.
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AddClipboardFormatListener(IntPtr hwnd);

    /// <summary>
    /// Removes the given window from the system-maintained clipboard format listener list.
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

    /// <summary>
    /// Sent when the contents of the clipboard have changed.
    /// </summary>
    public const int WM_CLIPBOARDUPDATE = 0x031D;

    /// <summary>
    /// To find message-only windows, specify HWND_MESSAGE in the hwndParent parameter of the FindWindowEx function.
    /// </summary>
    public static IntPtr HWND_MESSAGE = new IntPtr(-3);

    #endregion

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, IntPtr lpSECURITY_ATTRIBUTES, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool CloseHandle(IntPtr hObject);


    public static uint GetFileInformationByHandleWorker(string file, out int lastError )
    {
        uint nNumberOfLinks = uint.MaxValue;
        lastError = 0;

        BY_HANDLE_FILE_INFORMATION hfi = new BY_HANDLE_FILE_INFORMATION { };

        IntPtr handle = CreateFile(file, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
        if (handle.ToInt32() != INVALID_HANDLE_VALUE)
        {
            if (GetFileInformationByHandle(handle, ref hfi))
                nNumberOfLinks = hfi.nNumberOfLinks;
            else
                lastError = Marshal.GetLastWin32Error();

            CloseHandle(handle);
        }
        else
            lastError = Marshal.GetLastWin32Error();

        return nNumberOfLinks;
    }

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool GetFileInformationByHandle(IntPtr handle, ref BY_HANDLE_FILE_INFORMATION hfi);

    public const int INVALID_HANDLE_VALUE = -1;

    public const uint GENERIC_READ = 0x80000000;
    public const int ERROR_INSUFFICIENT_BUFFER = 122;

    public const int FILE_SHARE_READ = 1;
    public const int FILE_SHARE_WRITE = 2;
    public const int FILE_SHARE_DELETE = 4;
    public const int FILE_ATTRIBUTE_NORMAL = 0x80;

    public const int CREATE_NEW = 1;
    public const int CREATE_ALWAYS = 2;
    public const int OPEN_EXISTING = 3;
    public const int OPEN_ALWAYS = 4;
    public const int TRUNCATE_EXISTING = 5;

    [StructLayout(LayoutKind.Sequential)]
    public struct BY_HANDLE_FILE_INFORMATION
    {
        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint dwVolumeSerialNumber;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint nNumberOfLinks;
        public uint nFileIndexHigh;
        public uint nFileIndexLow;
    };

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GlobalFree(IntPtr hMem);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint EnumClipboardFormats(uint format);

    /// <summary>
    /// Use Marshal.GetLastWin32Error instead. https://stackoverflow.com/a/17918729/9327173
    /// </summary>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern uint GetLastError();

    [DllImport("Kernel32.Dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
    public static extern bool EnableWow64FSRedirection(bool enable);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool IsClipboardFormatAvailable(uint format);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool CloseClipboard();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GlobalUnlock(IntPtr hMem);

    /// <summary>
    /// Retrieves the current size of the specified global memory object, in bytes.
    /// </summary>
    /// <param name="hMem"></param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern UIntPtr GlobalSize(IntPtr hMem);


    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError =true)]
    public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleIcon(IntPtr hIcon);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern short GetKeyState(int keyCode);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetClipboardData(uint uFormat, IntPtr data);

    [DllImport(@"urlmon.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public extern static System.UInt32 FindMimeFromData(
        System.UInt32 pBC,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        System.UInt32 cbSize,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
        System.UInt32 dwMimeFlags,
        out System.UInt32 ppwzMimeOut,
        System.UInt32 dwReserverd
    );

    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DestroyIcon(IntPtr hIcon);
}
