using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IUserControlWithSizeChange : IUserControl
    {
        void OnSizeChanged(DesktopSize maxSize);
    }

