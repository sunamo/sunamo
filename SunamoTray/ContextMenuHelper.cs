using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace forms
{
    /// <summary>
    /// There are only two variants:
    /// SunamoTray - no import SWF to shared, but have to import to forms
    /// shared - must import SWF, the worst, chaoss with WPF
    /// forms - chaoss with WPF, increase size of app due to whole forms project
    /// </summary>
    public class ContextMenuHelper
    {
        public static ContextMenu Get(VoidObjectEventArgs onQuit)
        {
            ContextMenu cm = new ContextMenu();

            if (onQuit != null)
            {
                cm.MenuItems.Add(MenuItemHelper.Get("Quit", onQuit));
            }
            

            return cm;
        }
    }
}