using System.Windows.Controls;
using sunamo.Essential;

public static partial class TextBoxExtensions{ 
/// <summary>
    /// Before first calling I have to set validated = true
    /// </summary>
    /// <param name = "validated"></param>
    /// <param name = "tb"></param>
    /// <param name = "control"></param>
    /// <param name = "trim"></param>
    public static void Validate(this TextBox control, object tb, bool trim = true)
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

    public static bool validated;

}