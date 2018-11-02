using desktop.Storage;
using sunamo.Data;
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
        /// <summary>
        /// TUList due to FrameworkElement will be as many as have dependency property
        /// </summary>
        TUList<FrameworkElement, DependencyProperty> SavedElements { get; set; }
        SettingsManager SettingsManager { get; }
        void AddSavedElement(FrameworkElement fw, DependencyProperty dp);
    }
}
