/// <summary>
/// 
/// </summary>
using System.Windows.Forms;
using System;
namespace forms
{
    /// <summary>
    /// ToolStripMenuItemHelper
    /// </summary>
    public class TSMIH
    {
        ToolStripMenuItem tsi = null;

        public TSMIH(ToolStripMenuItem tsi)
        {
            this.tsi = tsi;
        }

        public void AddValuesOfEnumAsItems(Array bs, EventHandler eh)
        {
            foreach (object item in bs)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = item.ToString();
                tsmi.Tag = item;
                tsmi.Click += eh;
                tsi.DropDownItems.Add(tsmi);
            }
        }

        public static ToolStripMenuItem CreateNew(string p)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = p;
            return tsmi;
        }
    }
}
