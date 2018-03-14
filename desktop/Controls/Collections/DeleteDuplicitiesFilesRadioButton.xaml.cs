using sunamo.Data;
using sunamo.Helpers;
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
    /// Interaction logic for DeleteDuplicitiesFilesRadioButton.xaml
    /// </summary>
    public partial class DeleteDuplicitiesFilesRadioButton : UserControl, ISelectFromMany<TWithSizeInString<string>>
    {
        public SelectFromManyHelper<TWithSizeInString<string>> sfmh = null;

        public DeleteDuplicitiesFilesRadioButton()
        {
            InitializeComponent();

            sfmh = new SelectFromManyHelper<TWithSizeInString<string>>(this);
        }

        public void AddControl(TWithSizeInString<string> data, bool tick)
        {
            spFolders.Children.Add(ControlsGenerator.RadioButtonWithDescription(data, !sfmh.sufficientFileName, tick));
        }

        public void AddControls()
        {
            spFolders.Children.Clear();
            AddControl(new TWithSizeInString<string> { t = sfmh.defaultFileForLeave, sizeS = sfmh.defaultFileSize }, true);
            foreach (var item in sfmh.filesWithSize)
            {
                if (item.Key != sfmh.defaultFileForLeave)
                {
                    AddControl(new TWithSizeInString<string> { t = item.Key, sizeS = item.Value }, false);
                }
            }
        }
    }
}
