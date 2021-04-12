
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

#if !NET5
namespace sunamo.Clipboard
{
    /// <summary>
    /// Funguje dokonale, jen se nesmí používat P/Invoke metody když to neumím. Nemusel bych přeinstalovávat!!!
    /// </summary>
	public sealed class ClipboardMonitor : IDisposable, IClipboardMonitor
    {
        public static ClipboardMonitor Instance = new ClipboardMonitor();

		 static bool _pernamentlyBlock = false;
		 static bool? _afterSet = false;

        /// <summary>
        /// Helper variable which change only IClipboardHelper and IClipboardMonitor
        /// after set to clipboard is set to null
        /// then is set to true
        /// on last is set to false
        /// everything is ready to use, developer must only call ClipboardHelper.SetText, nothing else
        /// </summary>
        public bool? afterSet { get => _afterSet; set => _afterSet = value; }
        public bool pernamentlyBlock { get => _pernamentlyBlock; set => _pernamentlyBlock = value; }

        /// <summary>
        /// Don't exists in mono
        /// </summary>
        private HwndSource hwndSource = new HwndSource(0, 0, 0, 0, 0, 0, 0, null, W32.HWND_MESSAGE);

        private ClipboardMonitor()
		{
			hwndSource.AddHook(WndProc);
			W32.AddClipboardFormatListener(hwndSource.Handle);
		}

		public void Dispose()
		{
			W32.RemoveClipboardFormatListener(hwndSource.Handle);
			hwndSource.RemoveHook(WndProc);
			hwndSource.Dispose();
		}

        const long wParamChromeOmnibox = 4;
        /// <summary>
        /// wParam during copy most text, which isnt captured ěx
        /// </summary>
        const long wParamRight = 5;
        long lastWParam = 0;
        public static string last = null;

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
            //////////DebugLogger.Instance.WriteArgs("hwnd", hwnd, "msg", msg, "wParam", wParam, "lParam", lParam);

            if (!pernamentlyBlock)
			{
                handled = true;

                if (afterSet == null)
                {
                    afterSet = true;
                }
                else if (afterSet == true)
                {
                    afterSet = false;
                }
                else
                {
                    if (msg == W32.WM_CLIPBOARDUPDATE)
                    {
                        if (last == null)
                        {
                            CopyToClipboard();
                        }
                        else
                        {
                            string content = ClipboardHelper.GetText();
                            if (last != content)
                            {
                                CopyToClipboard();
                            }
                            else
                            {
                                //last = null;
                            }
                        }
                    }
                }
            }

            return IntPtr.Zero;
		}

        private void CopyToClipboard()
        {
            last = ClipboardHelper.GetText();

            //if (ClipboardContentChanged != null)
            //{
            //    // Will be add all lines again if wont check for permanently block
            //    //ClipboardContentChanged();
            //}

            // Something copied to clipboard but still have Empty
            if (actionWithString != null)
            {
                actionWithString(last);
            }
            
        }


        Action<string> actionWithString;
        /// <summary>
        /// Must be method instead of event
        /// When I was attaching event more times in different UC, handler method would be calling times as registered in one UC
        /// </summary>
        /// <param name="a"></param>
        public void SetHandler(Action<string> a)
        {
            actionWithString = a;
        }

        //public void SetHandler(Action a)
        //{
        //    action = a;
        //}
    }
}
#endif 