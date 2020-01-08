using SunamoMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SunamoTreeView
{
    public class Item : ViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        //public bool IsChecked { get; set; }
        public bool IsDirectory { get; set; }
        public byte TokensCount { get; set; }
        
        public bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }
        
    }
}
