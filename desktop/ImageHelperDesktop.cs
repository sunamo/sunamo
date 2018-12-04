using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace desktop
{
    public class ImageHelperDesktop : ImageHelperBase<ImageSource, Image>
    {
        public override Image MsAppx(string relPath)
        {
            BitmapSource bs = new BitmapImage(new Uri(ImageHelper.protocol + relPath));
            return ReturnImage(bs);
        }

        public override Image MsAppx(bool disabled, AppPics appPic)
        {
            ///Subfolder/ResourceFile.xaml
            return ReturnImage(BitmapImageHelper.MsAppx(disabled, appPic));
        }

        public override Image MsAppxI(string appPic2)
        {
            BitmapSource bs = new BitmapImage(new Uri(ImageHelper.protocol + "i/" + appPic2 + ".png"));
            return ReturnImage(bs);
        }

        public override Image ReturnImage(ImageSource bs)
        {
            Image image = new Image();
            image.Stretch = Stretch.Uniform;
            image.Source = bs;
            return image;
        }

        public override Image ReturnImage(ImageSource bs, double width, double height)
        {
            Image image = new Image();
            image.Stretch = Stretch.Uniform;
            image.Source = bs;
            image.Width = width;
            image.Height = height;
            return image;
        }
    }
}
