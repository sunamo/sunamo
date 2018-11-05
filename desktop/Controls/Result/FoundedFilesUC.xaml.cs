using sunamo;
using sunamo.Data;
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

namespace desktop.Controls.Result
{
    /// <summary>
    /// Interaction logic for FoundedFilesUC.xaml
    /// </summary>
    public partial class FoundedFilesUC : UserControl, ISelectedT<string>
    {
        public event VoidString Selected;
        string selectedItem = null;
        public string SelectedItem => selectedItem;
        public static List<string> basePaths = null;


        public FoundedFilesUC()
        {
            InitializeComponent();
        }

        public void Init(params string[] basePath)
        {
            basePaths = basePath.ToList();
            SunamoComparerICompare.StringLength.Desc s = new SunamoComparerICompare.StringLength.Desc(SunamoComparer.StringLength.Instance);
            basePaths.Sort(s);
            CA.WithEndSlash(basePaths);
        }

        public TUList<string, Brush> DefaultBrushes(string green, string red)
        {
            TUList<string, Brush> result = new TUList<string, Brush>();
            result.Add(TU<string, Brush>.Get(green, Brushes.Green));
            result.Add(TU<string, Brush>.Get(red, Brushes.Red));
            return result;
        }

        public void AddFoundedFiles(List<string> foundedList, TUList<string, Brush> p)
        {
            foreach (var item in foundedList)
            {
                FoundedFileUC foundedFile = new FoundedFileUC(item, p);
                foundedFile.Selected += FoundedFile_Selected;
                sp.Children.Add(foundedFile);
            }
        }

        public FoundedFileUC GetFoundedFileByPath(string path)
        {
            foreach (FoundedFileUC item in sp.Children)
            {
                if (item.fileFullPath == path)
                {
                    return item;
                }
            }

            return null;
        }

        private void FoundedFile_Selected(string s)
        {
            selectedItem = s;
            Selected(s);
        }

        public void SelectFile(string file)
        {
            Selected(file);
        }

        public void ClearFoundedResult()
        {
            sp.Children.Clear();
        }

        public void OnSelected(string p)
        {
            Selected(p);
        }
    }
}
