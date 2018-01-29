using sunamo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
    /// Interaction logic for SelectImageFileAndMakeSingleColorTransparent.xaml
    /// </summary>
    public partial class SelectImageFileAndMakeSingleColorTransparent : UserControl
    {
        public int MinimalImageWidth { get; set; }
        public int MinimalImageHeight { get; set; }
        public bool Square { get; set; }

        public SelectImageFileAndMakeSingleColorTransparent()
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

        public event VoidStringBitmapBitmapSource FileSelected;
        public BitmapImage bi = null;
        public Bitmap bmp = null;
        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            string file = null;
            file = DW.SelectOfFile(Environment.SpecialFolder.DesktopDirectory);
            if (file != null)
            {
                if (File.Exists(file))
                {
                    SelectedFile = file;
                     bi = new BitmapImage(new Uri(file));
                    bmp = new Bitmap(file);
                    System.Drawing.Color first2 =  bmp.GetPixel(0, 0);
                    
                    

                    bi = new BitmapImage();
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, ImageFormat.Png);
                    var arr = ms.ToArray();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    var bs = bi;
                    bmp = shared.Pictures.BitmapImage2Bitmap(bs);
                    //bmp.MakeTransparent(System.Drawing.Color.FromArgb(pxs[0, 0].Alpha, pxs[0, 0].Red, pxs[0, 0].Green, pxs[0, 0].Blue));
                    FileSelected(file, bmp, bs);
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
