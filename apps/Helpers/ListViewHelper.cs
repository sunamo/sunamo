using apps;
using apps.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace apps
{
    public class ListViewHelper<T, U> : SelectorHelper<T, U> where T :IIdentificator
    {
        /// <summary>
        /// EK, OOP.
        /// Výchozí pro A2 bylo SelectionMode.Extended
        /// </summary>
        /// <param name="lb"></param>
        public ListViewHelper(ListView lb, ListViewSelectionMode sm, ObservableCollection<SelectorHelperItem> boc) : base(lb, boc)
        {
            lb.SelectionMode = sm;
        }

        protected override void RemoveFromSelector(object o)
        {
            if (oc != null)
            {
                for (int i = 0; i < oc.Count; i++)
                {
                    if (EqualityComparer<U>.Default.Equals( SelectedU, (U)o))
                    {
                        oc.RemoveAt(i);
                    }
                }
            }
            else
            {
                selector.Items.Remove(o);
            }
            UpdateItemsSource();
        }
    }

    public class ListViewHelper<T> : SelectorHelper<T>
        {
            /// <summary>
            /// EK, OOP.
            /// Výchozí pro A2 bylo SelectionMode.Extended
            /// </summary>
            /// <param name="lb"></param>
            public ListViewHelper(ListView lb, ListViewSelectionMode sm, ObservableCollection<SelectorHelperItem> boc) : base(lb, boc)
            {
                lb.SelectionMode = sm;
            }

        protected override void RemoveFromSelector(object o)
        {
            if (oc != null)
            {
                for (int i = 0; i < oc.Count; i++)
                {
                    if ((oc[i] as SelectorHelperItem).Id == o)
                    {
                        oc.RemoveAt(i);
                    }
                }
            }
            else
            {
                selector.Items.Remove(o);
            }
            UpdateItemsSource();
        }
    }

        public class ListViewHelper : SelectorHelper
        {
            /// <summary>
            /// EK, OOP.
            /// Výchozí pro A2 bylo SelectionMode.Extended
            /// </summary>
            /// <param name="lb"></param>
            public ListViewHelper(ListView lb, ListViewSelectionMode sm, ObservableCollection<SelectorHelperItem> boc) : base(lb, boc)
            {
                lb.SelectionMode = sm;
            }

        protected override void RemoveFromSelector(object o)
        {
            if (oc != null)
            {
                oc.Remove((SelectorHelperItem)o);

            }
            else
            {
                selector.Items.Remove(o);
            }
            UpdateItemsSource();
        }
    }
    
}
