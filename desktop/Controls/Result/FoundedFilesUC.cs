using desktop.Controls.Result;
using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool? Filter(string text)
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
                bool someVisible = false;

                Regex r = null;

                if (SH.IsWildcard(text))
                {
                    text = Wildcard.WildcardToRegex(text);
                    r = new Regex(text);
                }
                

                    foreach (FoundedFileUC item in sp.Children)
                    {
                        if (item.Contains(r,text))
                        {
                            someVisible = true;
                            item.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            item.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }

                if (!someVisible)
                {
                    OnSelected(null);
                }
                }
                return cancel;
       
        }
    }
}