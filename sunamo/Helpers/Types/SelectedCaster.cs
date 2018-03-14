using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.Types
{
    public class SelectedCastHelper<T> : ISelectedT<T>
    {
        ISelected selected = null;

        public SelectedCastHelper(ISelected selected)
        {
            this.selected = selected;
        }

        public T Selected => (T)selected.SelectedFile;
    }
}
