using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace F.WF
{
    public partial class DialogLogin : Form
    {
        //bool publicSaveLogic = false;
        //const string h = "h";
        //const string l = "l";

        ///// <summary>
        ///// 
        ///// </summary>
        //public DialogLogin()
        //{
        //    InitializeComponent();
        //}

        ///// <summary>
        ///// A1 je vhodna tehdy kdyz napriklad poustim python skripty, ve kterych nemuzu overit zda se mi podarilo nalogovat
        ///// </summary>
        ///// <param name="publicSaveLogic"></param>
        //public DialogLogin(bool publicSaveLogic)
        //    : this()
        //{
        //    this.publicSaveLogic = publicSaveLogic;
        //    if (publicSaveLogic)
        //    {
        //        this.txtHeslo.Text = RA.ReturnValueString(h);
        //        this.txtLogin.Text = RA.ReturnValueString(l);
        //        this.chbUlozHeslo.Checked = this.txtHeslo.Text != "";
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //    if (publicSaveLogic)
            //    {
            //        RA.WriteToKeyString(h, "");
            //        RA.WriteToKeyString(l, this.txtLogin.Text);

            //        if (this.chbUlozHeslo.Checked)
            //        {
            //            RA.WriteToKeyString(h, this.txtHeslo.Text);
            //        }

            //        if (txtLogin.Text.Trim() != "" && txtHeslo.Text.Trim() != "")
            //        {
            //            DialogResult = System.Windows.Forms.DialogResult.OK;
            //        }
            //        else
            //        {
            //            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //        }

            //    }
            //    else
            //    {
            //        DialogResult = System.Windows.Forms.DialogResult.OK;
            //    }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        //public bool HaveLoginedData()
        //{
        //    string he = RA.ReturnValueString(h);
        //    string lo = RA.ReturnValueString(l);

        //    return he != "" && lo != "";
        //}
    }
}