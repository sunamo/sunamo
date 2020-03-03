
using SunamoMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SunamoTreeView
{
    public class ItemsViewModel : ViewModel
    {
        //private readonly ObservableCollection<ItemViewModel> _customers;
        private readonly ICommand _tickInThisItemAllCommand;
        private readonly ICommand _tickInThisItemAndUnderAllCommand;
        private readonly ICommand _untickInThisItemAllCommand;
        private readonly ICommand _untickInThisItemAndUnderAllCommand;

        public static Window MainWindow;

        public ItemsViewModel()
        {
            //_customers = new ObservableCollection<CustomerViewModel>();
            _tickInThisItemAllCommand = new DelegateCommand(TickInThisItemAll, CanTickOrUntickAll);
            _tickInThisItemAndUnderAllCommand = new DelegateCommand(TickInThisItemAndUnderAll, CanTickOrUntickUnderAll);
            _untickInThisItemAllCommand = new DelegateCommand(UntickInThisItemAll, CanTickOrUntickAll);
            _untickInThisItemAndUnderAllCommand = new DelegateCommand(UntickInThisItemAndUnderAll, CanTickOrUntickUnderAll);
        }

        /// <summary>
        /// TODO: ItemViewModel
        /// </summary>
        /// <summary>
        /// TODO: ItemViewModel
        /// </summary>
        public static ObservableCollection<Item> Items
        {
            get
            {
                return ItemProvider.LastItems;
            }
        }

        public ICommand UntickInThisItemAllCommand
        {
            get
            {
                return _untickInThisItemAllCommand;
            }
        }
        public ICommand UntickInThisItemAndUnderAllCommand
        {
            get
            {
                return _untickInThisItemAndUnderAllCommand;
            }
        }

        public ICommand TickInThisItemAllCommand
        {
            get
            {
                return _tickInThisItemAllCommand;
            }
        }

        public ICommand TickInThisItemAndUnderAllCommand
        {
            get
            {
                return _tickInThisItemAndUnderAllCommand;
            }
        }

        private bool CanTickOrUntickAll(object state)
        {
            return ((CheckBox)MainWindow.Tag) != null;
            //return Items.Count > 0;
        }

        private bool CanTickOrUntickUnderAll(object state)
        {
            return ((CheckBox)MainWindow.Tag) != null;
            //return Items.Count > 0;
        }


        private void TickInThisItemAll(object state)
        {
            List<string> s = new List<string>();
            s.AddRange(GetThisAndUnderFiles(((CheckBox)MainWindow.Tag).Tag.ToString()));
            TickOrUntick(s, true);
        }

        public static void TickOrUntick(List<string> paths, bool tick)
        {
            foreach (var item in ItemProvider.LastItemsNonHiearchic)
            {
                foreach (var item2 in paths)
                {
                    if (item.Path == item2)
                    {
                        item.IsChecked = tick;
                    }
                }
            }
        }

        private void TickInThisItemAndUnderAll(object state)
        {
            List<string> s = new List<string>();
            foreach (var item in ItemProvider.LastItemsNonHiearchic)
            {
                if (item.Path.StartsWith(((CheckBox)MainWindow.Tag).Tag.ToString()))
                {
                    s.Add(item.Path);
                }

            }
            TickOrUntick(s, true);
        }

        private void UntickInThisItemAll(object state)
        {
            List<string> s = new List<string>();
            s.AddRange(GetThisAndUnderFiles(((CheckBox)MainWindow.Tag).Tag.ToString()));
            TickOrUntick(s, false);
        }

        private IEnumerable<string> GetThisAndUnderFiles(string p)
        {
            //List<string> dd = new List<string>();
            byte bl = ((byte)p.Split(AllChars.bs).Length);
            if (!p.EndsWith(AllStrings.bs))
            {
                bl++;    
            }
            
            foreach (var item in ItemProvider.LastItemsNonHiearchic)
            {
                if (item.TokensCount == bl && !item.IsDirectory && item.Path.StartsWith(p))
                {
                    yield return item.Path;
                }
            }
        }

        private void UntickInThisItemAndUnderAll(object state)
        {
            List<string> s = new List<string>();
            foreach (var item in ItemProvider.LastItemsNonHiearchic)
            {
                if (item.Path.StartsWith(((CheckBox)MainWindow.Tag).Tag.ToString()))
                {
                    s.Add(item.Path);
                }

            }
            TickOrUntick(s, false);
        }

    }
}