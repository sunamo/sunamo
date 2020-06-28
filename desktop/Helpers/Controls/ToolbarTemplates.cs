using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop.Helpers.Controls
{
    public class ToolbarTemplates
    {
        public static void AvailableShortcut(Dictionary<string, string> dictionary2, ToolBar tsAkce, ImageSource imageOnButton, int whImage, int whButton)
        {
            Button miShowControls = new Button();
            miShowControls.Click += delegate
            {
                WindowWithUserControl.AvailableShortcut(dictionary2);
            };
            Image image = new Image();
            //image.Source =  BitmapSourceHelper.;
            
            //miShowControls.Header = sess.i18n(XlfKeys.AvailableShortcuts) + "...";
        }
    }
}