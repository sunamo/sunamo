using apps.Essential;
using sunamo;
using sunamo.LoggerAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class LogMessage : LogMessageAbstract<Color, StorageFile>//, ILogMessage
    {
        public LogMessage()
        {

        }

        protected async override void SetBg(Color c)
        {
                await ThisApp.cd.RunAsync(ThisApp.cdp, () => {
                    Bg = c;
                });
        }

    }

}
