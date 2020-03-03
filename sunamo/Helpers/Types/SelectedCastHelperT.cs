using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.Types
{
    public class SelectedCastHelper<T> : ISelectedT<T>
    {
        private ISelectedT<T> _selected = null;

        public SelectedCastHelper(ISelectedT<T> selected)
        {
            _selected = selected;
        }

        public T SelectedItem => (T)_selected.SelectedItem;
    }
}