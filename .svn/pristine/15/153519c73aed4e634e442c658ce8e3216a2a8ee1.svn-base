using System.Windows.Forms;
public class ToolStripTextBox : System.Windows.Forms.ToolStripTextBox
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
