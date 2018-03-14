
using sunamo;
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

namespace desktop
{
    /// <summary>
    /// Interaction logic for TextBoxOnlyNumbers.xaml
    /// </summary>
    public partial class TextBoxOnlyNumbers : UserControl
    {
        public string Text
        {
            set
            {
                txt.Text = CharHelper.OnlyDigits(value);
            }
            get
            {
                return txt.Text;
            }
        }

        public TextBoxOnlyNumbers()
        {
            InitializeComponent();
            txt.TextChanged += Txt_TextChanged;
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt.Text = CharHelper.OnlyDigits(txt.Text);
        }
    }
}
