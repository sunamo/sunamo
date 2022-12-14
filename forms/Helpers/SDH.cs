using sunamo;

/// <summary>
/// Simple Dialog Helper
/// </summary>
using System.Windows.Forms;
using forms.Essential;
using sunamo.Essential;

public class SDH
{
    public static DialogResult Information(string msg)
    {
        return System.Windows.Forms.MessageBox.Show(msg, ThisApp.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static DialogResult Warning(string msg)
    {
        return System.Windows.Forms.MessageBox.Show(msg, ThisApp.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static DialogResult Error(string msg)
    {
        return System.Windows.Forms.MessageBox.Show(msg, ThisApp.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}