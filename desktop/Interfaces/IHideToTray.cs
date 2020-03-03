using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IHideToTray
{
    // cant be Title as in UC, because Window has own Property
    bool CancelClosing { get; set; }

    bool GetCancelClosing();
    void SetCancelClosing(bool b);
}