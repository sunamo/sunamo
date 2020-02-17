using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WebViewTag
{
    public WebViewTag()
    {

    }

    /// <summary>
    /// true success, false failed
    /// </summary>
    public bool? DoneNavigationCompleted = null;
    public bool DoneDOMContentLoaded = false;
    public bool DoneLoadComplete = false;
    public string content = string.Empty;
    public string toString = string.Empty;
}
