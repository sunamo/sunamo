using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace apps
{
    public class FrameworkElementHelper
    {
        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        /// <summary>
        /// Vrátí new Size(fe.ActualWidth, fe.ActualHeight);
        /// </summary>
        /// <param name="fe"></param>
        /// <returns></returns>
        public static Size GetMaxContentSize(FrameworkElement fe)
        {
            return new Size(fe.ActualWidth, fe.ActualHeight);
        }

        public static void SetMaxContentSize(FrameworkElement fe, Size s)
        {
            fe.MaxWidth = s.Width;
            fe.MaxHeight = s.Height;
        }

        

        public async static Task<RenderTargetBitmap> CaptureToStreamAsync(FrameworkElement uielement, IRandomAccessStream stream, Guid encoderId)
        {
            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(uielement);

                var pixels = await renderTargetBitmap.GetPixelsAsync();

                var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
                var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    logicalDpi,
                    logicalDpi,
                    pixels.ToArray());

                await encoder.FlushAsync();

                return renderTargetBitmap;
            }
            catch (Exception ex)
            {
                //DisplayMessage(ex.Message);
            }

            return null;
        }

        public static void SizeToContent(FrameworkElement window)
        {
            window.Measure(ControlHelper.SizePositiveInfinity);
            window.Arrange(new Rect(0, 0, window.DesiredSize.Width, window.DesiredSize.Height));
        }
    }
}
