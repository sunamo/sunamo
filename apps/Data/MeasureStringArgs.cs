using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Text;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class MeasureStringArgs : FontArgs
    {
        public MeasureStringArgs(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontStretch fontStretch, FontWeight fontWeight, string text)
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
