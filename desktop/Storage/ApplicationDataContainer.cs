using desktop.Controls.Collections;
using desktop.Controls.Input;
using desktop.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Key is filename
/// Value is ApplicationDataContainerList (every instance has unique file)
/// </summary>
public class ApplicationDataContainer : ApplicationDataConsts
{
    /// <summary>
    /// In key are control
    /// In value its saved values
    /// Must be bcoz every line has strictly structure - name|type|data. Never be | in data. Access through methods Get / Set
    /// Never direct access also in this class!! 
    /// </summary>
    private Dictionary<object, ApplicationDataContainerList> data = new Dictionary<object, ApplicationDataContainerList>();
    readonly Type type = typeof(Type);
    string file = "";
    public string innerDelimiter = AllStrings.asterisk;

    /// <summary>
    /// ctor for old approach
    /// </summary>
    /// <param name="file"></param>
    public ApplicationDataContainer(string file)
    {
        this.file = file;
        data.Add(file, new ApplicationDataContainerList(file));
    }

    /// <summary>
    /// Must be here due to sunamo.Tests
    /// </summary>
    public ApplicationDataContainerList Values
    {
        get
        {
            if (data.ContainsKey(file))
            {
                var list = data[file];
                return list;
            }
            return null;
        }
    }

    public ApplicationDataContainer()
    {

    }

    public void Add(Window cb)
    {
        // Automatically load
        var adcl = AddFrameworkElement(cb);
        var list = adcl.GetListString(ItemsSource);
        
        
        //cb.ItemsSource = list;
        //cb.KeyUp += Cb_KeyUp;
        //cb.DataContextChanged += Cb_DataContextChanged;
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
        ApplicationDataContainerList adcl = AddFrameworkElement(chb);
        chb.Click += Chb_Click;
        chb.IsChecked = adcl.GetNullableBool( IsChecked);
    }

    public void Add(TextBox txt)
    {
        var adcl = AddFrameworkElement(txt);
        txt.Text = adcl.GetString(Text);
        txt.TextChanged += Txt_TextChanged;
    }

    public void Add(CheckBoxListUC chbl)
    {
        var adcl = AddFrameworkElement(chbl);
        var list = adcl.GetListString(chbAdded, innerDelimiter);
        for (int i = 0; i < list.Count; i++)
        {
            var chb = CheckBoxHelper.Get(new ControlInitData { text = list[i] });
            chb.IsChecked = BTS.IntToBool(list[++i]);
            chbl.l.l.Add(chb);
        }
        chbl.l.CollectionChanged += Chbl_CollectionChanged;
    }

    private void Chbl_CollectionChanged(object sender, string operation, object data)
    {
        CheckBoxListUC chb = sender as CheckBoxListUC;

        InstantSB sb = new InstantSB(innerDelimiter);
        foreach (var item in chb.l.l)
        {
            sb.AddItem(item.Content);
            sb.AddItem(BTS.BoolToInt(item.IsChecked.Value));
        }

        Set(sender, chbAdded, sb.ToString());

        SaveControl(chb);
    }

    public void Add(SelectMoreFolders txtFolders)
    {
        var adcl = AddFrameworkElement(txtFolders);
        var folders = adcl.GetListString(SelectedFolders, innerDelimiter);
        foreach (var item in folders)
        {
            txtFolders.AddFolder(item);
        }
        txtFolders.FolderChanged += TxtFolders_FolderChanged;
        txtFolders.FolderRemoved += TxtFolders_FolderRemoved;
        
    }

    private void Txt_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox chb = sender as TextBox;
        Set(sender, Text, chb.Text);

        SaveControl(chb);
    }

    private void TxtFolders_FolderRemoved(object sender, List<string> selectedFolders)
    {
        SaveChangesSelectMoreFolders(sender, selectedFolders);
    }

    private void TxtFolders_FolderChanged(object sender, List<string> selectedFolders)
    {
        SaveChangesSelectMoreFolders(sender, selectedFolders);
    }

    private void SaveChangesSelectMoreFolders(object sender, List<string> selectedFolders)
    {
        SelectMoreFolders chb = sender as SelectMoreFolders;
        // bcoz every line has strictly structure - name|type|data. Never be | in data
        Set( sender,SelectedFolders, SF.PrepareToSerialization(selectedFolders, innerDelimiter));
        
        SaveControl(chb);
    }

    public T Get<T>(object sender, string key)
    {
        
            var v = data[sender];
            if (v.Contains(key))
            {
                return (T)v[key];
            }
        
        return default(T);
    }

    public object Get(object sender, string key)
    {
        return data[sender][key];
    }

    public void Set(object sender, string key, object v)
    {
        ThrowExceptions.StringContainsUnallowedSubstrings(type, "Set", v.ToString(), AllStrings.pipe);
        data[sender][key] = v;
    }

    

    public void Add(TextBlock tb)
    {
        var tb2 = AddFrameworkElement(tb);
        //tb.TextChanged +=
    }

    private void Chb_Click(object sender, RoutedEventArgs e)
    {
        CheckBox chb = sender as CheckBox;
        Set(sender,IsChecked, chb.IsChecked);
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
            Set(sender,ItemsSource, list);
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

