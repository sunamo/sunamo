using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

[ComVisible(true)]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IUserControlWithMenuItemsList : IUserControl
    {
        List<MenuItem> MenuItems();
    }