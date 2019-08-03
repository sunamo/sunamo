﻿using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Controls
{
    public static class ComboBoxExtensions
    {
        public static bool validated;

        /// <summary>
        /// Before first calling I have to set validated = true
        /// </summary>
        /// <param name="validated"></param>
        /// <param name="tb"></param>
        /// <param name="control"></param>
        /// <param name="trim"></param>
        public static void Validate(this ComboBox control, object tb, bool trim = true)
        {
            if (!validated)
            {
                return;
            }
            string text = control.Text;
            if (trim)
            {
                text = text.Trim();
            }
            if (text == string.Empty)
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
