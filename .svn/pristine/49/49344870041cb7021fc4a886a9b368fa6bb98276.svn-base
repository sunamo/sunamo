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
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }


        public static double getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = W32.GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = W32.GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            double ScreenScalingFactor = (double)PhysicalScreenHeight / (double)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }
    }
}
