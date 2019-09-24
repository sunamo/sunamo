#region Mono

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
    /// <summary>
    /// Funguje dokonale, jen se nesmí používat P/Invoke metody když to neumím. Nemusel bych přeinstalovávat!!!
    /// </summary>
	public sealed class ClipboardMonitor : IDisposable, IClipboardMonitor
    {
        public static ClipboardMonitor Instance = new ClipboardMonitor();

		 static bool _pernamentlyBlock = false;
		 static bool? _monitor = true;
		/// <summary>
		/// If true, in first clipboard change change its value = false and monitor = null. In second monitor = true and in third is clipboard watching.
		/// </summary>
		 static bool _afterSet = false;

        /// <summary>
        /// First is setted to false, after first save to clipboart auto to true
        /// </summary>
        public bool? monitor { get =>  _monitor; set => _monitor = value; }
        public bool afterSet { get => _afterSet; set => _afterSet = value; }
        public bool pernamentlyBlock { get => _pernamentlyBlock; set => _pernamentlyBlock = value; }

        // Don't exists in mono
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


        string last = null;

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
            //DebugLogger.Instance.WriteArgs("hwnd", hwnd, "msg", msg, "wParam", wParam, "lParam", lParam);

            if (!pernamentlyBlock)
			{
                handled = true;
                //bool avoidTwoTimes = lastWParam == wParamChromeOmnibox;
                //lastWParam = wParam.ToInt64();
                //if (avoidTwoTimes)
                //{
                //    lastWParam = wParamRight;
                //    return IntPtr.Zero;
                //}

                if (afterSet)
				{
					afterSet = false;
                    // With this I never on second attempt invoke event, because its jump into 3th case of this if
					//monitor = null;
				}
                #region MyRegion
                //else if (monitor.HasValue && !monitor.Value)
                //{
                //    monitor = null;
                //}
                //else if (!monitor.HasValue)
                //{
                //    monitor = true;
                //} 
                #endregion
                else if (monitor.HasValue && monitor.Value)
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
                                last = null;
                            }
                        }
                        // After second calling app will be crash and no unhandled exception is generated
                        // ClipboardHelper also is working perfectly with that
                        
                            
                        

					}
				}
				
            }

            return IntPtr.Zero;
		}

        private void CopyToClipboard()
        {
            // Will be add all lines again if wont check for permanently block
            ClipboardContentChanged();
            last = ClipboardHelper.GetText();
        }

        /// <summary>
        /// Occurs when the clipboard content changes.
        /// </summary>
        public event VoidVoid ClipboardContentChanged;
	}
}
#endregion
