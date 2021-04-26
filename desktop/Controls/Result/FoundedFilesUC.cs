using desktop.Controls.Result;
using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop.Controls.Result
{
    public class FoundedFilesUC : FoundedResultsUC
    {
        /// <summary>
        /// A2 is require but is available through FoundedFilesUC.DefaultBrush
        /// Already inserted is not deleted
        /// </summary>
        /// <param name="foundedList"></param>
        /// <param name="p"></param>
        public void AddFoundedFiles(List<string> foundedList, TUList<string, System.Windows.Media.Brush> p)
        {
            HideTbNoResultsFound();
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
        public void AddFoundedFile(string item, TUList<string, System.Windows.Media.Brush> p, ref int i)
        {
            HideTbNoResultsFound();

         

            FoundedFileUC foundedFile = new FoundedFileUC(item, p, i++);
            foundedFile.Selected += FoundedFile_Selected;
            sp.Children.Add(foundedFile);
        }

        private void FoundedFile_Selected(string s)
        {
            selectedItem = s;
            OnSelected(s);
        }

        public void Filter(string text)
        {
            if (sp.Children.Count != 0)
            {
                bool cancel = string.IsNullOrWhiteSpace(text);
                if (cancel)
                {
                    foreach (FoundedFileUC item in sp.Children)
                    {
                        item.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    foreach (FoundedFileUC item in sp.Children)
                    {
                        if (item.Contains(text))
                        {
                            item.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            item.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }
        }
    }
}