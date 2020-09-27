using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        Color result = new Color();
        public Color Result
        {
            get
            {
                return result;
            }
            set
            {
                RSlider.Value = value.R;
                GSlider.Value = value.G;
                BSlider.Value = value.B;
                ASlider.Value = value.A;
                result = value;
                SetColor(value);
            }
        }

        private void SetColor(Color value)
        {
            SolidColorBrush scb = new SolidColorBrush(value);
            htmlColor.Text = StringHexWindowsMediaColorConverter.ConvertTo(value);
            rectColor.Fill = scb;
            ColorChanged(result);

            RSlider.BorderBrush = GSlider.BorderBrush = BSlider.BorderBrush = ASlider.BorderBrush = scb;
            //return value;
        }

        public event VoidWpfColor ColorChanged;

        public ColorPicker()
        {
            this.InitializeComponent();

            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
            {
                ATextBlock.Text = sess.i18n(XlfKeys.Transparency) + ":";
                RTextBlock.Text = sess.i18n(XlfKeys.RedColoredFolder) + ":";
                GTextBlock.Text = sess.i18n(XlfKeys.GreenColorFolder) + ":";
                BTextBlock.Text = sess.i18n(XlfKeys.BlueColorFolder) + ":";
            }
            else
            {
                ATextBlock.Text = sess.i18n(XlfKeys.Opacity) + ":";
                RTextBlock.Text = sess.i18n(XlfKeys.RedColorComponent) + ":";
                GTextBlock.Text = sess.i18n(XlfKeys.GreenColorComponent) + ":";
                BTextBlock.Text = sess.i18n(XlfKeys.BlueColorComponent) + ":";
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rectColor != null)
            {
                Slider s = (sender as Slider);
                string name = s.Name;
                byte value = (byte)s.Value;
                switch (name)
                {
                    case "RSlider":
                        result.R = value;
                        break;
                    case "GSlider":
                        result.G = value;
                        break;
                    case "BSlider":
                        result.B = value;
                        break;
                    case "ASlider":
                        result.A = value;
                        break;
                    default:
                        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"");
                        break;
                }
                rectColor.Fill = new SolidColorBrush(result);
                SetColor(result);
                //ColorChanged(result);
            }
        }

        static Type type = typeof(ColorPicker);

        private void htmlColor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Result = StringHexWindowsMediaColorConverter.ConvertFrom(htmlColor.Text);
            }
        }
    }
}