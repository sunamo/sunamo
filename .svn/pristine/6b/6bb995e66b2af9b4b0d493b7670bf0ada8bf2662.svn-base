using System.Windows.Forms;
using System;
namespace forms
{
    public class TSTBH
    {
        public System.Windows.Forms.ToolStripTextBox tstb = null;
        public event KeyEventHandler PressEnter;
        bool clearAfterClick = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tstb"></param>
        public TSTBH(System.Windows.Forms.ToolStripTextBox tstb, bool vymazatPoKliku)
        {
            this.tstb = tstb;
            this.clearAfterClick = vymazatPoKliku;
            tstb.Click += new System.EventHandler(tstb_Click);
            tstb.KeyDown += new KeyEventHandler(tstb_KeyDown);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tstb_KeyDown(object sender, KeyEventArgs e)
        {
            if (tstb.Text.Trim() != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    PressEnter(tstb.Text, e);
                    if (!clearAfterClick)
                    {
                        tstb.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tstb_Click(object sender, System.EventArgs e)
        {
            if (clearAfterClick)
            {
                tstb.Text = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tstb"></param>
        public static TSTBH NastavTSTB(ToolStripTextBox tstb, bool cleanAfterClick)
        {
            TSTBH h = new TSTBH(tstb, cleanAfterClick);
            return h;
        }
    }
}
