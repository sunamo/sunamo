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
    /// Interaction logic for FoundedResultsUC.xaml
    /// </summary>
    public partial class FoundedResultsUC : UserControl, ISelectedT<string>
    {
        public event VoidString Selected;
        protected string selectedItem = null;
        public string SelectedItem => selectedItem;
        public static List<string> basePaths = CA.ToListString();

        public FoundedResultsUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A1 is used to trim when start with them
        /// </summary>
        /// <param name="basePath"></param>
        public void Init(params string[] basePath)
        {
            tbNoResultsFound.Text = sess.i18n(XlfKeys.NoResultsFound);
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

        protected void HideTbNoResultsFound()
        {
            tbNoResultsFound.Visibility = Visibility.Collapsed;
            sv.Visibility = Visibility.Visible;
        }

        public void AddFoundedResults(bool clear, TUList<string, Brush> p, List<TWithName<string>> foundedResult)
        {
            int i = 1;

            if (clear)
            {
                ClearFoundedResult();
            }

            if (foundedResult.Count > 0)
            {
                HideTbNoResultsFound();
                sv.Visibility = Visibility.Visible;
            }

            foreach (var item in foundedResult)
            {
                FoundedResultUC fr = new FoundedResultUC(item.name, p, i++);
                fr.Selected += OnSelected;

                TextBlock tb = TextBlockHelper.Get(new ControlInitData { text = item.t });

                fr.SecondRow = tb;

                sp.Children.Add(fr);
            }
        }

        /// <summary>
        ///  Can be use only getting, not for removing due to from lb wont be removed
        /// </summary>
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

        

        public void SelectFile(string file)
        {
            Selected(file);
        }

        public void ClearFoundedResult()
        {
            sp.Children.Clear();
            sv.Visibility = Visibility.Collapsed;
            tbNoResultsFound.Visibility = Visibility.Visible;
        }

        public void OnSelected(string p)
        {
            Selected(p);
        }

        /// <summary>
        /// return null if there is no element
        /// </summary>
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