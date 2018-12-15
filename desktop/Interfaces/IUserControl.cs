using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IUserControl
    {
        string Title { get; }
    // Stupid, better is doing that on ctor
    //void OnClosing();
    }

