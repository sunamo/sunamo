using System.Windows.Forms;
using System.Collections.Generic;
namespace forms
{
    /// <summary>
    /// 
    /// </summary>
    public class CHLBH
    {
        CheckedListBox chlb = null;
        public CHLBH(CheckedListBox chlb)
        {
            this.chlb = chlb;
        }

        public string[] AllSelectedS
        {
            get
            {
                List<string> vr = new List<string>();
                foreach (object item in chlb.CheckedItems)
                {
                    vr.Add(item.ToString());
                }
                return vr.ToArray();
            }
        }

        public object[] AllSelected
        {
            get
            {
                List<object> vr = new List<object>();
                foreach (object item in chlb.CheckedItems)
                {
                    vr.Add(item);
                }
                return vr.ToArray();
            }
            set
            {
                UnCheckAll();
                foreach (string item in value)
                {
                    int dex = chlb.Items.IndexOf(item);
                    chlb.SetItemChecked(dex, true);
                }
            }
        }

        public void UnCheckAll()
        {
            for (int i = 0; i < chlb.Items.Count; i++)
            {
                chlb.SetItemChecked(i, false);
            }
        }

        public void CheckAll()
        {
            for (int i = 0; i < chlb.Items.Count; i++)
            {
                chlb.SetItemChecked(i, true);
            }
        }
    }
}
