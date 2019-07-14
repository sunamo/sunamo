using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    /// <summary>
    /// 2019-7-4 is use nowhere
    /// </summary>
    public class WindowResult : Window, IUserControlWithResult
    {
        IUserControlWithResult iResult = null;

        public WindowResult(UserControl uc)
        {
            if (uc is IUserControlWithResult)
            {
                iResult = uc as IUserControlWithResult;
                iResult.ChangeDialogResult += IResult_ChangeDialogResult;
                this.Width = uc.Width + 10;
                this.Height = uc.Height + 40;
                Content = uc;    
            }
        }

        private void IResult_ChangeDialogResult(bool? b)
        {
            DialogResult = b;
        }

        public bool? DialogResult
        {
            set
            {
                if (ChangeDialogResult != null)
                {
                    ChangeDialogResult(value);
                }
            }
        }

        public void Accept(object input)
        {
            
        }

        
        public event VoidBoolNullable ChangeDialogResult;
    }
}
