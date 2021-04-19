using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SystemWindowsMediaSolidColorBrushExtensions
{
    public static System.Drawing.Brush ToSystemDrawing(this System.Windows.Media.SolidColorBrush c2)
    {
        var c = c2.Color;
        return new System.Drawing.SolidBrush(Color.FromArgb( c.A, c.R, c.G, c.B));
    }
}