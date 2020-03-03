using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SystemWindowsMediaColorExtensions
{
    public static System.Drawing.Color ToSystemDrawing(this System.Windows.Media.Color c)
    {
        return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
    }
}