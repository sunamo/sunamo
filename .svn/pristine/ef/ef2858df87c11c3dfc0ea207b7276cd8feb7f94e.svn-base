using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
namespace desktop
{
    public delegate void updateContentOfLabel(Label lbl, object c);
    public delegate void updateBorderBrushOfBorder(Border b, Brush br);
    public delegate Brush getBorderBrushOfBorder(Border b);
    public delegate void updateProgressBarWpf(System.Windows.Controls.ProgressBar pb, double value);
    public delegate void updateTextBlockText(TextBlock lbl, string text);
    public delegate void appendToTextBlock(TextBlock lbl, string text);

    public delegate void changeVisibilityUIElementWpf(UIElement uie, Visibility v);
    public delegate void updateContentOfStatusBarItem(StatusBarItem sbi, object o);
    public delegate void appendToTextBox(TextBox lbl, string text);
    public delegate void insertToListBoxWpf(ListBox lb, int index, object o);
    public delegate void setDataContext(FrameworkElement fe, object o);
    public delegate object getDataContext(FrameworkElement fe);
    public delegate void setEnabled(UIElement uie, bool ed);
    public delegate object getSelectedItemSelector(Selector cb);
    public delegate void setItemsSourceOfItemsControl(ItemsControl ic, IEnumerable o);
    public delegate void setCaretIndexOfTextBox(TextBox txt, int caretIndex);
    public delegate void focusTextBox(TextBox txt);
    public delegate string getTextOfTextBox(TextBox txt);
    public delegate void scrollToEndTextBox(TextBox txt);
    public delegate object getItemAtIndexInSelector(Selector s, int dex);
    public delegate void setSelectedItemSelector(Selector s, object item);
    public delegate void updateLayoutOfUIElement(UIElement uie);
    //public delegate ListBoxItem getListBoxItemFromObject(ListBox lb, object )
    public partial class IH
    {
        public static changeVisibilityUIElementWpf delegateChangeVisibilityUIElementWpf = null;
        public static insertToListBoxWpf delegateInsertToListBoxWpf = null;
        public static updateContentOfLabel delegateUpdateContentOfLabel = null;
        public static updateBorderBrushOfBorder delegateUpdateBorderBrushOfBorder = null;
        public static getBorderBrushOfBorder delegateGetBorderBrushOfBorder = null;
        public static updateProgressBarWpf delegateUpdateProgressBarWpf = null;
        public static updateTextBlockText delegateUpdateTextBlockText = null;
        public static appendToTextBlock delegateAppendToTextBlock = null;
        public static updateContentOfStatusBarItem delegateUpdateContentOfStatusBarItem = null;
        public static appendToTextBox delegateAppendToTextBox = null;
        public static setDataContext delegateSetDataContext = null;
        public static getDataContext delegateGetDataContext = null;
        public static setEnabled delegateSetEnabled = null;
        public static getSelectedItemSelector delegateGetSelectedItemSelector = null;
        public static setItemsSourceOfItemsControl delegateSetItemsSourceOfItemsControl = null;
        public static setCaretIndexOfTextBox delegateSetCaretIndexOfTextBox = null;
        public static focusTextBox delegateFocusTextBox = null;
        public static getTextOfTextBox delegateGetTextOfTextBox = null;
        public static scrollToEndTextBox delegateScrollToEndTextBox = null;
        public static getItemAtIndexInSelector delegateGetItemAtIndexInSelector = null;
        public static setSelectedItemSelector delegateSetSelectedItemSelector = null;
        public static updateLayoutOfUIElement delegateUpdateLayoutOfUIElement = null;
        public static ListBoxItem getListBoxItemFromObject = null;
            //
        static IH()
        {
            delegateChangeVisibilityUIElementWpf = new changeVisibilityUIElementWpf(updateVisibility);
            delegateInsertToListBoxWpf = new insertToListBoxWpf(insertToListBoxWpfValue);
            delegateUpdateContentOfLabel = new updateContentOfLabel(updateContentOfLabelValue);
            delegateUpdateBorderBrushOfBorder = new updateBorderBrushOfBorder(updateBorderBrushOfBorderValue);
            delegateGetBorderBrushOfBorder = new getBorderBrushOfBorder(getBorderBrushOfBorderValue);
            delegateUpdateProgressBarWpf = new updateProgressBarWpf(updateProgressBarWpfValue);
            delegateUpdateTextBlockText = new updateTextBlockText(updateTextBlockText);
            delegateAppendToTextBlock = new appendToTextBlock(appendToTextBlockText);
            delegateUpdateContentOfStatusBarItem = new updateContentOfStatusBarItem(updateContentOfStatusBarItemValue);
            delegateAppendToTextBox = new appendToTextBox(appendToTextBoxText);
            delegateSetDataContext = new setDataContext(setDataContextObject);
            delegateGetDataContext = new getDataContext(getDataContextObject);
            delegateSetEnabled = new setEnabled(setEnabledBool);
            delegateGetSelectedItemSelector = new getSelectedItemSelector(getSelectedItemSelector);
            delegateSetItemsSourceOfItemsControl = new setItemsSourceOfItemsControl(setItemsSourceOfItemsControlM);
            delegateSetCaretIndexOfTextBox = new setCaretIndexOfTextBox(setCaretIndexOfTextBox);
            delegateFocusTextBox = new focusTextBox(focusTextBox);
            delegateGetTextOfTextBox = new getTextOfTextBox(getTextOfTextBox);
            delegateScrollToEndTextBox = new scrollToEndTextBox(scrollToEndTextBox);
            delegateGetItemAtIndexInSelector = new getItemAtIndexInSelector(getItemAtIndexInSelector);
            delegateSetSelectedItemSelector = new setSelectedItemSelector(setSelectedItemSelector);
            delegateUpdateLayoutOfUIElement = new updateLayoutOfUIElement(updateLayoutOfUIElement);

        }

