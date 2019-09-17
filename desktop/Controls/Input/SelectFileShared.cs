using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using sunamo.Essential;

namespace desktop.Controls
{
    public partial class SelectFile : UserControl
    {
    public static Type type = typeof(SelectFile);

    public void Validate(object tbNewPath, ValidateData d = null)
    {
            string SelectedFile = RH.GetValueOfPropertyOrField(this, "SelectedFile").ToString();

        validated = FS.ExistsFile(SelectedFile);
        if (!validated)
        {
            InitApp.TemplateLogger.FileDontExists(SelectedFile);
        }
    }

    string selectedFile = "";
    public static bool validated;
}

}