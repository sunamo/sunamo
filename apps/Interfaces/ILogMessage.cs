using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace apps
{
    public interface ILogMessage
    {
        Brush Bg { get; set; }
        string Ts { get; set; }
    }
}
