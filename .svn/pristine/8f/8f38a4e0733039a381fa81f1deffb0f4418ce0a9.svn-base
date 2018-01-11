using System.Windows.Forms;
using System;
namespace forms
{
    public class TBH
    {
        /// <summary>
        /// Toto nepou��vej, m�sto toho pou�ij ovl�dac� prvek TextBoxForms
        /// </summary>
        /// <param name="txt"></param>
        public static void RegisterHandlerSelectAll(TextBox txt)
        {
            txt.KeyDown += new KeyEventHandler(txt_KeyDown);
        }

        static void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.A)
                {
                    ((TextBox)sender).SelectAll();
                }
            }
        }

        #region WPF
        #endregion
    }
}
