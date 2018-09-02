using sunamo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace apps
{
    /// <summary>
    /// Interaction logic for SelectImageFile.xaml
    /// </summary>
    public partial class SelectImageFile : UserControl
    {
        public SelectImageFile()
        {
            InitializeComponent();
            SelectedFile = null;
        }

        private string GetPathFile(StorageFile v)
        {
            if (v == null)
            {
                bi = null;
                return "None";
            }
            return v.Path;
        }

        private async void SetSelectedFile(StorageFile v)
        {
            selectedFile = v;
            tbSelectedFile.Text = "Selected file: " + GetPathFile( v);
        }

        public event VoidStorageFileBitmapImage FileSelected;

        private async void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = null;
            file = await Pickers.GetFile(Windows.Storage.Pickers.PickerViewMode.Thumbnail, Windows.Storage.Pickers.PickerLocationId.PicturesLibrary, ".jpg", ".png");
            if (file != null)
            {
                await OnSelectedFile(file);
            }
        }

        public async Task OnSelectedFile(StorageFile file)
        {
            if (await FSApps.ExistsFile(file))
            {
                SelectedFile = file;
                        bi = new BitmapImage();
                bi.SetSource(await file.OpenReadAsync());
                FileSelected(file, bi);
            }
        }

        BitmapImage bi = null;

        public BitmapImage SelectedBitmapImage
        {
            get
            {
                return bi;
            }
            set
            {
                bi = value;
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
