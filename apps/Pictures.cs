

using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace apps
{
    public class Pictures
    {


        

        public async static Task< Stream> TransformImage(StorageFile file, double newWidth, double newHeight, string newFullPath)
        {
            // create a stream from the file and decode the image
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
            BitmapSource bs = new BitmapImage(new Uri(file.Path));
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

            // create a new stream and encoder for the new image
            InMemoryRandomAccessStream ras = new InMemoryRandomAccessStream();
            BitmapEncoder enc = await BitmapEncoder.CreateForTranscodingAsync(ras, decoder);

            // convert the entire bitmap to a 100px by 100px bitmap
            enc.BitmapTransform.ScaledHeight = (uint)newHeight;
            enc.BitmapTransform.ScaledWidth = (uint)newWidth;

            BitmapBounds bounds = new BitmapBounds();
            bounds.Height = (uint)bs.PixelHeight;
            bounds.Width = (uint)bs.PixelWidth;
            bounds.X = 0;
            bounds.Y = 0;
            enc.BitmapTransform.Bounds = bounds;

            // write out to the stream
            try
            {
                await enc.FlushAsync();
            }
            catch (Exception ex)
            {
                //string s = ex.ToString();
                return null;
            }

            return ras.AsStream();
        }


        public async static Task<BitmapImage> UIElementToImage(UIElement uie)
        {
            var renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(uie);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            
                using (var stream = new InMemoryRandomAccessStream())
                {
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                    encoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Straight,
                        (uint)renderTargetBitmap.PixelWidth,
                        (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                        pixelBuffer.ToArray());

                    await encoder.FlushAsync();

                var bi = new BitmapImage();
                bi.SetSource(stream);
                return bi;
            }

            return null;
        }

    }
}
