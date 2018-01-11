using sunamo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;


class DrawingImageHelper
    {
        

        /// <summary>
        /// Do A1 se vkládá člen výčtu AppPics2.TS()
        /// Přípona se doplní automaticky na .png
        /// </summary>
        /// <param name="appPic2"></param>
        /// <returns></returns>
        public static Image MsAppxI(string appPic2)
        {
            BitmapImage bs = new BitmapImage(new Uri(ImageHelper.protocol + "i/" + appPic2 + ".png"));
            return ReturnImage(bs);
        }

        public static Image MsAppx(string relPath)
        {
            BitmapImage bs = new BitmapImage(new Uri(ImageHelper.protocol + relPath));
            return ReturnImage(bs);
        }

        public static Image ReturnImage(BitmapImage bs)
        {
            Bitmap bmp = new Bitmap(bs.StreamSource);

            return bmp;
        }

        public static Image MsAppx(bool disabled, AppPics appPic)
        {
            ///Subfolder/ResourceFile.xaml
            return ReturnImage(BitmapImageHelper.MsAppx(disabled, appPic));
        }
    }

