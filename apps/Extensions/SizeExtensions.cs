using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Xaml;

namespace apps
{
    public static class SizeExtensions
    {
        public static Size RecalculateSizeWithScaleFactor(this Size s)
        {
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(s.Width * scaleFactor, s.Height * scaleFactor);
            return size;
        }

        
    }
}
