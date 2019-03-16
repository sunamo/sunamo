using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop
{
    /// <summary>
    /// Combined with Window. Dont use Window due to WpfApp.mp
    /// </summary>
    public interface IEssentialMainPage : IPanel
    {
        // cant be Title as in UC, because Window has own Property
        bool CancelClosing { get; set; }
    }
}
