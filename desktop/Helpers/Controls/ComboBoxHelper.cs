using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public class ComboBoxHelper
{
    bool tagy = true;
    protected ComboBox cb = null;

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

    public static void SetFocus(ComboBox comboBox1)
    {
        Keyboard.Focus(comboBox1);
    }

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
                cb.Items.Add(item);
                if (i == 0)
                {
                    cb.SelectedIndex = 0;
                    
                }
                i++;
            }
            
        }

        void tsddb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedO = cb.SelectedItem;
            cb.ToolTip = originalToolTipText + " " + SelectedO.ToString();
        }

    public void AddValuesOfArrayAsItems(params object[] o)
    {
        AddValuesOfArrayAsItems(null, o);
    }

    /// <summary>
    /// A1 was handler of MouseDown, now without using. Use second method without A1. 
    /// </summary>
    /// <param name="eh"></param>
    /// <param name="o"></param>
    public void AddValuesOfArrayAsItems(RoutedEventHandler eh, params object[] o)
        {
        var enu = CA.ToList<object>(o);
            int i = 0;
            foreach (object item in enu)
            {
                cb.Items.Add(item);
                i++;
            }
        }

    public void AddValuesOfIntAsItems(RoutedEventHandler eh, int initialValue, int resizeOf, int degrees)
    {
        AddValuesOfIntAsItems(initialValue, resizeOf, degrees);
    }

    /// <summary>
    /// A1 was handler of MouseDown, now without using. Use second method without A1. 
    /// </summary>
    /// <param name="eh"></param>
    /// <param name="initialValue"></param>
    /// <param name="resizeOf"></param>
    /// <param name="degrees"></param>
    public void AddValuesOfIntAsItems(int initialValue, int resizeOf, int degrees)
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
                cb.Items.Add(item);
                y++;
            }
        }

    /// <summary>
    /// A2 zda se má do SelectedO uložit tsmi.Tag nebo jen tsmi
    /// </summary>
    /// <param name="tsddb"></param>
    /// <param name="tagy"></param>
        public ComboBoxHelper(ComboBox tsddb)
        {
            this.cb = tsddb;
            tsddb.SelectionChanged += tsddb_SelectionChanged;
        }
}
