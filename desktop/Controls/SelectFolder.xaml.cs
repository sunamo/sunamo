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
    /// Interaction logic for SelectFolder.xaml
    /// </summary>
    public partial class SelectFolder : UserControl
    {
        //public event VoidString FolderSelected;
        public event VoidString FolderChanged;
        public SelectFolder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nastaví složku pouze když složka bude existovat na disku
        /// </summary>
        public string SelectedFolder
        {
            get
            {
                return txtFolder.Text;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    //FireFolderChanged = false;
                    txtFolder.Text = value;
                    //FireFolderChanged = true;
                }
                else
                {
                    txtFolder.Text = "";
                }
            }
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            SelectOfFolder();
        }

        private void txtFolder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectOfFolder();
        }

        private void SelectOfFolder()
        {
            string folder = DW.SelectOfFolder(Environment.SpecialFolder.MyComputer);
            if (folder != null)
            {
                txtFolder.Text = folder;
                if (FolderChanged != null)
                {
                    FolderChanged(folder);
                }
            }
        }

        private void txtFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
