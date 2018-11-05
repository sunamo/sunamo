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

namespace desktop.Controls.Result
{
    /// <summary>
    /// Interaction logic for FoundedFileUC.xaml
    /// </summary>
    public partial class FoundedFileUC : UserControl
    {
        public event VoidString Selected;
        public string fileFullPath;
        /// <summary>
        /// Without removed FoundedFilesUC.basePaths
        /// </summary>
        string file;

        public FoundedFileUC(string filePath, TUList<string, Brush> p)
        {
            InitializeComponent();

            this.fileFullPath = filePath;
            this.file = filePath;

            bool replaced = false;
            foreach (var item in FoundedFilesUC.basePaths)
            {
                file = SH.ReplaceOnceIfStartedWith( file, item, "", out replaced);
                if (replaced)
                {
                    break;
                }
            }

            tbFileName.Text = file;
            foreach (var item in p)
            {
                    if (SH.Contains( fileFullPath, item.Key))
                    {
                        tbFileName.Foreground = item.Value;
                        break;
                    }
            }

            this.MouseLeftButtonDown += FoundedFileUC_MouseLeftButtonDown;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            Selected(fileFullPath);
        }

        private void FoundedFileUC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Selected(fileFullPath);
        }
    }
}
