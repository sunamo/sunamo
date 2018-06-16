using System.Drawing;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using sunamo;
using sunamo.Data;

namespace shared
{
    public class Pictures
    {
        private static Regex r = new Regex(":");

        public static BitmapSource MakeTransparentWindowsFormsButton(BitmapSource bs)
        {
            return MakeTransparentBitmap(bs, Colors.Magenta);
        }

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr24, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

        public static BitmapSource MakeTransparentBitmap(BitmapSource sourceImage, System.Windows.Media.Color transparentColor)
        {
            if (sourceImage.Format != PixelFormats.Bgra32) //if input is not ARGB format convert to ARGB firstly
            {
                sourceImage = new FormatConvertedBitmap(sourceImage, PixelFormats.Bgra32, null, 0.0);
            }

            int stride = (sourceImage.PixelWidth * sourceImage.Format.BitsPerPixel) / 8;

            byte[] pixels = new byte[sourceImage.PixelHeight * stride];

            sourceImage.CopyPixels(pixels, stride, 0);

            byte red = transparentColor.R;
            byte green = transparentColor.G;
            byte blue = transparentColor.B;
            for (int i = 0; i < sourceImage.PixelHeight * stride; i += (sourceImage.Format.BitsPerPixel / 8))
            {

                if (pixels[i] == blue
                && pixels[i + 1] == green
                && pixels[i + 2] == red)
                {
                    pixels[i + 3] = 0;
                }

            }

            BitmapSource newImage
                = BitmapSource.Create(sourceImage.PixelWidth, sourceImage.PixelHeight,
                                sourceImage.DpiX, sourceImage.DpiY, PixelFormats.Bgra32, sourceImage.Palette, pixels, stride);

            return newImage;
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

        public static ImageFormat GetImageFormatFromExtension2(string ext)
        {
            if (ext == "jpeg")
            {
                return ImageFormat.Jpeg;
            }
            else if (ext == "jpg")
            {
                return ImageFormat.Jpeg;
            }
            else if (ext == "png")
            {
                return ImageFormat.Png;
            }
            else if (ext == "gif")
            {
                return ImageFormat.Gif;
            }
            return null;
        }

        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        /// <param name="fn"></param>
        public static string SuccessfullyResized(string fn)
        {
            return "Successfully resized to " + fn;
        }

        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        public static string FileHasWrongExtension(string fnOri)
        {
            return "File " + fnOri + " has wrong file extension";
        }

        public static string ImageToBase64(string path, ImageFormat jpeg, out int width, out int height)
        {

            if (File.Exists(path))
            {
                Image imgo = Image.FromFile(path);
                width = imgo.Width;
                height = imgo.Height;
                return Pictures.ImageToBase64(imgo, jpeg);
            }
            width = 0;
            height = 0;
            return "";
        }

        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        //retrieves the datetime WITHOUT loading the whole image
        public static DateTime GetDateTakenFromImage(string path, DateTime getIfNotFound)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                int propId = 36867;
                foreach (PropertyItem item in myImage.PropertyItems)
                {
                    if (item.Id == propId)
                    {
                        PropertyItem propItem = myImage.GetPropertyItem(propId);
                        string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                        return DateTime.Parse(dateTaken);
                    }
                }
                return getIfNotFound;
            }
        }

        

