using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// For open in one time one window which is defined in MainWindow
/// </summary>
public interface IWindowOpener
{
    WindowWithUserControl windowWithUserControl { get; set; }
}

