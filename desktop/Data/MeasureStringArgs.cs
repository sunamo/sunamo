using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace desktop
{
    public class MeasureStringArgs : FontArgs
    {
        public MeasureStringArgs(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, System.Windows.FontWeight fontWeight, string text)
        {
            this.fontFamily = fontFamily;
            this.fontSize = fontSize;
            this.fontStretch = fontStretch;
            this.fontStyle = fontStyle;
            this.fontWeight = fontWeight;
            this.text = text;
        }

        
        public string text = "";
        //Size maxSize = Constants.maxSize;
    }
}
