using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

/// <summary>
/// This is only one implement IList
/// Must be in desktop - Use WpfApp.cd and Dispatcher is not in standard 
/// </summary>
/// <typeparam name="T"></typeparam>
public class NotifyChangesCollection<T> : IList<T> where T : INotifyPropertyChanged
{
    public NotifyChangesCollection<T> c => this;

    /// <summary>
    /// Its collection due to use also ObservableCollection and so
    /// </summary>
    public Collection<T> l = null;
    public event Action<object, ListOperation, object> CollectionChanged;
    /// <summary>
    /// sender is chbl but in Tag are last clicked chb
    /// </summary>
    private object _sender;

    public EventOnArgs eoa = null;
    public EventOnArgs eoaWasChanged = new EventOnArgs(false);

    public void EventOn(EventOnArgs e)
    {
        this.eoa = e;
    }

    /// <summary>
    /// Into args you can insert sth like this, new ObservableCollection<NotifyPropertyChangedWrapper<CheckBox>>()
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="c"></param>
    public NotifyChangesCollection(object sender, Collection<T> c)
    {
        _sender = sender;
        l = c;
    }

    public T this[int index] { get => l[index]; set => l[index] = value; }

    public int Count => l.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        //if (item.ToString().Contains("<b>"+SunamoPageHelperSunamo.i18n(XlfKeys.AllYourPhotosEvenPrivate)+":</b>"))
        //{

        //}

        item.PropertyChanged += Item_PropertyChanged;
        //
        //WpfApp.
        WpfApp.cd.Invoke((Action)delegate // <--- HERE
        {
            //_matchObsCollection.Add(match);
            l.Add(item);
        });
        
        if (eoa.onAdd)
        {
            OnCollectionChanged(ListOperation.Add, item);
        }
    }

    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (eoa.onPropertyChanged)
        {
            T t = (T)sender;
            if (e.PropertyName == "IsChecked")
            {
                var tb = (ToggleButton)sender;
                bool? isChecked = (bool?)tb.GetValue(ToggleButton.IsCheckedProperty);

                if (isChecked.GetValueOrDefault())
                {
                    OnCollectionChanged(ListOperation.Checked, t);
                }
                else
                {
                    OnCollectionChanged(ListOperation.Unchecked, t);
                }
            }
            else
            {
                OnCollectionChanged(ListOperation.PropertyChanged, t);
            }
            
        }   
    }



    public void Clear()
    {
        l.Clear();
        if (eoa.onClear)
        {
            OnCollectionChanged(ListOperation.Clear, null);
        }
    }

    public bool Contains(T item)
    {
        return l.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        l.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return l.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return l.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        l.Insert(index, item);
        if (eoa.onAdd)
        {
            OnCollectionChanged(ListOperation.Insert, item);
        }
    }

    public bool Remove(T item)
    {
        bool vr = l.Remove(item);
        if (eoa.onRemove)
        {
            OnCollectionChanged(ListOperation.Remove, item);
        }
        return vr;
    }

    public void RemoveAt(int index)
    {
        l.RemoveAt(index);
        OnCollectionChanged(ListOperation.RemoveAt, index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return l.GetEnumerator();
    }

    
    public void OnCollectionChanged(ListOperation op, object data)
    {
        if (CollectionChanged != null)
        {
            // must be _sender, not this
            CollectionChanged(_sender, op, data);
        }

        switch (op)
        {
            case ListOperation.RemoveAt:
            case ListOperation.Remove:
                eoaWasChanged.onRemove = true;
                break;
            case ListOperation.Insert:
            case ListOperation.Add:
                eoaWasChanged.onAdd = true;
                break;
            case ListOperation.Clear:
                eoaWasChanged.onClear = true;
                break;
            case ListOperation.Checked:
                eoaWasChanged.onCheck = true;
                break;
            case ListOperation.Unchecked:
                eoaWasChanged.onUnCheck = true;
                break;

            case ListOperation.PropertyChanged:
                eoaWasChanged.onPropertyChanged = true;
                break;
            default:
                break;
        }
        
    }

    
}