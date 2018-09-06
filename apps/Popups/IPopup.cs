using apps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace apps
{
    public interface IPopupEvents<T>
    {
        event VoidT<T> ClickCancel;
        event VoidT<T> ClickOK;
    }

    public interface IPopup : IColorTheme
    {
        Brush PopupBorderBrush { set; }
        
    }

    public interface IPopupDialogResult 
    {
        /// <summary>
        /// Null není pro zavření okna, null je pro 3. tlačítko
        /// </summary>
        event VoidBoolNullable ChangeDialogResult;
        /// <summary>
        /// Do Set zapiš jen ChangeDialogResult(value);
        /// </summary>
        bool? DialogResult { set; }
    }
}
