using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public static class SizeExtensions
{
    public static Size RecalculateSizeWithScaleFactor(this Size s)
    {
        var scaleFactor = DisplayHelper.GetScaleFactor();
        var size = new Size(s.Width / scaleFactor, s.Height / scaleFactor);
        return size;
    }


}