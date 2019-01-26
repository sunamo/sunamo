
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows;
using sunamo;
using sunamo.Data;
using System.Drawing;
using sunamo.Essential;

namespace shared
{
    public class Pictures
    {
        private static Regex r = new Regex(":");

        

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
			catch
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
						Exceptions. FileHasWrongExtension(fnOri);
					}
				}
				//}
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
                        InitApp.TemplateLogger.SuccessfullyResized(Path.GetFileName(temp));
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
                            Exceptions.FileHasWrongExtension(fnOri);
                        }
                    }
                }
            }
            else
            {
                if (writeToConsole)
                {
                    Exceptions.FileHasWrongExtension(Path.GetFileName(arg));
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
                    Exceptions.FileHasWrongExtension(fnOri);
                }
            }
        }
        #endregion
    }
}
