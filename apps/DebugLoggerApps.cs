using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#if DEBUG
namespace apps
{
    public class DebugLoggerApps
    {
        public static DebugLoggerApps Instance = new DebugLoggerApps();

        private DebugLoggerApps()
        {

        }


        public void Write(string message)
        {
            //DateTime.Now.ToLongTimeString() + " " + message);
            Debug.Write(message + Environment.NewLine);
        }

        public  void WriteActualPositionOfCanvasElement(UIElement svMenu)
        {
            DebugLoggerApps.Instance.Write(Canvas.GetLeft(svMenu) + " " + Canvas.GetTop(svMenu));
        }
    }
}
#endif
