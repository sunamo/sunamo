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
    /// Interaction logic for ColorPicker.xaml
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
                ATextBlock.Text = "Průhlednost:";
                RTextBlock.Text = "Červená barevná složka:";
                GTextBlock.Text = "Zelená barevná složka:";
                BTextBlock.Text = "Modrá barevná složka:";
            }
            else
            {
                ATextBlock.Text = "Opacity:";
                RTextBlock.Text = "Red color component:";
                GTextBlock.Text = "Green color component:";
                BTextBlock.Text = "Blue color component:";
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
                        throw new Exception("");
                }
                rectColor.Fill = new SolidColorBrush(result);
                SetColor(result);
                //ColorChanged(result);
            }
        }

        private void htmlColor_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

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
