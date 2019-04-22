using desktop.AwesomeFont;
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

namespace desktop.Controls.Input
{
    /// <summary>
    /// Interaction logic for SelectMoreFolders.xaml
    /// </summary>
    public partial class SelectMoreFolders : UserControl
    {
        public event VoidVoid SaveSetAsTemplate;

        public SelectMoreFolders()
        {
            InitializeComponent();

            Loaded += SelectMoreFolders_Loaded;
        }

        private void SelectMoreFolders_Loaded(object sender, RoutedEventArgs e)
        {
            SetAwesomeIcons();

            AddFolder(string.Empty);
        }

        public void AddFolder(string folder)
        {
            //TextBox sf = new TextBox();
            //sf.Text = folder;

            SelectFolder sf = new SelectFolder();
            sf.SelectedFolder = folder;
            sf.btnRemoveFolder.Visibility = Visibility.Visible;
            sf.FolderRemoved += Sf_FolderRemoved;
            
            spFolders.Children.Add(sf);
        }

        private void Sf_FolderRemoved(SelectFolder t)
        {
            spFolders.Children.Remove(t);
        }

        async void SetAwesomeIcons()
        {
            await AwesomeFontControls.SetAwesomeFontSymbol(btnAddFolder, "\uf07c New");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnAddAsTemplate, "\uf022 Save set as template");

        }

        private void BtnAddFolder_Click(object sender, RoutedEventArgs e)
        {
            AddFolder(string.Empty);
        }

        public static bool validated
        {
            get
            {
                return SelectFolder.validated;
            }
            set
            {
                SelectFolder .validated = value;
            }
        }

        public static void Validate(TextBlock tb, SelectMoreFolders control)
        {
            foreach (SelectFolder item in control.spFolders.Children)
            {
                item.Validate(tb);
            }
        }

        public void Validate(TextBlock tbFolder)
        {
            Validate(tbFolder, this);
        }

        /// <summary>
        /// Validate before call
        /// </summary>
        /// <returns></returns>
        public List<string> SelectedFolders()
        {
            List<string> result = new List<string>();
            foreach (SelectFolder item in spFolders.Children)
            {
                result.Add(item.SelectedFolder);
            }
            return result;
        }

        private void BtnAddAsTemplate_Click(object sender, RoutedEventArgs e)
        {
            SaveSetAsTemplate();
        }
    }
}
