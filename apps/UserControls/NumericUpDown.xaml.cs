using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps
{
    public sealed partial class NumericUpDown : UserControl
    {
        public event ValueChangedRoutedHandler<uint> ValueChanged;

        public NumericUpDown()
        {
            this.InitializeComponent();
        }

        string latest = "";

        public int Value
        {
            get
            {
                //return 0;
                return int.Parse(txtValue.Text);
            }
            set
            {
                txtValue.Text = value.ToString();
                latest = txtValue.Text;
                if (PropertyChanged != null)
                {
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new ValueChangedRoutedEventArgs<uint>((uint)value));
                    }
                    
                        PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                    
                    
                }
            }
        }

        private void btnIncrement_Click_1(object sender, RoutedEventArgs e)
        {
            if (Value != int.MaxValue)
            {
                Value++;
            }
            
        }

        private void btnDecrement_Click_1(object sender, RoutedEventArgs e)
        {
            if (Value != 0)
            {
                Value--;
            }
            
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(NumericUpDown), new PropertyMetadata(100));

        public event PropertyChangedEventHandler PropertyChanged;


        private void txtValue_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            uint nv = 0;
            if (uint.TryParse(txtValue.Text, out nv))
            {
                if (nv > 1000)
                {
                    txtValue.Text = latest;
                }
                else
                {
                    latest = txtValue.Text;
                    if (ValueChanged != null)
                    {
                        ValueChanged(sender, new ValueChangedRoutedEventArgs<uint>(nv));
                    }
                }
            }
            else
            {
                txtValue.Text = latest;
            }
        }
    }
}
