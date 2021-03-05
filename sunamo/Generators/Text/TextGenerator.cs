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

        KeyValuePair<string, IEnumerable<string>> kvp = p.Last();

        foreach (var item in withoutLast)
        {
            p2 = pc.PercentFor(item.Value.Count(), false);
            
            tog.List(item.Value, item.Key + " (" + p2 + "%)");
        }

        p2 = pc.PercentFor(kvp.Value.Count(), false);

        tog.List(kvp.Value, kvp.Key + " (" + p2 + "%)");



        return tog.ToString() ;
    }
}
