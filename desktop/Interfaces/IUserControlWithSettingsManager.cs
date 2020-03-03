using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop.Interfaces
{
    public interface IUserControlWithSettingsManager
    {
        // Picovina, its running automatically while startup and shutdown
        //void LoadSettings();
        //void SaveSettings();
        ApplicationDataContainer data
        {
            get;
        }
    }
}