using desktop.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

public class ApplicationDataContainer : ApplicationDataConsts
{
    public Dictionary<object, ApplicationDataContainerList> data = new Dictionary<object, ApplicationDataContainerList>();
    readonly Type type = typeof(Type);
    string file = "";

    /// <summary>
    /// ctor for old approach
    /// </summary>
    /// <param name="file"></param>
    public ApplicationDataContainer(string file)
    {
        this.file = file;
        data.Add(file, new ApplicationDataContainerList(file));
    }

    public ApplicationDataContainer()
    {

    }

    public ApplicationDataContainerList Values
    {
        get
        {
            var list = data[file];
            return list;
        }
    }

    public void Add(ComboBox cb)
    {
        // Automatically load
        var adcl = AddFrameworkElement(cb);
        var list = adcl.GetListString(ItemsSource);
        cb.ItemsSource = list;
        cb.KeyUp += Cb_KeyUp;
        cb.DataContextChanged += Cb_DataContextChanged;
        
    }

    public void Add(CheckBox chb)
    {
        var adcl = AddFrameworkElement(chb);
        chb.Click += Chb_Click;
        chb.IsChecked = adcl.GetNullableBool( IsChecked);
    }

    private void Chb_Click(object sender, RoutedEventArgs e)
    {
        CheckBox chb = sender as CheckBox;
        data[sender][IsChecked] = chb.IsChecked;
        SaveControl(chb);
    }

    

    private void Cb_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        var cb = sender as ComboBox;
        if (e.Key == System.Windows.Input.Key.Enter)
        {
            //var items = cb.Items;
            //var itemsS = cb.ItemsSource;
            List<string> list = AddToListString(cb.ItemsSource, cb.Text);
            cb.ItemsSource = list;
            data[sender][ItemsSource] = list;
            SaveControl(cb);
        }
    }

    private List<string> AddToListString(object list, string text)
    {
        var list2 = ((List<string>)list);
        list2.Add(text);
        return list2;
    }

    private void Cb_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        //SaveControl(sender);
    }

    public ApplicationDataContainerList AddFrameworkElement(FrameworkElement fw)
    {
        ApplicationDataContainerList result = new ApplicationDataContainerList(fw);
        data.Add(fw, result);
        return result;
    }

    public void SaveControl(object o)
    {
        FrameworkElement fw = (FrameworkElement)o;
        if (fw != null)
        {
            data[fw].SaveFile();
        }
        else
        {
            ThrowExceptions.DoesntHaveRequiredType(type, "SaveControl", "o");
        }
    }
}

