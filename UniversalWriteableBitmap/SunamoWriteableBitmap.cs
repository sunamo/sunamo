using apps;
using apps.Helpers;
using apps.Helpers.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UniversalWriteableBitmap
{
    public static class SunamoWriteableBitmap
    {
        public static Grid gridCreateWithImage = null;
        public static Grid grid = null;

        /// <summary>
        /// Funguje naprosto správně, už nic neměnit
        /// </summary>
        /// <param name="bi"></param>
        /// <param name="trans"></param>
        /// <param name="white2"></param>
        /// <returns></returns>
        public async static Task<WriteableBitmap> MakeWriteableBitmapTransparentAllOther(WriteableBitmap wb, Color trans, Color white2, int pixelWidth, int pixelHeight)
        {
            white2.A = 255;

            gridCreateWithImage.Background = new SolidColorBrush(trans);
            int nt = 0;
            int nt2 = 0;
            int nt3 = 0;
            using (BitmapContext ctx = wb.GetBitmapContext(ReadWriteMode.ReadWrite))
            {
                wb = ctx.WriteableBitmap;
                int to = wb.PixelWidth * wb.PixelHeight + 1;
                Color[,] pxs = new Color[wb.PixelWidth, wb.PixelHeight];

                for (int x = 0; x < wb.PixelWidth; x++)
                {
                    for (int y = 0; y < wb.PixelHeight; y++)
                    {
                        pxs[x, y] = wb.GetPixel(x, y);
                    }
                }

                var first = pxs[0, 0];
                
                for (int i = 0; i < pxs.GetLength(0); i++)
                {
                    for (int y = 0; y < pxs.GetLength(1); y++)
                    {
                        
                        var pxsi = pxs[i, y];
#if DEBUG
                        //ColorH.DebugWrite(pxsi);
#endif

                        bool b1 = false;
                        ColorH.IsColorSame(first, pxsi);

                        //bool b2 = pxsi.A < 254;
                        bool b2 = pxsi.A != 0;
                        if (b1)
                        {
                            nt3++;
                            pxs[i, y] = trans;
                            wb.SetPixel(i, y, trans);
                        }
                        else
                        {
                            //DebugLogger.Instance.Write(pxsi.Alpha + "-" + pxsi.Red + "-" + pxsi.Green + "-" + pxsi.Blue);
                            if (b2)
                            {
                                nt++;
                                pxs[i, y] = white2;
                                wb.SetPixel(i, y, white2);
                            }
                            else
                            {
                                nt2++;
                                pxs[i, y] = trans;
                                wb.SetPixel(i, y, trans);
                            }


                        }
                    }

                }


            }
            return wb;
            
                
            
        }

        public async static Task<WriteableBitmap> BufferToWriteableBitmap(IBuffer buffer, int width, int height)
        {
            WriteableBitmap bmp = BitmapFactory.New(width, height);
            bmp = await bmp.FromPixelBuffer(buffer, width, height);

            return bmp;


        }

        public async static Task<WriteableBitmap> CreateFromVisual(FrameworkElement text)
        {
            FrameworkElementHelper.SizeToContent(text);
                var renderTargetBitmap = new RenderTargetBitmap();
                //, (int)text.ActualWidth, (int)text.ActualHeight
                await renderTargetBitmap.RenderAsync(text);
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                var stream = new InMemoryRandomAccessStream();

                var bpf = BitmapPixelFormat.Bgra8;
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetPixelData(
                    bpf,
                    BitmapAlphaMode.Straight,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                    pixelBuffer.ToArray());

                await encoder.FlushAsync();

                WriteableBitmap wb = BitmapFactory.New(renderTargetBitmap.PixelWidth, renderTargetBitmap.PixelHeight);
            
                return await wb.FromStream(stream, bpf);
            
        }

    }
}
