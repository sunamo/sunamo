using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace System.Windows.Controls
{
    public static class RadioButtonExtensions
    {
        public static bool IsCheckedSimple(this RadioButton tb)
        {
            if (tb.IsChecked.HasValue)
            {
                return tb.IsChecked.Value;
            }
            return false;

        }
    }
}