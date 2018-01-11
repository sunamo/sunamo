using System.Windows;
using System.Windows.Media;
/// <summary>
/// Posloupnost je BitmapImage (sealed) -> BitmapSource (abstract) -> ImageSource (abstract)
/// </summary>
using System.Windows.Media.Imaging;
public static class ImageSourceHelper
{
    /// <summary>
    /// A1 jde v pohodě přetypovat na BitmapSource nebo ImageSource protože od nich dědí ale nikoliv na BitmapImage
    /// Do A4 zadej 0 pokud chceš aby obrázek vyplňoval celou šířku
    /// </summary>
    /// <param name="source"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="margin"></param>
    /// <returns></returns>
    public static BitmapFrame CreateResizedImage(BitmapSource source, double width, double height, double margin)
    {
        var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

        var group = new DrawingGroup();
        RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
        group.Children.Add(new ImageDrawing(source, rect));

        var drawingVisual = new DrawingVisual();
        using (var drawingContext = drawingVisual.RenderOpen())
            drawingContext.DrawDrawing(group);

        var resizedImage = new RenderTargetBitmap(
            (int)width, (int)height,         // Resized dimensions
            source.DpiX, source.DpiY,                // Default DPI values
            PixelFormats.Default); // Default pixel format
        resizedImage.Render(drawingVisual);

        return BitmapFrame.Create(resizedImage);
    }

    

    public static BitmapFrame CropImage(Point point, Size size, BitmapImage bi)
    {
        // bi je BitmapImage obrázek ke výřezu, point je bod od kterého se vyřezává, size je velikost která se vyřezává
        if (bi.DpiX != 96)
        {
            size.Width /= 96d;
            size.Width *= bi.DpiX;
            point.X /= 96d;
            point.X *= bi.DpiX;
        }
        if (bi.DpiY != 96)
        {
            size.Height /= 96d;
            size.Height *= bi.DpiY;
            point.Y /= 96d;
            point.Y *= bi.DpiY;
        }

        // Samotná operace výřezu
        return BitmapFrame.Create( new CroppedBitmap(bi, new Int32Rect((int)point.X, (int)point.Y, (int)size.Width, (int)size.Height)));
    }
}
