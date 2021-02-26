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
    public static void Validate(this TextBox control, object tb, ref ValidateData d)
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

        var tbTos = TextBlockHelper.TextOrToString(tb);

        if (d.validateMethod != null)
        {
            // ContainsInvalidFileNameChars return true if fails, therefore here cant be!
            if (!d.validateMethod(text))
            {
                if (d.messageWhenValidateMethodFails == null)
                {
                    d.messageWhenValidateMethodFails = tbTos + " must be filled";
                }

                d.messageToReallyShow = d.messageWhenValidateMethodFails;
                validated = false;
                return;
            }
        }


        if (CA.IsEqualToAnyElement<string>(text.Trim(), d.excludedStrings))
        {
            InitApp.TemplateLogger.HaveUnallowedValue(tbTos);
            validated = false;
            return;
        }

        if (text == string.Empty && !d.allowEmpty)
        {
            InitApp.TemplateLogger.MustHaveValue(tbTos);
            validated = false;
        }
        else
        {
            validated = true;
        }
    }

    public static bool validated;

}