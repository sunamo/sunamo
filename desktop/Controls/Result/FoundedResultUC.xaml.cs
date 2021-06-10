using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 
    /// </summary>
    public partial class FoundedResultUC : UserControl
    {
        //UIElement previousSecondRow = new UserControl();

        //public FoundedResultUC(string header, int serie)
        //{
        //    InitializeComponent();

        //    ellipseSerie.Stroke = Brushes.Black;

        //    tbHeader.Text = header;
        //    tbSerie.Text = serie.ToString();
        //}

        //public UIElement SecondRow
        //{
        //    set
        //    {
        //        //gridContent.Children.Remove(previousSecondRow);

        //        //Grid.SetRow(value, 1);
        //        //Grid.SetColumn(value, 0);
        //        //gridContent.Children.Add(value);

        //        //gridContent.UpdateLayout();

        //        dp.Children.Clear();
        //        dp.Children.Add(value);

        //        //tbDesc.Content = value;

        //        previousSecondRow = value;
        //    }
        //}

        public event VoidString Selected;
        /// <summary>
        /// Is used for colorful highlighting and Selected event
        /// </summary>
        public string fileFullPath = null;
        /// <summary>
        /// Is used for showing visually
        /// </summary>
        public string file = null;

        public bool Contains(string t)
        {
            return fileFullPath.Contains(t);
        }

        public bool Contains(Regex r, string text)
        {
            if (r != null)
            {
                return r.IsMatch(fileFullPath);
            }
            
            return fileFullPath.Contains(text);
        }

        /// <summary>
        /// A2 is require but is available through foundedResultsUC.DefaultBrush
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="p"></param>
        /// <param name="serie"></param>
        public FoundedResultUC(string filePath, TUList<string, Brush> p, int serie)
        {
            InitializeComponent();

            ellipseSerie.Stroke = Brushes.Black;
            tbSerie.Text = serie.ToString();
            this.fileFullPath = filePath;
            this.file = filePath;

            bool replaced = false;
            foreach (var item in FoundedFilesUC.basePaths)
            {
                file = SH.ReplaceOnceIfStartedWith(file, item, "", out replaced);
                if (replaced)
                {
                    break;
                }
            }

            tbFileName.Text = file;
            
            foreach (var item in p)
            {
                if (SH.Contains(fileFullPath, item.Key))
                {
                    ellipseSerie.Stroke = item.Value;
                    tbFileName.Foreground = item.Value;
                    break;
                }
            }

            this.MouseLeftButtonDown += FoundedFileUC_MouseLeftButtonDown;
        }

        public UIElement SecondRow
        {
            set
            {

            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            Selected(fileFullPath);
        }

        private void FoundedFileUC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Selected(fileFullPath);
        }
    }
}