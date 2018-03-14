using desktop.Data;
using System;
using System.Collections.Generic;
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

namespace desktop.Controls
{
    /// <summary>
    /// Interaction logic for CompareTwoIntegers.xaml
    /// </summary>
    public partial class CompareTwoIntegers : UserControl
    {
        public CompareTwoIntegers(string whatCompare, decimal value1, decimal value2, double widthValues, bool bold)
        {
            InitializeComponent();

            tbWhatComparing.Text = whatCompare;

            if (bold)
            {
                tbWhatComparing.FontWeight = System.Windows.FontWeight.FromOpenTypeWeight(600);
            }

            Brush brush1 = Brushes.Black;
            Brush brush2 = Brushes.Black;
            string symbol = " = ";
            if (value1 > value2)
            {
                brush1 = Brushes.Green;
                brush2 = Brushes.Red;
                symbol = " > ";
            }
            else if (value2 > value1)
            {
                brush1 = Brushes.Red;
                brush2 = Brushes.Green;
                symbol = " < ";
            }

            tbValue1.Text = value1.ToString();
            tbValue1.Foreground = brush1;
            tbValue1.Width = widthValues;

            tbCompare.Text = symbol;

            tbValue2.Text = value2.ToString();
            tbValue2.Foreground = brush2;
            tbValue2.Width = widthValues;

        }

        
    }
}
