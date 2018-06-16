using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace desktop.Windows
{
    public class WindowWithText : Window
    {
        ShowTextResult showTextResult = null;

        public WindowWithText(string text)
        {
            showTextResult = new ShowTextResult(text);
            showTextResult.Background = Brushes.Yellow;
            Content = showTextResult;
        }

    }
}
