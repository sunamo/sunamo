using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace forms
{
    public class MenuItemHelper 
    {
        public static MenuItem Get(string text, VoidObjectEventArgs onClick)
        {
            MenuItem mi = new MenuItem();
            mi.Text = text;
            mi.Click += new EventHandler( onClick);
            return mi;
        }
    }
}