using apps.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps.AwesomeFont
{
    /// <summary>
    /// Obsahuje metody pro přiřazení awesome font ikon různým controlům
    /// </summary>
    public static class AwesomeFontControls
    {
        public const string awesomeFontPath = "/Fonts/FontAwesome.otf#FontAwesome";
        
        /// <summary>
        /// A1 = Math.Min(WidthOfParent, HeightOfParent)
        /// </summary>
        /// <returns></returns>
        public static double CalculateFontSize(double woh)
        {
            return (woh / 2) - 6;
        }

        public async static Task SetAwesomeFontSymbol(TextBlock txtSearchIcon, string otf)
        {
            await ThisApp.cd.RunAsync(ThisApp.cdp, () =>
            {
                txtSearchIcon.FontFamily = new FontFamily(awesomeFontPath);
                txtSearchIcon.Text = otf;
                
            });
        }

        public async static Task SetAwesomeFontSymbol(TextBlock txtSearchIcon, string otf, Brush fg, double fontSize, string tooltip)
        {
            await ThisApp.cd.RunAsync(ThisApp.cdp, () =>
            {
                txtSearchIcon.FontFamily = new FontFamily(awesomeFontPath);
                txtSearchIcon.Text = otf;
                txtSearchIcon.Foreground = fg;
                txtSearchIcon.FontSize = fontSize;
                
            });
        }

        public static double CalculateFontSize(double widthOfIconControl, double heightOfIconControl)
        {
            return CalculateFontSize(Math.Min(widthOfIconControl, heightOfIconControl));
        }

        public async static Task SetAwesomeFontSymbol(ContentControl txtSearchIcon, string otf)
        {
            await ThisApp.cd.RunAsync(ThisApp.cdp, () =>
            {
                txtSearchIcon.FontFamily = new FontFamily(awesomeFontPath);
                txtSearchIcon.Content = otf;
            });
        }

        public async static Task SetAwesomeFontSymbolAsContent(ContentControl txtSearchIcon, string otf, double fontSize)
        {
            await ThisApp.cd.RunAsync(ThisApp.cdp, () =>
            {
                FontIcon fi = new FontIcon();
                fi.FontFamily = new FontFamily(awesomeFontPath);
                fi.FontSize = fontSize;
                fi.Glyph = otf;

                txtSearchIcon.VerticalContentAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                txtSearchIcon.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                txtSearchIcon.Content = fi;
            });
        }
    }
}
