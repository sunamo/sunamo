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
    /// Interaction logic for MultiLineTextBlock.xaml
    /// </summary>
    public partial class MultiLineTextBlock : UserControl
    {
        public MultiLineTextBlock()
        {
            InitializeComponent();


        }

        public void AddLines(Brush bg, Brush fg, params string[] lines)
        {
            foreach (var item in lines)
            {
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Stretch;
                tb.Background = bg;
                tb.Foreground = fg;
                tb.Text = item;
                spLines.Children.Add(tb);
            }
        }
    }
}
