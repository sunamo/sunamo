using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace apps.Helpers
{
    public class XamlHelper
    {
        public async static Task<BitmapImage> CreateBitmapImageFromVisual(FrameworkElement text)
        {
            if (text.ActualHeight != 0 && text.ActualWidth != 0 && false)
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                //, (int)text.ActualWidth, (int)text.ActualHeight
                await renderTargetBitmap.RenderAsync(text);
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                var stream = new InMemoryRandomAccessStream();


                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Straight,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                    pixelBuffer.ToArray());

                await encoder.FlushAsync();

                BitmapImage vr = new BitmapImage();
                vr.SetSource(stream);
                return vr;
            }
            return null;
        }

        public async static Task SaveImage(FrameworkElement text, StorageFile sf)
        {
            var renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(text);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            var stream = await sf.OpenAsync(FileAccessMode.ReadWrite);


            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            encoder.SetPixelData(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                (uint)renderTargetBitmap.PixelWidth,
                (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                pixelBuffer.ToArray());

            await encoder.FlushAsync();
        }
    }
}
