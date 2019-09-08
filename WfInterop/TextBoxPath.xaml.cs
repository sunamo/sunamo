using sunamo.Constants;
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

namespace WfInterop
{
    /// <summary>
    /// Interaction logic for TextBoxPath.xaml
    /// </summary>
    public partial class TextBoxPath : UserControl
    {
        public TextBoxPath()
        {
            InitializeComponent();

            Loaded += TextBoxPath_Loaded;
        }

        private void TextBoxPath_Loaded(object sender, RoutedEventArgs e)
        {
            txt.Init(DefaultPaths.sczRootPath);
        }
    }
}
