using System.Windows.Forms;
using System;
namespace forms
{
    public  delegate void updateLblStatus(Label lbl, string text);
    public  delegate void updateProgressBar(ToolStripProgressBar p, int value);
    public  delegate void enableButton(Button button, bool value);
    public  delegate void updateTsslStatus(ToolStripStatusLabel tssl, string text);
    public  delegate void addItemToListView(ListView lv, object item);
    public  delegate ListViewItem getListViewItemOnIndex(ListView lv, int dex);
    public  delegate ListViewItem.ListViewSubItem getListViewItemSubItemOnIndex(ListViewItem.ListViewSubItemCollection col, int dex);
    public  delegate ListViewItem.ListViewSubItem addSubItemToListViewItem(ListViewItem.ListViewSubItemCollection col, string s);
    public  delegate ListViewItem.ListViewSubItem addSubItemToListViewItem2(ListViewItem col, string s);
    public  delegate void setTextToListViewItemSubItem(ListViewItem.ListViewSubItem si, string text);


    /// <summary>
    /// Pak p��mo z k�du kde m�m ty prvky to vol�m t�eba takto:
    /// this.BeginInvoke(delegateStatusUpdate, lblResult, "Preview Loaded");
    /// 
    /// </summary>
    public static class IH
    {
        static IH()
        {
            delegateEnableButton = new enableButton(buttonUpdate);
            delegatePbarUpdate = new updateProgressBar(updateProgressBarValue);
            delegateLblUpdate = new updateLblStatus(updateLabelText);
            delegateTsslUpdate = new updateTsslStatus(updateTsslText);
        }

        public static updateLblStatus delegateLblUpdate;
        public static updateTsslStatus delegateTsslUpdate;
        public static updateProgressBar delegatePbarUpdate;
        public static enableButton delegateEnableButton;
        public static setTextInTextBoxForms delegateSetTextInTextBoxForms = new setTextInTextBoxForms(SetTextInTextBoxForms);
        public static getTextInTextBoxForms delegateGetTextInTextBoxForms = new getTextInTextBoxForms(GetTextInTextBoxForms);
        public static appendLineInTextBox delegateAppendLineInTextBox = new appendLineInTextBox(AppendLineInTextBox);
        public static appendLineInTextBoxForms delegateAppendLineInTextBoxForms = new appendLineInTextBoxForms(AppendLineInTextBoxForms);
        public static ClearAllNodesTreeView delegateClearAllNodesTreeView = new ClearAllNodesTreeView(ClearAllNodesTreeView);
        public static addItemToListView delegateAddItemToListView = new addItemToListView(AddItemToListView);
        public static getListViewItemOnIndex delegateGetListViewItemOnIndex = new getListViewItemOnIndex(GetListViewItemOnIndex);
        public static getListViewItemSubItemOnIndex delegateGetListViewItemSubItemOnIndex = new getListViewItemSubItemOnIndex(GetListViewItemSubItemOnIndex);
        public static addSubItemToListViewItem delegateAddSubItemToListViewItem = new addSubItemToListViewItem(AddSubItemToListViewItem);
        public static addSubItemToListViewItem2 delegateAddSubItemToListViewItem2 = new addSubItemToListViewItem2(AddSubItemToListViewItem2);
        public static setTextToListViewItemSubItem delegateSetTextToListViewItemSubItem = new setTextToListViewItemSubItem(SetTextToListViewItemSubItem);

        //
        public static void SetTextToListViewItemSubItem(ListViewItem.ListViewSubItem si, string text)
        {
            si.Text = text;
        }

        public static ListViewItem.ListViewSubItem AddSubItemToListViewItem2(ListViewItem col, string s)
        {
            return col.SubItems.Add(s);
            //return col.Add(s);
        }

        public static ListViewItem.ListViewSubItem AddSubItemToListViewItem(ListViewItem.ListViewSubItemCollection col, string s)
        {
            //return col.SubItems.Add(s);
            return col.Add(s);
        }

        public static ListViewItem.ListViewSubItem GetListViewItemSubItemOnIndex(ListViewItem.ListViewSubItemCollection col, int dex)
        {
            return col[dex];
        }

        public static ListViewItem GetListViewItemOnIndex(ListView lv, int dex)
        {
            return lv.Items[dex];
        }

        public static void AddItemToListView(ListView lv, object o)
        {
            lv.Items.Add((ListViewItem)o);
        }

        public static void ClearAllNodesTreeView(TreeView tv)
        {
            tv.Nodes.Clear();
        }

        public static void SetTextInTextBoxForms(TextBoxForms txt, string text)
        {
            txt.Text = text;
        }

        public static string GetTextInTextBoxForms(TextBoxForms txt)
        {
            return txt.Text;
        }

        public static void AppendLineInTextBox(TextBox txt, string text)
        {
            txt.AppendText(text + Environment.NewLine);
        }

        public static void AppendLineInTextBoxForms(TextBoxForms txt, string text)
        {
            txt.AppendText(text + Environment.NewLine);
        }

        public static void buttonUpdate(Button button, bool value)
        {
            button.Enabled = value;
        }

        /// <summary>
        /// Tato metoda je na WF, pro WPF pak updateProgressBarWpfValue
        /// </summary>
        /// <param name="p"></param>
        /// <param name="value"></param>
        public static void updateProgressBarValue(ToolStripProgressBar p, int value)
        {
            if (value > 100)
            {
                throw new Exception("Hodnota pro ProgressBar nem��e b�t vy��� ne� 100.");
                //value = 100;
            }
            p.Value = value;
        }

        public static void updateTsslText(ToolStripLabel lbl, string text)
        {
            lbl.Text = text;
        }

        public static void updateLabelText(Label lbl, string text)
        {
            lbl.Text = text;
        }
    }
}
