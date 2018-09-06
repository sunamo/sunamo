using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace apps.Helpers
{
    public static class CheckBoxHelper
    {
        public static CheckBox Get(TextWrapping noWrap, string v)
        {
            CheckBox chb = new CheckBox();
            TextBlock tb = new TextBlock();
            tb.Text = v;
            tb.TextWrapping = noWrap;
            chb.Content = tb;
            return chb;
        }
    }
}
