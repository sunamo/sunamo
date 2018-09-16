#region Mono
using desktop.Essential;
using desktop.Interfaces;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace sunamo.Clipboard
{
	public sealed class ClipboardMonitor : IDisposable, IClipboardMonitor
    {
        public static ClipboardMonitor Instance = new ClipboardMonitor();

		 static bool _pernamentlyBlock = false;
		 static bool? _monitor = true;
		/// <summary>
		/// If true, in first clipboard change change its value = false and monitor = null. In second monitor = true and in third is clipboard watching.
		/// </summary>
		 static bool _afterSet = false;

		private static class NativeMethods
		{
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
		}

		// Don't exists in mono
		private HwndSource hwndSource = new HwndSource(0, 0, 0, 0, 0, 0, 0, null, NativeMethods.HWND_MESSAGE);

        /// <summary>
        /// First is setted to false, after first save to clipboart auto to true
        /// </summary>
        public bool? monitor { get =>  _monitor; set => _monitor = value; }
        public bool afterSet { get => _afterSet; set => _afterSet = value; }
        public bool pernamentlyBlock { get => _pernamentlyBlock; set => _pernamentlyBlock = value; }

        private ClipboardMonitor()
		{
			hwndSource.AddHook(WndProc);
			NativeMethods.AddClipboardFormatListener(hwndSource.Handle);
		}

		public void Dispose()
		{
			NativeMethods.RemoveClipboardFormatListener(hwndSource.Handle);
			hwndSource.RemoveHook(WndProc);
			hwndSource.Dispose();
		}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (!pernamentlyBlock)
			{
				if (afterSet)
				{
					afterSet = false;
					monitor = null;
				}
				else if (monitor.HasValue && monitor.Value)
				{
					if (msg == NativeMethods.WM_CLIPBOARDUPDATE)
					{
						OnClipboardContentChanged.Invoke(this, EventArgs.Empty);

					}
				}
				else
				{
					monitor = true;
				}
			}

#if DEBUG
            ThisApp.Logger.WriteArgs("hwnd", hwnd, "msg", msg, "wParam", wParam, "handled", handled);
#endif

            return IntPtr.Zero;
		}

		/// <summary>
		/// Occurs when the clipboard content changes.
		/// </summary>
		public event EventHandler OnClipboardContentChanged;
	}
}
#endregion
