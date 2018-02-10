using System.Collections.Generic;
using System.Windows.Controls;

namespace desktop
{
    public interface IUserControlWithMenuItems
    {
        string Name { get; }
        Dictionary<string, List<MenuItem>> MenuItems();
    }
}
