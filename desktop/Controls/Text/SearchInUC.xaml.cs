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

namespace desktop.Controls.Text
{
    /// <summary>
    /// Interaction logic for SearchInUC.xaml
    /// </summary>
    public partial class SearchInUC : UserControl
    {
        public SearchInUC()
        {
            InitializeComponent();

            Loaded += SearchInUC_Loaded;
        }

        private void SearchInUC_Loaded(object sender, RoutedEventArgs e)
        {
            cbSearchInContentUC.Name = Name + "Cb";
            chbSearchInContent.Name = Name + "Chb";

            chbSearchInContent.Checked += ChbSearchInContent_Checked;
        }

        private void ChbSearchInContent_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        public Brush BorderBrush
        {
            set
            {
                b.BorderBrush = value;
            }
        }
    }
}