using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace apps.Helpers
{
    public static class ButtonHelper
    {
        public static Button Get(Orientation orientation, object content, RoutedEventHandler eh)
        {
            Button vr = Get(Orientation.Horizontal, content);
            vr.Click += eh;
            return vr;
        }

        public static Button Get(Orientation orientation, object content, ICommand eh)
        {
            Button vr = Get(Orientation.Horizontal, content);
            vr.Command = eh;
            return vr;
        }

        public static Button Get(Orientation orientation, object content, ISunamoAsyncCommand eh)
        {
            Button vr = Get(Orientation.Horizontal, content);
            vr.Click += delegate (object sender, RoutedEventArgs ea) {
                eh.Execute(sender);
            };
            return vr;
        }

        /// <summary>
        /// Pokud je A1 orientován do řádku(Horizontal), bude uprostřed řádku. Jinak sloupce.
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Button Get(Orientation orientation, object content)
        {
            Button vr = new Button();
            vr.Content = content;

            if (orientation == Orientation.Horizontal)
            {
                vr.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                vr.HorizontalAlignment = HorizontalAlignment.Center;
            }
            return vr;
        }
    }
}
