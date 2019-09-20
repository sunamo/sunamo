﻿using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class ShowTextResultWindow : Window, IUserControlInWindow
{
    public ShowTextResultWindow(string text)
    {
        var s = new ShowTextResult(text);
        Content = s;
        s.ChangeDialogResult += S_ChangeDialogResult;
    }

    public event VoidBoolNullable ChangeDialogResult;

    public void Accept(object input)
    {
        
    }

    private void S_ChangeDialogResult(bool? b)
    {
        ChangeDialogResult(b);
    }
}

