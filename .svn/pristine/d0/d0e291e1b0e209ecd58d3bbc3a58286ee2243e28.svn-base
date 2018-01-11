using System.Windows.Forms;
using System.IO;
using sunamo;

namespace forms
{
    public class TSCBHT<T> : TSCBH
    {
        public TSCBHT(ToolStripComboBox tscb)
            : base(tscb)
        {

        }

        public T SelectedT
        {
            get
            {
                return (T)SelectedO;
            }
        }
    }

    public class TSCBH
    {
        protected ToolStripComboBox tscb = null;
        public event EmptyHandler SelectedIndexChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tscb"></param>
        public TSCBH(ToolStripComboBox tscb)
        {
            this.tscb = tscb;
            tscb.SelectedIndexChanged += new System.EventHandler(tscb_SelectedIndexChanged);
            tscb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void tscb_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged();
            }
        }

        /// <summary>
        /// G zda byla v LB vybr. polozka.
        /// </summary>
        public object SelectedO
        {
            get
            {
                return tscb.SelectedItem;
            }
        }

        /// <summary>
        /// G zda byla v LB vybr. polozka.
        /// </summary>
        public bool Selected
        {
            get
            {
                string s = SelectedS;
                return !string.IsNullOrEmpty(s);
                //return lb;
            }
        }

        /// <summary>
        /// Nek. aut. Vybrana, musi se volat tedy az po.
        /// G Tr vybr. polozky.
        /// </summary>
        public string SelectedS
        {
            get
            {
                object o = tscb.SelectedItem;
                if (o == null)
                {
                    return null;
                }
                return o.ToString();
            }
            set
            {
                tscb.Items[tscb.SelectedIndex] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="mask"></param>
        /// <param name="cb"></param>
        public static void LoadFiles(AppFolders ap, string mask, ToolStripComboBox cb)
        {
            cb.Items.Clear();
            string[] files = Directory.GetFiles(AppData.GetFolder(ap), mask);
            files = FS.OnlyNames(files);
            cb.Items.AddRange(files);
        }
    }
}
