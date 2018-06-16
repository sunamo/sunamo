using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop.WindowsSettings
{
    public class WindowsDisplaySettings
    {
        public enum DeviceCap
        {
            // Logical size - how is controls displayed
            VERTRES = 10,
            // Psychical size - setted resolution but view is changed by scalin factor (vertres)
            DESKTOPVERTRES = 117,
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90

            // Other constants may be founded on pinvoke.net
        }

        public static double getScalingFactor()
        {
            bool nonOrdinalDpi;
            return getScalingFactor(out nonOrdinalDpi);
        }

        /// <summary>
        /// Make divide psychical size / logical size 
        /// </summary>
        /// <returns></returns>
        public static double getScalingFactor(out bool nonOrdinalDpi)
        {
            /*
             * Both graphics.DpiX and DeviceCap.LOGPIXELSX return 96 on Surface Pro in all scaling levels.
                Instead, I managed to calculate the scaling factor this way:
             */
            nonOrdinalDpi = false;
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            // 1152
            int LogicalScreenHeight = W32.GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            // 1440
            int PhysicalScreenHeight = W32.GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            // For scaling factor 1.25 perharps returns DPI 96
            int LogPixelsY = W32.GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSY);

            double ScreenScalingFactor = (double)PhysicalScreenHeight / (double)LogicalScreenHeight;
            float DpiScalingFactor = (float)LogPixelsY / (float)96;

            if (ScreenScalingFactor > 1 ||
            DpiScalingFactor > 1)
            {
                // do something nice for people who can't see very well...
                nonOrdinalDpi = true;
            }

            return ScreenScalingFactor; // 1.25 = 125%
        }
    }
}
