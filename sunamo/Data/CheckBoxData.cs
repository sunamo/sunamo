using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class CheckBoxData<T>
    {
        /// <summary>
        /// Set to IsChecked when TwoWayTable.DataCellWrapper == AddBeforeControl.CheckBox
        /// </summary>
        public bool? tick = false;
        /// <summary>
        /// Na to co se má zobrazit
        /// </summary>
        public T t = default(T);
    }
}