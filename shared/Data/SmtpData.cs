using sunamo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
/// <summary>
/// variables must be props to use as Binding
/// </summary>
public class SmtpData : SmtpServerData, IParseCollection, INotifyPropertyChanged
{
    public string login { get; set; }
    public string pw { get; set; }
  #region How I was doing idiot with creating table with row's grid instead of use GridViewColumn
    //GridLength width = new GridLength(20);

    //public static event VoidT<double> WidthChanged;
    //public GridLength Width
    //{
    //    get
    //    {
    //        return width;
    //    }
    //    set
    //    {
    //        width = value;
    //        OnPropertyChanged("Width");
    //        OnWidthChanged(width.Value);
    //    }
    //}

    //double widthGrid = 200;

    //public double WidthGrid
    //{
    //    get
    //    {
    //        return widthGrid;
    //    }
    //    set
    //    {
    //        widthGrid = value;
    //        OnPropertyChanged("WidthGrid");
    //    }
    //}

    //public void OnWidthChanged(double w)
    //{
    //    if (WidthChanged != null)
    //    {
    //        WidthChanged(w);
    //    }
    //} 
    #endregion  
    int isDefault = 0;
    public static Type type = typeof(SmtpData);

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged(string propName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    Visibility visibilityBtnSetDefault = Visibility.Visible;

    public Visibility VisibilityBtnSetDefault
    {
        get
        {
            return visibilityBtnSetDefault;
        }
        set
        {
            visibilityBtnSetDefault = value;
            OnPropertyChanged("VisibilityBtnSetDefault");
        }
    }

    public int IsDefault
    {
        get
        {
            return isDefault;
        }
        set
        {
            isDefault = value;
            if (isDefault == 0)
            {
                VisibilityBtnSetDefault = Visibility.Visible;
            }
            else if (isDefault == 1)
            {
                VisibilityBtnSetDefault = Visibility.Collapsed;
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), isDefault);
            }
        }
    }

    public string pwToShow
    {
        get
        {
            return pw;
        }
    }

    public void ParseCollection(List<string> s)
    {
        smtpServer = s[0];
        port = SmtpHelper.ParsePort(s[1]);
        login = s[2];
        pw = s[3];
        IsDefault = BTS.ParseInt(s[4], 0);
    }
}