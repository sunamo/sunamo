using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.Types
{
    public class SelectedCastHelper<T> : ISelectedT<T>
    {
        ISelectedT<T> selected = null;

        public SelectedCastHelper(ISelectedT<T> selected)
        {
            this.selected = selected;
        }

        public T SelectedItem => (T)selected.SelectedItem;
    }
}
