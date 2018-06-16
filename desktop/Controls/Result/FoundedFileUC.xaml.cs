using desktop.Converters;
using sunamo;
using sunamo.Interfaces;
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
    /// Interaction logic for FoundedFileUC.xaml
    /// </summary>
    public partial class FoundedFileUC : UserControl
    {
        public event VoidT<string> Selected;
        public event VoidT<string> CopyToClipboardAndDelete;
        public event VoidT<string> CutFile;
        public event VoidT<string> CopyToAnotherLocation;

        public static bool IsVisibleBtnCopyToClipboardAndDelete = true;
        public static bool IsVisibleBtnCutFile = true;
        public static bool IsVisibleCopyToAnotherLocation = false;

        public FoundedFileUC(string file, IEnumerable<TU<string, Brush>> colorList)
        {
            InitializeComponent();

            SetVisibility();

            FilePath = file;
            tbFile.Text = file;
            if (colorList != null)
            {
                foreach (var item in colorList)
                {
                    if (file.Contains(item.Key))
                    {
                        tbFile.Foreground = item.Value;
                        break;
                    }
                }
            }

            //grid.IsKeyboardFocusedChanged;
            //grid.IsKeyboardFocusWithinChanged;
            grid.MouseDown += grid_MouseDown;
            grid.GotFocus += Grid_GotFocus;
            grid.GotKeyboardFocus += Grid_GotKeyboardFocus;

            btnCopyToClipboardAndDelete.Click += btnCopyToClipboardAndDelete_Click;
            btnCutFile.Click += btnCutFile_Click;
            btnCopyToAnotherLocation.Click += BtnCopyToAnotherLocation_Click;
        }

        private void BtnCopyToAnotherLocation_Click(object sender, RoutedEventArgs e)
        {
            CopyToAnotherLocation(FilePath);
        }

        public static void SetVisibility(bool b)
        {
            IsVisibleBtnCopyToClipboardAndDelete = b;
            IsVisibleBtnCutFile = b;
            
        }

        private void SetVisibility()
        {
            btnCopyToClipboardAndDelete.Visibility = VisibilityToBoolConverter.ConvertFrom(IsVisibleBtnCopyToClipboardAndDelete);
            btnCutFile.Visibility = VisibilityToBoolConverter.ConvertFrom(IsVisibleBtnCutFile);
            btnCopyToAnotherLocation.Visibility = VisibilityToBoolConverter.ConvertFrom(IsVisibleCopyToAnotherLocation);
        }

        private void Grid_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            OnSelected();
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            OnSelected();
        }

        public string FilePath = "";

        private void btnCopyToClipboardAndDelete_Click(object sender, RoutedEventArgs e)
        {
            CopyToClipboardAndDelete(FilePath);
        }

        void OnSelected()
        {
            Selected(FilePath);
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnSelected();
        }

        private void FoundedFileUC_Selected(string t)
        {
            
        }

        private void btnCutFile_Click(object sender, RoutedEventArgs e)
        {
            CutFile(FilePath);
        }

        
    }
}
