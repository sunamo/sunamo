using desktop.Helpers;
using sunamo.Essential;
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
        static Type type = typeof(TextBoxHelper);
        
        static Dictionary<int, double> averageNumberWidthOnFontSize = new Dictionary<int, double>();
        static Dictionary<int, double> averageCharWidthOnFontSize = new Dictionary<int, double>();

        public static bool validated
        {
            set
            {
                TextBoxExtensions.validated = value;
            }
            get
            {
                return TextBoxExtensions.validated;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int VisibleLineCount(TextBox txt)
        {
            var f = txt.GetFirstVisibleLineIndex();
            var l = txt.GetLastVisibleLineIndex();

            return l - f;
        }

        public static void RegisterHighlightAllTextBox()
        {
            EventManager.RegisterClassHandler(typeof(TextBox),
         TextBox.GotFocusEvent,
         new RoutedEventHandler(TextBox_GotFocus));
            
        }

        static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        /// <summary>
        /// tag is not needed, value is obtained through []
        /// Tag here is mainly for comment what data control hold 
        /// </summary>
        /// <param name="tag"></param>
        public static TextBox Get(ControlInitData d)
        {
            TextBox txt = new TextBox();
            
            ControlHelper.SetForeground(txt, d.foreground);

            if (d.imagePath != null)
            {
                ThrowExceptions.IsNotNull(Exc.GetStackTrace(), type, Exc.CallingMethod(), "d.imagePath", d.imagePath);
            }
            if (d.OnClick != null)
            {
                ThrowExceptions.IsNotNull(Exc.GetStackTrace(), type, Exc.CallingMethod(), "d.OnClick", d.OnClick);
            }

            txt.Name = d.name;
            // Set up NaN due to fill all available size
            txt.Width = double.NaN;
            txt.Tag = d.tag;
            txt.ToolTip = d.tooltip;
            txt.Text = d.text;
            if (d.OnTextChange != null)
            {
                txt.TextChanged += d.OnTextChange;
            }
            
            return txt;
        }

        private static void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

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
#if DEBUG
            ////////DebugLogger.Instance.WriteLine($"Line: {line} txt lines: {txt.LineCount}");
#endif
            //try
            //{
            txt.SelectionStart = 0;
            
            ScrollToLineWorking(txt, line);
            //}
            //catch (Exception ex)
            //{
            //}
            // I had combobox which was focused by ctrl+1. But then lost focus due to next line
            //txt.Focus();
        }

        private static void ScrollToLineWorking(TextBox txt, int line)
        {

            try
            {
                /*Snažil jsem opravit 
                 * problém s napovídáním, mám git status, napíšu git, znovu mi to doplní status, mezerník a GetCharacterIndexFromLineIndex(Int32 lineIndex)\r\n   at desktop.TextBoxHelper.ScrollToLineWorking(TextBox txt, Int32 line)\r\n   at desktop.TextBoxHelper.ScrollToLin. 
                 * poprvé se to projevilo, podruhé už ne, tak to jednoduše zakomentuji
                 */ 
                txt.SelectionStart = txt.GetCharacterIndexFromLineIndex(line);
            }
            catch (Exception ex)
            {
                return;
            }
            txt.SelectionLength = txt.GetLineLength(line);
            txt.CaretIndex = txt.SelectionStart;
            txt.ScrollToLine(line);
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

        /// <summary>
        /// Instead of this use instance 
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="control"></param>
        /// <param name="trim"></param>
        public static void Validate(object tb, TextBox control, ref ValidateData d)
        {
            control.Validate(tb, ref d);
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