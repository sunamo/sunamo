using sunamo;
using sunamo.Data;
using sunamo.Interfaces;
using System;
using System.Collections;
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

        public TUList<string, Brush> DefaultBrushes(string green = "", string red = "")
        {
            TUList<string, Brush> result = new TUList<string, Brush>();
            result.Add(TU<string, Brush>.Get(green, Brushes.Green));
            result.Add(TU<string, Brush>.Get(red, Brushes.Red));
            return result;
        }

        /// <summary>
        /// A2 is require but is available through FoundedFilesUC.DefaultBrush
        /// Already inserted is not deleted
        /// </summary>
        /// <param name="foundedList"></param>
        /// <param name="p"></param>
        public void AddFoundedFiles(List<string> foundedList, TUList<string, Brush> p)
        {
            int i = 0;
            foreach (var item in foundedList)
            {
                AddFoundedFile(item, p, ref i);
            }
        }

        /// <summary>
        /// A2 is require but is available through FoundedFilesUC.DefaultBrush
        /// Already inserted is not deleted
        /// </summary>
        /// <param name="foundedList"></param>
        /// <param name="p"></param>
        public void AddFoundedFile(string item, TUList<string, Brush> p, ref int i)
        {
                FoundedFileUC foundedFile = new FoundedFileUC(item, p, i++);
                foundedFile.Selected += FoundedFile_Selected;
                sp.Children.Add(foundedFile);   
        }

        public List<FoundedFileUC> Items
        {
            get
            {
                List<FoundedFileUC> founded = new List<FoundedFileUC>(sp.Children.Count);
                foreach (FoundedFileUC item in sp.Children)
                {
                    founded.Add(item);
                }
                return founded;
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
        /// <summary>
        /// return null if there is no element
        /// </summary>
        /// <returns></returns>
        public string PathOfFirstFile()
        {
            if (Items.Count != 0)
            {
                return Items[0].tbFileName.Text;
            }
            return null;
        }

        
    }
}
