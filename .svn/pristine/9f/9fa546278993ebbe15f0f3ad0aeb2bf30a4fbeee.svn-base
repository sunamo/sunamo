using System.Windows.Forms;

    public class TextBoxForms : TextBox
    {
        public TextBoxForms()
        {
            ScrollBars = ScrollBars.Vertical;
        }

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
