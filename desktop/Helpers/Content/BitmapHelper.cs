using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BitmapHelper
{
    public static Bitmap ChangeColor(Bitmap scrBitmap)
    {
        //You can change your new color here. Red,Green,LawnGreen any..
        Color newColor = Color.Red;
        Color actualColor;
        //make an empty bitmap the same size as scrBitmap
        Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
        for (int i = 0; i < scrBitmap.Width; i++)
        {
            for (int j = 0; j < scrBitmap.Height; j++)
            {
                //get the pixel from the scrBitmap image
                actualColor = scrBitmap.GetPixel(i, j);
                // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                if (actualColor.A > 150)
                    newBitmap.SetPixel(i, j, newColor);
                else
                    newBitmap.SetPixel(i, j, actualColor);
            }
        }
        return newBitmap;
    }

    public static Image ChangeColor2( Image image, Color fromColor, Color toColor)
    {
        ImageAttributes attributes = new ImageAttributes();
        attributes.SetRemapTable(new ColorMap[]
        {
            new ColorMap()
            {
                OldColor = fromColor,
                NewColor = toColor,
            }
        }, ColorAdjustType.Bitmap);

        using (Graphics g = Graphics.FromImage(image))
        {
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.SmoothingMode =
           SmoothingMode.AntiAlias;
            g.TextRenderingHint =
                TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode =
                InterpolationMode.High;

            g.DrawImage(
                image,
                new Rectangle(Point.Empty, image.Size),
                0, 0, image.Width, image.Height,
                GraphicsUnit.Pixel,
                attributes);
        }

        return image;
    }





}