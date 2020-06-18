using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class StackPanelUC : UserControl, IUserControl
{
    public StackPanel sp = new StackPanel();

    public string Title => string.Empty;

    public void Init()
    {
        Content = sp;
    }
}