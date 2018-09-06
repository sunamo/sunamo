using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps.PopupsNoResponsive
{
    public interface IPopupCalculatedSize //: IPopup
    {
        //List<Control> GetChildElements();
        Brush PopupBorderBrush { set; }
        Border Border { get; }
        StackPanel StackPanel { get; }
    }
}
