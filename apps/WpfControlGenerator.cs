using sunamo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class WpfControlGenerator
    {
        public static StackPanel VerticalColoredList(List<ILogMessage<Color, StorageFile>> c)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            foreach (var item in c)
            {
                Grid g = new Grid();
                g.Background = new SolidColorBrush( item.Bg);
                TextBlock tb = new TextBlock();
                tb.Text = item.Message;
                Grid.SetColumn(tb, 0);
                Grid.SetRow(tb, 0);
                g.Children.Add(tb);
                sp.Children.Add(g);
            }
            return sp;
        }

        public static Grid LogMessage(ILogMessage<Color, StorageFile> c)
        {
                Grid g = new Grid();
                g.Background = new SolidColorBrush(c.Bg);
                TextBlock tb = new TextBlock();
                tb.Text = c.Message;
            tb.TextWrapping = TextWrapping.WrapWholeWords;
                Grid.SetColumn(tb, 0);
                Grid.SetRow(tb, 0);
                g.Children.Add(tb);
            return g;
        }

        /// <summary>
        /// První položka v každém řádku bude jednoznačné ID které se bude předávat do obsluhy příkazu když se klikne na položku
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="widthColumn"></param>
        /// <returns></returns>
        public static ListViewItem GetListViewItemsWithFixedWidthOfColumn(List<string> rows, List<GridLength> widthColumn)
        {
            Grid g = GetGridWithFixedWidthOfColumn(rows, widthColumn);

            ListViewItem lvi = new ListViewItem();
            lvi.Content = g;
            return lvi;
            //return g;
        }

        public static Grid GetGridWithFixedWidthOfColumn(List<string> rows, List<GridLength> widthColumn)
        {
            Grid g = new Grid();
            foreach (var item in widthColumn)
            {
                g.ColumnDefinitions.Add(GridHelper.GetColumnDefinition( item));
            }
            for (int y = 0; y < rows.Count; y++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = rows[y];
                Grid.SetColumn(tb, y);
                g.Children.Add(tb);
            }
            //}
            return g;
        }

        public static UIElement ListViewWithFixedHeader(double ActualWidth, List<List<string>> rows, List<string> header, List<double> widthColumn)
        {
            SunamoDictionary<int, bool> showColumns = new SunamoDictionary<int, bool>();
            double sum = widthColumn.Sum();
            double koef = 0;
            if (sum > ActualWidth)
            {
                koef = (ActualWidth - sum);
            }
            else if (sum < ActualWidth)
            {
                koef = ActualWidth / sum;
            }

            List<GridLength> d = new List<GridLength>(showColumns.Count);
            for (int i = 0; i < widthColumn.Count; i++)
            {
                double d2 = widthColumn[i];
                if (d2 == 0)
                {
                    showColumns.Add(i, false);
                    d.Add(GridHelper.GetGridLength(0));
                }
                else
                {
                    if (koef != 0)
                    {
                        showColumns.Add(i, true);
                        d.Add(GridHelper.GetGridLength(d2 * koef));
                    }
                    else
                    {
                        showColumns.Add(i, true);
                        d.Add(GridHelper.GetGridLength(d2));
                    }
                }
            }

            VirtualizingStackPanel vsp = new VirtualizingStackPanel();

            //ObservableCollection<ListViewItem> lvi = new ObservableCollection<ListViewItem>();
            for (int i = 0; i < rows.Count; i++)
            {
                var lvi = GetListViewItemsWithFixedWidthOfColumn(rows[i], d);
                //lvi.Add( );
                vsp.Children.Add(lvi);
            }

            return vsp;
        }
    }
}
