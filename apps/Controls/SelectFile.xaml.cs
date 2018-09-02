using sunamo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace apps
{
    /// <summary>
    /// Interaction logic for SelectFile.xaml
    /// </summary>
    public partial class SelectFile : UserControl
    {
        public SelectFile()
        {
            InitializeComponent();
            SelectedFile = null;
        }

        private void SetSelectedFile(StorageFile v)
        {
            selectedFile = v;
            tbSelectedFile.Text = "Selected file: " + GetPathStorageFile(v);
        }

        private string GetPathStorageFile(StorageFile v)
        {
            if (v == null)
            {
                return "None";
            }
            return v.Path;
        }

        public event VoidStorageFile FileSelected;

        private async void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = await Pickers.GetFile(PickerViewMode.Thumbnail, PickerLocationId.PicturesLibrary, ".jpg", ".png", ".gif", ".bmp");
            if (file != null)
            {
                if (await FSApps.ExistsFile(file))
                {
                    SelectedFile = file;
                    FileSelected(file);
                }
            }
        }

        StorageFile selectedFile = null;

        public StorageFile SelectedFile
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
