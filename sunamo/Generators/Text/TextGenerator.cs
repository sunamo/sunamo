using sunamo.Helpers.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// TextWriterList - instance
/// TextBuilder - instance
/// TextOutputGenerator - instance
/// TextGenerator - static
/// </summary>
public static class TextGenerator
{
    public static string GenerateListWithPercent(Dictionary<string, IEnumerable<string>> p)
    {
        int overall = 0;

        foreach (var item in p)
        {
            overall += item.Value.Count();
        }

        PercentCalculator pc = new PercentCalculator(overall);

        TextOutputGenerator tog = new TextOutputGenerator();

        var withoutLast = p.Take(p.Count() - 1);

        int p2 = 0;
        int p3 = 0;

        KeyValuePair<string, IEnumerable<string>> kvp = p.Last();

        Dictionary<string, int> percent2 = new Dictionary<string, int>();

        foreach (var item in withoutLast)
        {
            p2 = pc.PercentFor(item.Value.Count(), false);

            p3 += p2;

            percent2.Add(item.Key, p2);
        }

        p2 = pc.PercentFor(kvp.Value.Count(), false);
        p3 += p2;
        percent2.Add(kvp.Key, p2);

        int largest = 0;
        string keyLargest = null;

        if (p3 != 0)
        {
            foreach (var item in percent2)
            {
                if (item.Value > largest)
                {
                    largest = item.Value;
                    keyLargest = item.Key;
                    break;
                }
            }

            percent2[keyLargest] = percent2[keyLargest] + (100 - p3);
        }

        foreach (var item in withoutLast)
        {
            //tog.List(withoutLast.First(d => d.Key == item.Key).Value, item.Key + " (" + item.Value + "%)");
            tog.List(item.Value, item.Key + " (" + percent2[item.Key] + "%)");
        }

        //p2 = pc.PercentFor(kvp.Value.Count(), false);

        tog.List(kvp.Value, kvp.Key + " (" + (100- p2) + "%)");
        return tog.ToString() ;
    }
}