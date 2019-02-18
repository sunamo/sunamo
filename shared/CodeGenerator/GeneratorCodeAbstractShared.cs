using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract partial class GeneratorCodeAbstract{ 
public void AddTab(int tabCount)
        {
            //tabCount += 1;
            for (int i = 0; i < tabCount; i++)
            {
                sb.AddRaw((object)Consts.tab);
            }
        }
public static string AddTab(int tabCount, string text)
        {
            var radky = SH.GetLines(text);
            for (int i = 0; i < radky.Count; i++)
            {
                radky[i] = radky[i].Trim();
                for (int y = 0; y < tabCount; y++)
                {
                    radky[i] = Consts.tab + radky[i];
                }
            }
            string vr = SH.JoinString(Environment.NewLine, radky);
            return vr;
        }
}