using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.Helpers.Controls
{
    public class MenuItemTemplates
    {
        public static MenuItem AvailableShortcut(Dictionary<string, string> dictionary2)
        {
            MenuItem miShowControls = new MenuItem();
            miShowControls.Click += delegate
            {
                WindowWithUserControl.AvailableShortcut(dictionary2);
            };
            miShowControls.Header = sess.i18n(XlfKeys.AvailableShortcuts) + "...";
            return miShowControls;
        }
    }
}