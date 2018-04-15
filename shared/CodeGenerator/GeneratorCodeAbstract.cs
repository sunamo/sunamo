using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.CodeGenerator
{
    public abstract class GeneratorCodeAbstract
    {
        protected InstantSB sb = new InstantSB(" ");

        /// <summary>
        /// Use ToString() instead of public access
        /// </summary>
        protected string Final = "";

        public override string ToString()
        {
            string vr = sb.ToString();
            sb = new InstantSB(" ");
            return vr;
        }

        public void AddTab2(int pocetTab, string text)
        {
            sb.AddItem((object)AddTab(pocetTab, text));
        }

        /// <summary>
        /// Za voláním této metody pokud ukončuje nějaký celek jako jsou metody, valstnosti nebo konstruktor je vhodné volat ještě sb.AppendLine() - to platí pro metody v této třídě
        /// Ukončí složenou závorku a přidá nový řádek
        /// 
        /// </summary>
        public void EndBrace(int pocetTab)
        {
            //sb.AppendLine();
            AddTab(pocetTab);
            //sb.AppendLine();
            sb.AppendLine("}");

        }

        /// <summary>
        /// Přidá nový řádek, složenou závorku 
        /// Je to jediná zdejší metoda která na začátku přidává nový řádek.
        /// </summary>
        public void StartBrace(int pocetTab)
        {
            sb.AppendLine();
            AddTab(pocetTab);
            sb.AppendLine("{");
            //sb.AppendLine();
        }

        public void AddTab(int pocetTab)
        {
            //pocetTab += 1;
            for (int i = 0; i < pocetTab; i++)
            {
                sb.AddItem((object)Consts.tab);
            }
        }

        public void StartParenthesis()
        {
            sb.AddItem((object)"(");
        }

        public void EndParenthesis()
        {
            sb.AddItem((object)")");
        }

        public void AppendLine(int pocetTab, string p, params object[] p2)
        {
            if (p2.Length != 0)
            {
                sb.AppendLine(AddTab(pocetTab, string.Format(p, p2)));
            }
            else
            {
                sb.AppendLine(AddTab(pocetTab, p));
            }
        }

        public void Append(int pocetTab, string p, params object[] p2)
        {
            if (p2.Length != 0)
            {
                sb.AddItem(AddTab(pocetTab, string.Format(p, p2)));
            }
            else
            {
                sb.AddItem(AddTab(pocetTab, p));
            }
            string sbn = sb.ToString();
        }

        public static string AddTab(int pocetTab, string text)
        {
            var radky = SH.GetLines(text);
            for (int i = 0; i < radky.Count; i++)
            {
                radky[i] = radky[i].Trim();
                for (int y = 0; y < pocetTab; y++)
                {
                    radky[i] = Consts.tab + radky[i];
                }
            }
            string vr = SH.JoinString(Environment.NewLine, radky);
            return vr;
        }

        public void AssignValue(int pocetTab, string objectName, string variable, object value, bool addToHyphens)
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
            AssignValue(pocetTab, objectName, variable, vs, addToHyphens);
        }
    }
}
