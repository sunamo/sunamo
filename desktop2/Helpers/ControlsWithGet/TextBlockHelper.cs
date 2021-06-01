
using desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
/// <summary>
/// TBH je pro TextBox, TBH2 pro TextBlock
/// </summary>
public partial class TextBlockHelper 
{
    public static void InicializeWidths()
    {
        StackPanel p = new StackPanel();
        TextBlock txtTest = new TextBlock();
        txtTest.MinWidth = 0;
        Dictionary<int, double> charWidth = new Dictionary<int, double>();
        double? d = null;
        for (char i = 'a'; i <= 'z'; i++)
        {
            txtTest = new TextBlock();
            txtTest.Text = i.ToString();
            txtTest.Measure(ControlsHelperValues.SizePositiveInfinity);
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
        // Násobím 1-100(velikost písma) předchozím výsledkem - dostanu šířku TextBlocku při velikosti písma ai
        Dictionary<int, double> aweWidthFor = new Dictionary<int, double>();
        for (int i = 1; i < 101; i++)
        {
            aweWidthFor.Add(i, i * ave);
        }

        for (int i = 1; i < 101; i++)
        {
            txtTest = new TextBlock();
            p.Children.Add(txtTest);
            txtTest.Text = "1";
            txtTest.FontSize = i;
            txtTest.Measure(ControlsHelperValues.SizePositiveInfinity);
            averageNumberWidthOnFontSize.Add(i, txtTest.DesiredSize.Width);
            p.Children.Remove(txtTest);
        }

        p.Visibility = Visibility.Collapsed;
    }

    public static void AddTextPostColon(TextBlock tbSmtpServer)
    {
        tbSmtpServer.Text += ": ";
    }

    static TextBlockHelper()
    {
        InicializeWidths();
    }

    static Dictionary<int, double> averageNumberWidthOnFontSize = new Dictionary<int, double>();
    static Dictionary<int, double> averageCharWidthOnFontSize = new Dictionary<int, double>();
    public static double GetOptimalWidthForCountOfChars(int count, bool alsoLetters, TextBlock txt)
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

    public static void SetTextPostColonXlf(TextBlock lblStatusDownload, string xlf)
    {
        SetTextPostColonXlf(lblStatusDownload, sess.i18n(xlf));
    }

    public static void SetTextPostColon(TextBlock lblStatusDownload, string status)
    {
        status = SH.PostfixIfNotEmpty(status, AllStrings.colon);
        TextBlockHelper.SetText(lblStatusDownload, status);
    }

}