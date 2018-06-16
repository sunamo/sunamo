using sunamo.Essential;
using sunamo.Interfaces;
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
using desktop.Data;
using sunamo.Collections;

namespace desktop.Controls
{
    /// <summary>
    /// Interaction logic for FoundedFilesUC.xaml
    /// </summary>
    public partial class FoundedFilesUC : UserControl, ISelected
    {
        public object SelectedFile => selected;
        public event VoidT<string> CopyToClipboardAndDelete;
        public event VoidT<string> Selected;
        public event VoidT<string> CutFile;
        public event VoidT<string> CopyToAnotherLocation;
        object selected = null;
        Dictionary<string, FoundedFileUC> allFiles = new Dictionary<string, FoundedFileUC>();

        public bool IsSelected
        {
            get
            {
                return selected != null;
            }
            set
            {
                if (!value)
                {
                    selected = null;
                }
            }
        }

        public FoundedFilesUC()
        {
            InitializeComponent();
        }
        
        public void RemoveFile(string o)
        {
            spFoundedFiles.Items.Remove(allFiles[ o]);
        }

        /// <summary>
        /// A1 is contained path which will be marked green
        /// </summary>
        /// <param name="limeGreen"></param>
        /// <returns></returns>
        public TUList<string, Brush> DefaultBrushes(string limeGreen, string red)
        {
            TUList<string, Brush> colorString = null;
            colorString = new TUList<string, Brush>();
            colorString.Add(red, Brushes.Red);

            colorString.Add(limeGreen, Brushes.LimeGreen);
            return colorString;
        }

        public void ClearFoundedResult()
        {
            foreach (FoundedFileUC item in spFoundedFiles.Items)
            {
                item.Selected -= Uc_Selected;
            }

            spFoundedFiles.Items.Clear();
        }

    
        /// <summary>
        /// In A2 first must be negative (eg Red) - after assigment cycle will end
        /// </summary>
        /// <param name="founded"></param>
        /// <param name="colorList"></param>
        public void AddFoundedFiles(List<string> founded, IEnumerable<TU<string, Brush>> colorList)
        {
            foreach (var item in founded)
            {
                FoundedFileUC uc = new FoundedFileUC(item, colorList);
                uc.Selected += Uc_Selected;
                uc.CopyToClipboardAndDelete += Uc_CopyToClipboardAndDelete;
                uc.CutFile += Uc_CutFile;
                uc.CopyToAnotherLocation += Uc_CopyToAnotherLocation;
                spFoundedFiles.Items.Add(uc);
                allFiles.Add(item, uc);
            }
        }

        private void Uc_CopyToAnotherLocation(string t)
        {
            if (IsSelected)
            {
                if (CopyToAnotherLocation != null)
                {
                    CopyToAnotherLocation(t);
                }
            }
        }

        private void Uc_CutFile(string t)
        {
            if (IsSelected)
            {
                if (CutFile != null)
                {
                    CutFile(t);
                }
            }
        }

        private void Uc_CopyToClipboardAndDelete(string t)
        {
            if (IsSelected)
            {
                if (CopyToClipboardAndDelete != null)
                {
                    CopyToClipboardAndDelete(t);
                }
            }
        }

        private void Uc_Selected(string t)
        {
            selected = t;
            ThisApp.SetStatus(TypeOfMessage.Information, "Selected file: " + t);
            Selected(t);
        }
    }
}
