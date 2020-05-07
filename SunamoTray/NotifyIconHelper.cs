using forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SunamoTray
{
    public class NotifyIconHelper
    {
        static Action<object, EventArgs> quit = null;
        static Action<bool> setCancelClosing = null;

        /// <summary>
        /// Into A1 insert forms.ContextMenuHelper.Get
        /// </summary>
        /// <param name="cm"></param>
        public static void Create(Action<bool> SetCancelClosing, Stream streamIcon, VoidObjectEventArgs onDoubleClick, ContextMenu cm, Action<object, EventArgs> quitAction, Dictionary<string, Action> contextMenuItems = null)
        {
            quit = quitAction;
            setCancelClosing = SetCancelClosing;

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(streamIcon);
            ni.Visible = true;
            ni.ContextMenu = cm;

            ni.Click += new EventHandler( onDoubleClick);

            var mi = MenuItemHelper.Get("Quit", Quit);
            ni.ContextMenu.MenuItems.Add(mi);
            if (contextMenuItems != null)
            {
                foreach (var item in contextMenuItems)
                {
                    ni.ContextMenu.MenuItems.Add(MenuItemHelper.Get(item.Key, new VoidObjectEventArgs((a, b) => item.Value())));
                }
            }
        }

        static void Quit(object o, EventArgs ea)
        {
            setCancelClosing.Invoke(false);
            quit.Invoke(o, ea);
        }
    }
}