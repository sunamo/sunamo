using System.Windows.Forms;
using System;
using System.Collections.Generic;
namespace forms
{
    /// <summary>
    /// Generick� t��da t��dy TSDDBH - ToolStripDropDownButtonHelper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TSDDBHT<T> : TSDDBH
    {
        public TSDDBHT(ToolStripDropDownButton tsddb)
            : base(tsddb)
        { }

        public TSDDBHT(ToolStripDropDownButton tsddb, Array bs, T defaultValue)
            : base(tsddb)
        {
            originalToolTipText = tsddb.ToolTipText;
            AddValuesOfEnumAsItems(bs);
            SelectedO = defaultValue;
            tsddb.ToolTipText = originalToolTipText + " " + defaultValue.ToString();
        }

        public T SelectedT
        {
            get
            {
                return (T)SelectedO;
            }
        }
    }

    /// <summary>
    /// ToolStripDropDownButtonHelper
    /// </summary>
    public class TSDDBH
    {
        protected ToolStripDropDownButton tsddb = null;
        protected ToolStripMenuItem prev = new ToolStripMenuItem();
        protected string originalToolTipText = "";
        /// <summary>
        /// Objekt, ve kter�m je v�dy aktu�ln� zda v tsddb n�co je
        /// Tak�e se nelekni �e to je promm�n�
        /// </summary>
        public object SelectedO = null;

        public bool Selected
        {
            get
            {
                if (SelectedO != null)
                {
                    return SelectedO.ToString().Trim() != "";
                }
                return false;

            }
        }

        public string SelectedS
        {
            get
            {
                return SelectedO.ToString();
            }
        }

        public void AddValuesOfEnumAsItems(Array bs)
        {
            int i = 0;
            foreach (object item in bs)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = item.ToString();
                tsmi.Tag = item;
                if (i == 0)
                {
                    tsmi.Checked = true;
                    prev = tsmi;
                }
                tsmi.Click += new EventHandler(tsmi_Click);
                tsddb.DropDownItems.Add(tsmi);
                i++;
            }
        }

        public void tsmi_Click(object sender, EventArgs e)
        {
            prev.Checked = false;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            tsmi.Checked = true;
            prev = tsmi;
            if (tagy)
            {
                SelectedO = tsmi.Tag;
            }
            else
            {
                SelectedO = tsmi;
            }
            tsddb.ToolTipText = originalToolTipText + " " + SelectedO.ToString();
        }

        public void AddValuesOfArrayAsItems(EventHandler eh, params object[] o)
        {
            int i = 0;
            foreach (object item in o)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = item.ToString();
                tsmi.Tag = item;
                tsmi.Click += eh;
                tsddb.DropDownItems.Add(tsmi);
                i++;
            }
        }

        public void AddValuesOfIntAsItems(EventHandler eh, int initialValue, int resizeOf, int degrees)
        {
            int akt = initialValue;
            List<int> pred = new List<int>();
            for (int i = 0; i < degrees; i++)
            {
                akt -= resizeOf;
                pred.Add(akt);

            }
            pred.Reverse();
            akt = initialValue;
            List<int> po = new List<int>();
            for (int i = 0; i < degrees; i++)
            {
                akt += resizeOf;
                pred.Add(akt);
            }
            List<int> o = new List<int>();
            o.AddRange(pred);
            o.Add(initialValue);
            o.AddRange(po);
            int y = 0;
            foreach (int item in o)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = item.ToString();
                tsmi.Tag = item;
                tsmi.Click += eh;
                tsddb.DropDownItems.Add(tsmi);
                y++;
            }
        }

        bool tagy = true;

        public TSDDBH(ToolStripDropDownButton tsddb, bool tagy)
        {
            this.tsddb = tsddb;
            this.tagy = tagy;
        }

        public TSDDBH(ToolStripDropDownButton tsddb)
        {
            this.tsddb = tsddb;
            tagy = true;
        }
    }
}
