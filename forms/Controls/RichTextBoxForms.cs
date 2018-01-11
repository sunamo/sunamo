using System.Windows.Forms;
public class RichTextBoxForms : RichTextBox
{
    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);

        if (e.Control)
        {
            if (e.KeyCode == Keys.A)
            {
                this.SelectAll();
            }
        }
    }
}
