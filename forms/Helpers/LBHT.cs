using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
namespace forms
{
    public class LBHT_WF<T> : LBH_WF
    {
        public LBHT_WF(ListBox lbh)
            : base(lbh)
        {
        }

        public T SelectedT
        {
            get
            {
                return (T)SelectedO;
            }
        }

        public static List<T> GetItemsListT(ListBox.ObjectCollection oc)
        {
            List<T> vr = new List<T>();
            foreach (object var in oc)
            {
                if (var is T)
                {
                    vr.Add((T)var);
                }
            }
            return vr;
        }
    }
    /// <summary>
    /// Um. lepsi man. s LB.
    /// </summary>
    public class LBH_WF
    {
        public void AddRange(params object[] list)
        {
            lb.Items.AddRange(list);
        }

        /// <summary>
        /// Zkopiruje do schranky vsechny polozky v lb
        /// </summary>
        public void CopyToClipboard()
        {
            StringBuilder sb = new StringBuilder();
            foreach (object var in lb.Items)
            {
                sb.AppendLine(var.ToString());
            }
            Clipboard.SetText(sb.ToString());
        }

        #region DPP
        public event VoidObject ItemRemoved;
        /// <summary>
        /// LB, na kt. se kont.
        /// </summary>
        protected ListBox lb;

        #endregion

        #region base
        /// <summary>
        /// EK, OOP.
        /// </summary>
        /// <param name="lb"></param>
        public LBH_WF(ListBox lb)
        {
            this.lb = lb;

            lb.SelectionMode = SelectionMode.MultiExtended;
            lb.KeyDown += new KeyEventHandler(lb_KeyDown);
        }

        public bool runOne = false;
        public bool saveToClipboard = false;
        public bool removeOne = false;

        /// <summary>
        /// Back - otevrit v browseru
        /// Enter - ulozit do schranky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lb_KeyDown(object sender, KeyEventArgs e)
        {
            if (Selected)
            {
                #region Enter - Spusti akt. polozku v LB. Nepridava k ni nic.
                if (e.KeyCode == Keys.Enter)
                {
                    if (runOne)
                    {
                        Process.Start(SelectedS);
                    }
                }
                #endregion
                #region C - Ulozi do schr.
                else if (e.KeyCode == Keys.C)
                {
                    if (saveToClipboard)
                    {
                        Clipboard.SetText(SelectedS);
                    }
                }
                #endregion
                #region del - smaze tuto domenu
                else if (e.KeyCode == Keys.Delete)
                {
                    if (removeOne)
                    {
                        if (Selected)
                        {
                            if (ItemRemoved != null)
                            {
                                ItemRemoved(SelectedO);
                            }
                            
                        }
                    }
                }
                #endregion

            }
        }
        #endregion

        #region H
        /// <summary>
        /// G zda byla v LB vybr. polozka.
        /// </summary>
        public object SelectedO
        {
            get
            {
                return lb.SelectedItem;
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
                object o = lb.SelectedItem;
                if (o == null)
                {
                    return null;
                }
                return o.ToString();
            }
            set
            {
                lb.Items[lb.SelectedIndex] = value;
            }
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// Prida do pp lb polozku. Nekontroluje, zda jiz existuje.
        /// </summary>
        /// <param name="s"></param>
        public void Add(string s)
        {
            lb.Items.Add(s);
        }

        /// <summary>
        /// Odebere do pp lb polozku. Nekontroluje, zda jiz neexistuje.
        /// </summary>
        /// <param name="s"></param>
        public void Remove(string s)
        {
            lb.Items.Remove(s);
        }
        #endregion

        public List<string> ReturnPpk()
        {
            List<string> vr = new List<string>();
            foreach (object item in lb.Items)
            {
                vr.Add(item.ToString());
            }
            return vr;
        }

        public static List<string> GetSelectedListString(ListBox.SelectedObjectCollection selectedObjectCollection)
        {
            List<string> vr = new List<string>();
            foreach (object var in selectedObjectCollection)
            {
                vr.Add(var.ToString());
            }
            return vr;
        }

        public static List<T1> GetItemsListT<T1>(ListBox.ObjectCollection objectCollection)
        {
            List<T1> t1 = new List<T1>();
            foreach (T1 var in objectCollection)
            {
                t1.Add(var);
            }
            return t1;
        }

        public static List<string> GetItemsListString(ListBox.ObjectCollection objectCollection)
        {
            List<string> t1 = new List<string>();
            foreach (object var in objectCollection)
            {
                t1.Add(var.ToString());
            }
            return t1;
        }
    }
}
