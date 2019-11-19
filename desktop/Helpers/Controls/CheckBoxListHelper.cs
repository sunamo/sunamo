using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class CheckBoxListHelper
{
    public static IEnumerable<int> CheckedIndexes(IEnumerable<CheckBox> chbs)
    {
        //chbs[0].IsChecked = true;
        var indexes = chbs.Select((v, i) => new { v, i });
        var where = indexes.Where(x => BTS.GetValueOfNullable(x.v.IsChecked));
        List<int> v2 = new List<int>();
        foreach (var item in where)
        {
            v2.Add(item.i);
        }
        //var select = where.Select(x => x.i);
        //where.SelectMany<int>(d => d.;
        return v2;
    }

    //
    public static Dictionary<StackPanel, bool> CheckedContentDict(IEnumerable<CheckBox> chbs)
    {
        var unticked = UncheckedContent(chbs);
        var ticked = CheckedContent(chbs);

        Dictionary<StackPanel, bool> result = new Dictionary<StackPanel, bool>();

        foreach (var item in unticked)
        {
            result.Add(item, false);
        }

        foreach (var item in ticked)
        {
            result.Add(item, true);
        }

        return result;
    }

    /// <summary>
    /// Return StackPanel which have as only one child TextBlock
    /// </summary>
    /// <param name="chbs"></param>
    /// <returns></returns>
    public static IEnumerable<StackPanel> UncheckedContent(IEnumerable<CheckBox> chbs)
    {
        //chbs[0].IsChecked = true;
        var indexes = chbs.Select((v, i) => new { v, i });
        var where = indexes.Where(x => !BTS.GetValueOfNullable(x.v.IsChecked));
        return where.Select(d => d.v.Content).Cast<StackPanel>().ToList();
    }

    /// <summary>
    /// Return StackPanel which have as only one child TextBlock
    /// </summary>
    /// <param name="chbs"></param>
    /// <returns></returns>
    public static IEnumerable<StackPanel> CheckedContent(IEnumerable<CheckBox> chbs)
    {
        //chbs[0].IsChecked = true;
        var indexes = chbs.Select((v, i) => new { v, i });
        var where = indexes.Where(x => BTS.GetValueOfNullable(x.v.IsChecked));
        return where.Select(d => d.v.Content).Cast<StackPanel>().ToList();
    }

    /// <summary>
    /// Return StackPanel which have as only one child TextBlock
    /// </summary>
    /// <param name="chbs"></param>
    /// <returns></returns>
    public static List<StackPanel> AllContent(IEnumerable<CheckBox> chbs)
    {
        var d = chbs.Select(e => e.Content);
        var result = CA.ToListString(d);
        return result.Cast<StackPanel>().ToList();
    }
}

