using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace desktop.Interfaces
{
    public interface IWindowWithSettingsManager
    {
        Dictionary<DependencyObject, DependencyProperty> SavedElements { get; set; }
    }
}
