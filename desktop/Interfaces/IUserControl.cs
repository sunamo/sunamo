using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IUserControl : IPanel, IWindowOpener
    {
    string Title { get; }
    void Init();

    // Stupid, better is doing that on ctor
    //void OnClosing();
}

