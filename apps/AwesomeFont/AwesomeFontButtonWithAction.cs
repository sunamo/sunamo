using apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps.AwesomeFont
{
    /// <summary>
    /// Button s obrázkem z AwesomeFont
    /// </summary>
    class AwesomeFontButtonWithAction : ButtonWithAction
    {
        public async void InitAwesomeFontButtonWithAction(bool visible, double width, double height, TaskObject action, string otf, Brush fg, object idObject)
        {
            if (visible)
            {
                InitButtonWithAction(visible, width, height, action, null, idObject);

                TextBlock tb = new TextBlock();

                await AwesomeFontControls.SetAwesomeFontSymbol(tb, otf, fg, AwesomeFontControls.CalculateFontSize(width), "");

                base.SetButtonContent(tb);
            }
        }
    }
}
