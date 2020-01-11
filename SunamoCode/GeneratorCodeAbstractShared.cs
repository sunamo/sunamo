using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract partial class GeneratorCodeAbstract{
    public void AddTab2(int tabCount, string text)
    {
        sb.AddItem((object)AddTab(tabCount, text));
    }

    /// <summary>
    /// Za voláním této metody pokud ukončuje nějaký celek jako jsou metody, valstnosti nebo konstruktor je vhodné volat ještě sb.AppendLine() - to platí pro metody v této třídě
    /// Ukončí složenou závorku a přidá nový řádek
    /// 
    /// </summary>
    public void EndBrace(int tabCount)
    {
        //sb.AppendLine();
        AddTab(tabCount);
        //sb.AppendLine();
        sb.AppendLine(AllStrings.cbr);

    }

    /// <summary>
    /// Přidá nový řádek, složenou závorku 
    /// Je to jediná zdejší metoda která na začátku přidává nový řádek.
    /// </summary>
    public void StartBrace(int tabCount)
    {
        // Line always ending previous command
        //sb.AppendLine();
        AddTab(tabCount);
        sb.AppendLine(AllStrings.cbl);
        //sb.AppendLine();
    }

    public void StartParenthesis()
    {
        sb.AddItem((object)AllStrings.lb);
    }

    public void EndParenthesis()
    {
        sb.AddItem((object)AllStrings.rb);
    }

    public void AppendLine()
    {
        sb.AppendLine();
    }

    public void AppendLine(int tabCount, string p, params object[] p2)
    {
        if (p2.Length != 0)
        {
            sb.AppendLine(AddTab(tabCount, SH.Format2(p, p2)));
        }
        else
        {
            sb.AppendLine(AddTab(tabCount, p));
        }
    }

    public void Append(int tabCount, string p, params object[] p2)
    {
        if (p2.Length != 0)
        {
            sb.AddItem(AddTab(tabCount, SH.Format2(p, p2)));
        }
        else
        {
            sb.AddItem(AddTab(tabCount, p));
        }
        string sbn = sb.ToString();
    }

    public void AssignValue(int tabCount, string objectName, string variable, object value, bool addToHyphens)
    {
        string vs = null;
        if (value.GetType() == typeof(bool))
        {
            vs = value.ToString().ToLower();
        }
        else
        {
            vs = value.ToString();
        }
        AssignValue(tabCount, objectName, variable, vs, addToHyphens);
    }

    protected InstantSB sb = new InstantSB(AllStrings.space);

    /// <summary>
    /// Use ToString() instead of public access
    /// </summary>
    protected string Final = "";

    public override string ToString()
    {
        string vr = sb.ToString();
        sb = new InstantSB(AllStrings.space);
        return vr;
    }

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