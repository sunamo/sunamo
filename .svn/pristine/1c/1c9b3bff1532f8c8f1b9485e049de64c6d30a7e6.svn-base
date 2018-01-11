//AlphaNumericTextBox
using System;
using System.Windows.Forms;
public class AlphaNumericTextBox : TextBoxForms
{
    private string mPrevious;

    public AlphaNumericTextBox() { }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        mPrevious = this.Text;
        e.Handled = !char.IsWhiteSpace(e.KeyChar);// && !char.IsControl(e.KeyChar);
    }

    protected override void OnTextChanged(EventArgs e)
    {
    }

    public int Number
    {
        get
        {
            int dd = 0;
            if (int.TryParse(Text, out dd))
            {
                return dd;
            }
            return 0;
        }
    }

    protected override void OnLeave(EventArgs e)
    {
    }
}
