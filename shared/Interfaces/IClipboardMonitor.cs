﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IClipboardMonitor
    {
        IClipboardMonitor Instance { get; }
        bool? monitor { get; set; }
        bool afterSet { get; set; }
    }

