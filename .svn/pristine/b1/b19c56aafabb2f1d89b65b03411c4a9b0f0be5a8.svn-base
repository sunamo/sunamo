using desktop.Helpers;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace desktop.Data
{
    public class TextBlockData
    {
        public SolidColorBrush fg = Brushes.Black;
        public string text = "";
        public System.Windows.FontWeight fontWeight = FontWeightHelper.FromEnum(FontWeight2.normal);

        public FontWeight2 fontWeight2
        {
            set
            {
                FontWeightHelper.FromEnum(value);
            }
        }
    }

    public class TextBlockDataCompare : TextBlockData
    {
        public SolidColorBrush fg2 = Brushes.Black;
    }
}
