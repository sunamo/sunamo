using sunamo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps
{
    public sealed partial class ColorPicker : UserControl
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
            htmlColor.Text = StringHexColorConverter.ConvertTo(value);
            rectColor.Fill = scb;
            ColorChanged(result);

            RSlider.BorderBrush = GSlider.BorderBrush = BSlider.BorderBrush = ASlider.BorderBrush = scb;
            //return value;
        }

        public event VoidColor ColorChanged;

        public ColorPicker()
        {
            this.InitializeComponent();

                ATextBlock.Text = RL.GetString( "TransparencyColon");
                RTextBlock.Text = RL.GetString( "RedColorComponentColon");
                GTextBlock.Text = RL.GetString("GreenColorComponentColon");
                BTextBlock.Text = RL.GetString("BlueColorComponentColon");
            
            //ASlider.Value = 255;            
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
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
                        throw new Exception("Bad property value Name of Slider A1");
                }
                rectColor.Fill = new SolidColorBrush(result);
                SetColor(result);
                //ColorChanged(result);
            }
        }

        private void htmlColor_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                Result = StringHexColorConverter.ConvertFrom(htmlColor.Text);
            }
            
        }

        private void htmlColor_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
            
        }
    }
}
