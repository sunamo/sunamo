using System;
using System.Collections.Generic;
using System.Text;


public class ClipboardMonitorCore : IClipboardMonitor
{

    public static ClipboardMonitorCore Instance = new ClipboardMonitorCore();

    public bool? afterSet { get; set; }
    public bool pernamentlyBlock { get; set; }
}

