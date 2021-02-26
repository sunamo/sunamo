using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using sunamo.Essential;


namespace desktop.Controls
{
    public partial class SelectFolder : UserControl
    {
        public static Type type = typeof(SelectFolder);
        public static bool validated;

        public void Validate(object tbFolder, ref ValidateData d)
        {
            SelectFolder.Validate(tbFolder, this, ref d);
        }

        /// <summary>
        /// Before first calling I have to set validated = true
        /// Its instance to avoid include another namespace
        /// </summary>
        /// <param name="validated"></param>
        /// <param name="tb"></param>
        /// <param name="control"></param>
        /// <param name="trim"></param>
        public static void Validate(object tb, SelectFolder control, ref ValidateData d)
        {
            if (!validated)
            {
                return;
            }
            string text = RH.GetValueOfPropertyOrField( control, "SelectedFolder").ToString();

            text = text.Trim();

            if (text == string.Empty)
            {
                InitApp.TemplateLogger.MustHaveValue(TextBlockHelper.TextOrToString(tb));
                validated = false;
            }
            else if (!FS.ExistsDirectory(text))
            {
                InitApp.TemplateLogger.FolderDontExists(text);
                validated = false;
            }
            else
            {
                validated = true;
            }


        }
    }

}