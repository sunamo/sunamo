using apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps.AwesomeFont
{
    /// <summary>
    /// Vrací a nastavuje ikonku FontAwesomeIcon do instance DependencyObject
    /// Pokud je font family fonts/fontawesome-webfont.ttf#FontAwesome , dává se do Text &#x a pak f09b; , dohromady tedy &#xf09b;
    /// Pokud je font family ms-appx:///Assets/FontAwesome.otf#FontAwesome , dává se do Text \u a pak f09b , dohromady tedy \uf09b (bez středníku na konci!)
    /// </summary>
    public class FontAwesome
    {
        public static FontAwesomeIcon GetSymbol(DependencyObject dp)
        {
            return (FontAwesomeIcon)dp.GetValue(SymbolProperty);
        }

        public static FontFamily FontFamily { get; set; }

        static FontAwesome()
        {
            FontFamily = new FontFamily(AwesomeFontControls.awesomeFontPath);
        }

        public static void SetSymbol(DependencyObject dp, FontAwesomeIcon value)
        {
            string hex = ((int)value).ToString("X").ToLower();
            dp.SetValue(SymbolProperty, hex);
            if (dp is Button)
            {
                dp.SetValue(Button.FontFamilyProperty, new FontFamily( "ms-appx:///Fonts/FontAwesome.otf#FontAwesome"));
                dp.SetValue(Button.ContentProperty, "" + hex);
            }
            else if (dp is TextBlock)
            {
                TextBlock dpb = (TextBlock)dp;
                
                dp.SetValue(TextBlock.FontFamilyProperty, new FontFamily("/fontawesome-webfont.ttf#FontAwesome"));
                dp.SetValue(TextBlock.TextProperty, "&#x" + hex + ";");
            }
        }

        

        

        public static readonly DependencyProperty SymbolProperty = DependencyProperty.RegisterAttached("Symbol", typeof(FontAwesomeIcon), typeof(TextBlock), new PropertyMetadata(FontAwesomeIcon.None));
    }
}
