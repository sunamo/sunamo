using sunamo;
using sunamo.Data;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using static desktop.WindowsSettings.WindowsDisplaySettings;

namespace desktop
{
    public class DisplayHelper
    {
        public static SunamoSize GetPrimaryScreenResolution()
        {
            //Width: 2048 Height: 1152 (logical) - Real: Width: 2560 Height: 1440 (scale factor 1.25)
            return new SunamoSize(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
        }

        /// <summary>
        /// Cannot be used in console apps due to MainWindow reference
        /// </summary>
        /// <returns></returns>
        public static DoubleXY GetDpi(Visual visual)
        {
            PresentationSource source = PresentationSource.FromVisual(visual);

            double dpiX = 0;
            double dpiY = 0;
            if (source != null)
            {
                // return value is in logical unit, for DPI 96 return 1
                dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
                dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
            }

            return new DoubleXY(dpiX, dpiY);
        }

        public static DoubleXY GetDpi4(Visual visual)
        {
            Matrix matrix;
            var source = PresentationSource.FromVisual(visual);
            if (source != null)
            {
                matrix = source.CompositionTarget.TransformToDevice;
            }
            else
            {
                using (var src = new HwndSource(new HwndSourceParameters()))
                {
                    matrix = src.CompositionTarget.TransformToDevice;
                }
            }
            return null;
        }


        /// <summary>
        /// returns: X: 120 Y: 120 Real: scale factor 1.25
        /// </summary>
        /// <returns></returns>
        public static DoubleXY GetDpi()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();

            int Xdpi = W32.GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSX);
            int Ydpi = W32.GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSY);

            // Found here https://dzimchuk.net/best-way-to-get-dpi-value-in-wpf/ (method 2)
            //W32.ReleaseDC(IntPtr.Zero, desktop);

            return new DoubleXY(Xdpi, Ydpi);
        }

        /// <summary>
        /// returns: X: 1.25 Y: 1.25 Real: scale factor 1.25
        /// </summary>
        /// <returns></returns>
        public static DoubleXY GetDpi2()
        {
            DoubleXY xy = null;
            IntPtr hDc = W32.GetDC(IntPtr.Zero);
            if (hDc != IntPtr.Zero)
            {
                int dpiX = W32.GetDeviceCaps(hDc, (int)DeviceCap.LOGPIXELSX);
                int dpiY = W32.GetDeviceCaps(hDc, (int)DeviceCap.LOGPIXELSY);

                W32.ReleaseDC(IntPtr.Zero, hDc);

                xy = new DoubleXY((double)dpiX / 96, (double)dpiY / 96);
            }
            else
                throw new ArgumentNullException("Failed to get DC.");

            return xy;
        }

        /// <summary>
        /// Return always(with or wo manifest) 0, never fall to exception 
        /// Need add to app manifest: https://stackoverflow.com/a/43451508/9327173
        /// </summary>
        /// <returns></returns>
        public static int GetDpiW32()
        {
            try
            {
                return W32.GetDpiForWindow(IntPtr.Zero);
            }
            catch (Exception)
            {
                return Consts.DefaultDpi;
            }
        }

        /// <summary>
        /// returns: X: 120 Y: 120 Real: scale factor 1.25
        /// </summary>
        /// <returns></returns>
        public static DoubleXY GetDpiConsole()
        {
            double dpiX = 0;
            double dpiY = 0;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }
            return new DoubleXY(dpiX, dpiY);
        }


    }
}
