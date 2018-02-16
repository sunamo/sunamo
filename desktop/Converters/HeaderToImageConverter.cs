using sunamo.PInvoke;
using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace desktop.Converters
{
    //TODO: Try this, in attr are types string and bool but Convert() return ImageSource

    //[ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
        {
            public static HeaderToImageConverter Instance = new HeaderToImageConverter();

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                FileSystemEntry path = (value as FileSystemEntry);

            using (Icon i = IconExtractor.GetSmallIcon(path.path, path.file))
            {
                if (i != null)
                {
                    ImageSource img = Imaging.CreateBitmapSourceFromHIcon(
                                            i.Handle,
                                            new Int32Rect(0, 0, i.Width, i.Height),
                                            BitmapSizeOptions.FromEmptyOptions());
                    return img;
                }
            }
            return null;
        }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException("Cannot convert back");
            }
        }
    
}
