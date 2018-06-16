using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace desktop.Converters
{
    public class VisibilityToBoolConverter
    {
        static Type type = typeof(VisibilityToBoolConverter);

        public static bool ConvertTo(Visibility visibility)
        {
            switch (visibility)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Hidden:
                case Visibility.Collapsed:
                    break;
                default:
                    ThrowExceptions.NotImplementedCase(type, "ConvertTo");
                    break;
            }
            return false;
        }

        public static Visibility ConvertFrom(bool visibility)
        {
            return visibility ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
