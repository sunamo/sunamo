
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

/// <summary>
/// IEssentialMainPage is in apps
/// Must be derived from IWindowOpener - without implement in MainWindow cant be shown exceptions window
/// </summary>
public interface IEssentialMainWindow : IWindowOpener
{
    Control actual { get; set; }
        void SetMode(object mode);
     string ModeString { get; }
    }