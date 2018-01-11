using System.Windows.Forms;
namespace forms
{
    public class LVH
    {
        public static ListViewItem.ListViewSubItem GetListViewSubItemOnIndex(ListView lv, int id, int dex)
        {
            if (lv.Items.Count > id)
            {
                ListViewItem lic = GetListViewItemOnIndex(lv, id);
                ListViewItem.ListViewSubItemCollection lvsic = lic.SubItems;
                if (lvsic.Count > dex)
                {
                    return (ListViewItem.ListViewSubItem)lv.Invoke(IH.delegateGetListViewItemSubItemOnIndex, lic.SubItems, dex);
                }
            }
            return (ListViewItem.ListViewSubItem)lv.Invoke(IH.delegateAddSubItemToListViewItem2, GetListViewItemOnIndex(lv, id), "");
        }

        private static ListViewItem GetListViewItemOnIndex(ListView lv, int id)
        {
            ListViewItem lic = (ListViewItem)lv.Invoke(IH.delegateGetListViewItemOnIndex, lv, id); //lv.Items[id];
            return lic;
        }
    }
}
