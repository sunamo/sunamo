using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    public class WindowResult : Window, IResult
    {
        IResult iResult = null;

        public WindowResult(UserControl uc)
        {
            if (uc is IResult)
            {
                iResult = uc as IResult;
                iResult.Finished += iResult_Finished;
                this.Width = uc.Width + 10;
                this.Height = uc.Height + 40;
                Content = uc;    
            }
        }

        void iResult_Finished(object o)
        {
            Finished(o);
        }


        public event VoidObject Finished;
    }
}
