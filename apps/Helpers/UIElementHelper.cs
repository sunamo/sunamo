using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Core;

namespace apps
{
    class UIElementHelper
    {
        public  static void Refresh( UIElement uiElement)
        {
            uiElement.UpdateLayout();
        }
    }
}
