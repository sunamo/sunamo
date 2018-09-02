using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace apps
{
    public class UniversalInterop
    {
        /// <summary>
        /// Vrátí zda zařízení nemá HW tlačítka
        /// </summary>
        /// <returns></returns>
        public static bool IsLargeScreen()
        {
            bool isHardwareButtonsAPIPresent = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");

            if (isHardwareButtonsAPIPresent)
            {
                return false;
                //return Platform.WindowsPhone;
            }
            else
            {
                return true;
                //return Platform.Windows;
            }
        }

        public static bool IsPortrait(bool noScaleFactor)
        {
            Size size = PageHelper.WindowSize(noScaleFactor);
            return size.Height > size.Width;
        }

        
    }
}
