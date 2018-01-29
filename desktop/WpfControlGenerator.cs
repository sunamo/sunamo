using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop
{
    public class WpfControlGenerator
    {
        public static StackPanel VerticalColoredList(List<ILogMessage<Color, string>> c)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            foreach (var item in c)
            {
                Grid g = new Grid();
                g.Background = new SolidColorBrush(item.Bg);
                TextBlock tb = new TextBlock();
                tb.Text = item.Message;
                Grid.SetColumn(tb, 0);
                Grid.SetRow(tb, 0);
                g.Children.Add(tb);
                sp.Children.Add(g);
            }
            return sp;
        }

        public static Grid LogMessage(ILogMessage<Color, string> c)
        {
            Grid g = new Grid();
            g.Background = new SolidColorBrush(c.Bg);
            TextBlock tb = new TextBlock();
            tb.Text = c.Message;
            tb.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(tb, 0);
            Grid.SetRow(tb, 0);
            g.Children.Add(tb);
            return g;
        }
    }
}