        /// <summary>
        /// Po uložení obrázku jej i všechny ostatní prostředky zlikviduje.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="path"></param>
        public static void TransformImage(System.Drawing.Image image, int width, int height, string path)
        {

            #region Zakomentováno, z důvodu že mi to špatně zvětšovalo čtvercové obrázky na obdelníkové
            float scale = (float)width / (float)image.Width;
            using (Bitmap thumb = new Bitmap(width, height))
            {
                using (Graphics graphics = Graphics.FromImage(thumb))
                {
                    using (System.Drawing.Drawing2D.Matrix transform = new System.Drawing.Drawing2D.Matrix())
                    {
                        transform.Scale(scale, scale, MatrixOrder.Append);
                        graphics.SetClip(new System.Drawing.Rectangle(0, 0, width, height));
                        graphics.Transform = transform;
                        graphics.DrawImage(image, 0, 0, image.Width, image.Height);


                        ImageCodecInfo Info = getEncoderInfo("image/jpeg");
                        EncoderParameters Params = new EncoderParameters(1);
                        Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 66L);
                        SaveImage(path, thumb, Info, Params);
                        #endregion
                    }
                }
            }
        }

        /// <summary>
        /// Zdrojová metoda musí zavolat A2.Dispose nebo vložit vytváření A2 do Using klazule
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumb"></param>
        /// <param name="mime"></param>
        /// <param name="Params"></param>
        public static void SaveImage(string path, Image thumb, string mime, EncoderParameters Params)
        {
            SaveImage(path, thumb, getEncoderInfo(mime), Params);

        }

        /// <summary>        
        /// Zdrojová metoda musí zavolat A2.Dispose nebo vložit vytváření A2 do Using klazule
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumb"></param>
        /// <param name="Info"></param>
        /// <param name="Params"></param>
        private static void SaveImage(string path, Image thumb, ImageCodecInfo Info, EncoderParameters Params)
        {
            using (System.IO.MemoryStream mss = new System.IO.MemoryStream())
            {
                thumb.Save(mss, Info, Params);
                sunamo.FS.SaveMemoryStream(mss, path);
                //thumb.Dispose();
            }
        }

        /// <summary>
        /// Tato metoda(alespoň když ukládá do jpeg) všechno nastavuje na maximum i kvalitu a tak produkuje v případě malých obrázků stejně kvalitní při vyšší velikosti
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumb"></param>
        /// <param name="imageFormat"></param>
        public static void SaveImage(string path, Image thumb, ImageFormat imageFormat)
        {
            System.IO.MemoryStream mss = new System.IO.MemoryStream();
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);

            thumb.Save(mss, imageFormat);
            byte[] matriz = mss.ToArray();
            fs.Write(matriz, 0, matriz.Length);

            mss.Close();
            fs.Close();
        }

        /// <summary>
        /// Samotna M ktera zmensuje obrazek.
        /// Používá jinou metodu zmenšování pro jpeg a jinou pro ostatní typy.
        /// Nezapomeň poté co obrázek už nebudeš potřebovat jej ručně zlikvidovat metodou Dispose.
        /// Ä7 zda 
        /// </summary>
        /// <param name="strImageSrcPath"></param>
        /// <param name="strImageDesPath"></param>
        /// <param name="intWidth"></param>
        /// <param name="intHeight"></param>
        /// <returns></returns>
        public static Image ImageResize(Image image, int intWidth, int intHeight, ImageFormats imgsf)
        {
            Bitmap objImage = new Bitmap(image);

            if (intWidth > objImage.Width) intWidth = objImage.Width;
            if (intHeight > objImage.Height) intHeight = objImage.Height;
            if (intWidth == 0 & intHeight == 0)
            {
                intWidth = objImage.Width;
                intHeight = objImage.Height;
            }
            else if (intHeight == 0 & intWidth != 0)
            {
                intHeight = objImage.Height * intWidth / objImage.Width;
            }
            else if (intWidth == 0 & intHeight != 0)
            {
                intWidth = objImage.Width * intHeight / objImage.Height;
            }
            Image imgOutput = null;
            switch (imgsf)
            {
                case ImageFormats.Jpg:
                    System.Drawing.Size size = new System.Drawing.Size(intWidth, intHeight);
                    imgOutput = resizeImage(objImage, size);
                    break;
                case ImageFormats.Png:
                case ImageFormats.Gif:
                    imgOutput = objImage.GetThumbnailImage(intWidth, intHeight, null, IntPtr.Zero);
                    break;
                default:
                    break;
            }
            return imgOutput;
        }

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

        private static WriteableBitmap MakeWriteableBitmapTransparent(BitmapSource bi, PixelColor trans,  PixelColor white2)
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
        /// <returns></returns>
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
                d1 = ( decodePixelWidth / (double)ims.PixelWidth) / 1;
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

        public static void saveJpeg(string path, Image img, long quality)
        {
            try
            {
                // Encoder parameter for image quality
                EncoderParameter qualityParam =
                   new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                // Jpeg image codec
                ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

                if (jpegCodec == null)
                    return;

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                img.Save(path, jpegCodec, encoderParams);

            }
            catch (Exception ex)
            {

            }
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        static Bitmap resizeImage(Bitmap imgToResize, System.Drawing.Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap dest = new Bitmap(size.Width, size.Height);

            // Scale the bitmap in high quality mode.
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.DrawImage(imgToResize, new Rectangle(0, 0, size.Width, size.Height), new Rectangle(0, 0, imgToResize.Width, imgToResize.Height), GraphicsUnit.Pixel);
                gr.Dispose();
            }

            // Copy original Bitmap's EXIF tags to new bitmap.
            foreach (PropertyItem propertyItem in imgToResize.PropertyItems)
            {
                dest.SetPropertyItem(propertyItem);
            }

            imgToResize.Dispose();
            return dest;
        }

        /// <summary>
        /// Funguje spolehlivě jen na obrázky typu png nebo gif a měla by i na obrázky které se nenačítali z disku
        /// Nezapomeň poté co obrázek už nebudeš potřebovat jej ručně zlikvidovat metodou Dispose.
        /// Protože nastavuje ImageFormats na Gif, zmemšuje metodou Image.GetThumbnailImage která je silně zvětšuje
        /// </summary>
        /// <param name="image"></param>
        /// <param name="intWidth"></param>
        /// <param name="intHeight"></param>
        /// <returns></returns>
        public static Image ImageResize(Image image, int intWidth, int intHeight)
        {
            // Png nebo gif zmenšuje metodou Image.GetThumbnailImage
            return ImageResize(image, intWidth, intHeight, ImageFormats.Gif);
        }

        #region Commented
        #endregion

        /// <summary>
        /// Pokud A5 a zdroj nebude plně vyplňovat výstup, vrátím Point.Empty
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="finalWidth"></param>
        /// <param name="finalHeight"></param>
        /// <returns></returns>
        public static System.Drawing.Point CalculateForCrop(double w, double h, double finalWidth, double finalHeight, bool sourceMustFullFillRequiredSize)
        {
            if (w < finalWidth && sourceMustFullFillRequiredSize)
            {
                return System.Drawing.Point.Empty;
            }

            if (h < finalHeight && sourceMustFullFillRequiredSize)
            {
                return System.Drawing.Point.Empty;
            }

            double leftRight = w - finalWidth;
            double left = 0;
            if (leftRight != 0)
            {
                left = leftRight / 2d;
            }

            double topBottom = h - finalHeight;
            double top = 0;
            if (topBottom != 0)
            {
                top = topBottom / 2d;
            }

            return new System.Drawing.Point(Convert.ToInt32(left), Convert.ToInt32(top));
        }

        #region Již v CreateW10AppGraphics - několik PlaceToCenter metod
        /// <summary>
        /// Funguje naprosto správně, už nic neměnit
        /// </summary>
        /// <param name="bi"></param>
        /// <param name="trans"></param>
        /// <param name="white2"></param>
        /// <returns></returns>
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
                    bool b1 = ColorHelper.IsColorSame(first, pxsi);
                    bool b2 = pxsi.Alpha < 254;
                    if (b1)
                    {
                        pxs[i, y] = trans;
                    }
                    else
                    {
                        //DebugLogger.Instance.Write(pxsi.Alpha + "-" + pxsi.Red + "-" + pxsi.Green + "-" + pxsi.Blue);
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

                bi = shared.Pictures.PlaceToCenter(wb, wb.Width, wb.Height, false, 0, 0, arg, img2, true);

            }
            else
            {
                bi = shared.Pictures.PlaceToCenter(wb, wb.Width, wb.Height, false, minimalWidthPadding / 2, minimalHeightPadding / 2, arg, img2, useAtA1PixelSize);
            }

            //return PlaceToCenterExactly(img, args, width, height, i, temp, writeToConsole, minimalWidthPadding, minimalHeightPadding);
            return bi;
        }

        #region PlaceToCenter metody - využívající WPF třídu BitmapSource kterou vrací
        private static BitmapSource PlaceToCenter(BitmapSource img, double width, double height, bool writeToConsole, double minimalWidthPadding, double minimalHeightPadding, string arg, BitmapSource bmp2, bool useAtA1PixelSize = false)
        {

            string fnOri = Path.GetFileName(arg);
            string ext = "";
            if (sunamo.Pictures.GetImageFormatFromExtension1(fnOri, out ext))
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
                    SuccessfullyResized(Path.GetFileName(arg));
                }

                return CreateBitmapSourceAndDrawOpacity(img.PixelWidth, img.PixelHeight, bmp2, y, x, useAtA1PixelSize);
                #endregion

            }
            else
            {
                if (writeToConsole)
                {
                    FileHasWrongExtension(Path.GetFileName(arg));
                }
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
                    //vr = shared.Pictures.PlaceToCenterExactly(width, height, false, paddingLeftRight, paddingTopBottom, bi, ratioW, ratioH, true);
                    vr = Pictures.ImageResize(bi, width, height, 0, 0, sunamo.Pictures.GetImageFormatsFromExtension(bi), true);
                    vr = CreateBitmapSource(vr.PixelWidth, vr.PixelHeight, paddingLeftRight, paddingTopBottom, bi, vr, true);
                }
                else if (path.Contains("targetsize"))
                {
                    vr = shared.Pictures.PlaceToCenterExactly(width, height, false, paddingLeftRight, paddingTopBottom, bi, ratioW, ratioH, false);
                }
                else
                {
                    vr = shared.Pictures.PlaceToCenterExactly(width, height, false, paddingLeftRight, paddingTopBottom, bi, ratioW, ratioH, false);
                }
            }
            else
            {


                vr = Pictures.ImageResize(bi, width, height, 0, 0, sunamo.Pictures.GetImageFormatsFromExtension(bi), true);
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
            string fnOri = ""; // Path.GetFileName(args[i]);
            double minWidthImage = width - (minimalWidthPadding);
            double minHeightImage = height - (minimalHeightPadding);
            double newWidth = width;
            double newHeight = height;
            double innerWidth = arg.image.Width;
            double innerHeight = arg.image.Height;
            var img2 = Pictures.ImageResize(arg.path, minWidthImage, minHeightImage, 0, 0, sunamo.Pictures.GetImageFormatsFromExtension(arg.path), useAtA1PixelSize);
            BitmapSource bi = null;
            //bi = img2;
            if (true && img2 != null)
            {
                bi = CreateBitmapSource(width, height, minimalWidthPadding, minimalHeightPadding, arg.path, img2, useAtA1PixelSize);
            }
            else
            {
                if (writeToConsole)
                {
                    FileHasWrongExtension(fnOri);
                }
            }
            return bi;
        } 
        #endregion
        #endregion

        #region Další PlaceToCenter metody - Používají WF třídu Image kterou ihned ukládají na disk a nevrací
        public static bool PlaceToCenter(Image img, string ext, int width, int height, string finalPath, bool writeToConsole)
        {
            string fnOri = "";

            //string ext = "";
            if (true) //Pictures.GetImageFormatFromExtension1(fnOri, out ext))
            {

                float minWidthImage = width;
                float newWidth = img.Width;
                float newHeight = img.Height;
                while (newWidth > minWidthImage)
                {
                    newWidth *= .9f;
                    newHeight *= .9f;
                }
                while (newHeight > height)
                {
                    newWidth *= .9f;
                    newHeight *= .9f;
                }
                float y = (height - newHeight) / 2f;
                float x = (width - newWidth) / 2f;
                string temp = finalPath;
                //img = Pictures.ImageResize(img, (int)newWidth, (int)newHeight, Pictures.GetImageFormatsFromExtension2(ext));
                if (img != null)
                {
                    Bitmap bmp = new Bitmap(512, 384);
                    Graphics dc = Graphics.FromImage(bmp);
                    dc.Clear(System.Drawing.Color.Transparent);
                    var p = new System.Drawing.RectangleF(new PointF(x, y), new SizeF(newWidth, newHeight));
                    dc.DrawImage(img, p);
                    img.Dispose();

                    bmp.Save(finalPath, ImageFormat.Jpeg);
                }
                else
                {
                    if (writeToConsole)
                    {
                        FileHasWrongExtension(fnOri);
                    }
                }
                //}
            }
            else
            {
                if (writeToConsole)
                {
                    FileHasWrongExtension(ext);
                }
            }
            return false;
        }

        /// <summary>
        /// A1 je obrázek do kterého se zmenšuje
        /// 
        /// A2, A3 jsou délky stran cílového obrázku
        /// A4 je index k A2
        /// A5 je cesta do které se uloží finální obrázek
        /// A6 je zda se má ukládat na konzoli
        /// A7 jsou cesty k obrázkům, které chci zmenšit. To která cesta se použije rozhoduje index A5
        /// </summary>
        /// <param name="img"></param>
        /// <param name="args"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="i"></param>
        /// <param name="finalPath"></param>
        /// <param name="writeToConsole"></param>
        /// <returns></returns>
        public static bool PlaceToCenter(Image img, int width, int height, int i, string finalPath, bool writeToConsole, float minimalWidthPadding, float minimalHeightPadding, params string[] args)
        {
            string arg = args[i];
            Image imgArg = System.Drawing.Image.FromFile(arg);
            return PlaceToCenter(img, width, height, finalPath, writeToConsole, minimalWidthPadding, minimalHeightPadding, arg, imgArg);
        }

        private static bool PlaceToCenter(Image img, int width, int height, string finalPath, bool writeToConsole, float minimalWidthPadding, float minimalHeightPadding, string arg, Image imgArg)
        {
            string fnOri = Path.GetFileName(arg);
            string ext = "";
            if (sunamo.Pictures.GetImageFormatFromExtension1(fnOri, out ext))
            {
                float y = (height - img.Height);
                float x = (width - img.Width);
                // Prvně si já ověřím zda obrázek je delší než šířka aby to nebylo kostkované
                if (y >= 0 && x >= 0)
                {
                    #region MyRegion

                    Bitmap bmp2 = (Bitmap)imgArg; //new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    float w = 0;
                    float h = 0;
                    w = (float)img.Width;
                    h = (float)img.Height;
                    while (w > width && h > height)
                    {
                        w *= .9f;
                        h *= .9f;
                    }
                    int minimalHeightPadding2 = (int)(minimalHeightPadding * 2);
                    int minimalWidthPadding2 = (int)(minimalWidthPadding * 2);
                    if (minimalHeightPadding2 + imgArg.Height < img.Height)
                    {
                        y = ((img.Height - imgArg.Height) / 2);
                    }
                    if (minimalWidthPadding2 + imgArg.Width < img.Width)
                    {
                        x = ((img.Width - imgArg.Width) / 2);
                    }

                    Graphics g = Graphics.FromImage(img);
                    g.DrawImage(imgArg, new Rectangle((int)x, (int)y, imgArg.Width, imgArg.Height));
                    g.Flush();

                    //g.Save();
                    string temp = finalPath;



                    Pictures.SaveImage(temp, img, Pictures.GetImageFormatFromExtension2(ext));
                    img.Dispose();
                    if (writeToConsole)
                    {
                        SuccessfullyResized(Path.GetFileName(temp));
                    }
                    #endregion
                }
                else
                {
                    // OK, já teď potřebuji zjistit na jakou velikost mám tento obrázek zmenšit
                    float minWidthImage = width / 2;
                    float minHeightImage = height / 2;
                    float newWidth = width;
                    float newHeight = height;
                    while (newWidth > minWidthImage || newHeight > minHeightImage)
                    {
                        newWidth *= .9f;
                        newHeight *= .9f;
                    }
                    string temp = finalPath;
                    imgArg = Pictures.ImageResize(img, (int)newWidth, (int)newHeight, sunamo.Pictures.GetImageFormatsFromExtension(arg));
                    if (imgArg != null)
                    {

                        return PlaceToCenter(new Bitmap((int)newWidth, (int)newHeight), width, height, temp, writeToConsole, minimalWidthPadding, minimalHeightPadding, arg, imgArg);
                    }
                    else
                    {
                        if (writeToConsole)
                        {
                            FileHasWrongExtension(fnOri);
                        }
                    }
                }
            }
            else
            {
                if (writeToConsole)
                {
                    FileHasWrongExtension(Path.GetFileName(arg));
                }
            }
            return false;
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
        public static void PlaceToCenterExactly(Image img, int width, int height, int i, string finalPath, bool writeToConsole, float minimalWidthPadding, float minimalHeightPadding, params ImageWithPath[] args)
        {
            string fnOri = ""; // Path.GetFileName(args[i]);
            minimalWidthPadding *= 2;
            minimalHeightPadding *= 2;
            float minWidthImage = width - (minimalWidthPadding);
            float minHeightImage = height - (minimalHeightPadding);
            float newWidth = width;
            float newHeight = height;
            int newWidth2 = width;
            int newHeight2 = height;
            if (img == null)
            {
                img = new Bitmap(width, height);
            }
            Graphics g = Graphics.FromImage(img);
            g.Clear(System.Drawing.Color.Transparent);
            g.Flush();
            float innerWidth = args[i].image.Width;
            float innerHeight = args[i].image.Height;

            #region MyRegion
            #endregion

            while (innerHeight + minimalHeightPadding > img.Height || innerWidth + minimalWidthPadding > img.Width)
            {
                float p1h = innerHeight * 0.01f;
                innerHeight -= p1h;
                float p1w = innerWidth * 0.01f;
                innerWidth -= p1w;
            }


            string temp = finalPath;
            System.Drawing.Image img2 = Pictures.ImageResize(args[i].image, (int)innerWidth, (int)innerHeight, sunamo.Pictures.GetImageFormatsFromExtension(args[i].path));
            if (img2 != null)
            {
                #region MyRegion
                #endregion

                Bitmap bmp = new Bitmap(img);
                img.Dispose();

                shared.Pictures.PlaceToCenter(bmp, (int)newWidth2, (int)newHeight2, finalPath, false, 0f, 0f, args[i].path, img2);

                //return PlaceToCenterExactly(img, args, width, height, i, temp, writeToConsole, minimalWidthPadding, minimalHeightPadding);
            }
            else
            {
                if (writeToConsole)
                {
                    FileHasWrongExtension(fnOri);
                }
            }
        }
        #endregion
    }
}
