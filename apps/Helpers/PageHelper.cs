using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace apps
{
    public class PageHelper
    {
        /// <summary>
        /// Pokud má být aplikace použitelná na mobilech, A1 musí být vždy True
        /// Pokud bude false, vrátí se výška i šířka 2x delší než jaká ve skutečnosti je(resp. vrátí se správná - 720x1136 ale na obrazovku se zvládne vykreslit jen 360x568)
        /// </summary>
        /// <param name="noScaleFactor"></param>
        /// <returns></returns>
        public static Size WindowSize(bool noScaleFactor)
        {
            
                var sf = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
                var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
                double width = bounds.Width;
                double height = bounds.Height;
            if (noScaleFactor)
            {
                sf = 1;
            }
                return new Size(width *sf, height *sf);
            
        }
    }
}
