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

namespace desktop.UserControls
{
    /// <summary>
    /// Interaction logic for EmptyUC.xaml
    /// </summary>
    public partial class EmptyUC : UserControl, IUserControl, IKeysHandler
    {
        public EmptyUC()
        {
            InitializeComponent();

            Loaded += uc_Loaded;
        }

        public string Title => "";

        public WindowWithUserControl windowWithUserControl {
            get =>
                ((IWindowOpener)Application.Current.MainWindow).windowWithUserControl;
            set => 
                 ((IWindowOpener)Application.Current.MainWindow).windowWithUserControl = value;
        }

        public bool HandleKey(KeyEventArgs e)
        {
            return false;
        }

        public void Init()
        {

        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
