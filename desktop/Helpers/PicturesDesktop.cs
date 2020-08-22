using desktop.Data;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


    public partial class PicturesDesktop
    {
    static Type type = typeof(PicturesDesktop);

    /// <summary>
    /// A1 must be BitmapSource, not ImageSource
    /// A2 was originally Colors.Magenta
    /// </summary>
    /// <param name="bs"></param>
    public static BitmapSource MakeTransparentWindowsFormsButton(BitmapSource bs, System.Windows.Media.Color c)
        {
            return MakeTransparentBitmap(bs, c);
        }

    

		public static Bitmap BitmapImage2Bitmap(BitmapSource bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                // return bitmap; <-- leads to problems, stream is closed/closing ...
                return new Bitmap(bitmap);
            }
        }

    #region Mono
    #region Již v CreateW10AppGraphics - několik PlaceToCenter metod
    /// <summary>
    /// Funguje naprosto správně, už nic neměnit
    /// 11-2-2019 nsn
    /// Not use Lunapic or my code to create favicon. Always download image from net
    /// </summary>
    /// <param name="bi"></param>
    /// <param name="trans"></param>
    /// <param name="white2"></param>
    private static WriteableBitmap MakeWriteableBitmapTransparentAllOther(BitmapSource bi, PixelColor trans, PixelColor white2)
        {
            white2.Alpha = 255;
            PixelColor pxZero = new PixelColor() { Alpha = 0, Red = 0, Green = 0, Blue = 0 };
            WriteableBitmap wb = new WriteableBitmap(bi);
            var pxs = BitmapSourceHelper.GetPixels(bi);
            var first = pxs[0, 0];
            int nt = 0;
            int nt2 = 0;
            for (int i = 0; i < pxs.GetLength(0); i++)
            {
                for (int y = 0; y < pxs.GetLength(1); y++)
                {
                    var pxsi = pxs[i, y];
                    bool b1 = shared.ColorHelper.IsColorSame(first, pxsi);
                    bool b2 = pxsi.Alpha < 254;
                    if (b1)
                    {
                        pxs[i, y] = trans;
                    }
                    else
                    {
                        //////DebugLogger.Instance.Write(pxsi.Alpha + AllStrings.dash + pxsi.Red + AllStrings.dash + pxsi.Green + AllStrings.dash + pxsi.Blue);
                        if (b2)
                        {
                            nt++;
                            pxs[i, y] = white2;
                        }
                        else
                        {
                            nt2++;
                            pxs[i, y] = trans;
                        }


                    }
                }
            }



            BitmapSourceHelper.PutPixels(wb, pxs, 0, 0);
            return wb;
        }

        private static BitmapSource CreateBitmapSourceAndDrawOpacity(int pixelWidth, int pixelHeight, BitmapSource bmp2, double y, double x, bool useA3PixelSize)
        {
            DrawingVisual dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            //dc.DrawRectangle(new SolidColorBrush(System.Windows.Media.Colors.Red), new System.Windows.Media.Pen(new SolidColorBrush(System.Windows.Media.Colors.Red), 50), new Rect(x, y, bmp2.Width, bmp2.Height));
            dc.PushOpacity(1);
            double w = bmp2.Width;
            double h = bmp2.Height;

            if (useA3PixelSize)
            {
                w = bmp2.PixelWidth;
                h = bmp2.PixelHeight;
            }

            dc.DrawImage(bmp2, new Rect(x, y, w, h));

            ////dc.Pop();
            dc.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(pixelWidth, pixelHeight, 96, 96, PixelFormats.Default);
            bmp.Render(dv);



            return bmp;
        }

    


        private static BitmapSource CreateBitmapSource(double width, double height, double minimalWidthPadding, double minimalHeightPadding, string arg, BitmapSource img2, bool useAtA1PixelSize = false)
        {
            BitmapSource bi;
            //int stride = (int)width / 8;
            int stride = (int)width * ((img2.Format.BitsPerPixel + 7) / 8);
            byte[] pixels = new byte[(int)height * stride];

            BitmapSource img = null;
            img = BitmapSource.Create((int)(width), (int)(height), 96, 96, PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent, pixels, stride);





            var wb = img;
            if (minimalHeightPadding <= 0 && minimalWidthPadding <= 0)
            {

                bi = PicturesDesktop.PlaceToCenter(wb, wb.Width, wb.Height, false, 0, 0, arg, img2, true);

            }
            else
            {
                bi = PicturesDesktop.PlaceToCenter(wb, wb.Width, wb.Height, false, minimalWidthPadding / 2, minimalHeightPadding / 2, arg, img2, useAtA1PixelSize);
            }

            //return PlaceToCenterExactly(img, args, width, height, i, temp, writeToConsole, minimalWidthPadding, minimalHeightPadding);
            return bi;
        }

        #region PlaceToCenter metody - využívající WPF třídu BitmapSource kterou vrací
        private static BitmapSource PlaceToCenter(BitmapSource img, double width, double height, bool writeToConsole, double minimalWidthPadding, double minimalHeightPadding, string arg, BitmapSource bmp2, bool useAtA1PixelSize = false)
        {

            string fnOri = FS.GetFileName(arg);
            string ext = "";
            if (PicturesSunamo.GetImageFormatFromExtension1(fnOri, out ext))
            {
                double h2 = bmp2.Height;
                double w2 = bmp2.Width;
                if (useAtA1PixelSize)
                {
                    h2 = bmp2.PixelHeight;
                    w2 = bmp2.PixelWidth;
                }
                double y = (height - h2);
                double x = (width - w2);
                // Prvně si já ověřím zda obrázek je delší než šířka aby to nebylo kostkované
                if (y < 1 || x < 1)
                {
                    return CreateBitmapSourceAndDrawOpacity(bmp2.PixelWidth, bmp2.PixelHeight, bmp2, 0, 0, true);
                }
                if (y < 0)
                {
                    y = 0;
                }
                if (x < 0)
                {
                    x = 0;
                }


                #region MyRegion

                double w = 0;
                double h = 0;
                w = img.Width;
                h = img.Height;
                while (w > width && h > height)
                {
                    w *= .9f;
                    h *= .9f;
                }
                if (width <= height)
                {
                    double minimalHeightPadding2 = (minimalHeightPadding * 2);
                    double minimalWidthPadding2 = (minimalWidthPadding * 2);
                    if (minimalHeightPadding2 + bmp2.Height <= (img.Height - 1))
                    {
                        y = ((img.Height - bmp2.Height) / 2);
                    }
                    if (minimalWidthPadding2 + bmp2.Width <= (img.Width - 1))
                    {
                        x = ((img.Width - bmp2.Width) / 2);
                    }


                }
                else
                {

                }
                x /= 2;
                y /= 2;
                if (writeToConsole)
                {
                    InitApp.TemplateLogger.SuccessfullyResized(FS.GetFileName(arg));
                }

                return CreateBitmapSourceAndDrawOpacity(img.PixelWidth, img.PixelHeight, bmp2, y, x, useAtA1PixelSize);
                #endregion

            }
            else
            {
            ThrowExceptions.FileHasExtensionNotParseableToImageFormat(Exc.GetStackTrace(),type, Exc.CallingMethod(), fnOri);
        }
            return null;
        }

        

        public static BitmapSource PlaceToCenterFixedPercentSize(string path, string bi, double targetWidth, double targetHeight, double percentWidthIconOfImage, double percentHeightIconOfImage, PixelColor bgPixelColor, PixelColor fgPixelColor, PixelColor definitelyFgPixelColor)
        {

            var wb = bi;

            double width = targetWidth;
            double height = targetHeight;

            double paddingLeftRight = 0;
            double paddingTopBottom = 0;
            BitmapSource vr = null;
            double ratioW = 0;
            double ratioH = 0;
            bool ts16 = false;
            if (!path.Contains("unplated"))
            {
                double newWidth = width * percentWidthIconOfImage / 100;
                paddingLeftRight = (width - newWidth) / 2;
                double newHeight = height * percentHeightIconOfImage / 100;
                paddingTopBottom = (height - newHeight) / 2;

                if (path.Contains("targetsize-16"))
                {
                    ts16 = true;
                    //vr = PicturesShared.PlaceToCenterExactly(width, height, false, paddingLeftRight, paddingTopBottom, bi, ratioW, ratioH, true);
                    vr = PicturesDesktop.ImageResize(bi, width, height, 0, 0, PicturesSunamo.GetImageFormatsFromExtension(bi), true);
                    vr = CreateBitmapSource(vr.PixelWidth, vr.PixelHeight, paddingLeftRight, paddingTopBottom, bi, vr, true);
                }
                else if (path.Contains("targetsize"))
                {
                    vr = PicturesDesktop.PlaceToCenterExactly(width, height, false, paddingLeftRight, paddingTopBottom, bi, ratioW, ratioH, false);
                }
                else
                {
                    vr = PicturesDesktop.PlaceToCenterExactly(width, height, false, paddingLeftRight, paddingTopBottom, bi, ratioW, ratioH, false);
                }
            }
            else
            {


                vr = PicturesDesktop.ImageResize(bi, width, height, 0, 0, PicturesSunamo.GetImageFormatsFromExtension(bi), true);
                vr = CreateBitmapSource(vr.PixelWidth, vr.PixelHeight, 0, 0, bi, vr);

            }

            var wb2 = vr;
            if (!false)
            {
                wb2 = MakeWriteableBitmapTransparentAllOther(vr, bgPixelColor, fgPixelColor);
                //}
            }


            return wb2;

        }

        /// <summary>
        /// Umístí obrázek na střed s paddingem skoro přesným(maximálně o pár px vyšším)
        /// Do A7 a A8 zadávej hodnoty pro levý/pravý a vrchní/spodnní padding, nikoliv ale jejich součet, metoda si je sama vynásobí
        /// </summary>
        /// <param name="img"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="i"></param>
        /// <param name="finalPath"></param>
        /// <param name="writeToConsole"></param>
        /// <param name="minimalWidthPadding"></param>
        /// <param name="minimalHeightPadding"></param>
        /// <param name="args"></param>
        public static BitmapSource PlaceToCenterExactly(double width, double height, bool writeToConsole, double minimalWidthPadding, double minimalHeightPadding, string arg2, double ratioW, double ratioH, bool useAtA1PixelSize = false)
        {
            BitmapImage bi2 = new BitmapImage(new Uri(arg2));
            ratioW = bi2.PixelWidth / bi2.Width;
            ratioH = bi2.PixelHeight / bi2.Height;
            BitmapImageWithPath arg = new BitmapImageWithPath(arg2, bi2);

            // OK, já teď potřebuji zjistit na jakou velikost mám tento obrázek zmenšit
            string fnOri = ""; // FS.GetFileName(args[i]);
            double minWidthImage = width - (minimalWidthPadding);
            double minHeightImage = height - (minimalHeightPadding);
            double newWidth = width;
            double newHeight = height;
            double innerWidth = arg.image.Width;
            double innerHeight = arg.image.Height;
            var img2 = PicturesDesktop.ImageResize(arg.path, minWidthImage, minHeightImage, 0, 0, PicturesSunamo.GetImageFormatsFromExtension(arg.path), useAtA1PixelSize);
            BitmapSource bi = null;
            //bi = img2;
            if (true && img2 != null)
            {
                bi = CreateBitmapSource(width, height, minimalWidthPadding, minimalHeightPadding, arg.path, img2, useAtA1PixelSize);
            }
            else
            {
            ThrowExceptions.FileHasExtensionNotParseableToImageFormat(Exc.GetStackTrace(),type, Exc.CallingMethod(), fnOri);
        }
            return bi;
        }
    #endregion
    #endregion
    #endregion

    #region Mono
    /// <summary>
    /// 11-2-2019 nsn
    /// Not use Lunapic or my code to create favicon. Always download image from net
    /// </summary>
    /// <param name="bi"></param>
    /// <param name="trans"></param>
    /// <param name="white2"></param>
    private static WriteableBitmap MakeWriteableBitmapTransparentFill(BitmapSource bi, PixelColor trans, PixelColor white2)
        {
            white2.Alpha = 255;
            //PixelColor px = white2;
            trans = new PixelColor() { Alpha = 0, Red = 255, Green = 255, Blue = 255 };
            WriteableBitmap wb = new WriteableBitmap(bi);
            var pxs = BitmapSourceHelper.GetPixels(bi);
            var first = pxs[0, 0];
            for (int i = 0; i < pxs.GetLength(0); i++)
            {
                for (int y = 0; y < pxs.GetLength(1); y++)
                {
                    var pxsi = pxs[i, y];
                    if (pxsi.Red != 0 || pxsi.Blue != 0 || pxsi.Green != 0)
                    {
                        if (pxsi.Red == 255 || pxsi.Blue == 255 || pxsi.Green == 255)
                        {
                            if (pxsi.Alpha == 0)
                            {
                                pxs[i, y] = trans;
                            }
                            else
                            {
                                pxs[i, y] = white2;
                            }
                            //}
                        }
                        else
                        {
                            pxs[i, y] = trans;
                        }
                    }
                    else
                    {
                        if (pxsi.Alpha == 0)
                        {
                            pxs[i, y] = trans;
                        }
                        else
                        {
                            pxs[i, y] = white2;
                        }
                    }
                }
            }



            BitmapSourceHelper.PutPixels(wb, pxs, 0, 0);
            return wb;
        }

    /// <summary>
    /// 11-2-2019 nsn
    /// Not use Lunapic or my code to create favicon. Always download image from net
    /// </summary>
    /// <param name="bi"></param>
    /// <param name="trans"></param>
    /// <param name="white2"></param>
    private static WriteableBitmap MakeWriteableBitmapTransparent(BitmapSource bi, PixelColor trans, PixelColor white2)
        {
            white2.Alpha = 255;
            //PixelColor px = white2;
            trans = new PixelColor() { Alpha = 0, Red = 255, Green = 255, Blue = 255 };
            WriteableBitmap wb = new WriteableBitmap(bi);
            var pxs = BitmapSourceHelper.GetPixels(bi);
            var first = pxs[0, 0];
            for (int i = 0; i < pxs.GetLength(0); i++)
            {
                for (int y = 0; y < pxs.GetLength(1); y++)
                {
                    var pxsi = pxs[i, y];
                    if (pxsi.Red != 0 || pxsi.Blue != 0 || pxsi.Green != 0)
                    {
                        if (pxsi.Red == 255 || pxsi.Blue == 255 || pxsi.Green == 255)
                        {
                            if (pxsi.Alpha == 0)
                            {
                                pxs[i, y] = white2;
                            }
                            else
                            {
                                pxs[i, y] = trans;
                            }
                            //}
                        }
                        else
                        {
                            //pxs[i, y] = white2;
                        }
                    }
                    else
                    {
                        if (pxsi.Alpha == 0)
                        {
                            pxs[i, y] = trans;
                        }
                        else
                        {
                            pxs[i, y] = trans;
                        }
                    }
                }
            }



            BitmapSourceHelper.PutPixels(wb, pxs, 0, 0);
            return wb;
        }

        /// <summary>
        /// A7 zda 
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="decodePixelWidth"></param>
        /// <param name="decodePixelHeight"></param>
        /// <param name="paddingLeftRight"></param>
        /// <param name="paddingTopBottom"></param>
        /// <param name="imgsf"></param>
        public static BitmapSource ImageResize(string imageSource, double decodePixelWidth, double decodePixelHeight, double paddingLeftRight, double paddingTopBottom, ImageFormats imgsf, bool a2IsPixelWidth = false)
        {
            double margin = 0;

            #region Zmenšuje načerno
            #endregion





            #region Při menších rozlišení zmenšuje špatně
            #endregion




            #region Zmenšuje do obrázku velikosti 1x1px
            BitmapImage ims = new BitmapImage(new Uri(imageSource));

            double d1, d2;
            if (a2IsPixelWidth)
            {
                d1 = (decodePixelWidth / (double)ims.PixelWidth) / 1;
                d2 = (decodePixelHeight / (double)ims.PixelHeight) / 1;
            }
            else
            {
                d1 = decodePixelWidth / (double)ims.Width;
                d2 = decodePixelHeight / (double)ims.Height;
            }

            ScaleTransform st = new ScaleTransform();

            double rate = Math.Min(d1, d2);
            st.ScaleX = rate;
            st.ScaleY = rate;
            TransformedBitmap tb = new TransformedBitmap(ims, st);

            return tb;
            #endregion

        }
        #endregion

    }