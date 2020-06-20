using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Must use SelectionChanged of ComboBoxHelper, not ComboBox. Otherwise first in called in Control, then is set into Selected* properties and app goes wrong!!
/// </summary>
public partial class ComboBoxHelper
{
    bool tagy = true;

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

    public static object ValueFromTWithNameOrObject(object o)
    {
        if (o is TWithName<object>)
        {
            return ((TWithName<object>)o).t;
        }
        return o;
    }

    public static void SetFocus(ComboBox comboBox1)
    {
        Keyboard.Focus(comboBox1);
    }



    public void AddValuesOfEnumAsItems<T>() where T : struct
    {
        var arr = EnumHelper.GetValues<T>();
        AddValuesOfEnumAsItems(arr);
    }

    public void AddValuesOfEnumAsItems(IEnumerable bs)
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


    public void AddValuesOfEnumerableAsItems(IEnumerable l)
    {
        AddValuesOfArrayAsItems(null, null, l);
    }

    public void AddValuesOfArrayAsItems(params object[] o)
    {
        AddValuesOfArrayAsItems(null, o);
    }

    /// <summary>
    /// A1 is out of using - set null
    /// </summary>
    /// <param name="eh"></param>
    /// <param name="o"></param>
    public void AddValuesOfArrayAsItems(RoutedEventHandler eh, params object[] o)
    {
        AddValuesOfArrayAsItems(null, eh, o);
    }


    /// <summary>
    /// A1 can be null
    /// A2 was handler of MouseDown, now without using - set null. 
    /// </summary>
    /// <param name="eh"></param>
    /// <param name="o"></param>
    public void AddValuesOfArrayAsItems(Func<object, string> toMakeNameInTWithName, RoutedEventHandler eh, params object[] o)
    {
        var enu = CA.ToList<object>(o);
        // cant add here because A1 will try cast added string to Encoding and throw exception
        //if (enu[0].ToString().Trim() != string.Empty)
        //{
        //    enu.Insert(0, string.Empty);
        //}
        int i = 0;
        foreach (object item in enu)
        {
            if (toMakeNameInTWithName != null)
            {
                TWithName<object> t = new TWithName<object>();
                t.name = toMakeNameInTWithName.Invoke(item);
                t.t = item;
                cb.Items.Add(t);
            }
            else
            {
                cb.Items.Add(item);
            }

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
}