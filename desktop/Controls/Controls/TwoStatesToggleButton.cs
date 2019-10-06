using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
public static partial class TwoStatesToggleButton
{
    static Dictionary<ToggleButton, bool? > previousCheched = new Dictionary<ToggleButton, bool? >();
    public static int Count
    {
        get
        {
            return previousCheched.Count;
        }
    }

    public static void SetInitialChecked(ToggleButton tb, bool check)
    {
        tb.IsChecked = check;
        if (!previousCheched.ContainsKey(tb))
        {
            previousCheched.Add(tb, check);
        }
        else
        {
            throw new Exception(RLData.en["YouCannotCallSetInitialCheckedTwiceForTheSameToggleButton"]);
        }
    }

    /// <summary>
    /// musí se volat vždy jako první věc v metodě Click
    /// </summary>
    /// <param name = "tb"></param>
    /// <returns></returns>
    public static void AfterClick(ToggleButton tb)
    {
        bool save = !((bool)previousCheched[tb]);
        previousCheched[tb] = save;
        //}
        tb.IsChecked = save;
    }

}