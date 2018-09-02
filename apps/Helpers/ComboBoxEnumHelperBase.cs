using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace apps
{
    public abstract class ComboBoxEnumHelperBase<T> : ComboBoxHelperBase<T>
    {
        public ComboBoxEnumHelperBase(ComboBox cb) : base(cb)
        {

        }

        protected abstract void AddItems();
        public abstract void SetValue(T sablonyProjektu);
        public abstract void SetValue(string cbi);
        public abstract void RemoveItem(T t);
        public abstract T GetSelected();
    }

    public abstract class ComboBoxHelperBase<T>
    {
        protected ComboBox cb = null;

        public ComboBoxHelperBase(ComboBox cb)
        {
            this.cb = cb;
        }
    }
}
