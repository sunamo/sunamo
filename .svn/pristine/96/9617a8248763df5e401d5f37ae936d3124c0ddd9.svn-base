
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    public sealed partial class NumericUpDown : UserControl
    {
        public event ValueChangedRoutedHandler<uint> ValueChanged;

        public NumericUpDown()
        {
            this.InitializeComponent();
            
        }

        string latest = "";

        int max = int.MaxValue;
        public int Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                FrameworkElementHelper.SetAll3Widths(txtValue, TextBoxHelper.GetOptimalWidthForCountOfChars(value.ToString().Length, false, txtValue));
            }
        }

        public int Value
        {
            get
            {
                string val = txtValue.GetValue(TextBox.TextProperty).ToString();
                if (val != "")
                {
                    return int.Parse(val);
                }
                return 0;
                
            }
            set
            {
                //SetValue(ValueProperty, value);
                txtValue.SetValue(TextBox.TextProperty, value.ToString());
                //txtValue.Text = value.ToString();
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
            if (Value != Max)
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
