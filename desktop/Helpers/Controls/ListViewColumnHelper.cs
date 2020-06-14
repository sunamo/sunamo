using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Must be IIdentificatorDesktop to avoid checking ltems which isnt visible 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ListViewColumnHelper<T> where T : IIdentificatorDesktop<int>
{
    public static Type type = typeof(ListViewColumnHelper<T>);
    public int lastId = int.MinValue;
    private ListView lstViewXamlColumns;

    public ListViewColumnHelper(ListView lstViewXamlColumns)
    {
        this.lstViewXamlColumns = lstViewXamlColumns;
    }

    //public event Action<int, int> MultiCheck;


    public void CheckBox_Click(object sender, RoutedEventArgs e, Checkboxes chb2)
    {
        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        {
            var chb = (CheckBox)sender;
            var lastId2 = BTS.ParseInt(chb.Tag.ToString());
            GridView2_MultiCheck(lastId, lastId2, chb2);
        }
        else
        {
            var chb = (CheckBox)sender;
            lastId = BTS.ParseInt(chb.Tag.ToString());
        }
    }

    public void GridView2_MultiCheck(int arg1, int arg2, Checkboxes chb2)
    {
        var p = NH.Sort<int>(arg1, arg2);
        p[1]++;
        // is already checked actully, so i dont negate
        var col = ((ObservableCollection<T>)lstViewXamlColumns.ItemsSource);
        var first = col.First(d => d.Id == arg1);

        bool setUp = false;
        switch (chb2)
        {
            case Checkboxes.IsChecked:
                setUp =  first.IsChecked;
                break;
            case Checkboxes.IsSelected:
                setUp = first.IsSelected;
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod(), chb2);
                break;
        }

        for (int i = p[0]; i < p[1]; i++)
        {
            first = col.FirstOrDefault(d => d.Id == i);
            if (!EqualityComparer<T>.Default.Equals(default(T), first))
            {
                switch (chb2)
                {
                    case Checkboxes.IsChecked:
                        if (first.Visibility == Visibility.Visible)
                        {
                            first.IsChecked = setUp;
                        }
                        
                        break;
                    case Checkboxes.IsSelected:
                        if (first.Visibility == Visibility.Visible)
                        {
                            first.IsSelected = setUp;
                        }
                        break;
                    default:
                        break;
                }
                
            }
            
        }
    }
}