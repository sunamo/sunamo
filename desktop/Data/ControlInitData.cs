using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

public class ControlInitData
{
    public string tooltip= null;
    public string imagePath= null;
    public string text= null;
    public RoutedEventHandler OnClick= null;
    public object tag= null;
    public Brush foreground= null;
    public IEnumerable list = null;
    public bool checkable = true;
    public string group = null;
}

