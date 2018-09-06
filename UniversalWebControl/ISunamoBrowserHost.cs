using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UniversalWebControl
{
    public interface ISunamoBrowserHost
    {
        WebView SelectedWebView();
        SunamoBrowser lastOpenedSunamoBrowserAlsoInBackground { get;  }
        //SunamoBrowser lastOpenedSunamoBrowserOnlyInBackground { get; }
        /// <summary>
        /// Zda byl otevřen nový tab v poslední operaci
        /// </summary>
        bool wasOpenedNewTab { get; set; }
    }
}
