using sunamo;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SelectFile.xaml
    /// </summary>
    public partial class SelectFile : UserControl
    {
        public SelectFile()
        {
            InitializeComponent();
            SelectedFile = "";
        }

        private void SetSelectedFile(string v)
        {
            if (v == "")
            {
                v = "None";
            }
            selectedFile = v;
            tbSelectedFile.Text = "Selected file: " + v;
        }

        public event VoidString FileSelected;

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            string file = DW.SelectOfFile(Environment.SpecialFolder.DesktopDirectory);
            if (file != null)
            {
                if (File.Exists(file))
                {
                    SelectedFile = file;
                    FileSelected(file);
                }
            }
        }

        string selectedFile = "";

        public string SelectedFile
        {
            get
            {
                return selectedFile;
            }
            set
            {
                SetSelectedFile(value);
            }
        }
    }
}
