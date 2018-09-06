using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace apps
{
    /// <summary>
    /// Tento gridview pracuje s řádky v obsahu i hlavičce které je grid
    /// </summary>
    public class SunamoListView : ListView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            int index = IndexFromContainer(element);
            ListViewItem lvi = element as ListViewItem;
            if (index % 2 == 0)
            {
                lvi.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                lvi.Background = new SolidColorBrush(Color.FromArgb(255, 211, 211, 211));
                //lvi.Background = new SolidColorBrush(Colors.Black);
            }
        }

    }
}
