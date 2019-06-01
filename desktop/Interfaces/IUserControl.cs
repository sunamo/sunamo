using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Maybe will be desirable IWindowOpener
/// </summary>
public interface IUserControl : IPanel
    {
    string Title { get; }
    void Init();

    // Stupid, better is doing that on ctor
    //void OnClosing();
}

