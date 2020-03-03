using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface ISettingsManager<FrameworkElement, DependencyProperty>
    {
        void AddFromSavedElements(TUList<FrameworkElement, DependencyProperty> list);
        void LoadSettings(FrameworkElement sender, TUList<FrameworkElement, DependencyProperty> savedElements);
        void SaveSettings(FrameworkElement sender, TUList<FrameworkElement, DependencyProperty> savedElements);
    }
}