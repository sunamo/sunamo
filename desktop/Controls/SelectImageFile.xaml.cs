using sunamo;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for SelectImageFile.xaml
    /// </summary>
    public partial class SelectImageFile : UserControl
    {
        public int MinimalImageWidth { get; set; }
        public int MinimalImageHeight { get; set; }
        public bool Square { get; set; }

        public SelectImageFile()
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

        public event VoidStringBitmapBitmapImage FileSelected;

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            string file = null;
            file = DW.SelectOfFile(Environment.SpecialFolder.DesktopDirectory);
            if (file != null)
            {
                if (File.Exists(file))
                {
                    SelectedFile = file;
                        if (bi == null)
                    {
                        if (File.Exists(file))
                        {
                            bi = new BitmapImage(new Uri(file));
                        }
                    }
                    FileSelected(file, null, bi);
                }
            }
        }

        BitmapImage bi = null;

        

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
