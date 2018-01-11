using sunamo.Data;
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
    /// Interaction logic for DeleteDuplicitiesFilesWrapper.xaml
    /// </summary>
    public partial class DeleteDuplicitiesFilesWrapper : UserControl
    {
        public DeleteDuplicitiesFilesWrapper()
        {
            InitializeComponent();
        }

        public ISelectFromMany<TWithSizeInString<string>> PathWithSize
        {
            set
            {
                UIElement uie = (UIElement)value;
                Grid.SetRow(uie, 1);
                grid.Children.Add(uie);
            }
        }
    }
}
