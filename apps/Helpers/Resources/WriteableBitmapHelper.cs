using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace apps.Helpers.Resources
{
    public class WriteableBitmapHelper
    {
        public static async Task<IRandomAccessStream> EncodeWriteableBitmap(WriteableBitmap bmp, IRandomAccessStream writeStream, Guid encoderId)
        {
            // Copy buffer to pixels
            byte[] pixels;
            using (var stream = bmp.PixelBuffer.AsStream())
            {
                pixels = new byte[(uint)stream.Length];
                await stream.ReadAsync(pixels, 0, pixels.Length);
            }

            // Encode pixels into stream
            var encoder = await BitmapEncoder.CreateAsync(encoderId, writeStream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied,
               (uint)bmp.PixelWidth, (uint)bmp.PixelHeight,
               96, 96, pixels);
            await encoder.FlushAsync();

            return writeStream;
        }

        public async static Task< BitmapImage> ToBitmapImage(WriteableBitmap wb)
        {
            var ms = new InMemoryRandomAccessStream();
            await WriteableBitmapHelper.EncodeWriteableBitmap(wb, ms, BitmapEncoder.PngEncoderId);

            ms.Seek(0);

            var bm = new BitmapImage();

            //bm.CreateOptions = BitmapCreateOptions.None;
            bm.SetSource(ms);

            return bm;
        }

    }
}
