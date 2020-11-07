using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class AutoIndexedObservableCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged, IIdentificator<int>
{
    int dex = 1;

    public AutoIndexedObservableCollection()
    {
        CollectionChanged += FullObservableCollectionCollectionChanged;
    }

    public List<int> CheckedIndexes()
    {
        //List<int> result = new List<int>();
        return this.Where(d => d.IsChecked == true).Select(r=> r.Id).ToList();
    }

    public List<T> CheckedElements()
    {
        return this.Where(d => d.IsChecked == true).ToList();
    }

    public AutoIndexedObservableCollection(IEnumerable<T> pItems) : this()
    {
        foreach (var item in pItems)
        {
            
            this.Add(item);
        }
    }

    public void AddRange(IEnumerable<T> t)
    {
        foreach (var item in t)
        {
            Add(item);
        }
    }
    public  new void Add(T item)
    {
        item.Id = dex++;
        base.Add(item);
    }

    private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Object item in e.NewItems)
            {
                if (item != null)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                }
            }
        }
        if (e.OldItems != null)
        {
            foreach (Object item in e.OldItems)
            {
                if (item != null)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                }
            }
        }
    }

    private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
        OnCollectionChanged(args);
    }
}