using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace System.Windows.Controls
{
    public static class ToggleButtonExtensions
    {
        

        public static bool IsCheckedSimple(this ToggleButton tb)
        {
            if (tb.IsChecked.HasValue)
            {
                return tb.IsChecked.Value;
            }
            return false;
        }
    }
}
