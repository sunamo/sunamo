using desktop.WindowsSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace desktop
{
    public class VisualHelper
    {
        /// <summary>
        /// A1 je Control jen proto abych mohl zjistit DPI, pokud bys ho zjistil z jiného controlu můžeš do A1 použít UIElement
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        private static RenderTargetBitmap ConvertToBitmap(FrameworkElement uiElement, double resolutionX, double resolutionY)
        {
            double dpi = WindowsDisplaySettings.getScalingFactor();
            var scaleX = resolutionX / dpi;
            var scaleY = resolutionY / dpi;
            scaleX = dpi;
            scaleY = dpi;

            uiElement.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            var sz = uiElement.DesiredSize;
            var rect = new Rect(sz);
            uiElement.Arrange(rect);

            var bmp = new RenderTargetBitmap((int)(scaleX * (rect.Width)), (int)(scaleY * (rect.Height)), scaleX * dpi, scaleY * dpi, PixelFormats.Default);
            //ModifyPosition(uiElement);
            bmp.Render(uiElement);
            return bmp;
        }

        public static byte[] ConvertToJpegBytes(FrameworkElement uiElement, double resolutionX, double resolutionY)
        {
            var jpegString = CreateJpeg(ConvertToBitmap(uiElement, resolutionX, resolutionY));
            MemoryStream ms = null;
            ms = new MemoryStream();

            var streamWriter = new StreamWriter(ms, Encoding.UTF8);
                    
            streamWriter.Write(jpegString);

            ms.Seek(0, SeekOrigin.Begin);
            var vr = ms.ToArray();
            streamWriter.Close();
            return vr;
        }

        private static void ModifyPositionBack(FrameworkElement fe)
        {
            /// remeasure a size smaller than need, wpf will
            /// rearrange it to the original position
            fe.Measure(new Size());
        }

        private static void ModifyPosition(FrameworkElement fe)
        {
            /// get the size of the visual with margin
            Size fs = new Size(
                fe.ActualWidth +
                fe.Margin.Left + fe.Margin.Right,
                fe.ActualHeight +
                fe.Margin.Top + fe.Margin.Bottom);

            /// measure the visual with new size
            fe.Measure(fs);

            /// arrange the visual to align parent with (0,0)
            fe.Arrange(new Rect(
                -fe.Margin.Left, -fe.Margin.Top,
                fs.Width, fs.Height));
        }

        private static string CreateJpeg(RenderTargetBitmap bitmap)
        {
            var jpeg = new JpegBitmapEncoder();
            jpeg.Frames.Add(BitmapFrame.Create(bitmap));
            string result;

            using (var memoryStream = new MemoryStream())
            {
                jpeg.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                memoryStream.Close();
            }

            return result;
        }

        public static byte[] ConvertVisualToBytes(FrameworkElement maybePanel, FrameworkElement v, Size forceSizeTo)
        {
            /// get bound of the visual
            //Rect b = VisualTreeHelper.GetDescendantBounds(v);
            Rect b = BoundsRelativeTo(v, maybePanel);

            if (forceSizeTo != null)
            {
                b.Size = forceSizeTo;
            }

            /// new a RenderTargetBitmap with actual size of c
            RenderTargetBitmap r = new RenderTargetBitmap(
                (int)b.Width, (int)b.Height,
                96, 96, PixelFormats.Pbgra32);

            /// render visual
            ModifyPosition(v);
            r.Render(v);
            ModifyPositionBack(v);

            /// new a JpegBitmapEncoder and add r into it 
            JpegBitmapEncoder e = new JpegBitmapEncoder();
            e.Frames.Add(BitmapFrame.Create(r));

            MemoryStream ms = new MemoryStream();
            /// new a FileStream to write the image file
            //FileStream s = new FileStream(f, FileMode.OpenOrCreate, FileAccess.Write);
            e.Save(ms);
            var vr = ms.ToArray();
            ms.Close();
            return vr;
        }

        /// <summary>
        /// A1 je objekt u kterého chci zjistit Rect
        /// A2 je jeho hlavní kontainer(většinou nějaký panel)
        /// </summary>
        /// <param name="element"></param>
        /// <param name="relativeTo"></param>
        /// <returns></returns>
        public static Rect BoundsRelativeTo( FrameworkElement element, Visual relativeTo)
        {
            return
              element.TransformToVisual(relativeTo)
                     .TransformBounds(LayoutInformation.GetLayoutSlot(element));
        }

        static DrawingVisual ModifyToDrawingVisual(Visual v)
        {
            Rect b = VisualTreeHelper.GetDescendantBounds(v);
            /// new a drawing visual and get its context
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();

            /// generate a visual brush by input, and paint
            VisualBrush vb = new VisualBrush(v);
            dc.DrawRectangle(vb, null, b);
            dc.Close();

            return dv;
        }
    }
}
