using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop.Interfaces
{
    /// <summary>
    /// UC which dynamically change size when windows.
    /// </summary>
    public interface IUserControlSizeAdaptive
    {
        void SetSize(double width, double height);
    }
}