using desktop.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace desktop.Interfaces
{
    /// <summary>
    /// Better is use IUserControlWithMenuItemsList, because structure here is too complicated and then useless
    /// </summary>
    public interface IUserControlWithMenuItems : IUserControl
    {
        
        Dictionary<string, List<MenuItem>> MenuItems();
    }
}
