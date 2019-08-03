using desktop.AwesomeFont;
using sunamo;
using sunamo.Constants;
using sunamo.Essential;
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
        public static Type type = typeof(SelectFolder);
        public static bool validated;

        //public event VoidString FolderSelected;
        public event VoidString FolderChanged;
        public event VoidT<SelectFolder> FolderRemoved;

        public SelectFolder()
        {
            InitializeComponent();

#if DEBUG
            ComboBox cbDefaultFolders = new ComboBox();
            cbDefaultFolders.IsEditable = false;
            cbDefaultFolders.ItemsSource = DefaultPaths.All;
            cbDefaultFolders.SelectionChanged += CbDefaultFolders_SelectionChanged;
            
#endif

            Loaded += SelectFolder_Loaded;
        }

        private async void SelectFolder_Loaded(object sender, RoutedEventArgs e)
        {
            await AwesomeFontControls.SetAwesomeFontSymbol(btnRemoveFolder, "\uf00d");
        }

        public void Validate(object tbFolder)
        {
            SelectFolder.Validate(tbFolder, this);
        }

        /// <summary>
        /// Before first calling I have to set validated = true
        /// Its instance to avoid include another namespace
        /// </summary>
        /// <param name="validated"></param>
        /// <param name="tb"></param>
        /// <param name="control"></param>
        /// <param name="trim"></param>
        public static void Validate(object tb, SelectFolder control)
        {
            if (!validated)
            {
                return;
            }
            string text = control.SelectedFolder;
           
                text = text.Trim();
            
            if (text == string.Empty)
            {
                InitApp.TemplateLogger.MustHaveValue(TextBlockHelper.TextOrToString( tb));
                validated = false;
            }
            else if (!FS.ExistsDirectory(text))
            {
                InitApp.TemplateLogger.FolderDontExists(text);
                validated = false;
            }
            else
            {
                validated = true;
            }

            
        }

        private void CbDefaultFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            SelectOfFolder(cb.SelectedItem.ToString());
        }

        /// <summary>
        /// Nastaví složku pouze když složka bude existovat na disku
        /// When not, set SE
        /// </summary>
        public string SelectedFolder
        {
            get
            {
                return txtFolder.Text;
            }
            set
            {
                OnFolderChanged(value);
                if (FS.ExistsDirectory(value))
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
            SelectOfFolder(folder);
        }

        private void SelectOfFolder(string folder)
        {
            if (folder != null)
            {
                txtFolder.Text = folder;
                OnFolderChanged(folder);
            }
        }

        private void OnFolderChanged(string folder)
        {
            if (FolderChanged != null)
            {
                FolderChanged(folder);
            }
        }

        private void txtFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void BtnRemoveFolder_Click(object sender, RoutedEventArgs e)
        {
            if (FolderRemoved != null)
            {
                FolderRemoved(this);
            }
        }
    }
}
