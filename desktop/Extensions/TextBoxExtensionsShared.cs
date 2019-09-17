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
    public static void Validate(this TextBox control, object tb, ValidateData d = null)
    {
        if (!validated)
        {
            return;
        }

        if (d == null)
        {
            d = new ValidateData();
        }

        string text = control.Text;
        if (d.trim)
        {
            text = text.Trim();
        }

        if (CA.IsEqualToAnyElement<string>(text.Trim(), d.excludedStrings))
        {
            InitApp.TemplateLogger.HaveUnallowedValue(TextBlockHelper.TextOrToString(tb));
            validated = false;
            return;
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