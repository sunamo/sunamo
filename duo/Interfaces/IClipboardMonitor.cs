using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IClipboardMonitor
{
    // need to create static
    //IClipboardMonitor Instance { get; }
    //bool? monitor { get; set; }
    /// <summary>
    /// Whether after copy to clipboard from any source allow monitoring
    /// </summary>
    bool? afterSet { get; set; }
    bool pernamentlyBlock { get; set; }
}