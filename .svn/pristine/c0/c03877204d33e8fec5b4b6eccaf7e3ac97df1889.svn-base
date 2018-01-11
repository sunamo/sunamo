using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
public class ComboBoxHelper
{
    public static void AddRange2List(ComboBox cbInterpret, IList allInterprets)
    {
        for (int i = 0; i < allInterprets.Count; i++)
        {
            object o = allInterprets[i];
            if (o != null)
            {
                if (o.ToString().Trim() != "")
                {
                    cbInterpret.Items.Add(o);

                }
            }


        }
    }

    protected ComboBox tsddb = null;
        
        protected string originalToolTipText = "";
        /// <summary>
        /// Objekt, ve kterém je vždy aktuální zda v tsddb něco je
        /// Takže se nelekni že to je promměná
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
                tsddb.Items.Add(item);
                if (i == 0)
                {
                    tsddb.SelectedIndex = 0;
                    
                }
                i++;
            }
            
        }

        void tsddb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedO = tsddb.SelectedItem;
            tsddb.ToolTip = originalToolTipText + " " + SelectedO.ToString();
        }



        public void AddValuesOfArrayAsItems(RoutedEventHandler eh, params object[] o)
        {
            int i = 0;
            foreach (object item in o)
            {
                tsddb.Items.Add(item);
                i++;
            }
        }

        public void AddValuesOfIntAsItems(RoutedEventHandler eh, int initialValue, int resizeOf, int degrees)
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
                tsddb.Items.Add(item);
                y++;
            }
        }

        bool tagy = true;

    /// <summary>
        /// A2 zda se má do SelectedO uložit tsmi.Tag nebo jen tsmi
    /// </summary>
    /// <param name="tsddb"></param>
    /// <param name="tagy"></param>
        public ComboBoxHelper(ComboBox tsddb)
        {
            this.tsddb = tsddb;
            tsddb.SelectionChanged += tsddb_SelectionChanged;
        }
}
