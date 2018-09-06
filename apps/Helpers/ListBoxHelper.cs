using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Collections;
using Windows.System;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Core;
using sunamo;
using apps;
using System.Threading.Tasks;
using apps.AwesomeFont;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
using apps.Helpers;
using System.Collections.ObjectModel;

namespace apps
{
    public class ListBoxHelper<T> : SelectorHelper<T> where T : IIdentificator
    {
        /// <summary>
        /// EK, OOP.
        /// V�choz� pro A2 bylo SelectionMode.Extended
        /// </summary>
        /// <param name="lb"></param>
        public ListBoxHelper(ListBox lb, SelectionMode sm, ObservableCollection<SelectorHelperItem> boc) : base(lb, boc)
        {
                lb.SelectionMode = sm;
        }

        protected override void RemoveFromSelector(object o)
        {
            throw new NotImplementedException();
        }
    }

    public class ListBoxHelper : SelectorHelper
    {
        /// <summary>
        /// EK, OOP.
        /// V�choz� pro A2 bylo SelectionMode.Extended
        /// </summary>
        /// <param name="lb"></param>
        public ListBoxHelper(ListBox lb, SelectionMode sm, ObservableCollection<SelectorHelperItem> boc) : base(lb, boc)
        {
            lb.SelectionMode = sm;
        }

        protected override void RemoveFromSelector(object o)
        {
            throw new NotImplementedException();
        }
    }
}
