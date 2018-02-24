//using System.Windows.Forms;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Collections;
using sunamo.Interfaces;

namespace desktop
{
    public delegate void VoidMouseButtonGeneric1<in T>(MouseButton mb, T t);

    public class LBHT<T> : LBH
    {
        /// <summary>
        /// V�choz� pro A2 bylo SelectionMode.Extended
        /// </summary>
        /// <param name="lb"></param>
        /// <param name="sm"></param>
        public LBHT(ListBox lb, SelectionMode sm)
            : base(lb, sm)
        {
            lb.SelectionChanged += Lb_SelectionChanged;
            ItemRemoved += LBHT_ItemRemoved;

            lb.PreviewMouseDoubleClick += Lb_MouseDoubleClick;
        }

        private void LBHT_ItemRemoved(object o)
        {
            ItemRemovedT((T)o);
        }

        public event VoidT<T> ItemRemovedT;

        private void Lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveSelectedItem();
        }

        private void SaveSelectedItem()
        {
            if (lb.SelectedItem is T)
            {
                T t = (T)lb.SelectedItem;
                SaveSelectedItem(t);
            }
            else if (lb.SelectedItem is FrameworkElement)
            {
                // Vlastnost Tag je ve t��d� FrameworkElement
                FrameworkElement fw = lb.SelectedItem as FrameworkElement;
                if (fw.Tag is T)
                {
                    T t = (T)fw.Tag;
                    SaveSelectedItem(t);
                }
            }
        }

        private void SaveSelectedItem(T t)
        {
            Selected = t;
            if (this.MouseDown != null)
            {
                this.MouseDown(mb, t);
            }
        }

        private void Lb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mb = e.ChangedButton;
            SaveSelectedItem();
            if (IsSelected)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (runOne)
                    {
                        RunSelected();
                    }
                }
            }
        }

        public event VoidMouseButtonGeneric1<T> MouseDown;

        public T SelectedT
        {
            get
            {
                return (T)SelectedO;
            }
        }

        public static List<T> GetItemsListT(ItemCollection oc)
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
    public class LBH
    {
        protected object Selected = null;

        public static void AddRange2List(ListBox lb, IList il)
        {
            foreach (var item in il)
            {
                lb.Items.Add(item);
            }
        }
        public static void AddRange2(ListBox lb, params object[] list)
        {
            foreach (var item in list)
            {
                lb.Items.Add(item);
            }
        }

        public void AddRange(params object[] list)
        {
            foreach (var item in list)
            {
                lb.Items.Add(item);
            }
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

        protected MouseButton mb = MouseButton.XButton1;

        private void Lb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mb = e.ChangedButton;   
        }

        #region base
        /// <summary>
        /// EK, OOP.
        /// V�choz� pro A2 bylo SelectionMode.Extended
        /// </summary>
        /// <param name="lb"></param>
        public LBH(ListBox lb, SelectionMode sm)
        {
            this.lb = lb;

            lb.SelectionMode = sm;
            lb.KeyDown += new KeyEventHandler(lb_KeyDown);
            lb.PreviewMouseDown += Lb_MouseDown;
        }

        public bool Tag = false;
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
            
            if (IsSelected)
            {
                #region Enter - Spusti akt. polozku v LB. Nepridava k ni nic.
                if (e.Key == Key.Enter)
                {
                    if (runOne)
                    {
                        RunSelected();
                    }
                }
                #endregion
                #region C - Ulozi do schr.
                else if (e.Key == Key.C)
                {
                    if (saveToClipboard)
                    {
                        Clipboard.SetText(SelectedS);
                    }
                }
                #endregion
                #region del - smaze tuto domenu
                else if (e.Key == Key.Delete)
                {
                    if (removeOne)
                    {
                        if (IsSelected)
                        {
                            if (ItemRemoved != null)
                            {
                                ItemRemoved(SelectedO);
                            }
                            lb.Items.Remove(SelectedO);
                        }
                    }
                }
                #endregion
            }
        }

        protected void RunSelected()
        {
            if (Selected is IListBoxHelperItem)
            {
                IListBoxHelperItem lbi = Selected as IListBoxHelperItem;
                ProcessHelper.Start(lbi.RunOne);
            }
            else
            {
                ProcessHelper.Start(SelectedS);
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
                return Selected;
                //return lb.SelectedItem;
            }
        }

        /// <summary>
        /// G zda byla v LB vybr. polozka.
        /// </summary>
        public bool IsSelected
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
                //object o = lb.SelectedItem;
                object o = Selected;
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
        public void Add(object s)
        {
            lb.Items.Add(s);
        }

        /// <summary>
        /// Odebere do pp lb polozku. Nekontroluje, zda jiz neexistuje.
        /// </summary>
        /// <param name="s"></param>
        public void Remove(object s)
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

        public static List<string> GetSelectedListString(IList selectedObjectCollection)
        {
            List<string> vr = new List<string>();
            foreach (object var in selectedObjectCollection)
            {
                vr.Add(var.ToString());
            }
            return vr;
        }

        public static List<T1> GetItemsListT<T1>(ItemCollection objectCollection)
        {
            List<T1> t1 = new List<T1>();
            foreach (T1 var in objectCollection)
            {
                t1.Add(var);
            }
            return t1;
        }

        public static List<string> GetItemsListString(ItemCollection objectCollection)
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
