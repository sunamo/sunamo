using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

namespace System.Windows.Controls
{
    public static class ListBoxExtensions
    {
        public static bool validated;

        /// <summary>
        /// Before first calling I have to set validated = true
        /// </summary>
        /// <param name="validated"></param>
        /// <param name="tb"></param>
        /// <param name="control"></param>
        /// <param name="trim"></param>
        public static void Validate(this ListBox control, object tb, bool trim = true)
        {
            if (!validated)
            {
                return;
            }
            var count = control.SelectedItems.Count;

            if (count == 0)
            {
                InitApp.TemplateLogger.MustHaveValue(TextBlockHelper.TextOrToString(tb));
                validated = false;
            }
            else
            {
                validated = true;
            }
        }
    }
}
