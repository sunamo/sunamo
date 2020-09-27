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

// desktopControlsInput
namespace desktop.Controls.Input
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SelectManyFiles : UserControl
    {
       

        public SelectManyFiles()
        {
            InitializeComponent();

            Loaded += SelectMoreFiles_Loaded;
        }

        public event VoidVoid SaveSetAsTemplate;
        /// <summary>
        /// Only adding File with empty path
        /// </summary>
        public event Action<object, List<string>> FileAdded;
        public event Action<object, List<string>> FileChanged;
        public event Action<object, List<string>> FileRemoved;

        private void SelectMoreFiles_Loaded(object sender, RoutedEventArgs e)
        {
            SetAwesomeIcons();

            AddFile(string.Empty);
        }

        public void AddFile(string File)
        {
            //TextBox sf = new TextBox();
            //sf.Text = File;

            SelectFile sf = new SelectFile();
            sf.SelectedFile = File;
            sf.btnRemoveFile.Visibility = Visibility.Visible;
            sf.FileRemoved += Sf_FileRemoved;
            sf.FileSelected += Sf_FileChanged;

            spFiles.Children.Add(sf);
            if (FileAdded != null)
            {
                FileAdded(this, SelectedFiles());
            }
            // Must be called after sf is on panel and has registered Sf_FileChanged, because control for FileChanged != null
            Sf_FileChanged(File);
        }

        private void Sf_FileChanged(string s)
        {
            if (FileChanged != null)
            {
                FileChanged(this, SelectedFiles());
            }
        }

        public void Sf_FileRemoved(SelectFile t)
        {
            spFiles.Children.Remove(t);
            if (FileRemoved != null)
            {
                FileRemoved(this, SelectedFiles());
            }
        }

        async void SetAwesomeIcons()
        {
            await AwesomeFontControls.SetAwesomeFontSymbol(btnAddFile, "\uf07c " + sess.i18n(XlfKeys.New));
            await AwesomeFontControls.SetAwesomeFontSymbol(btnAddAsTemplate, "\uf022 Save set as template");
        }

        private void BtnAddFile_Click(object sender, RoutedEventArgs e)
        {
            AddFile(string.Empty);
        }

        

       

        public void RemoveAllFiles()
        {
            for (int i = spFiles.Children.Count - 1; i >= 0; i--)
            {
                Sf_FileRemoved((SelectFile)spFiles.Children[i]);
            }
        }

        /// <summary>
        /// Validate before call
        /// </summary>
        public List<string> SelectedFiles()
        {
            List<string> result = new List<string>();
            foreach (SelectFile item in spFiles.Children)
            {
                // Here I can eliminate empty strings, during Validate is calling Validate on every control, not use this method
                if (item.SelectedFile != string.Empty)
                {
                    result.Add(item.SelectedFile);
                }

            }
            return result;
        }

        private void BtnAddAsTemplate_Click(object sender, RoutedEventArgs e)
        {
            SaveSetAsTemplate();
        }
    }
}