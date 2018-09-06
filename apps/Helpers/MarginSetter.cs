using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps.Helpers
{
    public class MarginSetter
    {
        /// <summary>
        /// Pro A2 je nejlepší asi orange
        /// Výchozí pro A3 je 3, levý je 4násobek a spodní dvojnásobek, pravý vždy 0
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="topMargin"></param>
        public static void UppercaseTextBlock(TextBlock tb, Brush color, double topMargin)
        {
            tb.Text = tb.Text.ToUpper();
            tb.Margin = new Windows.UI.Xaml.Thickness(topMargin *4, topMargin, 0, topMargin * 2);
            if (color != null)
            {
                tb.Foreground = color;
            }
        }
    }
}
