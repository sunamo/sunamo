using desktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    public class TextBoxHelper
    {
        static Dictionary<int, double> averageNumberWidthOnFontSize = new Dictionary<int, double>();
        static Dictionary<int, double> averageCharWidthOnFontSize = new Dictionary<int, double>();

        static TextBoxHelper()
        {
            TextBoxHelper.InicializeWidths();
        }

        public static int GetLineLength(TextBox txt, int line)
        {
            // Counting from 0
            return txt.GetLineLength(line);
        }

        /// <summary>
        /// Get number of chars before
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int GetCharacterIndexFromLineIndex(TextBox txt, int line)
        {
            // Counting from 0
            return txt.GetCharacterIndexFromLineIndex(line);
        }

        public static string GetLineText(TextBox txt, int line)
        {
            // Counting from 0
            return txt.GetLineText(line);
        }

        /// <summary>
        /// line is from 0
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="line"></param>
        public static void ScrollToLine(TextBox txt, int line)
        {
            
            txt.SelectionStart = txt.GetCharacterIndexFromLineIndex(line);
            txt.SelectionLength = txt.GetLineLength(line);
            txt.CaretIndex = txt.SelectionStart;
            txt.ScrollToLine(line);
            txt.Focus();
        }

        public static void InicializeWidths()
        {
            StackPanel p = new StackPanel();
            TextBox txtTest = new TextBox();
            txtTest.MinWidth = 0;
            Dictionary<int, double> charWidth = new Dictionary<int, double>();

            double? d = null;
            for (char i = 'a'; i <= 'z'; i++)
            {
                txtTest = new TextBox();
                txtTest.Text = i.ToString();
                txtTest.Measure(ControlHelper.SizePositiveInfinity);
                txtTest.Arrange(new Rect(0, 0, txtTest.DesiredSize.Width, txtTest.DesiredSize.Height));
                txtTest.UpdateLayout();
                charWidth.Add(i, txtTest.ActualWidth);

                if (d == null)
                {
                    d = txtTest.ActualWidth;
                }
                else
                {
                    if (txtTest.ActualWidth > d.Value)
                    {
                        d = txtTest.ActualWidth;
                    }
                }

            }
            double ave = 0;

            double sum = 0;
            // Nejdříve vypočtu průměrnou velikost při FontSize=100
            foreach (var item in charWidth)
            {
                sum += item.Value;
            }
            ave = sum / charWidth.Count;
            // Pak vydělím 100
            ave /= 100;
            // Násobím 1-100(velikost písma) předchozím výsledkem - dostanu šířku textboxu při velikosti písma ai
            Dictionary<int, double> aweWidthFor = new Dictionary<int, double>();
            for (int i = 1; i < 101; i++)
            {
                aweWidthFor.Add(i, i * ave);
            }

            for (int i = 1; i < 101; i++)
            {
                txtTest = new TextBox();
                p.Children.Add(txtTest);
                txtTest.Text = "1";
                
                txtTest.FontSize = i;
                txtTest.Measure(ControlHelper.SizePositiveInfinity);
                averageNumberWidthOnFontSize.Add(i, txtTest.DesiredSize.Width);
                p.Children.Remove(txtTest);
            }
            p.Visibility = Visibility.Collapsed;
        }

        public static double GetOptimalWidthForCountOfChars(int count, bool alsoLetters, TextBox txt)
        {
            double countDouble = (double)count;
            double copy = (double)(int)txt.FontSize;
            if (copy != txt.FontSize)
            {
                copy++;
            }
            int copyInt = (int)copy;
            Dictionary<int, double> dict = null;
            if (alsoLetters)
            {

                dict = averageCharWidthOnFontSize;
            }
            else
            {
                dict = averageNumberWidthOnFontSize;
            }
            if (!dict.ContainsKey(copyInt))
            {
                copyInt = dict.Count;
            }
            return dict[copyInt] * countDouble;
        }
    }
}
