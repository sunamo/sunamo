#region Mono
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
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

    #region Convert between System.Windows and System.Drawing - same name in all helper classes
    public static ImageSource ImageSourceFromBitmap2(Bitmap bmp)
    {
        MemoryStream ms = new MemoryStream();
        ((System.Drawing.Bitmap)bmp).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        BitmapImage image = new BitmapImage();
        image.BeginInit();
        ms.Seek(0, SeekOrigin.Begin);
        image.StreamSource = ms;
        image.EndInit();
        return image;
    }

    public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
    {
        var handle = bmp.GetHbitmap();
        try
        {
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        finally { W32.DeleteObject(handle); }
    } 
    #endregion

    public static BitmapFrame CropImage(System.Windows.Point point, System.Windows.Size size, BitmapImage bi)
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
		return BitmapFrame.Create(new CroppedBitmap(bi, new Int32Rect((int)point.X, (int)point.Y, (int)size.Width, (int)size.Height)));
	}
}
#endregion