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


    public class PicturesDesktop
    {
    static Type type = typeof(PicturesDesktop);

    /// <summary>
    /// A1 must be BitmapSource, not ImageSource
    /// A2 was originally Colors.Magenta
    /// </summary>
    /// <param name="bs"></param>
    
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

