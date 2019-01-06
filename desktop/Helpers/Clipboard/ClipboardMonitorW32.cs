using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace desktop.Helpers.Clipboard
{
    public class ClipboardMonitorW32 : IDisposable
    {
        IntPtr nextClipboardViewer;
        WindowInteropHelper w;
        /// <summary>
		/// Occurs when the clipboard content changes.
		/// </summary>
		public event EventHandler ClipboardContentChanged;

        // Don't exists in mono
        private HwndSource hwndSource = new HwndSource(0, 0, 0, 0, 0, 0, 0, null, W32.HWND_MESSAGE);

        public void Dispose()
        {
            W32.ChangeClipboardChain(this.Handle, nextClipboardViewer);
            W32.RemoveClipboardFormatListener(hwndSource.Handle);
            hwndSource.RemoveHook(WndProc);
            hwndSource.Dispose();
        }

        public ClipboardMonitorW32(Window window)
        {
            w = new WindowInteropHelper(window);
            hwndSource.AddHook(WndProc);
            W32.AddClipboardFormatListener(hwndSource.Handle);
        }

        IntPtr Handle => w.Handle;

        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            Message m = new Message();
            m.HWnd = hwnd;
            m.Msg = msg;
            m.WParam = wParam;
            m.LParam = lParam;

            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                case 797:
                    
                    W32.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    ClipboardContentChanged(null, null);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        W32.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    //base.WndProc(ref m);
                    break;
            }

            return IntPtr.Zero;
        }
    }
}
