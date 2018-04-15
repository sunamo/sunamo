using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.Interfaces
{
    public interface IUserControlWithMenuItemsList : IUserControl
    {
        List<MenuItem> MenuItems();
    }
}
