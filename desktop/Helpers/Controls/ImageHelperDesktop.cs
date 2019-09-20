
using sunamo;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public partial class ImageHelperDesktop : ImageHelperBase<ImageSource, Image>
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
}