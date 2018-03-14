using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop
{
    public class TextPanel : StackPanel
    {
        public FontFamily fontFamily = new FontFamily("Segoe UI");
        public double fontSize = 12;
        public FontStyle fontStyle = FontStyles.Normal;
        /// <summary>
        /// Hodnota mezi 1-9, průměrná je 5
        /// </summary>
        public FontStretch fontStretch = FontStretch.FromOpenTypeStretch(5);
        public System.Windows.FontWeight fontWeight = System.Windows.FontWeight.FromOpenTypeWeight(500);

        public TextPanel()
        {
            Orientation = System.Windows.Controls.Orientation.Vertical;

        }

        public void H1(string text)
        {
            List<string> dd = FontHelper.DivideStringToRows(fontFamily, 50, FontStyles.Normal, fontStretch, System.Windows.FontWeight.FromOpenTypeWeight(601), text, new Size(ActualWidth, ActualHeight));
            foreach (var item in dd)
            {
                TextBlock tb = new TextBlock();
                tb.FontFamily = fontFamily;
                tb.FontSize = 50;
                tb.FontStyle = FontStyles.Normal;
                tb.FontStretch = fontStretch;
                tb.FontWeight = System.Windows.FontWeight.FromOpenTypeWeight(601);
                this.Children.Add(tb);
            }
        }
    }
}
