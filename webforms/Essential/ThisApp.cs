using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webforms.Essential
{
    /// <summary>
    /// 
    /// </summary>
    public class ThisApp : sunamo.Essential.ThisApp
    {
        static ThisApp()
        {
            sunamo.Essential.ThisApp.StatusSetted += ThisApp_StatusSetted;
        }

        /// <summary>
        /// Method SetStatus is in sunamo's ThisApp, then is this EventHandler invoked
        /// </summary>
        /// <param name="t"></param>
        /// <param name="message"></param>
        private static void ThisApp_StatusSetted(TypeOfMessage t, string message)
        {
            
        }


        
    }
}
