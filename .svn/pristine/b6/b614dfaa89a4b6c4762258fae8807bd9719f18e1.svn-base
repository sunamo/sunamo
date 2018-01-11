using System.Windows.Forms;
using System.Collections.Generic;
using System;
public class SearchingInLbWF
{
    /// <summary>
    /// ListBox ve kter�m se ukazuj� v�sledky
    /// </summary>
    ListBox lb = null;
    /// <summary>
    /// TextBox do kter�ho byl zadan� hledan� v�raz
    /// </summary>
    System.Windows.Forms.ToolStripTextBox tstb = null;
    /// <summary>
    /// V�choz� polo�ky. Nahraj� se do LB po stornov�n� hled�n�.
    /// </summary>
    object[] oc = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lb"></param>
    /// <param name="tstb"></param>
    public SearchingInLbWF(ListBox lb, System.Windows.Forms.ToolStripTextBox tstb, ToolStripButton toolStripButton2, ToolStripMenuItem tsmi)
    {
        this.lb = lb;
        this.tstb = tstb;
        tstb.TextChanged += new System.EventHandler(tstb_TextChanged);
        tstb.KeyDown += new KeyEventHandler(tstb_KeyDown);
        toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
        tsmi.Click += new EventHandler(tsmi_Click);
        List<object> f = new List<object>();
        foreach (object var in lb.Items)
        {
            f.Add(var);
        }
        oc = f.ToArray();
    }

    void tsmi_Click(object sender, EventArgs e)
    {
        tstb.Text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tstb_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Back)
        {
            tstb.Text = "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="zapnuto"></param>
    public void Searching(bool zapnuto)
    {
        if (zapnuto)
        {
            List<object> nechat = new List<object>();
            foreach (object var in oc)
            {
                if (var.ToString().Contains(tstb.Text))
                {
                    nechat.Add(var);
                }
            }
            lb.Items.Clear();
            lb.Items.AddRange(nechat.ToArray());
        }
        else
        {
            lb.Items.Clear();
            lb.Items.AddRange(oc);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        tstb.Text = "";
    }

    void tstb_TextChanged(object sender, System.EventArgs e)
    {
        if (tstb.Text == "")
        {
            Searching(false);
        }
        else
        {
            Searching(true);
        }
    }
}
