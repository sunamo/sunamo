using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forms.Essential
{
    public class ThisApp : sunamo.Essential.ThisApp
    {
        public ThisApp()
        {
            sunamo.Essential.ThisApp.StatusSetted += ThisApp_StatusSetted;
        }

        private void ThisApp_StatusSetted(TypeOfMessage t, string message)
        {
            
        }
    }
}
