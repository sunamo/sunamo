using sunamo;
using System.Windows.Controls;
using System.Windows.Media;

public partial class ImageHelperDesktop : ImageHelperBase<ImageSource, Image>
{ 
public static Image Get(string imagePath)
    {
        Image img = ImageHelper.ReturnImage(BitmapImageHelper.PathToBitmapImage(imagePath));
        return img;
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