        public static void updateLayoutOfUIElement(UIElement uie)
        {
            uie.UpdateLayout();
        }

        public static void setSelectedItemSelector(Selector s, object item)
        {
            s.SelectedItem = item;
        }

        public static object getItemAtIndexInSelector(Selector s, int dex)
        {
            return s.Items[dex];
        }

        public static void scrollToEndTextBox(TextBox txt)
        {
            txt.ScrollToEnd();
        }

        public static string getTextOfTextBox(TextBox txt)
        {
            return txt.Text;
        }

        public static void focusTextBox(TextBox txt)
        {
            txt.Focus();
        }

        public static void updateContentOfLabelValue(Label l, object content)
        {
            l.Content = content;
        }

        public static void setCaretIndexOfTextBox(TextBox txt, int caretIndex)
        {
            txt.CaretIndex = caretIndex;
        }

        public static void updateBorderBrushOfBorderValue(Border b, Brush br)
        {
            b.BorderBrush = br;
        }

        public static Brush getBorderBrushOfBorderValue(Border b)
        {
            return b.BorderBrush;
        }

        public static void updateContentOfStatusBarItemValue(StatusBarItem sbi, object o)
        {
            sbi.Content = o;
        }

        static void setItemsSourceOfItemsControlM(ItemsControl ic, IEnumerable o)
        {
            ic.ItemsSource = o;
        }

        /// <summary>
        /// Tato metoda je pro WPF, updateProgressBarValue pak na WF
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="value"></param>
        public static void updateProgressBarWpfValue(System.Windows.Controls.ProgressBar pb, double value)
        {
            if (value > 100)
            {
                //throw new Exception("Hodnota pro ProgressBar nemůže být vyšší než 100.");
                value = 100;
            }
            pb.Value = value;
        }

        public static void updateTextBlockText(TextBlock lbl, string text)
        {
            lbl.Text = text;
            lbl.ToolTip = text;
        }

        public static void appendToTextBlockText(TextBlock lbl, string text)
        {
            lbl.Text = lbl.Text + " " + text;
            lbl.ToolTip = lbl.Text;
        }

        public static void appendToTextBoxText(TextBox tb, string text)
        {
            tb.Text = tb.Text + " " + text;
            tb.ToolTip = tb.Text;
        }

        //
        public static void updateVisibility(UIElement ui, Visibility vis)
        {
            ui.Visibility = vis;
        }

        

        public static void insertToListBoxWpfValue(ListBox lb, int index, object o)
        {
            lb.Items.Insert(index, o);
        }

        public static void setDataContextObject(FrameworkElement fw, object dc)
        {
            fw.DataContext = dc;
        }

        public static object getDataContextObject(FrameworkElement fw)
        {
            return fw.DataContext;
        }

        public static void setEnabledBool(UIElement ui, bool b)
        {
            ui.IsEnabled = b;
        }

        public static object getSelectedItemSelector(Selector s)
        {
            return s.SelectedItem;
        }
    }
}
