using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Method which takes System.Drawing.Color
/// </summary>
public class DrawingColorHelper
{
    public static PixelColor PixelColorFromDrawingColor(System.Drawing.Color color, byte? alpha)
    {
        if (alpha == null)
        {
            alpha = color.A;
        }
        PixelColor white2 = new PixelColor() { Alpha = alpha.Value, Red = color.R, Green = color.G, Blue = color.B };
        return white2;
    }
}