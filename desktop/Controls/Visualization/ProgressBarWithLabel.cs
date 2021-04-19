using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class ProgressBarWithLabel : UserControl
{
    public ProgressBar pb = null;
    
    public TextBlock tb = null;
    public const string prefixWith = "   ";

    public string TbText
    {
        get
        {
            return WpfApp.cd.Invoke<string>(() => tb.Text);
        }
        set
        {
            if (string.IsNullOrWhiteSpace(TbText))
            {
                tb.Text = string.Empty;
            }
            else
            {
                WpfApp.cd.Invoke(() => tb.Text = prefixWith + value);
            }
        }
    }

    public ProgressBarWithLabel()
    {
        StackPanel sp = new StackPanel();
        sp.Orientation = Orientation.Horizontal;

        pb = new ProgressBar();
        pb.Width = 300;
        //pb.Value = 100;

        sp.Children.Add(pb);

        

        tb = new TextBlock();
        tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        tb.Width = 300;
        sp.Children.Add(tb);

        Content = sp;
    }


}