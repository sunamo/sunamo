
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

using System.Windows;
using sunamo;
using sunamo.Data;
using System.Drawing;
using sunamo.Essential;


    public class PicturesShared
    {
    static Type type = typeof(PicturesShared);
    private static Regex r = new Regex(AllStrings.colon);
    public static Bitmap RotateBitmap(Image bitmap)
    {
        var r = RandomHelper.RandomInt(0, 45);
        r -= RandomHelper.RandomInt(0, 90);
        return RotateBitmap(bitmap, (float)r);
    }

    public static Bitmap RotateBitmap(Image bitmap, float angle)
    {
        int w, h, x, y;
        var dW = (double)bitmap.Width;
        var dH = (double)bitmap.Height;

        double degrees = Math.Abs(angle);
        if (degrees <= 90)
        {
            double radians = 0.0174532925 * degrees;
            double dSin = Math.Sin(radians);
            double dCos = Math.Cos(radians);
            w = (int)(dH * dSin + dW * dCos);
            h = (int)(dW * dSin + dH * dCos);
            x = (w - bitmap.Width) / 2;
            y = (h - bitmap.Height) / 2;
        }
        else
        {
            degrees -= 90;
            double radians = 0.0174532925 * degrees;
            double dSin = Math.Sin(radians);
            double dCos = Math.Cos(radians);
            w = (int)(dW * dSin + dH * dCos);
            h = (int)(dH * dSin + dW * dCos);
            x = (w - bitmap.Width) / 2;
            y = (h - bitmap.Height) / 2;
        }

        var rotateAtX = bitmap.Width / 2f;
        var rotateAtY = bitmap.Height / 2f;

        var bmpRet = new Bitmap(w, h);
        bmpRet.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
        using (var graphics = Graphics.FromImage(bmpRet))
        {

            graphics.Clear(Color.Transparent);
            graphics.TranslateTransform(rotateAtX + x, rotateAtY + y);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-rotateAtX - x, -rotateAtY - y);
            graphics.DrawImage(bitmap, new PointF(0 + x, 0 + y));
        }
        return bmpRet;
    }

    public static object ImageToBase64(string imageFile)
    {
        return ImageToBase64(Bitmap.FromFile(imageFile), PicturesShared.GetImageFormatFromExtension2(FS.GetExtension(imageFile)));
    }

    public static void ChangeResolution(string path, float dpix, float dpiy)
    {
        Bitmap OriginalBitmap = new Bitmap(path);
        int old_wid = OriginalBitmap.Width;
        int old_hgt = OriginalBitmap.Height;
        int new_wid = old_wid;
        int new_hgt = new_wid;

        using (Bitmap bm = new Bitmap(new_wid, new_hgt))
        {
            System.Drawing.Point[] points =
            {
                new System.Drawing.Point(0, 0),
                new System.Drawing.Point(new_wid, 0),
                new System.Drawing.Point(0, new_hgt),
            };
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(OriginalBitmap, points);
            }
            bm.SetResolution(dpix, dpiy);
            SaveImage(FS.ChangeExtension( path, ".jpg", false), bm, GetImageFormatFromExtension2(FS.GetExtension(path)));
        }

    }

    public static ImageFormat GetImageFormatFromExtension2(string ext)
		{
        ext = ext.TrimStart(AllChars.dot);
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

		

       

        public static string ImageToBase64(string path, ImageFormat jpeg, out int width, out int height)
        {

            if (FS.ExistsFile(path))
            {
                Image imgo = Image.FromFile(path);
                width = imgo.Width;
                height = imgo.Height;
                return PicturesShared.ImageToBase64(imgo, jpeg);
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
                        string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), AllStrings.dash, 2);
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
                FS.SaveMemoryStream(mss, path);
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
        
		public static bool PlaceToCenter(Image img, int width, int height, int i, string finalPath, bool writeToConsole, float minimalWidthPadding, float minimalHeightPadding, params string[] args)
        {
            string arg = args[i];
            Image imgArg = System.Drawing.Image.FromFile(arg);
            return PlaceToCenter(img, width, height, finalPath, writeToConsole, minimalWidthPadding, minimalHeightPadding, arg, imgArg);
        }

        private static bool PlaceToCenter(Image img, int width, int height, string finalPath, bool writeToConsole, float minimalWidthPadding, float minimalHeightPadding, string arg, Image imgArg)
        {
            string fnOri = FS.GetFileName(arg);
            string ext = "";
            if (PicturesSunamo.GetImageFormatFromExtension1(fnOri, out ext))
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

                    PicturesShared.SaveImage(temp, img, PicturesShared.GetImageFormatFromExtension2(ext));
                    img.Dispose();
                    if (writeToConsole)
                    {
                        InitApp.TemplateLogger.SuccessfullyResized(FS.GetFileName(temp));
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
                    imgArg = PicturesShared.ImageResize(img, (int)newWidth, (int)newHeight, PicturesSunamo.GetImageFormatsFromExtension(arg));
                    if (imgArg != null)
                    {

                        return PlaceToCenter(new Bitmap((int)newWidth, (int)newHeight), width, height, temp, writeToConsole, minimalWidthPadding, minimalHeightPadding, arg, imgArg);
                    }
                    else
                    {
                    ThrowExceptions.FileHasExtensionNotParseableToImageFormat(type, RH.CallingMethod(), fnOri);
                }
                }
            }
            else
            {
            ThrowExceptions.FileHasExtensionNotParseableToImageFormat(type, RH.CallingMethod(), fnOri);
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
            string fnOri = ""; // FS.GetFileName(args[i]);
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
            System.Drawing.Image img2 = PicturesShared.ImageResize(args[i].image, (int)innerWidth, (int)innerHeight, PicturesSunamo.GetImageFormatsFromExtension(args[i].path));
            if (img2 != null)
            {
                #region MyRegion
                #endregion

                Bitmap bmp = new Bitmap(img);
                img.Dispose();

                PicturesShared.PlaceToCenter(bmp, (int)newWidth2, (int)newHeight2, finalPath, false, 0f, 0f, args[i].path, img2);

                //return PlaceToCenterExactly(img, args, width, height, i, temp, writeToConsole, minimalWidthPadding, minimalHeightPadding);
            }
            else
            {
            ThrowExceptions.FileHasExtensionNotParseableToImageFormat(type, RH.CallingMethod(), fnOri);
        }
        }
        #endregion
    }
